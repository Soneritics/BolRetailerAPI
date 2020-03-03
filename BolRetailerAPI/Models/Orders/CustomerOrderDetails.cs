namespace BolRetailerAPI.Models.Orders
{
    public class CustomerOrderDetails
    {
        public CustomerDetails ShipmentDetails { get; set; }
        public CustomerDetails BillingDetails { get; set; }
    }
}
