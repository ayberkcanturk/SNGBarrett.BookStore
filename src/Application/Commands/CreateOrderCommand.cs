using System.Collections.Generic;

using MediatR;

namespace SNGBarrett.BookStore.Application.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int CustomerId { get; set; }
        public IList<int> BookIds { get; set; }
    }
}
