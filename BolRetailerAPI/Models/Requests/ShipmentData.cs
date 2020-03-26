namespace BolRetailerAPI.Models.Requests
{
    /// <summary>
    /// Shipment data request for an order (item).
    /// </summary>
    public class ShipmentData
    {
        public string ShipmentReference { get; set; }
        public TransportInstruction Transport { get; set; }
    }
}
