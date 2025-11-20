using FluentAssertions;
using MjCuadrado.NetSdk.Commands;
using MjCuadrado.NetSdk.Models;
using MjCuadrado.NetSdk.Services;
using NSubstitute;
using Spectre.Console.Cli;

namespace MjCuadrado.NetSdk.Tests.Commands;

/// <summary>
/// Tests para InitCommand
/// </summary>
public class InitCommandTests : IDisposable
{
    private readonly IFileSystemService _fileSystemService;
    private readonly IConfigurationService _configurationService;
    private readonly ITemplateService _templateService;
    private readonly InitCommand _command;
    private readonly string _tempDir;

    public InitCommandTests()
    {
        _fileSystemService = Substitute.For<IFileSystemService>();
        _configurationService = Substitute.For<IConfigurationService>();
        _templateService = Substitute.For<ITemplateService>();
        _command = new InitCommand(_fileSystemService, _configurationService, _templateService);

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
        var act = () => new InitCommand(null!, _configurationService, _templateService);
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("fileSystemService");
    }

    [Fact]
    public void Constructor_WithNullConfigurationService_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => new InitCommand(_fileSystemService, null!, _templateService);
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("configurationService");
    }

    [Fact]
    public void Constructor_WithNullTemplateService_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => new InitCommand(_fileSystemService, _configurationService, null!);
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("templateService");
    }

    #endregion

    #region Execute Tests - Project Creation

    [Fact]
    public void Execute_WithProjectName_CreatesNewFolder()
    {
        // Arrange
        var settings = new InitCommand.Settings
        {
            ProjectName = "test-project",
            Force = false
        };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "init", null);

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(Arg.Any<string>()).Returns(false);
        _fileSystemService.HasWritePermissions(Arg.Any<string>()).Returns(true);
        _fileSystemService.GetAvailableDiskSpace(Arg.Any<string>()).Returns(100L * 1024 * 1024); // 100 MB
        _templateService.GenerateProjectStructure(Arg.Any<ProjectInfo>()).Returns(true);

        // Act
        var result = _command.Execute(context, settings);

        // Assert
        result.Should().Be(0);
        _templateService.Received(1).GenerateProjectStructure(Arg.Is<ProjectInfo>(p =>
            p.Name == "test-project" &&
            p.BasePath == Path.Combine(_tempDir, "test-project")
        ));
    }

    [Fact]
    public void Execute_WithoutProjectName_InitializesCurrentDirectory()
    {
        // Arrange
        var settings = new InitCommand.Settings
        {
            ProjectName = null,
            Force = false
        };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "init", null);

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(_tempDir).Returns(true);
        _fileSystemService.FileExists(Arg.Any<string>()).Returns(false); // config.json no existe
        _fileSystemService.HasWritePermissions(_tempDir).Returns(true);
        _fileSystemService.GetAvailableDiskSpace(_tempDir).Returns(100L * 1024 * 1024);
        _templateService.GenerateProjectStructure(Arg.Any<ProjectInfo>()).Returns(true);

        // Act
        var result = _command.Execute(context, settings);

        // Assert
        result.Should().Be(0);
        var expectedDirName = Path.GetFileName(_tempDir);
        _templateService.Received(1).GenerateProjectStructure(Arg.Is<ProjectInfo>(p =>
            p.Name == expectedDirName &&
            p.BasePath == _tempDir
        ));
    }

    [Fact]
    public void Execute_ExistingProject_ReturnsError()
    {
        // Arrange
        var settings = new InitCommand.Settings
        {
            ProjectName = "existing-project",
            Force = false
        };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "init", null);

        var projectPath = Path.Combine(_tempDir, "existing-project");
        var configPath = Path.Combine(projectPath, ".mjcuadrado-net-sdk", "config.json");

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(projectPath).Returns(true);
        _fileSystemService.FileExists(configPath).Returns(true); // config.json existe
        _fileSystemService.HasWritePermissions(projectPath).Returns(true);

        // Act
        var result = _command.Execute(context, settings);

        // Assert
        result.Should().Be(1);
        _templateService.DidNotReceive().GenerateProjectStructure(Arg.Any<ProjectInfo>());
    }

    [Fact]
    public void Execute_WithForce_OverwritesExisting()
    {
        // Arrange
        var settings = new InitCommand.Settings
        {
            ProjectName = "existing-project",
            Force = true
        };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "init", null);

        var projectPath = Path.Combine(_tempDir, "existing-project");
        var configPath = Path.Combine(projectPath, ".mjcuadrado-net-sdk", "config.json");

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(projectPath).Returns(true);
        _fileSystemService.FileExists(configPath).Returns(true);
        _fileSystemService.HasWritePermissions(projectPath).Returns(true);
        _fileSystemService.GetAvailableDiskSpace(projectPath).Returns(100L * 1024 * 1024);
        _templateService.GenerateProjectStructure(Arg.Any<ProjectInfo>()).Returns(true);

        // Act
        var result = _command.Execute(context, settings);

        // Assert
        result.Should().Be(0);
        _templateService.Received(1).GenerateProjectStructure(Arg.Is<ProjectInfo>(p =>
            p.Name == "existing-project" &&
            p.Force == true
        ));
    }

    #endregion

    #region Execute Tests - Validation Errors

    [Theory]
    [InlineData("project/name")]
    [InlineData("project\\name")]
    [InlineData("project:name")]
    [InlineData("project*name")]
    [InlineData("project?name")]
    [InlineData("project\"name")]
    [InlineData("project<name")]
    [InlineData("project>name")]
    [InlineData("project|name")]
    public void Execute_InvalidProjectName_ReturnsError(string invalidName)
    {
        // Arrange
        var settings = new InitCommand.Settings
        {
            ProjectName = invalidName,
            Force = false
        };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "init", null);

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);

        // Act
        var result = _command.Execute(context, settings);

        // Assert
        result.Should().Be(1);
        _templateService.DidNotReceive().GenerateProjectStructure(Arg.Any<ProjectInfo>());
    }

    [Fact]
    public void Execute_NoPermissions_ReturnsError()
    {
        // Arrange
        var settings = new InitCommand.Settings
        {
            ProjectName = "test-project",
            Force = false
        };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "init", null);

        var projectPath = Path.Combine(_tempDir, "test-project");

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(projectPath).Returns(false);
        _fileSystemService.HasWritePermissions(projectPath).Returns(false); // Sin permisos

        // Act
        var result = _command.Execute(context, settings);

        // Assert
        result.Should().Be(1);
        _templateService.DidNotReceive().GenerateProjectStructure(Arg.Any<ProjectInfo>());
    }

    [Fact]
    public void Execute_InsufficientDiskSpace_ReturnsError()
    {
        // Arrange
        var settings = new InitCommand.Settings
        {
            ProjectName = "test-project",
            Force = false
        };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "init", null);

        var projectPath = Path.Combine(_tempDir, "test-project");

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(projectPath).Returns(false);
        _fileSystemService.HasWritePermissions(projectPath).Returns(true);
        _fileSystemService.GetAvailableDiskSpace(projectPath).Returns(5L * 1024 * 1024); // Solo 5 MB

        // Act
        var result = _command.Execute(context, settings);

        // Assert
        result.Should().Be(1);
        _templateService.DidNotReceive().GenerateProjectStructure(Arg.Any<ProjectInfo>());
    }

    [Fact]
    public void Execute_TemplateServiceFails_ReturnsError()
    {
        // Arrange
        var settings = new InitCommand.Settings
        {
            ProjectName = "test-project",
            Force = false
        };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "init", null);

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(Arg.Any<string>()).Returns(false);
        _fileSystemService.HasWritePermissions(Arg.Any<string>()).Returns(true);
        _fileSystemService.GetAvailableDiskSpace(Arg.Any<string>()).Returns(100L * 1024 * 1024);
        _templateService.GenerateProjectStructure(Arg.Any<ProjectInfo>()).Returns(false); // Fallo

        // Act
        var result = _command.Execute(context, settings);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public void Execute_UnexpectedException_ReturnsError()
    {
        // Arrange
        var settings = new InitCommand.Settings
        {
            ProjectName = "test-project",
            Force = false
        };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "init", null);

        _fileSystemService.When(x => x.GetCurrentDirectory()).Do(_ => throw new InvalidOperationException("Test exception"));

        // Act
        var result = _command.Execute(context, settings);

        // Assert
        result.Should().Be(1);
    }

    #endregion

    #region Valid Project Names

    [Theory]
    [InlineData("my-project")]
    [InlineData("my_project")]
    [InlineData("my.project")]
    [InlineData("MyProject")]
    [InlineData("my-project-123")]
    [InlineData("123-project")]
    public void Execute_ValidProjectName_Succeeds(string validName)
    {
        // Arrange
        var settings = new InitCommand.Settings
        {
            ProjectName = validName,
            Force = false
        };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "init", null);

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(Arg.Any<string>()).Returns(false);
        _fileSystemService.HasWritePermissions(Arg.Any<string>()).Returns(true);
        _fileSystemService.GetAvailableDiskSpace(Arg.Any<string>()).Returns(100L * 1024 * 1024);
        _templateService.GenerateProjectStructure(Arg.Any<ProjectInfo>()).Returns(true);

        // Act
        var result = _command.Execute(context, settings);

        // Assert
        result.Should().Be(0);
        _templateService.Received(1).GenerateProjectStructure(Arg.Any<ProjectInfo>());
    }

    #endregion

    #region Settings Tests

    [Fact]
    public void Settings_DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var settings = new InitCommand.Settings();

        // Assert
        settings.ProjectName.Should().BeNull();
        settings.Force.Should().BeFalse();
        settings.Author.Should().Be("@user");
        settings.Framework.Should().Be("net10.0");
    }

    [Fact]
    public void Settings_CanSetAllProperties()
    {
        // Arrange & Act
        var settings = new InitCommand.Settings
        {
            ProjectName = "test-project",
            Force = true,
            Author = "@developer",
            Framework = "net9.0"
        };

        // Assert
        settings.ProjectName.Should().Be("test-project");
        settings.Force.Should().BeTrue();
        settings.Author.Should().Be("@developer");
        settings.Framework.Should().Be("net9.0");
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void Execute_CompleteWorkflow_CallsAllServices()
    {
        // Arrange
        var settings = new InitCommand.Settings
        {
            ProjectName = "integration-test",
            Force = false,
            Author = "@tester",
            Framework = "net10.0"
        };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "init", null);

        var projectPath = Path.Combine(_tempDir, "integration-test");

        _fileSystemService.GetCurrentDirectory().Returns(_tempDir);
        _fileSystemService.DirectoryExists(projectPath).Returns(false);
        _fileSystemService.FileExists(Arg.Any<string>()).Returns(false);
        _fileSystemService.HasWritePermissions(projectPath).Returns(true);
        _fileSystemService.GetAvailableDiskSpace(projectPath).Returns(100L * 1024 * 1024);
        _templateService.GenerateProjectStructure(Arg.Any<ProjectInfo>()).Returns(true);

        // Act
        var result = _command.Execute(context, settings);

        // Assert
        result.Should().Be(0);

        // Verificar que se llamó a FileSystemService
        _fileSystemService.Received(1).GetCurrentDirectory();
        _fileSystemService.Received().DirectoryExists(projectPath);
        _fileSystemService.Received(1).HasWritePermissions(projectPath);
        _fileSystemService.Received(1).GetAvailableDiskSpace(projectPath);

        // Verificar que se llamó a TemplateService con info correcta
        _templateService.Received(1).GenerateProjectStructure(Arg.Is<ProjectInfo>(p =>
            p.Name == "integration-test" &&
            p.BasePath == projectPath &&
            p.Author == "@tester" &&
            p.Framework == "net10.0" &&
            p.SdkVersion == "0.1.0" &&
            !p.Force
        ));
    }

    #endregion
}
