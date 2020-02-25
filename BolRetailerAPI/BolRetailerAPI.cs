using System.Net.Http;
using BolRetailerAPI.Endpoints;
using BolRetailerAPI.EndPoints;
using BolRetailerAPI.Services;

namespace BolRetailerAPI
{
    /// <summary>
    /// Wrapper class for the Bol Retailer API services.
    /// </summary>
    public class BolRetailerApi
    {
        private readonly AuthorizationToken.AuthorizationToken _authorizationToken;
        private readonly IEndPoints _endPoints;
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
            _endPoints = testMode ? new TestEndPoints() : new EndPoints.EndPoints();
            _httpClient = new HttpClient();
            _authorizationToken = new AuthorizationToken.AuthorizationToken(clientId, clientSecret, TokenService);
        }

        /// <summary>
        /// Gets the token service.
        /// </summary>
        /// <value>
        /// The token service.
        /// </value>
        public ITokenService TokenService => new TokenService(_httpClient, _endPoints);

        /// <summary>
        /// Gets the orders service.
        /// </summary>
        /// <value>
        /// The orders service.
        /// </value>
        public OrdersService OrdersService => new OrdersService(_httpClient, _endPoints, _authorizationToken);
    }
}
