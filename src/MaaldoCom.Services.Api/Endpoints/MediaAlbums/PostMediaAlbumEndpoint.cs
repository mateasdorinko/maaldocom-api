using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;

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
        var response = new PostMediaAlbumResponse();
        
        await Send.CreatedAtAsync(string.Empty, response, cancellation: ct);
    }
}