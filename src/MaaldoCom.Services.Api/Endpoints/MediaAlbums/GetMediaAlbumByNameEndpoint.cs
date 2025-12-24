using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Api.Extensions;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumByNameEndpoint : Endpoint<string, GetMediaAlbum>
{
    public override void Configure()
    {
        Get("/media-albums/{name}");
        ResponseCache(60);
        AllowAnonymous();
        Description(b => b.Produces(StatusCodes.Status404NotFound));
    }
    
    public override async Task HandleAsync(string name, CancellationToken ct)
    {
        var dtos = await new ListMediaAlbumsQuery().ExecuteAsync(ct);
        var model = dtos.FirstOrDefault(x => x.UrlFriendlyName == Route<string>("name"))?.ToGetModelDetail();
        
        if (model is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        await Send.OkAsync(model, ct);
    }
}