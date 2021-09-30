using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SNGBarrett.BookStore.Domain.Entities;

namespace SNGBarrett.BookStore.Infrastructure.EFCore
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            Author author1 = null;

            if (!context.Customers.Any())
            {
                await context.Customers.AddAsync(new Customer("ayberk.canturk@hi-mind.co.uk"));
            }

            if (!context.Authors.Any())
            {
                var authorEntry = await context.Authors.AddAsync(new Author("George", "Orwell"));
                author1 = authorEntry.Entity;
            }

            await context.SaveChangesAsync();

            if (!context.Books.Any())
            {
                await context.Books.AddAsync(new Book("1984", "George Orwell''s best seller book", 19.95m, new List<Author>() { author1 }));
            }

            await context.SaveChangesAsync();
        }
    }
}
