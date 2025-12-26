namespace MaaldoCom.Services.Domain.Entities;

public class Tag : BaseEntity
{
    public string? Name { get; set; }
    
    public ICollection<MediaAlbum> MediaAlbums { get; set; } = null!;
    public ICollection<Media> Media { get; set; } = null!;
}