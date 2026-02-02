namespace MaaldoCom.Services.Application.Email;

public interface IEmailProvider
{
    Task<EmailResponse> SendEmailAsync(string to, string from, string subject, string body);
    Task<EmailResponse> SendEmailAsync(string to, string subject, string body);
    Task<EmailResponse> SendEmailAsync(string subject, string body);
}