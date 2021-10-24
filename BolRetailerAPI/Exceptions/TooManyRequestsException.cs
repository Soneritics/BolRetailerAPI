using System;

namespace BolRetailerApi.Exceptions
{
    /// <summary>
    /// Request is throttled, so the Bol API returns a TooManyRequests status code.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class TooManyRequestsException : Exception
    {
    }
}
