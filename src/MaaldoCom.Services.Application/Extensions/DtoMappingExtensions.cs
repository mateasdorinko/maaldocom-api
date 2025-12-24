using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Domain.Entities;

namespace MaaldoCom.Services.Application.Extensions;

internal static class DtoMappingExtensions
{
    private static TDto MapBase<TDto, TEntity>(this TDto dto, TEntity entity)
        where TDto : BaseDto
        where TEntity : BaseAuditableEntity
    {
        dto.Id = entity.Id;
        dto.Guid = entity.Guid;
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
            var dto = new MediaAlbumDto().MapBase(entity);

            dto.Name = entity.Name;
            dto.UrlFriendlyName = entity.UrlFriendlyName;
            dto.Description = entity.Description;
            dto.Media = entity.Media.Select(x => x.ToDto()).ToList();

            return dto;
        }
    }

    extension(Medium entity)
    {
        public MediumDto ToDto()
        {
            var dto = new MediumDto().MapBase(entity);

            dto.FileName = entity.FileName;
            dto.Description = entity.Description;

            return dto;
        }
    }
}