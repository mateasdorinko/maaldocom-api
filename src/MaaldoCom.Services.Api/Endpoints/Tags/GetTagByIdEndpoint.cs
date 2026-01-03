using MaaldoCom.Services.Api.Endpoints.Tags.Models;
using MaaldoCom.Services.Application.Queries.Tags;

namespace MaaldoCom.Services.Api.Endpoints.Tags;

public class GetTagByIdEndpoint : Endpoint<GetTagByIdRequest, GetTagDetailResponse>
{
    public override void Configure()
    {
        Get($"{UrlMaker.GetTagsUrl()}/{{id:guid}}");
        ResponseCache(60);
        AllowAnonymous();
        Description(b => b.Produces(StatusCodes.Status404NotFound));
    }

    public override async Task HandleAsync(GetTagByIdRequest req, CancellationToken ct)
    {
        var result = await new GetTagQuery(User, req.Id).ExecuteAsync(ct);

        await result.Match(
            onSuccess: _ => Send.OkAsync(result.Value.ToDetailModel(), ct),
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}