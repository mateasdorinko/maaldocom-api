namespace MaaldoCom.Api.Application.Dtos;

public class TagDto : BaseDto
{
    public string? Name { get; set; }

    public IList<MediaAlbumDto> MediaAlbums { get; set; } = new List<MediaAlbumDto>();
    public IList<MediaDto> Media { get; set; } = new List<MediaDto>();
    public int Count { get; set; }
}
