using MaaldoCom.Api.Application.Cache;
using MaaldoCom.Api.Application.Dtos;
using MaaldoCom.Api.Application.Messaging;

namespace MaaldoCom.Api.Application.Queries.Knowledge;

public sealed record ListKnowledgeQuery : IQuery<IEnumerable<KnowledgeDto>>;

internal sealed class ListKnowledgeQueryHandler(ICacheManager cacheManager) : IQueryHandler<ListKnowledgeQuery, IEnumerable<KnowledgeDto>>
{
    public async Task<Result<IEnumerable<KnowledgeDto>>> HandleAsync(ListKnowledgeQuery query, CancellationToken ct)
    {
        var knowledge = await cacheManager.ListKnowledgeAsync(ct);

        return Result.Ok(knowledge);
    }
}
