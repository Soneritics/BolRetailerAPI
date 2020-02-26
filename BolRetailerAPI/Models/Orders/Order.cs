using System;
using System.Collections.Generic;

namespace BolRetailerAPI.Models.Orders
{
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime DateTimeOrderPlaced { get; set; }
        public CustomerOrderDetails CustomerDetails { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
