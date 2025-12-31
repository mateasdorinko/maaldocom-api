using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Api.Endpoints.MediaAlbums;

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
    
    
}