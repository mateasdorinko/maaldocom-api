namespace MaaldoCom.Api.Endpoints.Writings;

public class GetWritingByIdEndpoint(IQueryHandler<GetWritingDetailQuery, WritingDto> handler) : Endpoint<GetWritingByIdRequest, GetWritingDetailResponse>
{
    public override void Configure()
    {
        Get(UrlMaker.GetWritingUrl("{id:guid}"));
        Description(x => x
            .WithName("GetWritingById")
            .WithSummary("Gets a writing by its unique identifier."));
        Description(b => b.Produces(StatusCodes.Status404NotFound));
        AllowAnonymous();
        ResponseCache(1200); // 20 minutes
    }

    public override async Task HandleAsync(GetWritingByIdRequest req, CancellationToken ct)
    {
        var query = new GetWritingDetailQuery(req.Id);
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
