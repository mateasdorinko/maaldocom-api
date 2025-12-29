namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumDetailResponse : GetMediaAlbumResponse
{
    //public string? Name { get; set; }
    //public string? UrlFriendlyName { get; set; }
    //public DateTime Created { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; }
    public IEnumerable<GetMediaResponse> Media { get; set; } = new List<GetMediaResponse>();
    //public IEnumerable<string> Tags { get; set; } = new List<string>();
}