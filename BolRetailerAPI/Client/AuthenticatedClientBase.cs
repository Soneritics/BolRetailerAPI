using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BolRetailerApi.Endpoints;
using BolRetailerApi.Models.Authorization;
using BolRetailerApi.Models.Status;

namespace BolRetailerApi.Client
{
    /// <summary>
    /// Client base class for authenticated requests.
    /// </summary>
    /// <seealso cref="ClientBase" />
    public abstract class AuthenticatedClientBase : ClientBase
    {
        public RateLimits RateLimits { get; private set; }
        private readonly IAuthorizationToken _authorizationToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedClientBase"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="endPoints">The end points.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="rateLimits">The rate limits.</param>
        protected AuthenticatedClientBase(
            HttpClient httpClient,
            IEndPoints endPoints,
            IAuthorizationToken authorizationToken,
            RateLimits rateLimits = null)
            : base(httpClient, endPoints)
        {
            _authorizationToken = authorizationToken;
            RateLimits = rateLimits ?? new RateLimits();
        }

        /// <summary>
        /// Gets the HTTP request message with necessary authentication headers.
        /// </summary>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="post">The post.</param>
        /// <returns></returns>
        protected override async Task<HttpRequestMessage> GetHttpRequestMessage(
            HttpMethod httpMethod,
            string endPoint,
            object post = null)
        {
            var result = await base.GetHttpRequestMessage(httpMethod, endPoint, post);

            result.Headers.Accept.Clear();
            result.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.retailer.v7+json"));
            result.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                await _authorizationToken.GetAuthenticationBearerAsync()
            );
            
            return result;
        }

        /// <summary>
        /// Processes the HTTP headers.
        /// Override because the rate limits are processed as well.
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        protected override void ProcessHttpHeaders(HttpResponseMessage httpResponseMessage)
        {
            base.ProcessHttpHeaders(httpResponseMessage);
            
            if (httpResponseMessage.Headers.Contains("x-ratelimit-remaining"))
            {
                RateLimits = new RateLimits()
                {
                    Remaining = int.Parse(httpResponseMessage.Headers.GetValues("x-ratelimit-remaining").First()),
                    Limit = int.Parse(httpResponseMessage.Headers.GetValues("x-ratelimit-limit").First()),
                    ResetsAt = DateTime.Now.AddSeconds(int.Parse(httpResponseMessage.Headers.GetValues("x-ratelimit-reset").First())),
                    RetryAfter = httpResponseMessage.Headers.RetryAfter?.Delta?.Seconds
                };
            }
            else if (httpResponseMessage.Headers.RetryAfter != null)
            {
                var retryAfter = httpResponseMessage.Headers.RetryAfter?.Delta?.Seconds ?? 10;
                RateLimits = new RateLimits()
                {
                    Remaining = 0,
                    Limit = 0,
                    ResetsAt = DateTime.Now.AddSeconds(retryAfter),
                    RetryAfter = retryAfter
                };
            }
        }
    }
}
