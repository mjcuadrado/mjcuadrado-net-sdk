using FluentAssertions;
using MjCuadrado.NetSdk.Commands;
using NSubstitute;
using Spectre.Console.Cli;

namespace MjCuadrado.NetSdk.Tests.Commands;

/// <summary>
/// Tests para VersionCommand
/// </summary>
public class VersionCommandTests
{
    private readonly VersionCommand _command;

    public VersionCommandTests()
    {
        _command = new VersionCommand();
    }

    #region Execute Tests - Basic

    [Fact]
    public void Execute_WithoutVerbose_ReturnsZero()
    {
        // Arrange
        var settings = new VersionCommand.Settings { Verbose = false };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "version", null);

        // Act
        var exitCode = _command.Execute(context, settings);

        // Assert
        exitCode.Should().Be(0);
    }

    [Fact]
    public void Execute_WithVerbose_ReturnsZero()
    {
        // Arrange
        var settings = new VersionCommand.Settings { Verbose = true };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "version", null);

        // Act
        var exitCode = _command.Execute(context, settings);

        // Assert
        exitCode.Should().Be(0);
    }

    #endregion

    #region Settings Tests

    [Fact]
    public void Settings_DefaultVerbose_IsFalse()
    {
        // Arrange & Act
        var settings = new VersionCommand.Settings();

        // Assert
        settings.Verbose.Should().BeFalse();
    }

    [Fact]
    public void Settings_CanSetVerbose()
    {
        // Arrange & Act
        var settings = new VersionCommand.Settings { Verbose = true };

        // Assert
        settings.Verbose.Should().BeTrue();
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void Execute_AlwaysSucceeds()
    {
        // Arrange
        var settingsNonVerbose = new VersionCommand.Settings { Verbose = false };
        var settingsVerbose = new VersionCommand.Settings { Verbose = true };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "version", null);

        // Act
        var exitCodeNonVerbose = _command.Execute(context, settingsNonVerbose);
        var exitCodeVerbose = _command.Execute(context, settingsVerbose);

        // Assert
        exitCodeNonVerbose.Should().Be(0);
        exitCodeVerbose.Should().Be(0);
    }

    [Fact]
    public void Execute_DoesNotThrow()
    {
        // Arrange
        var settings = new VersionCommand.Settings { Verbose = true };
        var context = new CommandContext(Array.Empty<string>(), Substitute.For<IRemainingArguments>(), "version", null);

        // Act
        Action act = () => _command.Execute(context, settings);

        // Assert
        act.Should().NotThrow();
    }

    #endregion
}
