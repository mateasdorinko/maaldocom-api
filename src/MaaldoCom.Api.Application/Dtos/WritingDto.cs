namespace MaaldoCom.Api.Application.Dtos;

public class WritingDto : BaseDto
{
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public string? Blurb { get; set; }
    public string? Body { get; set; }
    public IList<TagDto> Tags { get; set; } = new List<TagDto>();
    public IList<CommentDto> Comments { get; set; } = new List<CommentDto>();

    public override string? ToString() => Title;
}
