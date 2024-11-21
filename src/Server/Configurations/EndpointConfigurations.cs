using Etn.MyLittleBoard.Server.Endpoints;

namespace Etn.MyLittleBoard.Server.Configurations;

public static class EndpointConfigurations
{
    public static WebApplication UseApplicationEndpoints(this WebApplication app)
    {
        app.MapLoginAndLogout();

        return app;
    }
}
