namespace MaaldoCom.Api.Endpoints.Default;

public class GetDefaultEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/");
        Description(x => x.WithName("GetDefault"));
        AllowAnonymous();
        Options(x => x.ExcludeFromDescription());
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.RedirectAsync(UrlMaker.GetDocsUrl());
    }
}
