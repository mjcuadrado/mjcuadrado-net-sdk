# Issue #20: Git Hooks

**Status:** ✅ Closed
**Created:** 2024-11-21
**Closed:** 2024-11-21
**Purpose:** Automated validation
**Commit:** a8787c2

---

## Objetivo

Crear Git Hooks para validación automática en commits y pushes, asegurando calidad de código y formato consistente.

---

## Archivos Creados

### 1. install-hooks.sh (234 líneas)

**Ubicación:** `.claude/scripts/install-hooks.sh`

**Propósito:**
Instalador de Git Hooks que crea 3 hooks en `.git/hooks/`

**Contenido:**
- Hook pre-commit completo (embedded)
- Hook commit-msg completo (embedded)
- Hook pre-push completo (embedded)
- Verificación de repositorio git
- Creación de directorio hooks
- Permisos ejecutables automáticos
- Output informativo

**Uso:**
```bash
./.claude/scripts/install-hooks.sh
```

**Output:**
```
🔧 Installing MJ² Git Hooks...

✅ Hooks installed successfully

Installed hooks:
  • pre-commit  - Format & build check
  • commit-msg  - Message format validation
  • pre-push    - Tests & coverage check

To bypass hooks (emergency only):
  git commit --no-verify
  git push --no-verify
```

---

### 2. HOOKS.md (377 líneas)

**Ubicación:** `.claude/scripts/HOOKS.md`

**Propósito:**
Documentación completa de Git Hooks

**Secciones:**
1. **Installation** - Cómo instalar hooks
2. **Hooks** - Descripción de cada hook:
   - pre-commit
   - commit-msg
   - pre-push
3. **Bypass Hooks** - Cuándo y cómo usar --no-verify
4. **Troubleshooting** - Soluciones a problemas comunes
5. **Best Practices** - DO vs DON'T
6. **Referencias** - Links externos

**Contenido detallado por hook:**
- Validaciones ejecutadas
- Flujo de ejecución
- Ejemplos de success y failure
- Cómo fix errores
- Cuándo bypass

---

## Hooks Implementados

### 1. pre-commit

**Ejecuta:** Antes de cada commit

**Validaciones:**
1. ✅ **Code format**
   ```bash
   dotnet format --verify-no-changes --verbosity quiet
   ```
   - Verifica que el código esté formateado
   - Falla si hay cambios de formato necesarios
   - Sugiere: `dotnet format`

2. ✅ **Build success**
   ```bash
   dotnet build --nologo --verbosity quiet
   ```
   - Verifica que el proyecto compile
   - Falla si hay errores de compilación

3. ⚠️ **TODOs detection** (warning only)
   ```bash
   git diff --cached --name-only | grep "\.cs$" | xargs grep -n "TODO\|FIXME"
   ```
   - Lista TODOs en archivos staged
   - NO bloquea el commit
   - Solo información

**Graceful degradation:**
- Si dotnet no está instalado: skip con warning
- Continúa con siguiente validación

**Flujo success:**
```
🔍 Running pre-commit checks...
📝 Checking code format...
✅ Format check passed
🔨 Building project...
✅ Build passed
✅ Pre-commit checks passed
```

**Flujo failure:**
```
📝 Checking code format...
❌ Format check failed
💡 Run: dotnet format
```

---

### 2. commit-msg

**Ejecuta:** Después de escribir mensaje de commit

**Validaciones:**
1. ✅ **Message format**
   ```
   <emoji> <type>(SPEC-ID): <description>
   ```
   o
   ```
   <emoji> <type>: <description>  (para chore, style, build, ci)
   ```

**Emojis válidos:**
| Emoji | Tipo | Phase/Purpose |
|-------|------|---------------|
| 🔴 | test | RED phase (failing tests) |
| 🟢 | feat | GREEN phase (implementation) |
| ♻️ | refactor | REFACTOR phase (quality) |
| 📚 | docs | Documentation |
| 🐛 | fix | Bug fix |
| ✨ | NEW | New feature (non-TDD) |
| 🔧 | chore | Maintenance |
| ⚡ | perf | Performance |
| 📦 | build | Build system |
| 🎨 | style | Code style |

**Types válidos:**
- `test` - Tests
- `feat` - Features
- `refactor` - Refactoring
- `docs` - Documentation
- `fix` - Bug fixes
- `chore` - Maintenance
- `style` - Formatting
- `perf` - Performance
- `build` - Build system
- `ci` - CI/CD
- `spec` - Specifications

**SPEC-ID format:**
- Pattern: `[A-Z]+-[0-9]+`
- Examples: `AUTH-001`, `USER-003`, `API-012`
- Opcional para: chore, style, build, ci

**Auto-skip:**
- Merge commits (`^Merge`)
- Claude Code commits (contienen `🤖 Generated with [Claude Code]`)

**Ejemplos válidos ✅:**
```bash
✅ 🔴 test(AUTH-001): add failing tests for login
✅ 🟢 feat(AUTH-001): implement auth service
✅ ♻️ refactor(AUTH-001): improve code quality
✅ 📚 docs(AUTH-001): sync documentation
✅ 🐛 fix(AUTH-001): correct token expiration
✅ 🔧 chore: update dependencies
✅ 🎨 style: format code
```

**Ejemplos inválidos ❌:**
```bash
❌ "implemented feature"           # No format
❌ "feat: add login"                # No emoji
❌ "🟢 implemented login"          # No type
❌ "🟢 feat add login"             # Missing colon
❌ "🟢 feat(AUTH001): add login"   # SPEC-ID sin guion
```

**Flujo failure:**
```
🔍 Validating commit message...
❌ Invalid commit message format

Expected format:
  <emoji> <type>(SPEC-ID): <description>

Your message:
  bad message
```

---

### 3. pre-push

**Ejecuta:** Antes de push a remote

**Validaciones:**
1. ✅ **All tests pass**
   ```bash
   dotnet test --nologo --verbosity quiet
   ```
   - Ejecuta todos los tests
   - Falla si algún test falla
   - Sugiere: Fix tests before pushing

2. ✅ **Coverage ≥85%**
   ```bash
   dotnet test --collect:"XPlat Code Coverage"
   ```
   - Genera coverage report
   - Parsea coverage.cobertura.xml
   - Extrae line-rate
   - Calcula porcentaje
   - Falla si <85%
   - Sugiere: Add more tests

3. ✅ **No merge conflicts**
   ```bash
   find src/ tests/ -type f -name "*.cs" -exec grep -l "<<<<<<< HEAD" {} \;
   ```
   - Busca conflict markers
   - Falla si encuentra alguno
   - Sugiere: Resolve conflicts

**Graceful degradation:**
- Si dotnet no está instalado: skip con warning
- Si coverage report no se encuentra: skip con warning
- Si no puede parsear coverage: skip con warning

**Flujo success:**
```
🔍 Running pre-push checks...
🧪 Running tests...
✅ All tests passed
📊 Checking coverage...
✅ Coverage: 87% (≥85%)
✅ Pre-push checks passed
```

**Flujo failure (tests):**
```
🧪 Running tests...
❌ Tests failed
💡 Fix tests before pushing
```

**Flujo failure (coverage):**
```
📊 Checking coverage...
❌ Coverage too low: 78% (need ≥85%)
💡 Add more tests to increase coverage
```

---

## Bypass Hooks

### --no-verify flag

**Usage:**
```bash
# Skip pre-commit y commit-msg
git commit --no-verify -m "WIP: work in progress"

# Skip pre-push
git push --no-verify origin feature-branch
```

### Cuándo usar ✅

**Casos válidos:**
1. **Work in progress** - Commit parcial para backup
   ```bash
   git commit --no-verify -m "WIP: implementing feature"
   ```

2. **Emergency hotfix** - Fix crítico en producción
   ```bash
   git commit --no-verify -m "HOTFIX: critical security patch"
   git push --no-verify origin main
   ```

3. **CI/CD only** - Dejar que CI valide
   ```bash
   git push --no-verify origin feature-branch
   # CI ejecutará validaciones
   ```

4. **Non-.NET project** - Proyecto sin dotnet
   ```bash
   git commit --no-verify -m "docs: update README"
   ```

### Cuándo NO usar ❌

**NUNCA bypass en:**
1. ❌ Merge a main/master
2. ❌ Production releases
3. ❌ Team collaboration (PR)
4. ❌ Final implementation
5. ❌ Regularmente (indica problema)

---

## Troubleshooting

### Hook no ejecuta

**Síntomas:**
- Hook no se ejecuta
- Commit/push pasa sin validación

**Solución:**
```bash
# Verificar permisos
ls -la .git/hooks/

# Debe mostrar -rwxr-xr-x (ejecutable)
# Si no:
chmod +x .git/hooks/pre-commit
chmod +x .git/hooks/commit-msg
chmod +x .git/hooks/pre-push

# Verificar que existen
ls .git/hooks/pre-commit .git/hooks/commit-msg .git/hooks/pre-push
```

### Format check falla

**Síntomas:**
```
❌ Format check failed
💡 Run: dotnet format
```

**Solución:**
```bash
# Auto-format código
dotnet format

# Revisar cambios
git diff

# Agregar y commit
git add .
git commit -m "🎨 style: format code"
```

### Build falla

**Síntomas:**
```
❌ Build failed
```

**Solución:**
```bash
# Build con output completo
dotnet build

# Revisar errores
# Fix código

# Commit
git commit -m "🐛 fix: resolve build errors"
```

### Tests fallan

**Síntomas:**
```
❌ Tests failed
💡 Fix tests before pushing
```

**Solución:**
```bash
# Run tests con output completo
dotnet test

# Fix tests
# ...

# Commit
git commit -m "🐛 fix(AUTH-001): fix failing test"
```

### Coverage bajo

**Síntomas:**
```
❌ Coverage too low: 78% (need ≥85%)
```

**Solución:**
```bash
# Ver coverage report detallado
dotnet test --collect:"XPlat Code Coverage"

# Identificar código no cubierto
# Agregar tests para paths no cubiertos

# Commit
git commit -m "🔴 test(AUTH-001): add coverage tests"
```

### dotnet no encontrado

**Síntomas:**
```
⚠️  dotnet not found, skipping checks
```

**Solución:**
```bash
# Instalar .NET SDK
# https://dotnet.microsoft.com/download

# Verificar instalación
dotnet --version

# Reinstalar hooks
./.claude/scripts/install-hooks.sh
```

---

## Integración con mj2

### project-manager (Issue #10)
- Auto-instala hooks en nuevos proyectos
- Durante Step: Initialize project

### tdd-implementer (Issue #11)
- Commits TDD validados:
  - 🔴 test(SPEC-ID): ...
  - 🟢 feat(SPEC-ID): ...
  - ♻️ refactor(SPEC-ID): ...
- Pre-push valida tests y coverage

### doc-syncer (Issue #12)
- Commits de docs validados:
  - 📚 docs(SPEC-ID): ...

### quality-gate (Issue #14)
- Pre-push validation alineada con quality-gate
- Mismos criterios de coverage

### git-manager (Issue #15)
- Hooks ejecutan antes de merge
- Validación en cada commit

---

## Estadísticas

| Archivo | Líneas | Tamaño | Contenido |
|---------|--------|--------|-----------|
| install-hooks.sh | 234 | 6.8K | Installer + 3 hooks embedded |
| HOOKS.md | 377 | 7.3K | Complete documentation |
| **Total** | **611** | **14.1K** | **2 files** |

**Hooks embedded en install-hooks.sh:**
- pre-commit: ~80 líneas
- commit-msg: ~80 líneas
- pre-push: ~70 líneas

---

## Validación

```bash
# 1. Instalar hooks
./.claude/scripts/install-hooks.sh

# 2. Verificar instalación
ls -la .git/hooks/
# Debe mostrar: pre-commit, commit-msg, pre-push (ejecutables)

# 3. Test pre-commit (format)
echo "test" >> test.txt
git add test.txt
# Debe correr format check + build

# 4. Test commit-msg (debe fallar)
git commit -m "bad message"
# Esperado: ❌ Invalid commit message format

# 5. Test commit-msg (debe pasar)
git commit -m "🔧 chore(TEST-001): add test file"
# Esperado: ✅ Commit message valid

# 6. Test pre-push
git push origin main
# Esperado: Tests run, coverage checked
```

---

## Best Practices

### ✅ DO
- Instalar hooks al inicio del proyecto
- Respetar formato de commit messages
- Fix format/build antes de commit
- Asegurar tests pasan antes de push
- Mantener coverage ≥85%
- Usar --no-verify solo en emergencias
- Leer error messages y seguir sugerencias

### ❌ DON'T
- Bypass hooks regularmente
- Commit código sin formatear
- Push con tests fallando
- Ignorar coverage warnings
- Skip validation en PRs
- Deshabilitar hooks permanentemente
- Commit con mensaje genérico

---

## Próximos Pasos

1. ✅ Crear Git Hooks (Issue #20) ← Este issue
2. ⏳ Issue #21: CLAUDE.md (documentación final)
3. ⏳ Integrar auto-install en project-manager
4. ⏳ Testing del sistema completo
5. ⏳ Release v1.0.0

---

## Referencias

- Commit: a8787c2
- Files:
  - `.claude/scripts/install-hooks.sh`
  - `.claude/scripts/HOOKS.md`
- GitHub Issue: #20
- Related Issues:
  - #10 (project-manager)
  - #11 (tdd-implementer)
  - #12 (doc-syncer)
  - #14 (quality-gate)
  - #15 (git-manager)

---

**mj2: Quality-enforced .NET 9 development**
