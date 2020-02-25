using System;
using System.Collections.Generic;

namespace BolRetailerAPI.Models
{
    /// <summary>
    /// Reduced Order model
    /// </summary>
    public class ReducedOrder
    {
        public string orderId { get; set; }
        public DateTime dateTimeOrderPlaced { get; set; }
        public List<ReducedOrderItem> orderItems { get; set; }
    }
}
