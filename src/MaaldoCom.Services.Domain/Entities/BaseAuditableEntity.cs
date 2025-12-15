namespace MaaldoCom.Services.Domain.Entities;

public abstract class BaseAuditableEntity : BaseEntity
{
    public string? CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public bool Active { get; set; }
}