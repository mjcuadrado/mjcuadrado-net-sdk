using FluentAssertions;
using MjCuadrado.NetSdk.Models;
using MjCuadrado.NetSdk.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace MjCuadrado.NetSdk.Tests.Services;

/// <summary>
/// Tests para DoctorService
/// </summary>
public class DoctorServiceTests : IDisposable
{
    private readonly IFileSystemService _fileSystemService;
    private readonly IConfigurationService _configurationService;
    private readonly DoctorService _service;
    private readonly string _tempDir;

    public DoctorServiceTests()
    {
        _fileSystemService = Substitute.For<IFileSystemService>();
        _configurationService = Substitute.For<IConfigurationService>();
        _service = new DoctorService(_fileSystemService, _configurationService);

        _tempDir = Path.Combine(Path.GetTempPath(), $"mjcuadrado-test-{Guid.NewGuid()}");
        Directory.CreateDirectory(_tempDir);
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempDir))
        {
            Directory.Delete(_tempDir, true);
        }
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_WithNullFileSystemService_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => new DoctorService(null!, _configurationService);
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("fileSystemService");
    }

    [Fact]
    public void Constructor_WithNullConfigurationService_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => new DoctorService(_fileSystemService, null!);
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("configurationService");
    }

    #endregion

    #region CheckDotNetVersion Tests

    [Fact]
    public void CheckDotNetVersion_WithDotNetInstalled_ReturnsSuccess()
    {
        // Act
        var result = _service.CheckDotNetVersion();

        // Assert
        // Este test depende de que .NET esté instalado en la máquina
        result.Success.Should().BeTrue();
        result.Version.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void CheckDotNetVersion_ReturnsVersionNumber()
    {
        // Act
        var result = _service.CheckDotNetVersion();

        // Assert
        if (result.Success)
        {
            result.Version.Should().MatchRegex(@"\d+\.\d+");
        }
    }

    #endregion

    #region CheckGitInstallation Tests

    [Fact]
    public void CheckGitInstallation_WithGitInstalled_ReturnsSuccess()
    {
        // Act
        var result = _service.CheckGitInstallation();

        // Assert
        // Este test depende de que Git esté instalado
        result.Success.Should().BeTrue();
        result.Version.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void CheckGitInstallation_ReturnsVersionNumber()
    {
        // Act
        var result = _service.CheckGitInstallation();

        // Assert
        if (result.Success)
        {
            result.Version.Should().MatchRegex(@"\d+\.\d+\.\d+");
        }
    }

    #endregion

    #region CheckProjectStructure Tests

    [Fact]
    public void CheckProjectStructure_WithoutSDKFolder_ReturnsFailure()
    {
        // Arrange
        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(Arg.Any<string>()).Returns(false);

        // Act
        var result = _service.CheckProjectStructure();

        // Assert
        result.Success.Should().BeFalse();
        result.MissingItems.Should().Contain(".mjcuadrado-net-sdk/");
    }

    [Fact]
    public void CheckProjectStructure_WithSDKFolderButNoConfig_ReturnsFailure()
    {
        // Arrange
        var sdkDir = Path.Combine(_tempDir, ".mjcuadrado-net-sdk");

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(sdkDir).Returns(true);
        _fileSystemService.FileExists(Arg.Any<string>()).Returns(false);

        // Act
        var result = _service.CheckProjectStructure();

        // Assert
        result.Success.Should().BeFalse();
        result.MissingItems.Should().Contain("config.json");
    }

    [Fact]
    public void CheckProjectStructure_WithCompleteStructure_ReturnsSuccess()
    {
        // Arrange
        var sdkDir = Path.Combine(_tempDir, ".mjcuadrado-net-sdk");
        var configPath = Path.Combine(sdkDir, "config.json");
        var claudeDir = Path.Combine(_tempDir, ".claude");

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);

        // Simular que todas las carpetas y archivos existen
        _fileSystemService.DirectoryExists(sdkDir).Returns(true);
        _fileSystemService.DirectoryExists(Path.Combine(sdkDir, "memory")).Returns(true);
        _fileSystemService.DirectoryExists(Path.Combine(sdkDir, "reports")).Returns(true);
        _fileSystemService.DirectoryExists(Path.Combine(sdkDir, "specs")).Returns(true);
        _fileSystemService.DirectoryExists(claudeDir).Returns(true);
        _fileSystemService.DirectoryExists(Path.Combine(claudeDir, "agents")).Returns(true);
        _fileSystemService.DirectoryExists(Path.Combine(claudeDir, "commands")).Returns(true);
        _fileSystemService.DirectoryExists(Path.Combine(claudeDir, "skills")).Returns(true);
        _fileSystemService.DirectoryExists(Path.Combine(claudeDir, "hooks")).Returns(true);
        _fileSystemService.FileExists(configPath).Returns(true);

        _configurationService.LoadConfiguration(_tempDir).Returns(new SdkConfiguration
        {
            Project = new ProjectConfig { Name = "test", Version = "1.0.0" }
        });

        // Act
        var result = _service.CheckProjectStructure();

        // Assert
        result.Success.Should().BeTrue();
        result.MissingItems.Should().BeEmpty();
    }

    [Fact]
    public void CheckProjectStructure_WithMissingSubfolders_ReturnsMissingList()
    {
        // Arrange
        var sdkDir = Path.Combine(_tempDir, ".mjcuadrado-net-sdk");
        var configPath = Path.Combine(sdkDir, "config.json");

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(sdkDir).Returns(true);
        _fileSystemService.FileExists(configPath).Returns(true);

        _configurationService.LoadConfiguration(_tempDir).Returns(new SdkConfiguration
        {
            Project = new ProjectConfig { Name = "test", Version = "1.0.0" }
        });

        // Solo memory existe, el resto no
        _fileSystemService.DirectoryExists(Path.Combine(sdkDir, "memory")).Returns(true);
        _fileSystemService.DirectoryExists(Path.Combine(sdkDir, "reports")).Returns(false);
        _fileSystemService.DirectoryExists(Path.Combine(sdkDir, "specs")).Returns(false);
        _fileSystemService.DirectoryExists(Arg.Is<string>(s => s.Contains(".claude"))).Returns(false);

        // Act
        var result = _service.CheckProjectStructure();

        // Assert
        result.Success.Should().BeFalse();
        result.MissingItems.Should().Contain("reports/");
        result.MissingItems.Should().Contain("specs/");
        result.MissingItems.Should().Contain(".claude/");
    }

    #endregion

    #region CheckDiskSpace Tests

    [Fact]
    public void CheckDiskSpace_WithSufficientSpace_ReturnsSuccess()
    {
        // Arrange
        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.GetAvailableDiskSpace(_tempDir).Returns(200L * 1024 * 1024); // 200 MB

        // Act
        var result = _service.CheckDiskSpace();

        // Assert
        result.Success.Should().BeTrue();
        result.AvailableBytes.Should().Be(200L * 1024 * 1024);
    }

    [Fact]
    public void CheckDiskSpace_WithInsufficientSpace_ReturnsFailure()
    {
        // Arrange
        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.GetAvailableDiskSpace(_tempDir).Returns(50L * 1024 * 1024); // Solo 50 MB

        // Act
        var result = _service.CheckDiskSpace();

        // Assert
        result.Success.Should().BeFalse();
        result.AvailableBytes.Should().Be(50L * 1024 * 1024);
    }

    [Fact]
    public void CheckDiskSpace_OnError_ReturnsFailure()
    {
        // Arrange
        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.GetAvailableDiskSpace(_tempDir).Returns(x => throw new IOException("Disk error"));

        // Act
        var result = _service.CheckDiskSpace();

        // Assert
        result.Success.Should().BeFalse();
        result.AvailableBytes.Should().Be(0);
    }

    #endregion

    #region CheckWritePermissions Tests

    [Fact]
    public void CheckWritePermissions_WithPermissions_ReturnsTrue()
    {
        // Arrange
        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.HasWritePermissions(_tempDir).Returns(true);

        // Act
        var result = _service.CheckWritePermissions();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void CheckWritePermissions_WithoutPermissions_ReturnsFalse()
    {
        // Arrange
        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.HasWritePermissions(_tempDir).Returns(false);

        // Act
        var result = _service.CheckWritePermissions();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void CheckWritePermissions_OnError_ReturnsFalse()
    {
        // Arrange
        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.HasWritePermissions(_tempDir).Returns(x => throw new UnauthorizedAccessException());

        // Act
        var result = _service.CheckWritePermissions();

        // Assert
        result.Should().BeFalse();
    }

    #endregion

    #region RunFullDiagnostic Tests

    [Fact]
    public void RunFullDiagnostic_ReturnsResult()
    {
        // Arrange
        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(Arg.Any<string>()).Returns(false);
        _fileSystemService.GetAvailableDiskSpace(_tempDir).Returns(200L * 1024 * 1024);
        _fileSystemService.HasWritePermissions(_tempDir).Returns(true);

        // Act
        var result = _service.RunFullDiagnostic();

        // Assert
        result.Should().NotBeNull();
        result.Checks.Should().HaveCount(5); // .NET, Git, Structure, Disk, Permissions
    }

    [Fact]
    public void RunFullDiagnostic_IncludesAllChecks()
    {
        // Arrange
        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(Arg.Any<string>()).Returns(false);
        _fileSystemService.GetAvailableDiskSpace(_tempDir).Returns(200L * 1024 * 1024);
        _fileSystemService.HasWritePermissions(_tempDir).Returns(true);

        // Act
        var result = _service.RunFullDiagnostic();

        // Assert
        var checkNames = result.Checks.Select(c => c.Name).ToList();
        checkNames.Should().Contain(".NET SDK");
        checkNames.Should().Contain("Git");
        checkNames.Should().Contain("Project Structure");
        checkNames.Should().Contain("Disk Space");
        checkNames.Should().Contain("Write Permissions");
    }

    [Fact]
    public void RunFullDiagnostic_WithMissingProject_AddsSuggestion()
    {
        // Arrange
        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(Arg.Any<string>()).Returns(false);
        _fileSystemService.GetAvailableDiskSpace(_tempDir).Returns(200L * 1024 * 1024);
        _fileSystemService.HasWritePermissions(_tempDir).Returns(true);

        // Act
        var result = _service.RunFullDiagnostic();

        // Assert
        result.Suggestions.Should().Contain(s => s.Contains("mjcuadrado-net-sdk init"));
    }

    [Fact]
    public void RunFullDiagnostic_WithInsufficientDiskSpace_AddsSuggestion()
    {
        // Arrange
        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(Arg.Any<string>()).Returns(true);
        _fileSystemService.GetAvailableDiskSpace(_tempDir).Returns(50L * 1024 * 1024); // Insuficiente
        _fileSystemService.HasWritePermissions(_tempDir).Returns(true);

        // Act
        var result = _service.RunFullDiagnostic();

        // Assert
        result.Suggestions.Should().Contain(s => s.Contains("100 MB"));
    }

    #endregion

    #region DiagnosticResult Tests

    [Fact]
    public void DiagnosticResult_AllChecksPassed_WhenAllSuccess()
    {
        // Arrange
        var result = new DiagnosticResult();
        result.Checks.Add(new DiagnosticCheck { Success = true });
        result.Checks.Add(new DiagnosticCheck { Success = true });

        // Act & Assert
        result.AllChecksPassed.Should().BeTrue();
    }

    [Fact]
    public void DiagnosticResult_AllChecksPassed_WhenAnyFailed()
    {
        // Arrange
        var result = new DiagnosticResult();
        result.Checks.Add(new DiagnosticCheck { Success = true });
        result.Checks.Add(new DiagnosticCheck { Success = false });

        // Act & Assert
        result.AllChecksPassed.Should().BeFalse();
    }

    #endregion
}
