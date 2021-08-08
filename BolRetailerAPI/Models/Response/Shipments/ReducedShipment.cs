using System;
using System.Collections.Generic;

namespace BolRetailerAPI.Models.Response.Shipments
{
    public class ReducedShipment
    {
        public long? ShipmentId { get; set; }
        public DateTime? ShipmentDateTime { get; set; }
        public string ShipmentReference { get; set; }
        public ReducedShipmentOrder Order { get; set; }
        public List<ReducedShipmentItem> ShipmentItems { get; set; }
        public ReducedTransport Transport { get; set; }
    }
}
