using MaaldoCom.Api.Endpoints.Knowledge.Models;
using MaaldoCom.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Api.Endpoints.Tags.Models;

namespace MaaldoCom.Api.Extensions;

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
