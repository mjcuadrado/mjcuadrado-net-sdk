using System.Reflection;
using System.Text;
using MjCuadrado.NetSdk.Models;

namespace MjCuadrado.NetSdk.Services;

/// <summary>
/// Implementación del servicio de templates
/// </summary>
public class TemplateService : ITemplateService
{
    private readonly IFileSystemService _fileSystemService;
    private readonly Assembly _assembly;

    // Estructura de carpetas completa del proyecto
    private static readonly string[] ProjectFolders = new[]
    {
        ".mjcuadrado-net-sdk",
        ".mjcuadrado-net-sdk/memory",
        ".mjcuadrado-net-sdk/reports",
        ".mjcuadrado-net-sdk/specs",
        ".claude",
        ".claude/agents",
        ".claude/commands",
        ".claude/skills",
        ".claude/hooks"
    };

    public TemplateService(IFileSystemService fileSystemService)
    {
        _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        _assembly = Assembly.GetExecutingAssembly();
    }

    /// <summary>
    /// Genera la estructura completa del proyecto
    /// </summary>
    public bool GenerateProjectStructure(ProjectInfo projectInfo)
    {
        if (projectInfo == null)
        {
            throw new ArgumentNullException(nameof(projectInfo));
        }

        if (string.IsNullOrWhiteSpace(projectInfo.BasePath))
        {
            throw new ArgumentException("La ruta base del proyecto es requerida", nameof(projectInfo));
        }

        try
        {
            // Crear todas las carpetas
            foreach (var folder in ProjectFolders)
            {
                var fullPath = Path.Combine(projectInfo.BasePath, folder);
                _fileSystemService.CreateDirectory(fullPath);
            }

            // Generar config.json
            var configPath = Path.Combine(projectInfo.BasePath, ".mjcuadrado-net-sdk", "config.json");
            GenerateConfigFile(configPath, projectInfo);

            // Generar archivos de documentación base
            GenerateReadmeFiles(projectInfo.BasePath, projectInfo);

            return true;
        }
        catch (Exception ex)
        {
            throw new IOException($"Error al generar la estructura del proyecto en: {projectInfo.BasePath}", ex);
        }
    }

    /// <summary>
    /// Genera el archivo config.json
    /// </summary>
    public bool GenerateConfigFile(string path, ProjectInfo projectInfo)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("La ruta no puede estar vacía", nameof(path));
        }

        if (projectInfo == null)
        {
            throw new ArgumentNullException(nameof(projectInfo));
        }

        try
        {
            var template = GetTemplateContent("config.json.template");
            var variables = CreateVariablesDictionary(projectInfo);
            var content = ReplaceVariables(template, variables);

            _fileSystemService.WriteTextFile(path, content);
            return true;
        }
        catch (Exception ex) when (ex is not ArgumentException && ex is not ArgumentNullException)
        {
            throw new IOException($"Error al generar el archivo de configuración: {path}", ex);
        }
    }

    /// <summary>
    /// Genera los archivos README de todas las carpetas
    /// </summary>
    public bool GenerateReadmeFiles(string basePath, ProjectInfo projectInfo)
    {
        if (string.IsNullOrWhiteSpace(basePath))
        {
            throw new ArgumentException("La ruta base no puede estar vacía", nameof(basePath));
        }

        if (projectInfo == null)
        {
            throw new ArgumentNullException(nameof(projectInfo));
        }

        try
        {
            var variables = CreateVariablesDictionary(projectInfo);

            // Documentación base en .mjcuadrado-net-sdk
            GenerateFile(basePath, ".mjcuadrado-net-sdk", "product.md", "product.md.template", variables);
            GenerateFile(basePath, ".mjcuadrado-net-sdk", "structure.md", "structure.md.template", variables);
            GenerateFile(basePath, ".mjcuadrado-net-sdk", "tech.md", "tech.md.template", variables);

            // READMEs de subcarpetas
            GenerateFile(basePath, ".mjcuadrado-net-sdk/specs", "README.md", "specs-README.md.template", variables);
            GenerateFile(basePath, ".mjcuadrado-net-sdk/memory", "README.md", "memory-README.md.template", variables);
            GenerateFile(basePath, ".mjcuadrado-net-sdk/reports", "README.md", "reports-README.md.template", variables);

            // READMEs de Claude
            GenerateFile(basePath, ".claude/agents", "README.md", "claude-agents-README.md.template", variables);
            GenerateFile(basePath, ".claude/commands", "README.md", "claude-commands-README.md.template", variables);
            GenerateFile(basePath, ".claude/skills", "README.md", "claude-skills-README.md.template", variables);
            GenerateFile(basePath, ".claude/hooks", "README.md", "claude-hooks-README.md.template", variables);

            return true;
        }
        catch (Exception ex) when (ex is not ArgumentException && ex is not ArgumentNullException)
        {
            throw new IOException($"Error al generar archivos README en: {basePath}", ex);
        }
    }

    /// <summary>
    /// Obtiene el contenido de un template embebido
    /// </summary>
    public string GetTemplateContent(string templateName)
    {
        if (string.IsNullOrWhiteSpace(templateName))
        {
            throw new ArgumentException("El nombre del template no puede estar vacío", nameof(templateName));
        }

        try
        {
            // Los recursos embebidos usan puntos en lugar de separadores de carpetas
            var resourceName = $"MjCuadrado.NetSdk.Templates.{templateName}";

            using var stream = _assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new FileNotFoundException($"Template no encontrado: {templateName}. Recurso: {resourceName}");
            }

            using var reader = new StreamReader(stream, Encoding.UTF8);
            return reader.ReadToEnd();
        }
        catch (Exception ex) when (ex is not ArgumentException && ex is not FileNotFoundException)
        {
            throw new IOException($"Error al leer el template: {templateName}", ex);
        }
    }

    /// <summary>
    /// Reemplaza variables en un template
    /// </summary>
    public string ReplaceVariables(string content, Dictionary<string, string> variables)
    {
        if (content == null)
        {
            throw new ArgumentNullException(nameof(content));
        }

        if (variables == null)
        {
            throw new ArgumentNullException(nameof(variables));
        }

        var result = content;
        foreach (var variable in variables)
        {
            var placeholder = $"{{{{{variable.Key}}}}}";
            result = result.Replace(placeholder, variable.Value);
        }

        return result;
    }

    /// <summary>
    /// Crea el diccionario de variables a partir de ProjectInfo
    /// </summary>
    public Dictionary<string, string> CreateVariablesDictionary(ProjectInfo projectInfo)
    {
        if (projectInfo == null)
        {
            throw new ArgumentNullException(nameof(projectInfo));
        }

        return new Dictionary<string, string>
        {
            { "PROJECT_NAME", projectInfo.Name ?? "my-project" },
            { "VERSION", "0.1.0" },
            { "DATE", projectInfo.CreatedDate ?? DateTime.UtcNow.ToString("yyyy-MM-dd") },
            { "AUTHOR", projectInfo.Author ?? "@user" },
            { "FRAMEWORK", projectInfo.Framework ?? "net10.0" },
            { "SDK_VERSION", projectInfo.SdkVersion ?? "0.1.0" }
        };
    }

    #region Helper Methods

    /// <summary>
    /// Genera un archivo a partir de un template
    /// </summary>
    private void GenerateFile(string basePath, string subPath, string fileName, string templateName, Dictionary<string, string> variables)
    {
        var template = GetTemplateContent(templateName);
        var content = ReplaceVariables(template, variables);
        var fullPath = Path.Combine(basePath, subPath, fileName);
        _fileSystemService.WriteTextFile(fullPath, content);
    }

    #endregion
}
