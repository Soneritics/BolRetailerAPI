using BolRetailerApi.Endpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Endpoints
{
    /// <summary>
    /// Tests for the EndPoints class
    /// </summary>
    [TestClass]
    public class EndPointTests
    {
        /// <summary>
        /// Ensures the live endpoint usage.
        /// </summary>
        [TestMethod]
        public void Ensure_Test_Endpoint_Usage()
        {
            var endpoints = new EndPoints();
            Assert.AreEqual(
                "https://api.bol.com/retailer/",
                endpoints.BaseUriApiCalls
            );
        }
    }
}
