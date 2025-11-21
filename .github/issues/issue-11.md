# Issue #11: Agente spec-builder (mj2)

**Estado:** ✅ **COMPLETADO** (2024-11-20)

**Título:** Crear agente spec-builder para construcción de SPECs en formato EARS

## 📋 Descripción

Crear el agente **spec-builder** de mj2, inspirado en el spec-builder de moai-adk, para construir especificaciones en formato EARS para proyectos .NET 9.

## 🎯 Objetivos

- [x] Crear agente spec-builder.md
- [x] Implementar sistema de análisis de requisitos
- [x] Implementar preguntas clarificadoras
- [x] Auto-detección de dominio
- [x] Generación de SPEC en formato EARS
- [x] Integración con Git

## 📝 Tareas técnicas

- [x] Crear archivo `.claude/agents/mj2/spec-builder.md`
- [x] Implementar Agent Persona (Analista de requisitos)
- [x] Implementar Language Handling (es, en)
- [x] Implementar Workflow de 4 fases:
  1. Analysis and Clarification
  2. SPEC Document Generation
  3. Git Integration
  4. Summary and Next Steps
- [x] Sistema de preguntas clarificadoras por dominio
- [x] Generación automática de SPEC ID
- [x] Creación de spec.md usando EARS (desde Skill)
- [x] Creación de plan.md con fases
- [x] Creación de acceptance.md con criterios
- [x] Integración Git (feature branches, PRs)
- [x] Mantener ≤800 líneas (actual: 452)
- [x] Máxima delegación a Skills

## ✅ Criterios de aceptación

- [x] Archivo `.claude/agents/mj2/spec-builder.md` creado
- [x] Tiene ≤800 líneas (452 ✅)
- [x] YAML frontmatter completo y válido
- [x] 12 secciones principales presentes
- [x] Agent Persona definido
- [x] Language Handling implementado (es, en)
- [x] Workflow de 4 fases documentado
- [x] NO duplica contenido de Skills
- [x] Referencias claras a foundation/specs.md
- [x] Referencias claras a foundation/ears.md
- [x] Referencias claras a foundation/tags.md
- [x] 2 ejemplos incluidos
- [x] Constraints documentados
- [x] Integration points definidos
- [x] Troubleshooting con 3 errores comunes

## 🧪 Validación realizada

### Validación de estructura
```
✅ Archivo existe
✅ 452 líneas (56% del límite de 800)
✅ YAML frontmatter válido
✅ 12 secciones principales presentes
✅ Idiomas: es + en
✅ 21 referencias a Skills
✅ NO duplica contenido de Skills
✅ Enfocado en orquestación
✅ Delega conocimiento a Skills
```

## 🔗 Dependencias

- Depende de: Issue #10 (project-manager)
- Prepara para: Issue #12 (tdd-implementer)

## 📚 Referencias

- [moai-adk spec-builder](https://github.com/modu-ai/moai-adk)
- [EARS ISO/IEC/IEEE 29148](https://en.wikipedia.org/wiki/Software_requirements_specification)
- [foundation/specs.md](../../skills/foundation/specs.md)
- [foundation/ears.md](../../skills/foundation/ears.md)
- [foundation/tags.md](../../skills/foundation/tags.md)

## 🏷️ Labels sugeridas

`phase-2`, `mj2`, `agents`, `specification`, `ears`

---

## 📊 Resumen de cierre

**Fecha de cierre:** 2024-11-20
**Estado:** ✅ COMPLETADO

### Agente implementado

**Archivo:** `.claude/agents/mj2/spec-builder.md` (452 líneas)

### Características del agente

**Responsabilidades principales:**
1. **Requirement Analysis** - Analiza feature description, identifica gaps, hace preguntas, detecta dominio
2. **SPEC Generation** - Genera SPEC-{DOMAIN}-{NNN} usando formato EARS (desde Skill)
3. **Planning** - Crea plan de implementación con fases y dependencias
4. **Acceptance Criteria** - Define criterios testables mapeados a EARS
5. **Git Integration** - Crea feature branch, commit inicial, PR si team mode

**Workflow de 4 fases:**
1. **Analysis and Clarification** - Parse input, load Skills, ask questions, detect domain, generate ID
2. **SPEC Document Generation** - Load EARS patterns, create structure, generate spec.md/plan.md/acceptance.md
3. **Git Integration** - Create branch, commit files, create Draft PR (team mode)
4. **Summary** - Output summary with next steps

**Idiomas soportados:**
- Español (es) - por defecto
- English (en)

**Dominios soportados:**
- AUTH, USER, ADMIN, DATA, API, UI, CORE, [Custom]

**Integración:**
- CLI: `mjcuadrado-net-sdk spec new {ID}`
- Claude Code: `/mj2:1-plan "feature description"`
- Recibe de: project-manager
- Envía a: tdd-implementer

**Skills integrados (CRÍTICOS):**
- `foundation/specs.md` - Estructura completa de SPEC
- `foundation/ears.md` - Patrones EARS completos
- `foundation/tags.md` - Sistema TAG (@SPEC:, @TEST:, @CODE:, @DOC:)
- `foundation/git.md` - Workflows Git (condicional)

### Arquitectura validada

**Filosofía mj2:** ✅ Agente corto + Skills robustos

**Delegación correcta:**
- NO duplica: Formato SPEC completo (va en foundation/specs.md)
- NO duplica: Sintaxis EARS completa (va en foundation/ears.md)
- NO duplica: Sistema TAG completo (va en foundation/tags.md)
- SÍ tiene: Workflow de cómo USAR esos Skills
- SÍ tiene: Preguntas clarificadoras
- SÍ tiene: Cuándo cargar cada Skill
- SÍ tiene: Cómo generar SPEC usando Skills
- SÍ tiene: 1-2 ejemplos simples con referencias

**Responsabilidad del agente:**
- Workflow de construcción de SPEC ✓
- Orquestación de Skills ✓
- Sistema de preguntas clarificadoras ✓
- Ejemplos simples que referencian Skills ✓

### Métricas

**Tamaño:**
- 452 líneas (56% del límite de 800)
- 21 referencias explícitas a Skills
- 2 ejemplos completos

**Cobertura:**
- 12/12 secciones obligatorias
- 2 ejemplos (CRUD simple + integración compleja)
- 3 errores comunes documentados
- 4 Skills referenciados

**Validación:**
- ✅ No duplica contenido de foundation/specs.md
- ✅ No duplica contenido de foundation/ears.md
- ✅ No duplica contenido de foundation/tags.md
- ✅ Referencias claras a Skills para detalles

### Ejemplos incluidos

**Ejemplo 1: Simple CRUD Feature**
- Input: "User profile management - view and edit"
- Dominio detectado: USER
- SPEC ID: SPEC-USER-001
- Plan: 3 fases
- Output: SPEC completa con next steps

**Ejemplo 2: Complex Integration**
- Input: "Payment processing with Stripe"
- Dominio detectado: API
- SPEC ID: SPEC-API-001
- Plan: 5 fases (setup, payment, webhook, refund, testing)
- Output: SPEC compleja con referencias Stripe

### Archivos creados

- ✅ `.claude/agents/mj2/spec-builder.md` (452 líneas)

### Commits

**Commit:** `c9debce`
**Mensaje:** `feat(mj2): add spec-builder agent`
**Push:** ✅ Exitoso a `origin/main`

### Próximos pasos

Issue completado exitosamente. Próxima tarea:
- **Issue #12:** tdd-implementer agent (implementación TDD guiada por SPECs)
- **Issue #13:** doc-syncer agent (sincronización de documentación)

---

**Fase 2 en progreso:** Sistema de agentes mj2 (2/4 agentes completados)
- ✅ project-manager
- ✅ spec-builder
- ⏳ tdd-implementer (próximo)
- ⏳ doc-syncer
