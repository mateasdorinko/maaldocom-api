using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumByNameEndpoint : Endpoint<GetMediaAlbumByNameRequest, GetMediaAlbumDetailResponse>
{
    public override void Configure()
    {
        Get($"{UrlMaker.MediaAlbumsRoute}/{{name}}");
        Description(x => x
            .WithName("GetMediaAlbumByName")
            .WithSummary("Gets a media album by its name and associated media items.")
            .WithTags("Endpoints"));
        ResponseCache(60);
        AllowAnonymous();
        Description(b => b.Produces(StatusCodes.Status404NotFound));
    }
    
    public override async Task HandleAsync(GetMediaAlbumByNameRequest req, CancellationToken ct)
    {
        var result = await new GetMediaAlbumDetailQuery(User, req.Name).ExecuteAsync(ct);

        await result.Match(
            onSuccess: _ => Send.OkAsync(result.Value.ToDetailModel(), ct),
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}