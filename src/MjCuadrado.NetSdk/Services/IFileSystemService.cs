namespace MjCuadrado.NetSdk.Services;

/// <summary>
/// Servicio para operaciones de sistema de archivos
/// </summary>
public interface IFileSystemService
{
    /// <summary>
    /// Crea un directorio
    /// </summary>
    /// <param name="path">Ruta del directorio</param>
    /// <returns>True si se creó exitosamente</returns>
    bool CreateDirectory(string path);

    /// <summary>
    /// Crea una estructura completa de directorios
    /// </summary>
    /// <param name="basePath">Ruta base</param>
    /// <param name="folders">Lista de carpetas a crear</param>
    /// <returns>True si todas se crearon exitosamente</returns>
    bool CreateDirectoryStructure(string basePath, string[] folders);

    /// <summary>
    /// Verifica si un archivo existe
    /// </summary>
    /// <param name="path">Ruta del archivo</param>
    /// <returns>True si existe</returns>
    bool FileExists(string path);

    /// <summary>
    /// Verifica si un directorio existe
    /// </summary>
    /// <param name="path">Ruta del directorio</param>
    /// <returns>True si existe</returns>
    bool DirectoryExists(string path);

    /// <summary>
    /// Copia un archivo
    /// </summary>
    /// <param name="source">Archivo origen</param>
    /// <param name="destination">Archivo destino</param>
    /// <returns>True si se copió exitosamente</returns>
    bool CopyFile(string source, string destination);

    /// <summary>
    /// Escribe un archivo de texto
    /// </summary>
    /// <param name="path">Ruta del archivo</param>
    /// <param name="content">Contenido a escribir</param>
    /// <returns>True si se escribió exitosamente</returns>
    bool WriteTextFile(string path, string content);

    /// <summary>
    /// Lee un archivo de texto
    /// </summary>
    /// <param name="path">Ruta del archivo</param>
    /// <returns>Contenido del archivo</returns>
    string ReadTextFile(string path);

    /// <summary>
    /// Obtiene el directorio actual
    /// </summary>
    /// <returns>Ruta del directorio actual</returns>
    string GetCurrentDirectory();

    /// <summary>
    /// Asegura que un directorio existe, creándolo si es necesario
    /// </summary>
    /// <param name="path">Ruta del directorio</param>
    /// <returns>True si existe o se creó exitosamente</returns>
    bool EnsureDirectoryExists(string path);

    /// <summary>
    /// Verifica que hay permisos de escritura en un directorio
    /// </summary>
    /// <param name="path">Ruta del directorio</param>
    /// <returns>True si hay permisos de escritura</returns>
    bool HasWritePermissions(string path);

    /// <summary>
    /// Obtiene el espacio disponible en disco (en bytes)
    /// </summary>
    /// <param name="path">Ruta para verificar</param>
    /// <returns>Bytes disponibles</returns>
    long GetAvailableDiskSpace(string path);
}
