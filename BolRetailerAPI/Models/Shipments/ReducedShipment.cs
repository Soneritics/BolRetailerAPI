using System;
using System.Collections.Generic;

namespace BolRetailerAPI.Models.Shipments
{
    public class ReducedShipment
    {
        public int ShipmentId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string ShipmentReference { get; set; }
        public List<ReducedShipmentItem> ShipmentItems { get; set; }
        public ReducedTransport Transport { get; set; }
    }
}
