using MaaldoCom.Api.Endpoints;
using MaaldoCom.Api.Endpoints.Knowledge.Models;
using MaaldoCom.Api.Endpoints.MediaAlbums.Models;
using MaaldoCom.Api.Endpoints.Tags.Models;
using MaaldoCom.Api.Application.Dtos;

namespace MaaldoCom.Api.Extensions;

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
        dto.Tags = model.Tags.Select(t => new TagDto { Name = t}).ToList();

        return dto;
    }

    public static MediaAlbumDto ToDto(this GetMediaAlbumDetailResponse model)
    {
        ArgumentNullException.ThrowIfNull(model);

        GetMediaAlbumResponse baseModel = model;
        var dto = new MediaAlbumDto().MapFromBaseModel(baseModel);

        dto.Name = model.Name;
        dto.UrlFriendlyName = model.UrlFriendlyName;
        dto.Description = model.Description;
        dto.Active = model.Active;
        dto.Media = model.Media.Select(m => m.ToDto()).ToList();
        dto.Tags = model.Tags.Select(t => new TagDto { Name = t }).ToList();
        dto.Created = model.Created;

        return dto;
    }

    public static MediaAlbumDto ToDto(this PostMediaAlbumRequest model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var dto = new MediaAlbumDto
        {
            Name = model.Name,
            UrlFriendlyName = model.UrlFriendlyName,
            Created = model.Created,
            Description = model.Description,
            Tags = model.Tags.Select(t => new TagDto { Name = t }).ToList(),
            Media = model.Media.Select(m => m.ToDto()).ToList()
        };

        return dto;
    }

    public static MediaDto ToDto(this GetMediaResponse model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var dto = new MediaDto().MapFromBaseModel(model);

        dto.FileName = model.FileName;
        dto.Description = model.Description;
        dto.Tags = model.Tags.Select(m => new TagDto { Name = m }).ToList();

        return dto;
    }

    public static MediaDto ToDto(this PostMediaRequest model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var dto = new MediaDto
        {
            FileName = model.FileName,
            Description = model.Description,
            SizeInBytes = model.SizeInBytes,
            FileExtension = model.FileExtension,
            Tags = model.Tags.Select(m => new TagDto { Name = m }).ToList()
        };

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
