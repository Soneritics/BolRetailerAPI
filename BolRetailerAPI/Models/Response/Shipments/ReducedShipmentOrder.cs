using System;

namespace BolRetailerApi.Models.Response.Shipments;

public class ReducedShipmentOrder
{
    public string OrderId { get; set; }
    public DateTime OrderPlacedDateTime { get; set; }
}