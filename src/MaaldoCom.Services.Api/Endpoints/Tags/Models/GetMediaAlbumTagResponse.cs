namespace MaaldoCom.Services.Api.Endpoints.Tags.Models;

public class GetMediaAlbumTagResponse
{
    [JsonPropertyOrder(1)]
    public string? Name { get; set; }

    [JsonPropertyOrder(2)]
    public string? Href { get; set; }
}