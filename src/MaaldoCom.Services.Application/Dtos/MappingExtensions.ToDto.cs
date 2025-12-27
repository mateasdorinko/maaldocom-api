using MaaldoCom.Services.Domain.Entities;

namespace MaaldoCom.Services.Application.Dtos;

public static partial class MappingExtensions
{
    extension<TDto>(TDto dto) where TDto : BaseDto
    {
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

        private TDto MapFromBaseEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            dto.Id = entity.Id;
            dto.Guid = entity.Uid;
        
            return dto;
        }
    }

    extension(MediaAlbum entity)
    {
        public MediaAlbumDto ToDto()
        {
            var dto = new MediaAlbumDto().MapFromBaseAuditableEntity(entity);

            dto.Name = entity.Name;
            dto.UrlFriendlyName = entity.UrlFriendlyName;
            dto.Description = entity.Description;
            dto.Media = entity.Media.Select(x => x.ToDto()).ToList();
            dto.Tags = entity.Tags.Select(x => x.ToDto()).ToList();

            return dto;
        }
    }

    extension(Media entity)
    {
        public MediaDto ToDto()
        {
            var dto = new MediaDto().MapFromBaseAuditableEntity(entity);

            dto.MediaAlbumId = entity.MediaAlbumId;
            dto.FileName = entity.FileName;
            dto.Description = entity.Description;
            dto.SizeInBytes = entity.SizeInBytes;
            dto.FileExtension = entity.FileExtension;
            dto.Tags = entity.Tags.Select(x => x.ToDto()).ToList();

            return dto;
        }
    }

    extension(Tag entity)
    {
        public TagDto ToDto()
        {
            var dto = new TagDto().MapFromBaseEntity(entity);

            dto.Name = entity.Name;

            return dto;
        }
    }
}