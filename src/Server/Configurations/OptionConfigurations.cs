using Etn.MyLittleBoard.Infrastructure.Configurations.Authentication;

namespace Etn.MyLittleBoard.Server.Configurations;

public static class OptionConfigurations
{
    public static IServiceCollection AddOptionConfigurations(
        this IServiceCollection services,
        ILogger logger)
    {
        services
            .AddOptionsWithValidateOnStart<AzureEntraOptions>()
            .BindConfiguration(AzureEntraOptions.Key)
            .ValidateDataAnnotations();

        services
            .AddOptionsWithValidateOnStart<GraphApiOptions>()
            .BindConfiguration(GraphApiOptions.Key)
            .ValidateDataAnnotations();

        logger.LogInformation("Options configured");

        return services;
    }
}
