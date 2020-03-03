using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BolRetailerAPI.AuthorizationToken;
using BolRetailerAPI.Client;
using BolRetailerAPI.Endpoints;
using BolRetailerAPI.Enum;
using BolRetailerAPI.Exceptions;
using BolRetailerAPI.Models;
using BolRetailerAPI.Models.Orders;
using BolRetailerAPI.Models.Requests;

namespace BolRetailerAPI.Services
{
    /// <summary>
    /// Orders API Service
    /// </summary>
    /// <seealso cref="BolRetailerAPI.Client.AuthenticatedClientBase" />
    public class OrdersService : AuthenticatedClientBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersService" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="endPoints">The end points.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="rateLimits">The rate limits.</param>
        public OrdersService(HttpClient httpClient, IEndPoints endPoints, IAuthorizationToken authorizationToken, RateLimits rateLimits = null) : base(httpClient, endPoints, authorizationToken, rateLimits)
        {
        }

        /// <summary>
        /// Internal class to map the open orders request.
        /// </summary>
        internal class OpenOrdersResponse
        {
            public List<ReducedOrder> orders { get; set; }
        }

        /// <summary>
        /// Gets the open orders asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReducedOrder>> GetOpenOrdersAsync()
        {
            var result = await GetApiResult<OpenOrdersResponse>(
                HttpMethod.Get,
                $"{EndPoints.BaseUriApiCalls}{EndPoints.OpenOrders}"
            );

            return result.orders;
        }

        /// <summary>
        /// Gets a single order asynchronous.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        public async Task<Order> GetOrderAsync(string orderId)
        {
            return await GetApiResult<Order>(
                HttpMethod.Get,
                $"{EndPoints.BaseUriApiCalls}{EndPoints.SingleOrder}{orderId}"
            );
        }

        /// <summary>
        /// Cancels an order item asynchronous.
        /// </summary>
        /// <param name="orderItemId">The order item identifier.</param>
        /// <param name="cancellationReason">The cancellation reason.</param>
        /// <returns></returns>
        public async Task<StatusResponse> CancelOrderItemAsync(string orderItemId, CancellationReason cancellationReason = null)
        {
            if (cancellationReason == default)
                cancellationReason = new CancellationReason();

            return await GetApiResult<StatusResponse>(
                HttpMethod.Put,
                $"{EndPoints.BaseUriApiCalls}{EndPoints.SingleOrder}{orderItemId}/cancellation",
                new { reasonCode = cancellationReason.ReasonValue }
            );
        }

        /// <summary>
        /// Cancels a complete order asynchronous.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="cancellationReason">The cancellation reason.</param>
        /// <returns></returns>
        /// <exception cref="Exception">No order items found.</exception>
        public async Task<List<StatusResponse>> CancelOrderAsync(string orderId, CancellationReason cancellationReason = null)
        {
            var order = await GetOrderAsync(orderId);
            if (order?.OrderItems?.Any() != true)
                throw new NoOrderItemsInOrderException($"No order items found in order {orderId}.");

            var result = new List<StatusResponse>();
            var taskList = order.OrderItems.Select(
                oi => Task.Run(async () =>
                {
                    result.Add(await CancelOrderItemAsync(oi.OrderItemId, cancellationReason));
                })
            );
            await Task.WhenAll(taskList);

            return result;
        }

        /// <summary>
        /// Sends shipping information of an order item asynchronous.
        /// </summary>
        /// <param name="orderItemId">The order item identifier.</param>
        /// <param name="shipmentData">The shipment data.</param>
        /// <returns></returns>
        public async Task<StatusResponse> ShipOrderItemAsync(string orderItemId, ShipmentData shipmentData)
        {
            return await GetApiResult<StatusResponse>(
                HttpMethod.Put,
                $"{EndPoints.BaseUriApiCalls}{EndPoints.SingleOrder}{orderItemId}/shipment",
                shipmentData
            );
        }

        /// <summary>
        /// Sends shipping information of an order asynchronous.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="shipmentData">The shipment data.</param>
        /// <returns></returns>
        /// <exception cref="NoOrderItemsInOrderException">No order items found in order {orderId}.</exception>
        public async Task<List<StatusResponse>> ShipOrderAsync(string orderId, ShipmentData shipmentData)
        {
            var order = await GetOrderAsync(orderId);
            if (order?.OrderItems?.Any() != true)
                throw new NoOrderItemsInOrderException($"No order items found in order {orderId}.");

            var result = new List<StatusResponse>();
            var taskList = order.OrderItems.Select(
                oi => Task.Run(async () =>
                {
                    result.Add(await ShipOrderItemAsync(oi.OrderItemId, shipmentData));
                })
            );
            await Task.WhenAll(taskList);

            return result;
        }
    }
}
