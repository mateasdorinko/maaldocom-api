namespace MaaldoCom.Services.Application.Queries.Knowledge;

public class ListKnowledgeQuery : ICommand<Result<IEnumerable<KnowledgeDto>>> { }

public class ListKnowledgeQueryHandler(ICacheManager cacheManager)
    : ICommandHandler<ListKnowledgeQuery, Result<IEnumerable<KnowledgeDto>>>
{
    public async Task<Result<IEnumerable<KnowledgeDto>>> ExecuteAsync(ListKnowledgeQuery query, CancellationToken ct)
    {
        var knowledge = await cacheManager.ListKnowledgeAsync(ct);

        return Result.Ok(knowledge);
    }
}
