using System;

namespace BolRetailerApi.Models.Response.Shipments;

public class TransportEvent
{
    public string EventCode { get; set; }
    public DateTime EventDateTime { get; set; }
}