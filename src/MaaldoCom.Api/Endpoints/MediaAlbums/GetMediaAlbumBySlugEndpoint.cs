namespace MaaldoCom.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumBySlugEndpoint(IQueryHandler<GetMediaAlbumDetailQuery, MediaAlbumDto> handler) : Endpoint<GetMediaAlbumBySlugRequest, GetMediaAlbumDetailResponse>
{
    public override void Configure()
    {
        Get(UrlMaker.GetMediaAlbumUrl("{slug}"));
        Description(x => x
            .WithName("GetMediaAlbumBySlug")
            .WithSummary("Gets a media album by its slug and associated media items."));
        Description(b => b.Produces(StatusCodes.Status404NotFound));
        AllowAnonymous();
        ResponseCache(1200); // 20 minutes
    }

    public override async Task HandleAsync(GetMediaAlbumBySlugRequest req, CancellationToken ct)
    {
        var query = new GetMediaAlbumDetailQuery(req.Slug);
        var result = await handler.HandleAsync(query, ct);

        await result.Match(
            onSuccess: _ =>
            {
                result.Value.Media = result.Value.Media.Where(m => m.Active).ToList();
                return Send.OkAsync(result.Value.ToDetailModel(), ct);
            },
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}
