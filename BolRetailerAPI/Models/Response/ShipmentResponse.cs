using System.Collections.Generic;
using BolRetailerAPI.Models.Response.Shipments;

namespace BolRetailerAPI.Models.Response
{
    internal class ShipmentResponse
    {
        public IEnumerable<ReducedShipment> Shipments { get; set; }
    }
}