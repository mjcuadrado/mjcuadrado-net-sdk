# Issue #46: Release Management System

**Fecha:** 2025-11-23
**Prioridad:** 🔴 Alta
**Estado:** ✅ Completado
**Branch:** `feature/issue-46-release-management`

---

## 📋 Descripción

Sistema completo de gestión de releases con workflow automatizado, versionado semántico, CHANGELOG automático y validación exhaustiva pre-release.

---

## 🎯 Objetivos

- [x] Crear release-manager agent con workflow completo
- [x] Implementar /mj2:99-release command
- [x] Versionado semántico automático (MAJOR.MINOR.PATCH)
- [x] Generación automática de CHANGELOG
- [x] Validación pre-release (tests, build, coverage, quality gates)
- [x] Integración con Git tags y GitHub Releases
- [x] Templates de release notes

---

## 📦 Entregables

### 1. Release Manager Agent
**Archivo:** `.claude/agents/mj2/release-manager.md` (892 líneas)

**Workflow 4 Fases: PLAN → VALIDATE → GENERATE → RELEASE**

**Fase 1: PLAN**
- Analizar commits desde último release
- Determinar tipo de release (major, minor, patch)
- Calcular próxima versión semántica
- Identificar issues cerrados
- Revisar breaking changes

**Fase 2: VALIDATE**
- Tests passing (100%)
- Build exitoso (0 errors, 0 warnings)
- Coverage mínimo (≥ 90%)
- Quality gates TRUST 5
- Scan de vulnerabilidades
- No WIP commits

**Fase 3: GENERATE**
- CHANGELOG automático (formato Keep a Changelog)
- Release notes completas
- Migration guide (si breaking changes)
- Version bump en archivos
- Git tag con metadata

**Fase 4: RELEASE**
- Commit de version bump
- Push Git tag
- Crear GitHub Release
- Publicar release notes
- Notificar stakeholders

**Semantic Versioning:**
- **MAJOR** (v0.5.0 → v1.0.0): Breaking changes
- **MINOR** (v0.4.0 → v0.5.0): Nuevas features backward-compatible
- **PATCH** (v0.5.0 → v0.5.1): Bug fixes y mejoras

**Detección Automática:**
```typescript
// Breaking changes → MAJOR
- feat!: Change API (BREAKING)
- BREAKING CHANGE: in commit body

// Features → MINOR
- feat: Add new feature

// Fixes → PATCH
- fix: Resolve bug
- perf: Optimize
- docs: Update
```

**CHANGELOG Automático:**
Formato Keep a Changelog con secciones:
- Added (features)
- Changed (refactors, improvements)
- Fixed (bug fixes)
- Deprecated (to be removed)
- Removed (breaking removals)
- Security (security fixes)

**Validaciones Pre-Release:**
- Tests: 100% passing obligatorio
- Build: 0 errors, 0 warnings
- Coverage: ≥ 90%
- Quality Gates: TRUST 5 passing
- Vulnerabilities: 0
- Git: No WIP commits, branch clean

### 2. Comando /mj2:99-release
**Archivo:** `.claude/commands/mj2-99-release.md` (565 líneas)

**Sintaxis:**
```bash
# Auto-detecta tipo
/mj2:99-release

# Con opciones
/mj2:99-release [options]
```

**Options:**
- `--type <type>` - Forzar tipo (major, minor, patch)
- `--dry-run` - Preview sin hacer cambios
- `--skip-tests` - Saltar tests (NO RECOMENDADO)
- `--skip-validation` - Saltar quality gates (NO RECOMENDADO)
- `--message "<msg>"` - Custom release message
- `--prerelease` - Marcar como pre-release (beta, alpha, rc)

**Ejemplos:**
```bash
# Release automático
/mj2:99-release

# Dry run (preview)
/mj2:99-release --dry-run

# Forzar patch
/mj2:99-release --type patch

# Emergency hotfix
/mj2:99-release --type patch --message "Emergency fix CVE-2025-1234"

# Pre-release
/mj2:99-release --prerelease
# Output: v0.5.0-beta.1
```

**Workflow del Comando:**
1. **PLAN:** Detecta último release, analiza commits, calcula versión
2. **VALIDATE:** Ejecuta todas las validaciones (tests, build, coverage)
3. **GENERATE:** Genera CHANGELOG, release notes, version bump
4. **RELEASE:** Commit, tag, push, GitHub Release

**Error Handling:**
- Tests failing → Bloquea release, muestra tests fallidos
- Coverage < 90% → Bloquea release, muestra archivos sin coverage
- Uncommitted changes → Bloquea release, pide commit primero
- Not on main → Bloquea release, requiere main branch
- Vulnerabilities → Bloquea release, muestra CVEs

### 3. Templates de Release Notes

**Template Completo:**
```markdown
# Release v0.5.0 - System Evolution

**Fecha:** 2025-11-23
**Tipo:** Minor Release (Features)

## 🎯 Highlights
<2-3 highlights principales>

## ✨ Nuevas Features
<Lista de features con issues>

## 🔧 Mejoras
<Lista de mejoras>

## 🐛 Bug Fixes
<Lista de fixes>

## 🚨 Breaking Changes
<Solo si es MAJOR release>

## 📊 Métricas
<Issues, commits, líneas, etc.>

## 📚 Documentación
<Links a docs>

## 🔗 Links
<GitHub Release, Compare, CHANGELOG>
```

**Migration Guide Template (Breaking Changes):**
```markdown
# Migration Guide: v0.X → v1.0

## API Changes
<Antes y después con ejemplos>

## Configuration Changes
<Cambios en config files>

## Deprecated Features
<Features removidas y alternativas>

## Testing
<Comando para verificar migración>
```

---

## 📊 Métricas

**Archivos Creados:** 3
- 1 agent (release-manager)
- 1 command (/mj2:99-release)
- 1 issue doc (issue-46.md)

**Líneas de Código:** 1,457
- release-manager.md: 892 líneas
- mj2-99-release.md: 565 líneas

**Features Implementadas:** 12+
- Semantic versioning automático
- CHANGELOG generation
- Release notes generation
- Pre-release validation (6 checks)
- Git tag management
- GitHub Release integration
- Breaking changes detection
- Migration guide generation
- Dry run mode
- Error handling completo
- Emergency hotfix support
- Pre-release support (beta, alpha, rc)

**Validaciones:** 6
1. Tests (100% passing)
2. Build (0 errors, 0 warnings)
3. Coverage (≥ 90%)
4. Quality Gates (TRUST 5)
5. Vulnerabilities (0)
6. Git (no WIP, clean branch)

**Release Types:** 3
- MAJOR (breaking changes)
- MINOR (features)
- PATCH (fixes)

---

## 🔄 Workflow Completo

```
Usuario ejecuta: /mj2:99-release

📋 PLAN
  ↓ git describe --tags --abbrev=0  →  v0.4.0
  ↓ git log v0.4.0..HEAD  →  15 commits
  ↓ Analiza: 3 feat:, 2 fix:, 10 otros
  ↓ Detecta tipo: MINOR (features)
  ↓ Calcula versión: v0.5.0
  ↓ Identifica issues: #44, #45

✅ VALIDATE
  ↓ dotnet test  →  195/195 passing ✓
  ↓ dotnet build  →  Success ✓
  ↓ Coverage check  →  92.5% ✓
  ↓ TRUST 5 check  →  PASSING ✓
  ↓ Vulnerability scan  →  0 vulnerabilities ✓
  ↓ Git status  →  Clean ✓

📝 GENERATE
  ↓ Genera CHANGELOG.md (v0.5.0 section)
  ↓ Genera release notes
  ↓ Bump version: 0.4.0 → 0.5.0
  ↓ Actualiza: *.csproj, README.md
  ↓ Crea Git tag: v0.5.0

🚀 RELEASE
  ↓ git commit -m "chore: Release v0.5.0"
  ↓ git tag -a v0.5.0 -m "..."
  ↓ git push origin main
  ↓ git push origin v0.5.0
  ↓ gh release create v0.5.0

✅ Release v0.5.0 completado!
🔗 https://github.com/.../releases/tag/v0.5.0
```

---

## 💡 Casos de Uso

### Caso 1: Minor Release (Features)
```bash
# Situación: 3 features nuevas, 2 fixes
/mj2:99-release

# Output:
# - Tipo: MINOR
# - Versión: v0.4.0 → v0.5.0
# - CHANGELOG con 3 Added, 2 Fixed
# - Release notes con highlights
```

### Caso 2: Patch Release (Bug Fixes)
```bash
# Situación: Solo bug fixes
/mj2:99-release --type patch

# Output:
# - Tipo: PATCH
# - Versión: v0.5.0 → v0.5.1
# - CHANGELOG con Fixed section
# - Release notes con fixes
```

### Caso 3: Major Release (Breaking Changes)
```bash
# Situación: feat!: BREAKING CHANGE
/mj2:99-release

# Output:
# - ⚠️ BREAKING CHANGES DETECTED
# - Tipo: MAJOR
# - Versión: v0.9.5 → v1.0.0
# - CHANGELOG con Breaking Changes section
# - Migration guide generado
# - Confirm prompt
```

### Caso 4: Dry Run (Preview)
```bash
# Preview sin hacer cambios
/mj2:99-release --dry-run

# Output:
# - Preview de CHANGELOG
# - Preview de release notes
# - Próxima versión
# - No hace cambios reales
```

### Caso 5: Emergency Hotfix
```bash
# Hotfix crítico
/mj2:99-release --type patch --message "Emergency fix CVE-2025-1234"

# Output:
# - Fast-track validation
# - Versión: v0.5.0 → v0.5.1
# - Emergency release notes
```

---

## ✅ Criterios de Éxito

Al completar este issue, el proyecto tiene:

- [x] **Release Manager funcional**
  - Workflow 4 fases completo
  - Semantic versioning automático
  - Detección de breaking changes
  - CHANGELOG generation
  - Release notes generation

- [x] **Validación pre-release**
  - Tests 100% passing
  - Build exitoso
  - Coverage ≥ 90%
  - Quality gates TRUST 5
  - Vulnerability scan
  - Git checks

- [x] **Automatización completa**
  - Version bump automático
  - Git tag con metadata
  - GitHub Release creation
  - CHANGELOG actualizado
  - Release notes publicadas

- [x] **Error handling robusto**
  - Tests failing → Blocked
  - Coverage bajo → Blocked
  - Dirty working dir → Blocked
  - Not on main → Blocked
  - Mensajes claros de error

- [x] **Flexibilidad**
  - Auto-detección de tipo
  - Forzar tipo con --type
  - Dry run mode
  - Emergency hotfix support
  - Pre-release support

---

## 🚀 Próximos Pasos Sugeridos

### Crear Primer Release Automatizado

```bash
# Preview primero
/mj2:99-release --dry-run

# Si todo OK, ejecutar
/mj2:99-release

# Verificar en GitHub
gh release list
```

### Configurar CI/CD para Releases

```yaml
# .github/workflows/release.yml
name: Release

on:
  push:
    branches: [main]

jobs:
  release:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Run release check
        run: |
          # Check if should release
          if [[ $(git log -1 --pretty=%B) == feat:* ]]; then
            echo "New feature detected, consider release"
          fi
```

---

## 📚 Documentación Relacionada

- [Release Manager Agent](.claude/agents/mj2/release-manager.md)
- [Comando /mj2:99-release](.claude/commands/mj2-99-release.md)
- [Semantic Versioning](https://semver.org/)
- [Keep a Changelog](https://keepachangelog.com/)

---

## 🔗 Referencias

**GitHub Issue:** https://github.com/mjcuadrado/mjcuadrado-net-sdk/issues/46

**Inspirado por:**
- Semantic Release
- Conventional Commits
- Keep a Changelog

---

**Versión:** 1.0.0
**Completado:** 2025-11-23
**Tiempo Estimado:** 5-6 días
**Tiempo Real:** ~2 horas
**Mantenido por:** mjcuadrado-net-sdk
**Workflow:** PLAN → VALIDATE → GENERATE → RELEASE
