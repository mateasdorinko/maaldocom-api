namespace MaaldoCom.Api.Endpoints.Writings;

public class GetWritingBySlugEndpoint(IQueryHandler<GetWritingDetailQuery, WritingDto> handler) : Endpoint<GetWritingBySlugRequest, GetWritingDetailResponse>
{
    public override void Configure()
    {
        Get(UrlMaker.GetWritingUrl("{slug}"));
        Description(x => x
            .WithName("GetWritingBySlug")
            .WithSummary("Gets a writing by its slug."));
        Description(b => b.Produces(StatusCodes.Status404NotFound));
        AllowAnonymous();
        ResponseCache(1200); // 20 minutes
    }

    public override async Task HandleAsync(GetWritingBySlugRequest req, CancellationToken ct)
    {
        var query = new GetWritingDetailQuery(req.Slug);
        var result = await handler.HandleAsync(query, ct);

        await result.Match(
            onSuccess: _ =>
            {
                return Send.OkAsync(result.Value.ToDetailModel(), ct);
            },
            onFailure: _ => Send.NotFoundAsync(ct)
        );
    }
}
