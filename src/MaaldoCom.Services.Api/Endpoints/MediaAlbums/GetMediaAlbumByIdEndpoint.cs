using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumByIdEndpoint : Endpoint<GetMediaAlbumByIdRequest, GetMediaAlbumDetailResponse>
{
    public override void Configure()
    {
        Get($"{Constants.MediaAlbumsRoute}/{{id:guid}}");
        ResponseCache(300);
        AllowAnonymous();
        Description(b => b.Produces(StatusCodes.Status404NotFound));
    }

    public override async Task HandleAsync(GetMediaAlbumByIdRequest req, CancellationToken ct)
    {
        var result = await new GetMediaAlbumDetailQuery(User, req.Id).ExecuteAsync(ct);

        // TODO: implement result match functional thingy
        if (result.IsSuccess) { await Send.OkAsync(result.Value.ToDetailModel(), ct); return; }
        if (result.IsFailed) { await Send.NotFoundAsync(ct); }
    }
}