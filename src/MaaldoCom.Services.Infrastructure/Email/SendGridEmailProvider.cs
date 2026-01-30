using SendGrid;
using SendGrid.Helpers.Mail;

namespace MaaldoCom.Services.Infrastructure.Email;

public class SendGridEmailProvider(string apiKey, string defaultFromAddress, string defaultToAddress) : IEmailProvider
{
    public async Task<EmailResponse> SendEmailAsync(string to, string from, string subject, string plainTextBody, string htmlBody)
    {
        var client = new SendGridClient(apiKey);

        var fromAddress = new EmailAddress(from);
        var toAddress = new EmailAddress(to);

        var message = MailHelper.CreateSingleEmail(fromAddress, toAddress, subject, plainTextBody, htmlBody);

        var sendgridResponse = await client.SendEmailAsync(message);

        return ToEmailResponse(sendgridResponse);
    }

    public async Task<EmailResponse> SendEmailAsync(string to, string subject, string plainTextBody, string htmlBody) =>
        await SendEmailAsync(to, defaultFromAddress, subject, plainTextBody, htmlBody);

    public async Task<EmailResponse> SendEmailAsync(string subject, string plainTextBody, string htmlBody) =>
        await SendEmailAsync(defaultToAddress, subject, plainTextBody, htmlBody);

    private static EmailResponse ToEmailResponse(Response response) =>
        new()
        {
            Body = response.Body,
            Headers = response.Headers,
            IsSuccessStatusCode = response.IsSuccessStatusCode,
            StatusCode = response.StatusCode
        };
}