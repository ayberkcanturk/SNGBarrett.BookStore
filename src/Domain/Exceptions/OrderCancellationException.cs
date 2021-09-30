using System;

namespace SNGBarrett.BookStore.Domain.Exceptions
{
    public class OrderCancellationException : Exception
    {
        public OrderCancellationException(int orderId, string message)
            : base($"Order cancellation failed for order Id: {orderId} with a reason: {message}")
        {
        }
    }
}
