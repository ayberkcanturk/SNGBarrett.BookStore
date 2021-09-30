using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SNGBarrett.BookStore.Domain.Entities;

namespace SNGBarrett.BookStore.Infrastructure.EFCore.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Ignore(c => c.DomainEvents);

            builder
                .HasMany<Order>(c=>c.Orders)
                .WithOne(order => order.Customer)
                .HasForeignKey(order => order.CustomerId);
        }
    }
}