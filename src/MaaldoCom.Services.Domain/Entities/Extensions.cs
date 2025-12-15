namespace MaaldoCom.Services.Domain.Entities;

public static class Extensions
{
    public static void InitializeForCreate(this BaseAuditableEntity entity, ClaimsPrincipal principal)
    {
        entity.Active = true;
        
        if (entity.Created.Equals(DateTime.MinValue)) { entity.Created = DateTime.UtcNow; }
        
        entity.CreatedBy = principal.GetUserId();
        entity.LastModifiedBy = principal.GetUserId();
    }
}