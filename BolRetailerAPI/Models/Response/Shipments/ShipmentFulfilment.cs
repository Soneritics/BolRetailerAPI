using System;
using BolRetailerAPI.Models.Enum;

namespace BolRetailerAPI.Models.Response.Shipments
{
    public class ShipmentFulfilment
    {
        public Method Method { get; set; }
        public DistributionParty DistributionParty { get; set; }
        public DateTime? LatestDeliveryDate { get; set; }
    }
}