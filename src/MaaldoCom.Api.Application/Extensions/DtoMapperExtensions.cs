namespace MaaldoCom.Api.Application.Extensions;

public static class DtoMapperExtensions
{
    public static IEnumerable<MediaAlbumDto> ToDtos(this IEnumerable<MediaAlbum> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return entities.Select(e => e.ToDto()).ToList();
    }

    public static IEnumerable<MediaDto> ToDtos(this IEnumerable<Media> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return entities.Select(e => e.ToDto()).ToList();
    }

    public static IEnumerable<WritingDto> ToDtos(this IEnumerable<Writing> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return entities.Select(e => e.ToDto()).ToList();
    }

    public static IEnumerable<TagDto> ToDtos(this IEnumerable<Tag> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return entities.Select(e => e.ToDto()).ToList();
    }

    public static IEnumerable<CommentDto> ToDtos(this IEnumerable<Comment> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return entities.Select(e => e.ToDto()).ToList();
    }

    public static IEnumerable<KnowledgeDto> ToDtos(this IEnumerable<Knowledge> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return entities.Select(e => e.ToDto()).ToList();
    }

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
        dto.Slug = entity.Slug;
        dto.Description = entity.Description;
        dto.Tags = entity.MediaAlbumTags?.Select(t => t.Tag.ToDto()).ToList()!;
        dto.Comments = entity.MediaAlbumComments?.Select(t => t.Comment.ToDto()).ToList()!;
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
        dto.Comments = entity.MediaComments?.Select(t => t.Comment.ToDto()).ToList()!;

        return dto;
    }

    public static WritingDto ToDto(this Writing entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var dto = new WritingDto().MapFromBaseAuditableEntity(entity);

        dto.Title = entity.Title;
        dto.Blurb = entity.Blurb;
        dto.Body = entity.Body;
        dto.Slug = entity.Slug;
        dto.Tags = entity.WritingTags?.Select(t => t.Tag.ToDto()).ToList()!;
        dto.Comments = entity.WritingComments?.Select(t => t.Comment.ToDto()).ToList()!;

        return dto;
    }

    public static TagDto ToDto(this Tag entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var dto = new TagDto().MapFromBaseEntity(entity);

        dto.Name = entity.Name;
        dto.Count = (entity.MediaAlbumTags?.Count ?? 0) + (entity.MediaTags?.Count ?? 0) + (entity.WritingTags?.Count ?? 0);
        dto.MediaAlbums = entity.MediaAlbumTags?.Where(mat => mat.MediaAlbum != null).Select(mat => new MediaAlbumDto
        {
            Id = mat.MediaAlbum.Id,
            Name = mat.MediaAlbum.Name,
            Slug = mat.MediaAlbum.Slug
        }).ToList()!;
        dto.Media = entity.MediaTags?.Where(mt => mt.Media != null).Select(mt => new MediaDto
        {
            Id = mt.Media.Id,
            MediaAlbumName = mt.Media.MediaAlbum!.Name,
            FileName = mt.Media.FileName,
            MediaAlbumSlug = mt.Media.MediaAlbum!.Slug,
            MediaAlbumId = mt.Media.MediaAlbumId
        }).ToList()!;
        dto.Writings = entity.WritingTags?.Where(wt => wt.Writing != null).Select(wt => new WritingDto()
        {
            Id = wt.Writing.Id,
            Title = wt.Writing.Title,
            Body = wt.Writing.Body,
            Slug = wt.Writing.Slug
        }).ToList()!;

        return dto;
    }

    public static CommentDto ToDto(this Comment entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var dto = new CommentDto().MapFromBaseAuditableEntity(entity);

        dto.Author = entity.Author;
        dto.Body = entity.Body;
        dto.Created = entity.Created;
        dto.MediaAlbums = entity.MediaAlbumComments?.Where(mat => mat.MediaAlbum != null).Select(mac => new MediaAlbumDto
        {
            Id = mac.MediaAlbum.Id,
            Name = mac.MediaAlbum.Name,
            Slug = mac.MediaAlbum.Slug
        }).ToList()!;
        dto.Media = entity.MediaComments?.Where(mt => mt.Media != null).Select(mc => new MediaDto
        {
            Id = mc.Media.Id,
            MediaAlbumName = mc.Media.MediaAlbum!.Name,
            FileName = mc.Media.FileName,
            MediaAlbumSlug = mc.Media.MediaAlbum!.Slug,
            MediaAlbumId = mc.Media.MediaAlbumId
        }).ToList()!;
        dto.Writings = entity.WritingComments?.Where(wt => wt.Writing != null).Select(wc => new WritingDto()
        {
            Id = wc.Writing.Id,
            Title = wc.Writing.Title,
            Body = wc.Writing.Body,
            Slug = wc.Writing.Slug
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
