namespace BolRetailerAPI.EndPoints
{
    /// <summary>
    /// Test endpoints.
    /// </summary>
    /// <seealso cref="BolRetailerAPI.EndPoints.EndPoints" />
    public class TestEndPoints : EndPoints
    {
        public override string BaseUriApiCalls { get; } = "https://api.bol.com/retailer-demo/";
    }
}
