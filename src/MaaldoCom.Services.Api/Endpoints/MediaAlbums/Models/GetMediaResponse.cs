namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;

public class GetMediaResponse : BaseModel
{
    public string? FileName { get; set; }
    public string? Description { get; set; }
}