namespace BolRetailerAPI.Models.Response.Shipments
{
    public class ShipmentTransport
    {
        public long? TransportId { get; set; }
        public string TransporterCode { get; set; }
        public string TrackAndTrace { get; set; }
        public string ShippingLabelId { get; set; }
    }
}