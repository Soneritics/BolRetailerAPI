using System;
using System.Collections.Generic;

namespace BolRetailerApi.Models.Response.Shipments
{
    public class ReducedShipment
    {
        public string ShipmentId { get; set; }
        public DateTime? ShipmentDateTime { get; set; }
        public string ShipmentReference { get; set; }
        public ReducedShipmentOrder Order { get; set; }
        public List<ReducedShipmentItem> ShipmentItems { get; set; }
        public ReducedTransport Transport { get; set; }
    }
}
