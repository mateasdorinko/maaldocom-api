namespace MaaldoCom.Api.Application.Email;

public interface IEmailProvider
{
    Task<EmailResponse> SendEmailAsync(string to, string from, string subject, string body, CancellationToken ct);
    Task<EmailResponse> SendEmailAsync(string from, string subject, string body, CancellationToken ct);
    Task<EmailResponse> SendEmailAsync(string subject, string body, CancellationToken ct);
}
