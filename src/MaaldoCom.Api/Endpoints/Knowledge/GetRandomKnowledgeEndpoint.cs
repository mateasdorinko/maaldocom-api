using MaaldoCom.Api.Endpoints.Knowledge.Models;
using MaaldoCom.Api.Application.Queries.Knowledge;

namespace MaaldoCom.Api.Endpoints.Knowledge;

public class GetRandomKnowledgeEndpoint(IQueryHandler<GetRandomKnowledgeQuery, KnowledgeDto> handler) : EndpointWithoutRequest<GetKnowledgeResponse>
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
        var query = new GetRandomKnowledgeQuery();
        var result = await handler.HandleAsync(query, ct);

        await Send.OkAsync(result.Value.ToGetModel(), ct);
    }
}
