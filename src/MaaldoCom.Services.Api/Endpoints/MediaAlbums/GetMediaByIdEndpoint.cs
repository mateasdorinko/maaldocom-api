using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaByIdEndpoint : Endpoint<GetMediaByIdRequest, GetMediaResponse>
{
    public override void Configure()
    {
        Get($"/media-albums/{{mediaAlbumId:guid}}/media/{{mediaId:guid}}/{{mediaType:regex(original|viewer|thumb)}}");
        Description(x => x
            .WithName("GetMediaById")
            .WithSummary("Gets a media item stream by its unique identifier within a media album."));
        ResponseCache(60);
        AllowAnonymous();
        Description(b => b.Produces(StatusCodes.Status404NotFound));
    }

    public override async Task HandleAsync(GetMediaByIdRequest req, CancellationToken ct)
    {
        var result = await new GetMediaBlobQuery(User, req.MediaAlbumId, req.MediaId, req.MediaType).ExecuteAsync(ct);

        await result.Match(
            onSuccess: _ => Send.StreamAsync(result.Value.Stream!, result.Value.FileName, result.Value.SizeInBytes, result.Value.ContentType!, cancellation: ct),
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}
