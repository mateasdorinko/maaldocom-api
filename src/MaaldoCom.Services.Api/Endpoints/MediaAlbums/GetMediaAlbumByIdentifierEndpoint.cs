using MaaldoCom.Services.Api.Contracts.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumByIdentifierEndpoint : Endpoint<string, GetMediaAlbumByIdentifierResponse>
{
    public override void Configure()
    {
        Get("/media-albums/{identifier}");
        ResponseCache(60);
        AllowAnonymous();
        Description(b => b.Produces(StatusCodes.Status404NotFound));
    }
    
    public override async Task HandleAsync(string req, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(req))
        {
            await Send.NotFoundAsync(ct);
        }
        else
        {
            await Send.OkAsync(new GetMediaAlbumByIdentifierResponse(), ct);
        } 
    }
}