using FluentAssertions;
using MjCuadrado.NetSdk.Commands;
using MjCuadrado.NetSdk.Services;
using NSubstitute;
using Spectre.Console.Cli;

namespace MjCuadrado.NetSdk.Tests.Commands;

/// <summary>
/// Tests para DoctorCommand
/// </summary>
public class DoctorCommandTests
{
    private readonly IDoctorService _doctorService;
    private readonly DoctorCommand _command;

    public DoctorCommandTests()
    {
        _doctorService = Substitute.For<IDoctorService>();
        _command = new DoctorCommand(_doctorService);
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_WithNullDoctorService_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => new DoctorCommand(null!);
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("doctorService");
    }

    #endregion

    #region Execute Tests - Success Cases

    [Fact]
    public void Execute_WithAllChecksPassed_ReturnsZero()
    {
        // Arrange
        var settings = new DoctorCommand.Settings { Verbose = false };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "doctor", null);

        var result = new DiagnosticResult();
        result.Checks.Add(new DiagnosticCheck
        {
            Name = ".NET SDK",
            Success = true,
            Message = "Installed",
            Details = "10.0.100"
        });
        result.Checks.Add(new DiagnosticCheck
        {
            Name = "Git",
            Success = true,
            Message = "Installed",
            Details = "2.39.3"
        });
        result.Checks.Add(new DiagnosticCheck
        {
            Name = "Project Structure",
            Success = true,
            Message = "Complete",
            Details = "All folders present"
        });
        result.Checks.Add(new DiagnosticCheck
        {
            Name = "Disk Space",
            Success = true,
            Message = "Sufficient",
            Details = "200.00 MB available"
        });
        result.Checks.Add(new DiagnosticCheck
        {
            Name = "Write Permissions",
            Success = true,
            Message = "OK",
            Details = "Can write to current directory"
        });

        _doctorService.RunFullDiagnostic().Returns(result);

        // Act
        var exitCode = _command.Execute(context, settings);

        // Assert
        exitCode.Should().Be(0);
        _doctorService.Received(1).RunFullDiagnostic();
    }

    [Fact]
    public void Execute_WithVerboseFlag_CallsDiagnostic()
    {
        // Arrange
        var settings = new DoctorCommand.Settings { Verbose = true };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "doctor", null);

        var result = new DiagnosticResult();
        result.Checks.Add(new DiagnosticCheck
        {
            Name = ".NET SDK",
            Success = true,
            Message = "Installed",
            Details = "10.0.100"
        });

        _doctorService.RunFullDiagnostic().Returns(result);

        // Act
        var exitCode = _command.Execute(context, settings);

        // Assert
        exitCode.Should().Be(0);
        _doctorService.Received(1).RunFullDiagnostic();
    }

    #endregion

    #region Execute Tests - Failure Cases

    [Fact]
    public void Execute_WithFailedChecks_ReturnsOne()
    {
        // Arrange
        var settings = new DoctorCommand.Settings { Verbose = false };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "doctor", null);

        var result = new DiagnosticResult();
        result.Checks.Add(new DiagnosticCheck
        {
            Name = ".NET SDK",
            Success = false,
            Message = "Not found",
            Details = "Not installed"
        });
        result.Checks.Add(new DiagnosticCheck
        {
            Name = "Git",
            Success = true,
            Message = "Installed",
            Details = "2.39.3"
        });
        result.Suggestions.Add("Install .NET SDK 9.0 or higher");

        _doctorService.RunFullDiagnostic().Returns(result);

        // Act
        var exitCode = _command.Execute(context, settings);

        // Assert
        exitCode.Should().Be(1);
        _doctorService.Received(1).RunFullDiagnostic();
    }

    [Fact]
    public void Execute_WithWarnings_ReturnsAppropriateCode()
    {
        // Arrange
        var settings = new DoctorCommand.Settings { Verbose = false };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "doctor", null);

        var result = new DiagnosticResult();
        result.Checks.Add(new DiagnosticCheck
        {
            Name = ".NET SDK",
            Success = true,
            Message = "Installed",
            Details = "10.0.100"
        });
        result.Checks.Add(new DiagnosticCheck
        {
            Name = "Git",
            Success = true,
            Message = "Installed",
            Details = "2.39.3"
        });
        result.Warnings.Add("Git is not configured");
        result.Suggestions.Add("Configure Git with user.name and user.email");

        _doctorService.RunFullDiagnostic().Returns(result);

        // Act
        var exitCode = _command.Execute(context, settings);

        // Assert
        exitCode.Should().Be(0); // Warnings no causan failure
        _doctorService.Received(1).RunFullDiagnostic();
    }

    [Fact]
    public void Execute_WithException_ReturnsOne()
    {
        // Arrange
        var settings = new DoctorCommand.Settings { Verbose = false };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "doctor", null);

        _doctorService.When(x => x.RunFullDiagnostic())
            .Do(_ => throw new InvalidOperationException("Test exception"));

        // Act
        var exitCode = _command.Execute(context, settings);

        // Assert
        exitCode.Should().Be(1);
    }

    #endregion

    #region Settings Tests

    [Fact]
    public void Settings_DefaultVerbose_IsFalse()
    {
        // Arrange & Act
        var settings = new DoctorCommand.Settings();

        // Assert
        settings.Verbose.Should().BeFalse();
    }

    [Fact]
    public void Settings_CanSetVerbose()
    {
        // Arrange & Act
        var settings = new DoctorCommand.Settings { Verbose = true };

        // Assert
        settings.Verbose.Should().BeTrue();
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void Execute_CompleteWorkflow_CallsServiceAndProcessesResult()
    {
        // Arrange
        var settings = new DoctorCommand.Settings { Verbose = false };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "doctor", null);

        var result = new DiagnosticResult();
        result.Checks.Add(new DiagnosticCheck
        {
            Name = ".NET SDK",
            Success = true,
            Message = "Installed",
            Details = "10.0.100"
        });
        result.Checks.Add(new DiagnosticCheck
        {
            Name = "Git",
            Success = false,
            Message = "Not found",
            Details = "Not installed"
        });
        result.Checks.Add(new DiagnosticCheck
        {
            Name = "Project Structure",
            Success = false,
            Message = "2 items missing",
            Details = ".mjcuadrado-net-sdk/, config.json"
        });
        result.Warnings.Add("Missing project structure");
        result.Suggestions.Add("Initialize project with: mjcuadrado-net-sdk init");
        result.Suggestions.Add("Install Git from https://git-scm.com/downloads");

        _doctorService.RunFullDiagnostic().Returns(result);

        // Act
        var exitCode = _command.Execute(context, settings);

        // Assert
        exitCode.Should().Be(1);
        _doctorService.Received(1).RunFullDiagnostic();

        // Verificar que se procesaron warnings y suggestions
        result.Warnings.Should().HaveCount(1);
        result.Suggestions.Should().HaveCount(2);
    }

    #endregion
}
