namespace MaaldoCom.Api.Application.Queries.Writings;

public sealed record GetWritingDetailQuery : IQuery<WritingDto>
{
    public GetWritingDetailQuery(string slug)
    {
        Slug = slug;
        SearchBy = SearchBy.Slug;
        SearchValue = slug;
    }

    public GetWritingDetailQuery(Guid id)
    {
        Id = id;
        SearchBy = SearchBy.Id;
        SearchValue = id;
    }

    public Guid? Id { get; }
    public string? Slug { get; }

    public readonly SearchBy SearchBy;
    public readonly object SearchValue;
}

internal sealed class GetWritingDetailQueryHandler(ICacheManager cacheManager) : IQueryHandler<GetWritingDetailQuery, WritingDto>
{
    public async Task<Result<WritingDto>> HandleAsync(GetWritingDetailQuery query, CancellationToken ct)
    {
        WritingDto? dto;

        switch (query.SearchBy)
        {
            case SearchBy.Id:
                dto = (await cacheManager.ListWritingsAsync(ct)).FirstOrDefault(w => w.Id == query.Id!.Value);

                return dto != null ?
                    Result.Ok(dto)! :
                    Result.Fail<WritingDto>(new EntityNotFoundError(nameof(Writing), query.SearchBy, query.SearchValue));
            case SearchBy.Slug:
                dto = (await cacheManager.ListWritingsAsync(ct)).FirstOrDefault(w => w.Slug == query.Slug);

                return dto != null ?
                    Result.Ok(dto)! :
                    Result.Fail<WritingDto>(new EntityNotFoundError(nameof(Writing), query.SearchBy, query.SearchValue));
            case SearchBy.NotSet:
            default:
                return Result.Fail<WritingDto>(new EntityNotFoundError(nameof(Writing), query.SearchBy, query.SearchValue));
        }
    }
}
