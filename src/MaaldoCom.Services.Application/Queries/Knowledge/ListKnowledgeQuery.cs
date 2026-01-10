namespace MaaldoCom.Services.Application.Queries.Knowledge;

public class ListKnowledgeQuery(ClaimsPrincipal user) : BaseQuery(user), ICommand<Result<IEnumerable<KnowledgeDto>>> { }

public class ListKnowledgeQueryHandler(ICacheManager cacheManager)
    : BaseQueryHandler(cacheManager), ICommandHandler<ListKnowledgeQuery, Result<IEnumerable<KnowledgeDto>>>
{
    public async Task<Result<IEnumerable<KnowledgeDto>>> ExecuteAsync(ListKnowledgeQuery query, CancellationToken cancellationToken)
    {
        var knowledge = await CacheManager.ListKnowledgeAsync(cancellationToken);

        return Result.Ok(knowledge);
    }
}