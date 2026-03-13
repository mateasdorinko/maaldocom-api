namespace MaaldoCom.Api.Endpoints.MediaAlbums.Models;

public class GetMediaAlbumDetailResponse : GetMediaAlbumResponse
{
    [JsonPropertyOrder(7)]
    public string? Description { get; set; }

    [JsonPropertyOrder(8)]
    public bool Active { get; set; }

    [JsonPropertyOrder(9)]
    public IEnumerable<GetCommentResponse> Comments { get; set; } =  new List<GetCommentResponse>();

    [JsonPropertyOrder(10)]
    public IEnumerable<GetMediaResponse> Media { get; set; } = new List<GetMediaResponse>();
}
