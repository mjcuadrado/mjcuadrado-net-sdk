using MjCuadrado.NetSdk.Models;

namespace MjCuadrado.NetSdk.Services;

/// <summary>
/// Servicio para gestión de configuración
/// </summary>
public interface IConfigurationService
{
    /// <summary>
    /// Carga la configuración desde un archivo
    /// </summary>
    /// <param name="path">Ruta del archivo config.json</param>
    /// <returns>Configuración cargada</returns>
    SdkConfiguration LoadConfiguration(string path);

    /// <summary>
    /// Guarda la configuración en un archivo
    /// </summary>
    /// <param name="path">Ruta del archivo config.json</param>
    /// <param name="config">Configuración a guardar</param>
    /// <returns>True si se guardó exitosamente</returns>
    bool SaveConfiguration(string path, SdkConfiguration config);

    /// <summary>
    /// Valida una configuración
    /// </summary>
    /// <param name="config">Configuración a validar</param>
    /// <returns>Resultado de la validación</returns>
    ValidationResult ValidateConfiguration(SdkConfiguration config);

    /// <summary>
    /// Crea una configuración por defecto
    /// </summary>
    /// <param name="projectInfo">Información del proyecto</param>
    /// <returns>Configuración con valores por defecto</returns>
    SdkConfiguration CreateDefaultConfiguration(ProjectInfo projectInfo);

    /// <summary>
    /// Combina dos configuraciones (base + overrides)
    /// </summary>
    /// <param name="baseConfig">Configuración base</param>
    /// <param name="overrides">Configuración con valores a sobrescribir</param>
    /// <returns>Configuración combinada</returns>
    SdkConfiguration MergeConfigurations(SdkConfiguration baseConfig, SdkConfiguration overrides);

    /// <summary>
    /// Busca el archivo de configuración en el directorio actual o padres
    /// </summary>
    /// <param name="startPath">Directorio donde iniciar la búsqueda</param>
    /// <returns>Ruta del config.json encontrado, o null si no existe</returns>
    string? FindConfigurationFile(string startPath);
}
