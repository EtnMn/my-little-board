using Etn.MyLittleBoard.Application.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace Etn.MyLittleBoard.Server.Configurations.CircuitHandlers;

// Capture users for custom services. https://learn.microsoft.com/en-us/aspnet/core/blazor/security/additional-scenarios?view=aspnetcore-9.0#circuit-handler-to-capture-users-for-custom-services
internal sealed class UserCircuitHandler(
    AuthenticationStateProvider authenticationStateProvider,
    IUserService userService) :
    CircuitHandler,
    IDisposable
{
    public override Task OnCircuitOpenedAsync(Circuit circuit,
        CancellationToken cancellationToken)
    {
        authenticationStateProvider.AuthenticationStateChanged += this.AuthenticationChanged;

        return base.OnCircuitOpenedAsync(circuit, cancellationToken);
    }

    private void AuthenticationChanged(Task<AuthenticationState> task)
    {
        _ = UpdateAuthentication(task);

        async Task UpdateAuthentication(Task<AuthenticationState> task)
        {
            try
            {
                AuthenticationState state = await task;
                userService.SetAuthenticatedUser(state.User);
            }
            catch
            {
                // Todo: EM: Log.
            }
        }
    }

    public override async Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        AuthenticationState state = await authenticationStateProvider.GetAuthenticationStateAsync();
        userService.SetAuthenticatedUser(state.User);
    }

    public void Dispose()
    {
        authenticationStateProvider.AuthenticationStateChanged -= this.AuthenticationChanged;
    }
}
