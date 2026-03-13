namespace MaaldoCom.Api.Domain.Entities;

public class Writing : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public string? Blurb { get; set; }
    public string? Body { get; set; }
    public ICollection<WritingTag> WritingTags { get; set; } = null!;
    public ICollection<WritingComment> WritingComments { get; set; } = null!;

    public override string? ToString() => Title;
}
