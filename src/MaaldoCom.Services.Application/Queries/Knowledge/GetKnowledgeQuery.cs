using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Application.Errors;
using MaaldoCom.Services.Application.Interfaces;

namespace MaaldoCom.Services.Application.Queries.Knowledge;

public class GetKnowledgeQuery(ClaimsPrincipal user, Guid id) : BaseQuery(user), ICommand<Result<KnowledgeDto>>
{
    public Guid Id { get; } = id;
}

public class GetKnowledgeQueryHandler(ICacheManager cacheManager)
    : BaseQueryHandler(cacheManager), ICommandHandler<GetKnowledgeQuery, Result<KnowledgeDto>>
{
    public async Task<Result<KnowledgeDto>> ExecuteAsync(GetKnowledgeQuery query, CancellationToken cancellationToken)
    {
        var cachedKnowledge = (await CacheManager.ListKnowledgeAsync(cancellationToken)).ToList();
        var knowledge = cachedKnowledge.FirstOrDefault(k => k.Id == query.Id);

        return knowledge != null ?
            Result.Ok(knowledge)! :
            Result.Fail<KnowledgeDto>(new EntityNotFound(nameof(Knowledge), SearchBy.Id, query.Id));
    }
}