namespace MaaldoCom.Api.Endpoints.Writings;

public class ListWritingsEndpoint(IQueryHandler<ListWritingsQuery, IEnumerable<WritingDto>> handler) : EndpointWithoutRequest<IEnumerable<GetWritingResponse>>
{
    public override void Configure()
    {
        Get(UrlMaker.WritingsRoute);
        Description(x => x
            .WithName("ListWritings")
            .WithSummary("Lists all writings."));
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = new ListWritingsQuery();
        var result = await handler.HandleAsync(query, ct);
        var response = result.Value
            .Where(w => w.Active)
            .ToGetModels();

        await Send.OkAsync(response, ct);
    }
}
