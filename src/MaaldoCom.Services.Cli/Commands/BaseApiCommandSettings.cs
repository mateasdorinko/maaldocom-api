namespace MaaldoCom.Services.Cli.Commands;

public abstract class BaseApiCommandSettings : CommandSettings
{
    [CommandArgument(0, "<environment>")]
    [Description("The target environment (local, dev, test, prod)")]
    public required string Environment { get; init; } = "dev";

    public override ValidationResult Validate()
    {
        try
        {
            ApiEnvironmentExtensions.ParseEnvironment(Environment);
            return ValidationResult.Success();
        }
        catch (ArgumentException ex)
        {
            return ValidationResult.Error(ex.Message);
        }
    }
}