using MaaldoCom.Services.Cli;

AnsiConsole.MarkupLine("[red bold]Hello world![/]");
AnsiConsole.MarkupLine("Hello world!");
AnsiConsole.MarkupLine("[red on white]Hello world![/]");

Styles.WriteError("This is an error message.");
Styles.WriteWarning("This is a warning message.");

Console.ReadLine();
AnsiConsole.Clear();

