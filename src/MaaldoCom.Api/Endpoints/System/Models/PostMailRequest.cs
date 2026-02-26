namespace MaaldoCom.Api.Endpoints.System.Models;

public class PostMailRequest
{
    public required string From { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}
