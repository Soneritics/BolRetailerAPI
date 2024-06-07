using System;
using BolRetailerApi.Models.Enum;

namespace BolRetailerApi.Models.Response.Shipments;

public class TransportEvent
{
    public TransporterEventCode EventCode { get; set; }
    public DateTime EventDateTime { get; set; }
}