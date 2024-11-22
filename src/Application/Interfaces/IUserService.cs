using System.Security.Claims;

namespace Etn.MyLittleBoard.Application.Interfaces;

public interface IUserService
{
    public void SetAuthenticatedUser(ClaimsPrincipal claimsPrincipal);

    public Task<string> GetUserPhoto64(CancellationToken cancellationToken);

    public User? AuthenticatedUser { get; }
}

public sealed record User(Guid Id, string Name, bool Administrator);
