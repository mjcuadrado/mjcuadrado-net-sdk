namespace MjCuadrado.NetSdk.Models;

/// <summary>
/// Información del proyecto durante la inicialización
/// </summary>
public class ProjectInfo
{
    /// <summary>
    /// Nombre del proyecto
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Ruta base del proyecto
    /// </summary>
    public string BasePath { get; set; } = string.Empty;

    /// <summary>
    /// Autor del proyecto
    /// </summary>
    public string Author { get; set; } = "@user";

    /// <summary>
    /// Framework target (.NET version)
    /// </summary>
    public string Framework { get; set; } = "net10.0";

    /// <summary>
    /// Versión del SDK utilizada
    /// </summary>
    public string SdkVersion { get; set; } = "0.1.0";

    /// <summary>
    /// Fecha de creación (ISO 8601)
    /// </summary>
    public string CreatedDate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd");

    /// <summary>
    /// Indica si debe sobrescribir archivos existentes
    /// </summary>
    public bool Force { get; set; } = false;
}
