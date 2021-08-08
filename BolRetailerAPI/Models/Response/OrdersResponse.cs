using System.Collections.Generic;
using BolRetailerAPI.Models.Response.Orders;

namespace BolRetailerAPI.Models.Response
{
    internal class OrdersResponse
    {
        public IEnumerable<ReducedOrder> Orders { get; set; }
    }
}