using BolRetailerAPI.Endpoints;

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
}