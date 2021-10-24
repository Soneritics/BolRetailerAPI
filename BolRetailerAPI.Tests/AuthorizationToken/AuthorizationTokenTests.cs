using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.AuthorizationToken
{
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
            var authorizationToken = new BolRetailerApi.Models.Authorization.AuthorizationToken(
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
            var authorizationToken = new BolRetailerApi.Models.Authorization.AuthorizationToken(
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
            var authorizationToken = new BolRetailerApi.Models.Authorization.AuthorizationToken(
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
            var authorizationToken = new BolRetailerApi.Models.Authorization.AuthorizationToken(
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
