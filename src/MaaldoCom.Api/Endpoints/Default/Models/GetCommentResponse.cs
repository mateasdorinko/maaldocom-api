namespace MaaldoCom.Api.Endpoints.Default.Models;

public class GetCommentResponse
{
    [JsonPropertyOrder(1)]
    public string? Author { get; set; }

    [JsonPropertyOrder(2)]
    public string? Body { get; set; }
}
