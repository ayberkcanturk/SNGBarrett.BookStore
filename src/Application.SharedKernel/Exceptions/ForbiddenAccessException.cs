using System;

namespace SNGBarrett.BookStore.Application.SharedKernel.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base() { }
    }
}
