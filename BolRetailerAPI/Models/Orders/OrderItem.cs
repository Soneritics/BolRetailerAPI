using System;

namespace BolRetailerAPI.Models.Orders
{
    public class OrderItem
    {
        public string OrderItemId { get; set; }
        public string OfferReference { get; set; }
        public string Ean { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal OfferPrice { get; set; }
        public string OfferId { get; set; }
        public decimal TransactionFee { get; set; }
        public DateTime LatestDeliveryDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string OfferCondition { get; set; }
        public bool CancelRequest { get; set; }
        public string FulfilmentMethod { get; set; }
    }
}