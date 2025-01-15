
namespace Etn.MyLittleBoard.Application.Interfaces;

public interface ICachedService
{
    ValueTask<TResponse> GetOrCreate<TResponse>(
        string key,
        Func<CancellationToken, ValueTask<TResponse>> factory,
        IEnumerable<string>? tags = default,
        CancellationToken cancellationToken = default);

    ValueTask Remove(string key, CancellationToken cancellationToken = default);

    ValueTask RemoveByTag(string tag, CancellationToken cancellationToken = default);

    ValueTask Set<TValue>(string key, TValue value, IEnumerable<string>? tags = null, CancellationToken cancellationToken = default);
}
