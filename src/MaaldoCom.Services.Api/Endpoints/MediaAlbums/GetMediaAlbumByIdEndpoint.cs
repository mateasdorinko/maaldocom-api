using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumByIdEndpoint : Endpoint<GetMediaAlbumByIdRequest, GetMediaAlbumDetailResponse>
{
    public override void Configure()
    {
        Get(UrlMaker.GetMediaAlbumUrl("{id:guid}"));
        Description(x => x
            .WithName("GetMediaAlbumById")
            .WithSummary("Gets a media album by its unique identifier and associated media items."));
        ResponseCache(60);
        AllowAnonymous();
        Description(b => b.Produces(StatusCodes.Status404NotFound));
    }

    public override async Task HandleAsync(GetMediaAlbumByIdRequest req, CancellationToken ct)
    {
        var result = await new GetMediaAlbumDetailQuery(User, req.Id).ExecuteAsync(ct);

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
