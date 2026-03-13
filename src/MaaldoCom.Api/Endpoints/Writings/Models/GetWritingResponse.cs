namespace MaaldoCom.Api.Endpoints.Writings.Models;

public class GetWritingResponse : BaseModel
{
    [JsonPropertyOrder(3)]
    public string? Title { get; set; }

    [JsonPropertyOrder(4)]
    public string? Slug { get; set; }

    [JsonPropertyOrder(5)]
    public DateTime Created { get; set; }

    [JsonPropertyOrder(6)]
    public string? Blurb { get; set; }

    [JsonPropertyOrder(7)]
    public IEnumerable<string> Tags { get; set; } = new List<string>();

    [JsonPropertyOrder(1)]
    public override string? Href => UrlMaker.GetWritingUrl(Id);

    [JsonPropertyOrder(2)]
    public string? AltHref => UrlMaker.GetWritingUrl(Slug!);
}
