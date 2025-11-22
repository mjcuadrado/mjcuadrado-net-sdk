using System.ComponentModel;
using MjCuadrado.NetSdk.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace MjCuadrado.NetSdk.Commands;

/// <summary>
/// Comando para diagnosticar el sistema y proyecto
/// </summary>
public class DoctorCommand : Command<DoctorCommand.Settings>
{
    private readonly IDoctorService _doctorService;

    public DoctorCommand(IDoctorService doctorService)
    {
        _doctorService = doctorService ?? throw new ArgumentNullException(nameof(doctorService));
    }

    public class Settings : CommandSettings
    {
        [CommandOption("--verbose")]
        [Description("Muestra información detallada del diagnóstico")]
        public bool Verbose { get; set; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        try
        {
            // Header
            var panel = new Panel(
                Align.Center(
                    new Markup("[bold yellow]🏥 Diagnóstico del Sistema[/]"),
                    VerticalAlignment.Middle))
            {
                Border = BoxBorder.Rounded,
                Padding = new Padding(1, 0, 1, 0)
            };
            AnsiConsole.Write(panel);
            AnsiConsole.WriteLine();

            // Ejecutar diagnóstico con spinner
            var result = AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .Start("[yellow]Ejecutando verificaciones...[/]", ctx => _doctorService.RunFullDiagnostic());

            // Mostrar tabla de resultados
            DisplayResults(result, settings.Verbose);

            // Mostrar warnings
            if (result.Warnings.Count > 0)
            {
                AnsiConsole.WriteLine();
                var warningsPanel = new Panel(
                    string.Join("\n", result.Warnings.Select(w => $"⚠ {w}")))
                {
                    Header = new PanelHeader(" Warnings ", Justify.Left),
                    Border = BoxBorder.Rounded,
                    BorderStyle = new Style(Color.Yellow)
                };
                AnsiConsole.Write(warningsPanel);
            }

            // Mostrar sugerencias
            if (result.Suggestions.Count > 0)
            {
                AnsiConsole.WriteLine();
                var suggestionsPanel = new Panel(
                    string.Join("\n", result.Suggestions.Select((s, i) => $"{i + 1}. {s}")))
                {
                    Header = new PanelHeader(" 💡 Sugerencias ", Justify.Left),
                    Border = BoxBorder.Rounded,
                    BorderStyle = new Style(Color.Cyan1)
                };
                AnsiConsole.Write(suggestionsPanel);
            }

            // Resumen final
            AnsiConsole.WriteLine();
            if (result.AllChecksPassed)
            {
                var successPanel = new Panel(
                    Align.Center(
                        new Markup("[bold green]✓ ¡Todo listo! El sistema está correctamente configurado.[/]"),
                        VerticalAlignment.Middle))
                {
                    Border = BoxBorder.Rounded,
                    BorderStyle = new Style(Color.Green),
                    Padding = new Padding(1, 0, 1, 0)
                };
                AnsiConsole.Write(successPanel);
                return 0;
            }
            else
            {
                var failedCount = result.Checks.Count(c => !c.Success);
                var errorPanel = new Panel(
                    Align.Center(
                        new Markup($"[bold red]✗ Se encontraron {failedCount} problema(s). Revisa las sugerencias arriba.[/]"),
                        VerticalAlignment.Middle))
                {
                    Border = BoxBorder.Rounded,
                    BorderStyle = new Style(Color.Red),
                    Padding = new Padding(1, 0, 1, 0)
                };
                AnsiConsole.Write(errorPanel);
                return 1;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]✗ Error inesperado: {ex.Message}[/]");
            return 1;
        }
    }

    private void DisplayResults(DiagnosticResult result, bool verbose)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn("[bold]Check[/]"));
        table.AddColumn(new TableColumn("[bold]Status[/]"));
        table.AddColumn(new TableColumn("[bold]Details[/]"));

        foreach (var check in result.Checks)
        {
            var statusMarkup = check.Success
                ? "[green]✓ " + check.Message + "[/]"
                : "[red]✗ " + check.Message + "[/]";

            var details = verbose || !check.Success
                ? (check.Details ?? "N/A")
                : (check.Details ?? "OK");

            table.AddRow(
                check.Name,
                statusMarkup,
                details
            );
        }

        AnsiConsole.Write(table);
    }
}
