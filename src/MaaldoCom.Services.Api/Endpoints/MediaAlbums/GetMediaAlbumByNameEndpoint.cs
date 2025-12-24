using MaaldoCom.Services.Api.Extensions;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumByNameEndpoint : Endpoint<GetMediaAlbumByNameRequest, GetMediaAlbumResponse>
{
    public override void Configure()
    {
        Get("/media-albums/{name}");
        ResponseCache(60);
        AllowAnonymous();
        Description(b => b.Produces(StatusCodes.Status404NotFound));
    }
    
    public override async Task HandleAsync(GetMediaAlbumByNameRequest req, CancellationToken ct)
    {
        var dtos = await new ListMediaAlbumsQuery().ExecuteAsync(ct);
        var model = dtos.FirstOrDefault(x => x.UrlFriendlyName == req.Name)?.ToGetModel(true);
        
        if (model is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        await Send.OkAsync(model, ct);
    }
}