using MaaldoCom.Api.Application.Cache;
using MaaldoCom.Api.Application.Dtos;
using MaaldoCom.Api.Application.Errors;
using MaaldoCom.Api.Application.Messaging;

namespace MaaldoCom.Api.Application.Queries.Tags;

public sealed record GetTagQuery : IQuery<TagDto>
{
    public GetTagQuery(string name)
    {
        Name = name;
        SearchBy = SearchBy.Name;
        SearchValue = name;
    }

    public GetTagQuery(Guid id)
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

internal sealed class GetTagQueryHandler(ICacheManager cacheManager) : IQueryHandler<GetTagQuery, TagDto>
{
    public async Task<Result<TagDto>> HandleAsync(GetTagQuery query, CancellationToken ct)
    {
        TagDto? dto;

        switch (query.SearchBy)
        {
            case SearchBy.Id:
                dto = await cacheManager.GetTagDetailAsync(query.Id!.Value, ct);

                return dto != null ?
                    Result.Ok(dto)! :
                    Result.Fail<TagDto>(new EntityNotFoundError("Tag", query.SearchBy, query.SearchValue));
            case SearchBy.Name:
                var cachedTagByName = (await cacheManager.ListTagsAsync(ct))
                    .FirstOrDefault(x => x.Name == query.SearchValue.ToString());

                if (cachedTagByName == null)
                {
                    return Result.Fail<TagDto>(new EntityNotFoundError("Tag", query.SearchBy, query.SearchValue));
                }

                dto = await cacheManager.GetTagDetailAsync(cachedTagByName!.Id, ct);

                return dto != null ?
                    Result.Ok(dto)! :
                    Result.Fail<TagDto>(new EntityNotFoundError("Tag", query.SearchBy, query.SearchValue));
            case SearchBy.NotSet:
            default:
                return Result.Fail<TagDto>(new EntityNotFoundError("Tag", query.SearchBy, query.SearchValue));
        }
    }
}
