using System.Threading.Tasks;
using BolRetailerAPI.Models.Authorization;

namespace BolRetailerAPI.Services.Authorization
{
    /// <summary>
    /// TokenService interface.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Gets a token.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <returns></returns>
        Task<Token> GetTokenAsync(string clientId, string clientSecret);
    }
}