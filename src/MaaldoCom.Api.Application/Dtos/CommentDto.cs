namespace MaaldoCom.Api.Application.Dtos;

public class CommentDto : BaseDto
{
    public string? Author { get; set; }
    public string? Body { get; set; }

    public IList<MediaAlbumDto> MediaAlbums { get; set; } = new List<MediaAlbumDto>();
    public IList<MediaDto> Media { get; set; } = new List<MediaDto>();
    public IList<WritingDto> Writings { get; set; } = new List<WritingDto>();

    public override string? ToString() => $"{Author}:{Body}";
}
