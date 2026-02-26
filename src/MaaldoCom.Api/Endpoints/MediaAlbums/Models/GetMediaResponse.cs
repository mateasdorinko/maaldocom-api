namespace MaaldoCom.Api.Endpoints.MediaAlbums.Models;

public class GetMediaResponse : BaseModel
{
    [JsonPropertyOrder(4)]
    public string? FileName { get; set; }

    [JsonPropertyOrder(5)]
    public string? Description { get; set; }

    [JsonPropertyOrder(6)]
    public long SizeInBytes { get; set; }

    [JsonPropertyOrder(7)]
    public string? ContentType { get; set; }

    [JsonPropertyOrder(8)]
    public IEnumerable<string> Tags { get; set; } = new List<string>();

    [JsonIgnore]
    public Guid MediaAlbumId { get; set; }

    [JsonPropertyOrder(1)]
    public override string? Href => UrlMaker.GetOriginalMediaUrl(MediaAlbumId, Id);

    [JsonPropertyOrder(2)]
    public string? ThumbHref => UrlMaker.GetThumbnailMediaUrl(MediaAlbumId, Id);

    [JsonPropertyOrder(3)]
    public string? ViewerHref => UrlMaker.GetViewerMediaUrl(MediaAlbumId, Id);
}
