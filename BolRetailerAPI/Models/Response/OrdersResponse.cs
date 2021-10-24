using System.Collections.Generic;
using BolRetailerApi.Models.Response.Orders;

namespace BolRetailerApi.Models.Response
{
    internal class OrdersResponse
    {
        public IEnumerable<ReducedOrder> Orders { get; set; }
    }
}