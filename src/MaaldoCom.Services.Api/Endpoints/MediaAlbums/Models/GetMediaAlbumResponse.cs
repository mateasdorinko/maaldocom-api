namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;

public class GetMediaAlbumResponse : BaseModel
{
    [JsonPropertyOrder(3)]
    public string? Name { get; set; }

    [JsonPropertyOrder(4)]
    public string? UrlFriendlyName { get; set; }

    [JsonPropertyOrder(5)]
    public DateTime Created { get; set; }

    [JsonPropertyOrder(6)]
    public IEnumerable<string> Tags { get; set; } = new List<string>();
    
    [JsonPropertyOrder(1)]
    public override string? Href => UrlMaker.GetMediaAlbumUrl(Id);

    [JsonPropertyOrder(2)]
    public string? AltHref => UrlMaker.GetMediaAlbumUrl(UrlFriendlyName!);
}