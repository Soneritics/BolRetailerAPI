namespace BolRetailerApi.Models.Request
{
    internal class ItemId
    {
        public string OrderItemId { get; set; }

        public ItemId(string orderItemId)
        {
            OrderItemId = orderItemId;
        }

        public ItemId()
        {
        }
    }
}