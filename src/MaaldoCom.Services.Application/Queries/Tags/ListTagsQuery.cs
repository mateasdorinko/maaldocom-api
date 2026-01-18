namespace MaaldoCom.Services.Application.Queries.Tags;

public class ListTagsQuery(ClaimsPrincipal user) : BaseQuery(user), ICommand<Result<IEnumerable<TagDto>>> { }

public class ListTagsQueryHandler(ICacheManager cacheManager)
    : BaseQueryHandler(cacheManager), ICommandHandler<ListTagsQuery, Result<IEnumerable<TagDto>>>
{
    public async Task<Result<IEnumerable<TagDto>>> ExecuteAsync(ListTagsQuery query, CancellationToken ct)
    {
        var tags = await CacheManager.ListTagsAsync(ct);

        return Result.Ok(tags);
    }
}
