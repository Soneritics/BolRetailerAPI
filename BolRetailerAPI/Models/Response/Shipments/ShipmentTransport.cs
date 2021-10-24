using System.Collections.Generic;

namespace BolRetailerApi.Models.Response.Shipments
{
    public class ShipmentTransport
    {
        public string TransportId { get; set; }
        public string TransporterCode { get; set; }
        public string TrackAndTrace { get; set; }
        public string ShippingLabelId { get; set; }
        public List<TransportEvent> TransportEvents { get; set; }
    }
}