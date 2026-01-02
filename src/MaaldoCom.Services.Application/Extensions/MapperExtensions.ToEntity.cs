using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Domain.Entities;

namespace MaaldoCom.Services.Application.Extensions;

public static partial class MapperExtensions
{
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
        entity.UrlFriendlyName = dto.UrlFriendlyName;
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

        return entity;
    }

    public static Tag ToEntity(this TagDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = new Tag().MapToBaseEntity(dto);

        entity.Name = dto.Name;

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