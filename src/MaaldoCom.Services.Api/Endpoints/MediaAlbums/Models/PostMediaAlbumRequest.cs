namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;

public class PostMediaAlbumRequest
{
    [JsonPropertyOrder(1)]
    public string? Name { get; set; }

    [JsonPropertyOrder(2)]
    public string? UrlFriendlyName { get; set; }

    [JsonPropertyOrder(3)]
    public DateTime Created { get; set; }

    [JsonPropertyOrder(4)]
    public string? Description { get; set; }

    [JsonPropertyOrder(5)]
    public IEnumerable<string> Tags { get; set; } = [];

    [JsonPropertyOrder(6)]
    public IEnumerable<PostMediumRequest> Media { get; set; } = [];
}
