namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class GetMediaAlbumDetailQuery : ICommand<Result<MediaAlbumDto>>
{
    public GetMediaAlbumDetailQuery(string name)
    {
        Name = name;
        SearchBy = SearchBy.Name;
        SearchValue = name;
    }

    public GetMediaAlbumDetailQuery(Guid id)
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

public class GetMediaAlbumDetailQueryHandler(ICacheManager cacheManager)
    : ICommandHandler<GetMediaAlbumDetailQuery, Result<MediaAlbumDto>>
{
    public async Task<Result<MediaAlbumDto>> ExecuteAsync(GetMediaAlbumDetailQuery query, CancellationToken ct)
    {
        MediaAlbumDto? dto;

        switch (query.SearchBy)
        {
            case SearchBy.Id:
                dto = await cacheManager.GetMediaAlbumDetailAsync(query.Id!.Value, ct);

                return dto != null ?
                    Result.Ok(dto)! :
                    Result.Fail<MediaAlbumDto>(new EntityNotFoundError(nameof(MediaAlbum), query.SearchBy, query.SearchValue));
            case SearchBy.Name:
                var cachedMediaAlbumByName = (await cacheManager.ListMediaAlbumsAsync(ct))
                    .FirstOrDefault(x => x.UrlFriendlyName == query.SearchValue.ToString());

                if (cachedMediaAlbumByName == null)
                {
                    return Result.Fail<MediaAlbumDto>(new EntityNotFoundError(nameof(MediaAlbum), query.SearchBy, query.SearchValue));
                }

                dto = await cacheManager.GetMediaAlbumDetailAsync(cachedMediaAlbumByName!.Id, ct);

                return dto != null ?
                    Result.Ok(dto)! :
                    Result.Fail<MediaAlbumDto>(new EntityNotFoundError(nameof(MediaAlbum), query.SearchBy, query.SearchValue));
            case SearchBy.NotSet:
            default:
                 return Result.Fail<MediaAlbumDto>(new EntityNotFoundError(nameof(MediaAlbum), query.SearchBy, query.SearchValue));
        }
    }
}
