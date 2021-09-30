using SNGBarrett.BookStore.Application.SharedKernel.Interfaces;

namespace SNGBarrett.BookStore.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        //Default to the first customer as we haven't setup the auth.
        public int UserId { get; } = 1;
    }
}
