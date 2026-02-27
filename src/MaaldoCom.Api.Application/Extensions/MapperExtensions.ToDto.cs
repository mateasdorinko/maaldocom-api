namespace MaaldoCom.Api.Application.Extensions;

public static partial class MapperExtensions
{
    extension<TDto>(TDto dto) where TDto : BaseDto
    {
        private TDto MapFromBaseEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            dto.Id = entity.Id;

            return dto;
        }

        private TDto MapFromBaseAuditableEntity<TEntity>(TEntity entity) where TEntity : BaseAuditableEntity
        {
            dto.MapFromBaseEntity(entity);

            dto.CreatedBy = entity.CreatedBy;
            dto.Created = entity.Created;
            dto.LastModifiedBy = entity.LastModifiedBy;
            dto.LastModified = entity.LastModified;
            dto.Active = entity.Active;

            return dto;
        }
    }

    public static MediaAlbumDto ToDto(this MediaAlbum entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var dto = new MediaAlbumDto().MapFromBaseAuditableEntity(entity);

        dto.Name = entity.Name;
        dto.UrlFriendlyName = entity.UrlFriendlyName;
        dto.Description = entity.Description;
        dto.Tags = entity.MediaAlbumTags?.Select(t => t.Tag.ToDto()).ToList()!;
        dto.Media = entity.Media?.Select(m => m.ToDto()).ToList()!;
        dto.DefaultMediaId = entity.Media?.FirstOrDefault()?.Id ?? Guid.Empty;

        return dto;
    }

    public static MediaDto ToDto(this Media entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var dto = new MediaDto().MapFromBaseAuditableEntity(entity);

        dto.MediaAlbumId = entity.MediaAlbumId;
        dto.MediaAlbumName = entity.MediaAlbum?.Name;
        dto.FileName = entity.FileName;
        dto.Description = entity.Description;
        dto.SizeInBytes = entity.SizeInBytes;
        dto.FileExtension = entity.FileExtension;
        dto.Tags = entity.MediaTags?.Select(t => t.Tag.ToDto()).ToList()!;

        return dto;
    }

    public static TagDto ToDto(this Tag entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var dto = new TagDto().MapFromBaseEntity(entity);

        dto.Name = entity.Name;
        dto.Count = (entity.MediaAlbumTags?.Count ?? 0) + (entity.MediaTags?.Count ?? 0);
        dto.MediaAlbums = entity.MediaAlbumTags?.Where(mat => mat.MediaAlbum != null).Select(mat => new MediaAlbumDto
        {
            Id = mat.MediaAlbum.Id,
            Name = mat.MediaAlbum.Name,
            UrlFriendlyName = mat.MediaAlbum.UrlFriendlyName
        }).ToList()!;
        dto.Media = entity.MediaTags?.Where(mt => mt.Media != null).Select(mt => new MediaDto
        {
            Id = mt.Media.Id,
            MediaAlbumName = mt.Media.MediaAlbum!.Name,
            FileName = mt.Media.FileName,
            MediaAlbumUrlFriendlyName = mt.Media.MediaAlbum!.UrlFriendlyName,
            MediaAlbumId = mt.Media.MediaAlbumId
        }).ToList()!;

        return dto;
    }

    public static KnowledgeDto ToDto(this Knowledge entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var dto = new KnowledgeDto().MapFromBaseEntity(entity);

        dto.Title = entity.Title;
        dto.Quote = entity.Quote;

        return dto;
    }
}
