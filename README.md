[![Build Status](https://soneritics.visualstudio.com/Bol%20Retailer%20API/_apis/build/status/Soneritics.BolRetailerAPI?branchName=master)](https://soneritics.visualstudio.com/Bol%20Retailer%20API/_build/latest?definitionId=2&branchName=master)
![License](http://img.shields.io/badge/license-MIT-green.svg)

# BolRetailerAPI
Bol.com Retailer API NuGet package.

## Bol.com retailer references
Documentation for the Bol.com retailer API can be found here:
* https://developers.bol.com/newretailerapiv3/
* https://api.bol.com/retailer/public/redoc/v3#tag/Process-Status

## NuGet package
The API can be included in your project by installing the NuGet package:
[BolRetailerAPI](https://www.nuget.org/packages/BolRetailerAPI/)

## Example usage
```cs
// Preferably inject via DI
var bolApi = new BolRetailerApi(clientId, clientSecret, testMode);

// List all orders
var orders = await bolApi.OrdersService.GetOpenOrdersAsync();

// Get all the info of a specific order
var singleOrder = await bolApi.OrdersService.GetOrderAsync(orders.First().OrderId);

// Set shipment for a complete order
var shippedOrder = await bolApi.OrdersService.ShipOrderAsync(
    order.OrderId,
    new ShipmentData()
    {
        ShipmentReference = "Reference for shipping",
        ShippingLabelCode = "ShipLabelCode",
        Transport = new TransportInstruction()
        {
            TrackAndTrace = "BOL12343955DE",
            TransporterCode = TransporterCode.ParcelNl
        }
    }
);

// Get a list of shipment details
var shippedOrderDetails = await bolApi.ShipmentService.GetShipmentListAsync(order.OrderId);

// Get full detaild of a shipment
var shipmentDetails = await api.ShipmentService.GetShipmentByIdAsync(shipmentId);
```

## Current implementation status
| Service                                       | Implemented |
|-----------------------------------------------|:-----------:|
| Commission                                    |             |
| Inbounds                                      |             |
| Insights                                      |             |
| Inventory                                     |             |
| Invoices                                      |             |
| Offers                                        |             |
| Orders                                        |     1.0     |
| Process Status                                |             |
| Reductions                                    |             |
| Returns                                       |             |
| Shipments                                     |     1.2     |
| Shipping labels                               |             |
| Transports                                    |             |
