namespace BolRetailerApi.Models.Request;

internal class ItemId
{
    public ItemId(string orderItemId)
    {
        OrderItemId = orderItemId;
    }

    public ItemId()
    {
    }

    public string OrderItemId { get; set; }
}