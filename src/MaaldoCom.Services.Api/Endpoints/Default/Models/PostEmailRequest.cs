namespace MaaldoCom.Services.Api.Endpoints.Default.Models;

public class PostEmailRequest
{
    public required string From { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}
