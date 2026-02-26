using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumByIdEndpoint(IQueryHandler<GetMediaAlbumDetailQuery, MediaAlbumDto> handler) : Endpoint<GetMediaAlbumByIdRequest, GetMediaAlbumDetailResponse>
{
    public override void Configure()
    {
        Get(UrlMaker.GetMediaAlbumUrl("{id:guid}"));
        Description(x => x
            .WithName("GetMediaAlbumById")
            .WithSummary("Gets a media album by its unique identifier and associated media items."));
        Description(b => b.Produces(StatusCodes.Status404NotFound));
        AllowAnonymous();
        ResponseCache(1200); // 20 minutes
    }

    public override async Task HandleAsync(GetMediaAlbumByIdRequest req, CancellationToken ct)
    {
        var query = new GetMediaAlbumDetailQuery(req.Id);
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
