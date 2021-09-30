using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using SNGBarrett.BookStore.Application.SharedKernel.Exceptions;
using SNGBarrett.BookStore.Application.SharedKernel.Interfaces;

namespace SNGBarrett.BookStore.Application.Commands
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public CancelOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(order => order.Id == request.OrderId, cancellationToken: cancellationToken);

            if (order == null) throw new NotFoundException("Order not found: {OrderId}", request.OrderId);

            order.Cancel();

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}