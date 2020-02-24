namespace BolRetailerAPI.Models.Orders
{
    public class CustomerOrderDetails
    {
        public CustomerDetails shipmentDetails { get; set; }
        public CustomerDetails billingDetails { get; set; }
    }
}
