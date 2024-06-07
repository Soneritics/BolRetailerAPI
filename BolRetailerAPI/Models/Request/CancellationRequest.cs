using System.Collections.Generic;

namespace BolRetailerApi.Models.Request;

internal class CancellationRequest
{
    public IEnumerable<OrderItemCancellation> OrderItems { get; set; }
}