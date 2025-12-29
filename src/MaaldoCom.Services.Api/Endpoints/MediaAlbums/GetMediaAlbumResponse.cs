using MaaldoCom.Services.Api.Endpoints.Tags;

namespace MaaldoCom.Services.Api.Endpoints.MediaAlbums;

public class GetMediaAlbumResponse : BaseModel
{
    public string? Name { get; set; }
    public string? UrlFriendlyName { get; set; }
    public DateTime Created { get; set; }
    public IEnumerable<GetTagResponse> Tags { get; set; } = new List<GetTagResponse>();
}