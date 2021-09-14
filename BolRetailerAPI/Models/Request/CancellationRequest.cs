using System.Collections.Generic;

namespace BolRetailerAPI.Models.Request
{
    internal class CancellationRequest
    {
        public IEnumerable<OrderItemCancellation> OrderItems { get; set; }
    }
}