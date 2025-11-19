namespace MjCuadrado.NetSdk.Services;

/// <summary>
/// Implementación del servicio de sistema de archivos
/// </summary>
public class FileSystemService : IFileSystemService
{
    /// <summary>
    /// Crea un directorio
    /// </summary>
    public bool CreateDirectory(string path)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("La ruta no puede estar vacía", nameof(path));
            }

            var normalizedPath = NormalizePath(path);

            if (Directory.Exists(normalizedPath))
            {
                return true; // Ya existe
            }

            Directory.CreateDirectory(normalizedPath);
            return true;
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException($"No hay permisos para crear el directorio: {path}", ex);
        }
        catch (PathTooLongException ex)
        {
            throw new PathTooLongException($"La ruta es demasiado larga: {path}", ex);
        }
        catch (IOException ex)
        {
            throw new IOException($"Error al crear el directorio: {path}", ex);
        }
    }

    /// <summary>
    /// Crea una estructura completa de directorios
    /// </summary>
    public bool CreateDirectoryStructure(string basePath, string[] folders)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                throw new ArgumentException("La ruta base no puede estar vacía", nameof(basePath));
            }

            if (folders == null || folders.Length == 0)
            {
                throw new ArgumentException("Debe proporcionar al menos una carpeta", nameof(folders));
            }

            var normalizedBasePath = NormalizePath(basePath);

            foreach (var folder in folders)
            {
                if (string.IsNullOrWhiteSpace(folder))
                {
                    continue;
                }

                var fullPath = Path.Combine(normalizedBasePath, folder);
                CreateDirectory(fullPath);
            }

            return true;
        }
        catch (Exception ex) when (ex is not ArgumentException)
        {
            throw new IOException($"Error al crear la estructura de directorios en: {basePath}", ex);
        }
    }

    /// <summary>
    /// Verifica si un archivo existe
    /// </summary>
    public bool FileExists(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return false;
        }

        try
        {
            var normalizedPath = NormalizePath(path);
            return File.Exists(normalizedPath);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Verifica si un directorio existe
    /// </summary>
    public bool DirectoryExists(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return false;
        }

        try
        {
            var normalizedPath = NormalizePath(path);
            return Directory.Exists(normalizedPath);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Copia un archivo
    /// </summary>
    public bool CopyFile(string source, string destination)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentException("La ruta origen no puede estar vacía", nameof(source));
            }

            if (string.IsNullOrWhiteSpace(destination))
            {
                throw new ArgumentException("La ruta destino no puede estar vacía", nameof(destination));
            }

            var normalizedSource = NormalizePath(source);
            var normalizedDestination = NormalizePath(destination);

            if (!File.Exists(normalizedSource))
            {
                throw new FileNotFoundException($"El archivo origen no existe: {source}");
            }

            // Asegurar que el directorio destino existe
            var destinationDir = Path.GetDirectoryName(normalizedDestination);
            if (!string.IsNullOrEmpty(destinationDir))
            {
                EnsureDirectoryExists(destinationDir);
            }

            File.Copy(normalizedSource, normalizedDestination, overwrite: true);
            return true;
        }
        catch (FileNotFoundException)
        {
            throw; // Re-lanzar FileNotFoundException sin envolver
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException($"No hay permisos para copiar el archivo: {source} -> {destination}", ex);
        }
        catch (IOException ex)
        {
            throw new IOException($"Error al copiar el archivo: {source} -> {destination}", ex);
        }
    }

    /// <summary>
    /// Escribe un archivo de texto
    /// </summary>
    public bool WriteTextFile(string path, string content)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("La ruta no puede estar vacía", nameof(path));
            }

            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var normalizedPath = NormalizePath(path);

            // Asegurar que el directorio existe
            var directory = Path.GetDirectoryName(normalizedPath);
            if (!string.IsNullOrEmpty(directory))
            {
                EnsureDirectoryExists(directory);
            }

            File.WriteAllText(normalizedPath, content);
            return true;
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException($"No hay permisos para escribir el archivo: {path}", ex);
        }
        catch (IOException ex)
        {
            throw new IOException($"Error al escribir el archivo: {path}", ex);
        }
    }

    /// <summary>
    /// Lee un archivo de texto
    /// </summary>
    public string ReadTextFile(string path)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("La ruta no puede estar vacía", nameof(path));
            }

            var normalizedPath = NormalizePath(path);

            if (!File.Exists(normalizedPath))
            {
                throw new FileNotFoundException($"El archivo no existe: {path}");
            }

            return File.ReadAllText(normalizedPath);
        }
        catch (FileNotFoundException)
        {
            throw; // Re-lanzar FileNotFoundException sin envolver
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException($"No hay permisos para leer el archivo: {path}", ex);
        }
        catch (IOException ex)
        {
            throw new IOException($"Error al leer el archivo: {path}", ex);
        }
    }

    /// <summary>
    /// Obtiene el directorio actual
    /// </summary>
    public string GetCurrentDirectory()
    {
        return Directory.GetCurrentDirectory();
    }

    /// <summary>
    /// Asegura que un directorio existe, creándolo si es necesario
    /// </summary>
    public bool EnsureDirectoryExists(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return false;
        }

        try
        {
            var normalizedPath = NormalizePath(path);

            if (Directory.Exists(normalizedPath))
            {
                return true;
            }

            Directory.CreateDirectory(normalizedPath);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Verifica que hay permisos de escritura en un directorio
    /// </summary>
    public bool HasWritePermissions(string path)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            var normalizedPath = NormalizePath(path);

            // Si el directorio no existe, verificar el directorio padre
            if (!Directory.Exists(normalizedPath))
            {
                var parentDir = Path.GetDirectoryName(normalizedPath);
                if (string.IsNullOrEmpty(parentDir))
                {
                    return false;
                }
                normalizedPath = parentDir;
            }

            // Intentar crear un archivo temporal para verificar permisos
            var testFile = Path.Combine(normalizedPath, $".write_test_{Guid.NewGuid()}.tmp");

            try
            {
                File.WriteAllText(testFile, "test");
                File.Delete(testFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Obtiene el espacio disponible en disco (en bytes)
    /// </summary>
    public long GetAvailableDiskSpace(string path)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = GetCurrentDirectory();
            }

            var normalizedPath = NormalizePath(path);

            // Obtener el directorio si se proporciona un archivo
            if (File.Exists(normalizedPath))
            {
                normalizedPath = Path.GetDirectoryName(normalizedPath) ?? normalizedPath;
            }

            // Si el directorio no existe, usar el padre
            while (!Directory.Exists(normalizedPath) && !string.IsNullOrEmpty(normalizedPath))
            {
                normalizedPath = Path.GetDirectoryName(normalizedPath) ?? string.Empty;
            }

            if (string.IsNullOrEmpty(normalizedPath))
            {
                normalizedPath = GetCurrentDirectory();
            }

            var drive = new DriveInfo(Path.GetPathRoot(normalizedPath) ?? normalizedPath);
            return drive.AvailableFreeSpace;
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// Normaliza una ruta para funcionar en cualquier sistema operativo
    /// </summary>
    private static string NormalizePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return path;
        }

        // Convertir separadores a los del sistema actual
        var normalized = path.Replace('\\', Path.DirectorySeparatorChar)
                            .Replace('/', Path.DirectorySeparatorChar);

        // Obtener ruta completa si es relativa
        try
        {
            normalized = Path.GetFullPath(normalized);
        }
        catch
        {
            // Si falla, devolver la ruta normalizada básica
        }

        return normalized;
    }
}
