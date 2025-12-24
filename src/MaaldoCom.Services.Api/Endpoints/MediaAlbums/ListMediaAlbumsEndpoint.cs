using MaaldoCom.Services.Api.Extensions;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class ListMediaAlbumsEndpoint : EndpointWithoutRequest<ListMediaAlbumsResponse>
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
        var response = new ListMediaAlbumsResponse { MediaAlbums = models };
        
        await Send.OkAsync(response, ct);
    }
}