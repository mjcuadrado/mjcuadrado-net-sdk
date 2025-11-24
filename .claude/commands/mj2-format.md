---
name: mj2-format
description: Format and lint code with dotnet format, prettier, and ESLint
agent: format-expert
version: 1.0.0
author: mjcuadrado-net-sdk
tags: [mj2, formatting, linting, quality]
---

# /mj2:format - Code Formatting & Linting

Formatea y valida código usando **format-expert** agent que orchestro dotnet format, prettier, y ESLint.

---

## 🎯 Purpose

Automatizar code formatting y linting para garantizar:
- **Consistencia** - Mismo style en todo el codebase
- **Legibilidad** - Código fácil de leer y mantener
- **Calidad** - Catch issues temprano
- **Automation** - Zero-friction workflow

**Supports:**
- C# files (.cs) → dotnet format + StyleCop
- TypeScript files (.ts, .tsx) → prettier + ESLint
- JavaScript files (.js, .jsx) → prettier + ESLint

---

## 📋 Usage

```bash
/mj2:format [path] [OPTIONS]
```

### Arguments

**`[path]`** (optional)
- Path to file or directory to format
- Default: `.` (current directory)
- Examples: `src/`, `src/Services/UserService.cs`, `.`

### Options

**`--check`**
- Verify formatting without modifying files
- Exit code: 0 (pass), 1 (violations found)
- Use en CI/CD pipelines

**`--fix`** (default)
- Auto-fix formatting violations
- Modifies files in place
- Default behavior si no flag specified

**`--staged`**
- Format only git staged files
- Use en pre-commit hooks
- Only works in git repositories

---

## 🔄 Actions

### Action 1: Format Project (Default)

**Command:**
```bash
/mj2:format
```

**What it does:**
1. Detect all .cs, .ts, .tsx, .js, .jsx files en current directory
2. Load configurations (.editorconfig, .prettierrc, .eslintrc.json)
3. Run dotnet format para C# files
4. Run prettier para TypeScript/JavaScript files
5. Run ESLint para linting
6. Report summary de changes

**Output:**
```
✅ Formato aplicado exitosamente

📊 Formato Overview:
   - Archivos procesados: 127 files
   - Archivos modificados: 23 files
   - C# files: 85 (dotnet format)
   - TypeScript files: 42 (prettier + ESLint)
   - Violaciones corregidas: 12 issues
   - Tiempo: 5.2s

🔧 Herramientas usadas:
   - dotnet format (.editorconfig detected)
   - prettier (.prettierrc detected)
   - ESLint (.eslintrc.json detected)

🤖 Mr. mj2 recomienda:
   1. Review changes: git diff
   2. Commit formatted code: git add . && git commit -m "style: format code"
   3. Configure pre-commit hook: /mj2:format --staged
   4. Add CI check: /mj2:format --check

💡 Tip: Enable "format on save" en IDE para evitar formatting issues
```

---

### Action 2: Check Mode (CI)

**Command:**
```bash
/mj2:format --check
```

**What it does:**
1. Verify formatting sin modificar archivos
2. Report violations found
3. Exit con code 1 si violations found
4. Perfect para CI/CD validation

**Output (violations found):**
```
❌ Formatting issues encontrados

📊 Check Results:
   - Archivos verificados: 127 files
   - Archivos con issues: 5 files

⚠️  Issues Found:

src/Services/UserService.cs:45
   - Expected: 4 spaces indentation
   - Found: 2 spaces
   - Fix: /mj2:format src/Services/UserService.cs --fix

src/Components/Button.tsx:12
   - Expected: Single quotes
   - Found: Double quotes
   - Fix: /mj2:format src/Components/Button.tsx --fix

🤖 Mr. mj2 recomienda:
   1. Run: /mj2:format --fix (auto-correct all)
   2. O corregir manualmente según rules
   3. Re-run check después de fixes

💡 Tip: Configure IDE para format on save
```

**Exit codes:**
- `0` - All files formatted correctly
- `1` - Formatting violations found
- `2` - Error occurred (tool not found, config error, etc.)

---

### Action 3: Format Staged Files (Pre-commit)

**Command:**
```bash
/mj2:format --staged
```

**What it does:**
1. Detect git staged files (.cs, .ts, .tsx, .js, .jsx)
2. Format solo staged files
3. Re-stage modified files automáticamente
4. Perfect para pre-commit hooks

**Output:**
```
✅ Staged files formateados

📊 Formato Overview:
   - Staged files: 3 files
   - UserService.cs (dotnet format) ✓
   - Button.tsx (prettier + ESLint) ✓
   - api.ts (prettier + ESLint) ✓
   - Violaciones corregidas: 7 issues
   - Re-staged automáticamente
   - Tiempo: 1.2s

🤖 Mr. mj2 recomienda:
   1. Review changes: git diff --cached
   2. Commit: git commit -m "..."

💡 Tip: Add to pre-commit hook para auto-format antes de commit
```

---

### Action 4: Format Specific Path

**Command:**
```bash
/mj2:format src/Services --fix
```

**What it does:**
1. Format solo archivos en src/Services directory
2. Apply mismo workflow que full project
3. Faster para targeted formatting

**Output:**
```
✅ Formato aplicado a src/Services

📊 Formato Overview:
   - Archivos procesados: 12 files
   - Archivos modificados: 3 files
   - C# files: 12 (dotnet format)
   - Tiempo: 0.8s

🤖 Mr. mj2 recomienda:
   git add src/Services && git commit -m "style: format Services"
```

---

## 🎨 Examples

### Example 1: Format Before Commit

```bash
# Format all files
/mj2:format

# Review changes
git diff

# Commit
git add .
git commit -m "style: format codebase"
```

---

### Example 2: CI Pipeline Check

```yaml
# .github/workflows/ci.yml
- name: Check code formatting
  run: /mj2:format --check

# Falla si formatting needed
```

---

### Example 3: Pre-commit Hook

```bash
#!/bin/bash
# .git/hooks/pre-commit

# Format staged files
/mj2:format --staged

# Re-stage formatted files (ya hecho automáticamente)
```

---

### Example 4: Fix Specific File

```bash
# Format single file
/mj2:format src/Services/UserService.cs

# Output:
#   ✅ 1 file formatted
```

---

### Example 5: Check Specific Directory

```bash
# Check formatting en src/ directory
/mj2:format src/ --check

# Exit code indica si formatting needed
echo $?  # 0 = clean, 1 = needs formatting
```

---

## ✅ Best Practices

### 1. Format Before Committing

```bash
# Always run antes de commit
/mj2:format
git add .
git commit -m "feat: add feature"
```

**Benefits:**
- Avoid formatting commits separados
- Clean git history
- No formatting debates en PRs

---

### 2. Use --check in CI

```yaml
# .github/workflows/ci.yml
- run: /mj2:format --check
```

**Benefits:**
- Bloquea PRs con código no formateado
- Enforce formatting automáticamente
- Zero config maintenance

---

### 3. Configure Pre-commit Hook

```bash
# .git/hooks/pre-commit
#!/bin/bash
/mj2:format --staged
```

**Benefits:**
- Auto-format before every commit
- Never commit unformatted code
- Zero manual intervention

---

### 4. Format on Save in IDE

**VS Code:**
```json
// .vscode/settings.json
{
  "editor.formatOnSave": true,
  "editor.defaultFormatter": "esbenp.prettier-vscode",
  "[csharp]": {
    "editor.defaultFormatter": "ms-dotnettools.csharp"
  }
}
```

**Benefits:**
- Instant formatting feedback
- No separate format step needed
- Consistent with team

---

### 5. Review Changes Before Committing

```bash
# Format
/mj2:format

# Review ALL changes
git diff

# Stage only intended changes
git add -p

# Commit
git commit
```

**Benefits:**
- Catch unexpected formatting changes
- Understand what changed
- More intentional commits

---

## 🔗 Integration

### With Agents

**format-expert agent:**
- Orchestrates dotnet format, prettier, ESLint
- Handles configuration detection
- Reports violations

**quality-gate agent:**
- Calls /mj2:format --check during quality validation
- Includes formatting en quality metrics

**tdd-implementer agent:**
- Formats código después de test implementation
- Ensures formatted tests

---

### With Skills

**dotnet-format skill:**
- C# formatting rules
- .editorconfig configuration

**prettier skill:**
- TypeScript/JavaScript formatting
- .prettierrc configuration

**eslint skill:**
- Linting rules
- .eslintrc.json configuration

---

### With Hooks

**pre-commit:**
- Auto-format staged files
- `/mj2:format --staged`

**pre-push:**
- Validate formatting antes de push
- `/mj2:format --check`

---

## 🐛 Troubleshooting

### Issue: "dotnet CLI not found"

**Solution:**
```bash
# Install .NET SDK 9+
# https://dotnet.microsoft.com/download
dotnet --version
```

---

### Issue: "prettier not found"

**Solution:**
```bash
# Install prettier
npm install --save-dev prettier
```

---

### Issue: Formatting conflicts entre prettier y ESLint

**Solution:**
```bash
# Install eslint-config-prettier
npm install --save-dev eslint-config-prettier

# Add to .eslintrc.json extends (must be last!)
{
  "extends": ["...", "prettier"]
}
```

---

### Issue: "No files found to format"

**Solution:**
```bash
# Check que estás en directorio correcto
pwd

# Check que archivos existen
ls src/**/*.{cs,ts,tsx}

# Specify path explicitly
/mj2:format src/
```

---

### Issue: Performance lenta

**Solution:**
```bash
# Format solo changed files
/mj2:format --staged

# O format specific directory
/mj2:format src/Services
```

---

## 📚 Related Commands

- `/mj2:quality-check` - Run full quality validation (includes formatting check)
- `/mj2:2-run` - Implement con TDD (formats code automatically)
- `/mj2:status` - Ver estado del proyecto

---

**Agent:** format-expert
**Version:** 1.0.0
**Created:** 2024-11-24
**Tags:** @CODE:FMT-055
