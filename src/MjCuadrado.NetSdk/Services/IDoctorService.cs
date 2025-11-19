namespace MjCuadrado.NetSdk.Services;

/// <summary>
/// Servicio para diagnóstico del sistema
/// </summary>
public interface IDoctorService
{
    /// <summary>
    /// Verifica la versión de .NET SDK instalada
    /// </summary>
    /// <returns>Tupla con éxito y versión encontrada</returns>
    (bool Success, string Version) CheckDotNetVersion();

    /// <summary>
    /// Verifica que Git está instalado y configurado
    /// </summary>
    /// <returns>Tupla con éxito, versión y si está configurado</returns>
    (bool Success, string Version, bool Configured) CheckGitInstallation();

    /// <summary>
    /// Verifica la estructura del proyecto actual
    /// </summary>
    /// <returns>Tupla con éxito y lista de elementos faltantes</returns>
    (bool Success, List<string> MissingItems) CheckProjectStructure();

    /// <summary>
    /// Verifica el espacio disponible en disco
    /// </summary>
    /// <returns>Tupla con éxito y bytes disponibles</returns>
    (bool Success, long AvailableBytes) CheckDiskSpace();

    /// <summary>
    /// Verifica permisos de escritura en el directorio actual
    /// </summary>
    /// <returns>True si hay permisos de escritura</returns>
    bool CheckWritePermissions();

    /// <summary>
    /// Ejecuta todos los checks y retorna un reporte completo
    /// </summary>
    /// <returns>Resultado del diagnóstico</returns>
    DiagnosticResult RunFullDiagnostic();
}

/// <summary>
/// Resultado de un diagnóstico completo
/// </summary>
public class DiagnosticResult
{
    public bool AllChecksPassed => Checks.All(c => c.Success);
    public List<DiagnosticCheck> Checks { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
    public List<string> Suggestions { get; set; } = new();
}

/// <summary>
/// Check individual de diagnóstico
/// </summary>
public class DiagnosticCheck
{
    public string Name { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Details { get; set; }
}
