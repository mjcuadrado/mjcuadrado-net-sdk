using FluentAssertions;
using MjCuadrado.NetSdk.Models;
using MjCuadrado.NetSdk.Services;

namespace MjCuadrado.NetSdk.Tests.Services;

public class TemplateServiceTests : IDisposable
{
    private readonly TemplateService _service;
    private readonly FileSystemService _fileSystemService;
    private readonly string _testDirectory;

    public TemplateServiceTests()
    {
        _fileSystemService = new FileSystemService();
        _service = new TemplateService(_fileSystemService);
        _testDirectory = Path.Combine(Path.GetTempPath(), $"mjcuadrado-template-test-{Guid.NewGuid()}");
        Directory.CreateDirectory(_testDirectory);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDirectory))
        {
            try
            {
                Directory.Delete(_testDirectory, recursive: true);
            }
            catch
            {
                // Ignorar errores al limpiar
            }
        }
    }

    #region GenerateProjectStructure Tests

    [Fact]
    public void GenerateProjectStructure_CreatesAllFolders()
    {
        // Arrange
        var projectInfo = CreateValidProjectInfo();

        // Act
        var result = _service.GenerateProjectStructure(projectInfo);

        // Assert
        result.Should().BeTrue();

        // Verificar carpetas principales
        Directory.Exists(Path.Combine(_testDirectory, ".mjcuadrado-net-sdk")).Should().BeTrue();
        Directory.Exists(Path.Combine(_testDirectory, ".claude")).Should().BeTrue();

        // Verificar subcarpetas de .mjcuadrado-net-sdk
        Directory.Exists(Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "memory")).Should().BeTrue();
        Directory.Exists(Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "reports")).Should().BeTrue();
        Directory.Exists(Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "specs")).Should().BeTrue();

        // Verificar subcarpetas de .claude
        Directory.Exists(Path.Combine(_testDirectory, ".claude", "agents")).Should().BeTrue();
        Directory.Exists(Path.Combine(_testDirectory, ".claude", "commands")).Should().BeTrue();
        Directory.Exists(Path.Combine(_testDirectory, ".claude", "skills")).Should().BeTrue();
        Directory.Exists(Path.Combine(_testDirectory, ".claude", "hooks")).Should().BeTrue();
    }

    [Fact]
    public void GenerateProjectStructure_CreatesConfigFile()
    {
        // Arrange
        var projectInfo = CreateValidProjectInfo();

        // Act
        _service.GenerateProjectStructure(projectInfo);

        // Assert
        var configPath = Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "config.json");
        File.Exists(configPath).Should().BeTrue();

        var content = File.ReadAllText(configPath);
        content.Should().Contain(projectInfo.Name);
    }

    [Fact]
    public void GenerateProjectStructure_CreatesReadmeFiles()
    {
        // Arrange
        var projectInfo = CreateValidProjectInfo();

        // Act
        _service.GenerateProjectStructure(projectInfo);

        // Assert
        // Archivos de documentación base
        File.Exists(Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "product.md")).Should().BeTrue();
        File.Exists(Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "structure.md")).Should().BeTrue();
        File.Exists(Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "tech.md")).Should().BeTrue();

        // READMEs de subcarpetas
        File.Exists(Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "specs", "README.md")).Should().BeTrue();
        File.Exists(Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "memory", "README.md")).Should().BeTrue();
        File.Exists(Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "reports", "README.md")).Should().BeTrue();

        // READMEs de Claude
        File.Exists(Path.Combine(_testDirectory, ".claude", "agents", "README.md")).Should().BeTrue();
        File.Exists(Path.Combine(_testDirectory, ".claude", "commands", "README.md")).Should().BeTrue();
        File.Exists(Path.Combine(_testDirectory, ".claude", "skills", "README.md")).Should().BeTrue();
        File.Exists(Path.Combine(_testDirectory, ".claude", "hooks", "README.md")).Should().BeTrue();
    }

    [Fact]
    public void GenerateProjectStructure_WhenProjectInfoNull_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => _service.GenerateProjectStructure(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void GenerateProjectStructure_WhenBasePathEmpty_ThrowsArgumentException()
    {
        // Arrange
        var projectInfo = CreateValidProjectInfo();
        projectInfo.BasePath = "";

        // Act & Assert
        var act = () => _service.GenerateProjectStructure(projectInfo);
        act.Should().Throw<ArgumentException>();
    }

    #endregion

    #region GenerateConfigFile Tests

    [Fact]
    public void GenerateConfigFile_ReplacesVariables()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "config.json");
        var projectInfo = CreateValidProjectInfo();
        projectInfo.Name = "test-project-123";

        // Act
        var result = _service.GenerateConfigFile(configPath, projectInfo);

        // Assert
        result.Should().BeTrue();
        File.Exists(configPath).Should().BeTrue();

        var content = File.ReadAllText(configPath);
        content.Should().Contain("test-project-123");
        content.Should().Contain(projectInfo.Framework);
        content.Should().Contain(projectInfo.Author);
    }

    [Fact]
    public void GenerateConfigFile_CreatesValidJson()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "config.json");
        var projectInfo = CreateValidProjectInfo();

        // Act
        _service.GenerateConfigFile(configPath, projectInfo);

        // Assert
        var content = File.ReadAllText(configPath);
        content.Should().Contain("\"project\":");
        content.Should().Contain("\"sdk\":");
        content.Should().Contain("\"language\":");
    }

    [Fact]
    public void GenerateConfigFile_WhenPathEmpty_ThrowsArgumentException()
    {
        // Arrange
        var projectInfo = CreateValidProjectInfo();

        // Act & Assert
        var act = () => _service.GenerateConfigFile("", projectInfo);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GenerateConfigFile_WhenProjectInfoNull_ThrowsArgumentNullException()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "config.json");

        // Act & Assert
        var act = () => _service.GenerateConfigFile(configPath, null!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region GenerateReadmeFiles Tests

    [Fact]
    public void GenerateReadmeFiles_CreatesAllReadmes()
    {
        // Arrange
        var projectInfo = CreateValidProjectInfo();
        // Crear las carpetas primero
        _fileSystemService.CreateDirectoryStructure(_testDirectory, new[]
        {
            ".mjcuadrado-net-sdk",
            ".mjcuadrado-net-sdk/specs",
            ".mjcuadrado-net-sdk/memory",
            ".mjcuadrado-net-sdk/reports",
            ".claude/agents",
            ".claude/commands",
            ".claude/skills",
            ".claude/hooks"
        });

        // Act
        var result = _service.GenerateReadmeFiles(_testDirectory, projectInfo);

        // Assert
        result.Should().BeTrue();

        // Verificar que se crearon todos los archivos
        var expectedFiles = new[]
        {
            Path.Combine(".mjcuadrado-net-sdk", "product.md"),
            Path.Combine(".mjcuadrado-net-sdk", "structure.md"),
            Path.Combine(".mjcuadrado-net-sdk", "tech.md"),
            Path.Combine(".mjcuadrado-net-sdk", "specs", "README.md"),
            Path.Combine(".mjcuadrado-net-sdk", "memory", "README.md"),
            Path.Combine(".mjcuadrado-net-sdk", "reports", "README.md"),
            Path.Combine(".claude", "agents", "README.md"),
            Path.Combine(".claude", "commands", "README.md"),
            Path.Combine(".claude", "skills", "README.md"),
            Path.Combine(".claude", "hooks", "README.md")
        };

        foreach (var file in expectedFiles)
        {
            var fullPath = Path.Combine(_testDirectory, file);
            File.Exists(fullPath).Should().BeTrue($"debería existir: {file}");
        }
    }

    [Fact]
    public void GenerateReadmeFiles_ReplacesProjectName()
    {
        // Arrange
        var projectInfo = CreateValidProjectInfo();
        projectInfo.Name = "my-awesome-project";
        _fileSystemService.CreateDirectory(Path.Combine(_testDirectory, ".mjcuadrado-net-sdk"));

        // Act
        _service.GenerateReadmeFiles(_testDirectory, projectInfo);

        // Assert
        var productPath = Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "product.md");
        var content = File.ReadAllText(productPath);
        content.Should().Contain("my-awesome-project");
    }

    [Fact]
    public void GenerateReadmeFiles_WhenBasePathEmpty_ThrowsArgumentException()
    {
        // Arrange
        var projectInfo = CreateValidProjectInfo();

        // Act & Assert
        var act = () => _service.GenerateReadmeFiles("", projectInfo);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GenerateReadmeFiles_WhenProjectInfoNull_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => _service.GenerateReadmeFiles(_testDirectory, null!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region GetTemplateContent Tests

    [Fact]
    public void GetTemplateContent_ReturnsContent()
    {
        // Act
        var content = _service.GetTemplateContent("config.json.template");

        // Assert
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain("{{PROJECT_NAME}}");
        content.Should().Contain("{{DATE}}");
    }

    [Fact]
    public void GetTemplateContent_WhenTemplateExists_ReturnsValidContent()
    {
        // Act
        var content = _service.GetTemplateContent("product.md.template");

        // Assert
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain("{{PROJECT_NAME}}");
    }

    [Fact]
    public void GetTemplateContent_WhenTemplateNameEmpty_ThrowsArgumentException()
    {
        // Act & Assert
        var act = () => _service.GetTemplateContent("");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetTemplateContent_WhenTemplateNotFound_ThrowsFileNotFoundException()
    {
        // Act & Assert
        var act = () => _service.GetTemplateContent("nonexistent.template");
        act.Should().Throw<FileNotFoundException>();
    }

    [Theory]
    [InlineData("config.json.template")]
    [InlineData("product.md.template")]
    [InlineData("structure.md.template")]
    [InlineData("tech.md.template")]
    [InlineData("specs-README.md.template")]
    [InlineData("memory-README.md.template")]
    [InlineData("reports-README.md.template")]
    [InlineData("claude-agents-README.md.template")]
    [InlineData("claude-commands-README.md.template")]
    [InlineData("claude-skills-README.md.template")]
    [InlineData("claude-hooks-README.md.template")]
    public void GetTemplateContent_AllTemplatesExist(string templateName)
    {
        // Act
        var content = _service.GetTemplateContent(templateName);

        // Assert
        content.Should().NotBeNullOrEmpty();
    }

    #endregion

    #region ReplaceVariables Tests

    [Fact]
    public void ReplaceVariables_ReplacesAllOccurrences()
    {
        // Arrange
        var content = "Project: {{PROJECT_NAME}}, Version: {{VERSION}}, Author: {{AUTHOR}}";
        var variables = new Dictionary<string, string>
        {
            { "PROJECT_NAME", "TestProject" },
            { "VERSION", "1.0.0" },
            { "AUTHOR", "@testuser" }
        };

        // Act
        var result = _service.ReplaceVariables(content, variables);

        // Assert
        result.Should().Be("Project: TestProject, Version: 1.0.0, Author: @testuser");
    }

    [Fact]
    public void ReplaceVariables_WhenNoVariables_ReturnsOriginalContent()
    {
        // Arrange
        var content = "No variables here";
        var variables = new Dictionary<string, string>();

        // Act
        var result = _service.ReplaceVariables(content, variables);

        // Assert
        result.Should().Be(content);
    }

    [Fact]
    public void ReplaceVariables_ReplacesMultipleOccurrencesOfSameVariable()
    {
        // Arrange
        var content = "{{PROJECT_NAME}} is a great project. Use {{PROJECT_NAME}} wisely.";
        var variables = new Dictionary<string, string>
        {
            { "PROJECT_NAME", "MyApp" }
        };

        // Act
        var result = _service.ReplaceVariables(content, variables);

        // Assert
        result.Should().Be("MyApp is a great project. Use MyApp wisely.");
    }

    [Fact]
    public void ReplaceVariables_WhenContentNull_ThrowsArgumentNullException()
    {
        // Arrange
        var variables = new Dictionary<string, string>();

        // Act & Assert
        var act = () => _service.ReplaceVariables(null!, variables);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ReplaceVariables_WhenVariablesNull_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => _service.ReplaceVariables("content", null!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region CreateVariablesDictionary Tests

    [Fact]
    public void CreateVariablesDictionary_CreatesAllVariables()
    {
        // Arrange
        var projectInfo = CreateValidProjectInfo();
        projectInfo.Name = "TestProject";
        projectInfo.Author = "@testauthor";
        projectInfo.Framework = "net10.0";
        projectInfo.SdkVersion = "0.2.0";
        projectInfo.CreatedDate = "2024-12-25";

        // Act
        var variables = _service.CreateVariablesDictionary(projectInfo);

        // Assert
        variables.Should().ContainKey("PROJECT_NAME");
        variables.Should().ContainKey("VERSION");
        variables.Should().ContainKey("DATE");
        variables.Should().ContainKey("AUTHOR");
        variables.Should().ContainKey("FRAMEWORK");
        variables.Should().ContainKey("SDK_VERSION");

        variables["PROJECT_NAME"].Should().Be("TestProject");
        variables["AUTHOR"].Should().Be("@testauthor");
        variables["FRAMEWORK"].Should().Be("net10.0");
        variables["SDK_VERSION"].Should().Be("0.2.0");
        variables["DATE"].Should().Be("2024-12-25");
    }

    [Fact]
    public void CreateVariablesDictionary_UsesDefaultsForNullValues()
    {
        // Arrange
        var projectInfo = new ProjectInfo
        {
            Name = "TestProject",
            BasePath = _testDirectory
        };

        // Act
        var variables = _service.CreateVariablesDictionary(projectInfo);

        // Assert
        variables["PROJECT_NAME"].Should().Be("TestProject");
        variables["AUTHOR"].Should().NotBeNullOrEmpty();
        variables["FRAMEWORK"].Should().NotBeNullOrEmpty();
        variables["SDK_VERSION"].Should().NotBeNullOrEmpty();
        variables["DATE"].Should().MatchRegex(@"^\d{4}-\d{2}-\d{2}$");
    }

    [Fact]
    public void CreateVariablesDictionary_WhenProjectInfoNull_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => _service.CreateVariablesDictionary(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void FullWorkflow_GeneratesCompleteProjectStructure()
    {
        // Arrange
        var projectInfo = CreateValidProjectInfo();
        projectInfo.Name = "integration-test-project";

        // Act
        var result = _service.GenerateProjectStructure(projectInfo);

        // Assert
        result.Should().BeTrue();

        // Verificar que todas las carpetas existen
        var expectedFolders = new[]
        {
            ".mjcuadrado-net-sdk",
            ".mjcuadrado-net-sdk/memory",
            ".mjcuadrado-net-sdk/reports",
            ".mjcuadrado-net-sdk/specs",
            ".claude",
            ".claude/agents",
            ".claude/commands",
            ".claude/skills",
            ".claude/hooks"
        };

        foreach (var folder in expectedFolders)
        {
            Directory.Exists(Path.Combine(_testDirectory, folder)).Should().BeTrue($"debería existir: {folder}");
        }

        // Verificar config.json
        var configPath = Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "config.json");
        File.Exists(configPath).Should().BeTrue();
        var configContent = File.ReadAllText(configPath);
        configContent.Should().Contain("integration-test-project");

        // Verificar que los READMEs tienen el nombre correcto del proyecto
        var productPath = Path.Combine(_testDirectory, ".mjcuadrado-net-sdk", "product.md");
        var productContent = File.ReadAllText(productPath);
        productContent.Should().Contain("integration-test-project");
    }

    #endregion

    #region Helper Methods

    private ProjectInfo CreateValidProjectInfo()
    {
        return new ProjectInfo
        {
            Name = "test-project",
            BasePath = _testDirectory,
            Author = "@testuser",
            Framework = "net10.0",
            SdkVersion = "0.1.0",
            CreatedDate = "2024-01-01",
            Force = false
        };
    }

    #endregion
}
