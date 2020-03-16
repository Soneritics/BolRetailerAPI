namespace BolRetailerAPI.Models.Shipments
{
    public class Transport
    {
        public int TransportId { get; set; }
        public string TransporterCode { get; set; }
        public string TrackAndTrace { get; set; }
        public int ShippingLabelId { get; set; }
        public string ShippingLabelCode { get; set; }
    }
}