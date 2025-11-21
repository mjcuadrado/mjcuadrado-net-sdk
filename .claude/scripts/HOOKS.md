# Git Hooks

Validación automática en commits y pushes para mantener la calidad del código.

## Installation

```bash
# Instalar hooks
./.claude/scripts/install-hooks.sh
```

Output esperado:
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

## Hooks

### pre-commit
**Ejecuta antes de cada commit**

#### Validaciones
- ✅ Code format (dotnet format --verify-no-changes)
- ✅ Build success (dotnet build)
- ⚠️  TODOs in code (warning only, no blocking)

#### Flujo
```bash
$ git commit -m "message"
🔍 Running pre-commit checks...
📝 Checking code format...
✅ Format check passed
🔨 Building project...
✅ Build passed
✅ Pre-commit checks passed
```

#### Si falla
```bash
$ git commit -m "message"
📝 Checking code format...
❌ Format check failed
💡 Run: dotnet format

# Fix y reintentar
$ dotnet format
$ git add .
$ git commit -m "message"
```

#### Bypass (emergencia)
```bash
git commit --no-verify -m "message"
```

---

### commit-msg
**Ejecuta después de escribir el mensaje de commit**

#### Formato esperado
```
<emoji> <type>(SPEC-ID): <description>
```

o

```
<emoji> <type>: <description>  (para chore, style, build, ci)
```

#### Emojis válidos
| Emoji | Tipo | Cuándo usar |
|-------|------|-------------|
| 🔴 | test | RED phase (failing tests) |
| 🟢 | feat | GREEN phase (passing implementation) |
| ♻️ | refactor | REFACTOR phase (quality improvements) |
| 📚 | docs | Documentation sync |
| 🐛 | fix | Bug fix |
| ✨ | NEW | New feature (without TDD) |
| 🔧 | chore | Maintenance tasks |
| ⚡ | perf | Performance improvements |
| 📦 | build | Build system changes |
| 🎨 | style | Code style changes |

#### Tipos válidos
- `test` - Tests (RED phase)
- `feat` - Features (GREEN phase)
- `refactor` - Refactoring (REFACTOR phase)
- `docs` - Documentation
- `fix` - Bug fixes
- `chore` - Maintenance
- `style` - Code style
- `perf` - Performance
- `build` - Build system
- `ci` - CI/CD config
- `spec` - Specifications

#### Ejemplos válidos ✅
```bash
✅ 🔴 test(AUTH-001): add failing tests for login
✅ 🟢 feat(AUTH-001): implement auth service
✅ ♻️ refactor(AUTH-001): improve code quality
✅ 📚 docs(AUTH-001): sync documentation
✅ 🐛 fix(AUTH-001): correct token expiration
✅ 🔧 chore: update dependencies
✅ 🎨 style: format code
```

#### Ejemplos inválidos ❌
```bash
❌ "implemented feature"           (no format)
❌ "feat: add login"                (no emoji)
❌ "🟢 implemented login"          (no type)
❌ "🟢 feat add login"             (missing colon)
❌ "🟢 feat(AUTH001): add login"   (SPEC-ID sin guion)
```

#### Si falla
```bash
$ git commit -m "bad message"
🔍 Validating commit message...
❌ Invalid commit message format

Expected format:
  <emoji> <type>(SPEC-ID): <description>

Your message:
  bad message

# Fix usando el formato correcto
$ git commit -m "🟢 feat(AUTH-001): implement login"
✅ Commit message valid
```

#### Commits especiales (auto-skip)
- Merge commits (`Merge branch...`)
- Claude Code commits (contienen `🤖 Generated with [Claude Code]`)

---

### pre-push
**Ejecuta antes de push a remote**

#### Validaciones
- ✅ All tests pass (dotnet test)
- ✅ Coverage ≥85% (XPlat Code Coverage)
- ✅ No merge conflict markers (`<<<<<<<`)

#### Flujo
```bash
$ git push origin main
🔍 Running pre-push checks...
🧪 Running tests...
✅ All tests passed
📊 Checking coverage...
✅ Coverage: 87% (≥85%)
✅ Pre-push checks passed
```

#### Si fallan los tests
```bash
$ git push origin main
🧪 Running tests...
❌ Tests failed
💡 Fix tests before pushing

# Fix tests y reintentar
$ dotnet test
$ git commit -am "🐛 fix(AUTH-001): fix failing test"
$ git push origin main
```

#### Si coverage <85%
```bash
$ git push origin main
📊 Checking coverage...
❌ Coverage too low: 78% (need ≥85%)
💡 Add more tests to increase coverage

# Agregar más tests
$ # ... create more tests ...
$ git commit -am "🔴 test(AUTH-001): add coverage tests"
$ git push origin main
```

#### Bypass (emergencia)
```bash
git push --no-verify origin main
```

---

## Bypass Hooks

### Cuándo usar --no-verify

#### ✅ Casos válidos
- **Work in progress:** Commit parcial para backup
- **Emergency hotfix:** Fix crítico en producción
- **CI/CD only:** Dejar que CI valide
- **Non-.NET project:** Proyecto sin dotnet

#### ❌ NUNCA bypass en
- Merge a main/master
- Production releases
- Team collaboration (PR)
- Final implementation

### Cómo bypass

```bash
# Skip pre-commit y commit-msg
git commit --no-verify -m "WIP: work in progress"

# Skip pre-push
git push --no-verify origin feature-branch
```

---

## Troubleshooting

### Hook no ejecuta

**Problema:** Hook no se ejecuta

**Solución:**
```bash
# Verificar permisos
ls -la .git/hooks/

# Debe mostrar -rwxr-xr-x (ejecutable)
# Si no:
chmod +x .git/hooks/pre-commit
chmod +x .git/hooks/commit-msg
chmod +x .git/hooks/pre-push
```

### Format check falla

**Problema:** `❌ Format check failed`

**Solución:**
```bash
# Auto-format
dotnet format

# Revisar cambios
git diff

# Commit
git add .
git commit -m "🎨 style: format code"
```

### Build falla

**Problema:** `❌ Build failed`

**Solución:**
```bash
# Build con output completo
dotnet build

# Fix errores
# ...

# Commit
git commit -m "🐛 fix: resolve build errors"
```

### Tests fallan

**Problema:** `❌ Tests failed`

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

**Problema:** `❌ Coverage too low: 78%`

**Solución:**
```bash
# Ver coverage report
dotnet test --collect:"XPlat Code Coverage"

# Identificar código no cubierto
# Agregar tests

# Commit
git commit -m "🔴 test(AUTH-001): add coverage tests"
```

### dotnet no encontrado

**Problema:** `⚠️  dotnet not found`

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

## Desinstalar Hooks

```bash
# Remover hooks
rm .git/hooks/pre-commit
rm .git/hooks/commit-msg
rm .git/hooks/pre-push

# O renombrar para deshabilitar temporalmente
mv .git/hooks/pre-commit .git/hooks/pre-commit.disabled
mv .git/hooks/commit-msg .git/hooks/commit-msg.disabled
mv .git/hooks/pre-push .git/hooks/pre-push.disabled
```

---

## Best Practices

### ✅ DO
- Instalar hooks al inicio del proyecto
- Respetar el formato de commit messages
- Fix format/build antes de commit
- Asegurar tests pasan antes de push
- Mantener coverage ≥85%
- Usar --no-verify solo en emergencias

### ❌ DON'T
- Bypass hooks regularmente
- Commit código sin formatear
- Push con tests fallando
- Ignore coverage warnings
- Skip validation en PRs
- Deshabilitar hooks permanentemente

---

## Referencias

- [Git Hooks Documentation](https://git-scm.com/book/en/v2/Customizing-Git-Git-Hooks)
- [Conventional Commits](https://www.conventionalcommits.org/)
- [dotnet format](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-format)
- [Code Coverage](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage)
