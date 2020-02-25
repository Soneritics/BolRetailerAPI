using BolRetailerAPI.EndPoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BolRetailerAPI.Tests.Endpoints
{
    /// <summary>
    /// Tests for the TestEndPoints class
    /// </summary>
    [TestClass]
    public class TestEndPointTests
    {
        /// <summary>
        /// Ensures the test endpoint usage.
        /// </summary>
        [TestMethod]
        public void Ensure_Test_Endpoint_Usage()
        {
            var endpoints = new TestEndPoints();
            Assert.AreEqual(
                "https://api.bol.com/retailer-demo/",
                endpoints.BaseUriApiCalls
            );
        }
    }
}
