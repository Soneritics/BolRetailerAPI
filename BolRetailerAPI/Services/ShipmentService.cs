using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BolRetailerApi.Client;
using BolRetailerApi.Endpoints;
using BolRetailerApi.Models.Authorization;
using BolRetailerApi.Models.Enum;
using BolRetailerApi.Models.Response;
using BolRetailerApi.Models.Response.Shipments;
using BolRetailerApi.Models.Status;

namespace BolRetailerApi.Services;

/// <summary>
///     Shipment service
/// </summary>
/// <seealso cref="AuthenticatedClientBase" />
public class ShipmentService : AuthenticatedClientBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ShipmentService" /> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="endPoints">The end points.</param>
    /// <param name="authorizationToken">The authorization token.</param>
    /// <param name="rateLimits">The rate limits.</param>
    public ShipmentService(
        HttpClient httpClient,
        IEndPoints endPoints,
        IAuthorizationToken authorizationToken,
        RateLimits rateLimits = null)
        : base(httpClient, endPoints, authorizationToken, rateLimits)
    {
    }

    /// <summary>
    ///     Gets the shipment list of a specific order.
    ///     Max 50 items per page.
    /// </summary>
    /// <param name="orderId">The order identifier.</param>
    /// <param name="page">The page.</param>
    /// <returns>List of ReducedShipment for the specified order.</returns>
    public async Task<IEnumerable<ReducedShipment>> GetShipmentListForOrderAsync(
        string orderId,
        int page = 1)
    {
        var url = $"{EndPoints.BaseUriApiCalls}{EndPoints.Shipments}?page={page}&order-id={orderId}";
        var result = await GetApiResult<ShipmentResponse>(HttpMethod.Get, url);

        return result.Shipments;
    }

    /// <summary>
    ///     Gets the shipment list.
    ///     Max 50 items per page.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="method">The method to handle the shipping (BOL/Retailer).</param>
    /// <returns>List of ReducedShipment for the specified distribution party.</returns>
    public async Task<IEnumerable<ReducedShipment>> GetShipmentListAsync(
        int page = 1,
        Method method = Method.FBR)
    {
        var fulfilmentMethod = method == Method.FBR ? "FBR" : "FBB";
        var url =
            $"{EndPoints.BaseUriApiCalls}{EndPoints.Shipments}"
            + $"?page={page}"
            + $"&fulfilment-method={fulfilmentMethod}";

        var result = await GetApiResult<ShipmentResponse>(HttpMethod.Get, url);

        return result.Shipments;
    }

    /// <summary>
    ///     Gets the shipment by shipment id asynchronous.
    /// </summary>
    /// <param name="shipmentId">The shipment identifier.</param>
    /// <returns>Shipment.</returns>
    public async Task<Shipment> GetShipmentByIdAsync(string shipmentId)
    {
        return await GetApiResult<Shipment>(
            HttpMethod.Get,
            $"{EndPoints.BaseUriApiCalls}{EndPoints.Shipments}/{shipmentId}"
        );
    }
}