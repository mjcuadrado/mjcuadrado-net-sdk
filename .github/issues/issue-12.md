# Issue #12: Agente tdd-implementer (mj2)

**Estado:** ✅ **COMPLETADO** (2024-11-20)

**Título:** Crear agente tdd-implementer para ejecutar ciclo TDD completo

## 📋 Descripción

Crear el agente **tdd-implementer** de mj2, el más crítico del sistema, para ejecutar el ciclo completo TDD (RED-GREEN-REFACTOR) en proyectos .NET 9.

## 🎯 Objetivos

- [x] Crear agente tdd-implementer.md
- [x] Implementar ciclo TDD completo (RED-GREEN-REFACTOR)
- [x] Aplicar TRUST 5 principles
- [x] Validar coverage ≥85%
- [x] Commits automáticos con emojis
- [x] Máxima delegación a Skills

## 📝 Tareas técnicas

- [x] Crear archivo `.claude/agents/mj2/tdd-implementer.md`
- [x] Implementar Agent Persona (Maestro TDD)
- [x] Implementar Language Handling (es, en)
- [x] Implementar Workflow de 4 fases:
  - Phase 0: Preparation
  - Phase 1: RED (failing tests)
  - Phase 2: GREEN (minimal implementation)
  - Phase 3: REFACTOR (quality improvements)
  - Phase 4: Quality Gate
- [x] Sistema de commits con emojis (🔴 🟢 ♻️)
- [x] Validación TRUST 5 completa
- [x] Validación coverage ≥85%
- [x] Integración con xUnit + FluentAssertions
- [x] Mantener ≤800 líneas (actual: 517)
- [x] Máxima delegación a Skills

## ✅ Criterios de aceptación

- [x] Archivo `.claude/agents/mj2/tdd-implementer.md` creado
- [x] Tiene ≤800 líneas (517 ✅)
- [x] YAML frontmatter completo y válido
- [x] 12 secciones principales presentes
- [x] Agent Persona definido
- [x] Language Handling implementado (es, en)
- [x] Workflow RED-GREEN-REFACTOR documentado
- [x] NO duplica contenido de dotnet/xunit.md
- [x] NO duplica contenido de dotnet/csharp.md
- [x] NO duplica contenido de foundation/trust.md
- [x] Referencias claras a Skills críticos
- [x] Commits con emojis documentados
- [x] Coverage ≥85% requirement documentado
- [x] TRUST 5 validation documentado

## 🧪 Validación realizada

### Validación de estructura
```
✅ Archivo existe
✅ 517 líneas (65% del límite de 800)
✅ YAML frontmatter válido
✅ 12 secciones principales presentes
✅ Idiomas: es + en
✅ 34 referencias a Skills críticos
✅ NO duplica contenido de Skills
✅ Enfocado en orquestación TDD
✅ Delega conocimiento técnico a Skills
```

## 🔗 Dependencias

- Depende de: Issue #11 (spec-builder)
- Prepara para: Issue #13 (doc-syncer)
- **Este es el agente MÁS CRÍTICO del sistema mj2**

## 📚 Referencias

- [TDD by Example - Kent Beck](https://www.amazon.com/Test-Driven-Development-Kent-Beck/dp/0321146530)
- [xUnit Documentation](https://xunit.net/)
- [FluentAssertions](https://fluentassertions.com/)
- [dotnet/xunit.md](../../skills/dotnet/xunit.md)
- [dotnet/csharp.md](../../skills/dotnet/csharp.md)
- [foundation/trust.md](../../skills/foundation/trust.md)

## 🏷️ Labels sugeridas

`phase-2`, `mj2`, `agents`, `tdd`, `testing`, `critical`

---

## 📊 Resumen de cierre

**Fecha de cierre:** 2024-11-20
**Estado:** ✅ COMPLETADO

### Agente implementado

**Archivo:** `.claude/agents/mj2/tdd-implementer.md` (517 líneas)

**Este es el agente MÁS CRÍTICO del sistema mj2** - Sin él, no hay TDD real.

### Características del agente

**Filosofía inflexible:**
- RED primero, siempre - No hay código sin test que falle
- GREEN mínimo - Haz que pase, no más
- REFACTOR sin piedad - El código perfecto es código refactorizado
- 85% coverage o no hay merge - Sin excepciones

**Responsabilidades principales:**
1. **RED Phase** - Write failing tests, load xUnit patterns, add @TEST: tags, commit 🔴
2. **GREEN Phase** - Minimal implementation, load C# conventions, add @CODE: tags, commit 🟢
3. **REFACTOR Phase** - Improve quality, apply TRUST 5, verify coverage ≥85%, commit ♻️
4. **Quality Validation** - Trigger quality-gate, validate TRUST 5, generate coverage report

**Workflow de 4 fases:**

**Phase 0: Preparation**
- Load SPEC from docs/specs/SPEC-{ID}/spec.md
- Parse requirements (@SPEC: tags)
- Load Skills (xunit, csharp, trust, tags)
- Analyze complexity

**Phase 1: 🔴 RED**
- Create test file structure
- Generate comprehensive tests (using dotnet/xunit.md patterns)
- Run tests → Expect FAIL
- Verify tests fail correctly
- Commit with 🔴 emoji

**Phase 2: 🟢 GREEN**
- Create code file structure
- Implement MINIMAL code (using dotnet/csharp.md conventions)
- Run tests → Expect PASS
- Verify all tests pass
- Commit with 🟢 emoji

**Phase 3: ♻️ REFACTOR**
- Load TRUST 5 principles (from foundation/trust.md)
- Apply refactoring patterns (from dotnet/csharp.md)
- Keep tests passing
- Verify TRUST 5 compliance
- Run coverage → Verify ≥85%
- Commit with ♻️ emoji

**Phase 4: Quality Gate**
- Trigger quality-gate agent
- Generate final report
- Output summary with next steps

**Idiomas soportados:**
- Español (es) - por defecto
- English (en)

**Integración:**
- CLI: `mjcuadrado-net-sdk tdd run SPEC-ID`
- Claude Code: `/mj2:2-run SPEC-ID`
- Recibe: SPEC from spec-builder
- Triggers: quality-gate agent
- Envía: Results to doc-syncer

**Skills críticos integrados:**
- `dotnet/xunit.md` - Patrones completos de testing con xUnit y FluentAssertions
- `dotnet/csharp.md` - Convenciones completas de C# y refactoring
- `foundation/trust.md` - TRUST 5 principles completos
- `foundation/tags.md` - Sistema TAG (@TEST:, @CODE:, @SPEC:, @DOC:)

### Arquitectura validada

**Filosofía mj2:** ✅ Agente corto + Skills robustos

**Delegación máxima:**
- NO duplica: Patrones xUnit completos (va en dotnet/xunit.md)
- NO duplica: Convenciones C# completas (va en dotnet/csharp.md)
- NO duplica: TRUST 5 principles completos (va en foundation/trust.md)
- NO duplica: Sistema TAG completo (va en foundation/tags.md)
- SÍ tiene: Workflow TDD paso a paso
- SÍ tiene: Cuándo cargar cada Skill
- SÍ tiene: Cómo aplicar patrones DE los Skills
- SÍ tiene: Commits y git workflow
- SÍ tiene: 2 ejemplos simples con referencias

**Responsabilidad del agente:**
- Orchestar el ciclo TDD completo ✓
- Cargar y usar Skills apropiados ✓
- Validar calidad (TRUST 5, coverage) ✓
- Generar commits con emojis ✓
- Ejemplos simples que referencian Skills ✓

### Métricas

**Tamaño:**
- 517 líneas (65% del límite de 800)
- 34 referencias explícitas a Skills
- 2 ejemplos completos (simple + complejo)

**Cobertura:**
- 12/12 secciones obligatorias
- 2 ejemplos (simple feature + complex feature)
- 3 errores comunes documentados
- 4 Skills críticos referenciados

**Validación:**
- ✅ No duplica contenido de dotnet/xunit.md
- ✅ No duplica contenido de dotnet/csharp.md
- ✅ No duplica contenido de foundation/trust.md
- ✅ No duplica contenido de foundation/tags.md
- ✅ Referencias claras a Skills para detalles

### Commits con emojis

El agente genera automáticamente 3 commits por SPEC:

1. **🔴 RED**: `test(SPEC-ID): add failing tests`
2. **🟢 GREEN**: `feat(SPEC-ID): implement minimal solution`
3. **♻️ REFACTOR**: `refactor(SPEC-ID): improve code quality`

### Ejemplos incluidos

**Ejemplo 1: Simple Feature**
- Input: `/mj2:2-run AUTH-001`
- Process: Load SPEC → RED (5 tests FAIL) → GREEN (tests PASS) → REFACTOR (87% coverage)
- Output: 3 commits, 87% coverage, Next: /mj2:3-sync

**Ejemplo 2: Complex Feature**
- Input: `/mj2:2-run USER-003`
- 12 requirements → 18 tests (3 files) → 4 code files → DI + async + docs → 89% coverage

### Constraints documentados

**Hard Constraints (MUST):**
- ⛔ NEVER skip RED phase
- ⛔ NEVER write code before tests
- ⛔ NEVER commit if tests failing (except RED)
- ⛔ NEVER commit if coverage <85%
- ⛔ ALWAYS add @TEST: and @CODE: tags

**Soft Constraints (SHOULD):**
- ⚠️ Methods ≤50 lines
- ⚠️ Use async/await
- ⚠️ Dependency Injection
- ⚠️ XML documentation

### Archivos creados

- ✅ `.claude/agents/mj2/tdd-implementer.md` (517 líneas)

### Commits

**Commit:** `cfe8429`
**Mensaje:** `feat(mj2): add tdd-implementer agent`
**Push:** ✅ Exitoso a `origin/main`

### Próximos pasos

Issue completado exitosamente. Próxima tarea:
- **Issue #13:** doc-syncer agent (último agente base del sistema mj2)

---

**Fase 2 en progreso:** Sistema de agentes mj2 (3/4 agentes completados)
- ✅ project-manager (239 líneas)
- ✅ spec-builder (452 líneas)
- ✅ tdd-implementer (517 líneas) - **AGENTE CRÍTICO**
- ⏳ doc-syncer (próximo y último)

**Sin este agente, no hay TDD real. Es el corazón de mj2.** ❤️
