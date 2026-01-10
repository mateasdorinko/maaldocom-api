using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Dtos;

namespace MaaldoCom.Services.Api.Extensions;

public static partial class MapperExtensions
{
    public static IEnumerable<GetMediaAlbumDetailResponse> ToDetailModels(this IEnumerable<MediaAlbumDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToDetailModel()).ToList();
    }
}