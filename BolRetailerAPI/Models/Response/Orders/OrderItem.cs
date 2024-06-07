using System;
using System.Collections.Generic;

namespace BolRetailerApi.Models.Response.Orders;

public class OrderItem
{
    public string OrderItemId { get; set; }
    public bool CancellationRequest { get; set; }
    public OrderFulfilment Fulfilment { get; set; }
    public OrderOffer Offer { get; set; }
    public OrderProduct Product { get; set; }
    public int Quantity { get; set; }
    public int QuantityShipped { get; set; }
    public int QuantityCancelled { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Commission { get; set; }
    public DateTime LatestChangedDateTime { get; set; }
    public IEnumerable<AdditionalService> AdditionalServices { get; set; }
}