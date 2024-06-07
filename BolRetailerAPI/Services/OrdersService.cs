using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BolRetailerApi.Client;
using BolRetailerApi.Endpoints;
using BolRetailerApi.Exceptions;
using BolRetailerApi.Models.Authorization;
using BolRetailerApi.Models.Enum;
using BolRetailerApi.Models.Request;
using BolRetailerApi.Models.Response;
using BolRetailerApi.Models.Response.Orders;
using BolRetailerApi.Models.Status;

namespace BolRetailerApi.Services;

/// <summary>
///     Orders API Service
/// </summary>
/// <seealso cref="AuthenticatedClientBase" />
public class OrdersService : AuthenticatedClientBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OrdersService" /> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="endPoints">The end points.</param>
    /// <param name="authorizationToken">The authorization token.</param>
    /// <param name="rateLimits">The rate limits.</param>
    public OrdersService(
        HttpClient httpClient,
        IEndPoints endPoints,
        IAuthorizationToken authorizationToken,
        RateLimits rateLimits = null)
        : base(httpClient, endPoints, authorizationToken, rateLimits)
    {
    }

    /// <summary>
    ///     Gets the orders from the API.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="onlyOpenOrders">if set to <c>true</c> [only open orders].</param>
    /// <param name="method">The method to handle the shipping (BOL/Retailer).</param>
    /// <returns>List of (reduced) orders.</returns>
    public async Task<IEnumerable<ReducedOrder>> GetOrdersAsync(
        int page = 1,
        bool onlyOpenOrders = false,
        Method method = Method.FBR)
    {
        var status = onlyOpenOrders ? "OPEN" : "ALL";
        var fulfilmentMethod = method == Method.FBR ? "FBR" : "FBB";

        var url =
            $"{EndPoints.BaseUriApiCalls}{EndPoints.Orders}?"
            + $"fulfilment-method={fulfilmentMethod}"
            + $"&status={status}"
            + $"&page={page}";

        var result = await GetApiResult<OrdersResponse>(HttpMethod.Get, url);

        return result.Orders;
    }

    /// <summary>
    ///     Gets a single order's details.
    /// </summary>
    /// <param name="orderId">The order identifier.</param>
    /// <returns>An Order entity.</returns>
    public async Task<Order> GetOrderAsync(string orderId)
    {
        return await GetApiResult<Order>(
            HttpMethod.Get,
            $"{EndPoints.BaseUriApiCalls}{EndPoints.SingleOrder}{orderId}"
        );
    }

    /// <summary>
    ///     Gets the order item ids from the items of an order.
    /// </summary>
    /// <param name="orderId">The order identifier.</param>
    /// <returns>List of ids.</returns>
    public async Task<IEnumerable<string>> GetOrderItemIdsAsync(string orderId)
    {
        var order = await GetOrderAsync(orderId);

        return order?.OrderItems?.Any() != true
            ? default
            : order.OrderItems.Select(oi => oi.OrderItemId);
    }

    /// <summary>
    ///     Cancels the specified order items from ONE order.
    /// </summary>
    /// <param name="orderItemIds">The order item ids.</param>
    /// <param name="cancellationReason">The cancellation reason.</param>
    /// <returns>StatusResponse.</returns>
    public async Task<IEnumerable<StatusResponse>> CancelOrderItemsAsync(
        IEnumerable<string> orderItemIds,
        CancellationReason cancellationReason = null)
    {
        var result = new List<StatusResponse>();

        foreach (var orderItemId in orderItemIds)
            result.Add(await CancelOrderItemAsync(orderItemId, cancellationReason));

        return result;
    }

    /// <summary>
    ///     Cancels a single order item.
    /// </summary>
    /// <param name="orderItemId">The order item identifier.</param>
    /// <param name="cancellationReason">The cancellation reason.</param>
    /// <returns>StatusResponse.</returns>
    public async Task<StatusResponse> CancelOrderItemAsync(
        string orderItemId,
        CancellationReason cancellationReason = null)
    {
        cancellationReason ??= new CancellationReason();
        var cancellationRequest = new CancellationRequest
        {
            OrderItems = new[]
            {
                new OrderItemCancellation
                {
                    OrderItemId = orderItemId,
                    ReasonCode = cancellationReason.ReasonValue
                }
            }
        };

        return await GetApiResult<StatusResponse>(
            HttpMethod.Put,
            $"{EndPoints.BaseUriApiCalls}{EndPoints.Orders}/cancellation",
            cancellationRequest
        );
    }

    /// <summary>
    ///     Cancels a complete the order.
    ///     To do this, first the detailed order info is fetched.
    ///     Keep this into account with the rate limits.
    /// </summary>
    /// <param name="orderId">The order identifier.</param>
    /// <param name="cancellationReason">The cancellation reason.</param>
    /// <returns>StatusResponse.</returns>
    /// <exception cref="NoOrderItemsInOrderException">No order items found in order {orderId}.</exception>
    public async Task<IEnumerable<StatusResponse>> CancelOrderAsync(
        string orderId,
        CancellationReason cancellationReason = null)
    {
        var orderItemIds = await GetOrderItemIdsAsync(orderId);

        if (orderItemIds?.Any() != true)
            throw new NoOrderItemsInOrderException($"No order items found in order {orderId}.");

        return await CancelOrderItemsAsync(orderItemIds, cancellationReason);
    }

    /// <summary>
    ///     Sets shipment state for order items.
    ///     Use this method when you purchased a BOL shipping label.
    /// </summary>
    /// <param name="orderItemIds">The order item ids.</param>
    /// <param name="shipmentReference">The shipment reference.</param>
    /// <param name="labelId">The label identifier.</param>
    /// <returns>StatusResponse.</returns>
    public async Task<IEnumerable<StatusResponse>> ShipOrderItemsAsync(
        IEnumerable<string> orderItemIds,
        string shipmentReference,
        string labelId)
    {
        var result = new List<StatusResponse>();
        var orderItems = orderItemIds.Select(oi => new ItemId(oi));

        foreach (var orderItem in orderItems)
        {
            var shipmentRequest = new ShipmentRequest
            {
                OrderItems = new[] { orderItem },
                ShipmentReference = shipmentReference,
                ShippingLabelId = labelId
            };

            result.Add(
                await GetApiResult<StatusResponse>(
                    HttpMethod.Put,
                    $"{EndPoints.BaseUriApiCalls}{EndPoints.Orders}/shipment",
                    shipmentRequest
                )
            );
        }

        return result;
    }

    /// <summary>
    ///     Sets shipment state for order items.
    ///     Use this method when you use your own transporter.
    /// </summary>
    /// <param name="orderItemIds">The order item ids.</param>
    /// <param name="transporterCode">The transporter code.</param>
    /// <param name="trackingCode">The tracking code.</param>
    /// <returns>StatusResponse.</returns>
    public async Task<IEnumerable<StatusResponse>> ShipOrderItemsAsync(
        IEnumerable<string> orderItemIds,
        TransporterCode transporterCode,
        string trackingCode)
    {
        var result = new List<StatusResponse>();
        var orderItems = orderItemIds.Select(oi => new ItemId(oi));

        foreach (var orderItem in orderItems)
        {
            var shipmentRequest = new ShipmentRequest
            {
                OrderItems = new[] { orderItem },
                Transport = new TransportInstruction
                {
                    TransporterCode = transporterCode.TransporterCodeValue,
                    TrackAndTrace = trackingCode
                }
            };

            result.Add(
                await GetApiResult<StatusResponse>(
                    HttpMethod.Put,
                    $"{EndPoints.BaseUriApiCalls}{EndPoints.Orders}/shipment",
                    shipmentRequest
                )
            );
        }

        return result;
    }

    /// <summary>
    ///     Sets the shipment state for a complete order.
    ///     To do this, first the detailed order info is fetched.
    ///     Keep this into account with the rate limits.
    ///     Also, only use this method when you purchased a BOL shipping label.
    /// </summary>
    /// <param name="orderId">The order identifier.</param>
    /// <param name="shipmentReference">The shipment reference.</param>
    /// <param name="labelId">The label identifier.</param>
    /// <returns>StatusResponse.</returns>
    /// <exception cref="NoOrderItemsInOrderException">No order items found in order {orderId}.</exception>
    public async Task<IEnumerable<StatusResponse>> ShipOrderAsync(
        string orderId,
        string shipmentReference,
        string labelId)
    {
        var orderItemIds = await GetOrderItemIdsAsync(orderId);

        if (orderItemIds?.Any() != true)
            throw new NoOrderItemsInOrderException($"No order items found in order {orderId}.");

        return await ShipOrderItemsAsync(
            orderItemIds,
            shipmentReference,
            labelId
        );
    }

    /// <summary>
    ///     Sets the shipment state for a complete order.
    ///     To do this, first the detailed order info is fetched.
    ///     Keep this into account with the rate limits.
    ///     Also, only use this method when you use your own transporter.
    /// </summary>
    /// <param name="orderId">The order identifier.</param>
    /// <param name="transporterCode">The transporter code.</param>
    /// <param name="trackingCode">The tracking code.</param>
    /// <returns>StatusResponse.</returns>
    /// <exception cref="NoOrderItemsInOrderException">No order items found in order {orderId}.</exception>
    public async Task<IEnumerable<StatusResponse>> ShipOrderAsync(
        string orderId,
        TransporterCode transporterCode,
        string trackingCode)
    {
        var orderItemIds = await GetOrderItemIdsAsync(orderId);

        if (orderItemIds?.Any() != true)
            throw new NoOrderItemsInOrderException($"No order items found in order {orderId}.");

        return await ShipOrderItemsAsync(
            orderItemIds,
            transporterCode,
            trackingCode
        );
    }
}