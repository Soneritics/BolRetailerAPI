using BolRetailerApi.Endpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests;

/// <summary>
///     Tests for the BolRetailerApi class.
/// </summary>
[TestClass]
public class BolRetailerApiTests
{
    /// <summary>
    ///     Test if the test endpoints are used when initiating with testmode = true
    /// </summary>
    [TestMethod]
    public void Init_Uses_Test_Endpoint()
    {
        var api = new BolRetailerApiTestClass("", "", true);
        Assert.IsInstanceOfType(api.GetEndPoints, typeof(TestEndPoints));
    }

    /// <summary>
    ///     Test if the live endpoints are used when initiating with testmode = false
    /// </summary>
    [TestMethod]
    public void Init_Uses_Live_Endpoint_Explicit()
    {
        var api = new BolRetailerApiTestClass("", "");
        Assert.IsInstanceOfType(api.GetEndPoints, typeof(EndPoints));
    }

    /// <summary>
    ///     Test if the live endpoints are used when initiating with no testmode provided.
    /// </summary>
    [TestMethod]
    public void Init_Uses_Live_Endpoint_Implicit()
    {
        var api = new BolRetailerApiTestClass("", "");
        Assert.IsInstanceOfType(api.GetEndPoints, typeof(EndPoints));
    }
}