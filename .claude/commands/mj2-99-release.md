---
name: mj2-99-release
description: Gestiona releases con versionado semántico y CHANGELOG automático
tags: [release, versioning, changelog]
---

# /mj2:99-release - Release Management

Comando para crear releases profesionales con validación exhaustiva, versionado semántico automático y CHANGELOG generado.

---

## 📋 Uso

```bash
# Sintaxis básica (auto-detecta tipo)
/mj2:99-release

# Con opciones
/mj2:99-release [options]

# Dry run (preview sin cambios)
/mj2:99-release --dry-run
```

---

## 🎯 Opciones

### --type <type>
Fuerza el tipo de release (sobrescribe detección automática)

**Valores válidos:**
- `major` - Breaking changes (v0.5.0 → v1.0.0)
- `minor` - Nuevas features (v0.4.0 → v0.5.0)
- `patch` - Bug fixes (v0.5.0 → v0.5.1)

### --dry-run
Simula el release sin hacer cambios reales

**Útil para:**
- Preview de CHANGELOG
- Verificar próxima versión
- Revisar validaciones

### --skip-tests
⚠️ Salta la validación de tests (NO RECOMENDADO)

**Solo usar si:**
- Tests ya ejecutados manualmente
- Situación de emergencia
- Hotfix crítico

### --skip-validation
⚠️ Salta quality gates (NO RECOMENDADO)

**Solo usar si:**
- Validación manual ya realizada
- Emergency release

### --message "<mensaje>"
Mensaje custom para el release

**Ejemplo:**
```bash
/mj2:99-release --message "Emergency security fix for CVE-2025-1234"
```

### --prerelease
Marca el release como pre-release (beta, alpha, rc)

**Ejemplo:**
```bash
/mj2:99-release --type minor --prerelease
# Output: v0.5.0-beta.1
```

---

## 💡 Ejemplos

### Ejemplo 1: Release Automático

```bash
/mj2:99-release
```

**Output:**
```markdown
🚀 Release Manager - Automated Release

📋 PLAN
✓ Último release: v0.4.0 (2025-11-20)
✓ Commits desde entonces: 15
  - 3 feat: (features)
  - 2 fix: (bug fixes)
  - 1 perf: (performance)
  - 9 docs/chore: (otros)

✓ Tipo detectado: MINOR (3 features)
✓ Próxima versión: v0.5.0

Issues cerrados:
- #44: Feedback & Learning System
- #45: Agent & Skill Factory

✅ VALIDATE
✓ Tests: 195/195 passing (100%)
✓ Build: Success (0 errors, 0 warnings)
✓ Coverage: 92.5% (≥ 90% ✓)
✓ Quality Gates: PASSING
  - TRUST 5: ✓
  - Code smells: 0
  - Security: No vulnerabilities
✓ No WIP commits
✓ Branch up to date with main

📝 GENERATE
✓ CHANGELOG.md actualizado (v0.5.0 section added)
  - Added: 6 features
  - Changed: 2 improvements
  - Fixed: 2 bug fixes

✓ Release notes generadas:
  - Title: "v0.5.0 - System Evolution"
  - Highlights: 2
  - Features: 2 major
  - Métricas: 5,400+ líneas

✓ Version bumped: 0.4.0 → 0.5.0
  - src/**/*.csproj updated
  - README.md updated

🚀 RELEASE
✓ Commit: "chore: Release v0.5.0"
✓ Git tag: v0.5.0 (annotated, signed)
✓ Pushed to origin
✓ GitHub Release created
✓ Release notes published

✅ Release v0.5.0 completed successfully!

📊 Summary:
- Type: MINOR
- Version: v0.4.0 → v0.5.0
- Issues: 2 (#44, #45)
- Commits: 15
- Lines: 5,400+

🔗 Links:
- Release: https://github.com/mjcuadrado/mjcuadrado-net-sdk/releases/tag/v0.5.0
- Compare: https://github.com/mjcuadrado/mjcuadrado-net-sdk/compare/v0.4.0...v0.5.0
- CHANGELOG: https://github.com/mjcuadrado/mjcuadrado-net-sdk/blob/main/CHANGELOG.md
```

### Ejemplo 2: Dry Run

```bash
/mj2:99-release --dry-run
```

**Output:**
```markdown
🚀 Release Manager - Dry Run Mode

📋 PLAN
✓ Tipo: MINOR
✓ Versión: v0.4.0 → v0.5.0
✓ 15 commits to release

✅ VALIDATE
<todas las validaciones>

📝 GENERATE
✓ Preview CHANGELOG:

## [0.5.0] - 2025-11-23

### Added
- Agent Factory meta-agente (#45)
- Skill Factory meta-agente (#45)
- Feedback & Learning System (#44)

### Changed
- Updated README.md with v0.5.0 features

### Fixed
- Minor bug fixes

✓ Preview Release Notes:
<release notes preview>

🔍 DRY RUN - No changes made

To proceed with release:
/mj2:99-release
```

### Ejemplo 3: Patch Release

```bash
/mj2:99-release --type patch
```

**Output:**
```markdown
📋 PLAN
✓ Tipo: PATCH (forzado)
✓ Versión: v0.5.0 → v0.5.1
✓ 3 commits:
  - fix: Resolve N+1 query
  - fix: Validation error message
  - perf: Cache optimization

<resto del workflow>

✅ Release v0.5.1 completed!
```

### Ejemplo 4: Major Release con Breaking Changes

```bash
/mj2:99-release
```

**Output:**
```markdown
📋 PLAN
⚠️  BREAKING CHANGES DETECTED!

Commits con breaking changes:
- feat!: Change API response format (BREAKING CHANGE in body)
- BREAKING CHANGE: Remove deprecated OldMethod()

✓ Tipo: MAJOR (breaking changes detected)
✓ Versión: v0.9.5 → v1.0.0

⚠️  This is a MAJOR release with breaking changes.
Continue? (y/n): y

✅ VALIDATE
<validaciones>

📝 GENERATE
✓ CHANGELOG with BREAKING CHANGES section
✓ Migration guide generated (235 lines)
✓ Release notes with upgrade instructions

🚀 RELEASE
<release process>

✅ Release v1.0.0 completed!
🎉 MILESTONE: First stable release!
```

### Ejemplo 5: Emergency Hotfix

```bash
/mj2:99-release --type patch --message "Emergency fix for CVE-2025-1234"
```

**Output:**
```markdown
📋 PLAN
✓ Tipo: PATCH (emergency hotfix)
✓ Versión: v0.5.0 → v0.5.1
✓ Message: "Emergency fix for CVE-2025-1234"

✅ VALIDATE
⚠️  Fast-track validation for emergency release

🚀 RELEASE
✓ Emergency release v0.5.1 published
⚠️  Remember to follow up with full validation
```

---

## 🔄 Workflow Detallado

### Fase 1: PLAN

**1. Detectar Último Release:**
```bash
git describe --tags --abbrev=0
# Output: v0.4.0
```

**2. Analizar Commits:**
```bash
git log v0.4.0..HEAD --oneline --format="%s"
```

**3. Clasificar Commits:**
```typescript
// Breaking changes?
if (commits.some(c =>
  c.includes('BREAKING') ||
  c.includes('!:') ||
  c.body.includes('BREAKING CHANGE')
)) {
  return 'MAJOR';
}

// Features?
if (commits.some(c => c.startsWith('feat:'))) {
  return 'MINOR';
}

// Default: PATCH
return 'PATCH';
```

**4. Calcular Próxima Versión:**
```typescript
function calculateNextVersion(current: string, type: ReleaseType): string {
  const [major, minor, patch] = current.split('.').map(Number);

  switch (type) {
    case 'MAJOR':
      return `${major + 1}.0.0`;
    case 'MINOR':
      return `${major}.${minor + 1}.0`;
    case 'PATCH':
      return `${major}.${minor}.${patch + 1}`;
  }
}
```

### Fase 2: VALIDATE

**Pre-Release Checklist Automático:**

```markdown
## Validation Results

### Tests
- [x] All tests passing: 195/195 (100%)
- [x] Integration tests: 45/45
- [x] E2E tests: 12/12

### Build
- [x] Build successful: ✓
- [x] Warnings: 0
- [x] Errors: 0

### Quality
- [x] Coverage: 92.5% (≥ 90%)
- [x] TRUST 5: PASSING
- [x] Code smells: 0 critical

### Security
- [x] Vulnerabilities: 0
- [x] Dependencies updated
- [x] Security scan: PASS

### Git
- [x] No WIP commits
- [x] Branch clean
- [x] Up to date with main
```

### Fase 3: GENERATE

**CHANGELOG Generation:**

```markdown
## [0.5.0] - 2025-11-23

### Added
- Agent Factory meta-agente para crear agentes custom (#45)
- Skill Factory meta-agente para crear skills (#45)
- /mj2:create-agent command con 9 dominios (#45)
- /mj2:create-skill command con 7 categorías (#45)
- Feedback & Learning System (#44)
- /mj2:9-feedback command (#44)

### Changed
- Actualizado README.md con v0.5.0 features
- Mejorado ROADMAP.md con issues completados

### Fixed
- N/A

[0.5.0]: https://github.com/mjcuadrado/mjcuadrado-net-sdk/compare/v0.4.0...v0.5.0
```

**Release Notes Generation:**

```markdown
# Release v0.5.0 - System Evolution

## 🎯 Highlights

**🚀 GAME CHANGER: Agent & Skill Factory**
<highlights...>

## ✨ Nuevas Features
<features...>

## 📊 Métricas
<métricas...>

## 🔗 Links
<links...>
```

### Fase 4: RELEASE

**Git Operations:**
```bash
# 1. Commit version bump
git add .
git commit -m "chore: Release v0.5.0"

# 2. Create annotated tag
git tag -a v0.5.0 -m "Release v0.5.0: System Evolution"

# 3. Push to origin
git push origin main
git push origin v0.5.0

# 4. Create GitHub Release
gh release create v0.5.0 \
  --title "v0.5.0 - System Evolution" \
  --notes-file RELEASE_NOTES.md \
  --latest
```

---

## 🚨 Errores Comunes

### Error: Tests Failing

```bash
/mj2:99-release
```

**Error:**
```
❌ VALIDATION FAILED

✗ Tests: 193/195 passing (98.97%)
  Failed tests:
  - OrdersControllerTests.CreateOrder_WithInvalidData_Returns400
  - ProductsRepositoryTests.GetById_WithNonExistent_ReturnsNull

⚠️  Release blocked by failing tests.

Options:
1. Fix failing tests first (RECOMMENDED)
2. Use --skip-tests (NOT RECOMMENDED)

To fix:
dotnet test --filter "FullyQualifiedName~OrdersControllerTests"
```

### Error: Coverage Below Minimum

```bash
/mj2:99-release
```

**Error:**
```
❌ VALIDATION FAILED

✗ Coverage: 87.3% (< 90% minimum)

Files below coverage:
- OrdersController.cs: 82.5%
- ProductsRepository.cs: 85.1%

⚠️  Release blocked by insufficient coverage.

To fix:
1. Add tests for uncovered code
2. Run: dotnet test --collect:"XPlat Code Coverage"
```

### Error: Uncommitted Changes

```bash
/mj2:99-release
```

**Error:**
```
❌ VALIDATION FAILED

✗ Uncommitted changes detected:
  M  src/OrdersController.cs
  ?? temp.txt

⚠️  Release blocked by dirty working directory.

To fix:
git add .
git commit -m "Your commit message"
```

### Error: Not on Main Branch

```bash
/mj2:99-release
```

**Error:**
```
❌ VALIDATION FAILED

✗ Current branch: feature/my-feature
✓ Expected branch: main

⚠️  Releases must be created from main branch.

To fix:
git checkout main
git pull origin main
```

---

## 📚 Ver También

- **Agente:** `.claude/agents/mj2/release-manager.md`
- **Git Workflow:** `.claude/skills/foundation/git.md`
- **Semantic Versioning:** https://semver.org/

---

## ✅ Salida Esperada

Al ejecutar exitosamente:

1. **CHANGELOG.md actualizado**
   - Nueva sección con versión y fecha
   - Commits organizados por categoría
   - Links a comparación

2. **Git Tag creado**
   - Tag anotado con metadata
   - Pushed a origin
   - Visible en GitHub

3. **GitHub Release**
   - Release publicado
   - Release notes completas
   - Marked as latest

4. **Version bumped**
   - Archivos .csproj actualizados
   - README.md actualizado
   - Commit de version bump

5. **Validación completa**
   - Tests passing (100%)
   - Quality gates passed
   - No vulnerabilities

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-23
