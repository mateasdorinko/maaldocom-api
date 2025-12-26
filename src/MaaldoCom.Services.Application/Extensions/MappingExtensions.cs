using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Domain.Entities;

namespace MaaldoCom.Services.Application.Extensions;

public static class MappingExtensions
{   
    // ToEntity
    private static TEntity MapBaseEntity<TEntity, TDto>(this TEntity entity, TDto dto)
        where TEntity : BaseAuditableEntity
        where TDto : BaseDto
    {
        entity.Id = dto.Id;
        entity.Uid = dto.Guid;
        entity.CreatedBy = dto.CreatedBy;
        entity.Created = dto.Created;
        entity.LastModifiedBy = dto.LastModifiedBy;
        entity.LastModified = dto.LastModified;
        entity.Active = dto.Active;
        
        return entity;
    }
    
    extension(MediaAlbumDto dto)
    {
        public MediaAlbum ToEntity()
        {
            var entity = new MediaAlbum().MapBaseEntity(dto);

            entity.Name = dto.Name;
            entity.UrlFriendlyName = dto.UrlFriendlyName;
            entity.Description = dto.Description;
            entity.Media = dto.Media.Select(x => x.ToEntity()).ToList();
        
            return entity;
        }
    }

    extension(MediaDto dto)
    {
        public Media ToEntity()
        {
            var entity = new Media().MapBaseEntity(dto);

            entity.FileName = dto.FileName;
            entity.Description = dto.Description;

            return entity;
        }
    }
    
    // ToDto
    private static TDto MapBaseDto<TDto, TEntity>(this TDto dto, TEntity entity) 
        where TDto : BaseDto
        where TEntity : BaseAuditableEntity
    {
        dto.Id = entity.Id;
        dto.Guid = entity.Uid;
        dto.CreatedBy = entity.CreatedBy;
        dto.Created = entity.Created;
        dto.LastModifiedBy = entity.LastModifiedBy;
        dto.LastModified = entity.LastModified;
        dto.Active = entity.Active;
        
        return dto;
    }
    
    extension(MediaAlbum entity)
    {
        public MediaAlbumDto ToDto()
        {
            var dto = new MediaAlbumDto().MapBaseDto(entity);

            dto.Name = entity.Name;
            dto.UrlFriendlyName = entity.UrlFriendlyName;
            dto.Description = entity.Description;
            dto.Media = entity.Media.Select(x => x.ToDto()).ToList();

            return dto;
        }
    }

    extension(Media entity)
    {
        public MediaDto ToDto()
        {
            var dto = new MediaDto().MapBaseDto(entity);

            dto.FileName = entity.FileName;
            dto.Description = entity.Description;

            return dto;
        }
    }
}