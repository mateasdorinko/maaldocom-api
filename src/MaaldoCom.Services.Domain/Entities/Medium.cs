namespace MaaldoCom.Services.Domain.Entities;

[Table("Medium")]
public class Medium : BaseAuditableEntity
{
    public string? FileName { get; set; }
    public string? Description { get; set; }

    public override string? ToString() => FileName;
}