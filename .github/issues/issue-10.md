# Issue #10: Agente project-manager (mj2)

**Estado:** ✅ **COMPLETADO** (2024-11-20)

**Título:** Crear agente project-manager para inicialización de proyectos .NET 9

## 📋 Descripción

Crear el agente **project-manager** de mj2, inspirado en el project-manager de moai-adk, para inicializar proyectos .NET 9 con estructura mjcuadrado-net-sdk.

## 🎯 Objetivos

- [x] Crear agente project-manager.md
- [x] Implementar workflow de inicialización
- [x] Soporte multiidioma (es, en)
- [x] Integración con Skills
- [x] Modos INITIALIZE y OPTIMIZE

## 📝 Tareas técnicas

- [x] Crear estructura `.claude/agents/mj2/`
- [x] Crear archivo `project-manager.md`
- [x] Implementar Agent Persona
- [x] Implementar Language Handling (es, en)
- [x] Implementar Workflow de 5 fases:
  1. Detection and Analysis
  2. User Interview (6 preguntas)
  3. Structure Creation
  4. Git Configuration
  5. Skill Recommendations
- [x] Implementar Output Format (español e inglés)
- [x] Agregar 2+ ejemplos de uso
- [x] Documentar Constraints (hard y soft)
- [x] Documentar Integration points
- [x] Documentar Metrics
- [x] Agregar Troubleshooting (3+ errores comunes)
- [x] Agregar Referencias a Skills
- [x] Mantener ≤800 líneas (actual: 239 líneas)

## ✅ Criterios de aceptación

- [x] Archivo `.claude/agents/mj2/project-manager.md` creado
- [x] Tiene ≤800 líneas (239 ✅)
- [x] YAML frontmatter completo y válido
- [x] 12 secciones principales presentes
- [x] Agent Persona definido
- [x] Language Handling implementado (es, en)
- [x] Workflow de 5 fases documentado
- [x] 2+ ejemplos incluidos
- [x] Constraints documentados
- [x] Integration points definidos
- [x] Troubleshooting con 3+ errores comunes
- [x] Referencias a Skills incluidas
- [x] No duplica contenido de Skills
- [x] Enfocado en orquestación

## 🧪 Validación realizada

### Validación de estructura
```
✅ Archivo existe
✅ 239 líneas (30% del límite de 800)
✅ YAML frontmatter válido
✅ 12/12 secciones obligatorias presentes
✅ Idiomas: es + en (sin coreano)
✅ Referencias a 4 Skills
✅ No duplica contenido de Skills
✅ Enfocado en orquestación
✅ Delega conocimiento a Skills
✅ Bloques de código: 10 pequeños, 1 mediano
✅ Sin bloques grandes (>30 líneas)
```

## 🔗 Dependencias

- Depende de: Issues #1-#9 (Fase 1 MVP completada)
- Prepara para: Issue #11 (spec-builder)

## 📚 Referencias

- [moai-adk project-manager](https://github.com/modu-ai/moai-adk/.claude/agents/alfred/)
- [TRUST 5 Principles](../../skills/foundation/trust.md)
- [TAG System](../../skills/foundation/tags.md)
- [SPEC Format](../../skills/foundation/specs.md)

## 🏷️ Labels sugeridas

`phase-2`, `mj2`, `agents`, `initialization`

---

## 📊 Resumen de cierre

**Fecha de cierre:** 2024-11-20
**Estado:** ✅ COMPLETADO

### Agente implementado

**Archivo:** `.claude/agents/mj2/project-manager.md` (239 líneas)

### Características del agente

**Modos de operación:**
1. **INITIALIZE** - Inicialización de proyectos nuevos con entrevista completa
2. **OPTIMIZE** - Análisis y mejoras de proyectos existentes

**Responsabilidades principales:**
- Inicializar proyectos .NET 9 con estructura mjcuadrado-net-sdk
- Entrevistar al usuario (6 preguntas configuración)
- Crear estructura `.mjcuadrado-net-sdk/`
- Generar `config.json` personalizado
- Recomendar Skills apropiados según configuración
- Configurar Git automáticamente

**Workflow de 5 fases:**
1. **Detection and Analysis** - Detecta si es proyecto nuevo u optimización
2. **User Interview** - 6 preguntas (nombre, descripción, framework, db, git strategy, idioma)
3. **Structure Creation** - Crea carpetas, config.json, documentación
4. **Git Configuration** - Inicializa repo, crea .gitignore, commit inicial
5. **Skill Recommendations** - Analiza config y recomienda Skills

**Idiomas soportados:**
- Español (es) - por defecto
- English (en)

**Integración:**
- CLI: `mjcuadrado-net-sdk init [proyecto]`
- Claude Code: `/mj2:0-project`
- Prepara proyecto para: spec-builder, tdd-implementer

**Skills integrados:**
- Auto-carga: `foundation/trust`, `foundation/tags`, `foundation/specs`, `dotnet/csharp`
- Recomienda: `dotnet/ef-core` (si db), `foundation/git` (si team mode)

### Arquitectura validada

**Filosofía mj2:** ✅ Agente corto + Skills robustos

**Delegación correcta:**
- `foundation/trust.md` → TRUST 5 principles
- `foundation/tags.md` → TAG system
- `foundation/specs.md` → SPEC format
- `foundation/ears.md` → EARS syntax
- `dotnet/csharp.md` → C# conventions

**Responsabilidad del agente:**
- Workflow de inicialización ✓
- Orquestación de Skills ✓
- Ejemplos simples y concisos ✓

### Métricas

**Tamaño:**
- 239 líneas (30% del límite de 800)
- 10 bloques de código pequeños
- 1 bloque mediano (config.json)
- 0 bloques grandes

**Cobertura:**
- 12/12 secciones obligatorias
- 2 ejemplos completos
- 3 errores comunes documentados
- 4 Skills referenciados

### Ejemplos incluidos

**Ejemplo 1: New project initialization**
- Input: `/mj2:0-project`
- Output: Proyecto inicializado con estructura completa

**Ejemplo 2: Optimize existing project**
- Input: `/mj2:0-project` (proyecto existente)
- Output: Análisis y sugerencias de mejora

### Archivos creados

- ✅ `.claude/agents/mj2/project-manager.md` (239 líneas)

### Commit

**Commit:** `ba5d08b`
**Mensaje:** `feat(mj2): add project-manager agent`
**Push:** ✅ Exitoso a `origin/main`

### Próximos pasos

Issue completado exitosamente. Próxima tarea:
- **Issue #11:** spec-builder agent (límite 800 líneas)
- **Issue #12:** tdd-implementer agent
- **Issue #13:** doc-syncer agent

---

**Fase 2 iniciada:** Sistema de agentes mj2 para desarrollo SPEC-First
