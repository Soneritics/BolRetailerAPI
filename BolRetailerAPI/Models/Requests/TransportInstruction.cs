using BolRetailerAPI.Enum;

namespace BolRetailerAPI.Models.Requests
{
    /// <summary>
    /// TransportInstruction part of the ShipmentData object.
    /// </summary>
    public class TransportInstruction
    {
        public TransporterCode TransporterCode { get; set; }
        public string TrackAndTrace { get; set; }
    }
}