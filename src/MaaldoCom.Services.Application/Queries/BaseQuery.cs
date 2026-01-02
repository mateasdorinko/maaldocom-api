using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Application.Interfaces;
using Microsoft.Extensions.Caching.Hybrid;

namespace MaaldoCom.Services.Application.Queries;

public abstract class BaseQuery(ClaimsPrincipal user)
{
    [JsonIgnore]
    public ClaimsPrincipal User { get; } = user;
}

public abstract class BaseQueryHandler
{
    protected BaseQueryHandler(IMaaldoComDbContext maaldoComDbContext, HybridCache hybridCache)
    {
        MaaldoComDbContext = maaldoComDbContext;
        HybridCache = hybridCache;

        MaaldoComDbContext.EnableChangeTracking();
    }

    [JsonIgnore]
    protected IMaaldoComDbContext MaaldoComDbContext { get; }
    
    [JsonIgnore]
    protected HybridCache HybridCache { get; }
    
    protected const string MediaAlbumListCacheKey = "mediaalbums";
    protected const string KnowledgeListCacheKey = "knowledge";
    protected const string TagListCacheKey = "tags";
}