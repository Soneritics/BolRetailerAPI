using System;
using System.Collections.Generic;

namespace BolRetailerAPI.Models.Response.Orders
{
    public class Order
    {
        public string OrderId { get; set; }
        public bool PickupPoint { get; set; }
        public DateTime OrderPlacedDateTime { get; set; }
        public ShipmentDetails ShipmentDetails { get; set; }
        public BillingDetails BillingDetails { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
