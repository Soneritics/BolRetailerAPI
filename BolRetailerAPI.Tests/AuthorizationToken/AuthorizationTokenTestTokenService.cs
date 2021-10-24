using System.Threading.Tasks;
using BolRetailerApi.Models.Authorization;
using BolRetailerApi.Services.Authorization;

namespace Tests.AuthorizationToken
{
    /// <summary>
    /// Tests for the token service.
    /// </summary>
    /// <seealso cref="ITokenService" />
    public class AuthorizationTokenTestTokenService : ITokenService
    {
        /// <summary>
        /// Keep track of number of calls.
        /// </summary>
        public int Calls { get; set; } = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationTokenTestTokenService"/> class.
        /// </summary>
        public AuthorizationTokenTestTokenService()
        {
        }

        /// <summary>
        /// Gets a token. Always returns a token with a validity of 100 seconds.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <returns></returns>
        public async Task<Token> GetTokenAsync(string clientId, string clientSecret)
        {
            return await Task.Run(() => new Token()
            {
                access_token = $"test-access-token-{Calls++}",
                scope = "global",
                token_type = "bearer",
                expires_in = 10
            });
        }
    }
}