using MaaldoCom.Services.Api.Endpoints.Tags.Models;
using MaaldoCom.Services.Application.Queries.Tags;

namespace MaaldoCom.Services.Api.Endpoints.Tags;

public class ListTagsEndpoint : EndpointWithoutRequest<IEnumerable<GetTagResponse>>
{
    public override void Configure()
    {
        Get(UrlMaker.TagsRoute);
        Description(x => x
            .WithName("ListTags")
            .WithSummary("Lists all tags."));
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = (await new ListTagsQuery().ExecuteAsync(ct)).Value;
        var response = result.ToGetModels();

        await Send.OkAsync(response, ct);
    }
}
