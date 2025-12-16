using MaaldoCom.Services.Api.Contracts.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class ListMediaAlbumsEndpoint : EndpointWithoutRequest<ListMediaAlbumsResponse>
{
    public override void Configure()
    {
        Get("/media-albums");
        ResponseCache(60);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.OkAsync(new ListMediaAlbumsResponse(), ct);
    }
}