namespace MaaldoCom.Services.Cli;

public static class Styles
{
    public static Style Error = new(foreground: Color.Red, decoration: Decoration.Bold);
    public static Style Warning = new(foreground: Color.Yellow);

    public static void WriteError(string message) => AnsiConsole.Write(new Markup($"{message}\n", Error));
    public static void WriteWarning(string message) => AnsiConsole.Write(new Markup($"{message}\n", Warning));
}