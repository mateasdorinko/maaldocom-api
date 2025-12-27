using MaaldoCom.Services.Application.Dtos;

namespace MaaldoCom.Services.Api.Endpoints;

public static partial class MappingExtensions
{
    extension<TDto>(TDto dto) where TDto : BaseDto
    {
        private TDto MapBaseDto<TModel>(TModel model) where TModel : BaseModel
        {
            dto.Guid = model.Guid;
            dto.Created = model.Created;
            dto.Active = model.Active;
        
            return dto;
        }
    }
}