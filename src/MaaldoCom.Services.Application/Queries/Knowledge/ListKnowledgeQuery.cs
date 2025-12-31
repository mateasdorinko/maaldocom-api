using System.Security.Claims;
using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace MaaldoCom.Services.Application.Queries.Knowledge;

public class ListKnowledgeQuery(ClaimsPrincipal user) : BaseQuery(user), ICommand<IEnumerable<KnowledgeDto>> { }

public class ListKnowledgeQueryHandler(IMaaldoComDbContext maaldoComDbContext, HybridCache hybridCache)
    : BaseQueryHandler(maaldoComDbContext, hybridCache), ICommandHandler<ListKnowledgeQuery, IEnumerable<KnowledgeDto>>
{
    public async Task<IEnumerable<KnowledgeDto>> ExecuteAsync(ListKnowledgeQuery query, CancellationToken cancellationToken)
    {
        var knowledge = await HybridCache.GetOrCreateAsync<IEnumerable<KnowledgeDto>>(
            "knowledge-list",
            async _ => (await MaaldoComDbContext.Knowledge.ToListAsync(cancellationToken)).ToDtos(), cancellationToken: cancellationToken);

        return knowledge;
    }
}