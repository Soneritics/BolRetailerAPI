using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BolRetailerAPI.Client;
using BolRetailerAPI.Endpoints;
using BolRetailerAPI.Models;

namespace BolRetailerAPI.Services
{
    /// <summary>
    /// TokenService to get tokens from the Bol API.
    /// </summary>
    /// <seealso cref="BolRetailerAPI.Client.ClientBase" />
    /// <seealso cref="BolRetailerAPI.Services.ITokenService" />
    public class TokenService : ClientBase, ITokenService
    {
        /// <summary>
        /// API credentials DTO, only used within this class.
        /// </summary>
        internal class ApiCredentials
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
    }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="endPoints">The end points.</param>
        public TokenService(HttpClient httpClient, IEndPoints endPoints) : base(httpClient, endPoints)
        {
        }

        /// <summary>
        /// Gets a token.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <returns></returns>
        public async Task<Token> GetTokenAsync(string clientId, string clientSecret)
        {
            var postObject = new ApiCredentials()
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            };

            return await GetApiResult<Token>(
                HttpMethod.Post,
                $"{EndPoints.BaseUriLogin}{EndPoints.Token}",
                postObject
            );
        }

        /// <summary>
        /// Set Basic authorization to fetch the token.
        /// </summary>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="post">The post.</param>
        /// <returns></returns>
        protected override async Task<HttpRequestMessage> GetHttpRequestMessage(HttpMethod httpMethod, string endPoint, object post = null)
        {
            var result = await base.GetHttpRequestMessage(httpMethod, endPoint);
            var credentials = (ApiCredentials)post;

            result.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{credentials.ClientId}:{credentials.ClientSecret}"))
            );

            return result;
        }
    }
}
