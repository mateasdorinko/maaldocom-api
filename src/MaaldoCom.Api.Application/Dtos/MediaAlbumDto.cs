namespace MaaldoCom.Api.Application.Dtos;

public class MediaAlbumDto : BaseDto
{
    public string? Name { get; set; }
    public string? UrlFriendlyName { get; set; }
    public string? Description { get; set; }
    public IList<MediaDto> Media { get; set; } = new List<MediaDto>();
    public IList<TagDto> Tags { get; set; } = new List<TagDto>();
    public Guid DefaultMediaId { get; set; }

    public override string? ToString() => Name;
}
