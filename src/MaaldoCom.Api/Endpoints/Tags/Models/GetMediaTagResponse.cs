namespace MaaldoCom.Api.Endpoints.Tags.Models;

public class GetMediaTagResponse
{
    [JsonPropertyOrder(1)]
    public Guid MediaAlbumId { get; set; }

    [JsonPropertyOrder(2)]
    public string? MediaAlbumName { get; set; }

    [JsonPropertyOrder(3)]
    public string? MediaAlbumUrlFriendlyName { get; set; }

    [JsonPropertyOrder(4)]
    public Guid MediaId { get; set; }

    [JsonPropertyOrder(5)]
    public string? Name { get; set; }

    [JsonPropertyOrder(6)]
    public string? Href { get; set; }
}
