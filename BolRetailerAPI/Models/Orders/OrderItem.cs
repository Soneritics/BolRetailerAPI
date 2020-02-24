using System;

namespace BolRetailerAPI.Models.Orders
{
    public class OrderItem
    {
        public string orderItemId { get; set; }
        public string offerReference { get; set; }
        public string ean { get; set; }
        public string title { get; set; }
        public int quantity { get; set; }
        public decimal offerPrice { get; set; }
        public string offerId { get; set; }
        public decimal transactionFee { get; set; }
        public DateTime latestDeliveryDate { get; set; }
        public DateTime expiryDate { get; set; }
        public string offerCondition { get; set; }
        public bool cancelRequest { get; set; }
        public string fulfilmentMethod { get; set; }
    }
}