using MaaldoCom.Services.Api.Endpoints.Tags;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaResponse : BaseModel
{
    public string? FileName { get; set; }
    public string? Description { get; set; }
    public IEnumerable<GetTagResponse> Tags { get; set; } = new List<GetTagResponse>();
}