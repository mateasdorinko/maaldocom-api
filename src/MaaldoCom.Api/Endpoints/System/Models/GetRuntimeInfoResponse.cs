namespace MaaldoCom.Api.Endpoints.System.Models;

public class GetRuntimeInfoResponse
{
    public string? AspNetCoreEnvironment { get; set; }
    public string? ClrVersion { get; set; }
    public bool Is64BitProcess { get; set; }
    public bool Is64BitSystem { get; set; }
    public string? MachineName { get; set; }
    public string? OsVersion { get; set; }
    public int ProcessId { get; set; }
    public int ProcessorCount { get; set; }
    public string? ProcessPath { get; set; }
    public string? User { get; set; }
}
