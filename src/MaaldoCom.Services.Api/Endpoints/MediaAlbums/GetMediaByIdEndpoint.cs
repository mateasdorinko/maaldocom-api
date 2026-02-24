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
        Description(b => b.Produces(StatusCodes.Status404NotFound));
        AllowAnonymous();
        ResponseCache(2600000); // about a month
    }

    public override async Task HandleAsync(GetMediaByIdRequest req, CancellationToken ct)
    {
        var result = await new GetMediaBlobQuery(req.MediaAlbumId, req.MediaId, req.MediaType).ExecuteAsync(ct);

        await result.Match(
            onSuccess: async _ =>
            {
                await using var stream = result.Value.Stream!;
                // pass filename only for download (sets Content-Disposition: attachment)
                // pass null for viewer/thumb to stream inline (no Content-Disposition header)
                var fileName = req.MediaType == "original" ? result.Value.FileName : null;
                await Send.StreamAsync(stream, fileName, result.Value.SizeInBytes, result.Value.ContentType!, cancellation: ct);
            },
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}
