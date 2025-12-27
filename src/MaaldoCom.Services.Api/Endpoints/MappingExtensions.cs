using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Dtos;

namespace MaaldoCom.Services.Api.Endpoints;

public static class MappingExtensions
{
    // ToDto
    private static TDto MapBaseDto<TDto, TModel>(this TDto dto, TModel model) 
        where TDto : BaseDto
        where TModel : BaseModel
    {
        dto.Guid = model.Guid;
        dto.Created = model.Created;
        dto.Active = model.Active;
        
        return dto;
    }
    
    // ToModel
    private static TModel MapBaseModel<TModel, TDto>(this TModel model, TDto dto)
        where TModel : BaseModel
        where TDto : BaseDto
    {
        model.Guid = dto.Guid;
        model.Created = dto.Created;
        model.Active = dto.Active;
        
        return model;
    }
    
    extension(IEnumerable<MediaAlbumDto> dtos)
    {
        public IEnumerable<GetMediaAlbumResponse> ToGetModels(bool includeMedia = false)
        {
            return dtos.Select(x => x.ToGetModel(includeMedia)).ToList();
        }

        public ListMediaAlbumsResponse ToListModel()
        {
            return new ListMediaAlbumsResponse
            {
                MediaAlbums = dtos.Select(x => x.ToGetModel()).ToList()
            };
        }
    }
    
    extension(MediaAlbumDto dto)
    {
        public GetMediaAlbumResponse ToGetModel(bool includeMedia = false)
        {
            var model = new GetMediaAlbumResponse().MapBaseModel(dto);
        
            model.Name = dto.Name;
            model.UrlFriendlyName = dto.UrlFriendlyName;
            model.Description = dto.Description;

            if (includeMedia)
            {
                model.Media = dto.Media.Select(x => x.ToGetModel()).ToList();
            }

            return model;
        }
    }

    extension(MediaDto dto)
    {
        public GetMediaResponse ToGetModel()
        {
            var model = new GetMediaResponse().MapBaseModel(dto);

            model.FileName = dto.FileName;
            model.Description = dto.Description;

            return model;
        }
    }
}