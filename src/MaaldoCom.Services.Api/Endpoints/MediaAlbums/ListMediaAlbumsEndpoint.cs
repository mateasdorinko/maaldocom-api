using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class ListMediaAlbumsEndpoint : EndpointWithoutRequest<IEnumerable<GetMediaAlbumResponse>>
{
    public override void Configure()
    {
        Get($"{Constants.MediaAlbumsRoute}");
        ResponseCache(300);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = (await new ListMediaAlbumsQuery(User).ExecuteAsync(ct)).Value;
        var response = result.ToModels();
        
        await Send.OkAsync(response, ct);
    }
}