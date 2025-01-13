using Etn.MyLittleBoard.Application.Configurations;
using Etn.MyLittleBoard.Domain.Aggregates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Etn.MyLittleBoard.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        ILogger logger)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(DomainEventBase))!);

            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        logger.LogInformation("Application services registered");

        return services;
    }
}
