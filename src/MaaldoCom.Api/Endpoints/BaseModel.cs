namespace MaaldoCom.Api.Endpoints;

public abstract class BaseModel
{
    [JsonPropertyOrder(0)]
    public Guid Id { get; set; }

    public abstract string? Href { get; }
}