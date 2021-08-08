using System;

namespace BolRetailerAPI.Models.Response.Shipments
{
    public class ShipmentOrder
    {
        public string OrderId { get; set; }
        public DateTime OrderPlacedDateTime { get; set; }
    }
}