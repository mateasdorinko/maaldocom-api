namespace MaaldoCom.Api.Application.Extensions;

public static class EntityMapperExtensions
{
    public static IEnumerable<MediaAlbum> ToEntities(this IEnumerable<MediaAlbumDto> dtos, ClaimsPrincipal user)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(e => e.ToEntity()).ToList();
    }

    public static IEnumerable<Media> ToEntities(this IEnumerable<MediaDto> dtos, ClaimsPrincipal user)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(e => e.ToEntity()).ToList();
    }

    public static IEnumerable<Writing> ToEntities(this IEnumerable<WritingDto> dtos, ClaimsPrincipal user)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(e => e.ToEntity()).ToList();
    }

    public static IEnumerable<Tag> ToEntities(this IEnumerable<TagDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(e => e.ToEntity()).ToList();
    }

    public static IEnumerable<Comment> ToEntities(this IEnumerable<CommentDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(e => e.ToEntity()).ToList();
    }

    public static IEnumerable<Knowledge> ToEntities(this IEnumerable<KnowledgeDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);

        return dtos.Select(e => e.ToEntity()).ToList();
    }

    extension<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        private TEntity MapToBaseEntity<TDto>(TDto dto) where TDto : BaseDto
        {
            entity.Id = dto.Id;

            return entity;
        }
    }

    extension<TEntity>(TEntity entity) where TEntity : BaseAuditableEntity
    {
        private TEntity MapToBaseAuditableEntity<TDto>(TDto dto) where TDto : BaseDto
        {
            entity.MapToBaseEntity(dto);

            entity.Created = dto.Created;
            entity.CreatedBy = dto.CreatedBy;
            entity.LastModified = dto.LastModified;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.Active = dto.Active;

            return entity;
        }
    }

    public static MediaAlbum ToEntity(this MediaAlbumDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = new MediaAlbum().MapToBaseAuditableEntity(dto);

        entity.Name = dto.Name;
        entity.Slug = dto.Slug;
        entity.Description = dto.Description;

        if (dto.Tags.Any())
        {
            entity.MediaAlbumTags = new List<MediaAlbumTag>();
            foreach (var tagDto in dto.Tags)
            {
                entity.MediaAlbumTags.Add(new MediaAlbumTag { Tag = tagDto.ToEntity() });
            }
        }

        if (dto.Media.Any())
        {
            entity.Media = new List<Media>();
            foreach (var mediaDto in dto.Media)
            {
                entity.Media.Add(mediaDto.ToEntity());
            }
        }

        if (dto.Comments.Any())
        {
            entity.MediaAlbumComments = new List<MediaAlbumComment>();
            foreach (var commentDto in dto.Comments)            {
                entity.MediaAlbumComments.Add(new MediaAlbumComment { Comment = commentDto.ToEntity() });
            }
        }

        return entity;
    }

    public static Media ToEntity(this MediaDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = new Media().MapToBaseAuditableEntity(dto);

        entity.MediaAlbumId = dto.MediaAlbumId;
        entity.FileName = dto.FileName;
        entity.Description = dto.Description;
        entity.SizeInBytes = dto.SizeInBytes;
        entity.FileExtension = dto.FileExtension;

        if (dto.Tags.Any())
        {
            entity.MediaTags = new List<MediaTag>();
            foreach (var tagDto in dto.Tags)
            {
                entity.MediaTags.Add(new MediaTag { Tag = tagDto.ToEntity() });
            }
        }

        if (dto.Comments.Any())
        {
            entity.MediaComments = new List<MediaComment>();
            foreach (var commentDto in dto.Comments)
            {
                entity.MediaComments.Add(new MediaComment { Comment = commentDto.ToEntity() });
            }
        }

        return entity;
    }

    public static Writing ToEntity(this WritingDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = new Writing().MapToBaseAuditableEntity(dto);

        entity.Title = dto.Title;
        entity.Slug = dto.Slug;
        entity.Blurb = dto.Blurb;
        entity.Body = dto.Body;

        if (dto.Tags.Any())
        {
            entity.WritingTags = new List<WritingTag>();
            foreach (var tagDto in dto.Tags)
            {
                entity.WritingTags.Add(new WritingTag() { Tag = tagDto.ToEntity() });
            }
        }

        if (dto.Comments.Any())
        {
            entity.WritingComments = new List<WritingComment>();
            foreach (var commentDto in dto.Comments)            {
                entity.WritingComments.Add(new WritingComment { Comment = commentDto.ToEntity() });
            }
        }

        return entity;
    }

    public static Tag ToEntity(this TagDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = new Tag().MapToBaseEntity(dto);

        entity.Name = dto.Name;

        return entity;
    }

    public static Comment ToEntity(this CommentDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = new Comment().MapToBaseAuditableEntity(dto);

        entity.Author = dto.Author;
        entity.Body = dto.Body;

        return entity;
    }

    public static Knowledge ToEntity(this KnowledgeDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = new Knowledge().MapToBaseEntity(dto);

        entity.Title = dto.Title;
        entity.Quote = dto.Quote;

        return entity;
    }
}
