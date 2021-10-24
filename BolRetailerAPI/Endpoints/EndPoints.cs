namespace BolRetailerApi.Endpoints
{
    /// <summary>
    /// Live endpoints.
    /// </summary>
    /// <seealso cref="IEndPoints" />
    public class EndPoints : IEndPoints
    {
        public virtual string BaseUriLogin { get; } = "https://login.bol.com/";
        public virtual string BaseUriApiCalls { get; } = "https://api.bol.com/retailer/";

        public string Token { get; } = "token?grant_type=client_credentials";
        public string Orders { get; } = "orders";
        public string SingleOrder { get; } = "orders/";
        public string Shipments { get; } = "shipments";
    }
}
