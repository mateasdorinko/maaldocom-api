using MaaldoCom.Services.Domain.Entities;

namespace MaaldoCom.Services.Application.Dtos;

public static partial class MappingExtensions
{
    extension<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        private TEntity MapBaseEntity<TDto>(TDto dto) where TDto : BaseDto
        {
            entity.Id = dto.Id;
            entity.Uid = dto.Guid;
        
            return entity;
        }
    }
    
    extension<TEntity>(TEntity entity) where TEntity : BaseAuditableEntity
    {
        private TEntity MapBaseAuditableEntity<TDto>(TDto dto) where TDto : BaseDto
        {
            entity.MapBaseEntity(dto);
        
            entity.CreatedBy = dto.CreatedBy;
            entity.Created = dto.Created;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModified = dto.LastModified;
            entity.Active = dto.Active;
        
            return entity;
        }
    }
    
    extension(MediaAlbumDto dto)
    {
        public MediaAlbum ToEntity()
        {
            var entity = new MediaAlbum().MapBaseAuditableEntity(dto);

            entity.Name = dto.Name;
            entity.UrlFriendlyName = dto.UrlFriendlyName;
            entity.Description = dto.Description;
            entity.Media = dto.Media.Select(x => x.ToEntity()).ToList();
            entity.Tags = dto.Tags.Select(x => x.ToEntity()).ToList();
        
            return entity;
        }
    }

    extension(MediaDto dto)
    {
        public Media ToEntity()
        {
            var entity = new Media().MapBaseAuditableEntity(dto);

            entity.MediaAlbumId = dto.MediaAlbumId;
            entity.FileName = dto.FileName;
            entity.Description = dto.Description;
            entity.SizeInBytes = dto.SizeInBytes;
            entity.FileExtension = dto.FileExtension;
            entity.Tags = dto.Tags.Select(x => x.ToEntity()).ToList();

            return entity;
        }
    }

    extension(TagDto dto)
    {
        public Tag ToEntity()
        {
            var entity = new Tag().MapBaseEntity(dto);

            entity.Name = dto.Name;

            return entity;
        }
    }
}