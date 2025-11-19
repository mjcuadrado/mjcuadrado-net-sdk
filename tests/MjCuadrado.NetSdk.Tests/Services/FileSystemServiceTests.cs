using FluentAssertions;
using MjCuadrado.NetSdk.Services;

namespace MjCuadrado.NetSdk.Tests.Services;

public class FileSystemServiceTests : IDisposable
{
    private readonly FileSystemService _service;
    private readonly string _testDirectory;

    public FileSystemServiceTests()
    {
        _service = new FileSystemService();
        _testDirectory = Path.Combine(Path.GetTempPath(), $"mjcuadrado-test-{Guid.NewGuid()}");
        Directory.CreateDirectory(_testDirectory);
    }

    public void Dispose()
    {
        // Limpiar directorio de pruebas
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

    #region CreateDirectory Tests

    [Fact]
    public void CreateDirectory_WhenPathValid_CreatesDirectory()
    {
        // Arrange
        var testPath = Path.Combine(_testDirectory, "test-folder");

        // Act
        var result = _service.CreateDirectory(testPath);

        // Assert
        result.Should().BeTrue();
        Directory.Exists(testPath).Should().BeTrue();
    }

    [Fact]
    public void CreateDirectory_WhenDirectoryAlreadyExists_ReturnsTrue()
    {
        // Arrange
        var testPath = Path.Combine(_testDirectory, "existing-folder");
        Directory.CreateDirectory(testPath);

        // Act
        var result = _service.CreateDirectory(testPath);

        // Assert
        result.Should().BeTrue();
        Directory.Exists(testPath).Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreateDirectory_WhenPathEmpty_ThrowsArgumentException(string? path)
    {
        // Act & Assert
        var act = () => _service.CreateDirectory(path!);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateDirectory_WhenNestedPath_CreatesAllDirectories()
    {
        // Arrange
        var testPath = Path.Combine(_testDirectory, "level1", "level2", "level3");

        // Act
        var result = _service.CreateDirectory(testPath);

        // Assert
        result.Should().BeTrue();
        Directory.Exists(testPath).Should().BeTrue();
    }

    #endregion

    #region CreateDirectoryStructure Tests

    [Fact]
    public void CreateDirectoryStructure_CreatesAllFolders()
    {
        // Arrange
        var folders = new[] { "folder1", "folder2", "folder3" };

        // Act
        var result = _service.CreateDirectoryStructure(_testDirectory, folders);

        // Assert
        result.Should().BeTrue();
        foreach (var folder in folders)
        {
            var fullPath = Path.Combine(_testDirectory, folder);
            Directory.Exists(fullPath).Should().BeTrue();
        }
    }

    [Fact]
    public void CreateDirectoryStructure_WithNestedFolders_CreatesAllFolders()
    {
        // Arrange
        var folders = new[] { "a/b/c", "x/y/z" };

        // Act
        var result = _service.CreateDirectoryStructure(_testDirectory, folders);

        // Assert
        result.Should().BeTrue();
        Directory.Exists(Path.Combine(_testDirectory, "a", "b", "c")).Should().BeTrue();
        Directory.Exists(Path.Combine(_testDirectory, "x", "y", "z")).Should().BeTrue();
    }

    [Fact]
    public void CreateDirectoryStructure_WhenBasePathEmpty_ThrowsArgumentException()
    {
        // Act & Assert
        var act = () => _service.CreateDirectoryStructure("", new[] { "folder1" });
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateDirectoryStructure_WhenFoldersNull_ThrowsArgumentException()
    {
        // Act & Assert
        var act = () => _service.CreateDirectoryStructure(_testDirectory, null!);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateDirectoryStructure_WhenFoldersEmpty_ThrowsArgumentException()
    {
        // Act & Assert
        var act = () => _service.CreateDirectoryStructure(_testDirectory, Array.Empty<string>());
        act.Should().Throw<ArgumentException>();
    }

    #endregion

    #region FileExists Tests

    [Fact]
    public void FileExists_WhenFileExists_ReturnsTrue()
    {
        // Arrange
        var testFile = Path.Combine(_testDirectory, "test.txt");
        File.WriteAllText(testFile, "test content");

        // Act
        var result = _service.FileExists(testFile);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void FileExists_WhenFileDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var testFile = Path.Combine(_testDirectory, "nonexistent.txt");

        // Act
        var result = _service.FileExists(testFile);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void FileExists_WhenPathEmpty_ReturnsFalse(string? path)
    {
        // Act
        var result = _service.FileExists(path!);

        // Assert
        result.Should().BeFalse();
    }

    #endregion

    #region DirectoryExists Tests

    [Fact]
    public void DirectoryExists_WhenDirectoryExists_ReturnsTrue()
    {
        // Arrange
        var testDir = Path.Combine(_testDirectory, "existing-dir");
        Directory.CreateDirectory(testDir);

        // Act
        var result = _service.DirectoryExists(testDir);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void DirectoryExists_WhenDirectoryDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var testDir = Path.Combine(_testDirectory, "nonexistent-dir");

        // Act
        var result = _service.DirectoryExists(testDir);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void DirectoryExists_ReturnsCorrectValue()
    {
        // Arrange
        var existingDir = Path.Combine(_testDirectory, "exists");
        var nonExistingDir = Path.Combine(_testDirectory, "does-not-exist");
        Directory.CreateDirectory(existingDir);

        // Act & Assert
        _service.DirectoryExists(existingDir).Should().BeTrue();
        _service.DirectoryExists(nonExistingDir).Should().BeFalse();
    }

    #endregion

    #region CopyFile Tests

    [Fact]
    public void CopyFile_WhenSourceExists_CopiesFile()
    {
        // Arrange
        var sourceFile = Path.Combine(_testDirectory, "source.txt");
        var destFile = Path.Combine(_testDirectory, "dest.txt");
        var content = "test content";
        File.WriteAllText(sourceFile, content);

        // Act
        var result = _service.CopyFile(sourceFile, destFile);

        // Assert
        result.Should().BeTrue();
        File.Exists(destFile).Should().BeTrue();
        File.ReadAllText(destFile).Should().Be(content);
    }

    [Fact]
    public void CopyFile_WhenDestinationDirectoryDoesNotExist_CreatesItAndCopies()
    {
        // Arrange
        var sourceFile = Path.Combine(_testDirectory, "source.txt");
        var destFile = Path.Combine(_testDirectory, "subdir", "dest.txt");
        File.WriteAllText(sourceFile, "test");

        // Act
        var result = _service.CopyFile(sourceFile, destFile);

        // Assert
        result.Should().BeTrue();
        File.Exists(destFile).Should().BeTrue();
    }

    [Fact]
    public void CopyFile_WhenSourceDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var sourceFile = Path.Combine(_testDirectory, "nonexistent.txt");
        var destFile = Path.Combine(_testDirectory, "dest.txt");

        // Act & Assert
        var act = () => _service.CopyFile(sourceFile, destFile);
        act.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void CopyFile_WhenSourceEmpty_ThrowsArgumentException()
    {
        // Act & Assert
        var act = () => _service.CopyFile("", "dest.txt");
        act.Should().Throw<ArgumentException>();
    }

    #endregion

    #region WriteTextFile Tests

    [Fact]
    public void WriteTextFile_CreatesFileWithContent()
    {
        // Arrange
        var testFile = Path.Combine(_testDirectory, "test.txt");
        var content = "Hello World!";

        // Act
        var result = _service.WriteTextFile(testFile, content);

        // Assert
        result.Should().BeTrue();
        File.Exists(testFile).Should().BeTrue();
        File.ReadAllText(testFile).Should().Be(content);
    }

    [Fact]
    public void WriteTextFile_WhenDirectoryDoesNotExist_CreatesDirectoryAndFile()
    {
        // Arrange
        var testFile = Path.Combine(_testDirectory, "subdir", "test.txt");
        var content = "test";

        // Act
        var result = _service.WriteTextFile(testFile, content);

        // Assert
        result.Should().BeTrue();
        File.Exists(testFile).Should().BeTrue();
    }

    [Fact]
    public void WriteTextFile_WhenFileExists_OverwritesContent()
    {
        // Arrange
        var testFile = Path.Combine(_testDirectory, "test.txt");
        File.WriteAllText(testFile, "old content");
        var newContent = "new content";

        // Act
        var result = _service.WriteTextFile(testFile, newContent);

        // Assert
        result.Should().BeTrue();
        File.ReadAllText(testFile).Should().Be(newContent);
    }

    [Fact]
    public void WriteTextFile_WhenPathEmpty_ThrowsArgumentException()
    {
        // Act & Assert
        var act = () => _service.WriteTextFile("", "content");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void WriteTextFile_WhenContentNull_ThrowsArgumentNullException()
    {
        // Arrange
        var testFile = Path.Combine(_testDirectory, "test.txt");

        // Act & Assert
        var act = () => _service.WriteTextFile(testFile, null!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region ReadTextFile Tests

    [Fact]
    public void ReadTextFile_WhenFileExists_ReturnsContent()
    {
        // Arrange
        var testFile = Path.Combine(_testDirectory, "test.txt");
        var content = "test content";
        File.WriteAllText(testFile, content);

        // Act
        var result = _service.ReadTextFile(testFile);

        // Assert
        result.Should().Be(content);
    }

    [Fact]
    public void ReadTextFile_WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var testFile = Path.Combine(_testDirectory, "nonexistent.txt");

        // Act & Assert
        var act = () => _service.ReadTextFile(testFile);
        act.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void ReadTextFile_WhenPathEmpty_ThrowsArgumentException()
    {
        // Act & Assert
        var act = () => _service.ReadTextFile("");
        act.Should().Throw<ArgumentException>();
    }

    #endregion

    #region GetCurrentDirectory Tests

    [Fact]
    public void GetCurrentDirectory_ReturnsValidPath()
    {
        // Act
        var result = _service.GetCurrentDirectory();

        // Assert
        result.Should().NotBeNullOrEmpty();
        Directory.Exists(result).Should().BeTrue();
    }

    #endregion

    #region EnsureDirectoryExists Tests

    [Fact]
    public void EnsureDirectoryExists_WhenDirectoryDoesNotExist_CreatesIt()
    {
        // Arrange
        var testDir = Path.Combine(_testDirectory, "new-dir");

        // Act
        var result = _service.EnsureDirectoryExists(testDir);

        // Assert
        result.Should().BeTrue();
        Directory.Exists(testDir).Should().BeTrue();
    }

    [Fact]
    public void EnsureDirectoryExists_WhenDirectoryExists_ReturnsTrue()
    {
        // Arrange
        var testDir = Path.Combine(_testDirectory, "existing-dir");
        Directory.CreateDirectory(testDir);

        // Act
        var result = _service.EnsureDirectoryExists(testDir);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void EnsureDirectoryExists_WhenPathEmpty_ReturnsFalse()
    {
        // Act
        var result = _service.EnsureDirectoryExists("");

        // Assert
        result.Should().BeFalse();
    }

    #endregion

    #region HasWritePermissions Tests

    [Fact]
    public void HasWritePermissions_WhenDirectoryWritable_ReturnsTrue()
    {
        // Act
        var result = _service.HasWritePermissions(_testDirectory);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void HasWritePermissions_WhenPathEmpty_ReturnsFalse()
    {
        // Act
        var result = _service.HasWritePermissions("");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void HasWritePermissions_WhenDirectoryDoesNotExist_ChecksParentDirectory()
    {
        // Arrange
        var nonExistentDir = Path.Combine(_testDirectory, "nonexistent");

        // Act
        var result = _service.HasWritePermissions(nonExistentDir);

        // Assert
        // Should check parent directory (_testDirectory) which is writable
        result.Should().BeTrue();
    }

    #endregion

    #region GetAvailableDiskSpace Tests

    [Fact]
    public void GetAvailableDiskSpace_ReturnsPositiveValue()
    {
        // Act
        var result = _service.GetAvailableDiskSpace(_testDirectory);

        // Assert
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void GetAvailableDiskSpace_WhenPathEmpty_UsesCurrentDirectory()
    {
        // Act
        var result = _service.GetAvailableDiskSpace("");

        // Assert
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public void GetAvailableDiskSpace_WhenPathIsFile_ReturnsSpaceForParentDirectory()
    {
        // Arrange
        var testFile = Path.Combine(_testDirectory, "test.txt");
        File.WriteAllText(testFile, "test");

        // Act
        var result = _service.GetAvailableDiskSpace(testFile);

        // Assert
        result.Should().BeGreaterThan(0);
    }

    #endregion

    #region Path Normalization Tests

    [Fact]
    public void CreateDirectory_WithBackslashes_WorksOnAllPlatforms()
    {
        // Arrange
        var testPath = Path.Combine(_testDirectory, "test\\with\\backslashes");

        // Act
        var result = _service.CreateDirectory(testPath);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void CreateDirectory_WithForwardSlashes_WorksOnAllPlatforms()
    {
        // Arrange
        var testPath = _testDirectory + "/test/with/forward/slashes";

        // Act
        var result = _service.CreateDirectory(testPath);

        // Assert
        result.Should().BeTrue();
    }

    #endregion
}
