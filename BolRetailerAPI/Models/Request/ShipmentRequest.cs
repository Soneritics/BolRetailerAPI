using System.Collections.Generic;

namespace BolRetailerApi.Models.Request
{
    internal class ShipmentRequest
    {
        public IEnumerable<ItemId> OrderItems { get; set; }
        public string ShipmentReference { get; set; }
        public string ShippingLabelId { get; set; }
        public TransportInstruction Transport { get; set; }
    }
}