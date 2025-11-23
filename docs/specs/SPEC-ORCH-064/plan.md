# SPEC-ORCH-064: Implementation Plan

**SPEC ID:** SPEC-ORCH-064
**Created:** 2025-11-23
**Status:** In Progress
**Tags:** @SPEC:ORCH-064

---

## 🎯 Implementation Strategy

### Timeline: 3-4 días

**Day 1:** Skills & Documentation + Status Command (inicio)
**Day 2:** Status Command (finalización) + Help Command
**Day 3:** Update Agent Outputs
**Day 4:** Testing & Documentation Sync

---

## 📋 Task Breakdown

### Phase 1: Skills & Documentation (6-8 hours)

#### Task 1.1: orchestration-patterns.md skill
**Estimate:** 3-4 hours
**Files:** `.claude/skills/mj2/orchestration-patterns.md`
**Lines:** ~400

**Subtasks:**
- [ ] Definir estructura del skill
- [ ] Pattern 1: Sequential Workflow (Standard)
  - Diagrama mermaid
  - Explicación
  - Ejemplo completo
- [ ] Pattern 2: Quality Gate (Conditional)
  - Diagrama mermaid
  - Explicación
  - Ejemplo completo
- [ ] Pattern 3: Parallel Branches (Manual)
  - Diagrama mermaid
  - Explicación
  - Ejemplo completo
- [ ] Agent Responsibilities Matrix
  - Tabla de 26 agentes
  - Input/Output/Next Step por agente
- [ ] Skills Loading Strategy
  - foundation/* - Shared by all
  - dotnet/* - Backend agents
  - frontend/* - Frontend agents
  - testing/* - Testing agents
- [ ] User Intervention Points
  - 5 intervention points documentados
- [ ] Workflow State Tracking
  - TAG chain examples
  - Git log queries

**Validation:**
- Skill tiene ~400 líneas
- 3 patrones completos con diagramas
- Matrix incluye 26 agentes
- Ejemplos ejecutables

#### Task 1.2: README.md Update
**Estimate:** 2-3 hours
**Files:** `README.md`
**Lines:** +50-80

**Subtasks:**
- [ ] Crear sección "🤖 Mr. mj2 - Tu Asistente de Desarrollo"
- [ ] Explicar concepto de orquestación
- [ ] Listar agentes especializados que coordina
- [ ] Diagrama del workflow (mermaid)
- [ ] Referencias a `/mj2:status` y `/mj2:help`
- [ ] Actualizar TOC

**Validation:**
- Sección clara y concisa (~50-80 líneas)
- Diagrama visual incluido
- Enlaces a comandos

#### Task 1.3: Git Commit (SPEC phase)
**Estimate:** 30 min
```bash
git add docs/specs/SPEC-ORCH-064/
git add .claude/skills/mj2/orchestration-patterns.md
git add README.md
git commit -m "📋 spec(ORCH-064): Add orchestration patterns & Mr. mj2 docs @SPEC:ORCH-064"
```

---

### Phase 2: Status Command (6-8 hours)

#### Task 2.1: workflow-status.md agent
**Estimate:** 4-5 hours
**Files:** `.claude/agents/mj2/workflow-status.md`
**Lines:** ~300

**Subtasks:**
- [ ] Agent metadata (frontmatter)
- [ ] Agent Persona
- [ ] Responsibilities section
- [ ] Workflow: DETECT → ANALYZE → FORMAT → RECOMMEND
- [ ] Data Sources implementation:
  - Read `.mjcuadrado-net-sdk/config.json`
  - Parse git log for phases
  - Check coverage.json (if exists)
  - Validate TAG chain
- [ ] Output format templates:
  - Project overview
  - Workflow progress (per phase)
  - SPEC-specific status
  - Next step recommendation
  - Tips
- [ ] Language handling (es/en)
- [ ] Error handling (no project, no SPEC, etc.)

**Validation:**
- Agent ~300 líneas
- 4-phase workflow implementado
- Data sources funcionan
- Output templates completos

#### Task 2.2: mj2-status.md command
**Estimate:** 1-2 hours
**Files:** `.claude/commands/mj2-status.md`
**Lines:** ~150

**Subtasks:**
- [ ] Command metadata (frontmatter)
- [ ] Usage documentation
- [ ] Examples:
  - `/mj2:status` (general)
  - `/mj2:status AUTH-001` (specific SPEC)
- [ ] Output format examples
- [ ] Error cases

**Validation:**
- Command ~150 líneas
- Ejemplos completos
- Delegate to workflow-status agent

#### Task 2.3: Testing
**Estimate:** 1 hour

**Test Cases:**
- [ ] `/mj2:status` en proyecto no inicializado
- [ ] `/mj2:status` en proyecto inicializado (fase 0)
- [ ] `/mj2:status` con SPEC creada (fase 1)
- [ ] `/mj2:status` con implementación en progreso (fase 2)
- [ ] `/mj2:status AUTH-001` (SPEC específica)

#### Task 2.4: Git Commit (CODE phase - part 1)
**Estimate:** 30 min
```bash
git add .claude/agents/mj2/workflow-status.md
git add .claude/commands/mj2-status.md
git commit -m "🟢 feat(ORCH-064): Add /mj2:status command @CODE:ORCH-064"
```

---

### Phase 3: Help Command (3-4 hours)

#### Task 3.1: mj2-help.md command
**Estimate:** 2-3 hours
**Files:** `.claude/commands/mj2-help.md`
**Lines:** ~200

**Subtasks:**
- [ ] Command metadata (frontmatter)
- [ ] Usage documentation
- [ ] Workflow commands section:
  - /mj2:0-project
  - /mj2:1-plan
  - /mj2:2-run
  - /mj2:quality-check
  - /mj2:3-sync
- [ ] Additional commands section (15+ commands):
  - /mj2:status (NEW)
  - /mj2:git-merge
  - /mj2:2f-build
  - /mj2:4-e2e
  - /mj2:5-deploy
  - /mj2:9-feedback
  - /mj2:create-agent
  - /mj2:create-skill
  - /mj2:99-release
  - ... (todos los 20+ comandos)
- [ ] Contextual help (by argument):
  - `/mj2:help workflow`
  - `/mj2:help commands`
  - `/mj2:help COMMAND`
- [ ] Language handling
- [ ] Tips section

**Validation:**
- Command ~200 líneas
- 20+ comandos documentados
- Contextual help funciona
- Output en español

#### Task 3.2: Testing
**Estimate:** 30 min

**Test Cases:**
- [ ] `/mj2:help` (sin args)
- [ ] `/mj2:help workflow`
- [ ] `/mj2:help commands`
- [ ] `/mj2:help 1-plan` (specific command)

#### Task 3.3: Git Commit (CODE phase - part 2)
**Estimate:** 30 min
```bash
git add .claude/commands/mj2-help.md
git commit -m "🟢 feat(ORCH-064): Add /mj2:help command @CODE:ORCH-064"
```

---

### Phase 4: Update Agent Outputs (6-8 hours)

**Pattern template:**
```markdown
✅ [Acción] completada: [ID]

🤖 Mr. mj2 recomienda:
   1. [Próximo paso principal]
   2. [Paso alternativo]
   3. Ver estado: /mj2:status [ID]

📊 Estado actual:
   [Métricas relevantes]

💡 Tip: Usa /mj2:help para ver comandos disponibles
```

#### Task 4.1: Update project-manager.md
**Estimate:** 1-1.5 hours
**Files:** `.claude/agents/mj2/project-manager.md`

**Changes:**
- [ ] Actualizar output format en Phase 6 (Success Output)
- [ ] Añadir "Mr. mj2 recomienda" section
- [ ] Próximo paso: `/mj2:1-plan "feature"`
- [ ] Estado: Proyecto inicializado, config.json creado
- [ ] Tip: `/mj2:help` para comandos

**Before:**
```
✅ Proyecto inicializado: my-api
📁 Estructura creada
🎯 Próximo: /mj2:1-plan "feature"
```

**After:**
```
✅ Proyecto inicializado: my-api

🤖 Mr. mj2 recomienda:
   1. Crear primera SPEC: /mj2:1-plan "user authentication"
   2. Ver estado: /mj2:status
   3. Ayuda: /mj2:help workflow

📊 Estado actual:
   Proyecto: my-api (v0.1.0)
   Framework: .NET 9.0
   Database: PostgreSQL
   Git: Inicializado ✅

💡 Tip: El workflow SPEC-First comienza con /mj2:1-plan
```

#### Task 4.2: Update spec-builder.md
**Estimate:** 1-1.5 hours
**Files:** `.claude/agents/mj2/spec-builder.md`

**Changes:**
- [ ] Actualizar output format en Phase 4 (Output)
- [ ] Añadir "Mr. mj2 recomienda" section
- [ ] Próximo paso: `/mj2:2-run SPEC-ID`
- [ ] Estado: SPEC creada, archivos generados, branch creada
- [ ] Tip: Review SPEC antes de implementar

#### Task 4.3: Update tdd-implementer.md
**Estimate:** 1-1.5 hours
**Files:** `.claude/agents/mj2/tdd-implementer.md`

**Changes:**
- [ ] Actualizar output format en Phase 4 (Quality Gate)
- [ ] Añadir "Mr. mj2 recomienda" section
- [ ] Próximo paso: `/mj2:quality-check SPEC-ID`
- [ ] Estado: Tests, Coverage, TRUST 5 status
- [ ] Tip: Coverage debe ser ≥85%

#### Task 4.4: Update quality-gate.md
**Estimate:** 1-1.5 hours
**Files:** `.claude/agents/mj2/quality-gate.md`

**Changes:**
- [ ] Actualizar output format (Success/Failure)
- [ ] Añadir "Mr. mj2 recomienda" section
- [ ] Si PASS: `/mj2:3-sync SPEC-ID`
- [ ] Si FAIL: Fix issues and re-run
- [ ] Estado: Validation results detallados
- [ ] Tip: TRUST 5 es crítico

#### Task 4.5: Update doc-syncer.md
**Estimate:** 1-1.5 hours
**Files:** `.claude/agents/mj2/doc-syncer.md`

**Changes:**
- [ ] Actualizar output format en Phase 4 (Success Output)
- [ ] Añadir "Mr. mj2 recomienda" section
- [ ] Próximo paso: Review docs y crear PR
- [ ] Estado: Docs sincronizados, TAG chain completa
- [ ] Tip: Workflow completo! 🎉

#### Task 4.6: Testing Updated Outputs
**Estimate:** 1 hour

**Test Cases:**
- [ ] Run `/mj2:0-project test-project` → Verify new output
- [ ] Run `/mj2:1-plan "test feature"` → Verify new output
- [ ] Simulate `/mj2:2-run` output → Verify format
- [ ] Simulate `/mj2:quality-check` output → Verify format
- [ ] Simulate `/mj2:3-sync` output → Verify format

#### Task 4.7: Git Commit (CODE phase - part 3)
**Estimate:** 30 min
```bash
git add .claude/agents/mj2/project-manager.md
git add .claude/agents/mj2/spec-builder.md
git add .claude/agents/mj2/tdd-implementer.md
git add .claude/agents/mj2/quality-gate.md
git add .claude/agents/mj2/doc-syncer.md
git commit -m "♻️ refactor(ORCH-064): Update agent outputs with Mr. mj2 format @CODE:ORCH-064"
```

---

### Phase 5: Documentation Sync (2-3 hours)

#### Task 5.1: Verify TAG Chain
**Estimate:** 30 min

**Verification:**
```bash
# Buscar todos los @SPEC:ORCH-064
git log --all --grep="@SPEC:ORCH-064"

# Buscar todos los @CODE:ORCH-064
git log --all --grep="@CODE:ORCH-064"

# Próximo: Añadir @DOC:ORCH-064
```

**Expected TAG chain:**
```
@SPEC:ORCH-064 → spec.md, plan.md, acceptance.md, orchestration-patterns.md, README.md
@CODE:ORCH-064 → workflow-status.md, mj2-status.md, mj2-help.md, 5 agentes actualizados
@DOC:ORCH-064 → CHANGELOG.md, architecture.md (si aplica)
```

#### Task 5.2: Update CHANGELOG.md
**Estimate:** 1 hour
**Files:** `CHANGELOG.md`

**Entry:**
```markdown
## [Unreleased]

### Added
- **Workflow Orchestrator:** Concepto "Mr. mj2" explicado en README (@SPEC:ORCH-064)
- **`/mj2:status` command:** Muestra estado del workflow en tiempo real (@CODE:ORCH-064)
- **`/mj2:help` command:** Guía contextual de comandos disponibles (@CODE:ORCH-064)
- **orchestration-patterns.md skill:** Documentación de patrones de orquestación (@SPEC:ORCH-064)
- **workflow-status.md agent:** Analiza y reporta estado del proyecto (@CODE:ORCH-064)

### Changed
- **Agent outputs:** 5 agentes core actualizados con formato "Mr. mj2 recomienda" (@CODE:ORCH-064)
  - project-manager.md
  - spec-builder.md
  - tdd-implementer.md
  - quality-gate.md
  - doc-syncer.md

### Improved
- **UX:** Usuarios tienen claridad sobre próximos pasos en cada fase
- **Documentation:** Concepto de orquestación ahora explícito
- **Guidance:** Outputs más guiados con tips útiles
```

#### Task 5.3: End-to-End Testing
**Estimate:** 1 hour

**Full Workflow Test:**
```bash
# 1. Verificar /mj2:help
/mj2:help

# 2. Verificar /mj2:status antes de inicializar
/mj2:status

# 3. Inicializar proyecto test
/mj2:0-project test-orchestrator

# 4. Verificar output nuevo de project-manager
# Expected: Formato "Mr. mj2 recomienda"

# 5. Verificar /mj2:status después de inicializar
/mj2:status
# Expected: Phase 0 ✅

# 6. Crear SPEC test
/mj2:1-plan "test orchestration"

# 7. Verificar output nuevo de spec-builder
# Expected: Formato "Mr. mj2 recomienda"

# 8. Verificar /mj2:status con SPEC
/mj2:status TEST-001
# Expected: Phase 1 ✅, Phase 2 ⏳

# 9. Verificar /mj2:help workflow
/mj2:help workflow
# Expected: Workflow completo explicado
```

#### Task 5.4: Final Git Commit (DOC phase)
**Estimate:** 30 min
```bash
git add CHANGELOG.md
git commit -m "📚 docs(ORCH-064): Sync documentation @DOC:ORCH-064"
```

---

## 📊 Progress Tracking

### Checklist

**Phase 1: Skills & Documentation**
- [ ] orchestration-patterns.md skill (~400 líneas)
- [ ] README.md sección "Mr. mj2" (~50-80 líneas)
- [ ] Git commit @SPEC:ORCH-064

**Phase 2: Status Command**
- [ ] workflow-status.md agent (~300 líneas)
- [ ] mj2-status.md command (~150 líneas)
- [ ] Testing (5 test cases)
- [ ] Git commit @CODE:ORCH-064 (part 1)

**Phase 3: Help Command**
- [ ] mj2-help.md command (~200 líneas)
- [ ] Testing (4 test cases)
- [ ] Git commit @CODE:ORCH-064 (part 2)

**Phase 4: Agent Outputs**
- [ ] project-manager.md updated
- [ ] spec-builder.md updated
- [ ] tdd-implementer.md updated
- [ ] quality-gate.md updated
- [ ] doc-syncer.md updated
- [ ] Testing (5 test cases)
- [ ] Git commit @CODE:ORCH-064 (part 3)

**Phase 5: Documentation Sync**
- [ ] TAG chain verification
- [ ] CHANGELOG.md updated
- [ ] End-to-end testing
- [ ] Git commit @DOC:ORCH-064

---

## 🎯 Success Criteria

### Completion Criteria

- [x] Todas las tareas completadas
- [x] Todos los tests passing
- [x] TAG chain completa (@SPEC → @CODE → @DOC)
- [x] ~1,200-1,500 líneas totales
- [x] Sin errores en comandos
- [x] Documentación consistente

### Deliverables

- [x] 1 skill (~400 líneas)
- [x] 1 agent (~300 líneas)
- [x] 2 commands (~350 líneas)
- [x] README updated (~50-80 líneas)
- [x] 5 agentes updated
- [x] CHANGELOG updated
- [x] SPEC completa (spec.md, plan.md, acceptance.md)

---

## 🔗 References

- **SPEC:** `docs/specs/SPEC-ORCH-064/spec.md`
- **Analysis:** `.github/analysis/workflow-orchestration-analysis-2025-11-23.md`
- **Issue #64:** `.github/issues/issue-64.md`

---

**Created:** 2025-11-23
**Status:** In Progress
**Next:** Execute Phase 1 (Skills & Documentation)
