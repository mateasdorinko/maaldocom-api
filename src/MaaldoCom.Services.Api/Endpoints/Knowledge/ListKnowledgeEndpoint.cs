namespace MaaldoCom.Services.Api.Endpoints.Knowledge;

public class ListKnowledgeEndpoint : EndpointWithoutRequest<ListKnowledgeResponse>
{
    public override void Configure()
    {
        Get("/knowledge");
        ResponseCache(60);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.OkAsync(new ListKnowledgeResponse(), ct);
    }
}