namespace BolRetailerApi.Models.Response.Orders
{
    public class ReducedOrderItem
    {
        public string OrderItemId { get; set; }
        public string Ean { get; set; }
        public int Quantity { get; set; }
        public int QuantityShipped { get; set; }
        public int QuantityCancelled { get; set; }
    }
}