using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Commands.MediaAlbums;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class PostMediaAlbumEndpoint(Application.Messaging.ICommandHandler<CreateMediaAlbumCommand, MediaAlbumDto> handler)
    : Endpoint<PostMediaAlbumRequest, PostMediaAlbumResponse>
{
    public override void Configure()
    {
        Post(UrlMaker.MediaAlbumsRoute);
        Description(x => x
            .WithName("PostMediaAlbum")
            .WithSummary("Creates a new media album."));
        Permissions("write:media-albums");
    }

    public override async Task HandleAsync(PostMediaAlbumRequest req, CancellationToken ct)
    {
        var dto = req.ToDto();
        var command = new CreateMediaAlbumCommand(User, dto);
        var result = await handler.HandleAsync(command, ct);

        await result.Match(
            onSuccess: _ => Send.CreatedAtAsync<GetMediaAlbumByIdEndpoint>(
                routeValues: new { result.Value.Id },
                responseBody: result.Value.ToPostModel(),
                cancellation: ct),
            onFailure: errors =>
            {
                foreach (var error in errors) { AddError(error.Message); }
                return Send.ErrorsAsync(cancellation: ct);
            }
        );
    }
}
