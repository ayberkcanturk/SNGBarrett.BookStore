using Microsoft.Extensions.DependencyInjection;
using SNGBarrett.BookStore.Application.SharedKernel.Interfaces;
using SNGBarrett.BookStore.WebAPI.Services;

namespace SNGBarrett.BookStore.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
