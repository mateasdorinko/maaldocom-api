using MaaldoCom.Api.Application.Cache;
using MaaldoCom.Api.Application.Dtos;
using MaaldoCom.Api.Application.Messaging;

namespace MaaldoCom.Api.Application.Queries.Knowledge;

public sealed record GetRandomKnowledgeQuery : IQuery<KnowledgeDto>;

internal sealed class GetRandomKnowledgeQueryHandler(ICacheManager cacheManager)
    : IQueryHandler<GetRandomKnowledgeQuery, KnowledgeDto>
{
    public async Task<Result<KnowledgeDto>> HandleAsync(GetRandomKnowledgeQuery query, CancellationToken ct)
    {
        var cachedKnowledge = (await cacheManager.ListKnowledgeAsync(ct)).ToList();

        var random = new Random();
        var randomKnowledge = cachedKnowledge[random.Next(cachedKnowledge.Count)];

        return Result.Ok(randomKnowledge);
    }
}
