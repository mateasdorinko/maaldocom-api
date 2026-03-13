namespace MaaldoCom.Api.Domain.Entities;

public class WritingTag
{
    public Guid WritingId { get; set; }
    public Guid TagId { get; set; }

    public Writing Writing { get; set; } = null!;
    public Tag Tag { get; set; } = null!;

    public override string ToString() => $"{Writing}:{Tag}";
}
