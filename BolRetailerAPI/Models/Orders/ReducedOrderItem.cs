namespace BolRetailerAPI.Models.Orders
{
    /// <summary>
    /// Reduced Order Item model
    /// </summary>
    public class ReducedOrderItem
    {
        public string OrderItemId { get; set; }
        public string Ean { get; set; }
        public bool CancelRequest { get; set; }
        public int Quantity { get; set; }
    }
}
