using System;
using System.Collections.Generic;

namespace BolRetailerAPI.Models.Orders
{
    public class Order
    {
        public string orderId { get; set; }
        public DateTime dateTimeOrderPlaced { get; set; }
        public CustomerOrderDetails customerDetails { get; set; }
        public List<OrderItem> orderItems { get; set; }
    }
}
