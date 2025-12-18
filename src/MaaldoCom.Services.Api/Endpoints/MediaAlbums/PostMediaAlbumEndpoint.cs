namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class PostMediaAlbumEndpoint : Endpoint<PostMediaAlbumRequest, PostMediaAlbumResponse>
{
    public override void Configure()
    {
        Post("/media-albums");
    }

    public override async Task HandleAsync(PostMediaAlbumRequest req, CancellationToken ct)
    {
        await Send.CreatedAtAsync(string.Empty, new PostMediaAlbumResponse(), cancellation: ct);
    }
}