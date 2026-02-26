namespace MaaldoCom.Api.Domain.Entities;

public class MediaTag
{
    public Guid MediaId { get; set; }
    public Guid TagId { get; set; } 
    
    public Media Media { get; set; } = null!;
    public Tag Tag { get; set; } = null!;

    public override string ToString() => $"{Media}:{Tag}";
}