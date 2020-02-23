using System.Net.Http;
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
            var postObject = new
            {
                client_id = clientId,
                client_secret = clientSecret
            };

            return await GetApiResult<Token>(
                HttpMethod.Post,
                $"{EndPoints.BaseUriLogin}{EndPoints.Token}",
                postObject
            );
        }
    }
}
