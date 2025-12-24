namespace MaaldoCom.Services.Application.Dtos;

public class MediaAlbumDto : BaseDto
{
    public string? Name { get; set; }
    public string? UrlFriendlyName { get; set; }
    public string? Description { get; set; }
    public IList<MediumDto> Media { get; set; } = new List<MediumDto>();

    public override string? ToString() => Name;
}