namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;

public class GetMediaAlbumResponse : BaseModel
{
    public string? Name { get; set; }
    public string? UrlFriendlyName { get; set; }
    public string? Description { get; set; }
    public IList<GetMediaResponse> Media { get; set; } = new List<GetMediaResponse>();
}