using MediatR;

namespace SNGBarrett.BookStore.Application.Commands
{
    public class CancelOrderCommand : IRequest
    {
        public int OrderId { get; set; }
    }
}