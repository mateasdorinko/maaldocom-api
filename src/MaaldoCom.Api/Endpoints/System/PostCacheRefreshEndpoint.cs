using MaaldoCom.Api.Application.Commands.System;

namespace MaaldoCom.Api.Endpoints.System;

public class PostCacheRefreshEndpoint(Application.Messaging.ICommandHandler<CacheRefreshCommand> handler) : EndpointWithoutRequest
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
        var command = new CacheRefreshCommand();
        var result = await handler.HandleAsync(command, ct);

        await Send.NoContentAsync(ct);
    }
}
