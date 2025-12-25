using System.Security.Claims;
using System.Text.Json.Serialization;
using MaaldoCom.Services.Application.Interfaces;
using Microsoft.Extensions.Caching.Hybrid;

namespace MaaldoCom.Services.Application.Queries;

public abstract class BaseQueryCommand(ClaimsPrincipal user)
{
    [JsonIgnore]
    public ClaimsPrincipal User { get; } = user;
}

public abstract class BaseQueryCommandHandler
{
    protected BaseQueryCommandHandler(IMaaldoComDbContext maaldoComDbContext, HybridCache hybridCache)
    {
        MaaldoComDbContext = maaldoComDbContext;
        HybridCache = hybridCache;

        //MaaldoComDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected IMaaldoComDbContext MaaldoComDbContext { get; }
    protected HybridCache HybridCache { get; }
}