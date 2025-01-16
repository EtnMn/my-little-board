namespace Etn.MyLittleBoard.Application.Interfaces;

public interface ICachedRequest<out TResponse> : IRequest<TResponse>
{
    public string Key { get; }

    public string[]? Tags { get; }
}
