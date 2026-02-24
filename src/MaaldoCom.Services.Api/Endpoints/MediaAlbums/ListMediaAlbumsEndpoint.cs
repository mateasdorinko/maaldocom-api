using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class ListMediaAlbumsEndpoint : EndpointWithoutRequest<IEnumerable<GetMediaAlbumResponse>>
{
    public override void Configure()
    {
        Get(UrlMaker.MediaAlbumsRoute);
        Description(x => x
            .WithName("ListMediaAlbums")
            .WithSummary("Lists all media albums."));
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await new ListMediaAlbumsQuery().ExecuteAsync(ct);
        var response = result.Value
            .Where(ma => ma.Active && ma.UrlFriendlyName != "hotshots")
            .ToGetModels();

        await Send.OkAsync(response, ct);
    }
}
