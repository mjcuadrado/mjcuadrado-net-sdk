using FluentAssertions;
using MjCuadrado.NetSdk.Models;
using MjCuadrado.NetSdk.Services;

namespace MjCuadrado.NetSdk.Tests.Services;

public class ConfigurationServiceTests : IDisposable
{
    private readonly ConfigurationService _service;
    private readonly string _testDirectory;

    public ConfigurationServiceTests()
    {
        _service = new ConfigurationService();
        _testDirectory = Path.Combine(Path.GetTempPath(), $"mjcuadrado-config-test-{Guid.NewGuid()}");
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

    #region LoadConfiguration Tests

    [Fact]
    public void LoadConfiguration_WhenValidFile_ReturnsConfiguration()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "config.json");
        var validJson = @"{
            ""project"": {
                ""name"": ""test-project"",
                ""version"": ""1.0.0"",
                ""template_version"": ""0.1.0"",
                ""created"": ""2024-01-01"",
                ""updated"": ""2024-01-02"",
                ""language"": ""csharp"",
                ""framework"": ""net10.0"",
                ""mode"": ""personal"",
                ""author"": ""@testuser""
            },
            ""sdk"": {
                ""version"": ""0.1.0"",
                ""min_dotnet_version"": ""9.0.0""
            },
            ""language"": {
                ""conversation_language"": ""es"",
                ""conversation_language_name"": ""Spanish""
            },
            ""github"": {
                ""enabled"": false
            },
            ""optimization"": {
                ""template_synced"": false
            }
        }";
        File.WriteAllText(configPath, validJson);

        // Act
        var config = _service.LoadConfiguration(configPath);

        // Assert
        config.Should().NotBeNull();
        config.Project.Name.Should().Be("test-project");
        config.Project.Version.Should().Be("1.0.0");
        config.Sdk.Version.Should().Be("0.1.0");
        config.Language.ConversationLanguage.Should().Be("es");
    }

    [Fact]
    public void LoadConfiguration_WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "nonexistent.json");

        // Act & Assert
        var act = () => _service.LoadConfiguration(configPath);
        act.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void LoadConfiguration_WhenPathEmpty_ThrowsArgumentException()
    {
        // Act & Assert
        var act = () => _service.LoadConfiguration("");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void LoadConfiguration_WhenInvalidJson_ThrowsInvalidOperationException()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "invalid.json");
        File.WriteAllText(configPath, "{invalid json content}");

        // Act & Assert
        var act = () => _service.LoadConfiguration(configPath);
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*parsear*");
    }

    [Fact]
    public void LoadConfiguration_WhenEmptyFile_ThrowsInvalidOperationException()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "empty.json");
        File.WriteAllText(configPath, "");

        // Act & Assert
        var act = () => _service.LoadConfiguration(configPath);
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void LoadConfiguration_WithComments_ParsesCorrectly()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "with-comments.json");
        var jsonWithComments = @"{
            // Esto es un comentario
            ""project"": {
                ""name"": ""test"",
                ""version"": ""1.0.0"",
                ""template_version"": ""0.1.0"",
                ""created"": ""2024-01-01"",
                ""updated"": ""2024-01-01"",
                ""language"": ""csharp"",
                ""framework"": ""net10.0"",
                ""mode"": ""personal"",
                ""author"": ""@user""
            },
            ""sdk"": { ""version"": ""0.1.0"", ""min_dotnet_version"": ""9.0.0"" },
            ""language"": { ""conversation_language"": ""es"", ""conversation_language_name"": ""Spanish"" },
            ""github"": { ""enabled"": false },
            ""optimization"": { ""template_synced"": false }
        }";
        File.WriteAllText(configPath, jsonWithComments);

        // Act
        var config = _service.LoadConfiguration(configPath);

        // Assert
        config.Should().NotBeNull();
        config.Project.Name.Should().Be("test");
    }

    #endregion

    #region SaveConfiguration Tests

    [Fact]
    public void SaveConfiguration_WhenValid_WritesFile()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "save-test.json");
        var config = CreateValidConfiguration();

        // Act
        var result = _service.SaveConfiguration(configPath, config);

        // Assert
        result.Should().BeTrue();
        File.Exists(configPath).Should().BeTrue();

        // Verify content
        var savedConfig = _service.LoadConfiguration(configPath);
        savedConfig.Project.Name.Should().Be(config.Project.Name);
    }

    [Fact]
    public void SaveConfiguration_WhenDirectoryDoesNotExist_CreatesDirectory()
    {
        // Arrange
        var subdir = Path.Combine(_testDirectory, "subdir");
        var configPath = Path.Combine(subdir, "config.json");
        var config = CreateValidConfiguration();

        // Act
        var result = _service.SaveConfiguration(configPath, config);

        // Assert
        result.Should().BeTrue();
        Directory.Exists(subdir).Should().BeTrue();
        File.Exists(configPath).Should().BeTrue();
    }

    [Fact]
    public void SaveConfiguration_WhenPathEmpty_ThrowsArgumentException()
    {
        // Arrange
        var config = CreateValidConfiguration();

        // Act & Assert
        var act = () => _service.SaveConfiguration("", config);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void SaveConfiguration_WhenConfigNull_ThrowsArgumentNullException()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "test.json");

        // Act & Assert
        var act = () => _service.SaveConfiguration(configPath, null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void SaveConfiguration_WhenInvalidConfig_ThrowsInvalidOperationException()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "test.json");
        var config = CreateValidConfiguration();
        config.Project.Version = "invalid-version"; // Version inválida

        // Act & Assert
        var act = () => _service.SaveConfiguration(configPath, config);
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*no es válida*");
    }

    [Fact]
    public void SaveConfiguration_UpdatesUpdatedDate()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "test.json");
        var config = CreateValidConfiguration();
        config.Project.Updated = "2020-01-01";

        // Act
        _service.SaveConfiguration(configPath, config);

        // Assert
        var savedConfig = _service.LoadConfiguration(configPath);
        savedConfig.Project.Updated.Should().NotBe("2020-01-01");
        savedConfig.Project.Updated.Should().MatchRegex(@"^\d{4}-\d{2}-\d{2}$");
    }

    [Fact]
    public void SaveConfiguration_WritesIndentedJson()
    {
        // Arrange
        var configPath = Path.Combine(_testDirectory, "test.json");
        var config = CreateValidConfiguration();

        // Act
        _service.SaveConfiguration(configPath, config);

        // Assert
        var json = File.ReadAllText(configPath);
        json.Should().Contain("  "); // Indentación
        json.Should().Contain("\n"); // Líneas nuevas
    }

    #endregion

    #region ValidateConfiguration Tests

    [Fact]
    public void ValidateConfiguration_WhenValid_ReturnsValid()
    {
        // Arrange
        var config = CreateValidConfiguration();

        // Act
        var result = _service.ValidateConfiguration(config);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void ValidateConfiguration_WhenNull_ReturnsInvalid()
    {
        // Act
        var result = _service.ValidateConfiguration(null!);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Message.Contains("null"));
    }

    [Fact]
    public void ValidateConfiguration_WhenProjectNameEmpty_ReturnsInvalid()
    {
        // Arrange
        var config = CreateValidConfiguration();
        config.Project.Name = "";

        // Act
        var result = _service.ValidateConfiguration(config);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Message.Contains("nombre del proyecto"));
    }

    [Fact]
    public void ValidateConfiguration_WhenProjectNameInvalid_ReturnsInvalid()
    {
        // Arrange
        var config = CreateValidConfiguration();
        config.Project.Name = "invalid/name*with@special";

        // Act
        var result = _service.ValidateConfiguration(config);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Message.Contains("caracteres inválidos"));
    }

    [Fact]
    public void ValidateConfiguration_WhenVersionInvalid_ReturnsInvalid()
    {
        // Arrange
        var config = CreateValidConfiguration();
        config.Project.Version = "not-a-version";

        // Act
        var result = _service.ValidateConfiguration(config);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Message.Contains("semver"));
    }

    [Fact]
    public void ValidateConfiguration_WhenDateInvalid_ReturnsInvalid()
    {
        // Arrange
        var config = CreateValidConfiguration();
        config.Project.Created = "2024/01/01"; // Formato incorrecto

        // Act
        var result = _service.ValidateConfiguration(config);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Message.Contains("ISO 8601"));
    }

    [Fact]
    public void ValidateConfiguration_WhenLanguageNotSupported_ReturnsInvalid()
    {
        // Arrange
        var config = CreateValidConfiguration();
        config.Language.ConversationLanguage = "de"; // Alemán no soportado

        // Act
        var result = _service.ValidateConfiguration(config);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Message.Contains("no soportado"));
    }

    [Fact]
    public void ValidateConfiguration_WhenGitHubEnabledWithoutRepo_ReturnsInvalid()
    {
        // Arrange
        var config = CreateValidConfiguration();
        config.GitHub.Enabled = true;
        config.GitHub.Repository = null;

        // Act
        var result = _service.ValidateConfiguration(config);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Message.Contains("repositorio"));
    }

    [Theory]
    [InlineData("1.0.0")]
    [InlineData("0.1.0")]
    [InlineData("10.20.30")]
    [InlineData("1.0.0-alpha")]
    [InlineData("1.0.0-beta.1")]
    [InlineData("1.0.0+20130313144700")]
    [InlineData("1.0.0-beta+exp.sha.5114f85")]
    public void ValidateConfiguration_WithValidSemver_ReturnsValid(string version)
    {
        // Arrange
        var config = CreateValidConfiguration();
        config.Project.Version = version;

        // Act
        var result = _service.ValidateConfiguration(config);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("es")]
    [InlineData("en")]
    [InlineData("pt")]
    [InlineData("fr")]
    public void ValidateConfiguration_WithSupportedLanguages_ReturnsValid(string language)
    {
        // Arrange
        var config = CreateValidConfiguration();
        config.Language.ConversationLanguage = language;

        // Act
        var result = _service.ValidateConfiguration(config);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    #endregion

    #region CreateDefaultConfiguration Tests

    [Fact]
    public void CreateDefaultConfiguration_WithValidProjectInfo_ReturnsValidConfig()
    {
        // Arrange
        var projectInfo = new ProjectInfo
        {
            Name = "my-project",
            Author = "@testuser",
            Framework = "net10.0"
        };

        // Act
        var config = _service.CreateDefaultConfiguration(projectInfo);

        // Assert
        config.Should().NotBeNull();
        config.Project.Name.Should().Be("my-project");
        config.Project.Author.Should().Be("@testuser");
        config.Project.Version.Should().Be("0.1.0");
        config.Sdk.Version.Should().Be("0.1.0");
        config.Language.ConversationLanguage.Should().Be("es");
        config.GitHub.Enabled.Should().BeFalse();
    }

    [Fact]
    public void CreateDefaultConfiguration_WhenProjectInfoNull_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => _service.CreateDefaultConfiguration(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CreateDefaultConfiguration_WhenProjectNameEmpty_ThrowsArgumentException()
    {
        // Arrange
        var projectInfo = new ProjectInfo { Name = "" };

        // Act & Assert
        var act = () => _service.CreateDefaultConfiguration(projectInfo);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateDefaultConfiguration_SetsCurrentDate()
    {
        // Arrange
        var projectInfo = new ProjectInfo { Name = "test" };

        // Act
        var config = _service.CreateDefaultConfiguration(projectInfo);

        // Assert
        config.Project.Created.Should().MatchRegex(@"^\d{4}-\d{2}-\d{2}$");
        config.Project.Updated.Should().MatchRegex(@"^\d{4}-\d{2}-\d{2}$");
        config.Project.Created.Should().Be(config.Project.Updated);
    }

    [Fact]
    public void CreateDefaultConfiguration_UsesDefaultsWhenOptionalFieldsNull()
    {
        // Arrange
        var projectInfo = new ProjectInfo { Name = "test" };

        // Act
        var config = _service.CreateDefaultConfiguration(projectInfo);

        // Assert
        config.Project.Language.Should().Be("csharp");
        config.Project.Framework.Should().Be("net10.0");
        config.Project.Author.Should().Be("@user");
    }

    #endregion

    #region MergeConfigurations Tests

    [Fact]
    public void MergeConfigurations_WhenOverridesNull_ReturnsBase()
    {
        // Arrange
        var baseConfig = CreateValidConfiguration();

        // Act
        var merged = _service.MergeConfigurations(baseConfig, null!);

        // Assert
        merged.Should().NotBeNull();
        merged.Project.Name.Should().Be(baseConfig.Project.Name);
    }

    [Fact]
    public void MergeConfigurations_WhenBaseNull_ThrowsArgumentNullException()
    {
        // Arrange
        var overrides = CreateValidConfiguration();

        // Act & Assert
        var act = () => _service.MergeConfigurations(null!, overrides);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void MergeConfigurations_OverridesProjectName()
    {
        // Arrange
        var baseConfig = CreateValidConfiguration();
        baseConfig.Project.Name = "original";

        var overrides = new SdkConfiguration
        {
            Project = new ProjectConfig { Name = "overridden" }
        };

        // Act
        var merged = _service.MergeConfigurations(baseConfig, overrides);

        // Assert
        merged.Project.Name.Should().Be("overridden");
    }

    [Fact]
    public void MergeConfigurations_PreservesNonOverriddenValues()
    {
        // Arrange
        var baseConfig = CreateValidConfiguration();
        baseConfig.Project.Name = "original";
        baseConfig.Project.Version = "1.0.0";

        var overrides = new SdkConfiguration
        {
            Project = new ProjectConfig { Version = "2.0.0" }
        };

        // Act
        var merged = _service.MergeConfigurations(baseConfig, overrides);

        // Assert
        merged.Project.Name.Should().Be("original"); // No fue overrideado
        merged.Project.Version.Should().Be("2.0.0"); // Fue overrideado
    }

    [Fact]
    public void MergeConfigurations_DoesNotModifyOriginal()
    {
        // Arrange
        var baseConfig = CreateValidConfiguration();
        var originalName = baseConfig.Project.Name;

        var overrides = new SdkConfiguration
        {
            Project = new ProjectConfig { Name = "different" }
        };

        // Act
        var merged = _service.MergeConfigurations(baseConfig, overrides);

        // Assert
        baseConfig.Project.Name.Should().Be(originalName); // Original no modificado
        merged.Project.Name.Should().Be("different");
    }

    [Fact]
    public void MergeConfigurations_MergesGitHubConfig()
    {
        // Arrange
        var baseConfig = CreateValidConfiguration();
        baseConfig.GitHub.Enabled = false;

        var overrides = new SdkConfiguration
        {
            GitHub = new GitHubConfig
            {
                Enabled = true,
                Repository = "owner/repo"
            }
        };

        // Act
        var merged = _service.MergeConfigurations(baseConfig, overrides);

        // Assert
        merged.GitHub.Enabled.Should().BeTrue();
        merged.GitHub.Repository.Should().Be("owner/repo");
    }

    #endregion

    #region FindConfigurationFile Tests

    [Fact]
    public void FindConfigurationFile_WhenExistsInCurrentDir_ReturnsPath()
    {
        // Arrange
        var sdkDir = Path.Combine(_testDirectory, ".mjcuadrado-net-sdk");
        Directory.CreateDirectory(sdkDir);
        var configPath = Path.Combine(sdkDir, "config.json");
        File.WriteAllText(configPath, "{}");

        // Act
        var found = _service.FindConfigurationFile(_testDirectory);

        // Assert
        found.Should().NotBeNull();
        found.Should().Be(configPath);
    }

    [Fact]
    public void FindConfigurationFile_WhenExistsInParentDir_ReturnsPath()
    {
        // Arrange
        var sdkDir = Path.Combine(_testDirectory, ".mjcuadrado-net-sdk");
        Directory.CreateDirectory(sdkDir);
        var configPath = Path.Combine(sdkDir, "config.json");
        File.WriteAllText(configPath, "{}");

        var subDir = Path.Combine(_testDirectory, "subdir", "nested");
        Directory.CreateDirectory(subDir);

        // Act
        var found = _service.FindConfigurationFile(subDir);

        // Assert
        found.Should().NotBeNull();
        found.Should().Be(configPath);
    }

    [Fact]
    public void FindConfigurationFile_WhenDoesNotExist_ReturnsNull()
    {
        // Act
        var found = _service.FindConfigurationFile(_testDirectory);

        // Assert
        found.Should().BeNull();
    }

    [Fact]
    public void FindConfigurationFile_WhenPathEmpty_UsesCurrentDirectory()
    {
        // Act
        var found = _service.FindConfigurationFile("");

        // Assert - No debería lanzar excepción
        // El resultado depende de si hay un config.json en el directorio actual del test runner
        // Ambos resultados (null o path válido) son aceptables aquí
        _ = found; // Simplemente verificar que no lanza excepción
    }

    [Fact]
    public void FindConfigurationFile_WhenDirectoryDoesNotExist_ReturnsNull()
    {
        // Arrange
        var nonExistentPath = Path.Combine(_testDirectory, "does-not-exist");

        // Act
        var found = _service.FindConfigurationFile(nonExistentPath);

        // Assert
        found.Should().BeNull();
    }

    #endregion

    #region Helper Methods

    private static SdkConfiguration CreateValidConfiguration()
    {
        return new SdkConfiguration
        {
            Project = new ProjectConfig
            {
                Name = "test-project",
                Version = "1.0.0",
                TemplateVersion = "0.1.0",
                Created = "2024-01-01",
                Updated = "2024-01-01",
                Language = "csharp",
                Framework = "net10.0",
                Mode = "personal",
                Author = "@testuser"
            },
            Sdk = new SdkConfig
            {
                Version = "0.1.0",
                MinDotNetVersion = "9.0.0"
            },
            Language = new LanguageConfig
            {
                ConversationLanguage = "es",
                ConversationLanguageName = "Spanish"
            },
            GitHub = new GitHubConfig
            {
                Enabled = false,
                Repository = null,
                AutoDeleteBranches = null
            },
            Optimization = new OptimizationConfig
            {
                LastSync = null,
                TemplateSynced = false
            }
        };
    }

    #endregion
}
