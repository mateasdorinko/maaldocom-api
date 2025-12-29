namespace MaaldoCom.Services.Domain.Entities;

public class MediaTag : BaseEntity
{
    public Guid MediaId { get; set; }
    public Guid TagId { get; set; } 
    
    public Media Media { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}