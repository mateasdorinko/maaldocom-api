using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class ListMediaAlbumsEndpoint : EndpointWithoutRequest<IEnumerable<GetMediaAlbumResponse>>
{
    public override void Configure()
    {
        Get("/media-albums");
        ResponseCache(60);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var dtos = await new ListMediaAlbumsQuery(User).ExecuteAsync(ct);
        var response = dtos.ToModels();
        
        await Send.OkAsync(response, ct);
    }
}