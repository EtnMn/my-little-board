using Azure.Identity;
using Etn.MyLittleBoard.Application.Interfaces;
using Etn.MyLittleBoard.Infrastructure.Configurations.Authentication;
using Etn.MyLittleBoard.Infrastructure.Configurations.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System.Security.Claims;

namespace Etn.MyLittleBoard.Infrastructure.Services;

public sealed class UserService : IUserService
{
    private readonly GraphServiceClient graphServiceClient;

    public UserService(IOptions<AzureEntraOptions> azureEntraOptions, IOptions<GraphApiOptions> graphApiOptions)
    {
        ClientSecretCredentialOptions options = new()
        {
            AuthorityHost = new($"https://login.microsoftonline.com/{azureEntraOptions.Value.TenantId}/v2.0"),
        };

        ClientSecretCredential clientSecretCredential = new(
            azureEntraOptions.Value.TenantId,
            azureEntraOptions.Value.ClientId,
            azureEntraOptions.Value.ClientSecret,
            options);

        this.graphServiceClient = new GraphServiceClient(
            clientSecretCredential,
            graphApiOptions.Value.Scopes);
    }

    public void SetAuthenticatedUser(ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal is not null &&
            claimsPrincipal.Identity is not null &&
            claimsPrincipal.Identity.IsAuthenticated)
        {
            _ = Guid.TryParse(claimsPrincipal.FindFirstValue(CustomClaims.ObjectIdentifier), out Guid id);
            this.AuthenticatedUser = new User(
                id,
                claimsPrincipal.Identity.Name ?? string.Empty,
                claimsPrincipal.IsInRole(Role.Administrator));
        }
        else
        {
            this.AuthenticatedUser = default;
        }
    }

    public async Task<string> GetUserPhoto64(CancellationToken cancellationToken)
    {
        if (this.AuthenticatedUser is not null)
        {
            Stream? photoStream = await this.graphServiceClient
                .Users[this.AuthenticatedUser.Id.ToString()]
                .Photo.Content.GetAsync(cancellationToken: cancellationToken);

            if (photoStream is not null)
            {
                MemoryStream photoMemoryStream = new();
                await photoStream!.CopyToAsync(photoMemoryStream, cancellationToken);
                return Convert.ToBase64String(photoMemoryStream.ToArray());
            }
            else
            {
                return string.Empty;
            }
        }
        else
        {
            return string.Empty;
        }
    }


    public User? AuthenticatedUser { get; private set; }
}
