using System.Net.Http;
using BolRetailerApi.Endpoints;
using BolRetailerApi.Models.Authorization;
using BolRetailerApi.Models.Status;
using BolRetailerApi.Services;
using BolRetailerApi.Services.Authorization;

namespace BolRetailerApi;

/// <summary>
///     Wrapper class for the Bol Retailer API services.
/// </summary>
public class BolRetailerApi : IBolRetailerApi
{
    private readonly AuthorizationToken _authorizationToken;
    private readonly HttpClient _httpClient;
    protected readonly IEndPoints EndPoints;

    /// <summary>
    ///     Gets the orders service.
    /// </summary>
    /// <value>
    ///     The orders service.
    /// </value>
    private OrdersService _ordersService;

    /// <summary>
    ///     Gets the shipment service.
    /// </summary>
    /// <value>
    ///     The shipment service.
    /// </value>
    private ShipmentService _shipmentService;

    /// <summary>
    ///     Gets the token service.
    /// </summary>
    /// <value>
    ///     The token service.
    /// </value>
    private ITokenService _tokenService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BolRetailerApi" /> class.
    ///     Setup the necessary objects. This object can be used to inject via DI.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <param name="clientSecret">The client secret.</param>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="testMode">if set to <c>true</c> [test mode].</param>
    public BolRetailerApi(string clientId, string clientSecret, HttpClient httpClient, bool testMode = false)
    {
        RateLimits = new RateLimits();
        EndPoints = testMode ? new TestEndPoints() : new EndPoints();
        _httpClient = httpClient;
        _authorizationToken = new AuthorizationToken(clientId, clientSecret, TokenService);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BolRetailerApi" /> class.
    ///     Setup the necessary objects. This object can be used to inject via DI.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <param name="clientSecret">The client secret.</param>
    /// <param name="testMode">if set to <c>true</c> [test mode].</param>
    public BolRetailerApi(string clientId, string clientSecret, bool testMode = false)
        : this(clientId, clientSecret, new HttpClient(), testMode)
    {
    }

    public RateLimits RateLimits { get; }
    public ITokenService TokenService => _tokenService ??= new TokenService(_httpClient, EndPoints);

    public OrdersService OrdersService =>
        _ordersService ??= new OrdersService(_httpClient, EndPoints, _authorizationToken, RateLimits);

    public ShipmentService ShipmentService =>
        _shipmentService ??= new ShipmentService(_httpClient, EndPoints, _authorizationToken, RateLimits);
}