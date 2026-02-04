using MaaldoCom.Services.Api.Endpoints.Knowledge.Models;
using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Api.Endpoints.Tags.Models;
using MaaldoCom.Services.Application.Dtos;

namespace MaaldoCom.Services.Api.Extensions;

public static partial class MapperExtensions
{
    public static IEnumerable<GetMediaAlbumResponse> ToGetModels(this IEnumerable<MediaAlbumDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToGetModel()).ToList();
    }

    public static IEnumerable<GetMediaResponse> ToGetModels(this IEnumerable<MediaDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToGetModel()).ToList();
    }

    public static IEnumerable<GetTagResponse> ToGetModels(this IEnumerable<TagDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToGetModel()).ToList();
    }

    public static IEnumerable<GetKnowledgeResponse> ToGetModels(this IEnumerable<KnowledgeDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToGetModel()).ToList();
    }
}
