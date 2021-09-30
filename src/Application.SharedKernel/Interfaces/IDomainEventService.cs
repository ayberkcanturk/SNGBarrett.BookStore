using System.Threading.Tasks;
using SNGBarrett.BookStore.Domain.Events;

namespace SNGBarrett.BookStore.Application.SharedKernel.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
