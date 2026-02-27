using MaaldoCom.Api.Endpoints;
using MaaldoCom.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Api.Endpoints.Tags.Models;

namespace MaaldoCom.Api.Extensions;

public static partial class MapperExtensions
{
    public static GetMediaAlbumDetailResponse ToDetailModel(this MediaAlbumDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetMediaAlbumDetailResponse().MapToBaseModel(dto);

        model.Name = dto.Name;
        model.UrlFriendlyName = dto.UrlFriendlyName;
        model.Created = dto.Created;
        model.Description = dto.Description;
        model.Active = dto.Active;
        model.Media = dto.Media.Select(m => m.ToGetModel()).ToList();
        model.Tags = dto.Tags.Select(m => m.Name!).ToList();
        model.DefaultMediaId = dto.DefaultMediaId;

        return model;
    }

    public static GetTagDetailResponse ToDetailModel(this TagDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetTagDetailResponse().MapToBaseModel(dto);

        model.Name = dto.Name;
        model.Count = dto.Count;
        model.MediaAlbums = dto.MediaAlbums.Select(ma => new GetMediaAlbumTagResponse
        {
            Name = ma.Name,
            MediaAlbumId = ma.Id,
            UrlFriendlyName = ma.UrlFriendlyName,
            Href = UrlMaker.GetMediaAlbumUrl(ma.Id)
        });
        model.Media = dto.Media.Select(m => new GetMediaTagResponse
        {
            Name = m.FileName,
            MediaId = m.Id,
            MediaAlbumId = m.MediaAlbumId,
            MediaAlbumName = m.MediaAlbumName,
            MediaAlbumUrlFriendlyName = m.MediaAlbumUrlFriendlyName,
            Href = UrlMaker.GetMediaUrl(m.MediaAlbumId, m.Id)
        });

        return model;
    }
}
