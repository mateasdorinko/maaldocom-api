using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumByNameEndpoint : Endpoint<GetMediaAlbumByNameRequest, GetMediaAlbumDetailResponse>
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
        var dtos = await new ListMediaAlbumsQueryCommand(User).ExecuteAsync(ct);
        var response = dtos.ToDetailModels().FirstOrDefault(x => x.UrlFriendlyName == req.Name);
        
        if (response is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        await Send.OkAsync(response, ct);
    }
}