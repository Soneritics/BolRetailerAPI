using BolRetailerAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BolRetailerAPI.Tests.Models
{
    [TestClass]
    public class TokenTests
    {
        /// <summary>
        /// Test that the token expiration is calculated correctly.
        /// </summary>
        [TestMethod]
        public void Token_Expiration_Is_Correct()
        {
            var validityInSeconds = 3;

            var token = new Token()
            {
                expires_in = validityInSeconds
            };

            Assert.AreEqual(
                token.Created.AddSeconds(validityInSeconds).ToFileTime(), 
                token.ExpiresAt.ToFileTime()
            );
        }
    }
}
