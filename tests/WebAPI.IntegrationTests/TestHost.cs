using Microsoft.AspNetCore.Mvc.Testing;

using SNGBarrett.BookStore.WebAPI;

using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using SNGBarrett.BookStore.Infrastructure.EFCore;

namespace WebAPI.IntegrationTests
{
    public class TestHost<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // ReSharper disable once AsyncVoidLambda
            builder.ConfigureServices(async services =>
            {
                var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

                services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("IntegrationTests");
                options.UseInternalServiceProvider(serviceProvider);
            });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<ApplicationDbContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<TestHost<TStartup>>>();

                    await context.Database.EnsureCreatedAsync();

                    try
                    {
                        await ApplicationDbContextSeed.SeedSampleDataAsync(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}", ex.Message);
                    }
                }
            });
        }
    }
}