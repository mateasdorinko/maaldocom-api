using MaaldoCom.Api.Endpoints.MediaAlbums.Models;

namespace MaaldoCom.Api.Extensions;

public static partial class MapperExtensions
{
    public static PostMediaAlbumResponse ToPostModel(this MediaAlbumDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var model = new PostMediaAlbumResponse().MapToBaseModel(dto);

        model.Name = dto.Name;
        model.UrlFriendlyName = dto.UrlFriendlyName;
        model.Created = dto.Created;

        if (dto.Tags != null)
        {
            model.Tags = dto.Tags.Select(m => m.Name!).ToList();
        }

        return model;
    }
}
