namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;

public class GetMediaByIdRequest
{
    public Guid MediaAlbumId { get; set; }
    public Guid MediaId { get; set; }
}