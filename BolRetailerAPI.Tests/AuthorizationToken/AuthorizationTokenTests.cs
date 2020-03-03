using System.Threading.Tasks;
using BolRetailerAPI.Models;
using BolRetailerAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BolRetailerAPI.Tests.AuthorizationToken
{
    /// <summary>
    /// Tests for the token service.
    /// </summary>
    /// <seealso cref="BolRetailerAPI.Services.ITokenService" />
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

    /// <summary>
    /// Tests for the authorization token class.
    /// </summary>
    [TestClass]
    public class AuthorizationTokenTests
    {
        /// <summary>
        /// Test if the token is expired when expiration date/time is &lt;= now
        /// </summary>
        [TestMethod]
        public void Expired_Token()
        {
            var tokenService = new AuthorizationTokenTestTokenService();
            var authorizationToken = new BolRetailerAPI.AuthorizationToken.AuthorizationToken(
                "",
                "",
                tokenService
            );

            var tokenIsValid = authorizationToken.IsValid();

            Assert.IsFalse(tokenIsValid);
        }

        /// <summary>
        /// Test if the token is valid after a refresh
        /// </summary>
        [TestMethod]
        public void Token_Is_Valid_After_Refresh()
        {
            var tokenService = new AuthorizationTokenTestTokenService();
            var authorizationToken = new BolRetailerAPI.AuthorizationToken.AuthorizationToken(
                "",
                "",
                tokenService
            );

            authorizationToken.RefreshAsync().GetAwaiter().GetResult();
            var tokenIsValid = authorizationToken.IsValid();

            Assert.IsTrue(tokenIsValid);
            Assert.AreEqual(1, tokenService.Calls);
        }

        /// <summary>
        /// Test if getting the bearer automatically refreshes the token when it's expired.
        /// </summary>
        [TestMethod]
        public void Get_Bearer_Automatically_Refreshes_Token()
        {
            var tokenService = new AuthorizationTokenTestTokenService();
            var authorizationToken = new BolRetailerAPI.AuthorizationToken.AuthorizationToken(
                "",
                "",
                tokenService
            );

            var bearer = authorizationToken.GetAuthenticationBearerAsync().GetAwaiter().GetResult();

            Assert.AreEqual("test-access-token-0", bearer);
            Assert.AreEqual(1, tokenService.Calls);
        }

        /// <summary>
        /// Test if a token requests is performed in one API call.
        /// </summary>
        [TestMethod]
        public void Token_Refresh_Costs_One_Api_Call()
        {
            var refreshTimes = 25;
            var tokenService = new AuthorizationTokenTestTokenService();
            var authorizationToken = new BolRetailerAPI.AuthorizationToken.AuthorizationToken(
                "",
                "",
                tokenService
            );

            for (var i = 0; i < refreshTimes; i++)
                authorizationToken.RefreshAsync().GetAwaiter().GetResult();

            Assert.AreEqual(refreshTimes, tokenService.Calls);
        }
    }
}
