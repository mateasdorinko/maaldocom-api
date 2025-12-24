namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class ListMediaAlbumsResponse
{
    public IEnumerable<GetMediaAlbumResponse> MediaAlbums { get; set; } = new List<GetMediaAlbumResponse>();
}