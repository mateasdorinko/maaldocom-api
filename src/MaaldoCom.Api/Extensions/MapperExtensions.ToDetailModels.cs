using MaaldoCom.Api.Endpoints.MediaAlbums.Models;

namespace MaaldoCom.Api.Extensions;

public static partial class MapperExtensions
{
    public static IEnumerable<GetMediaAlbumDetailResponse> ToDetailModels(this IEnumerable<MediaAlbumDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToDetailModel()).ToList();
    }
}
