namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;

public class ListMediaAlbumsResponse
{
    public IEnumerable<GetMediaAlbumResponse> MediaAlbums { get; set; } = new List<GetMediaAlbumResponse>();
}