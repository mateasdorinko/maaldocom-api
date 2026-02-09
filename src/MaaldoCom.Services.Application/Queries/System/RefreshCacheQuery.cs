namespace MaaldoCom.Services.Application.Queries.System;

public class RefreshCacheQuery(ClaimsPrincipal user) : BaseQuery(user), ICommand<Result>;

public class RefreshCacheQueryHandler(ICacheManager cacheManager, ILogger<RefreshCacheQueryHandler> logger)
    : BaseQueryHandler<RefreshCacheQueryHandler>(cacheManager, logger), ICommandHandler<RefreshCacheQuery, Result>
{
    public async Task<Result> ExecuteAsync(RefreshCacheQuery query, CancellationToken ct)
    {
        await CacheManager.RefreshCacheAsync(ct);
        return Result.Ok();
    }
}
