using System;
using System.Collections.Generic;

namespace BolRetailerAPI.Models.Orders
{
    /// <summary>
    /// Reduced Order model
    /// </summary>
    public class ReducedOrder
    {
        public string OrderId { get; set; }
        public DateTime DateTimeOrderPlaced { get; set; }
        public List<ReducedOrderItem> OrderItems { get; set; }
    }
}
