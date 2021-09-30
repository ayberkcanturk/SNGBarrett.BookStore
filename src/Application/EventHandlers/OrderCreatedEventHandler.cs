using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SNGBarrett.BookStore.Application.SharedKernel.Exceptions;
using SNGBarrett.BookStore.Application.SharedKernel.Interfaces;
using SNGBarrett.BookStore.Application.SharedKernel.Models;
using SNGBarrett.BookStore.Domain.Events;

namespace SNGBarrett.BookStore.Application.EventHandlers
{
    public class OrderCreatedEventHandler : INotificationHandler<DomainEventNotification<OrderCreatedEvent>>
    {
        private readonly ILogger<OrderCreatedEvent> _logger;
        private readonly IApplicationDbContext _context;

        public OrderCreatedEventHandler(ILogger<OrderCreatedEvent> logger, IApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Handle(DomainEventNotification<OrderCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event Handled: {DomainEvent}", domainEvent.GetType().Name);

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == domainEvent.Order.Id, 
                cancellationToken: cancellationToken);

            if (order == null) throw new NotFoundException($"Order with id {domainEvent.Order.Id} could not found");

            foreach (var orderLine in order.OrderLines)
            {
                orderLine.Book.DecreaseStock();
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
