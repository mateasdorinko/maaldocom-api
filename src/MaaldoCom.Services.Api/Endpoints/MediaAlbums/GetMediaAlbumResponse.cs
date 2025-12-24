namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumResponse : BaseModel
{
    public string? Name { get; set; }
    public string? UrlFriendlyName { get; set; }
    public string? Description { get; set; }
    public IList<GetMediumResponse> Media { get; set; } = new List<GetMediumResponse>();
}