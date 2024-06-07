using BolRetailerApi.Models.Response.Orders;

namespace BolRetailerApi.Models.Response.Shipments;

public class ShipmentItem
{
    public string OrderItemId { get; set; }
    public ShipmentFulfilment Fulfilment { get; set; }
    public OrderOffer Offer { get; set; }
    public OrderProduct Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Commission { get; set; }
}