namespace MaaldoCom.Services.Api.Endpoints.Knowledge;

public class GetRandomKnowledgeEndpoint : EndpointWithoutRequest<GetRandomKnowledgeResponse>
{
    public override void Configure()
    {
        Get("/knowledge/random");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.OkAsync(new GetRandomKnowledgeResponse(), ct);
    }
}