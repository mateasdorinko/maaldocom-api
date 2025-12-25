namespace MaaldoCom.Services.Domain.Entities;

[Table("Media")]
public class Medium : BaseAuditableEntity
{
    public int MediaAlbumId { get; set; }
    public string? FileName { get; set; }
    public string? Description { get; set; }
    public long SizeInBytes { get; set; }
    public string? FileExtension { get; set; }
    
    public MediaAlbum MediaAlbum { get; set; }
    public ICollection<Tag> Tags { get; set; }

    public override string? ToString() => FileName;
}