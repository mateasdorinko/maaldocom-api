namespace MaaldoCom.Services.Domain.Entities;

[Table("MediaAlbums")]
public class MediaAlbum : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? UrlFriendlyName { get; set; }
    public string? Description { get; set; }
    public IList<Medium> Media { get; set; }

    public MediaAlbum()
    {
        Media = new List<Medium>();
    }

    public override string? ToString() => Name;
}