using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using SNGBarrett.BookStore.Domain.Events;

namespace SNGBarrett.BookStore.Domain.Entities
{
    public class Book : AuditableEntity<int>, IHasDomainEvent
    {
        protected Book() { }

        public Book(string title, string description, decimal sellingPrice, IList<Author> authors) : this()
        {
            Title = title;
            Description = description;
            SellingPrice = sellingPrice;
            Authors = authors;
        }


        [MaxLength(150)]
        public string Title { get; protected set; }
        
        [MaxLength(500)]
        public string Description { get; protected set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
        public decimal SellingPrice { get; protected set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
        public int StockQuantity { get; protected set; }

        public virtual IList<Author> Authors { get; protected set; } = new List<Author>();
        public void AddAuthor(Author author) => Authors.Add(author);
        public void IncreaseStock(int quantity = 1) => StockQuantity += quantity;
        public void DecreaseStock(int quantity = 1) => StockQuantity -= quantity;
        
        
        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}
