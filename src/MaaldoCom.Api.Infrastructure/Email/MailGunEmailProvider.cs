using System.Net.Http.Headers;
using System.Text;

namespace MaaldoCom.Api.Infrastructure.Email;

public class MailGunEmailProvider : IEmailProvider
{
    private readonly HttpClient _httpClient;
    private readonly string _domain;
    private readonly string _defaultFrom;
    private readonly string _defaultTo;

    public MailGunEmailProvider(string apiKey, string domain, string apiBaseUrl, string defaultFrom, string defaultTo)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(apiBaseUrl);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{apiKey}")));
        _domain = domain;
        _defaultFrom = defaultFrom;
        _defaultTo = defaultTo;
    }

    internal MailGunEmailProvider(HttpClient httpClient, string domain, string defaultFrom, string defaultTo)
    {
        _httpClient = httpClient;
        _domain = domain;
        _defaultFrom = defaultFrom;
        _defaultTo = defaultTo;
    }

    public async Task<EmailResponse> SendEmailAsync(string to, string from, string subject, string body, CancellationToken ct)
    {
        var formContent = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("from", from),
            new KeyValuePair<string, string>("to", to),
            new KeyValuePair<string, string>("subject", subject),
            new KeyValuePair<string, string>("text", body)
        ]);

        var requestUri = $"/v3/{_domain}/messages";
        var response = await _httpClient.PostAsync(requestUri, formContent, ct);

        return ToEmailResponse(response);
    }

    public async Task<EmailResponse> SendEmailAsync(string from, string subject, string body, CancellationToken ct) =>
        await SendEmailAsync(_defaultTo, from, subject, body, ct);

    public async Task<EmailResponse> SendEmailAsync(string subject, string body, CancellationToken ct) =>
        await SendEmailAsync(_defaultFrom, subject, body, ct);

    private static EmailResponse ToEmailResponse(HttpResponseMessage response) =>
        new()
        {
            Body = response.Content,
            Headers = response.Headers,
            IsSuccessStatusCode = response.IsSuccessStatusCode,
            StatusCode = response.StatusCode
        };
}
