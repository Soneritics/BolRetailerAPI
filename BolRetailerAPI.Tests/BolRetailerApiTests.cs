using BolRetailerAPI.Endpoints;
using BolRetailerAPI.EndPoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BolRetailerAPI.Tests
{
    /// <summary>
    /// Test class helper for the BolRetailerApi class.
    /// </summary>
    /// <seealso cref="BolRetailerAPI.BolRetailerApi" />
    public class BolRetailerApiTestClass : BolRetailerApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BolRetailerApiTestClass"/> class.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="testMode">if set to <c>true</c> [test mode].</param>
        public BolRetailerApiTestClass(string clientId, string clientSecret, bool testMode = false) : base(clientId, clientSecret, testMode)
        {
        }

        /// <summary>
        /// Expose the selected endpoints.
        /// </summary>
        /// <value>
        /// The get end points.
        /// </value>
        public IEndPoints GetEndPoints => EndPoints;
    }

    /// <summary>
    /// Tests for the BolRetailerApi class.
    /// </summary>
    [TestClass]
    public class BolRetailerApiTests
    {
        /// <summary>
        /// Test if the test endpoints are used when initiating with testmode = true
        /// </summary>
        [TestMethod]
        public void Init_Uses_Test_Endpoint()
        {
            var api = new BolRetailerApiTestClass("", "", true);
            Assert.IsInstanceOfType(api.GetEndPoints, typeof(TestEndPoints));
        }

        /// <summary>
        /// Test if the live endpoints are used when initiating with testmode = false
        /// </summary>
        [TestMethod]
        public void Init_Uses_Live_Endpoint_Explicit()
        {
            var api = new BolRetailerApiTestClass("", "", false);
            Assert.IsInstanceOfType(api.GetEndPoints, typeof(EndPoints.EndPoints));
        }

        /// <summary>
        /// Test if the live endpoints are used when initiating with no testmode provided.
        /// </summary>
        [TestMethod]
        public void Init_Uses_Live_Endpoint_Implicit()
        {
            var api = new BolRetailerApiTestClass("", "");
            Assert.IsInstanceOfType(api.GetEndPoints, typeof(EndPoints.EndPoints));
        }
    }
}
