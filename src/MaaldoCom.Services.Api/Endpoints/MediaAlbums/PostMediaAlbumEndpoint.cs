using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Commands.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class PostMediaAlbumEndpoint : Endpoint<PostMediaAlbumRequest, PostMediaAlbumResponse>
{
    public override void Configure()
    {
        Post(UrlMaker.MediaAlbumsRoute);
        Permissions("write:media-albums");
        Description(x => x
            .WithName("PostMediaAlbum")
            .WithSummary("Creates a new media album."));
    }

    public override async Task HandleAsync(PostMediaAlbumRequest req, CancellationToken ct)
    {
        var dto = req.ToDto();
        var result = await new CreateMediaAlbumCommand(User, dto).ExecuteAsync(ct);

        await result.Match(
            onSuccess: _ => Send.CreatedAtAsync<GetMediaAlbumByIdEndpoint>(
                routeValues: new { result.Value.Id },
                responseBody: result.Value.ToPostModel(),
                cancellation: ct),
            onFailure: errors =>
            {
                foreach (var error in errors) { AddError(error.Message); }
                return Send.ErrorsAsync(cancellation: ct);
            }
        );
    }
}
