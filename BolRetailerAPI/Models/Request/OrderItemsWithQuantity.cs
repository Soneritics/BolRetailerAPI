namespace BolRetailerApi.Models.Request;

public class OrderItemsWithQuantity
{
    public OrderItemsWithQuantity(string orderItemId, int quantity)
    {
        OrderItemId = orderItemId;
    }

    public OrderItemsWithQuantity()
    {
    }

    public string OrderItemId { get; set; }
    
    public int Quantity { get; set; } = 1;
}