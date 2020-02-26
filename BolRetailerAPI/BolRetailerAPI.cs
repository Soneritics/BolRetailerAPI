using System.Net.Http;
using BolRetailerAPI.Endpoints;
using BolRetailerAPI.EndPoints;
using BolRetailerAPI.Models;
using BolRetailerAPI.Services;

namespace BolRetailerAPI
{
    /// <summary>
    /// Wrapper class for the Bol Retailer API services.
    /// </summary>
    public class BolRetailerApi
    {
        public RateLimits RateLimits { get; }
        protected readonly IEndPoints EndPoints;
        private readonly AuthorizationToken.AuthorizationToken _authorizationToken;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BolRetailerApi"/> class.
        /// Setup the necessary objects. This object can be used to inject via DI.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="testMode">if set to <c>true</c> [test mode].</param>
        public BolRetailerApi(string clientId, string clientSecret, bool testMode = false)
        {
            RateLimits = new RateLimits();
            EndPoints = testMode ? new TestEndPoints() : new EndPoints.EndPoints();
            _httpClient = new HttpClient();
            _authorizationToken = new AuthorizationToken.AuthorizationToken(clientId, clientSecret, TokenService);
        }

        /// <summary>
        /// Gets the token service.
        /// </summary>
        /// <value>
        /// The token service.
        /// </value>
        private ITokenService _tokenService;
        public ITokenService TokenService => _tokenService ??= new TokenService(_httpClient, EndPoints);

        /// <summary>
        /// Gets the orders service.
        /// </summary>
        /// <value>
        /// The orders service.
        /// </value>
        private OrdersService _ordersService;
        public OrdersService OrdersService => _ordersService ??= new OrdersService(_httpClient, EndPoints, _authorizationToken, RateLimits);
    }
}
