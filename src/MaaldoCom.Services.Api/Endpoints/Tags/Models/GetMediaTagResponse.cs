namespace MaaldoCom.Services.Api.Endpoints.Tags.Models;

public class GetMediaTagResponse
{
    [JsonPropertyOrder(1)]
    public string? MediaALbumName { get; set; }

    [JsonPropertyOrder(2)]
    public string? Name { get; set; }

    [JsonPropertyOrder(3)]
    public string? Href { get; set; }
}