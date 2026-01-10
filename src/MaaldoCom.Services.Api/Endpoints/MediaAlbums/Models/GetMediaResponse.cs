namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;

public class GetMediaResponse : BaseModel
{
    [JsonPropertyOrder(2)]
    public string? FileName { get; set; }

    [JsonPropertyOrder(3)]
    public string? Description { get; set; }

    [JsonPropertyOrder(4)]
    public long SizeInBytes { get; set; }

    [JsonPropertyOrder(5)]
    public IEnumerable<string> Tags { get; set; } = new List<string>();
    
    [JsonIgnore]
    public Guid MediaAlbumId { get; set; }

    [JsonPropertyOrder(6)]
    public string? BlobUrl { get; set; }
    
    public override string? Href => UrlMaker.GetMediaUrl(MediaAlbumId, Id);
}