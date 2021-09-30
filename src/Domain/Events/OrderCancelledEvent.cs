using SNGBarrett.BookStore.Domain.Entities;

namespace SNGBarrett.BookStore.Domain.Events
{
    public class OrderCancelledEvent : DomainEvent
    {
        public OrderCancelledEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
