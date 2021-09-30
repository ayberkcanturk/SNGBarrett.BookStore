using System;

namespace SNGBarrett.BookStore.Application.SharedKernel.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
