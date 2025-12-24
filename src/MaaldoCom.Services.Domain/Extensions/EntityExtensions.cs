using MaaldoCom.Services.Domain.Entities;

namespace MaaldoCom.Services.Domain.Extensions;

public static class EntityExtensions
{
    extension(BaseAuditableEntity entity)
    {
        public void InitializeForCreate(ClaimsPrincipal principal)
        {
            entity.Active = true;
        
            if (entity.Created.Equals(DateTime.MinValue)) { entity.Created = DateTime.UtcNow; }
        
            entity.CreatedBy = principal.GetUserId();
            entity.LastModifiedBy = principal.GetUserId();
        }
    }
}