namespace MaaldoCom.Api.Endpoints.Writings.Models;

public class GetWritingDetailResponse : GetWritingResponse
{
    [JsonPropertyOrder(8)]
    public IEnumerable<GetCommentResponse> Comments { get; set; } =  new List<GetCommentResponse>();

    [JsonPropertyOrder(9)]
    public string Body { get; set; } = string.Empty;
}
