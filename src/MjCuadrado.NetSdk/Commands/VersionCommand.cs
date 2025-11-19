using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using Spectre.Console;
using Spectre.Console.Cli;

namespace MjCuadrado.NetSdk.Commands;

/// <summary>
/// Comando para mostrar la versión del SDK
/// </summary>
public class VersionCommand : Command<VersionCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("--verbose")]
        [Description("Muestra información detallada de la versión")]
        public bool Verbose { get; set; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        var version = Assembly.GetExecutingAssembly()
            .GetName()
            .Version?.ToString(3) ?? "0.1.0";

        var dotnetVersion = Environment.Version.ToString();

        AnsiConsole.MarkupLine($"[bold cyan]mjcuadrado-net-sdk[/] v{version}");
        AnsiConsole.MarkupLine($".NET {dotnetVersion}");

        if (settings.Verbose)
        {
            AnsiConsole.WriteLine();
            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("Property")
                .AddColumn("Value");

            table.AddRow("SDK Version", version);
            table.AddRow(".NET Runtime", dotnetVersion);
            table.AddRow("OS", Environment.OSVersion.ToString());
            table.AddRow("Architecture", RuntimeInformation.ProcessArchitecture.ToString());
            table.AddRow("Framework", RuntimeInformation.FrameworkDescription);

            AnsiConsole.Write(table);
        }

        return 0;
    }
}
