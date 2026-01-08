namespace MaaldoCom.Services.Application.Queries.Tags;

public class ListTagsQuery(ClaimsPrincipal user) : BaseQuery(user), ICommand<Result<IEnumerable<TagDto>>> { }

public class ListTagsQueryHandler(ICacheManager cacheManager)
    : BaseQueryHandler(cacheManager), ICommandHandler<ListTagsQuery, Result<IEnumerable<TagDto>>>
{
    public async Task<Result<IEnumerable<TagDto>>> ExecuteAsync(ListTagsQuery query, CancellationToken cancellationToken)
    {
        var tags = await CacheManager.ListTagsAsync(cancellationToken);

        return Result.Ok(tags);
    }
}