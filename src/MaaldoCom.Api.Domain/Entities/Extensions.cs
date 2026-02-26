using MaaldoCom.Api.Domain.Extensions;

namespace MaaldoCom.Api.Domain.Entities;

public static class Extensions
{
    extension(BaseAuditableEntity entity)
    {
        public void InitializeForCreate(ClaimsPrincipal principal)
        {
            if (entity.Created.Equals(DateTime.MinValue)) { entity.Created = DateTime.UtcNow; }

            entity.CreatedBy = principal.GetUserId();
            entity.LastModifiedBy = principal.GetUserId();
            entity.Active = true;
        }
    }
}