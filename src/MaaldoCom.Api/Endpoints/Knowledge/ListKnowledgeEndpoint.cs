using MaaldoCom.Api.Endpoints.Knowledge.Models;
using MaaldoCom.Api.Application.Queries.Knowledge;

namespace MaaldoCom.Api.Endpoints.Knowledge;

public class ListKnowledgeEndpoint(IQueryHandler<ListKnowledgeQuery, IEnumerable<KnowledgeDto>> handler) : EndpointWithoutRequest<IEnumerable<GetKnowledgeResponse>>
{
    public override void Configure()
    {
        Get($"{UrlMaker.KnowledgeRoute}");
        Description(x => x
            .WithName("ListKnowledge")
            .WithSummary("Lists all knowledge items."));
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = new ListKnowledgeQuery();
        var result = await handler.HandleAsync(query, ct);

        await Send.OkAsync(result.Value.ToGetModels(), ct);
    }
}
