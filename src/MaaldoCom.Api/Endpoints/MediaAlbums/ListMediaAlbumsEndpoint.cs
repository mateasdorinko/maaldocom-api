using MaaldoCom.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Api.Application.Queries.MediaAlbums;

namespace MaaldoCom.Api.Endpoints.MediaAlbums;

public class ListMediaAlbumsEndpoint(IQueryHandler<ListMediaAlbumsQuery, IEnumerable<MediaAlbumDto>> handler) : EndpointWithoutRequest<IEnumerable<GetMediaAlbumResponse>>
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
        var query = new ListMediaAlbumsQuery();
        var result = await handler.HandleAsync(query, ct);
        var response = result.Value
            .Where(ma => ma.Active && ma.UrlFriendlyName != "hotshots")
            .ToGetModels();

        await Send.OkAsync(response, ct);
    }
}
