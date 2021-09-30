using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SNGBarrett.BookStore.Application.SharedKernel.Interfaces;
using SNGBarrett.BookStore.Application.SharedKernel.Security;

namespace SNGBarrett.BookStore.Application.SharedKernel.Behaviour
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService _currentUserService;

        public AuthorizationBehaviour(
            ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;

        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (_currentUserService.UserId == null)
                {
                    throw new UnauthorizedAccessException();
                }
            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}
