namespace MaaldoCom.Api.Endpoints.Default;

public class GetHealthCheckEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/health");
        Description(x => x.WithName("GetHealthCheck"));
        AllowAnonymous();
        Options(x => x.ExcludeFromDescription());
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.OkAsync(new { status = "healthy" }, ct);
    }
}
