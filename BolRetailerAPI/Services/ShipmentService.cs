using System.Net.Http;
using System.Threading.Tasks;
using BolRetailerAPI.AuthorizationToken;
using BolRetailerAPI.Client;
using BolRetailerAPI.Endpoints;
using BolRetailerAPI.Models;
using BolRetailerAPI.Models.Shipments;

namespace BolRetailerAPI.Services
{
    /// <summary>
    /// Shipment service
    /// </summary>
    /// <seealso cref="BolRetailerAPI.Client.AuthenticatedClientBase" />
    public class ShipmentService : AuthenticatedClientBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShipmentService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="endPoints">The end points.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="rateLimits">The rate limits.</param>
        public ShipmentService(HttpClient httpClient, IEndPoints endPoints, IAuthorizationToken authorizationToken, RateLimits rateLimits = null) : base(httpClient, endPoints, authorizationToken, rateLimits)
        {
        }

        /// <summary>
        /// Gets the shipment list asynchronous.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public async Task<ReducedShipmentList> GetShipmentListAsync(string orderId, int page = 1)
        {
            return await GetApiResult<ReducedShipmentList>(
                HttpMethod.Get,
                $"{EndPoints.BaseUriApiCalls}{EndPoints.Shipments}?page={page}&order-id={orderId}"
            );
        }

        /// <summary>
        /// Gets the shipment by shipment id asynchronous.
        /// </summary>
        /// <param name="shipmentId">The shipment identifier.</param>
        /// <returns></returns>
        public async Task<Shipment> GetShipmentByIdAsync(int shipmentId)
        {
            return await GetApiResult<Shipment>(
                HttpMethod.Get,
                $"{EndPoints.BaseUriApiCalls}{EndPoints.Shipments}/{shipmentId}"
            );
        }
    }
}
