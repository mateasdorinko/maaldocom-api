namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;

public class GetMediaAlbum : BaseModel
{
    public string? Name { get; set; }
    public string? UrlFriendlyName { get; set; }
    public string? Description { get; set; }
}