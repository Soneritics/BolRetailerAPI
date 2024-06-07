using System;

namespace BolRetailerApi.Exceptions;

/// <summary>
///     Exception that indicates that an order has no order items.
/// </summary>
/// <seealso cref="System.Exception" />
public class NoOrderItemsInOrderException(string message) : Exception(message);