using System;
using System.Collections.Generic;
using BolRetailerAPI.Models.Response.Orders;

namespace BolRetailerAPI.Models.Response.Shipments
{
    public class Shipment
    {
        public long ShipmentId { get; set; }
        public DateTime ShipmentDateTime { get; set; }
        public string ShipmentReference { get; set; }
        public bool PickupPoint { get; set; }
        public ShipmentOrder Order { get; set; }
        public ShipmentDetails ShipmentDetails { get; set; }
        public BillingDetails BillingDetails { get; set; }
        public List<ShipmentItem> ShipmentItems { get; set; }
        public ShipmentTransport Transport { get; set; }
    }
}