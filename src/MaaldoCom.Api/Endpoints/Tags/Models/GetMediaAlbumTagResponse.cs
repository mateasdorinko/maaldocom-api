namespace MaaldoCom.Api.Endpoints.Tags.Models;

public class GetMediaAlbumTagResponse
{
    [JsonPropertyOrder(1)]
    public Guid MediaAlbumId { get; set; }

    [JsonPropertyOrder(2)]
    public string? Name { get; set; }

    [JsonPropertyOrder(3)]
    public string? UrlFriendlyName { get; set; }

    [JsonPropertyOrder(4)]
    public string? Href { get; set; }
}
