using MaaldoCom.Services.Api.Endpoints.Knowledge.Models;
using MaaldoCom.Services.Application.Queries.Knowledge;

namespace MaaldoCom.Services.Api.Endpoints.Knowledge;

public class ListKnowledgeEndpoint : EndpointWithoutRequest<IEnumerable<GetKnowledgeResponse>>
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
        var result = (await new ListKnowledgeQuery(User).ExecuteAsync(ct)).Value;
        var response = result.ToGetModels();

        await Send.OkAsync(response, ct);
    }
}
