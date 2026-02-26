using MaaldoCom.Api.Endpoints.Tags.Models;
using MaaldoCom.Api.Application.Queries.Tags;

namespace MaaldoCom.Api.Endpoints.Tags;

public class ListTagsEndpoint(IQueryHandler<ListTagsQuery, IEnumerable<TagDto>> handler) : EndpointWithoutRequest<IEnumerable<GetTagResponse>>
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
        var query = new ListTagsQuery();
        var result = await handler.HandleAsync(query, ct);

        await Send.OkAsync(result.Value.ToGetModels(), ct);
    }
}
