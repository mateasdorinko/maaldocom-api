namespace MaaldoCom.Api.Domain.Entities;

public class Comment : BaseAuditableEntity
{
    public string? Author { get; set; }
    public string? Body { get; set; }

    public ICollection<MediaAlbumComment> MediaAlbumComments { get; set; } = null!;
    public ICollection<MediaComment> MediaComments { get; set; } = null!;
    public ICollection<WritingComment> WritingComments { get; set; } = null!;

    public override string? ToString() => $"{Author}:{Body}";
}
