namespace BolRetailerApi.Endpoints
{
    /// <summary>
    /// Test endpoints.
    /// </summary>
    /// <seealso cref="EndPoints" />
    public class TestEndPoints : EndPoints
    {
        public override string BaseUriApiCalls { get; } = "https://api.bol.com/retailer-demo/";
    }
}
