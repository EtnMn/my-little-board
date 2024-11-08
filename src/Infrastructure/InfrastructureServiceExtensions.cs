using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Domain.Interfaces;
using Etn.MyLittleBoard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
        services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        logger.LogInformation("Infrastructure services registered");
        return services;
    }
}
