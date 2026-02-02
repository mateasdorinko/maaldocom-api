namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;

public class PostMediumRequest
{
    [JsonPropertyOrder(1)]
    public string? FileName { get; set; }

    [JsonPropertyOrder(2)]
    public string? Description { get; set; }

    [JsonPropertyOrder(3)]
    public long SizeInBytes { get; set; }

    [JsonPropertyOrder(4)]
    public string? FileExtension { get; set; }

    [JsonPropertyOrder(5)]
    public IEnumerable<string> Tags { get; set; } = [];
}