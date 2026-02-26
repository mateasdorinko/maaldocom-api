using MaaldoCom.Api.Endpoints.System.Models;
using MaaldoCom.Api.Domain.Extensions;

namespace MaaldoCom.Api.Endpoints.System;

public class GetRuntimeInfoEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get(UrlMaker.GetRuntimeInfoUrl());
        Description(x => x
            .WithName("GetRuntimeInfo")
            .WithSummary("Gets runtime info"));
        Permissions("read:runtime-info");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = new GetRuntimeInfoResponse
        {
            AspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!,
            ClrVersion = Environment.Version.ToString(),
            Is64BitSystem = Environment.Is64BitOperatingSystem,
            Is64BitProcess = Environment.Is64BitProcess,
            MachineName = Environment.MachineName,
            OsVersion = Environment.OSVersion.ToString(),
            ProcessId = Environment.ProcessId,
            ProcessorCount = Environment.ProcessorCount,
            ProcessPath = Environment.ProcessPath,
            User = User.GetUserId()
        };

        await Send.OkAsync(response, ct);
    }
}
