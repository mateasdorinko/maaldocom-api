namespace MaaldoCom.Api.Domain.Entities;

public class MediaAlbumComment
{
    public Guid MediaAlbumId { get; set; }
    public Guid CommentId { get; set; }

    public MediaAlbum MediaAlbum { get; set; } = null!;
    public Comment Comment { get; set; } = null!;

    public override string ToString() => $"{MediaAlbum}:{Comment}";
}
