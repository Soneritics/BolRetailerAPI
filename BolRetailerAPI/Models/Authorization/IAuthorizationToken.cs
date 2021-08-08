using System.Threading.Tasks;

namespace BolRetailerAPI.Models.Authorization
{
    /// <summary>
    /// Authorization token interface.
    /// </summary>
    public interface IAuthorizationToken
    {
        /// <summary>
        /// Returns true if the token is (still) valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the token is valid; otherwise, <c>false</c>.
        /// </returns>
        bool IsValid();

        /// <summary>
        /// Refreshes the token asynchronous.
        /// </summary>
        /// <returns></returns>
        Task RefreshAsync();

        /// <summary>
        /// Gets the authentication bearer asynchronous.
        /// If the token is not valid, a new token is generated.
        /// </summary>
        /// <returns></returns>
        Task<string> GetAuthenticationBearerAsync();
    }
}
