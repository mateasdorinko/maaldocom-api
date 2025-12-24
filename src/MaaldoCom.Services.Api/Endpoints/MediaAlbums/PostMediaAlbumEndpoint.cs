using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class PostMediaAlbumEndpoint : Endpoint<PostMediaAlbum>
{
    public override void Configure()
    {
        Post("/media-albums");
    }

    public override async Task HandleAsync(PostMediaAlbum req, CancellationToken ct)
    {
        await Send.CreatedAtAsync(string.Empty, cancellation: ct);
    }
}