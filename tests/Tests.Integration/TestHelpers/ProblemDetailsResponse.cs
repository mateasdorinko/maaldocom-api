namespace Tests.Integration.TestHelpers;

public class ProblemDetailsResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public Errors Errors { get; set; } = new();
}

public class Errors
{
    public List<string> GeneralErrors { get; set; } = new List<string>();
}
