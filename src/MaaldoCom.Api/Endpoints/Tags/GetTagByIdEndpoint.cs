using MaaldoCom.Api.Endpoints.Tags.Models;
using MaaldoCom.Api.Application.Queries.Tags;

namespace MaaldoCom.Api.Endpoints.Tags;

public class GetTagByIdEndpoint(IQueryHandler<GetTagQuery, TagDto> handler) : Endpoint<GetTagByIdRequest, GetTagDetailResponse>
{
    public override void Configure()
    {
        Get(UrlMaker.GetTagUrl("{id:guid}"));
        Description(x => x
            .WithName("GetTagById")
            .WithSummary("Gets a tag by its unique identifier and associated tagged entities."));
        Description(b => b.Produces(StatusCodes.Status404NotFound));
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(GetTagByIdRequest req, CancellationToken ct)
    {
        var query = new GetTagQuery(req.Id);
        var result = await handler.HandleAsync(query, ct);

        await result.Match(
            onSuccess: _ => Send.OkAsync(result.Value.ToDetailModel(), ct),
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}
