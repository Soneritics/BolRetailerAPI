using System;
using BolRetailerApi.Models.Enum;

namespace BolRetailerApi.Models.Response.Orders;

public class OrderFulfilment
{
    public string Method { get; set; }
    public DistributionParty? DistributionParty { get; set; }
    public DateTime? LatestDeliveryDate { get; set; }
    public DateTime? ExactDeliveryDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public TimeFrameType? TimeFrameType { get; set; }
}