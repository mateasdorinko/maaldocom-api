using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class ListMediaAlbumsEndpoint : EndpointWithoutRequest<IEnumerable<GetMediaAlbumResponse>>
{
    public override void Configure()
    {
        Get(UrlMaker.MediaAlbumsRoute);
        Description(x => x.WithName("ListMediaAlbums").WithSummary("Lists all media albums."));
        ResponseCache(60);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = (await new ListMediaAlbumsQuery(User).ExecuteAsync(ct));
        var response = result.Value.ToModels();
        
        await Send.OkAsync(response, ct);
    }
}