# SPEC-ORCH-064: Workflow Orchestrator & "Mr. mj2"

**ID:** SPEC-ORCH-064
**Domain:** ORCH (Orchestration)
**Type:** Enhancement
**Status:** In Progress
**Created:** 2025-11-23
**Updated:** 2025-11-23
**Author:** @mjcuadrado
**Tags:** @SPEC:ORCH-064

---

## 📋 Overview

Hacer explícito el concepto de orquestación "Mr. mj2" inspirado en moai-adk "Mr. Alfred", sin cambiar la arquitectura actual de mj2. Mejorar la experiencia de usuario proporcionando comandos de introspección (`/mj2:status`, `/mj2:help`) y outputs guiados.

---

## 🎯 Goals

1. **Documentar "Mr. mj2"** como concepto de orquestación en README.md
2. **Crear skill de patrones de orquestación** para uso interno de agentes
3. **Implementar `/mj2:status`** para mostrar estado del workflow
4. **Implementar `/mj2:help`** para guía contextual de comandos
5. **Mejorar outputs de agentes** con formato "Mr. mj2 recomienda"
6. **Sin cambios arquitectónicos** - mantener lo que funciona

---

## 📐 Requirements (EARS Format)

### R1: Orchestration Patterns Skill

**WHEN** un agente necesita entender patrones de orquestación,
**THE SYSTEM SHALL** proporcionar `.claude/skills/mj2/orchestration-patterns.md` con:
- Sequential workflow pattern (Standard)
- Quality gate pattern (Conditional)
- Parallel branches pattern (Manual)
- Agent responsibilities matrix
- Skills loading strategy
- User intervention points
- Workflow state tracking con TAG chain

**SUCCESS CRITERIA:**
- Skill de ~400 líneas
- 3 patrones documentados
- Matrix de responsabilidades de 26 agentes
- Ejemplos de cada patrón

### R2: Workflow Status Agent

**WHEN** el usuario ejecuta `/mj2:status`,
**THE SYSTEM SHALL** analizar el estado del proyecto y mostrar:
- Metadata del proyecto (nombre, versión, branch)
- Progreso del workflow por fases (✅ done, 🟡 in progress, ⏳ pending)
- Estado de la SPEC especificada (si se proporciona SPEC-ID)
- Próximo paso recomendado
- Tips de ayuda

**WHERE** los datos provienen de:
- `.mjcuadrado-net-sdk/config.json` - Metadata
- Git log - Commits por fase
- Coverage reports - `coverage.json` (si existe)
- TAG chain - @SPEC → @TEST → @CODE → @DOC

**SUCCESS CRITERIA:**
- Agent de ~300 líneas
- Command de ~150 líneas
- Output claro en español/inglés según config
- Detecta fase actual correctamente

### R3: Help Command

**WHEN** el usuario ejecuta `/mj2:help`,
**THE SYSTEM SHALL** mostrar:
- Lista de comandos principales (workflow)
- Lista de comandos adicionales
- Guía contextual según comando especificado
- Tips útiles

**SUCCESS CRITERIA:**
- Command de ~200 líneas
- Lista todos los 20+ comandos existentes
- Ayuda contextual por comando
- Output en español (default)

### R4: README.md Updated

**WHEN** un usuario lee README.md,
**THE SYSTEM SHALL** incluir sección "Mr. mj2" que explique:
- Qué es Mr. mj2 (orquestador conceptual)
- Qué agentes especializados coordina
- Workflow SPEC-First (0-project → 1-plan → 2-run → quality-check → 3-sync)
- Referencia a `/mj2:status` y `/mj2:help`

**SUCCESS CRITERIA:**
- Sección de ~50-80 líneas
- Diagrama visual del workflow
- Referencias a comandos

### R5: Agent Output Format

**WHEN** un agente completa su trabajo,
**THE SYSTEM SHALL** mostrar output con formato:
```
✅ [Acción] completada: [ID]

🤖 Mr. mj2 recomienda:
   1. [Próximo paso principal]
   2. [Paso alternativo si aplica]
   3. [Ver estado: /mj2:status]

📊 Estado actual:
   [Métricas relevantes]

💡 Tip: [Consejo útil]
```

**WHERE** se actualiza en estos agentes:
- project-manager.md
- spec-builder.md
- tdd-implementer.md
- quality-gate.md
- doc-syncer.md

**SUCCESS CRITERIA:**
- 5 agentes actualizados
- Formato consistente
- Guidance clara de próximos pasos

---

## 🔧 Technical Design

### File Structure

```
.claude/
├── skills/
│   └── mj2/
│       └── orchestration-patterns.md      # NEW (~400 líneas)
├── agents/
│   └── mj2/
│       ├── workflow-status.md             # NEW (~300 líneas)
│       ├── project-manager.md             # UPDATED (outputs)
│       ├── spec-builder.md                # UPDATED (outputs)
│       ├── tdd-implementer.md             # UPDATED (outputs)
│       ├── quality-gate.md                # UPDATED (outputs)
│       └── doc-syncer.md                  # UPDATED (outputs)
└── commands/
    ├── mj2-status.md                      # NEW (~150 líneas)
    └── mj2-help.md                        # NEW (~200 líneas)

README.md                                   # UPDATED (+ Mr. mj2 section)
```

### orchestration-patterns.md Structure

```markdown
# MJ² Orchestration Patterns

## Overview
[Qué es orquestación en mj2]

## Pattern 1: Sequential Workflow (Standard)
[Diagrama + explicación + ejemplo]

## Pattern 2: Quality Gate (Conditional)
[Diagrama + explicación + ejemplo]

## Pattern 3: Parallel Branches (Manual)
[Diagrama + explicación + ejemplo]

## Agent Responsibilities Matrix
[Tabla con 26 agentes y sus responsabilidades]

## Skills Loading Strategy
[Cómo los agentes cargan skills compartidas]

## User Intervention Points
[Cuándo el usuario debe intervenir]

## Workflow State Tracking
[Cómo usar TAG chain para tracking]
```

### workflow-status.md Agent Structure

```markdown
---
name: workflow-status
description: Analyzes project state and shows workflow progress
model: claude-sonnet-4-5-20250929
version: 1.0.0
tags: [mj2, orchestration, status]
---

# Workflow Status Agent

## Responsibilities
[Analizar estado del proyecto]

## Workflow
1. DETECT - Detectar fase actual
2. ANALYZE - Analizar progreso
3. FORMAT - Formatear output
4. RECOMMEND - Recomendar próximo paso

## Data Sources
[config.json, git log, coverage, TAG chain]

## Output Format
[Template de output]

## Examples
[Ejemplos de diferentes estados]
```

### mj2-status.md Command Structure

```yaml
---
name: /mj2:status
description: Show current workflow state
agent: mj2/workflow-status
---

# /mj2:status

[Usage, examples, output]
```

### mj2-help.md Command Structure

```yaml
---
name: /mj2:help
description: Show available commands and workflow guidance
---

# /mj2:help

[Commands list, examples, contextual help]
```

---

## 🧪 Test Strategy

### Manual Testing

**T1: `/mj2:status` en proyecto vacío**
```bash
$ /mj2:status
Expected: Mensaje indicando que proyecto no está inicializado
```

**T2: `/mj2:status` en proyecto inicializado**
```bash
$ /mj2:status
Expected: Estado del proyecto con fase 0 completa
```

**T3: `/mj2:status` con SPEC en progreso**
```bash
$ /mj2:status AUTH-001
Expected: Estado detallado de SPEC-AUTH-001
```

**T4: `/mj2:help` sin argumentos**
```bash
$ /mj2:help
Expected: Lista de todos los comandos
```

**T5: `/mj2:help` con comando específico**
```bash
$ /mj2:help workflow
Expected: Explicación detallada del workflow
```

**T6: Output mejorado de agentes**
```bash
$ /mj2:2-run AUTH-001
Expected: Output con formato "Mr. mj2 recomienda"
```

### Validation Checklist

- [ ] orchestration-patterns.md skill creado y válido
- [ ] workflow-status.md agent creado y funcional
- [ ] mj2-status.md command funciona correctamente
- [ ] mj2-help.md command funciona correctamente
- [ ] README.md actualizado con sección Mr. mj2
- [ ] 5 agentes core actualizados con nuevo formato
- [ ] Outputs son consistentes en español
- [ ] TAG chain: @SPEC:ORCH-064 presente en todos los archivos

---

## 📊 Success Metrics

### Quantitative

- [x] 1 skill creado (~400 líneas)
- [x] 1 agent creado (~300 líneas)
- [x] 2 commands creados (~350 líneas total)
- [x] 1 README actualizado (~50-80 líneas nuevas)
- [x] 5 agentes actualizados
- [x] ~1,200-1,500 líneas totales

### Qualitative

- [x] Concepto "Mr. mj2" claramente explicado
- [x] Usuarios entienden cómo usar `/mj2:status`
- [x] Usuarios entienden cómo usar `/mj2:help`
- [x] Outputs de agentes son más guiados y claros
- [x] Sin cambios en arquitectura actual
- [x] Documentación completa en español

---

## 🚫 Out of Scope

### NO Implementar

1. ❌ **Sintaxis `@agent-name`** - No soportado por Claude Code
2. ❌ **Ejecución paralela de agentes** - No soportado por Claude Code
3. ❌ **Agente "mr-mj2.md" ejecutable** - Rompe arquitectura
4. ❌ **Cambios en comandos existentes** - Solo outputs
5. ❌ **Auto-ejecución de comandos** - User-driven siempre

---

## 🔗 References

- **Analysis:** `.github/analysis/workflow-orchestration-analysis-2025-11-23.md`
- **Issue #64:** `.github/issues/issue-64.md`
- **Inspired by:** moai-adk "Mr. Alfred" (conceptual orchestrator)
- **Claude Code limitations:** Sequential execution, no inter-agent calls

---

## 📝 Implementation Notes

### Phase 1: Skills & Documentation (Day 1)
1. Crear orchestration-patterns.md skill
2. Actualizar README.md con Mr. mj2
3. Git: @SPEC:ORCH-064 tags

### Phase 2: Status Command (Day 1-2)
1. Crear workflow-status.md agent
2. Crear mj2-status.md command
3. Probar con diferentes estados
4. Git: @CODE:ORCH-064 tags

### Phase 3: Help Command (Day 2)
1. Crear mj2-help.md command
2. Documentar todos los comandos existentes
3. Probar contextual help
4. Git: @CODE:ORCH-064 tags

### Phase 4: Update Agent Outputs (Day 3)
1. Actualizar project-manager.md
2. Actualizar spec-builder.md
3. Actualizar tdd-implementer.md
4. Actualizar quality-gate.md
5. Actualizar doc-syncer.md
6. Git: @CODE:ORCH-064 tags

### Phase 5: Documentation Sync (Day 3-4)
1. Verificar TAG chain completa
2. Actualizar CHANGELOG.md
3. Probar workflow completo end-to-end
4. Git: @DOC:ORCH-064 tags

---

## ✅ Acceptance Criteria

### Must Have

- [x] orchestration-patterns.md skill (~400 líneas)
- [x] workflow-status.md agent (~300 líneas)
- [x] mj2-status.md command (~150 líneas)
- [x] mj2-help.md command (~200 líneas)
- [x] README.md sección "Mr. mj2" (~50-80 líneas)
- [x] 5 agentes core actualizados
- [x] `/mj2:status` funciona correctamente
- [x] `/mj2:help` funciona correctamente
- [x] Documentación en español
- [x] TAG chain: @SPEC:ORCH-064 → @CODE:ORCH-064 → @DOC:ORCH-064

### Nice to Have

- [ ] Actualizar más de 5 agentes (los 26 eventualmente)
- [ ] Agregar más ejemplos en orchestration-patterns.md
- [ ] Diagrams visuales en README.md (mermaid)
- [ ] Auto-detect idioma en outputs

---

**Version:** 1.0.0
**Status:** In Progress
**Next:** Implementar Phase 1 (Skills & Documentation)
