using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Api.Endpoints;
using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Api.Endpoints.Tags.Models;

namespace MaaldoCom.Services.Api.Extensions;

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

        return model;
    }

    public static GetTagDetailResponse ToDetailModel(this TagDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new GetTagDetailResponse().MapToBaseModel(dto);

        model.Name = dto.Name;
        model.MediaAlbums = dto.MediaAlbums.Select(ma => new GetMediaAlbumTagResponse
        {
            Name = ma.Name,
            Href = UrlMaker.GetMediaAlbumUrl(ma.Id)
        });
        model.Media = dto.Media.Select(m => new GetMediaTagResponse
        {
            Name = m.FileName,
            MediaAlbumName = m.MediaAlbumName,
            Href = UrlMaker.GetMediaUrl(m.MediaAlbumId, m.Id)
        });

        return model;
    }
}