using System;
using System.Threading.Tasks;
using BolRetailerApi.Services.Authorization;

namespace BolRetailerApi.Models.Authorization;

/// <summary>
///     Authorization token implementation.
/// </summary>
/// <seealso cref="IAuthorizationToken" />
public class AuthorizationToken : IAuthorizationToken
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly ITokenService _tokenService;
    private Token _token;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AuthorizationToken" /> class.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <param name="clientSecret">The client secret.</param>
    /// <param name="tokenService">The token service.</param>
    public AuthorizationToken(string clientId, string clientSecret, ITokenService tokenService)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
        _tokenService = tokenService;
    }

    /// <summary>
    ///     Returns true if the token is (still) valid.
    /// </summary>
    /// <returns>
    ///     <c>true</c> if the token is valid; otherwise, <c>false</c>.
    /// </returns>
    public bool IsValid()
    {
        return _token != default && _token.ExpiresAt > DateTime.Now;
    }

    /// <summary>
    ///     Refreshes the token asynchronous.
    /// </summary>
    public async Task RefreshAsync()
    {
        _token = await _tokenService.GetTokenAsync(_clientId, _clientSecret);
    }

    /// <summary>
    ///     Gets the authentication bearer asynchronous.
    ///     If the token is not valid, a new token is generated.
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetAuthenticationBearerAsync()
    {
        if (!IsValid())
            await RefreshAsync();

        return _token.access_token;
    }
}