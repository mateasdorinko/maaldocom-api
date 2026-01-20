namespace MaaldoCom.Services.Cli.Infrastructure;

public enum ApiEnvironment
{
    Local,
    Dev,
    Test,
    Prod
}

public static class ApiEnvironmentExtensions
{
    public static string ToConfigKey(this ApiEnvironment environment)
    {
        return environment switch
        {
            ApiEnvironment.Local => "local",
            ApiEnvironment.Dev => "dev",
            ApiEnvironment.Test => "test",
            ApiEnvironment.Prod => "prod",
            _ => throw new ArgumentOutOfRangeException(nameof(environment))
        };
    }

    public static ApiEnvironment ParseEnvironment(string value)
    {
        return value.ToLowerInvariant() switch
        {
            "local" => ApiEnvironment.Local,
            "dev" => ApiEnvironment.Dev,
            "test" => ApiEnvironment.Test,
            "prod" => ApiEnvironment.Prod,
            _ => throw new ArgumentException($"Invalid environment: {value}. Valid values are: local, dev, test, prod", nameof(value))
        };
    }
}
