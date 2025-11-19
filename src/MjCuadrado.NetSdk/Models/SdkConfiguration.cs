using System.Text.Json.Serialization;

namespace MjCuadrado.NetSdk.Models;

/// <summary>
/// Configuración principal del SDK mjcuadrado-net-sdk
/// </summary>
public class SdkConfiguration
{
    [JsonPropertyName("project")]
    public ProjectConfig Project { get; set; } = new();

    [JsonPropertyName("sdk")]
    public SdkConfig Sdk { get; set; } = new();

    [JsonPropertyName("language")]
    public LanguageConfig Language { get; set; } = new();

    [JsonPropertyName("github")]
    public GitHubConfig GitHub { get; set; } = new();

    [JsonPropertyName("optimization")]
    public OptimizationConfig Optimization { get; set; } = new();
}

/// <summary>
/// Configuración del proyecto
/// </summary>
public class ProjectConfig
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    public string Version { get; set; } = "0.1.0";

    [JsonPropertyName("template_version")]
    public string TemplateVersion { get; set; } = "0.1.0";

    [JsonPropertyName("created")]
    public string Created { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd");

    [JsonPropertyName("updated")]
    public string Updated { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd");

    [JsonPropertyName("language")]
    public string Language { get; set; } = "csharp";

    [JsonPropertyName("framework")]
    public string Framework { get; set; } = "net10.0";

    [JsonPropertyName("mode")]
    public string Mode { get; set; } = "personal";

    [JsonPropertyName("author")]
    public string Author { get; set; } = "@user";
}

/// <summary>
/// Configuración del SDK
/// </summary>
public class SdkConfig
{
    [JsonPropertyName("version")]
    public string Version { get; set; } = "0.1.0";

    [JsonPropertyName("min_dotnet_version")]
    public string MinDotNetVersion { get; set; } = "9.0.0";
}

/// <summary>
/// Configuración de idioma
/// </summary>
public class LanguageConfig
{
    [JsonPropertyName("conversation_language")]
    public string ConversationLanguage { get; set; } = "es";

    [JsonPropertyName("conversation_language_name")]
    public string ConversationLanguageName { get; set; } = "Spanish";
}

/// <summary>
/// Configuración de GitHub
/// </summary>
public class GitHubConfig
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = false;

    [JsonPropertyName("repository")]
    public string? Repository { get; set; }

    [JsonPropertyName("auto_delete_branches")]
    public bool? AutoDeleteBranches { get; set; }
}

/// <summary>
/// Configuración de optimización
/// </summary>
public class OptimizationConfig
{
    [JsonPropertyName("last_sync")]
    public string? LastSync { get; set; }

    [JsonPropertyName("template_synced")]
    public bool TemplateSynced { get; set; } = false;
}
