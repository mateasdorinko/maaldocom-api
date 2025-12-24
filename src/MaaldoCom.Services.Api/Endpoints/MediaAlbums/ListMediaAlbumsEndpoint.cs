using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Api.Extensions;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class ListMediaAlbumsEndpoint : EndpointWithoutRequest<IEnumerable<GetMediaAlbum>>
{
    public override void Configure()
    {
        Get("/media-albums");
        ResponseCache(60);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var dtos = await new ListMediaAlbumsQuery().ExecuteAsync(ct);
        var models = dtos.ToGetModels();
        
        await Send.OkAsync(models, ct);
    }
}