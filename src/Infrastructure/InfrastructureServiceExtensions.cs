using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Interfaces;
using Etn.MyLittleBoard.Infrastructure.Data;
using Etn.MyLittleBoard.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Etn.MyLittleBoard.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ILogger logger)
    {
        string? connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddSingleton<ICachedService, CachedService>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserService, UserService>();

#pragma warning disable EXTEXP0018
        services.AddHybridCache(options =>
        {
            // Maximum size of cached items
            options.MaximumPayloadBytes = 1024 * 1024 * 10; // 10MB
            options.MaximumKeyLength = 512;

            // Default timeouts
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                LocalCacheExpiration = TimeSpan.FromMinutes(30),
                Expiration = TimeSpan.FromMinutes(30),
            };
        });
#pragma warning restore EXTEXP0018


        logger.LogInformation("Infrastructure services registered");
        return services;
    }
}
