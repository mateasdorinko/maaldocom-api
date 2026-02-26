namespace MaaldoCom.Api.Endpoints.MediaAlbums.Models;

public class GetMediaAlbumResponse : BaseModel
{
    [JsonIgnore]
    public Guid DefaultMediaId { get; set; }

    [JsonPropertyOrder(4)]
    public string? Name { get; set; }

    [JsonPropertyOrder(5)]
    public string? UrlFriendlyName { get; set; }

    [JsonPropertyOrder(6)]
    public DateTime Created { get; set; }

    [JsonPropertyOrder(7)]
    public IEnumerable<string> Tags { get; set; } = new List<string>();

    [JsonPropertyOrder(1)]
    public override string? Href => UrlMaker.GetMediaAlbumUrl(Id);

    [JsonPropertyOrder(2)]
    public string? AltHref => UrlMaker.GetMediaAlbumUrl(UrlFriendlyName!);

    [JsonPropertyOrder(3)]
    public string? ThumbHref => UrlMaker.GetThumbnailMediaUrl(Id, DefaultMediaId);
}
