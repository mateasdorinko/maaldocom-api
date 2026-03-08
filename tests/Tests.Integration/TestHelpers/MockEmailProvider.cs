using MaaldoCom.Api.Application.Email;

namespace Tests.Integration.TestHelpers;

public class MockEmailProvider : IEmailProvider
{
    public Task<EmailResponse> SendEmailAsync(string to, string from, string subject, string body, CancellationToken ct)
        => Task.FromResult(new EmailResponse { IsSuccessStatusCode = true, StatusCode = HttpStatusCode.OK });

    public Task<EmailResponse> SendEmailAsync(string from, string subject, string body, CancellationToken ct)
        => Task.FromResult(new EmailResponse { IsSuccessStatusCode = true, StatusCode = HttpStatusCode.OK });

    public Task<EmailResponse> SendEmailAsync(string subject, string body, CancellationToken ct)
        => Task.FromResult(new EmailResponse { IsSuccessStatusCode = true, StatusCode = HttpStatusCode.OK });
}
