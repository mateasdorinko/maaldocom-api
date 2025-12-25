namespace MaaldoCom.Services.Domain.Entities;

[Table("Tags")]
public class Tag : BaseEntity
{
    public string? Name { get; set; }
    
    public ICollection<MediaAlbum> MediaAlbums { get; set; } = null!;
    public ICollection<Medium> Media { get; set; } = null!;
}