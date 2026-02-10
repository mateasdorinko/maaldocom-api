using MaaldoCom.Services.Application.Queries.System;

namespace MaaldoCom.Services.Api.Endpoints.System;

public class GetCacheRefreshEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post(UrlMaker.GetCacheRefreshUrl());
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
