using System.Collections.Generic;

using SNGBarrett.BookStore.Domain.Events;

namespace SNGBarrett.BookStore.Domain.Entities
{
    public class Author : AuditableEntity<int>, IHasDomainEvent
    {
        protected Author()
        {
        }

        public Author(string name, string surname) : this()
        {
            Name = name;
            Surname = surname;
        }

        public Author(int id) : this()
        {
            Id = id;
        }


        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public virtual IList<Book> Books { get; protected set; } = new List<Book>();

        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}
