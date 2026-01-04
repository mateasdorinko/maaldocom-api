namespace MaaldoCom.Services.Application.Dtos;

public class MediaDto : BaseDto
{
    public Guid MediaAlbumId { get; set; }
    public string? MediaAlbumName { get; set; }
    public string? FileName { get; set; }
    public string? Description { get; set; }
    public long SizeInBytes { get; set; }
    public string? FileExtension { get; set; }
    
    public IList<TagDto> Tags { get; set; } = new List<TagDto>();

    public override string? ToString() => FileName;
}