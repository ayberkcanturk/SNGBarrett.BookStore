using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SNGBarrett.BookStore.Application.SharedKernel.Interfaces;
using SNGBarrett.BookStore.Domain.Events;

namespace SNGBarrett.BookStore.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken: cancellationToken);

            var books = await _context.Books.Where(x => request.BookIds.Contains(x.Id)).ToListAsync(cancellationToken);

            var order = customer.CreateOrder(books);

            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
