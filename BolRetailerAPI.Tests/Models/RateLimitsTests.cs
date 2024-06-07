using System;
using BolRetailerApi.Models.Status;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Models;

/// <summary>
///     Tests for the RateLimits model.
/// </summary>
[TestClass]
public class RateLimitsTests
{
    /// <summary>
    ///     Test if the rate limits are still valid and should not be refreshed.
    /// </summary>
    [TestMethod]
    public void Is_Valid()
    {
        var rateLimits = new RateLimits
        {
            ResetsAt = DateTime.Now.AddSeconds(10)
        };

        var isValid = rateLimits.IsStillValid();

        Assert.IsTrue(isValid);
    }

    /// <summary>
    ///     Test if RateLimits are not valid anymore because they have refreshed.
    /// </summary>
    [TestMethod]
    public void Is_Invalid()
    {
        var rateLimits = new RateLimits
        {
            ResetsAt = DateTime.Now
        };

        var isValid = rateLimits.IsStillValid();

        Assert.IsFalse(isValid);
    }
}