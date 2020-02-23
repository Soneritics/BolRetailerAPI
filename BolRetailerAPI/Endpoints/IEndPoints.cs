namespace BolRetailerAPI.Endpoints
{
    /// <summary>
    /// Endpoint definitions.
    /// </summary>
    public interface IEndPoints
    {
        string BaseUriLogin { get; }
        string BaseUriApiCalls { get; }

        string Token { get; }
    }
}
