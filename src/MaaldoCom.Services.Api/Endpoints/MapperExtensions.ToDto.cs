using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Api.Endpoints.MediaAlbums;
using MaaldoCom.Services.Api.Endpoints.Knowledge;
using MaaldoCom.Services.Api.Endpoints.Tags;

namespace MaaldoCom.Services.Api.Endpoints;

public static partial class MapperExtensions
{
    extension<TDto>(TDto dto) where TDto : BaseDto
    {
        private TDto MapFromBaseModel<TEntity>(TEntity model) where TEntity : BaseModel
        {
            dto.Id = model.Id;
        
            return dto;
        }
    }
    
    public static MediaAlbumDto ToDto(this GetMediaAlbumResponse model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var dto = new MediaAlbumDto().MapFromBaseModel(model);

        dto.Name = model.Name;
        dto.UrlFriendlyName = model.UrlFriendlyName;
        dto.Created = model.Created;
        dto.Tags = model.Tags.Select(t => t.ToDto()).ToList();

        return dto;
    }

    public static MediaAlbumDto ToDto(this GetMediaAlbumDetailResponse model)
    {
        ArgumentNullException.ThrowIfNull(model);

        GetMediaAlbumResponse baseModel = model;
        var dto = new MediaAlbumDto().MapFromBaseModel(baseModel);

        dto.Description = model.Description;
        dto.Active = model.Active;
        dto.Media = model.Media.Select(m => m.ToDto()).ToList();

        return dto;
    }

    public static MediaDto ToDto(this GetMediaResponse model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var dto = new MediaDto().MapFromBaseModel(model);

        dto.FileName = model.FileName;
        dto.Description = model.Description;
        dto.Tags = model.Tags.Select(m => m.ToDto()).ToList();

        return dto;
    }

    public static TagDto ToDto(this GetTagResponse model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var dto = new TagDto().MapFromBaseModel(model);
        
        dto.Name = model.Name;

        return dto;
    }

    public static KnowledgeDto ToDto(this GetKnowledgeResponse model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var dto = new KnowledgeDto().MapFromBaseModel(model);

        dto.Title = model.Title;
        dto.Quote = model.Quote;

        return dto;
    }
}