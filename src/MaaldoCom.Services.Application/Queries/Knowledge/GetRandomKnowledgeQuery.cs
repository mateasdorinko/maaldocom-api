namespace MaaldoCom.Services.Application.Queries.Knowledge;

public class GetRandomKnowledgeQuery : ICommand<Result<KnowledgeDto>> { }

public class GetRandomKnowledgeQueryHandler(ICacheManager cacheManager)
    : ICommandHandler<GetRandomKnowledgeQuery, Result<KnowledgeDto>>
{
    public async Task<Result<KnowledgeDto>> ExecuteAsync(GetRandomKnowledgeQuery query, CancellationToken ct)
    {
        var cachedKnowledge = (await cacheManager.ListKnowledgeAsync(ct)).ToList();

        var random = new Random();
        var randomKnowledge = cachedKnowledge[random.Next(cachedKnowledge.Count)];

        return Result.Ok(randomKnowledge);
    }
}
