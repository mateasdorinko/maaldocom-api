using MaaldoCom.Services.Api.Endpoints;
using MaaldoCom.Services.Api.Endpoints.MediaAlbums;
using MaaldoCom.Services.Application.Dtos;

namespace MaaldoCom.Services.Api.Extensions;

internal static class MappingExtensions
{
    #region ToDto
    private static TDto MapBaseDto<TDto, TViewModel>(this TDto dto, TViewModel model) 
        where TDto : BaseDto
        where TViewModel : BaseModel
    {
        dto.Guid = model.Guid;
        dto.Created = model.Created;
        dto.Active = model.Active;
        
        return dto;
    }
    #endregion
    
    #region ToModel
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

    extension(MediumDto dto)
    {
        public GetMediumResponse ToGetModel()
        {
            var model = new GetMediumResponse().MapBaseModel(dto);

            model.FileName = dto.FileName;
            model.Description = dto.Description;

            return model;
        }
    }
    #endregion
}