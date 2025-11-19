using MjCuadrado.NetSdk.Models;

namespace MjCuadrado.NetSdk.Services;

/// <summary>
/// Servicio para gestión de templates
/// </summary>
public interface ITemplateService
{
    /// <summary>
    /// Genera la estructura completa del proyecto
    /// </summary>
    /// <param name="projectInfo">Información del proyecto</param>
    /// <returns>True si se generó exitosamente</returns>
    bool GenerateProjectStructure(ProjectInfo projectInfo);

    /// <summary>
    /// Genera el archivo config.json
    /// </summary>
    /// <param name="path">Ruta donde guardar el archivo</param>
    /// <param name="projectInfo">Información del proyecto</param>
    /// <returns>True si se generó exitosamente</returns>
    bool GenerateConfigFile(string path, ProjectInfo projectInfo);

    /// <summary>
    /// Genera los archivos README de todas las carpetas
    /// </summary>
    /// <param name="basePath">Ruta base del proyecto</param>
    /// <param name="projectInfo">Información del proyecto</param>
    /// <returns>True si se generaron exitosamente</returns>
    bool GenerateReadmeFiles(string basePath, ProjectInfo projectInfo);

    /// <summary>
    /// Obtiene el contenido de un template
    /// </summary>
    /// <param name="templateName">Nombre del template (ej: "config.json.template")</param>
    /// <returns>Contenido del template</returns>
    string GetTemplateContent(string templateName);

    /// <summary>
    /// Reemplaza variables en un template
    /// </summary>
    /// <param name="content">Contenido del template</param>
    /// <param name="variables">Diccionario de variables a reemplazar</param>
    /// <returns>Contenido con variables reemplazadas</returns>
    string ReplaceVariables(string content, Dictionary<string, string> variables);

    /// <summary>
    /// Crea el diccionario de variables a partir de ProjectInfo
    /// </summary>
    /// <param name="projectInfo">Información del proyecto</param>
    /// <returns>Diccionario de variables</returns>
    Dictionary<string, string> CreateVariablesDictionary(ProjectInfo projectInfo);
}
