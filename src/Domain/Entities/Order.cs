using System.Collections.Generic;
using System.Linq;

using SNGBarrett.BookStore.Domain.Events;
using SNGBarrett.BookStore.Domain.Exceptions;
using SNGBarrett.BookStore.Domain.ValueObjects;

namespace SNGBarrett.BookStore.Domain.Entities
{
    public class Order : AuditableEntity<int>, IHasDomainEvent
    {
        protected Order()
        {
        }

        public Order(Customer customer) : this()
        {
            Customer = customer;
        }

        public int CustomerId { get; protected set; }
        public virtual Customer Customer { get; protected set; }
        public virtual IList<OrderLine> OrderLines { get; protected set; } = new List<OrderLine>();
        public bool Cancelled { get; protected set; }
        public Status Status { get; protected set; } = Status.OrderPlaced;

        public void Cancel()
        {
            if (Cancelled) throw new OrderCancellationException(Id, "Order is already cancelled.");

            if (Status == Status.Dispatched)
                throw new OrderCancellationException(Id, "Order is already dispatched.");

            Cancelled = true;

            DomainEvents.Add(new OrderCancelledEvent(this));
        }

        public void AddOrderLine(OrderLine orderLine) => OrderLines.Add(orderLine);
        public decimal TotalPrice => OrderLines.Sum(x => x.OrderLinePrice);


        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}
