using System;
using System.Collections.Generic;
using BolRetailerAPI.Models.Orders;

namespace BolRetailerAPI.Models.Shipments
{
    public class Shipment
    {
        public int ShipmentId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string ShipmentReference { get; set; }
        public List<ShipmentItem> ShipmentItems { get; set; }
        public Transport Transport { get; set; }
        public CustomerDetails CustomerDetails { get; set; }
        public CustomerDetails BillingDetails { get; set; }
    }
}
