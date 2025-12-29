namespace MaaldoCom.Services.Domain.Entities;

public class MediaAlbumTag : BaseEntity
{
    public Guid MediaAlbumId { get; set; }
    public Guid TagId { get; set; } 
    
    public MediaAlbum MediaAlbum { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}