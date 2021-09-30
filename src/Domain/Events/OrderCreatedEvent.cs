using SNGBarrett.BookStore.Domain.Entities;

namespace SNGBarrett.BookStore.Domain.Events
{
    public class OrderCreatedEvent : DomainEvent
    {
        public OrderCreatedEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
