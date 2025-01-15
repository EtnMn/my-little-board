using Etn.MyLittleBoard.Application.Interfaces;
using Microsoft.Extensions.Caching.Hybrid;

namespace Etn.MyLittleBoard.Infrastructure.Services;

internal sealed class CachedService(HybridCache cache) : ICachedService
{
    public async ValueTask<TResponse> GetOrCreate<TResponse>(
        string key,
        Func<CancellationToken, ValueTask<TResponse>> factory,
        IEnumerable<string>? tags = default,
        CancellationToken cancellationToken = default)
    {
        TResponse result = await cache.GetOrCreateAsync(
            key,
            factory,
            tags: tags,
            cancellationToken: cancellationToken);

        return result;
    }

    public ValueTask Set<TValue>(
        string key,
        TValue value,
        IEnumerable<string>? tags = null,
        CancellationToken cancellationToken = default)
    {
        return cache.SetAsync(key, value, tags: tags, cancellationToken: cancellationToken);
    }

    public ValueTask Remove(string key, CancellationToken cancellationToken = default)
    {
        return cache.RemoveAsync(key, cancellationToken);
    }

    public ValueTask RemoveByTag(string tag, CancellationToken cancellationToken = default)
    {
        return cache.RemoveByTagAsync(tag, cancellationToken);
    }
}
