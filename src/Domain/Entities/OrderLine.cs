using System.Collections.Generic;

using SNGBarrett.BookStore.Domain.Events;

namespace SNGBarrett.BookStore.Domain.Entities
{
    public class OrderLine : AuditableEntity<int>, IHasDomainEvent
    {
        protected OrderLine() { }

        public OrderLine(Book book) : this()
        {
            Book = book;
            BookId = book.Id;
        }

        public int OrderId { get; protected set; }
        public virtual Order Order { get; protected set; }
        public int BookId { get; protected set; }
        public virtual Book Book { get; protected set; }

        public decimal OrderLinePrice => Book.SellingPrice;

        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}
