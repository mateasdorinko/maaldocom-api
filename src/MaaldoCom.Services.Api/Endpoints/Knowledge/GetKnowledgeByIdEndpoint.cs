using MaaldoCom.Services.Api.Endpoints.Knowledge.Models;
using MaaldoCom.Services.Application.Queries.Knowledge;

namespace MaaldoCom.Services.Api.Endpoints.Knowledge;

public class GetKnowledgeByIdEndpoint : Endpoint<GetKnowledgeByIdRequest, GetKnowledgeResponse>
{
    public override void Configure()
    {
        Get($"{UrlMaker.KnowledgeRoute}/{{id:guid}}");
        ResponseCache(60);
        AllowAnonymous();
        Description(b => b.Produces(StatusCodes.Status404NotFound));
    }

    public override async Task HandleAsync(GetKnowledgeByIdRequest req, CancellationToken ct)
    {
        var result = await new GetKnowledgeQuery(User, req.Id).ExecuteAsync(ct);

        await result.Match(
            onSuccess: _ => Send.OkAsync(result.Value.ToModel(), ct),
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}