namespace MaaldoCom.Services.Api.Endpoints.Knowledge.Models;

public class GetKnowledgeResponse : BaseModel
{
    [JsonPropertyOrder(2)]
    public string? Title { get; set; }

    [JsonPropertyOrder(3)]
    public string? Quote { get; set; }
    
    [JsonPropertyOrder(1)]
    public override string? Href => UrlMaker.GetKnowledgeUrl(Id);
}