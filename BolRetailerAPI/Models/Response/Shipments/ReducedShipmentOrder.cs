using System;

namespace BolRetailerAPI.Models.Response.Shipments
{
    public class ReducedShipmentOrder
    {
        public string OrderId { get; set; }
        public DateTime OrderPlacedDateTime { get; set; }
    }
}