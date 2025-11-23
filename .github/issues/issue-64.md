# Issue #64: Workflow Orchestrator & "Mr. mj2"

**Fecha:** 2025-11-23
**Prioridad:** 🟡 Media
**Estado:** 📋 Planificado
**Versión:** v0.6.0
**Branch:** feature/ISSUE-064-workflow-orchestrator
**Tiempo Estimado:** 3-4 días

---

## 📋 Descripción

Hacer explícito el concepto de orquestación **"Mr. mj2"** sin cambiar la arquitectura actual de mj2.

**Inspirado en:** moai-adk "Mr. Alfred" (conceptual orchestrator)

**Gap identificado:** mj2 tiene orquestación implícita pero no documentada. Los usuarios no entienden cómo los agentes se coordinan.

---

## 🎯 Objetivos

### 1. Documentar "Mr. mj2" Conceptual
- README.md con sección "Mr. mj2"
- Explicar que es un concepto, no un agente ejecutable
- Mostrar cómo orquesta agentes especializados

### 2. Skill de Orquestación
- `.claude/skills/mj2/orchestration-patterns.md` (~400 líneas)
  - Sequential workflow pattern
  - Quality gate pattern (conditional)
  - Parallel branches pattern (manual)
  - Agent responsibilities matrix
  - Skills loading strategy
  - Workflow state tracking con TAG chain

### 3. Comando `/mj2:status`
- `.claude/agents/mj2/workflow-status.md` (~300 líneas)
  - Analiza estado del proyecto
  - Muestra progreso del workflow
  - Indica próximo paso recomendado
- `.claude/commands/mj2-status.md` (~150 líneas)
  - Usage: `/mj2:status [SPEC-ID]`
  - Output: Estado completo del workflow

### 4. Comando `/mj2:help`
- `.claude/commands/mj2-help.md` (~200 líneas)
  - Lista todos los comandos disponibles
  - Explica workflow SPEC-First
  - Guía contextual según fase actual

### 5. Actualizar Outputs de Agentes
- Todos los agentes con formato "Mr. mj2 recomienda"
- Mostrar estado actual
- Listar validaciones pendientes
- Próximos 2-3 pasos
- Referencias a `/mj2:status` y `/mj2:help`

---

## 📦 Entregables

### 1. README.md Actualizado

```markdown
## 🤖 Mr. mj2 - Tu Asistente de Desarrollo

Mr. mj2 es el orquestador conceptual que coordina todos los agentes especializados.
Cuando usas mj2, Mr. mj2 entiende tu intención y delega el trabajo a los expertos:

- **Project Manager:** Inicializa proyectos con estructura óptima
- **SPEC Builder:** Convierte ideas en especificaciones EARS
- **TDD Implementer:** Ejecuta el ciclo RED-GREEN-REFACTOR
- **Quality Gate:** Valida que el código cumple TRUST 5
- **Doc Syncer:** Mantiene documentación sincronizada

Mr. mj2 nunca trabaja solo - orquesta expertos para cada tarea.

### 🔄 Workflow SPEC-First

```
0. /mj2:0-project    → Inicializar proyecto
1. /mj2:1-plan       → Crear SPEC (Plan)
2. /mj2:2-run        → Implementar con TDD (Run)
3. /mj2:quality-check → Validar calidad
4. /mj2:3-sync       → Sincronizar docs (Sync)
```

💡 Usa `/mj2:status` para ver tu posición en el workflow
💡 Usa `/mj2:help` para ayuda contextual
```

### 2. orchestration-patterns.md Skill

**Archivo:** `.claude/skills/mj2/orchestration-patterns.md`

**Contenido:**
- Pattern 1: Sequential Workflow (Standard)
- Pattern 2: Quality Gate (Conditional)
- Pattern 3: Parallel Branches (Manual)
- Agent Responsibilities Matrix
- Skills Loading Strategy
- User Intervention Points
- Workflow State Tracking (TAG chain)

### 3. workflow-status Agent

**Archivo:** `.claude/agents/mj2/workflow-status.md`

**Responsibility:**
Analiza estado del proyecto y muestra progreso del workflow.

**Data Sources:**
1. `.mjcuadrado-net-sdk/config.json` - Metadata del proyecto
2. `docs/specs/SPEC-*/status.json` - Estado de cada SPEC (si existe)
3. Git log - Commits por fase (RED, GREEN, REFACTOR, DOCS)
4. Coverage reports - `coverage.json`
5. TAG chain - Verificar @SPEC → @TEST → @CODE → @DOC

**Output Example:**
```
🤖 Mr. mj2 - Workflow Status

📊 Proyecto: my-api (v0.1.0)
🌿 Branch: feature/SPEC-AUTH-001

Workflow Progress:
✅ Phase 0: Proyecto inicializado (2025-11-20)
✅ Phase 1: SPEC-AUTH-001 creada (2025-11-21)
🟡 Phase 2: Implementación 87% (tests passing, coverage 87%)
⏳ Phase 3: Quality check pendiente
⏳ Phase 4: Documentación pendiente

🎯 Próximo paso:
   Completar cobertura: dotnet test --collect:"XPlat Code Coverage"
   Si ≥85%, ejecutar: /mj2:quality-check AUTH-001

💡 Tip: Usa /mj2:help para ver comandos disponibles
```

### 4. mj2-status Command

**Archivo:** `.claude/commands/mj2-status.md`

```yaml
---
name: /mj2:status
description: Show current workflow state
agent: mj2/workflow-status
---

# /mj2:status

Muestra el estado actual del workflow y próximos pasos.

## Usage

```bash
/mj2:status
/mj2:status SPEC-ID  # Estado de una SPEC específica
```

## Output

Ver ejemplo en workflow-status.md
```

### 5. mj2-help Command

**Archivo:** `.claude/commands/mj2-help.md`

```yaml
---
name: /mj2:help
description: Show available commands and workflow guidance
---

# /mj2:help

Muestra comandos disponibles y guía del workflow.

## Usage

```bash
/mj2:help
/mj2:help workflow     # Explicación del workflow
/mj2:help commands     # Lista de comandos
/mj2:help COMMAND      # Ayuda de comando específico
```

## Output

```
🤖 Mr. mj2 - Ayuda

📚 Workflow SPEC-First:

0️⃣ /mj2:0-project           Inicializar proyecto
1️⃣ /mj2:1-plan              Crear SPEC (Plan)
2️⃣ /mj2:2-run               Implementar con TDD (Run)
3️⃣ /mj2:quality-check       Validar calidad
4️⃣ /mj2:3-sync              Sincronizar docs (Sync)

🔧 Comandos adicionales:

/mj2:status                  Ver estado del workflow
/mj2:git-merge              Merge feature branch
/mj2:2f-build               Build frontend (React)
/mj2:4-e2e                  E2E tests (Playwright)
/mj2:5-deploy               Deploy to environment
/mj2:9-feedback             Manage feedback
/mj2:create-agent           Create custom agent
/mj2:create-skill           Create custom skill
/mj2:99-release             Create release

💡 Tip: Usa /mj2:status para ver tu posición en el workflow
```
```

### 6. Actualizar Outputs de Agentes

**Patrón actual:**
```
✅ TDD completado: SPEC-AUTH-001
🎯 Próximo: /mj2:3-sync AUTH-001
```

**Patrón mejorado:**
```
✅ TDD completado: SPEC-AUTH-001

🤖 Mr. mj2 recomienda:
   1. Validar quality: /mj2:quality-check AUTH-001
   2. Si pasa quality: /mj2:3-sync AUTH-001
   3. Ver estado: /mj2:status AUTH-001

📊 Estado actual:
   Tests: 4/4 passing ✅
   Coverage: 87% ✅
   TRUST 5: Pending

💡 Tip: Usa /mj2:help para ver comandos disponibles
```

**Agentes a actualizar:**
- project-manager.md
- spec-builder.md
- tdd-implementer.md
- quality-gate.md
- doc-syncer.md
- (Todos los 26 agentes eventualmente)

---

## ✅ Criterios de Éxito

- [ ] README.md actualizado con sección "Mr. mj2"
- [ ] orchestration-patterns.md skill creado (~400 líneas)
- [ ] workflow-status.md agent creado (~300 líneas)
- [ ] mj2-status.md command creado (~150 líneas)
- [ ] mj2-help.md command creado (~200 líneas)
- [ ] 5+ agentes actualizados con nuevo formato de output
- [ ] `/mj2:status` funciona correctamente
- [ ] `/mj2:help` funciona correctamente
- [ ] Documentación completa en español

---

## 🔗 Referencias

- **Análisis completo:** `.github/analysis/workflow-orchestration-analysis-2025-11-23.md`
- **Inspirado en:** moai-adk "Mr. Alfred" (conceptual orchestrator)
- **Claude Code limitations:** No direct inter-agent calling, sequential execution
- **Pattern actual:** Slash Command → Agent Definition → Agent Execution

---

## ⚠️ Restricciones Técnicas (NO Implementar)

### ❌ Delegación `@agent-name`
**Razón:** No soportado por Claude Code SDK
**Alternativa:** Usar comandos slash `/mj2:X`

### ❌ Ejecución Paralela de Agentes
**Razón:** Claude Code ejecuta agentes secuencialmente
**Alternativa:** Workflow secuencial user-driven (actual)

### ❌ Agente "mr-mj2.md" Ejecutable
**Razón:** Rompe arquitectura actual, Claude Code no soporta inter-agent calls
**Alternativa:** "Mr. mj2" es conceptual, no ejecutable

---

## 🚀 Impacto

**Para Usuarios:**
- ✅ Claridad conceptual sobre orquestación
- ✅ Saben qué hacer en cada paso del workflow
- ✅ Ayuda contextual con `/mj2:help`
- ✅ Visibilidad del progreso con `/mj2:status`

**Para el Proyecto:**
- ✅ Sin cambios arquitectónicos (se mantiene lo que funciona)
- ✅ Adopta conceptos útiles de moai-adk
- ✅ Respeta límites de Claude Code
- ✅ Mejor experiencia de usuario
- ✅ Documentación unificada de orquestación

**Ejemplo Antes/Después:**

**Antes:**
```bash
$ /mj2:2-run AUTH-001
✅ TDD completado
🎯 Próximo: /mj2:3-sync
# Usuario no sabe si debe hacer algo antes
```

**Después:**
```bash
$ /mj2:2-run AUTH-001
✅ TDD completado: SPEC-AUTH-001

🤖 Mr. mj2 recomienda:
   1. Validar quality: /mj2:quality-check AUTH-001
   2. Si pasa: /mj2:3-sync AUTH-001
   3. Ver estado: /mj2:status

📊 Estado: Tests ✅ Coverage 87% ✅
💡 Tip: /mj2:help para más comandos
# Usuario tiene claridad completa
```

---

## 📝 Notas de Implementación

### Fase 1: Documentación (1 día)
1. Actualizar README.md con "Mr. mj2"
2. Crear orchestration-patterns.md skill
3. Documentar patrones existentes

### Fase 2: Comandos Nuevos (1 día)
1. Crear workflow-status agent
2. Crear mj2-status command
3. Crear mj2-help command
4. Probar ambos comandos

### Fase 3: Actualizar Outputs (1-2 días)
1. Actualizar template de outputs
2. Actualizar 5 agentes core (project-manager, spec-builder, tdd-implementer, quality-gate, doc-syncer)
3. Probar workflow completo
4. Validar que outputs guían correctamente

### Fase 4: Documentación Issue (medio día)
1. Crear .github/issues/issue-64.md
2. Commit y push

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Prioridad:** 🟡 MEDIA
**Milestone:** v0.6.0
**Tiempo:** 3-4 días
