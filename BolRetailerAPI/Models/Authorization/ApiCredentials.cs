namespace BolRetailerApi.Models.Authorization
{
    /// <summary>
    /// API credentials DTO, only used within this package.
    /// </summary>
    internal class ApiCredentials
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}