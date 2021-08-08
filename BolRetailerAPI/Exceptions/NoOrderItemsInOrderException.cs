using System;

namespace BolRetailerAPI.Exceptions
{
    /// <summary>
    /// Exception that indicates that an order has no order items.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class NoOrderItemsInOrderException : Exception
    {
        public NoOrderItemsInOrderException(string? message) : base(message)
        {
        }
    }
}
