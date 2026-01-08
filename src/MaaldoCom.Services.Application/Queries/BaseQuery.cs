namespace MaaldoCom.Services.Application.Queries;

public abstract class BaseQuery(ClaimsPrincipal user)
{
    public ClaimsPrincipal User { get; } = user;
}

public abstract class BaseQueryHandler(ICacheManager cacheManager)
{
    protected ICacheManager CacheManager { get; } = cacheManager;
}