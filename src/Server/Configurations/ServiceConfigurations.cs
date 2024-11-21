using Etn.MyLittleBoard.Application;
using Etn.MyLittleBoard.Infrastructure;

namespace Etn.MyLittleBoard.Server.Configurations;

public static class ServiceConfigurations
{
    public static IServiceCollection AddServiceConfigurations(
        this IServiceCollection services,
        ILogger logger,
        WebApplicationBuilder builder)
    {
        services.AddApplicationServices(logger);
        services.AddInfrastructureServices(builder.Configuration, logger);

        return services;
    }
}
