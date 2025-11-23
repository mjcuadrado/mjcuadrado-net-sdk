# Workflow Orchestration Analysis: moai-adk vs mj2

**Fecha:** 2025-11-23
**Autor:** mjcuadrado
**Versión:** 1.0.0
**Estado:** Análisis completo

---

## 📋 Executive Summary

Análisis comparativo de sistemas de orquestación de agentes entre **moai-adk** (Mr. Alfred) y **mj2** (mjcuadrado-net-sdk), incluyendo recomendaciones para mejorar el workflow de mj2 sin comprometer su arquitectura actual.

**Conclusión clave:** mj2 ya implementa un sistema de orquestación efectivo basado en comandos slash, pero puede beneficiarse de conceptos de moai-adk sin necesidad de cambios arquitectónicos profundos.

---

## 🔍 1. Análisis de moai-adk: "Mr. Alfred"

### 1.1 ¿Qué es Mr. Alfred?

**Mr. Alfred NO es un agente concreto**, sino un **concepto de orquestación** que representa:

1. **Prompt-based orchestrator** - Usa las capacidades nativas de Claude como conductor
2. **Command router** - Parsea comandos del usuario y delega a agentes especializados
3. **Context manager** - Gestiona contextos de hasta 10 agentes paralelos (200k tokens c/u = 2M total)
4. **Workflow coordinator** - Maneja dependencias secuenciales (SPEC → TDD → Docs)

**Evidencia técnica:**
- No existe archivo `alfred.md` en el repositorio
- La documentación lo describe como "conceptual orchestrator"
- La orquestación funciona mediante Claude + prompts + MCP

### 1.2 Mecanismos de Delegación

**A. Sintaxis explícita `@agent-name`:**
```bash
@agent-docs-manager Translate README.md to Korean README.ko.md
```

**B. Comandos slash (similar a mj2):**
```bash
/moai:1-plan "feature description"
/moai:2-run SPEC-ID
/moai:3-sync SPEC-ID
```

**C. Ejecución paralela:**
- Soporta hasta 10 agentes concurrentes
- Cada agente con contexto independiente de 200k tokens
- Utilización efectiva: 2M tokens

### 1.3 Workflow Plan-Run-Sync

```
Usuario → Mr. Alfred (Claude + prompts)
            ↓
         Parse intent
            ↓
    ┌───────┴────────┐
    │                │
PLAN Phase      RUN Phase      SYNC Phase
    │                │              │
spec-builder   tdd-implementer  docs-manager
    │                │              │
    └────────────────┴──────────────┘
            ↓
      Resultado agregado
```

**Características:**
- Orquestación secuencial con checkpoints
- Cada fase valida la anterior antes de continuar
- Output de cada agente guía al siguiente
- Usuario puede intervenir en cualquier momento

---

## 🔍 2. Análisis de mj2: Arquitectura Actual

### 2.1 Sistema de Comandos Slash

**Estructura actual:**
```
/mj2:0-project  → project-manager   (Inicialización)
/mj2:1-plan     → spec-builder      (SPEC en EARS)
/mj2:2-run      → tdd-implementer   (TDD cycle)
/mj2:3-sync     → doc-syncer        (Docs sync)
/mj2:quality-check → quality-gate   (Validación)
```

**Implementación:**
- Cada comando `.claude/commands/mj2-X.md` define:
  - `agent: mj2/agent-name` - Agente a invocar
  - `description` - Propósito del comando
  - `usage` - Ejemplos de uso
  - `output` - Formato de respuesta

### 2.2 Workflow Secuencial

**Archivo:** `.claude/agents/mj2/project-manager.md:198-200`
```
### Agent Flow
```
project-manager → spec-builder → tdd-implementer → doc-syncer
```
```

**Características:**
- Workflow lineal y explícito
- Cada agente documenta el siguiente paso
- Usuario ejecuta comandos manualmente
- No hay ejecución paralela

### 2.3 Orquestación Actual

**Quality Gate como orquestador implícito:**

**Archivo:** `.claude/agents/mj2/quality-gate.md:351-360`
```
### Agent Flow
```
tdd-implementer (after ♻️ REFACTOR)
  ↓ automatic
quality-gate (THIS)
  ↓ if PASS
doc-syncer
  ↓ if FAIL
[report + block]
```
```

**Responsabilidades:**
- Validar output del `tdd-implementer`
- Bloquear workflow si no pasa TRUST 5
- Guiar al usuario al siguiente paso si pasa

### 2.4 Skills como Conocimiento Compartido

**Patrón de reutilización:**
```markdown
## Agent: spec-builder
Loads Skills:
- foundation/specs.md
- foundation/ears.md
- foundation/tags.md

## Agent: tdd-implementer
Loads Skills:
- dotnet/xunit.md
- dotnet/csharp.md
- foundation/trust.md
- foundation/tags.md
```

**Ventajas:**
- Sin duplicación de lógica
- Actualización centralizada
- Especialización por dominio

### 2.5 Sistema TAG para Trazabilidad

**Cadena de tracking:**
```
/mj2:1-plan   → @SPEC:EX-AUTH-001
/mj2:2-run    → @TEST:EX-AUTH-001 + @CODE:EX-AUTH-001
/mj2:3-sync   → @DOC:EX-AUTH-001
```

**Validación:**
```bash
quality-gate verifica:
- @SPEC existe
- @TEST vinculado a @SPEC
- @CODE vinculado a @TEST
- Coverage ≥ 85%
- TRUST 5 principles
```

---

## 📊 3. Comparación moai-adk vs mj2

### 3.1 Tabla Comparativa

| Aspecto | moai-adk | mj2 | Ventaja |
|---------|----------|-----|---------|
| **Orquestador Central** | Mr. Alfred (conceptual) | quality-gate (implícito) | moai-adk (más explícito) |
| **Sintaxis Invocación** | `@agent-name` + `/moai:X` | `/mj2:X` solamente | moai-adk (más flexible) |
| **Ejecución Paralela** | ✅ Hasta 10 agentes (2M tokens) | ❌ Secuencial | moai-adk |
| **Delegación Explícita** | ✅ `@agent-docs-manager ...` | ❌ Solo comandos slash | moai-adk |
| **Workflow Definido** | Plan-Run-Sync | Plan-Run-Sync-Quality | mj2 (más robusto) |
| **Trazabilidad** | No documentada | ✅ Sistema TAG completo | mj2 |
| **Skills Compartidas** | Sí (128 skills) | ✅ Sí (45 skills) | Empate |
| **Quality Gates** | No visible | ✅ quality-gate agent | mj2 |
| **Git Integration** | Básico | ✅ git-manager + hooks | mj2 |
| **Idioma** | 12 idiomas | 2 (es, en) | moai-adk |
| **Stack** | Python/Node/Universal | .NET 9 + React 18 | Empate (diferente target) |

### 3.2 Fortalezas de moai-adk

✅ **Delegación flexible** - `@agent-name` permite invocar cualquier agente desde cualquier contexto
✅ **Ejecución paralela** - 10 agentes concurrentes para tareas independientes
✅ **Concepto "Mr. Alfred"** - Representación clara del orquestador (aunque sea conceptual)
✅ **Mayor número de skills** - 128 skills (aunque muchas son para Node/Python)

### 3.3 Fortalezas de mj2

✅ **Quality Gate explícito** - Validación automática entre fases
✅ **Sistema TAG** - Trazabilidad completa (@SPEC → @TEST → @CODE → @DOC)
✅ **Git integration** - git-manager + hooks system (Python v2.0.0)
✅ **TRUST 5 principles** - Calidad de código enforced
✅ **Stack moderno .NET** - .NET 9 + React 18 + PostgreSQL
✅ **Workflow más robusto** - Incluye quality check antes de sync

---

## 🎯 4. Limitaciones Técnicas de Claude Code

### 4.1 Sin Delegación Directa Entre Agentes

**Confirmado por claude-code-guide:**

> "Short answer: NO direct inter-agent calling mechanism exists. However, your project demonstrates the correct pattern: agents are chained through CLI commands and slash commands."

**Implicaciones:**
- Los agentes NO pueden llamarse entre sí directamente
- La orquestación debe ser **user-driven** o **command-driven**
- No hay ejecución paralela nativa

### 4.2 Patrón Recomendado por Claude Code

```
Slash Command → Agent Definition File → Agent Execution
```

**Ejemplo en mj2:**
```yaml
# .claude/commands/mj2-1-plan.md
---
name: /mj2:1-plan
agent: mj2/spec-builder
---
```

**Flujo:**
1. Usuario ejecuta `/mj2:1-plan "feature"`
2. Claude Code carga `.claude/commands/mj2-1-plan.md`
3. Ve `agent: mj2/spec-builder`
4. Carga `.claude/agents/mj2/spec-builder.md`
5. Ejecuta el agente con contexto

### 4.3 Sin Ejecución Paralela

**Claude Code ejecuta agentes secuencialmente:**
- Un agente a la vez
- Estado limpio entre ejecuciones
- Determinístico y fácil de debuggear

**Ventaja implícita:**
- Más fácil de razonar
- Sin race conditions
- Clear audit trail

---

## 💡 5. Propuestas de Mejora para mj2

### 5.1 Crear "Mr. mj2" - Orquestador Conceptual

**Objetivo:** Hacer explícito el concepto de orquestación sin cambiar la arquitectura.

**Implementación:**

**A. Actualizar README.md con sección "Mr. mj2":**
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
```

**B. Crear skill `mj2/mr-mj2-orchestration.md`:**
```markdown
# Mr. mj2 Orchestration Skill

## Overview

Mr. mj2 es el concepto de orquestación de mjcuadrado-net-sdk.
No es un agente concreto, sino el sistema que coordina el workflow.

## Workflow Standard

0. PROJECT → project-manager
1. PLAN → spec-builder
2. RUN → tdd-implementer
3. QUALITY → quality-gate
4. SYNC → doc-syncer

## Delegation Rules

- Un agente a la vez (secuencial)
- Output de cada agente guía al siguiente
- Usuario ejecuta comandos manualmente
- Quality gate bloquea si no pasa validación

## Intent Understanding

Mr. mj2 (conceptual) analiza:
- ¿Qué fase del workflow?
- ¿Qué agente es apropiado?
- ¿Están las dependencias satisfechas?
- ¿Qué skills necesita el agente?
```

### 5.2 Implementar `/mj2:status` - Estado del Workflow

**Propósito:** Mostrar dónde está el usuario en el workflow.

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

## Output Example

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
```
```

**Agente:** `.claude/agents/mj2/workflow-status.md`
```markdown
# Workflow Status Agent

## Responsibility

Analiza estado del proyecto y muestra progreso del workflow.

## Data Sources

1. `.mjcuadrado-net-sdk/config.json` - Metadata del proyecto
2. `docs/specs/SPEC-*/status.json` - Estado de cada SPEC
3. Git log - Commits por fase (RED, GREEN, REFACTOR, DOCS)
4. Coverage reports - `coverage.json`
5. TAG chain - Verificar @SPEC → @TEST → @CODE → @DOC

## Output Format

Spanish/English según config:
- Project metadata
- Current branch
- Workflow phases con status (✅ done, 🟡 in progress, ⏳ pending)
- Next recommended step
```

### 5.3 Implementar `/mj2:help` - Guía de Comandos

**Propósito:** Ayudar al usuario a entender qué comando usar.

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
...

💡 Tip: Usa /mj2:status para ver tu posición en el workflow
```
```

### 5.4 Mejorar Output de Agentes con "Next Step"

**Patrón actual:**
```
✅ TDD completado: SPEC-AUTH-001
🎯 Próximo: /mj2:3-sync AUTH-001
```

**Patrón mejorado con "Mr. mj2":**
```
✅ TDD completado: SPEC-AUTH-001

🤖 Mr. mj2 recomienda:
   1. Revisar tests: dotnet test --verbosity detailed
   2. Si todo OK: /mj2:quality-check AUTH-001
   3. Si quality OK: /mj2:3-sync AUTH-001

📊 Estado:
   Tests: 4/4 passing
   Coverage: 87% (≥85% ✅)
   TRUST 5: Pending validation
```

**Implementación:** Actualizar templates en cada agente para incluir:
- Estado actual
- Validaciones pendientes
- Próximos 2-3 pasos
- Referencias a `/mj2:status` y `/mj2:help`

### 5.5 Documentar Orquestación en Skills

**Crear:** `.claude/skills/mj2/orchestration-patterns.md`

```markdown
# MJ² Orchestration Patterns

## Overview

Patrones de orquestación de agentes en mj2.

## Pattern 1: Sequential Workflow (Standard)

```
User → /mj2:1-plan
         ↓
     spec-builder
         ↓
     Output: "Next: /mj2:2-run"
         ↓
User → /mj2:2-run
         ↓
     tdd-implementer
         ↓
     Output: "Next: /mj2:quality-check"
         ↓
     ...
```

## Pattern 2: Quality Gate (Conditional)

```
User → /mj2:2-run
         ↓
     tdd-implementer (REFACTOR completo)
         ↓
User → /mj2:quality-check
         ↓
     quality-gate
         ↓
    [PASS] → "Next: /mj2:3-sync"
    [FAIL] → "Fix issues and re-run"
```

## Pattern 3: Parallel Branches (Manual)

```
Main workflow:
/mj2:1-plan → /mj2:2-run → /mj2:3-sync

Parallel (user decision):
/mj2:2f-build (frontend)
/mj2:4-e2e (E2E tests)
/mj2:5-deploy (deployment)

User decides cuando ejecutar cada uno.
```

## Agent Responsibilities Matrix

| Agent | Phase | Input | Output | Next Step |
|-------|-------|-------|--------|-----------|
| project-manager | 0 | Project info | config.json | /mj2:1-plan |
| spec-builder | 1 | Feature description | SPEC-ID | /mj2:2-run |
| tdd-implementer | 2 | SPEC-ID | Code + Tests | /mj2:quality-check |
| quality-gate | 3 | SPEC-ID | Validation report | /mj2:3-sync or [FAIL] |
| doc-syncer | 4 | SPEC-ID | Updated docs | Workflow complete |

## Skills Loading

Agents load shared skills to avoid duplication:

- `foundation/*` - Loaded by all (TRUST, TAGS, SPECS)
- `dotnet/*` - Loaded by backend agents
- `frontend/*` - Loaded by frontend agents
- `testing/*` - Loaded by testing agents

## User Intervention Points

1. **After /mj2:1-plan:** Review SPEC, adjust if needed
2. **During /mj2:2-run:** Monitor TDD cycle (RED→GREEN→REFACTOR)
3. **After /mj2:quality-check:** Fix issues if validation fails
4. **Before /mj2:3-sync:** Final review of implementation
5. **After /mj2:3-sync:** Review docs, create PR

## Workflow State Tracking

Usar TAG chain para tracking:
```bash
# Ver estado completo:
git log --oneline --grep="@SPEC:AUTH-001"

# Output example:
abc1234 📚 docs(AUTH-001): Sync docs @DOC:AUTH-001
def5678 ♻️ refactor(AUTH-001): Apply TRUST 5 @CODE:AUTH-001
ghi9012 🟢 test(AUTH-001): Pass tests @TEST:AUTH-001
jkl3456 🔴 test(AUTH-001): Add failing tests @TEST:AUTH-001
mno7890 📋 spec(AUTH-001): Create SPEC @SPEC:AUTH-001
```
```

### 5.6 Issue #64: Workflow Orchestrator (NEW)

**Crear Issue #64 para v0.6.0:**

```markdown
# Issue #64: Workflow Orchestrator & Mr. mj2

**Prioridad:** 🟡 Media
**Versión:** v0.6.0
**Tiempo:** 3-4 días

## Objetivo

Hacer explícito el concepto de orquestación "Mr. mj2" sin cambiar arquitectura.

## Entregables

1. **README.md actualizado** - Sección "Mr. mj2" explicando orquestación
2. **Skill:** `.claude/skills/mj2/orchestration-patterns.md` (~400 líneas)
3. **Agent:** `.claude/agents/mj2/workflow-status.md` (~300 líneas)
4. **Command:** `.claude/commands/mj2-status.md` (~150 líneas)
5. **Command:** `.claude/commands/mj2-help.md` (~200 líneas)
6. **Actualizar outputs:** Todos los agentes con formato "Mr. mj2 recomienda"

## Inspiración

- moai-adk "Mr. Alfred" (conceptual orchestrator)
- Mantener arquitectura secuencial de mj2
- Sin delegación directa (limitación de Claude Code)

## No Hacer

❌ NO crear agente "mr-mj2.md" que ejecute otros agentes
❌ NO intentar ejecución paralela
❌ NO sintaxis @agent-name (no soportado por Claude Code)
```

---

## 🚀 6. Recomendaciones de Implementación

### 6.1 Prioridad Alta (Implementar Ya)

1. ✅ **Issue #64: Workflow Orchestrator** (3-4 días)
   - Documentar "Mr. mj2" conceptual
   - Crear `/mj2:status` y `/mj2:help`
   - Actualizar outputs de agentes

2. ✅ **Skill:** `orchestration-patterns.md`
   - Documentar patrones de orquestación actuales
   - Agent responsibility matrix
   - Workflow state tracking

### 6.2 Prioridad Media (Futuro)

3. **Mejorar quality-gate** como orquestador
   - Más inteligente en routing
   - Sugerir fixes específicos
   - Auto-retry con correcciones

4. **Dashboard visual** del workflow
   - Generar diagrama mermaid del estado
   - Mostrar en `/mj2:status`

### 6.3 NO Implementar

❌ **Delegación `@agent-name`** - No soportado por Claude Code
❌ **Ejecución paralela** - No soportado por Claude Code
❌ **Agente "mr-mj2.md" ejecutable** - Rompe arquitectura actual

---

## 📊 7. Impacto Esperado

### 7.1 Para Usuarios

**Antes:**
```bash
# Usuario no sabe qué hacer:
$ /mj2:2-run AUTH-001
✅ TDD completado
🎯 Próximo: /mj2:3-sync
```

**Después:**
```bash
$ /mj2:2-run AUTH-001
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

### 7.2 Para Desarrolladores

✅ **Claridad conceptual** - "Mr. mj2" representa la orquestación
✅ **Mejor UX** - Usuarios saben qué hacer en cada paso
✅ **Troubleshooting fácil** - `/mj2:status` muestra estado completo
✅ **Documentación unificada** - Toda la orquestación en un lugar

### 7.3 Para el Proyecto

✅ **Sin cambios arquitectónicos** - Se mantiene lo que funciona
✅ **Inspirado en moai-adk** - Adopta conceptos útiles
✅ **Respeta límites de Claude Code** - No intenta lo imposible
✅ **Extensible** - Fácil agregar nuevos comandos/agentes

---

## 🎯 8. Conclusiones

### 8.1 Estado Actual de mj2

**mj2 YA TIENE un sistema de orquestación robusto:**
- ✅ Workflow secuencial bien definido
- ✅ Quality gate como validador
- ✅ Sistema TAG para trazabilidad
- ✅ Skills compartidas para reutilización
- ✅ Git integration completo

**Lo que falta:**
- ⏳ Hacer explícito el concepto de orquestación ("Mr. mj2")
- ⏳ Comandos de ayuda (`/mj2:status`, `/mj2:help`)
- ⏳ Outputs más guiados con próximos pasos

### 8.2 Aprendizajes de moai-adk

**Adoptar:**
- ✅ Concepto "Mr. Alfred/mj2" como orquestador conceptual
- ✅ Outputs que guían al usuario explícitamente
- ✅ Comandos de introspección (status, help)

**NO adoptar:**
- ❌ Sintaxis `@agent-name` (no soportado)
- ❌ Ejecución paralela (no soportado)
- ❌ Agente orquestador ejecutable (rompe arquitectura)

### 8.3 Roadmap de Implementación

**v0.6.0 (Próximo):**
- Issue #64: Workflow Orchestrator (3-4 días)
- Documentar "Mr. mj2" en README
- Implementar `/mj2:status` y `/mj2:help`
- Skill `orchestration-patterns.md`

**v0.7.0 (Futuro):**
- Mejorar quality-gate con más inteligencia
- Dashboard visual del workflow
- Más comandos de introspección

---

## 📚 9. Referencias

### 9.1 Documentos Consultados

- **moai-adk README:** https://github.com/modu-ai/moai-adk
- **Claude Code SDK docs:** Via claude-code-guide agent
- **mj2 agents:** `.claude/agents/mj2/*.md`
- **mj2 commands:** `.claude/commands/mj2-*.md`

### 9.2 Archivos Clave de mj2

- `.claude/agents/mj2/project-manager.md:198-200` - Agent flow
- `.claude/agents/mj2/quality-gate.md:351-360` - Orchestration point
- `.claude/skills/mj2/workflow-core.md` - Workflow documentation
- `.claude/commands/mj2-{0,1,2,3}*.md` - Standard workflow commands

### 9.3 Evidencia Técnica

**Claude Code limitations (confirmado):**
> "Short answer: NO direct inter-agent calling mechanism exists."
> "Execution is sequential. Claude Code runs agents one at a time."

**moai-adk implementation (confirmado):**
> "Alfred appears to be a **prompt-based conductor** rather than autonomous code."
> "No actual 'alfred.py' or dedicated orchestrator file is visible"

---

**Versión:** 1.0.0
**Última actualización:** 2025-11-23
**Próxima revisión:** Después de implementar Issue #64
