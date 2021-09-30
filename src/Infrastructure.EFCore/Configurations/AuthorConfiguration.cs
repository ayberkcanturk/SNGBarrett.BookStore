using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SNGBarrett.BookStore.Domain.Entities;

namespace SNGBarrett.BookStore.Infrastructure.EFCore.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Surname)
                .IsRequired()
                .HasMaxLength(255);

            builder.Ignore(a => a.DomainEvents);

            builder.HasMany<Book>(author => author.Books)
                .WithMany(b => b.Authors);
        }
    }
}
