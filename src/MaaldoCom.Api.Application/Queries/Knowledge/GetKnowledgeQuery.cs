using MaaldoCom.Api.Application.Cache;
using MaaldoCom.Api.Application.Dtos;
using MaaldoCom.Api.Application.Errors;
using MaaldoCom.Api.Application.Messaging;

namespace MaaldoCom.Api.Application.Queries.Knowledge;

public sealed record GetKnowledgeQuery(Guid Id) : IQuery<KnowledgeDto>;

internal sealed class GetKnowledgeQueryHandler(ICacheManager cacheManager) : IQueryHandler<GetKnowledgeQuery, KnowledgeDto>
{
    public async Task<Result<KnowledgeDto>> HandleAsync(GetKnowledgeQuery query, CancellationToken ct)
    {
        var cachedKnowledge = (await cacheManager.ListKnowledgeAsync(ct)).ToList();
        var knowledge = cachedKnowledge.FirstOrDefault(k => k.Id == query.Id);

        return knowledge != null ?
            Result.Ok(knowledge)! :
            Result.Fail<KnowledgeDto>(new EntityNotFoundError(nameof(Knowledge), SearchBy.Id, query.Id));
    }
}
