using MaaldoCom.Services.Api.Endpoints.Knowledge.Models;
using MaaldoCom.Services.Application.Queries.Knowledge;

namespace MaaldoCom.Services.Api.Endpoints.Knowledge;

public class GetKnowledgeByIdEndpoint : Endpoint<GetKnowledgeByIdRequest, GetKnowledgeResponse>
{
    public override void Configure()
    {
        Get(UrlMaker.GetKnowledgeUrl("{id:guid}"));
        Description(x => x
            .WithName("GetKnowledgeById")
            .WithSummary("Gets a knowledge item by its ID."));
        Description(b => b.Produces(StatusCodes.Status404NotFound));
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(GetKnowledgeByIdRequest req, CancellationToken ct)
    {
        var result = await new GetKnowledgeQuery(req.Id).ExecuteAsync(ct);

        await result.Match(
            onSuccess: _ => Send.OkAsync(result.Value.ToGetModel(), ct),
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}
