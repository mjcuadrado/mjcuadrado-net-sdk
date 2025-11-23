---
name: /mj2:status
description: Show current workflow state
agent: mj2/workflow-status
---

# /mj2:status

Muestra el estado actual del workflow SPEC-First y guía sobre el próximo paso.

**Tags:** @CODE:ORCH-064

## Usage

```bash
/mj2:status                # Estado general del proyecto
/mj2:status SPEC-ID        # Estado de SPEC específica
```

## Examples

### Example 1: Estado General

```bash
$ /mj2:status

🤖 Mr. mj2 - Workflow Status

📊 Proyecto: my-api (v0.1.0)
🌿 Branch: main

Workflow Progress:
✅ Phase 0: Proyecto inicializado (2025-11-20)
✅ Phase 1: SPEC-AUTH-001 creada (2025-11-21)
⏳ Phase 2: Implementación pendiente
⏳ Phase 3: Quality check pendiente
⏳ Phase 4: Documentación pendiente

🎯 Próximo paso:
   Implementar SPEC: /mj2:2-run AUTH-001

💡 Tip: Usa /mj2:help workflow para ver el proceso completo
```

### Example 2: Estado de SPEC Específica

```bash
$ /mj2:status AUTH-001

🤖 Mr. mj2 - Workflow Status: SPEC-AUTH-001

📊 Proyecto: my-api (v0.1.0)
🌿 Branch: feature/SPEC-AUTH-001

Workflow Progress:
✅ Phase 0: Proyecto inicializado (2025-11-20)
✅ Phase 1: SPEC-AUTH-001 creada (2025-11-21)
🟡 Phase 2: Implementación en progreso
   Tests: 4/4 passing ✅
   Coverage: 87% ✅ (≥85%)
   TRUST 5: Pendiente validación
   TAG chain: @SPEC ✅ @TEST ✅ @CODE ✅
⏳ Phase 3: Quality check pendiente
⏳ Phase 4: Documentación pendiente

🎯 Próximo paso:
   Ejecutar quality check: /mj2:quality-check AUTH-001

📊 Estado detallado:
   Commits: 3 (RED, GREEN, REFACTOR)
   Última actualización: 2025-11-23 10:30
   Branch: feature/SPEC-AUTH-001

💡 Tip: Quality check valida coverage, tests, y TRUST 5 principles
```

### Example 3: Proyecto No Inicializado

```bash
$ /mj2:status

⚠️ Proyecto no inicializado

🤖 Mr. mj2 recomienda:
   1. Inicializar proyecto: /mj2:0-project <nombre>
   2. Ver ayuda: /mj2:help workflow

💡 Tip: El workflow SPEC-First comienza con /mj2:0-project
```

## What it does

1. **Detecta estado del proyecto**
   - Verifica si `.mjcuadrado-net-sdk/` existe
   - Lee metadata de `config.json`

2. **Analiza progreso del workflow**
   - Phase 0: Proyecto inicializado
   - Phase 1: SPEC creada
   - Phase 2: Implementación (TDD cycle)
   - Phase 3: Quality check
   - Phase 4: Docs sync

3. **Verifica datos**
   - Git log (commits con TAGs)
   - Coverage reports
   - Quality gate reports
   - TAG chain (@SPEC → @TEST → @CODE → @DOC)

4. **Muestra output estructurado**
   - Estado por fase (✅ done, 🟡 in progress, ⏳ pending, ❌ failed)
   - Próximo paso recomendado
   - Tips contextuales

## Output Symbols

- ✅ **Completado** - Fase terminada exitosamente
- 🟡 **En progreso** - Trabajando en esta fase
- ⏳ **Pendiente** - Aún no iniciada
- ❌ **Fallido** - Fase con errores (requiere fix)

## Agent

Delegates to: `.claude/agents/mj2/workflow-status.md`

Loads Skills:
- mj2/orchestration-patterns.md (patrones de orquestación)

## Integration

**Usado después de:**
- Cualquier comando del workflow
- Para verificar progreso
- Para decidir próximo paso

**Referencias desde:**
- Outputs de agentes ("Ver estado: /mj2:status")
- `/mj2:help` (comando recomendado)
- README.md (comandos útiles)

## Tips

💡 Ejecuta `/mj2:status` frecuentemente para mantener visibilidad del progreso

💡 Si tienes múltiples SPECs, usa `/mj2:status SPEC-ID` para análisis específico

💡 Combina con `/mj2:help` para guía completa del workflow

## Related Commands

- `/mj2:help` - Guía de comandos disponibles
- `/mj2:help workflow` - Explicación del workflow SPEC-First
- `/mj2:0-project` - Inicializar proyecto
- `/mj2:1-plan` - Crear SPEC
- `/mj2:2-run` - Implementar SPEC
- `/mj2:quality-check` - Validar calidad
- `/mj2:3-sync` - Sincronizar docs

---

**Version:** 1.0.0
**Created:** 2025-11-23
**Tags:** @CODE:ORCH-064
