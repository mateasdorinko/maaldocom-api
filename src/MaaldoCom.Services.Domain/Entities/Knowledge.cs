namespace MaaldoCom.Services.Domain.Entities;

[Table("Knowledge")]
public class Knowledge : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Quote { get; set; }
}