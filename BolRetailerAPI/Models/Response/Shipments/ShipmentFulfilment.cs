using System;
using BolRetailerApi.Models.Enum;

namespace BolRetailerApi.Models.Response.Shipments
{
    public class ShipmentFulfilment
    {
        public Method Method { get; set; }
        public DistributionParty DistributionParty { get; set; }
        public DateTime? LatestDeliveryDate { get; set; }
    }
}