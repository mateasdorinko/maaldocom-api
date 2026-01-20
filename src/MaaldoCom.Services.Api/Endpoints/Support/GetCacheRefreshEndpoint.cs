using MaaldoCom.Services.Application.Queries.Support;

namespace MaaldoCom.Services.Api.Endpoints.Support;

public class GetCacheRefreshEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get(UrlMaker.CacheRefreshRoute);
        Description(x => x
            .WithName("RefreshCache")
            .WithSummary("Refreshes cached data"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await new RefreshCacheQuery(User).ExecuteAsync(ct);
        await Send.NoContentAsync(ct);
    }
}