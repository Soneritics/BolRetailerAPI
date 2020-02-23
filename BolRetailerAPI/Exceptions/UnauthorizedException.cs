using System;

namespace BolRetailerAPI.Exceptions
{
    /// <summary>
    /// Token is invalid, so the Bol API returns a Unauthorized status code.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UnauthorizedException : Exception
    {
    }
}
