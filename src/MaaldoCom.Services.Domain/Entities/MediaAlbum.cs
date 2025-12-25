namespace MaaldoCom.Services.Domain.Entities;

[Table("MediaAlbums")]
public class MediaAlbum : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? UrlFriendlyName { get; set; }
    public string? Description { get; set; }
    public ICollection<Medium> Media { get; set; }
    public ICollection<Tag> Tags { get; set; }

    public override string? ToString() => Name;
}