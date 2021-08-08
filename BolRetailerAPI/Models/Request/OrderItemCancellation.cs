namespace BolRetailerAPI.Models.Request
{
    internal class OrderItemCancellation
    {
        public string OrderItemId { get; set; }
        public string ReasonCode { get; set; }
    }
}