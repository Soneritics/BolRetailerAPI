using BolRetailerAPI.Endpoints;

namespace BolRetailerAPI.EndPoints
{
    /// <summary>
    /// Live endpoints.
    /// </summary>
    /// <seealso cref="BolRetailerAPI.Endpoints.IEndPoints" />
    public class EndPoints : IEndPoints
    {
        public string BaseUriLogin { get; } = "https://login.bol.com/";
        public string BaseUriApiCalls { get; } = "https://api.bol.com/";

        public string Token { get; } = "token?grant_type=client_credentials";
    }
}
