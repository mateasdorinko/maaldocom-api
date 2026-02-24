using MaaldoCom.Services.Api.Endpoints.Tags.Models;
using MaaldoCom.Services.Application.Queries.Tags;

namespace MaaldoCom.Services.Api.Endpoints.Tags;

public class GetTagByIdEndpoint : Endpoint<GetTagByIdRequest, GetTagDetailResponse>
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
        var result = await new GetTagQuery(req.Id).ExecuteAsync(ct);

        await result.Match(
            onSuccess: _ => Send.OkAsync(result.Value.ToDetailModel(), ct),
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}
