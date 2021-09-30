using System;

namespace SNGBarrett.BookStore.Domain.Exceptions
{
    public class OrderCouldNotPlacedException : Exception
    {
        public OrderCouldNotPlacedException(string message)
            : base($"Order could not placed with reason: {message}")
        {
        }
    }
}
