using Etn.MyLittleBoard.Infrastructure.Configurations.Authentication;
using Etn.MyLittleBoard.Server.Components;
using Etn.MyLittleBoard.Server.Configurations.CircuitHandlers;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Identity.Web;

namespace Etn.MyLittleBoard.Server.Configurations;

public static class WebApplicationConfigurations
{
    public static IServiceCollection AddWebApplicationServiceConfigurations(
        this IServiceCollection services,
        ILogger logger,
        WebApplicationBuilder builder)
    {
        services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services
            .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection(AzureEntraOptions.Key));

        builder.Services.AddAuthorization();
        builder.Services.AddMicrosoftIdentityConsentHandler();
        builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<CircuitHandler, UserCircuitHandler>());

        logger.LogInformation("Web application services registered");

        return services;
    }

    public static IApplicationBuilder UseApplicationMiddleware(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        app
            .MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        return app;
    }
}
