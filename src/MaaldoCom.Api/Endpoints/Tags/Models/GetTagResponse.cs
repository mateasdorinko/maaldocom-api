namespace MaaldoCom.Api.Endpoints.Tags.Models;

public class GetTagResponse : BaseModel
{
    [JsonPropertyOrder(1)]
    public override string? Href => UrlMaker.GetTagUrl(Id);

    [JsonPropertyOrder(2)]
    public string? AltHref => UrlMaker.GetTagUrl(Name!);

    [JsonPropertyOrder(3)]
    public string? Name { get; set; }

    [JsonPropertyOrder(4)]
    public int Count { get; set; }
}
