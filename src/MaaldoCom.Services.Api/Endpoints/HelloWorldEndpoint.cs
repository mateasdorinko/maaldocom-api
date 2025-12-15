namespace MaaldoCom.Services.Api.Endpoints;

public class HelloWorldEndpoint : EndpointWithoutRequest<GetHelloWorldResponse>
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.OkAsync(new GetHelloWorldResponse(), ct);
    }
}