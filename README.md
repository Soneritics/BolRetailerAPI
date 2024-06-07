[![Build Status](https://soneritics.visualstudio.com/Bol%20Retailer%20API/_apis/build/status/Soneritics.BolRetailerAPI?branchName=master)](https://soneritics.visualstudio.com/Bol%20Retailer%20API/_build/latest?definitionId=2&branchName=master)
![License](http://img.shields.io/badge/license-MIT-green.svg)

# BolRetailerAPI v10
Bol.com Retailer API NuGet package.

## Bol.com retailer references
Documentation for the Bol.com retailer API can be found here:
* https://api.bol.com/retailer/public/Retailer-API/getting-started.html

## NuGet package
The API can be included in your project by installing the NuGet package:
[BolRetailerAPI](https://www.nuget.org/packages/BolRetailerAPI/)
(Make sure to install the correct version.)

## Example usage
```cs
// Preferably inject via DI
var bolApi = new BolRetailerApi(clientId, clientSecret, testMode);

// List all orders
var orders = await bolApi.OrdersService.GetOpenOrdersAsync();

// Get all the info of a specific order
var order = await bolApi.OrdersService.GetOrderAsync(orders.First().OrderId);

// Set shipment for a complete order
var shippedOrder = await api.OrdersService.ShipOrderAsync(
    order.OrderId,
    TransporterCode.Tnt,
    "3SABCD000000001"
);

// Get a list of shipment details
var shippedOrderDetails = await bolApi.ShipmentService.GetShipmentListForOrderAsync(order.OrderId);

// Get full details of a shipment
var shipmentDetails = await api.ShipmentService.GetShipmentByIdAsync(shipmentId);
```

For use in an Azure function, use the following code in your Startup:
```cs
public override void Configure(IFunctionsHostBuilder builder)
{
    builder
        .Services
        .AddHttpClient()
        .AddScoped<IBolRetailerApi>(sp =>
            new BolRetailerApi(
                Environment.GetEnvironmentVariable("ClientId"),
                Environment.GetEnvironmentVariable("ClientSecret"),
                sp.GetRequiredService<HttpClient>(),
                Environment.GetEnvironmentVariable("TestMode") == "1"
            )
        );
}
```

## Example project
Checkout the repo for an example project on how to use the API.
Create an appsettings.json file in the example project with the following settings if you don't want to enter them manually:

```json
{
  "clientId": "",
  "clientSecret":  ""
}
```

The example project uses the test endpoints, so no production data is altered.

## Current implementation status
The following has been implemented:
* Orders
  - Get all (open) orders
  - Get a single order's details
  - Cancel an order (item)
  - Ship an order (item)
* Shipments
  - Get shipment list~~~~
  - Get a single shipment's details
