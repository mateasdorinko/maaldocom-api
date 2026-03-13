namespace MaaldoCom.Api.Domain.Entities;

public class MediaComment
{
    public Guid MediaId { get; set; }
    public Guid CommentId { get; set; }

    public Media Media { get; set; } = null!;
    public Comment Comment { get; set; } = null!;

    public override string ToString() => $"{Media}:{Comment}";
}
