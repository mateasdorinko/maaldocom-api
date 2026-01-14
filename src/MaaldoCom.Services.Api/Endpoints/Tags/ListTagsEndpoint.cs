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
            .WithSummary("Lists all tags.")
            .WithTags("Endpoints"));
        ResponseCache(60);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = (await new ListTagsQuery(User).ExecuteAsync(ct)).Value;
        var response = result.ToModels();

        await Send.OkAsync(response, ct);
    }
}