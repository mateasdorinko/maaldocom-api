namespace MaaldoCom.Api.Application.Commands.System;

public sealed record CacheRefreshCommand : ICommand;

internal sealed class CacheRefreshCommandHandler(ICacheManager cacheManager) : ICommandHandler<CacheRefreshCommand>
{
    public async Task<Result> HandleAsync(CacheRefreshCommand command, CancellationToken ct)
    {
        await cacheManager.RefreshCacheAsync(ct);
        return Result.Ok();
    }
}
