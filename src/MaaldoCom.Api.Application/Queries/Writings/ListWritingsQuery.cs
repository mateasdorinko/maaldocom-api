namespace MaaldoCom.Api.Application.Queries.Writings;

public sealed record ListWritingsQuery : IQuery<IEnumerable<WritingDto>>;

internal sealed class ListWritingsQueryHandler(ICacheManager cacheManager) : IQueryHandler<ListWritingsQuery, IEnumerable<WritingDto>>
{
    public async Task<Result<IEnumerable<WritingDto>>> HandleAsync(ListWritingsQuery query, CancellationToken ct)
    {
        var writings = await cacheManager.ListWritingsAsync(ct);
        var activeWritings = writings.Where(w => w.Active);

        return Result.Ok(activeWritings);
    }
}
