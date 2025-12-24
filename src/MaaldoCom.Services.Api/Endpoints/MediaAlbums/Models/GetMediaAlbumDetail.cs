namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums.Models;

public class GetMediaAlbumDetail : GetMediaAlbum
{
    public IList<GetMedium> Media { get; set; } = new List<GetMedium>();
}