using MaaldoCom.Services.Api.Endpoints.Knowledge.Models;
using MaaldoCom.Services.Application.Queries.Knowledge;

namespace MaaldoCom.Services.Api.Endpoints.Knowledge;

public class GetRandomKnowledgeEndpoint : EndpointWithoutRequest<GetKnowledgeResponse>
{
    public override void Configure()
    {
        Get(UrlMaker.GetRandomKnowledgeUrl());
        Description(x => x
            .WithName("GetRandomKnowledge")
            .WithSummary("Gets a random knowledge item."));
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = (await new GetRandomKnowledgeQuery(User).ExecuteAsync(ct));
        var response = result.Value.ToGetModel();
        
        await Send.OkAsync(response, ct);
    }
}