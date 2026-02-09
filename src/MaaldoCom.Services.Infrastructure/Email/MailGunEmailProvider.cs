using System.Net.Http.Headers;
using System.Text;

namespace MaaldoCom.Services.Infrastructure.Email;

public class MailGunEmailProvider(string apiKey, string domain, string apiBaseUrl, string defaultFrom, string defaultTo) : IEmailProvider
{
    public async Task<EmailResponse> SendEmailAsync(string to, string from, string subject, string body, CancellationToken ct)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(apiBaseUrl);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{apiKey}")));

        var formContent = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("from", from),
            new KeyValuePair<string, string>("to", to),
            new KeyValuePair<string, string>("subject", subject),
            new KeyValuePair<string, string>("text", body)
        ]);

        var requestUri = $"/v3/{domain}/messages";
        var response = await client.PostAsync(requestUri, formContent, ct);

        return ToEmailResponse(response);
    }

    public async Task<EmailResponse> SendEmailAsync(string from, string subject, string body, CancellationToken ct) =>
        await SendEmailAsync(defaultTo, from, subject, body, ct);

    public async Task<EmailResponse> SendEmailAsync(string subject, string body, CancellationToken ct) =>
        await SendEmailAsync(defaultFrom, subject, body, ct);

    private static EmailResponse ToEmailResponse(HttpResponseMessage response) =>
        new()
        {
            Body = response.Content,
            Headers = response.Headers,
            IsSuccessStatusCode = response.IsSuccessStatusCode,
            StatusCode = response.StatusCode
        };
}
