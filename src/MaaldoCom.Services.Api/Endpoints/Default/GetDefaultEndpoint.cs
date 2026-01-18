namespace MaaldoCom.Services.Api.Endpoints.Default;

public class GetDefaultEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
        Description(x => x.WithName("GetDefault"));
        Options(x => x.ExcludeFromDescription());
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.RedirectAsync("/docs");
    }
}
