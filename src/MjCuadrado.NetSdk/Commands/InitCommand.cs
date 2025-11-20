using System.ComponentModel;
using System.Text.RegularExpressions;
using MjCuadrado.NetSdk.Models;
using MjCuadrado.NetSdk.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace MjCuadrado.NetSdk.Commands;

/// <summary>
/// Comando para inicializar un nuevo proyecto
/// </summary>
public class InitCommand : Command<InitCommand.Settings>
{
    private readonly IFileSystemService _fileSystemService;
    private readonly IConfigurationService _configurationService;
    private readonly ITemplateService _templateService;

    public InitCommand(
        IFileSystemService fileSystemService,
        IConfigurationService configurationService,
        ITemplateService templateService)
    {
        _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        _templateService = templateService ?? throw new ArgumentNullException(nameof(templateService));
    }

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[nombre-proyecto]")]
        [Description("Nombre del proyecto a crear. Si se omite, inicializa en el directorio actual.")]
        public string? ProjectName { get; set; }

        [CommandOption("--force")]
        [Description("Sobrescribe archivos existentes si el proyecto ya existe")]
        public bool Force { get; set; }

        [CommandOption("--author")]
        [Description("Autor del proyecto (default: @user)")]
        public string Author { get; set; } = "@user";

        [CommandOption("--framework")]
        [Description("Framework target (default: net10.0)")]
        public string Framework { get; set; } = "net10.0";
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        try
        {
            // 1. Determinar ruta y nombre del proyecto
            var (projectPath, projectName) = DetermineProjectPath(settings);

            // 2. Validar nombre del proyecto
            if (!IsValidProjectName(projectName))
            {
                AnsiConsole.MarkupLine("[red]✗ Error: Nombre de proyecto inválido[/]");
                AnsiConsole.MarkupLine($"[yellow]El nombre '{projectName}' contiene caracteres no permitidos.[/]");
                AnsiConsole.MarkupLine("[dim]Caracteres permitidos: letras, números, guiones, puntos y underscores[/]");
                return 1;
            }

            // 3. Validar que el directorio no exista (o usar --force)
            if (!settings.Force && _fileSystemService.DirectoryExists(projectPath))
            {
                var configPath = Path.Combine(projectPath, ".mjcuadrado-net-sdk", "config.json");
                if (_fileSystemService.FileExists(configPath))
                {
                    AnsiConsole.MarkupLine("[red]✗ Error: El proyecto ya existe en esta ubicación[/]");
                    AnsiConsole.MarkupLine($"[yellow]Ubicación: {projectPath}[/]");
                    AnsiConsole.MarkupLine("[dim]Usa --force para sobrescribir[/]");
                    return 1;
                }
            }

            // 4. Validar permisos de escritura
            if (!_fileSystemService.HasWritePermissions(projectPath))
            {
                AnsiConsole.MarkupLine("[red]✗ Error: Sin permisos de escritura[/]");
                AnsiConsole.MarkupLine($"[yellow]No tienes permisos para escribir en: {projectPath}[/]");
                return 1;
            }

            // 5. Validar espacio en disco (mínimo 10MB)
            var availableSpace = _fileSystemService.GetAvailableDiskSpace(projectPath);
            if (availableSpace < 10 * 1024 * 1024) // 10 MB
            {
                AnsiConsole.MarkupLine("[red]✗ Error: Espacio en disco insuficiente[/]");
                AnsiConsole.MarkupLine($"[yellow]Disponible: {availableSpace / (1024 * 1024)} MB, Requerido: 10 MB[/]");
                return 1;
            }

            // 6. Crear proyecto con spinner
            var success = AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .Start("[yellow]Inicializando proyecto...[/]", ctx =>
                {
                    return CreateProject(projectPath, projectName, settings, ctx);
                });

            if (!success)
            {
                AnsiConsole.MarkupLine("[red]✗ Error al crear el proyecto[/]");
                return 1;
            }

            // 7. Mostrar resumen de éxito
            DisplaySuccessSummary(projectPath, projectName);

            return 0;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]✗ Error inesperado: {ex.Message}[/]");
            return 1;
        }
    }

    private (string projectPath, string projectName) DetermineProjectPath(Settings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.ProjectName))
        {
            // Inicializar en directorio actual
            var currentDir = _fileSystemService.GetCurrentDirectory();
            var currentDirName = Path.GetFileName(currentDir);
            return (currentDir, currentDirName);
        }
        else
        {
            // Crear nueva carpeta
            var currentDir = _fileSystemService.GetCurrentDirectory();
            var projectPath = Path.Combine(currentDir, settings.ProjectName);
            return (projectPath, settings.ProjectName);
        }
    }

    private bool IsValidProjectName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        // No permitir caracteres especiales peligrosos: /\:*?"<>|
        var invalidChars = new[] { '/', '\\', ':', '*', '?', '"', '<', '>', '|' };
        return !name.Any(c => invalidChars.Contains(c));
    }

    private bool CreateProject(string projectPath, string projectName, Settings settings, StatusContext ctx)
    {
        try
        {
            // Crear ProjectInfo
            var projectInfo = new ProjectInfo
            {
                Name = projectName,
                BasePath = projectPath,
                Author = settings.Author,
                Framework = settings.Framework,
                SdkVersion = "0.1.0",
                CreatedDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Force = settings.Force
            };

            // Paso 1: Crear estructura de carpetas
            ctx.Status("[yellow]Creando estructura de carpetas...[/]");
            var success = _templateService.GenerateProjectStructure(projectInfo);

            if (!success)
            {
                return false;
            }

            // Paso 2: Generar config.json ya se hace en GenerateProjectStructure
            ctx.Status("[yellow]Configuración completada...[/]");

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void DisplaySuccessSummary(string projectPath, string projectName)
    {
        AnsiConsole.WriteLine();

        // Panel de éxito
        var successPanel = new Panel(
            new Markup($"[green]✓ Proyecto '[bold]{projectName}[/]' inicializado exitosamente![/]"))
        {
            Border = BoxBorder.Rounded,
            Padding = new Padding(1, 0, 1, 0)
        };
        AnsiConsole.Write(successPanel);

        AnsiConsole.WriteLine();

        // Tabla con estructura creada
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[bold]Estructura creada[/]");
        table.AddColumn("[bold]Descripción[/]");

        table.AddRow(
            "[cyan].mjcuadrado-net-sdk/[/]",
            "Configuración y documentación del SDK"
        );
        table.AddRow(
            "[cyan]├─ config.json[/]",
            "Configuración del proyecto"
        );
        table.AddRow(
            "[cyan]├─ memory/[/]",
            "Memoria conversacional de IA"
        );
        table.AddRow(
            "[cyan]├─ reports/[/]",
            "Reportes de proyecto"
        );
        table.AddRow(
            "[cyan]└─ specs/[/]",
            "Especificaciones EARS"
        );
        table.AddRow(
            "[cyan].claude/[/]",
            "Configuración de Claude Code"
        );
        table.AddRow(
            "[cyan]├─ agents/[/]",
            "Agentes especializados"
        );
        table.AddRow(
            "[cyan]├─ commands/[/]",
            "Comandos personalizados"
        );
        table.AddRow(
            "[cyan]├─ skills/[/]",
            "Skills reutilizables"
        );
        table.AddRow(
            "[cyan]└─ hooks/[/]",
            "Hooks de automatización"
        );

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

        // Panel con próximos pasos
        var nextStepsPanel = new Panel(
            Align.Left(new Markup(
                $"[bold yellow]📋 Próximos pasos:[/]\n\n" +
                $"1. [cyan]cd {projectName}[/]\n" +
                $"2. Edita [cyan].mjcuadrado-net-sdk/config.json[/] según tus necesidades\n" +
                $"3. Lee [cyan].mjcuadrado-net-sdk/product.md[/] para definir tu producto\n" +
                $"4. Explora [cyan].claude/[/] para configurar agentes y comandos\n" +
                $"5. Ejecuta [green]mjcuadrado-net-sdk doctor[/] para verificar la instalación"
            ), VerticalAlignment.Top))
        {
            Header = new PanelHeader(" 🚀 Siguiente "),
            Border = BoxBorder.Rounded,
            Padding = new Padding(2, 1, 2, 1)
        };
        AnsiConsole.Write(nextStepsPanel);
        AnsiConsole.WriteLine();
    }
}
