using System;

namespace BolRetailerAPI.Models.Shipments
{
    public class ShipmentItem
    {
        public string OrderItemId { get; set; }
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime LatestDeliveryDate { get; set; }
        public string Ean { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal OfferPrice { get; set; }
        public string OfferCondition { get; set; }
        public string OfferReference { get; set; }
        public string FulfilmentMethod { get; set; }
    }
}