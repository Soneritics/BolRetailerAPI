using System;
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
    /// <param name="status">Fulfilment status</param>
    /// <param name="method">The method to handle the shipping (BOL/Retailer).</param>
    /// <param name="changedAfter">Only get changed after this date time</param>
    /// <returns>List of (reduced) orders.</returns>
    public async Task<IEnumerable<ReducedOrder>> GetOrdersAsync(
        int page = 1,
        FulfilmentStatus status = FulfilmentStatus.OPEN,
        Method method = Method.ALL,
        DateTime changedAfter = default)
    {
        var fulfilmentStatus = status switch
        {
            FulfilmentStatus.ALL => "ALL",
            FulfilmentStatus.OPEN => "OPEN",
            FulfilmentStatus.SHIPPED => "SHIPPED",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Status not allowed")
        };

        var fulfilmentMethod = method switch
        {
            Method.FBR => "FBR",
            Method.FBB => "FBB",
            Method.ALL => "ALL",
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, "Method not allowed")
        };

        var url = $"{EndPoints.BaseUriApiCalls}{EndPoints.Orders}?"
            + $"fulfilment-method={fulfilmentMethod}"
            + $"&status={fulfilmentStatus}"
            + $"&page={page}";

        if (changedAfter != default)
        {
            url += $"&latest-change-date={changedAfter:O}";
        }

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
    /// <returns>List of order items.</returns>
    public async Task<IEnumerable<OrderItemsWithQuantity>> GetOrderItemsAsync(string orderId)
    {
        var order = await GetOrderAsync(orderId);

        return order?.OrderItems?.Any() != true
            ? default
            : order.OrderItems.Select(oi => new OrderItemsWithQuantity(oi.OrderItemId, oi.Quantity));
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
        var orderItemIds = (await GetOrderItemsAsync(orderId)).ToList();

        if (orderItemIds.Count == 0)
            throw new NoOrderItemsInOrderException($"No order items found in order {orderId}");

        return await CancelOrderItemsAsync(orderItemIds.Select(oi => oi.OrderItemId), cancellationReason);
    }

    /// <summary>
    ///     Sets shipment state for order items.
    ///     Use this method when you purchased a BOL shipping label.
    /// </summary>
    /// <param name="orderItemIds">The order item ids.</param>
    /// <param name="shipmentReference">The shipment reference.</param>
    /// <param name="labelId">The label identifier.</param>
    /// <returns>StatusResponse.</returns>
    public async Task<StatusResponse> ShipOrderItemsAsync(
        IEnumerable<OrderItemsWithQuantity> orderItems,
        string shipmentReference,
        string labelId)
    {
        var shipmentRequest = new ShipmentRequest
        {
            OrderItems = orderItems,
            ShipmentReference = shipmentReference,
            ShippingLabelId = labelId
        };

        return await GetApiResult<StatusResponse>(
            HttpMethod.Post,
            $"{EndPoints.BaseUriApiCalls}{EndPoints.Shipments}",
            shipmentRequest
        );
    }

    /// <summary>
    ///     Sets shipment state for order items.
    ///     Use this method when you use your own transporter.
    /// </summary>
    /// <param name="orderItems">The order items.</param>
    /// <param name="transporterCode">The transporter code.</param>
    /// <param name="trackingCode">The tracking code.</param>
    /// <returns>StatusResponse.</returns>
    public async Task<StatusResponse> ShipOrderItemsAsync(
        IEnumerable<OrderItemsWithQuantity> orderItems,
        TransporterCode transporterCode,
        string trackingCode)
    {
        var shipmentRequest = new ShipmentRequest
        {
            OrderItems = orderItems,
            Transport = new TransportInstruction
            {
                TransporterCode = transporterCode.TransporterCodeValue,
                TrackAndTrace = trackingCode
            }
        };

        return await GetApiResult<StatusResponse>(
            HttpMethod.Post,
            $"{EndPoints.BaseUriApiCalls}{EndPoints.Shipments}",
            shipmentRequest
        );
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
    public async Task<StatusResponse> ShipOrderAsync(
        string orderId,
        string shipmentReference,
        string labelId)
    {
        var orderItems = (await GetOrderItemsAsync(orderId)).ToList();

        if (orderItems.Count == 0)
            throw new NoOrderItemsInOrderException($"No order items found in order {orderId}.");
        
        return await ShipOrderItemsAsync(
            orderItems,
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
    public async Task<StatusResponse> ShipOrderAsync(
        string orderId,
        TransporterCode transporterCode,
        string trackingCode)
    {
        var orderItems = (await GetOrderItemsAsync(orderId)).ToList();

        if (orderItems.Count == 0)
            throw new NoOrderItemsInOrderException($"No order items found in order {orderId}.");

        return await ShipOrderItemsAsync(
            orderItems,
            transporterCode,
            trackingCode
        );
    }
}