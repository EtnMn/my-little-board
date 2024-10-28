using Etn.MyLittleBoard.Server.Components;
using Etn.MyLittleBoard.Server.Configuration.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MudBlazor.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddOptionsWithValidateOnStart<AzureEntraOptions>()
    .BindConfiguration(AzureEntraOptions.Key)
    .ValidateDataAnnotations();

builder.Services
    .AddOptionsWithValidateOnStart<GraphApiOptions>()
    .BindConfiguration(GraphApiOptions.Key)
    .ValidateDataAnnotations();

builder.Services
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection(AzureEntraOptions.Key))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddMicrosoftGraph(builder.Configuration.GetSection(GraphApiOptions.Key))
    .AddInMemoryTokenCaches();

builder.Services.AddAuthorization();

builder.Services.
    AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddMicrosoftIdentityConsentHandler();

builder.Services.AddMudServices();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
