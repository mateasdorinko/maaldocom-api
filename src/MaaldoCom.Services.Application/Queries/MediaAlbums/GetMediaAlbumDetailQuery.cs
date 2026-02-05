namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class GetMediaAlbumDetailQuery : BaseQuery, ICommand<Result<MediaAlbumDto>>
{
    public GetMediaAlbumDetailQuery(ClaimsPrincipal user, string name) : base(user)
    {
        Name = name;
        SearchBy = SearchBy.Name;
        SearchValue = name;
    }

    public GetMediaAlbumDetailQuery(ClaimsPrincipal user, Guid id) : base(user)
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

public class GetMediaAlbumDetailQueryHandler(ICacheManager cacheManager) : BaseQueryHandler(cacheManager), ICommandHandler<GetMediaAlbumDetailQuery, Result<MediaAlbumDto>>
{
    public async Task<Result<MediaAlbumDto>> ExecuteAsync(GetMediaAlbumDetailQuery query, CancellationToken ct)
    {
        MediaAlbumDto? dto;

        switch (query.SearchBy)
        {
            case SearchBy.Id:
                dto = await CacheManager.GetMediaAlbumDetailAsync(query.Id!.Value, ct);

                return dto != null ?
                    Result.Ok(dto)! :
                    Result.Fail<MediaAlbumDto>(new EntityNotFoundError(nameof(MediaAlbum), query.SearchBy, query.SearchValue));
            case SearchBy.Name:
                var cachedMediaAlbumByName = (await CacheManager.ListMediaAlbumsAsync(ct))
                    .FirstOrDefault(x => x.UrlFriendlyName == query.SearchValue.ToString());

                if (cachedMediaAlbumByName == null)
                {
                    return Result.Fail<MediaAlbumDto>(new EntityNotFoundError(nameof(MediaAlbum), query.SearchBy, query.SearchValue));
                }

                dto = await CacheManager.GetMediaAlbumDetailAsync(cachedMediaAlbumByName!.Id, ct);

                return dto != null ?
                    Result.Ok(dto)! :
                    Result.Fail<MediaAlbumDto>(new EntityNotFoundError(nameof(MediaAlbum), query.SearchBy, query.SearchValue));
            case SearchBy.NotSet:
            default:
                 return Result.Fail<MediaAlbumDto>(new EntityNotFoundError(nameof(MediaAlbum), query.SearchBy, query.SearchValue));
        }
    }
}
