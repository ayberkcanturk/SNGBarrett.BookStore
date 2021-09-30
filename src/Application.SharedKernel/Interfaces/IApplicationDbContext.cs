using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SNGBarrett.BookStore.Domain.Entities;

namespace SNGBarrett.BookStore.Application.SharedKernel.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderLine> OrderLines { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
