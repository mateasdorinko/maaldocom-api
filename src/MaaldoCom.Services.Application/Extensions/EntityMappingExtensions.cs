using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Domain.Entities;

namespace MaaldoCom.Services.Application.Extensions;

internal static class EntityMappingExtensions
{
    private static TEntity MapAuditableFields<TEntity, TDto>(this TEntity entity, TDto dto)
        where TEntity : BaseAuditableEntity
        where TDto : BaseDto
    {
        entity.Id = dto.Id;
        entity.Guid = dto.Guid;
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
            var entity = new MediaAlbum().MapAuditableFields(dto);

            entity.Name = dto.Name;
            entity.UrlFriendlyName = dto.UrlFriendlyName;
            entity.Description = dto.Description;
            entity.Media = dto.Media.Select(x => x.ToEntity()).ToList();
        
            return entity;
        }
    }

    extension(MediumDto dto)
    {
        public Medium ToEntity()
        {
            var entity = new Medium().MapAuditableFields(dto);

            entity.FileName = dto.FileName;
            entity.Description = dto.Description;

            return entity;
        }
    }
}