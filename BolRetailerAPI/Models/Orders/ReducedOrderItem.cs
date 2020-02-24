namespace BolRetailerAPI.Models.Orders
{
    /// <summary>
    /// Reduced Order Item model
    /// </summary>
    public class ReducedOrderItem
    {
        public string orderItemId { get; set; }
        public string ean { get; set; }
        public bool cancelRequest { get; set; }
        public int quantity { get; set; }
    }
}
