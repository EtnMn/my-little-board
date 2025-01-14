using Etn.MyLittleBoard.Application.Interfaces;

namespace Etn.MyLittleBoard.Application.Configurations;

internal sealed class RequestCachingBehavior<TRequest, TResponse>(ICachedService cachedService) :
    IPipelineBehavior<TRequest, TResponse?> where TRequest : ICachedRequest<TResponse?>
{
    public async Task<TResponse?> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse?> next,
        CancellationToken cancellationToken)
    {
        TResponse? response = await cachedService.GetOrCreate(
            request.Key,
            async x => await next(),
            request.Tags,
            cancellationToken);

        return response;
    }
}
