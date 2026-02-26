namespace MaaldoCom.Api.Domain.Entities;

public class Media : BaseAuditableEntity
{
    public Guid MediaAlbumId { get; set; }
    public string? FileName { get; set; }
    public string? Description { get; set; }
    public long SizeInBytes { get; set; }
    public string? FileExtension { get; set; }
    
    public MediaAlbum MediaAlbum { get; set; } = null!;
    public ICollection<MediaTag> MediaTags { get; set; } = null!;

    public override string ToString() => $"{MediaAlbum}:{FileName}";
}