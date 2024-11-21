using Etn.MyLittleBoard.Server.Configurations;
using MudBlazor.Services;

ILogger logger = LoggerFactory
    .Create(logger => logger.AddConsole().AddDebug())
    .CreateLogger<Program>();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptionConfigurations(logger);
builder.Services.AddServiceConfigurations(logger, builder);
builder.Services.AddWebApplicationServiceConfigurations(logger, builder);
builder.Services.AddMudServices();

WebApplication app = builder.Build();

app.UseApplicationMiddleware();
app.UseApplicationEndpoints();

await app.RunAsync();

