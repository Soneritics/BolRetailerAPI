using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BolRetailerAPI.AuthorizationToken;
using BolRetailerAPI.Endpoints;

namespace BolRetailerAPI.Client
{
    /// <summary>
    /// Client base class for authenticated requests.
    /// </summary>
    /// <seealso cref="BolRetailerAPI.Client.ClientBase" />
    public abstract class AuthenticatedClientBase : ClientBase
    {
        private readonly IAuthorizationToken _authorizationToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedClientBase"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="endPoints">The end points.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        protected AuthenticatedClientBase(HttpClient httpClient, IEndPoints endPoints, IAuthorizationToken authorizationToken) : base(httpClient, endPoints)
        {
            _authorizationToken = authorizationToken;
        }

        /// <summary>
        /// Gets the HTTP request message with necessary authentication headers.
        /// </summary>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="post">The post.</param>
        /// <returns></returns>
        protected override async Task<HttpRequestMessage> GetHttpRequestMessage(HttpMethod httpMethod, string endPoint, object post = null)
        {
            var result = await base.GetHttpRequestMessage(httpMethod, endPoint, post);

            result.Headers.Accept.Clear();
            result.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.retailer.v3+json"));
            result.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                await _authorizationToken.GetAuthenticationBearerAsync()
            );
            
            return result;
        }
    }
}
