﻿using System;
using System.Collections.Generic;

namespace BolRetailerApi.Models.Response.Orders;

public class ReducedOrder
{
    public string OrderId { get; set; }
    public DateTime OrderPlacedDateTime { get; set; }
    public IEnumerable<ReducedOrderItem> OrderItems { get; set; }
}