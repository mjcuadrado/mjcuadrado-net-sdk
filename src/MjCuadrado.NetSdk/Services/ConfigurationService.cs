using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using MjCuadrado.NetSdk.Models;

namespace MjCuadrado.NetSdk.Services;

/// <summary>
/// Implementación del servicio de configuración
/// </summary>
public class ConfigurationService : IConfigurationService
{
    private static readonly string[] SupportedLanguages = { "es", "en", "pt", "fr" };
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };

    /// <summary>
    /// Carga la configuración desde un archivo
    /// </summary>
    public SdkConfiguration LoadConfiguration(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("La ruta no puede estar vacía", nameof(path));
        }

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"El archivo de configuración no existe: {path}");
        }

        try
        {
            var json = File.ReadAllText(path);
            var config = JsonSerializer.Deserialize<SdkConfiguration>(json, JsonOptions);

            if (config == null)
            {
                throw new InvalidOperationException($"El archivo de configuración está vacío o es inválido: {path}");
            }

            return config;
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Error al parsear el archivo JSON: {path}", ex);
        }
        catch (Exception ex) when (ex is not ArgumentException && ex is not FileNotFoundException)
        {
            throw new IOException($"Error al leer el archivo de configuración: {path}", ex);
        }
    }

    /// <summary>
    /// Guarda la configuración en un archivo
    /// </summary>
    public bool SaveConfiguration(string path, SdkConfiguration config)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("La ruta no puede estar vacía", nameof(path));
        }

        if (config == null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        try
        {
            // Validar antes de guardar
            var validation = ValidateConfiguration(config);
            if (!validation.IsValid)
            {
                var errors = string.Join(", ", validation.Errors.Select(e => e.ToString()));
                throw new InvalidOperationException($"La configuración no es válida: {errors}");
            }

            // Asegurar que el directorio existe
            var directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Actualizar fecha de modificación
            config.Project.Updated = DateTime.UtcNow.ToString("yyyy-MM-dd");

            var json = JsonSerializer.Serialize(config, JsonOptions);
            File.WriteAllText(path, json);

            return true;
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException($"No hay permisos para escribir el archivo: {path}", ex);
        }
        catch (IOException ex)
        {
            throw new IOException($"Error al escribir el archivo de configuración: {path}", ex);
        }
    }

    /// <summary>
    /// Valida una configuración
    /// </summary>
    public ValidationResult ValidateConfiguration(SdkConfiguration config)
    {
        var result = new ValidationResult();

        if (config == null)
        {
            result.AddError("config", "La configuración no puede ser null");
            return result;
        }

        // Validar Project
        if (config.Project == null)
        {
            result.AddError("project", "La sección 'project' es requerida");
        }
        else
        {
            if (string.IsNullOrWhiteSpace(config.Project.Name))
            {
                result.AddError("project.name", "El nombre del proyecto es requerido");
            }
            else if (!IsValidProjectName(config.Project.Name))
            {
                result.AddError("project.name", $"El nombre del proyecto contiene caracteres inválidos: {config.Project.Name}");
            }

            if (!IsValidSemver(config.Project.Version))
            {
                result.AddError("project.version", $"La versión del proyecto no es válida (semver): {config.Project.Version}");
            }

            if (!IsValidSemver(config.Project.TemplateVersion))
            {
                result.AddError("project.template_version", $"La versión del template no es válida (semver): {config.Project.TemplateVersion}");
            }

            if (!IsValidIsoDate(config.Project.Created))
            {
                result.AddError("project.created", $"La fecha de creación no es válida (ISO 8601): {config.Project.Created}");
            }

            if (!IsValidIsoDate(config.Project.Updated))
            {
                result.AddError("project.updated", $"La fecha de actualización no es válida (ISO 8601): {config.Project.Updated}");
            }
        }

        // Validar SDK
        if (config.Sdk == null)
        {
            result.AddError("sdk", "La sección 'sdk' es requerida");
        }
        else
        {
            if (!IsValidSemver(config.Sdk.Version))
            {
                result.AddError("sdk.version", $"La versión del SDK no es válida (semver): {config.Sdk.Version}");
            }

            if (!IsValidSemver(config.Sdk.MinDotNetVersion))
            {
                result.AddError("sdk.min_dotnet_version", $"La versión mínima de .NET no es válida (semver): {config.Sdk.MinDotNetVersion}");
            }
        }

        // Validar Language
        if (config.Language == null)
        {
            result.AddError("language", "La sección 'language' es requerida");
        }
        else
        {
            if (!IsSupportedLanguage(config.Language.ConversationLanguage))
            {
                result.AddError("language.conversation_language", $"Idioma no soportado: {config.Language.ConversationLanguage}. Soportados: {string.Join(", ", SupportedLanguages)}");
            }
        }

        // Validar GitHub (opcional pero si está, debe ser válido)
        if (config.GitHub?.Enabled == true && string.IsNullOrWhiteSpace(config.GitHub.Repository))
        {
            result.AddError("github.repository", "Si GitHub está habilitado, el repositorio es requerido");
        }

        // Validar Optimization - LastSync si existe debe ser ISO 8601
        if (config.Optimization?.LastSync != null && !IsValidIsoDate(config.Optimization.LastSync))
        {
            result.AddError("optimization.last_sync", $"La fecha de última sincronización no es válida (ISO 8601): {config.Optimization.LastSync}");
        }

        return result;
    }

    /// <summary>
    /// Crea una configuración por defecto
    /// </summary>
    public SdkConfiguration CreateDefaultConfiguration(ProjectInfo projectInfo)
    {
        if (projectInfo == null)
        {
            throw new ArgumentNullException(nameof(projectInfo));
        }

        if (string.IsNullOrWhiteSpace(projectInfo.Name))
        {
            throw new ArgumentException("El nombre del proyecto es requerido", nameof(projectInfo));
        }

        var now = DateTime.UtcNow.ToString("yyyy-MM-dd");

        return new SdkConfiguration
        {
            Project = new ProjectConfig
            {
                Name = projectInfo.Name,
                Version = "0.1.0",
                TemplateVersion = "0.1.0",
                Created = now,
                Updated = now,
                Language = "csharp",
                Framework = projectInfo.Framework ?? "net10.0",
                Mode = "personal",
                Author = projectInfo.Author ?? "@user"
            },
            Sdk = new SdkConfig
            {
                Version = "0.1.0",
                MinDotNetVersion = "9.0.0"
            },
            Language = new LanguageConfig
            {
                ConversationLanguage = "es",
                ConversationLanguageName = "Spanish"
            },
            GitHub = new GitHubConfig
            {
                Enabled = false,
                Repository = null,
                AutoDeleteBranches = null
            },
            Optimization = new OptimizationConfig
            {
                LastSync = null,
                TemplateSynced = false
            }
        };
    }

    /// <summary>
    /// Combina dos configuraciones (base + overrides)
    /// </summary>
    public SdkConfiguration MergeConfigurations(SdkConfiguration baseConfig, SdkConfiguration overrides)
    {
        if (baseConfig == null)
        {
            throw new ArgumentNullException(nameof(baseConfig));
        }

        if (overrides == null)
        {
            return baseConfig;
        }

        // Crear una copia profunda usando serialización
        var json = JsonSerializer.Serialize(baseConfig, JsonOptions);
        var merged = JsonSerializer.Deserialize<SdkConfiguration>(json, JsonOptions)!;

        // Merge Project
        if (overrides.Project != null)
        {
            if (!string.IsNullOrWhiteSpace(overrides.Project.Name))
                merged.Project.Name = overrides.Project.Name;
            if (!string.IsNullOrWhiteSpace(overrides.Project.Version))
                merged.Project.Version = overrides.Project.Version;
            if (!string.IsNullOrWhiteSpace(overrides.Project.TemplateVersion))
                merged.Project.TemplateVersion = overrides.Project.TemplateVersion;
            if (!string.IsNullOrWhiteSpace(overrides.Project.Created))
                merged.Project.Created = overrides.Project.Created;
            if (!string.IsNullOrWhiteSpace(overrides.Project.Updated))
                merged.Project.Updated = overrides.Project.Updated;
            if (!string.IsNullOrWhiteSpace(overrides.Project.Language))
                merged.Project.Language = overrides.Project.Language;
            if (!string.IsNullOrWhiteSpace(overrides.Project.Framework))
                merged.Project.Framework = overrides.Project.Framework;
            if (!string.IsNullOrWhiteSpace(overrides.Project.Mode))
                merged.Project.Mode = overrides.Project.Mode;
            if (!string.IsNullOrWhiteSpace(overrides.Project.Author))
                merged.Project.Author = overrides.Project.Author;
        }

        // Merge SDK
        if (overrides.Sdk != null)
        {
            if (!string.IsNullOrWhiteSpace(overrides.Sdk.Version))
                merged.Sdk.Version = overrides.Sdk.Version;
            if (!string.IsNullOrWhiteSpace(overrides.Sdk.MinDotNetVersion))
                merged.Sdk.MinDotNetVersion = overrides.Sdk.MinDotNetVersion;
        }

        // Merge Language
        if (overrides.Language != null)
        {
            if (!string.IsNullOrWhiteSpace(overrides.Language.ConversationLanguage))
                merged.Language.ConversationLanguage = overrides.Language.ConversationLanguage;
            if (!string.IsNullOrWhiteSpace(overrides.Language.ConversationLanguageName))
                merged.Language.ConversationLanguageName = overrides.Language.ConversationLanguageName;
        }

        // Merge GitHub
        if (overrides.GitHub != null)
        {
            merged.GitHub.Enabled = overrides.GitHub.Enabled;
            if (overrides.GitHub.Repository != null)
                merged.GitHub.Repository = overrides.GitHub.Repository;
            if (overrides.GitHub.AutoDeleteBranches.HasValue)
                merged.GitHub.AutoDeleteBranches = overrides.GitHub.AutoDeleteBranches;
        }

        // Merge Optimization
        if (overrides.Optimization != null)
        {
            merged.Optimization.TemplateSynced = overrides.Optimization.TemplateSynced;
            if (overrides.Optimization.LastSync != null)
                merged.Optimization.LastSync = overrides.Optimization.LastSync;
        }

        return merged;
    }

    /// <summary>
    /// Busca el archivo de configuración en el directorio actual o padres
    /// </summary>
    public string? FindConfigurationFile(string startPath)
    {
        if (string.IsNullOrWhiteSpace(startPath))
        {
            startPath = Directory.GetCurrentDirectory();
        }

        if (!Directory.Exists(startPath))
        {
            return null;
        }

        var currentDir = new DirectoryInfo(startPath);

        while (currentDir != null)
        {
            var configPath = Path.Combine(currentDir.FullName, ".mjcuadrado-net-sdk", "config.json");
            if (File.Exists(configPath))
            {
                return configPath;
            }

            currentDir = currentDir.Parent;
        }

        return null;
    }

    #region Validación helpers

    /// <summary>
    /// Valida que una versión sea semver válida
    /// </summary>
    private static bool IsValidSemver(string? version)
    {
        if (string.IsNullOrWhiteSpace(version))
        {
            return false;
        }

        // Regex simplificado para semver: major.minor.patch con optional -prerelease+metadata
        var semverPattern = @"^\d+\.\d+\.\d+(-[0-9A-Za-z-]+(\.[0-9A-Za-z-]+)*)?(\+[0-9A-Za-z-]+(\.[0-9A-Za-z-]+)*)?$";
        return Regex.IsMatch(version, semverPattern);
    }

    /// <summary>
    /// Valida que una fecha sea ISO 8601 válida (yyyy-MM-dd)
    /// </summary>
    private static bool IsValidIsoDate(string? date)
    {
        if (string.IsNullOrWhiteSpace(date))
        {
            return false;
        }

        return DateTime.TryParseExact(date, "yyyy-MM-dd", null,
            System.Globalization.DateTimeStyles.None, out _);
    }

    /// <summary>
    /// Valida que el nombre del proyecto sea válido (sin caracteres especiales peligrosos)
    /// </summary>
    private static bool IsValidProjectName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        // Permitir letras, números, guiones, underscores y puntos
        var validNamePattern = @"^[a-zA-Z0-9._-]+$";
        return Regex.IsMatch(name, validNamePattern);
    }

    /// <summary>
    /// Valida que el idioma esté soportado
    /// </summary>
    private static bool IsSupportedLanguage(string? language)
    {
        if (string.IsNullOrWhiteSpace(language))
        {
            return false;
        }

        return SupportedLanguages.Contains(language.ToLowerInvariant());
    }

    #endregion
}
