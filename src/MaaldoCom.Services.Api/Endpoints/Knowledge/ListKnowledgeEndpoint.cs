using MaaldoCom.Services.Application.Queries.Knowledge;

namespace MaaldoCom.Services.Api.Endpoints.Knowledge;

public class ListKnowledgeEndpoint : EndpointWithoutRequest<IEnumerable<GetKnowledgeResponse>>
{
    public override void Configure()
    {
        Get("/knowledge");
        ResponseCache(60);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var dtos = await new ListKnowledgeQuery(User).ExecuteAsync(ct);
        //var response = dtos.ToModels();
        
        await Send.OkAsync(new List<GetKnowledgeResponse>(), ct);
    }
}