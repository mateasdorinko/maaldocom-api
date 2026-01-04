using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Application.Errors;
using MaaldoCom.Services.Application.Interfaces;

namespace MaaldoCom.Services.Application.Queries.Tags;

public class GetTagQuery : BaseQuery, ICommand<Result<TagDto>>
{
    public GetTagQuery(ClaimsPrincipal user, string name) : base(user)
    {
        Name = name;
        SearchBy = SearchBy.Name;
        SearchValue = name;
    }

    public GetTagQuery(ClaimsPrincipal user, Guid id) : base(user)
    {
        Id = id;
        SearchBy = SearchBy.Id;
        SearchValue = id;
    }

    public Guid? Id { get; }
    public string? Name { get; }

    public readonly SearchBy SearchBy;
    public readonly object SearchValue;
}

public class GetTagQueryHandler(ICacheManager cacheManager)
    : BaseQueryHandler(cacheManager), ICommandHandler<GetTagQuery, Result<TagDto>>
{
    public async Task<Result<TagDto>> ExecuteAsync(GetTagQuery query, CancellationToken cancellationToken)
    {
        TagDto? dto;

        switch (query.SearchBy)
        {
            case SearchBy.Id:
                dto = await CacheManager.GetTagDetailAsync(query.Id!.Value, cancellationToken);

                return dto != null ?
                    Result.Ok(dto)! :
                    Result.Fail<TagDto>(new EntityNotFoundError("Tag", query.SearchBy, query.SearchValue));
            case SearchBy.Name:
                var cachedTagByName = (await CacheManager.ListTagsAsync(cancellationToken))
                    .FirstOrDefault(x => x.Name == query.SearchValue.ToString());

                if (cachedTagByName == null)
                {
                    return Result.Fail<TagDto>(new EntityNotFoundError("Tag", query.SearchBy, query.SearchValue));
                }

                dto = await CacheManager.GetTagDetailAsync(cachedTagByName!.Id, cancellationToken);

                return dto != null ?
                    Result.Ok(dto)! :
                    Result.Fail<TagDto>(new EntityNotFoundError("Tag", query.SearchBy, query.SearchValue));
            case SearchBy.NotSet:
            default:
                return Result.Fail<TagDto>(new EntityNotFoundError("Tag", query.SearchBy, query.SearchValue));
        }
    }
}