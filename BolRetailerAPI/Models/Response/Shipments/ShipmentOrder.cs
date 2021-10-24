using System;

namespace BolRetailerApi.Models.Response.Shipments
{
    public class ShipmentOrder
    {
        public string OrderId { get; set; }
        public DateTime OrderPlacedDateTime { get; set; }
    }
}