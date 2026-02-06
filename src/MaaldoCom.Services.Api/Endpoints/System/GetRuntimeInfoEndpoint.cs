using MaaldoCom.Services.Api.Endpoints.System.Models;

namespace MaaldoCom.Services.Api.Endpoints.System;

public class GetRuntimeInfoEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get(UrlMaker.GetRuntimeInfoUrl());
        Permissions("read:runtime-info");
        Description(x => x
            .WithName("GetRuntimeInfo")
            .WithSummary("Gets runtime info"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = new GetRuntimeInfoResponse { };

        await Send.OkAsync(response, ct);
    }
}
