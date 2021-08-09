[![Build Status](https://soneritics.visualstudio.com/Bol%20Retailer%20API/_apis/build/status/Soneritics.BolRetailerAPI?branchName=master)](https://soneritics.visualstudio.com/Bol%20Retailer%20API/_build/latest?definitionId=2&branchName=master)
![License](http://img.shields.io/badge/license-MIT-green.svg)

# BolRetailerAPI v5
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

// Get full detaild of a shipment
var shipmentDetails = await api.ShipmentService.GetShipmentByIdAsync(shipmentId);
```

## Current implementation status
The following has been implemented:
* Orders
  - Get all (open) orders
  - Get a single order's details
  - Cancel an order (item)
  - Ship an order (item)
* Shipments
  - Get shipment list
  - Get a single shipment's details

## Contributions are welcome
If you want to add new functionality, please make a PR to be reviewed.

If you want to implement new services, you might want to use the BOL.com Swagger to automatically generate the models.
To do so, run the following command in a Linux shell (or Ubuntu bash on Windows):

```bash
curl -X POST https://generator3.swagger.io/api/generate -H 'content-type: application/json' -d '{"specURL" : "https://api.bol.com/retailer/public/apispec/v5","lang" : "aspnetcore","type" : "CLIENT","codegenVersion" : "V3"}' --output swaggergen.zip
```