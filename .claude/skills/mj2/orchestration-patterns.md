# MJ² Orchestration Patterns

**Version:** 1.0.0
**Created:** 2025-11-23
**Tags:** @SPEC:ORCH-064
**Category:** MJ² Core
**Difficulty:** Advanced

---

## 📋 Overview

Este skill documenta los patrones de orquestación de agentes en **mj2** (mjcuadrado-net-sdk).

**"Mr. mj2"** es el concepto de orquestación - no es un agente ejecutable, sino el sistema que coordina el workflow SPEC-First a través de comandos slash y agentes especializados.

### Filosofía de Orquestación

- **User-driven:** El usuario ejecuta comandos manualmente
- **Sequential:** Un agente a la vez (no paralelo)
- **Guided:** Cada agente indica el próximo paso
- **Validated:** Quality gates bloquean si hay errores
- **Traceable:** TAG chain (@SPEC → @TEST → @CODE → @DOC)

---

## 🎯 Orquestación en mj2

### Concepto "Mr. mj2"

**Mr. mj2** es el orquestador conceptual inspirado en moai-adk "Mr. Alfred". Representa el sistema de coordinación entre agentes especializados.

**NO es:**
- ❌ Un agente ejecutable (no existe `mr-mj2.md`)
- ❌ Un proceso automático que ejecuta todo
- ❌ Un sistema de delegación directa entre agentes

**SÍ es:**
- ✅ El concepto de cómo los agentes se coordinan
- ✅ El workflow SPEC-First documentado
- ✅ La guía para usuarios sobre qué hacer en cada paso
- ✅ El sistema de comandos slash que invoca agentes

### Workflow SPEC-First

```
User → /mj2:0-project    [project-manager]
         ↓
User → /mj2:1-plan       [spec-builder]
         ↓
User → /mj2:2-run        [tdd-implementer]
         ↓
User → /mj2:quality-check [quality-gate]
         ↓
User → /mj2:3-sync       [doc-syncer]
         ↓
       Workflow Complete!
```

**Características:**
- Cada comando invoca UN agente específico
- Usuario ejecuta comandos manualmente
- Output de cada agente guía al siguiente
- Quality gate bloquea si no pasa validación

---

## 📐 Pattern 1: Sequential Workflow (Standard)

### Descripción

El patrón más común en mj2. Workflow lineal donde cada fase debe completarse antes de la siguiente.

### Diagrama

```
┌─────────────┐
│   User      │
└──────┬──────┘
       │ /mj2:0-project
       ↓
┌──────────────────┐
│ project-manager  │ → ✅ Proyecto inicializado
└──────┬───────────┘    🎯 Next: /mj2:1-plan
       │
       │ User ejecuta /mj2:1-plan
       ↓
┌──────────────────┐
│  spec-builder    │ → ✅ SPEC creada
└──────┬───────────┘    🎯 Next: /mj2:2-run
       │
       │ User ejecuta /mj2:2-run
       ↓
┌──────────────────┐
│ tdd-implementer  │ → ✅ TDD completo
└──────┬───────────┘    🎯 Next: /mj2:quality-check
       │
       │ User ejecuta /mj2:quality-check
       ↓
┌──────────────────┐
│  quality-gate    │ → ✅ Quality OK
└──────┬───────────┘    🎯 Next: /mj2:3-sync
       │
       │ User ejecuta /mj2:3-sync
       ↓
┌──────────────────┐
│   doc-syncer     │ → ✅ Docs synced
└──────────────────┘    🎉 Complete!
```

### Uso

**Cuándo usar:** Desarrollo normal de features (90% de casos)

**Ejemplo completo:**
```bash
# 1. Inicializar proyecto
$ /mj2:0-project my-api
✅ Proyecto inicializado

# 2. Crear SPEC
$ /mj2:1-plan "user authentication with JWT"
✅ SPEC-AUTH-001 creada

# 3. Implementar con TDD
$ /mj2:2-run AUTH-001
✅ TDD completado (Tests: 4/4, Coverage: 87%)

# 4. Validar calidad
$ /mj2:quality-check AUTH-001
✅ Quality check PASSED

# 5. Sincronizar docs
$ /mj2:3-sync AUTH-001
✅ Docs sincronizados
🎉 Workflow completo!
```

### Output Pattern

Cada agente debe seguir este formato:

```markdown
✅ [Acción] completada: [ID]

🤖 Mr. mj2 recomienda:
   1. [Próximo paso principal]
   2. [Paso alternativo]
   3. Ver estado: /mj2:status [ID]

📊 Estado actual:
   [Métricas relevantes]

💡 Tip: [Consejo útil]
```

---

## 📐 Pattern 2: Quality Gate (Conditional)

### Descripción

Punto de control que bloquea el workflow si no pasa validaciones. Es el único patrón condicional en mj2.

### Diagrama

```
┌──────────────────┐
│ tdd-implementer  │ → ✅ TDD completo
└──────┬───────────┘    🎯 Next: /mj2:quality-check
       │
       │ User ejecuta /mj2:quality-check
       ↓
┌──────────────────┐
│  quality-gate    │ → Validación...
└──────┬───────────┘
       │
       ├─── PASS ───→ ✅ Quality OK
       │              🎯 Next: /mj2:3-sync
       │              User ejecuta /mj2:3-sync
       │                    ↓
       │              ┌──────────────┐
       │              │ doc-syncer   │
       │              └──────────────┘
       │
       └─── FAIL ───→ ❌ Quality FAILED
                      🔧 Fix issues:
                         - Coverage < 85%
                         - TRUST 5 violations
                      🎯 Fix y re-run /mj2:2-run
```

### Validaciones

**quality-gate valida:**

1. **Coverage ≥ 85%**
   ```bash
   dotnet test --collect:"XPlat Code Coverage"
   # Expected: ≥85%
   ```

2. **TRUST 5 Principles**
   - Testable
   - Readable
   - Understandable
   - Secure
   - Traceable

3. **TAG Chain**
   ```bash
   @SPEC:ID → @TEST:ID → @CODE:ID presente
   ```

4. **Tests Passing**
   ```bash
   dotnet test
   # Expected: 100% passing
   ```

### Ejemplo PASS

```bash
$ /mj2:quality-check AUTH-001

✅ Quality Check PASSED: SPEC-AUTH-001

🤖 Mr. mj2 recomienda:
   1. Sincronizar docs: /mj2:3-sync AUTH-001
   2. Ver estado: /mj2:status AUTH-001
   3. Revisar coverage: cat coverage.json

📊 Validation Results:
   Tests: 4/4 passing ✅
   Coverage: 87% (≥85% ✅)
   TRUST 5: All checks passed ✅
   TAG chain: Complete ✅

💡 Tip: Quality gates aseguran código production-ready
```

### Ejemplo FAIL

```bash
$ /mj2:quality-check AUTH-001

❌ Quality Check FAILED: SPEC-AUTH-001

🤖 Mr. mj2 recomienda:
   1. Fix coverage: Añadir tests para AuthService.ValidateToken
   2. Re-run implementation: /mj2:2-run AUTH-001
   3. Ver detalles: /mj2:status AUTH-001

📊 Validation Results:
   Tests: 3/4 passing ❌ (1 failing)
   Coverage: 72% (< 85% ❌)
   TRUST 5: Readable violation ❌
   TAG chain: Complete ✅

🔧 Issues to Fix:
   1. Test "ValidateToken_InvalidToken_ThrowsException" failing
   2. Coverage en AuthService: 65% (target: ≥85%)
   3. Method "ValidateToken" tiene cyclomatic complexity 12 (max: 10)

💡 Tip: Fix los issues y vuelve a ejecutar /mj2:2-run AUTH-001
```

---

## 📐 Pattern 3: Parallel Branches (Manual)

### Descripción

Ramas paralelas que el usuario decide cuándo ejecutar. **NO son automáticas ni concurrentes** - el usuario las ejecuta manualmente según necesidad.

### Diagrama

```
Main Workflow (Sequential):
┌─────────────┐
│ 0-project   │
└──────┬──────┘
       │
       ↓
┌─────────────┐
│  1-plan     │
└──────┬──────┘
       │
       ↓
┌─────────────┐
│  2-run      │ ←─── CORE WORKFLOW
└──────┬──────┘
       │
       ↓
┌──────────────┐
│ quality-check│
└──────┬───────┘
       │
       ↓
┌─────────────┐
│  3-sync     │
└─────────────┘

Parallel Branches (Manual):
       ┌────────────────────┐
       │ 2f-build (frontend)│ ← User decide cuándo
       └────────────────────┘
       ┌────────────────────┐
       │ 4-e2e (E2E tests)  │ ← User decide cuándo
       └────────────────────┘
       ┌────────────────────┐
       │ 5-deploy (deploy)  │ ← User decide cuándo
       └────────────────────┘
```

### Ejemplo

```bash
# Main workflow
$ /mj2:1-plan "user profile"
$ /mj2:2-run PROFILE-001

# Usuario decide: "Quiero frontend ahora"
$ /mj2:2f-build PROFILE-001
✅ Frontend built

# Main workflow continúa
$ /mj2:quality-check PROFILE-001
$ /mj2:3-sync PROFILE-001

# Usuario decide: "Ahora E2E tests"
$ /mj2:4-e2e PROFILE-001
✅ E2E tests passing

# Usuario decide: "Ahora deploy"
$ /mj2:5-deploy staging
✅ Deployed to staging
```

### Comandos Paralelos

| Comando | Cuándo Ejecutar | Independiente de |
|---------|-----------------|------------------|
| `/mj2:2f-build` | Después de backend ready | Main workflow |
| `/mj2:4-e2e` | Después de frontend + backend | Main workflow |
| `/mj2:5-deploy` | Después de quality check | Main workflow |
| `/mj2:9-feedback` | Cualquier momento | Todo |
| `/mj2:create-agent` | Cualquier momento | Todo |
| `/mj2:create-skill` | Cualquier momento | Todo |

**Nota importante:** Aunque se llaman "paralelos", **NO se ejecutan concurrentemente**. Son branches que el usuario ejecuta cuando los necesita, fuera del workflow main.

---

## 🎯 Agent Responsibilities Matrix

### Core Workflow Agents

| Agent | Phase | Input | Output | Next Step | Duration |
|-------|-------|-------|--------|-----------|----------|
| **project-manager** | 0 | Project info | config.json, structure | /mj2:1-plan | 10-15 min |
| **spec-builder** | 1 | Feature description | SPEC-ID (EARS format) | /mj2:2-run | 20-30 min |
| **tdd-implementer** | 2 | SPEC-ID | Code + Tests (TDD cycle) | /mj2:quality-check | 1-4 hours |
| **quality-gate** | 3 | SPEC-ID | Validation report | /mj2:3-sync or [FAIL] | 5-10 min |
| **doc-syncer** | 4 | SPEC-ID | Updated docs | Workflow complete | 15-20 min |

### Specialized Agents

| Agent | Purpose | When to Use | Input | Output |
|-------|---------|-------------|-------|--------|
| **git-manager** | Git operations | Merge, branch management | Branch names | Git operations done |
| **frontend-builder** | Frontend TDD | React components | SPEC-ID | Frontend code + tests |
| **e2e-tester** | E2E tests | After frontend + backend | SPEC-ID | E2E tests passing |
| **devops-expert** | Deployment | Deploy to environments | Environment, strategy | Deployment done |
| **database-expert** | DB migrations | Database changes | Migration type | Migration applied |
| **security-expert** | Security audit | Before production | Code, dependencies | Security report |
| **performance-engineer** | Performance | Performance issues | Target (api/frontend) | Optimizations applied |
| **accessibility-expert** | A11y audit | Frontend features | Target components | WCAG 2.1 report |
| **api-designer** | API design | New APIs | SPEC-ID | API design doc |
| **feedback-manager** | Feedback system | Continuous improvement | Feedback type | Feedback processed |
| **release-manager** | Releases | Version releases | Release type | Release created |
| **agent-factory** | Create agents | Custom agents needed | Agent spec | New agent |
| **skill-factory** | Create skills | Custom skills needed | Skill spec | New skill |
| **debug-helper** | Debugging | Debug issues | Error context | Debug guidance |
| **migration-expert** | Migrations | Legacy code migration | Migration strategy | Migration plan |
| **component-designer** | UI components | Component design | Component requirements | Component spec |
| **workflow-status** | Status | Check workflow state | SPEC-ID (optional) | Workflow status |
| **monitoring-expert** | Monitoring | Setup observability | Monitoring scope | Monitoring configured |
| **ui-ux-expert** | UX design | UX research | Feature requirements | UX design doc |
| **implementation-planner** | Planning | Implementation planning | SPEC-ID | Implementation plan |
| **format-expert** | Formatting | Code formatting | Path (optional) | Code formatted |
| **docs-manager** | Documentation | Docs management | Action | Docs updated |

### Total: 26 Agentes (21 existentes + 5 proyectados v0.6.0-v0.8.0)

---

## 📚 Skills Loading Strategy

### Foundation Skills (Shared by ALL agents)

```markdown
## Agent: [any-agent]
Loads Skills:
- foundation/trust.md       # TRUST 5 principles
- foundation/tags.md        # TAG system (@SPEC, @TEST, @CODE, @DOC)
- foundation/specs.md       # SPEC structure (EARS format)
- foundation/git.md         # Git operations
- foundation/ears.md        # EARS format details
```

**Propósito:** Conocimiento común que todos los agentes necesitan.

### Domain-Specific Skills

**Backend Agents:**
```markdown
Loads Skills:
- dotnet/csharp.md         # C# 13 language
- dotnet/aspnet-core.md    # ASP.NET Core 9
- dotnet/ef-core.md        # Entity Framework Core 9
- dotnet/xunit.md          # xUnit testing
```

**Frontend Agents:**
```markdown
Loads Skills:
- frontend/react.md        # React 18
- frontend/typescript.md   # TypeScript 5
- frontend/vite.md         # Vite build tool
- frontend/mui.md          # Material UI v6
```

**Testing Agents:**
```markdown
Loads Skills:
- testing/playwright.md    # E2E testing
- testing/vitest.md        # Unit testing
- testing/testcontainers.md # Integration testing
```

**DevOps Agents:**
```markdown
Loads Skills:
- tools/docker.md          # Docker containers
- tools/github-actions.md  # CI/CD
- tools/opentelemetry.md   # Observability
```

**Ventaja:** Sin duplicación de conocimiento, actualización centralizada.

---

## 👤 User Intervention Points

### Punto 1: Después de /mj2:1-plan (SPEC Review)

**User debe:**
- ✅ Revisar SPEC generada
- ✅ Verificar requirements (EARS format)
- ✅ Ajustar si necesario
- ✅ Confirmar antes de implementar

**Por qué:** La SPEC es el contrato - debe ser correcta antes de código.

**Ejemplo:**
```bash
$ /mj2:1-plan "user authentication"
✅ SPEC-AUTH-001 creada

🤖 Mr. mj2 recomienda:
   1. REVIEW SPEC: cat docs/specs/SPEC-AUTH-001/spec.md
   2. Si OK: /mj2:2-run AUTH-001
   3. Si ajustes: Edit SPEC y commit

# User revisa y decide
$ cat docs/specs/SPEC-AUTH-001/spec.md
# ... revisa ...
$ /mj2:2-run AUTH-001  # Continuar
```

### Punto 2: Durante /mj2:2-run (TDD Monitoring)

**User puede:**
- 👁️ Monitorear commits (RED, GREEN, REFACTOR)
- 👁️ Ver tests ejecutándose
- 👁️ Verificar coverage en tiempo real
- ⏸️ Intervenir si hay errores

**Por qué:** TDD cycle debe ser transparente y supervisable.

### Punto 3: Después de /mj2:quality-check (Fix Issues)

**User debe:**
- ✅ Si PASS: Continuar a /mj2:3-sync
- ❌ Si FAIL: Fix issues y re-run /mj2:2-run

**Por qué:** Quality gate es bloqueante - no hay bypass.

### Punto 4: Antes de /mj2:3-sync (Final Review)

**User debe:**
- ✅ Review final de implementación
- ✅ Verificar que todo funciona
- ✅ Confirmar que está listo para docs sync

**Por qué:** Docs se generan basadas en código - código debe estar correcto.

### Punto 5: Después de /mj2:3-sync (PR Review)

**User debe:**
- ✅ Revisar docs generadas
- ✅ Verificar TAG chain completa
- ✅ Crear Pull Request
- ✅ Solicitar code review (si es team mode)

**Por qué:** Último checkpoint antes de merge a main.

---

## 🔍 Workflow State Tracking

### TAG Chain System

Cada fase del workflow añade un TAG al código/docs:

```
Phase 1: SPEC   → @SPEC:ID
Phase 2: TDD    → @TEST:ID + @CODE:ID
Phase 3: Quality → (valida TAG chain)
Phase 4: Sync   → @DOC:ID
```

### Verificar TAG Chain

```bash
# Ver todos los commits de una SPEC
$ git log --oneline --grep="@SPEC:AUTH-001"

# Output esperado:
# abc1234 📚 docs(AUTH-001): Sync docs @DOC:AUTH-001
# def5678 ♻️ refactor(AUTH-001): Apply TRUST 5 @CODE:AUTH-001
# ghi9012 🟢 test(AUTH-001): Pass tests @TEST:AUTH-001
# jkl3456 🔴 test(AUTH-001): Add failing tests @TEST:AUTH-001
# mno7890 📋 spec(AUTH-001): Create SPEC @SPEC:AUTH-001
```

### Estado por Fase

**Phase 0: Project Initialized**
```bash
$ ls .mjcuadrado-net-sdk/
config.json  memory/  project/  reports/  specs/
```

**Phase 1: SPEC Created**
```bash
$ ls docs/specs/SPEC-AUTH-001/
acceptance.md  plan.md  spec.md
```

**Phase 2: Implementation Done**
```bash
$ git log --oneline -3
# abc1234 ♻️ refactor(AUTH-001): Apply TRUST 5 @CODE:AUTH-001
# def5678 🟢 test(AUTH-001): Pass tests @TEST:AUTH-001
# ghi9012 🔴 test(AUTH-001): Add failing tests @TEST:AUTH-001
```

**Phase 3: Quality Validated**
```bash
$ cat .mj2/reports/quality-gate-AUTH-001.json
{
  "spec_id": "AUTH-001",
  "status": "PASS",
  "coverage": 87,
  "tests_passing": true,
  "trust5": "passed"
}
```

**Phase 4: Docs Synced**
```bash
$ git log --oneline -1
# abc1234 📚 docs(AUTH-001): Sync docs @DOC:AUTH-001

$ grep "@DOC:AUTH-001" README.md
## Authentication (@DOC:AUTH-001)
```

---

## 🎯 Orchestration Best Practices

### 1. Siempre Seguir el Workflow

**Correcto:**
```bash
/mj2:0-project → /mj2:1-plan → /mj2:2-run → /mj2:quality-check → /mj2:3-sync
```

**Incorrecto:**
```bash
# ❌ Saltar quality-check
/mj2:2-run → /mj2:3-sync

# ❌ Implementar sin SPEC
/mj2:2-run AUTH-001  # (sin /mj2:1-plan primero)
```

### 2. User es el Orquestador

- ✅ Usuario ejecuta comandos manualmente
- ✅ Usuario revisa outputs
- ✅ Usuario decide cuándo continuar
- ❌ NO hay auto-ejecución de comandos

### 3. Quality Gate es Bloqueante

- ✅ Si FAIL: Fix y re-run /mj2:2-run
- ❌ NO hacer bypass
- ❌ NO ir a /mj2:3-sync si FAIL

### 4. TAG Chain Completa

- ✅ Cada fase añade su TAG
- ✅ Validar TAG chain antes de PR
- ❌ NO hacer commits sin TAGs

### 5. Un Agente a la Vez

- ✅ Esperar que un agente complete
- ✅ Revisar output antes de continuar
- ❌ NO ejecutar múltiples comandos sin esperar

---

## 🔗 Comandos de Introspección

### /mj2:status

Muestra estado actual del workflow.

**Usage:**
```bash
/mj2:status              # Estado general del proyecto
/mj2:status AUTH-001     # Estado de SPEC específica
```

**Output:** Ver workflow-status.md agent

### /mj2:help

Muestra comandos disponibles y guía.

**Usage:**
```bash
/mj2:help                # Lista todos los comandos
/mj2:help workflow       # Explica workflow SPEC-First
/mj2:help 1-plan         # Ayuda de comando específico
```

---

## 📝 References

- **Analysis:** `.github/analysis/workflow-orchestration-analysis-2025-11-23.md`
- **SPEC:** `docs/specs/SPEC-ORCH-064/`
- **Inspired by:** moai-adk "Mr. Alfred" (conceptual orchestrator)

---

**Version:** 1.0.0
**Last Updated:** 2025-11-23
**Tags:** @SPEC:ORCH-064
