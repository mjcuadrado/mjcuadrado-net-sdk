# SPEC-FMT-055: Format Expert Agent

**SPEC ID:** SPEC-FMT-055
**Title:** Format Expert Agent
**Domain:** QUALITY
**Created:** 2024-11-24
**Author:** mjcuadrado
**Status:** draft
**Priority:** 🟡 Media
**Complexity:** medium
**Estimated Hours:** 24-30 hours
**Issue:** #55
**Tags:** @SPEC:FMT-055

---

## 📋 Resumen Ejecutivo

Implementar **Format Expert Agent** para automatizar code formatting y linting en proyectos .NET y React. El agente orchestrará herramientas de formatting (dotnet format, prettier) y linting (ESLint, StyleCop) para garantizar código consistente y cumplimiento de style guidelines.

**Entregables:**
- 1 agente: format-expert.md (~650 líneas)
- 1 comando: /mj2-format (~150 líneas)
- 3 skills: dotnet-format.md (~300), prettier.md (~250), eslint.md (~300)
- Integración con git workflow y pre-commit hooks
- Soporte para .cs, .ts, .tsx, .js, .jsx files

---

## 🎯 Objetivos

### Objetivo Principal
Automatizar code formatting y linting para garantizar consistencia de código en proyectos full-stack (.NET + React).

### Objetivos Específicos
1. **Formatting automatizado** - Aplicar formatting rules automáticamente
2. **Linting validation** - Validar código contra style guidelines
3. **Git integration** - Integrar con workflow git (staged files, pre-commit)
4. **Multi-language support** - Soporte para C#, TypeScript, JavaScript
5. **Configuration detection** - Auto-detectar archivos de configuración
6. **Performance** - Formatear solo archivos modificados

---

## 📐 Requirements (EARS Format)

### Functional Requirements

#### FR-1: Code Formatting Automation (Ubiquitous)
The system SHALL automatically format code files using:
- **dotnet format** para archivos .cs (C#)
- **Prettier** para archivos .ts, .tsx, .js, .jsx (TypeScript/JavaScript)
- Respetando configuración en .editorconfig, .prettierrc
- Preservando funcionalidad del código

**Acceptance Criteria:**
- Formatting aplicado correctamente a archivos C#
- Formatting aplicado correctamente a archivos TypeScript/JavaScript
- Configuración personalizada respetada
- Código funcional preservado (no breaking changes)

---

#### FR-2: Linting Validation (State-driven)
WHILE executing linting validation
THEN system SHALL validate code against style guidelines using:
- **StyleCop Analyzers** para C# (vía dotnet format)
- **ESLint** para TypeScript/JavaScript
- Reportando violaciones con ubicación y severidad
- Sugiriendo fixes automáticos cuando disponibles

**Acceptance Criteria:**
- Linting rules validadas para C#
- Linting rules validadas para TypeScript/JavaScript
- Violaciones reportadas con file:line:column
- Auto-fix suggestions proporcionadas

---

#### FR-3: Git Workflow Integration (Event-driven)
WHEN user requests formatting
THEN system SHALL support:
- `--staged` flag: Formatear solo staged files (pre-commit)
- `--check` flag: Verificar sin modificar (CI/CD)
- `--fix` flag: Auto-corregir violaciones
- Path targeting: Formatear specific files/directories

**Acceptance Criteria:**
- Staged files correctamente identificados y formateados
- Check mode no modifica archivos
- Fix mode corrige violaciones automáticamente
- Path targeting funciona correctamente

---

#### FR-4: Configuration Detection (Ubiquitous)
The system SHALL auto-detect formatting configuration from:
- **.editorconfig** (C# y TypeScript)
- **.prettierrc** / **.prettierrc.json** (TypeScript/JavaScript)
- **.eslintrc** / **.eslintrc.json** (TypeScript/JavaScript)
- Falling back to sensible defaults si no existen
- Logging configuration source used

**Acceptance Criteria:**
- .editorconfig detectado y aplicado
- .prettierrc detectado y aplicado
- .eslintrc detectado y aplicado
- Defaults applied when configs missing
- Configuration source logged

---

#### FR-5: Multi-Language Support (Ubiquitous)
The system SHALL support formatting para:
- **C# files** (.cs): dotnet format + StyleCop
- **TypeScript files** (.ts, .tsx): prettier + ESLint
- **JavaScript files** (.js, .jsx): prettier + ESLint
- Detecting file type automáticamente
- Applying appropriate tools per type

**Acceptance Criteria:**
- C# files formatted with dotnet format
- TypeScript files formatted with prettier + ESLint
- JavaScript files formatted with prettier + ESLint
- File type detection correcta
- Appropriate tools applied per type

---

#### FR-6: Format Expert Agent (Ubiquitous)
The system SHALL provide format-expert agent with:
- **Workflow:** ANALYZE → FORMAT → LINT → VALIDATE (4 fases)
- **Responsibilities:** 6 responsibilities (file detection, config loading, tool orchestration, validation, reporting, integration)
- **Integration:** Orquestrar dotnet-format, prettier, eslint skills
- **Output format:** "Mr. mj2 recomienda" style
- **Error handling:** Graceful handling de tool failures

**Acceptance Criteria:**
- Agent file exists (~650 líneas)
- 4-phase workflow implemented
- 6 responsibilities documented
- Skills integration working
- Output format consistent with other agents
- Error handling robust

---

#### FR-7: /mj2-format Command (Ubiquitous)
The system SHALL provide /mj2-format command with:
- **Syntax:** `/mj2:format [path] [--check|--fix|--staged]`
- **Options:**
  - `[path]` - Optional path to file/directory (default: .)
  - `--check` - Verify formatting without modifying
  - `--fix` - Auto-fix violations (default)
  - `--staged` - Format only staged files
- **Examples:** Comprehensive examples para cada option
- **Error handling:** Validation de options y paths

**Acceptance Criteria:**
- Command file exists (~150 líneas)
- All options supported
- Path parameter working
- Staged files detection working
- Examples comprehensive
- Error messages helpful

---

#### FR-8: Skills Creation (Ubiquitous)
The system SHALL provide 3 skills:
- **dotnet-format.md** (~300 líneas):
  - dotnet format CLI usage
  - .editorconfig integration
  - StyleCop Analyzers rules
  - Best practices
- **prettier.md** (~250 líneas):
  - Prettier configuration
  - .prettierrc options
  - Integration con TypeScript
  - Common patterns
- **eslint.md** (~300 líneas):
  - ESLint configuration
  - Rules for TypeScript
  - Auto-fix capabilities
  - Integration con prettier

**Acceptance Criteria:**
- dotnet-format.md exists (~300 líneas)
- prettier.md exists (~250 líneas)
- eslint.md exists (~300 líneas)
- All skills comprehensive
- Examples provided
- Best practices documented

---

### Non-Functional Requirements

#### NFR-1: Performance
The system SHALL:
- Format files in ≤ 5 seconds para proyectos pequeños (< 50 files)
- Format files in ≤ 30 seconds para proyectos medianos (< 500 files)
- Process only modified files cuando posible
- Use parallel processing when beneficial
- Cache configuration detection

**Rationale:** Users expect fast formatting, especially during pre-commit.

---

#### NFR-2: Usability
The system SHALL:
- Provide clear progress indicators durante formatting
- Report violations con file:line:column precision
- Suggest auto-fixes cuando disponibles
- Use consistent output format ("Mr. mj2 recomienda")
- Provide helpful error messages con resolution steps

**Rationale:** Clear feedback mejora developer experience.

---

#### NFR-3: Compatibility
The system SHALL be compatible with:
- **.NET 9+** (dotnet format requirements)
- **Node.js 18+** (prettier y ESLint requirements)
- **Git 2.30+** (staged files detection)
- **Windows, macOS, Linux** (cross-platform)
- **VS Code, Rider, Visual Studio** (IDE compatibility)

**Rationale:** Wide compatibility garantiza adoption.

---

#### NFR-4: Maintainability
The system SHALL:
- Follow TRUST 5 principles en agent design
- Use consistent formatting en documentation
- Provide comprehensive examples (3+ per skill)
- Document common pitfalls y solutions
- Include troubleshooting section

**Rationale:** Good documentation facilita mantenimiento.

---

## 🔗 Dependencies

### External Dependencies
- **dotnet CLI** (9.0+) - For dotnet format
- **Node.js** (18+) - For prettier y ESLint
- **npm/yarn** - Para instalar prettier y eslint
- **Git** (2.30+) - Para staged files detection

### Internal Dependencies
- **mj2 config system** - Para load project configuration
- **mj2 git workflow** - Para git integration
- **Pre-commit hooks** - Para auto-formatting integration

### Skills Dependencies
- **tools/dotnet-format.md** (nuevo)
- **tools/prettier.md** (nuevo)
- **tools/eslint.md** (nuevo)

---

## 🚫 Constraints

### Technical Constraints
1. **Tool availability** - Requiere dotnet CLI, Node.js installed
2. **Configuration files** - Requiere .editorconfig, .prettierrc, .eslintrc cuando custom rules needed
3. **Git repository** - Staged files detection solo funciona en git repos
4. **File encoding** - Assumes UTF-8 encoding

### Business Constraints
1. **Time** - 24-30 hours estimated (4-5 días)
2. **Scope** - Solo C# y TypeScript/JavaScript (no otros lenguajes)
3. **Integration** - No modifica existing pre-commit hooks setup

### User Constraints
1. **Learning curve** - Users deben entender formatting concepts
2. **Configuration** - Users pueden necesitar customizar config files
3. **Tool installation** - Users deben instalar dotnet CLI y Node.js

---

## 🎨 Examples

### Example 1: Format Entire Project
```bash
/mj2:format

✅ Formato aplicado a proyecto completo

📊 Formato Overview:
   - Archivos procesados: 127 files
   - C# files: 85 (dotnet format)
   - TypeScript files: 42 (prettier + ESLint)
   - Violaciones corregidas: 23 issues
   - Tiempo: 8.5s

🔧 Herramientas usadas:
   - dotnet format (.editorconfig detected)
   - prettier (.prettierrc detected)
   - ESLint (.eslintrc.json detected)

🤖 Mr. mj2 recomienda:
   1. Commit los cambios formateados
   2. Agregar format check al CI: /mj2:format --check
   3. Configurar pre-commit hook para auto-format
   4. Ver documentación: /mj2:help format
```

### Example 2: Check Formatting (CI Mode)
```bash
/mj2:format --check

❌ Formato issues encontrados

📊 Check Results:
   - Archivos verificados: 127 files
   - Violaciones: 5 issues found

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
   2. O corregir manualmente según reglas
   3. Re-run check después de fixes
```

### Example 3: Format Staged Files (Pre-commit)
```bash
/mj2:format --staged

✅ Staged files formateados

📊 Formato Overview:
   - Staged files: 3 files
   - UserService.cs (dotnet format) ✅
   - Button.tsx (prettier + ESLint) ✅
   - api.ts (prettier + ESLint) ✅
   - Violaciones corregidas: 7 issues
   - Tiempo: 1.2s

🤖 Mr. mj2 recomienda:
   1. Review los cambios: git diff
   2. Add los cambios formateados: git add .
   3. Commit: git commit -m "..."
   4. Tip: Agregar al pre-commit hook para auto-format
```

---

## ✅ Success Criteria

**Issue #55 está DONE cuando:**

1. ✅ **format-expert.md agent** creado (~650 líneas)
   - 4-phase workflow: ANALYZE → FORMAT → LINT → VALIDATE
   - 6 responsibilities documented
   - Skills integration (dotnet-format, prettier, eslint)
   - "Mr. mj2 recomienda" output format
   - Error handling robust

2. ✅ **/mj2-format command** creado (~150 líneas)
   - Syntax: `/mj2:format [path] [--check|--fix|--staged]`
   - All options working
   - Comprehensive examples

3. ✅ **3 skills creados** (~850 líneas total)
   - dotnet-format.md (~300 líneas)
   - prettier.md (~250 líneas)
   - eslint.md (~300 líneas)

4. ✅ **Formatting funciona** para:
   - C# files (.cs) con dotnet format
   - TypeScript files (.ts, .tsx) con prettier + ESLint
   - JavaScript files (.js, .jsx) con prettier + ESLint

5. ✅ **Git integration funciona**:
   - Staged files detection
   - Check mode (no modifications)
   - Fix mode (auto-corrections)

6. ✅ **Configuration detection funciona**:
   - .editorconfig auto-detected
   - .prettierrc auto-detected
   - .eslintrc auto-detected

7. ✅ **Performance aceptable**:
   - Proyectos pequeños: ≤ 5s
   - Proyectos medianos: ≤ 30s

8. ✅ **Documentation actualizada**:
   - README.md (25 agentes, 25 comandos)
   - ROADMAP.md (Issue #55 COMPLETADO)
   - CHANGELOG.md (entry para Issue #55)

9. ✅ **TAG chain completa**:
   - @SPEC:FMT-055
   - @CODE:FMT-055
   - @DOC:FMT-055

10. ✅ **Integration testing passed**:
    - Format C# project
    - Format TypeScript project
    - Format full-stack project
    - Check mode CI integration
    - Staged files workflow

---

## 🔍 Out of Scope

**NO incluido en este SPEC:**
1. **Otros lenguajes** - Python, Java, Go, etc.
2. **Custom formatters** - Solo dotnet format, prettier, ESLint
3. **IDE plugins** - Solo CLI tools
4. **Auto-save formatting** - Solo manual/pre-commit
5. **Cloud formatting** - Solo local execution

---

## 📚 References

- **Issue #55:** `.github/issues/issue-55.md`
- **moai-adk:** format-expert agent
- **dotnet format:** https://github.com/dotnet/format
- **Prettier:** https://prettier.io/
- **ESLint:** https://eslint.org/
- **StyleCop:** https://github.com/DotNetAnalyzers/StyleCopAnalyzers
- **.editorconfig:** https://editorconfig.org/

---

**Created:** 2024-11-24
**Status:** Draft
**Next:** Create plan.md and acceptance.md
