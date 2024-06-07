using System.Collections.Generic;
using BolRetailerApi.Models.Response.Shipments;

namespace BolRetailerApi.Models.Response;

internal class ShipmentResponse
{
    public IEnumerable<ReducedShipment> Shipments { get; set; }
}