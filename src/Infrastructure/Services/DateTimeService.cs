using System;

using SNGBarrett.BookStore.Application.SharedKernel.Interfaces;

namespace SNGBarrett.BookStore.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
