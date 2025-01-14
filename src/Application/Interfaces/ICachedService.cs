
namespace Etn.MyLittleBoard.Application.Interfaces;

public interface ICachedService
{
    Task<TResponse> GetOrCreate<TResponse>(
        string key,
        Func<CancellationToken, ValueTask<TResponse>> factory,
        IEnumerable<string>? tags = default,
        CancellationToken cancellationToken = default);

    Task Remove(string key, CancellationToken cancellationToken = default);

    Task RemoveByTag(string tag, CancellationToken cancellationToken = default);

    Task Set<TValue>(string key, TValue value, IEnumerable<string>? tags = null, CancellationToken cancellationToken = default);
}
