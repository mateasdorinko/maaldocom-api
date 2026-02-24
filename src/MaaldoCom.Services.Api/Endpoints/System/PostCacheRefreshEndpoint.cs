using MaaldoCom.Services.Application.Commands.System;

namespace MaaldoCom.Services.Api.Endpoints.System;

public class PostCacheRefreshEndpoint : EndpointWithoutRequest
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
        await new CacheRefreshCommand().ExecuteAsync(ct);
        await Send.NoContentAsync(ct);
    }
}
