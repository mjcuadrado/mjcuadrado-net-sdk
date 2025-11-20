using Microsoft.Extensions.DependencyInjection;
using MjCuadrado.NetSdk;
using MjCuadrado.NetSdk.Commands;
using MjCuadrado.NetSdk.Services;
using Spectre.Console;
using Spectre.Console.Cli;

// Configurar servicios con DI
var services = new ServiceCollection();
services.AddSingleton<IFileSystemService, FileSystemService>();
services.AddSingleton<IConfigurationService, ConfigurationService>();
services.AddSingleton<ITemplateService, TemplateService>();

var registrar = new TypeRegistrar(services);
var app = new CommandApp(registrar);

app.Configure(config =>
{
    config.SetApplicationName("mjcuadrado-net-sdk");
    config.SetApplicationVersion("0.1.0");

    config.ValidateExamples();

    // Comando init
    config.AddCommand<InitCommand>("init")
        .WithDescription("Inicializa un nuevo proyecto con la estructura mjcuadrado-net-sdk")
        .WithExample(new[] { "init", "mi-proyecto" })
        .WithExample(new[] { "init" })
        .WithExample(new[] { "init", "mi-proyecto", "--force" });

    // Comando doctor
    config.AddCommand<DoctorCommand>("doctor")
        .WithDescription("Verifica las dependencias del sistema y la salud del proyecto")
        .WithExample(new[] { "doctor" })
        .WithExample(new[] { "doctor", "--verbose" });

    // Comando version
    config.AddCommand<VersionCommand>("version")
        .WithDescription("Muestra la versión del SDK instalado")
        .WithExample(new[] { "version" })
        .WithExample(new[] { "version", "--verbose" });
});

try
{
    return await app.RunAsync(args);
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
    return 1;
}
