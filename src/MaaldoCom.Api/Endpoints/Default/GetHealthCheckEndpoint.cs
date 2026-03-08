using MaaldoCom.Api.Endpoints.Default.Models;

namespace MaaldoCom.Api.Endpoints.Default;

public class GetHealthCheckEndpoint : EndpointWithoutRequest<GetHealthCheckResponse>
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
        var result = new GetHealthCheckResponse { Status = "healthy" };
        await Send.OkAsync(result, ct);
    }
}
