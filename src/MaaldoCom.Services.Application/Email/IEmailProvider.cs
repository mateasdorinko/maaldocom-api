namespace MaaldoCom.Services.Application.Email;

public interface IEmailProvider
{
    Task<EmailResponse> SendEmailAsync(string to, string from, string subject, string plainTextBody, string htmlBody);
    Task<EmailResponse> SendEmailAsync(string to, string subject, string plainTextBody, string htmlBody);
    Task<EmailResponse> SendEmailAsync(string subject, string plainTextBody, string htmlBody);
}