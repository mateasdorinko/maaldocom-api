namespace MaaldoCom.Services.Api.Endpoints.System.Models;

public class GetRuntimeInfoResponse
{
    public string? AspNetCoreEnvironment { get; set; }
    public string? MachineName { get; set; }
    public string? OsVersion { get; set; }
    public string? ClrVersion { get; set; }
}
