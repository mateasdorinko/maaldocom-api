namespace MaaldoCom.Api.Domain.Entities;

public class WritingComment
{
    public Guid WritingId { get; set; }
    public Guid CommentId { get; set; }

    public Writing Writing { get; set; } = null!;
    public Comment Comment { get; set; } = null!;

    public override string ToString() => $"{Writing}:{Comment}";
}
