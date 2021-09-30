using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SNGBarrett.BookStore.Domain.Entities;

namespace SNGBarrett.BookStore.Infrastructure.EFCore.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.CustomerId).IsRequired();
            builder.Property(o => o.OrderLines).IsRequired();
            
            builder.Ignore(o => o.DomainEvents);
            builder.Ignore(o => o.OrderLines);

            builder
                .HasMany<OrderLine>(o=>o.OrderLines)
                .WithOne(b => b.Order)
                .HasForeignKey(b=>b.OrderId);

            builder.HasOne<Customer>(o=>o.Customer)
                .WithMany(b => b.Orders)
                .HasForeignKey(b=>b.CustomerId);
        }
    }
}