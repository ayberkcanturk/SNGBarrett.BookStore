using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SNGBarrett.BookStore.Application.SharedKernel.Interfaces;
using SNGBarrett.BookStore.Infrastructure.Services;

namespace SNGBarrett.BookStore.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
