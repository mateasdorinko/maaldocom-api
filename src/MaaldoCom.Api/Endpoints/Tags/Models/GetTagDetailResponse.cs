namespace MaaldoCom.Api.Endpoints.Tags.Models;

public class GetTagDetailResponse : GetTagResponse
{
    [JsonPropertyOrder(5)]
    public IEnumerable<GetMediaAlbumTagResponse> MediaAlbums { get; set; } = new  List<GetMediaAlbumTagResponse>();

    [JsonPropertyOrder(6)]
    public IEnumerable<GetMediaTagResponse> Media { get; set; } = new  List<GetMediaTagResponse>();

    [JsonPropertyOrder(7)]
    public IEnumerable<GetWritingTagResponse> Writings { get; set; } = new  List<GetWritingTagResponse>();
}
