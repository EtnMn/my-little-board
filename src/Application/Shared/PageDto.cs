namespace Etn.MyLittleBoard.Application.Shared;

public sealed record PageDto<T>(
    T[] Items,
    int Skip,
    int Take,
    int Count)
    where T : class
{
}
