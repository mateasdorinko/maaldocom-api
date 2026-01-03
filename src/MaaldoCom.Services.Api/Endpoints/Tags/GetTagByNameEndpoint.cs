using MaaldoCom.Services.Api.Endpoints.Tags.Models;
using MaaldoCom.Services.Application.Queries.Tags;

namespace MaaldoCom.Services.Api.Endpoints.Tags;

public class GetTagByNameEndpoint : Endpoint<GetTagByNameRequest, GetTagResponse>
{
    public override void Configure()
    {
        Get($"{UrlMaker.GetTagsUrl()}/{{name}}");
        ResponseCache(60);
        AllowAnonymous();
        Description(b => b.Produces(StatusCodes.Status404NotFound));
    }

    public override async Task HandleAsync(GetTagByNameRequest req, CancellationToken ct)
    {
        var result = await new GetTagQuery(User, req.Name).ExecuteAsync(ct);

        await result.Match(
            onSuccess: _ => Send.OkAsync(result.Value.ToModel(), ct),
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}