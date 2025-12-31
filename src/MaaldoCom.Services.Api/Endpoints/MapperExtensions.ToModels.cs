using MaaldoCom.Services.Api.Endpoints.Knowledge;
using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Api.Endpoints.MediaAlbums;
using MaaldoCom.Services.Api.Endpoints.Tags;

namespace MaaldoCom.Services.Api.Endpoints;

public static partial class MapperExtensions
{
    extension(IEnumerable<MediaAlbumDto> dtos)
    {
        public IEnumerable<GetMediaAlbumResponse> ToModels()
        {
            ArgumentNullException.ThrowIfNull(dtos);

            return dtos.Select(dto => dto.ToModel()).ToList();
        }

        public IEnumerable<GetMediaAlbumDetailResponse> ToDetailModels()
        {
            ArgumentNullException.ThrowIfNull(dtos);

            return dtos.Select(dto => dto.ToDetailModel()).ToList();
        }
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