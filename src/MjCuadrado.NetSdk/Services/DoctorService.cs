using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MjCuadrado.NetSdk.Services;

/// <summary>
/// Implementación del servicio de diagnóstico
/// </summary>
public class DoctorService : IDoctorService
{
    private readonly IFileSystemService _fileSystemService;
    private readonly IConfigurationService _configurationService;

    public DoctorService(
        IFileSystemService fileSystemService,
        IConfigurationService configurationService)
    {
        _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
    }

    /// <summary>
    /// Verifica la versión de .NET SDK instalada
    /// </summary>
    public (bool Success, string Version) CheckDotNetVersion()
    {
        try
        {
            var result = ExecuteCommand("dotnet", "--version");
            if (!result.success)
            {
                return (false, "Not installed");
            }

            var version = result.output.Trim();

            // Parsear versión (formato: 10.0.100 o 9.0.101)
            var match = Regex.Match(version, @"^(\d+)\.(\d+)");
            if (!match.Success)
            {
                return (false, version);
            }

            var major = int.Parse(match.Groups[1].Value);

            // Verificar que sea >= 9.0
            if (major >= 9)
            {
                return (true, version);
            }

            return (false, $"{version} (required >= 9.0)");
        }
        catch (Exception)
        {
            return (false, "Not installed");
        }
    }

    /// <summary>
    /// Verifica si Git está instalado y configurado
    /// </summary>
    public (bool Success, string Version, bool Configured) CheckGitInstallation()
    {
        try
        {
            // Verificar que git esté instalado
            var versionResult = ExecuteCommand("git", "--version");
            if (!versionResult.success)
            {
                return (false, "Not installed", false);
            }

            var version = versionResult.output.Trim();

            // Extraer número de versión (formato: "git version 2.39.3")
            var match = Regex.Match(version, @"(\d+\.\d+\.\d+)");
            var versionNumber = match.Success ? match.Groups[1].Value : version;

            // Verificar configuración
            var nameResult = ExecuteCommand("git", "config user.name");
            var emailResult = ExecuteCommand("git", "config user.email");

            var configured = nameResult.success && emailResult.success &&
                           !string.IsNullOrWhiteSpace(nameResult.output) &&
                           !string.IsNullOrWhiteSpace(emailResult.output);

            return (true, versionNumber, configured);
        }
        catch (Exception)
        {
            return (false, "Not installed", false);
        }
    }

    /// <summary>
    /// Verifica la estructura del proyecto actual
    /// </summary>
    public (bool Success, List<string> MissingItems) CheckProjectStructure()
    {
        var missingItems = new List<string>();
        var currentDir = _fileSystemService.GetCurrentDirectory();

        // Verificar carpeta principal
        var sdkDir = Path.Combine(currentDir, ".mjcuadrado-net-sdk");
        if (!_fileSystemService.DirectoryExists(sdkDir))
        {
            missingItems.Add(".mjcuadrado-net-sdk/");
            // Si no existe la carpeta principal, no es un proyecto inicializado
            return (false, missingItems);
        }

        // Verificar config.json
        var configPath = Path.Combine(sdkDir, "config.json");
        if (!_fileSystemService.FileExists(configPath))
        {
            missingItems.Add("config.json");
        }
        else
        {
            // Validar que el config.json sea válido
            try
            {
                var config = _configurationService.LoadConfiguration(currentDir);
                if (config == null)
                {
                    missingItems.Add("config.json (invalid)");
                }
            }
            catch
            {
                missingItems.Add("config.json (invalid)");
            }
        }

        // Verificar carpetas requeridas
        var requiredFolders = new[]
        {
            "memory",
            "reports",
            "specs"
        };

        foreach (var folder in requiredFolders)
        {
            var folderPath = Path.Combine(sdkDir, folder);
            if (!_fileSystemService.DirectoryExists(folderPath))
            {
                missingItems.Add($"{folder}/");
            }
        }

        // Verificar carpeta .claude
        var claudeDir = Path.Combine(currentDir, ".claude");
        if (!_fileSystemService.DirectoryExists(claudeDir))
        {
            missingItems.Add(".claude/");
        }
        else
        {
            // Verificar subcarpetas de Claude
            var claudeFolders = new[] { "agents", "commands", "skills", "hooks" };
            foreach (var folder in claudeFolders)
            {
                var folderPath = Path.Combine(claudeDir, folder);
                if (!_fileSystemService.DirectoryExists(folderPath))
                {
                    missingItems.Add($".claude/{folder}/");
                }
            }
        }

        return (missingItems.Count == 0, missingItems);
    }

    /// <summary>
    /// Verifica el espacio disponible en disco
    /// </summary>
    public (bool Success, long AvailableBytes) CheckDiskSpace()
    {
        try
        {
            var currentDir = _fileSystemService.GetCurrentDirectory();
            var availableBytes = _fileSystemService.GetAvailableDiskSpace(currentDir);

            // Requerir mínimo 100 MB
            var minimumBytes = 100L * 1024 * 1024;
            return (availableBytes >= minimumBytes, availableBytes);
        }
        catch (Exception)
        {
            return (false, 0);
        }
    }

    /// <summary>
    /// Verifica permisos de escritura en el directorio actual
    /// </summary>
    public bool CheckWritePermissions()
    {
        try
        {
            var currentDir = _fileSystemService.GetCurrentDirectory();
            return _fileSystemService.HasWritePermissions(currentDir);
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Ejecuta todos los checks y retorna un reporte completo
    /// </summary>
    public DiagnosticResult RunFullDiagnostic()
    {
        var result = new DiagnosticResult();

        // 1. Check .NET SDK
        var dotnetCheck = CheckDotNetVersion();
        result.Checks.Add(new DiagnosticCheck
        {
            Name = ".NET SDK",
            Success = dotnetCheck.Success,
            Message = dotnetCheck.Success ? "Installed" : "Not found or outdated",
            Details = dotnetCheck.Version
        });

        if (!dotnetCheck.Success)
        {
            result.Suggestions.Add("Install .NET SDK 9.0 or higher from https://dotnet.microsoft.com/download");
        }

        // 2. Check Git
        var gitCheck = CheckGitInstallation();
        result.Checks.Add(new DiagnosticCheck
        {
            Name = "Git",
            Success = gitCheck.Success,
            Message = gitCheck.Success ? "Installed" : "Not found",
            Details = gitCheck.Version
        });

        if (gitCheck.Success && !gitCheck.Configured)
        {
            result.Warnings.Add("Git is not configured");
            result.Suggestions.Add("Configure Git with: git config --global user.name \"Your Name\"");
            result.Suggestions.Add("Configure Git with: git config --global user.email \"your@email.com\"");
        }

        if (!gitCheck.Success)
        {
            result.Suggestions.Add("Install Git from https://git-scm.com/downloads");
        }

        // 3. Check project structure
        var structureCheck = CheckProjectStructure();
        var structureSuccess = structureCheck.Success;
        var missingCount = structureCheck.MissingItems.Count;

        result.Checks.Add(new DiagnosticCheck
        {
            Name = "Project Structure",
            Success = structureSuccess,
            Message = structureSuccess ? "Complete" : $"{missingCount} items missing",
            Details = structureSuccess ? "All folders present" : string.Join(", ", structureCheck.MissingItems)
        });

        if (!structureSuccess)
        {
            if (structureCheck.MissingItems.Contains(".mjcuadrado-net-sdk/"))
            {
                result.Suggestions.Add("Initialize project with: mjcuadrado-net-sdk init");
            }
            else
            {
                result.Warnings.Add($"Missing: {string.Join(", ", structureCheck.MissingItems)}");
                result.Suggestions.Add("Run 'mjcuadrado-net-sdk init --force' to recreate missing structure");
            }
        }

        // 4. Check disk space
        var diskCheck = CheckDiskSpace();
        var diskMB = diskCheck.AvailableBytes / (1024.0 * 1024.0);
        result.Checks.Add(new DiagnosticCheck
        {
            Name = "Disk Space",
            Success = diskCheck.Success,
            Message = diskCheck.Success ? "Sufficient" : "Insufficient",
            Details = $"{diskMB:F2} MB available"
        });

        if (!diskCheck.Success)
        {
            result.Suggestions.Add("Free up at least 100 MB of disk space");
        }

        // 5. Check write permissions
        var permissionsCheck = CheckWritePermissions();
        result.Checks.Add(new DiagnosticCheck
        {
            Name = "Write Permissions",
            Success = permissionsCheck,
            Message = permissionsCheck ? "OK" : "Denied",
            Details = permissionsCheck ? "Can write to current directory" : "Cannot write to current directory"
        });

        if (!permissionsCheck)
        {
            result.Suggestions.Add("Check directory permissions or run with appropriate privileges");
        }

        return result;
    }

    #region Helper Methods

    /// <summary>
    /// Ejecuta un comando externo y captura su salida
    /// </summary>
    private (bool success, string output) ExecuteCommand(string command, string arguments)
    {
        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(processStartInfo);
            if (process == null)
            {
                return (false, string.Empty);
            }

            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return (process.ExitCode == 0, output);
        }
        catch (Exception)
        {
            return (false, string.Empty);
        }
    }

    #endregion
}
