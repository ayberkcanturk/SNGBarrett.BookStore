using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SNGBarrett.BookStore.Domain.Entities;

namespace SNGBarrett.BookStore.Infrastructure.EFCore.Configurations
{
    public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.Property(o => o.BookId).IsRequired();
            builder.Property(o => o.OrderId).IsRequired();

            builder.Ignore(o => o.DomainEvents);

            builder
                .HasOne<Order>(x=>x.Order)
                .WithMany(o=>o.OrderLines)
                .HasForeignKey(b => b.OrderId);

            builder
                .HasOne<Book>(x => x.Book)
                .WithMany();

        }
    }
}
