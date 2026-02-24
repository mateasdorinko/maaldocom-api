namespace MaaldoCom.Services.Application.Queries.Tags;

public class ListTagsQuery : ICommand<Result<IEnumerable<TagDto>>> { }

public class ListTagsQueryHandler(ICacheManager cacheManager) : ICommandHandler<ListTagsQuery, Result<IEnumerable<TagDto>>>
{
    public async Task<Result<IEnumerable<TagDto>>> ExecuteAsync(ListTagsQuery query, CancellationToken ct)
    {
        var tags = await cacheManager.ListTagsAsync(ct);

        return Result.Ok(tags);
    }
}
