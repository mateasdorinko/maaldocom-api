using System.Net;
using System.Net.Http.Headers;

namespace MaaldoCom.Api.Application.Email;

public class EmailResponse
{
    public HttpContent? Body { get; set; }
    public HttpResponseHeaders? Headers { get; set; }
    public bool IsSuccessStatusCode { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}