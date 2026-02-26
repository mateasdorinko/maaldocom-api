namespace MaaldoCom.Services.Application.Queries.Tags;

public sealed record ListTagsQuery : IQuery<IEnumerable<TagDto>>;

internal sealed class ListTagsQueryHandler(ICacheManager cacheManager) : IQueryHandler<ListTagsQuery, IEnumerable<TagDto>>
{
    public async Task<Result<IEnumerable<TagDto>>> HandleAsync(ListTagsQuery query, CancellationToken ct)
    {
        var tags = await cacheManager.ListTagsAsync(ct);

        return Result.Ok(tags);
    }
}
