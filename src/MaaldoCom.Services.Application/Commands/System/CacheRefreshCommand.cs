namespace MaaldoCom.Services.Application.Commands.System;

public class CacheRefreshCommand : ICommand<Result>;

public class CacheRefreshCommandHandler(ICacheManager cacheManager) : ICommandHandler<CacheRefreshCommand, Result>
{
    public async Task<Result> ExecuteAsync(CacheRefreshCommand command, CancellationToken ct)
    {
        await cacheManager.RefreshCacheAsync(ct);
        return Result.Ok();
    }
}
