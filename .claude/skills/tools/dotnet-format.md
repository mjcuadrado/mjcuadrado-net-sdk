---
name: dotnet-format
description: .NET code formatting with dotnet format CLI tool
category: tools
difficulty: intermediate
version: 1.0.0
author: mjcuadrado-net-sdk
tags: [dotnet, formatting, cli, csharp, stylecop]
related_skills: [dotnet/csharp, dotnet/ef-core]
---

# dotnet format - .NET Code Formatting

**CLI tool para formatear código C# automáticamente según .editorconfig y StyleCop rules.**

---

## 📋 Overview

`dotnet format` es la herramienta oficial de Microsoft para formatear código .NET. Incluida en .NET SDK 6+, aplica:
- **Code style rules** desde .editorconfig
- **Analyzer rules** (StyleCop, FxCop, etc.)
- **Whitespace formatting** (indentation, spacing)

**Benefits:**
- Consistencia automática en codebase
- Integración con CI/CD
- Soporte para StyleCop Analyzers
- Cross-platform (Windows, macOS, Linux)

---

## 🚀 Installation

### Verificar Instalación

```bash
# dotnet format incluido en .NET SDK 6+
dotnet --version
# Output: 9.0.100 (o superior)

# Verificar dotnet format disponible
dotnet format --version
# Output: 9.0.100+abc123
```

### Requisitos

- **.NET SDK 9.0+** (recomendado)
- **.editorconfig** (opcional pero recomendado)
- **StyleCop.Analyzers** NuGet package (para reglas avanzadas)

---

## 💻 Basic Usage

### Format Entire Solution

```bash
# Format todos los proyectos en solution
dotnet format MySolution.sln

# Output:
#   Formatting code files in solution 'MySolution.sln'
#   Formatted 45 files in 2.3s
```

### Format Single Project

```bash
# Format proyecto específico
dotnet format MyProject.csproj

# Output:
#   Formatting code files in project 'MyProject.csproj'
#   Formatted 12 files in 0.8s
```

### Format Specific Folder

```bash
# Format todos los .cs en carpeta
dotnet format --include src/Services/

# Output:
#   Formatted 8 files in src/Services/
```

### Check Without Formatting (CI Mode)

```bash
# Verificar sin modificar archivos
dotnet format --verify-no-changes

# Exit code:
#   0 - No formatting needed
#   1 - Formatting needed
#   2 - Error occurred
```

---

## ⚙️ .editorconfig Integration

### Example .editorconfig

```ini
# .editorconfig
root = true

# All files
[*]
charset = utf-8
end_of_line = lf
insert_final_newline = true
trim_trailing_whitespace = true

# C# files
[*.cs]
indent_style = space
indent_size = 4

# C# Code Style Rules
csharp_new_line_before_open_brace = all
csharp_new_line_before_else = true
csharp_new_line_before_catch = true
csharp_new_line_before_finally = true

csharp_prefer_braces = true:warning
csharp_prefer_simple_using_statement = true:suggestion

# C# Formatting Rules
csharp_space_after_cast = false
csharp_space_after_keywords_in_control_flow_statements = true
csharp_space_between_method_declaration_parameter_list_parentheses = false

# Naming Conventions
dotnet_naming_rule.interface_should_be_begins_with_i.severity = warning
dotnet_naming_rule.interface_should_be_begins_with_i.symbols = interface
dotnet_naming_rule.interface_should_be_begins_with_i.style = begins_with_i

dotnet_naming_symbols.interface.applicable_kinds = interface
dotnet_naming_symbols.interface.applicable_accessibilities = public, internal, private, protected

dotnet_naming_style.begins_with_i.required_prefix = I
dotnet_naming_style.begins_with_i.capitalization = pascal_case

# Code Quality Rules
dotnet_diagnostic.CA1062.severity = warning  # Validate arguments
dotnet_diagnostic.CA1303.severity = none     # Don't pass literals as localized parameters
```

### Apply .editorconfig

```bash
# Format usando .editorconfig en directorio actual
dotnet format

# Format usando .editorconfig específico
dotnet format --include src/ --editorconfig /path/to/.editorconfig
```

---

## 🔬 StyleCop Analyzers Integration

### Install StyleCop Package

```xml
<!-- Add to .csproj -->
<ItemGroup>
  <PackageReference Include="StyleCop.Analyzers" Version="1.2.0-beta.556">
    <PrivateAssets>all</PrivateAssets>
    <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
  </PackageReference>
</ItemGroup>
```

### Configure StyleCop Rules

```json
// stylecop.json
{
  "$schema": "https://raw.githubusercontent.com/DotNetAnalyzers/StyleCopAnalyzers/master/StyleCop.Analyzers/StyleCop.Analyzers/Settings/stylecop.schema.json",
  "settings": {
    "documentationRules": {
      "companyName": "My Company",
      "copyrightText": "Copyright (c) {companyName}. All rights reserved."
    },
    "orderingRules": {
      "usingDirectivesPlacement": "outsideNamespace"
    }
  }
}
```

```xml
<!-- Include stylecop.json in .csproj -->
<ItemGroup>
  <AdditionalFiles Include="stylecop.json" />
</ItemGroup>
```

### Format with StyleCop Rules

```bash
# Format aplicando StyleCop rules
dotnet format --severity info

# Output:
#   SA1200: Using directive should appear within a namespace declaration
#   SA1633: File should have header
#   Formatted 10 files with 5 violations fixed
```

---

## 🛠️ Common Options

### Severity Levels

```bash
# Format solo errores
dotnet format --severity error

# Format warnings y errores
dotnet format --severity warn

# Format todo (info, suggestions, warnings, errors)
dotnet format --severity info
```

### Include/Exclude Paths

```bash
# Include specific paths
dotnet format --include src/Services/ --include src/Models/

# Exclude specific paths
dotnet format --exclude **/Migrations/ --exclude **/obj/

# Combine include y exclude
dotnet format --include src/ --exclude **/bin/ --exclude **/obj/
```

### Verbosity Levels

```bash
# Quiet output
dotnet format --verbosity quiet

# Normal output (default)
dotnet format --verbosity normal

# Detailed output
dotnet format --verbosity detailed

# Diagnostic output
dotnet format --verbosity diagnostic
```

### Report Generation

```bash
# Generate JSON report
dotnet format --report /path/to/report.json

# Report incluye:
# - Files formatted
# - Violations fixed
# - Time elapsed
```

---

## ✅ Best Practices

### 1. Use .editorconfig for Team Consistency

```bash
# .editorconfig en raíz del repo
# Versionado en git
# Compartido por todo el team
```

**Benefits:**
- Mismo formatting para todos los devs
- Consistencia cross-IDE (VS, Rider, VS Code)
- Single source of truth

### 2. Run in CI with --verify-no-changes

```yaml
# .github/workflows/ci.yml
- name: Check code formatting
  run: dotnet format --verify-no-changes --verbosity diagnostic
```

**Benefits:**
- Bloquea PRs con código no formateado
- Enforce formatting rules automáticamente
- No surprises en code reviews

### 3. Format Before Committing

```bash
# Pre-commit hook (.git/hooks/pre-commit)
#!/bin/bash
dotnet format --include $(git diff --cached --name-only --diff-filter=ACM | grep '\.cs$')
```

**Benefits:**
- Nunca commitear código no formateado
- Automatic formatting en workflow diario
- Reduce code review feedback

### 4. Use StyleCop for Advanced Rules

```bash
# Install StyleCop.Analyzers
# Configure en .editorconfig
# Run: dotnet format --severity info
```

**Benefits:**
- Enforce naming conventions
- Enforce documentation rules
- Enforce ordering rules

### 5. Integrate with IDE

**Visual Studio:**
- Tools → Options → Text Editor → C# → Code Style
- Import .editorconfig settings

**JetBrains Rider:**
- Settings → Editor → Code Style → C#
- Enable "Use .editorconfig"

**VS Code:**
- Install C# extension
- Enable "Format on Save"

---

## 🎯 Examples

### Example 1: Format Solution in CI

```bash
# CI script
dotnet restore MySolution.sln
dotnet format MySolution.sln --verify-no-changes --verbosity diagnostic

# If formatting needed (exit code 1):
echo "Code formatting errors found. Run 'dotnet format' locally."
exit 1
```

### Example 2: Format Only Changed Files

```bash
# Get changed .cs files desde main
CHANGED_FILES=$(git diff --name-only main...HEAD | grep '\.cs$')

# Format solo changed files
if [ -n "$CHANGED_FILES" ]; then
  dotnet format --include $CHANGED_FILES
fi
```

### Example 3: Format with Severity Filter

```bash
# Fix solo errors (no warnings/info)
dotnet format --severity error

# Output:
#   CS8600: Converting null literal to non-nullable type
#   Formatted 3 files
```

### Example 4: Generate Formatting Report

```bash
# Format y generate report
dotnet format --report format-report.json --verbosity diagnostic

# View report
cat format-report.json
```

```json
{
  "FormattedFiles": [
    "src/Services/UserService.cs",
    "src/Models/User.cs"
  ],
  "ViolationsFixed": 12,
  "TimeElapsed": "00:00:02.3456789"
}
```

### Example 5: Format Specific Project with Exclusions

```bash
# Format MyProject excluyendo Migrations y Generated
dotnet format MyProject.csproj \
  --exclude **/Migrations/** \
  --exclude **/Generated/** \
  --severity warn \
  --verbosity detailed
```

---

## 🐛 Troubleshooting

### Issue 1: "No project or solution file found"

**Error:**
```
Could not find a project or solution file in /path/to/directory
```

**Solution:**
```bash
# Specify solution/project explicitly
dotnet format MySolution.sln

# Or navigate to directory con .sln/.csproj
cd /path/to/project
dotnet format
```

---

### Issue 2: Formatting Conflicts with IDE

**Problem:** dotnet format results differ from IDE formatting.

**Solution:**
```bash
# Ensure IDE usa .editorconfig
# Visual Studio: Tools → Options → Text Editor → C# → Code Style
# Enable "Generate .editorconfig file from settings"

# O force IDE to use .editorconfig:
# Add <EditorConfigEnabled>true</EditorConfigEnabled> to .csproj
```

---

### Issue 3: StyleCop Rules Not Applied

**Problem:** StyleCop violations not fixed by dotnet format.

**Solution:**
```bash
# Ensure StyleCop.Analyzers installed
dotnet list package | grep StyleCop.Analyzers

# Format with --severity info (includes StyleCop)
dotnet format --severity info

# Check StyleCop rules en .editorconfig:
# dotnet_diagnostic.SA1200.severity = warning
```

---

### Issue 4: Performance Slow on Large Solution

**Problem:** dotnet format takes >30 seconds.

**Solution:**
```bash
# Format only changed files
git diff --name-only --diff-filter=ACM | grep '\.cs$' | xargs dotnet format --include

# Or exclude large generated folders
dotnet format --exclude **/obj/** --exclude **/bin/** --exclude **/Migrations/**
```

---

### Issue 5: Exit Code 1 Even After Formatting

**Problem:** `dotnet format --verify-no-changes` fails después de run `dotnet format`.

**Solution:**
```bash
# Possible causes:
# 1. Git line endings (CRLF vs LF)
#    - Fix: Ensure .editorconfig has end_of_line = lf
#    - Run: git config core.autocrlf false

# 2. IDE auto-formatting on save conflicts
#    - Fix: Disable IDE auto-format o sync con .editorconfig

# 3. File encoding issues
#    - Fix: Ensure .editorconfig has charset = utf-8
```

---

## 📚 Additional Resources

- **Official Docs:** https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format
- **EditorConfig:** https://editorconfig.org/
- **StyleCop Analyzers:** https://github.com/DotNetAnalyzers/StyleCopAnalyzers
- **Code Style Rules:** https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/

---

**Version:** 1.0.0
**Last Updated:** 2024-11-24
**Maintainer:** mjcuadrado-net-sdk
