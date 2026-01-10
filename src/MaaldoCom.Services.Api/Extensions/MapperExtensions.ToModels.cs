using MaaldoCom.Services.Api.Endpoints.Knowledge.Models;
using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Api.Endpoints.Tags.Models;
using MaaldoCom.Services.Application.Dtos;

namespace MaaldoCom.Services.Api.Extensions;

public static partial class MapperExtensions
{
    public static IEnumerable<GetMediaAlbumResponse> ToModels(this IEnumerable<MediaAlbumDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToModel()).ToList();
    }

    public static IEnumerable<GetMediaResponse> ToModels(this IEnumerable<MediaDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToModel()).ToList();
    }

    public static IEnumerable<GetTagResponse> ToModels(this IEnumerable<TagDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToModel()).ToList();
    }

    public static IEnumerable<GetKnowledgeResponse> ToModels(this IEnumerable<KnowledgeDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(dto => dto.ToModel()).ToList();
    }
}