using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumByNameEndpoint(IQueryHandler<GetMediaAlbumDetailQuery, MediaAlbumDto> handler) : Endpoint<GetMediaAlbumByNameRequest, GetMediaAlbumDetailResponse>
{
    public override void Configure()
    {
        Get(UrlMaker.GetMediaAlbumUrl("{name}"));
        Description(x => x
            .WithName("GetMediaAlbumByName")
            .WithSummary("Gets a media album by its name and associated media items."));
        Description(b => b.Produces(StatusCodes.Status404NotFound));
        AllowAnonymous();
        ResponseCache(1200); // 20 minutes
    }

    public override async Task HandleAsync(GetMediaAlbumByNameRequest req, CancellationToken ct)
    {
        var query = new GetMediaAlbumDetailQuery(req.Name);
        var result = await handler.HandleAsync(query, ct);

        await result.Match(
            onSuccess: _ =>
            {
                result.Value.Media = result.Value.Media.Where(m => m.Active).ToList();
                return Send.OkAsync(result.Value.ToDetailModel(), ct);
            },
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}
