using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BolRetailerAPI.AuthorizationToken;
using BolRetailerAPI.Client;
using BolRetailerAPI.Endpoints;
using BolRetailerAPI.Models.Orders;

namespace BolRetailerAPI.Services
{
    /// <summary>
    /// Orders API Service
    /// </summary>
    /// <seealso cref="BolRetailerAPI.Client.AuthenticatedClientBase" />
    public class OrdersService : AuthenticatedClientBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="endPoints">The end points.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        public OrdersService(HttpClient httpClient, IEndPoints endPoints, IAuthorizationToken authorizationToken) : base(httpClient, endPoints, authorizationToken)
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
    }
}
