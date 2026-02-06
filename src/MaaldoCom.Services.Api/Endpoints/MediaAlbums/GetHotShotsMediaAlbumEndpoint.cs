using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetHotShotsMediaAlbumEndpoint : EndpointWithoutRequest<GetMediaAlbumDetailResponse>
{
    public override void Configure()
    {
        Get(UrlMaker.GetHotShotsMediaAlbumUrl());
        Description(x => x
            .WithName("GetHotshotsMediaAlbum")
            .WithSummary("Gets the Hotshots media album."));
        ResponseCache(60);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await new GetHotshotsMediaAlbumDetailQuery(User).ExecuteAsync(ct);

        result.Value.Media = result.Value.Media.Where(m => m.Active).ToList();
        await Send.OkAsync(result.Value.ToDetailModel(), ct);
    }
}
