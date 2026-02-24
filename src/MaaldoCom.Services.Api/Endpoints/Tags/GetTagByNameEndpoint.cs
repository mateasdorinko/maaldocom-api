using MaaldoCom.Services.Api.Endpoints.Tags.Models;
using MaaldoCom.Services.Application.Queries.Tags;

namespace MaaldoCom.Services.Api.Endpoints.Tags;

public class GetTagByNameEndpoint : Endpoint<GetTagByNameRequest, GetTagDetailResponse>
{
    public override void Configure()
    {
        Get(UrlMaker.GetTagUrl("{name}"));
        Description(x => x
            .WithName("GetTagByName")
            .WithSummary("Gets a tag by its name and associated tagged entities."));
        Description(b => b.Produces(StatusCodes.Status404NotFound));
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(GetTagByNameRequest req, CancellationToken ct)
    {
        var result = await new GetTagQuery(req.Name).ExecuteAsync(ct);

        await result.Match(
            onSuccess: _ => Send.OkAsync(result.Value.ToDetailModel(), ct),
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}
