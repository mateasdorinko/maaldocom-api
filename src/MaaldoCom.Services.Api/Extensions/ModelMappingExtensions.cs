using MaaldoCom.Services.Api.Endpoints;
using MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Services.Application.Dtos;

namespace MaaldoCom.Services.Api.Extensions;

internal static class ModelMappingExtensions
{
    private static TModel MapBase<TModel, TDto>(this TModel model, TDto dto)
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
        public IEnumerable<GetMediaAlbum> ToGetModels()
        {
            return dtos.Select(ToGetModel);
        }
    }

    extension(MediaAlbumDto dto)
    {
        public GetMediaAlbumDetail ToGetModelDetail()
        {
            var model = new GetMediaAlbumDetail().MapBase(dto);
        
            model.Name = dto.Name;
            model.UrlFriendlyName = dto.UrlFriendlyName;
            model.Description = dto.Description;
            model.Media = dto.Media.Select(x => x.ToGetModel()).ToList();

            return model;
        }

        public GetMediaAlbum ToGetModel()
        {
            var model = new GetMediaAlbum().MapBase(dto);
        
            model.Name = dto.Name;
            model.UrlFriendlyName = dto.UrlFriendlyName;
            model.Description = dto.Description;

            return model;
        }
    }

    extension(MediumDto dto)
    {
        public GetMedium ToGetModel()
        {
            var model = new GetMedium().MapBase(dto);

            model.FileName = dto.FileName;
            model.Description = dto.Description;

            return model;
        }
    }
}