using MaaldoCom.Services.Api.Endpoints.Tags.Models;

namespace MaaldoCom.Services.Api.Endpoints.Tags;

public class PostTagEndpoint : Endpoint<PostTagRequest, PostTagResponse>
{
    public override void Configure()
    {
        Post(UrlMaker.TagsRoute);
        Description(x => x
            .WithName("PostTag")
            .WithSummary("Creates a new tag.")
            .WithTags("Endpoints"));
    }

    public override async Task HandleAsync(PostTagRequest req, CancellationToken ct)
    {
        var response = new PostTagResponse();

        await Send.CreatedAtAsync(string.Empty, response, cancellation: ct);
    }
}