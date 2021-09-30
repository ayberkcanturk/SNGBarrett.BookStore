using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SNGBarrett.BookStore.Domain.Entities;

namespace SNGBarrett.BookStore.Infrastructure.EFCore.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(b => b.SellingPrice)
                .IsRequired();

            builder.Property(b => b.StockQuantity)
                .IsRequired();

            builder.Ignore(b => b.DomainEvents);

            builder
                .HasMany<Author>(b => b.Authors)
                .WithMany(a => a.Books);
        }
    }
}
