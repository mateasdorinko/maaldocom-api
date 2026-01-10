using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaByIdEndpoint : Endpoint<GetMediaByIdRequest, GetMediaResponse>
{
    public override void Configure()
    {
        Get($"{UrlMaker.MediaAlbumsRoute}/{{mediaAlbumId:guid}}/media/{{mediaId:guid}}"); //TODO: fix this to use UrlMaker
        ResponseCache(60);
        AllowAnonymous();
        Description(b => b.Produces(StatusCodes.Status404NotFound));
    }

    public override async Task HandleAsync(GetMediaByIdRequest req, CancellationToken ct)
    {
        var contentType = ContentTypeMapper.GetContentType(".png");
        await Send.RedirectAsync("https://maaldo.com/logo.png", allowRemoteRedirects: true);
    }
}