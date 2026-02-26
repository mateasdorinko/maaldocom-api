namespace MaaldoCom.Api.Domain.Entities;

public class Tag : BaseEntity
{
    public string? Name { get; set; }
    
    public ICollection<MediaAlbumTag> MediaAlbumTags { get; set; } = null!;
    public ICollection<MediaTag> MediaTags { get; set; } = null!;

    public override string? ToString() => Name;
}