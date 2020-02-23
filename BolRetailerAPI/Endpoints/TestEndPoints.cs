namespace BolRetailerAPI.EndPoints
{
    /// <summary>
    /// Test endpoints.
    /// </summary>
    /// <seealso cref="BolRetailerAPI.EndPoints.EndPoints" />
    public class TestEndPoints : EndPoints
    {
        public new string BaseUriLogin { get; } = "https://login.bol.com/retailer-demo/";
        public new string BaseUriApiCalls { get; } = "https://api.bol.com/retailer-demo/";
    }
}
