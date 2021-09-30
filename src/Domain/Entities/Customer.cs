using System.Collections.Generic;
using System.Linq;

using SNGBarrett.BookStore.Domain.Events;
using SNGBarrett.BookStore.Domain.Exceptions;

namespace SNGBarrett.BookStore.Domain.Entities
{
    //Aggregate Root
    public class Customer : AuditableEntity<int>, IHasDomainEvent
    {
        protected Customer()
        {
        }

        public Customer(string email) : this()
        {
            Email = email;
        }

        public string Email { get; protected set; }
        public virtual IList<Order> Orders { get; protected set; } = new List<Order>();

        public Order CreateOrder(IList<Book> orderedBooks)
        {
            var order = new Order(this);

            foreach (var book in orderedBooks)
            {
                order.AddOrderLine(new OrderLine(book));
            }

            if (!order.OrderLines.Any())
            {
                throw new OrderCouldNotPlacedException("Order should contain at least one order line.");
            }

            if (order.TotalPrice <= 0)
            {
                throw new OrderCouldNotPlacedException("Order Total Price is equal or lower than zero (0)");
            }

            Orders.Add(order);

            this.DomainEvents.Add(new OrderCreatedEvent(order));

            return order;
        }

        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}
