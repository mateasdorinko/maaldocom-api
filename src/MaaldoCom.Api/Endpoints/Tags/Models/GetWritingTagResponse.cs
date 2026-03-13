namespace MaaldoCom.Api.Endpoints.Tags.Models;

public class GetWritingTagResponse
{
    [JsonPropertyOrder(1)]
    public Guid WritingId { get; set; }

    [JsonPropertyOrder(2)]
    public string? Title { get; set; }

    [JsonPropertyOrder(3)]
    public string? Slug { get; set; }

    [JsonPropertyOrder(4)]
    public string? Href { get; set; }
}
