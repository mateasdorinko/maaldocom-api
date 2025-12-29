namespace MaaldoCom.Services.Domain.Entities;

public class MediaAlbum : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? UrlFriendlyName { get; set; }
    public string? Description { get; set; }
    public ICollection<Media> Media { get; set; } = null!;
    public ICollection<MediaAlbumTag> Tags { get; set; } = null!;

    public override string? ToString() => Name;
}