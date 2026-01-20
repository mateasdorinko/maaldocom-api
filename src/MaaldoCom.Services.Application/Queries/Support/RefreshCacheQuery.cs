namespace MaaldoCom.Services.Application.Queries.Support;

public class RefreshCacheQuery(ClaimsPrincipal user) : BaseQuery(user), ICommand<Result>;

public class RefreshCacheQueryHandler(ICacheManager cacheManager)
    : BaseQueryHandler(cacheManager), ICommandHandler<RefreshCacheQuery, Result>
{
    public async Task<Result> ExecuteAsync(RefreshCacheQuery query, CancellationToken ct)
    {
        await CacheManager.RefreshCacheAsync(ct);
        return Result.Ok();
    }
}