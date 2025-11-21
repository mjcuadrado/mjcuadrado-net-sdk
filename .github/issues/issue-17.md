# Issue #17: Foundation Skills

**Status:** ✅ Closed
**Created:** 2024-11-20
**Closed:** 2024-11-20
**Agent:** Multiple (used by all)
**Commit:** 0fca5cd

---

## Objetivo

Crear 5 Skills fundamentales en `.claude/skills/foundation/` que contengan el conocimiento base utilizado por TODOS los agentes del sistema mj2.

---

## Skills Creados

### 1. trust.md (943 líneas)
**Contenido:**
- TRUST 5 principles completos:
  - **T**est First (coverage ≥85%)
  - **R**eadable (métodos ≤50 líneas)
  - **U**nified (consistencia)
  - **S**ecured (bcrypt, validación)
  - **T**rackable (TAG system)
- Scripts de validación para cada principio
- Ejemplos con ✅ BIEN vs ❌ MAL
- Referencias a estándares OWASP, Clean Code

**Usado por:** quality-gate, tdd-implementer, doc-syncer

---

### 2. tags.md (511 líneas)
**Contenido:**
- Sistema TAG completo @SPEC → @TEST → @CODE → @DOC
- 4 tipos de TAGs:
  - `@SPEC:EX-{ID}:{REQ}` - Requisitos en specs
  - `@TEST:EX-{ID}:{REQ}` - Tests que validan
  - `@CODE:EX-{ID}:{REQ}` - Implementación
  - `@DOC:EX-{ID}` - Documentación
- Script de validación de cadena TAG
- Herramientas de búsqueda y reporte
- Nomenclatura de IDs y requisitos

**Usado por:** spec-builder, tdd-implementer, doc-syncer, quality-gate

---

### 3. specs.md (519 líneas)
**Contenido:**
- Formato SPEC estándar con 3 archivos:
  - `spec.md` - Requisitos en EARS + metadatos
  - `plan.md` - Plan de implementación por fases
  - `acceptance.md` - Criterios de aceptación testables
- Estructura de frontmatter YAML
- Metadatos: status, complexity, estimated_hours
- Scripts de validación de SPEC
- Ejemplos completos (SPEC-AUTH-001)

**Usado por:** spec-builder, quality-gate

---

### 4. ears.md (543 líneas)
**Contenido:**
- EARS (Easy Approach to Requirements Syntax) completo
- 5 tipos de requisitos:
  1. **Ubiquitous:** The system SHALL...
  2. **Event-driven:** WHEN... THEN... SHALL...
  3. **State-driven:** WHILE... THEN... SHALL...
  4. **Optional:** WHERE... MAY...
  5. **Complex:** WHILE... WHEN... THEN... SHALL...
- SHALL vs MAY usage
- Ejemplos por dominio (AUTH, USER, API)
- Script de validación de sintaxis EARS
- Checklist de validación

**Usado por:** spec-builder, quality-gate

---

### 5. git.md (722 líneas)
**Contenido:**
- Git workflows completos:
  - **Personal Mode:** Auto-merge a main, branches efímeras
  - **Team Mode:** Draft PR, code review, GitFlow
- Branch naming: `feature/SPEC-{ID}`, `bugfix/SPEC-{ID}`, `hotfix/SPEC-{ID}`
- Commit message format con emojis:
  - 🔴 test (RED phase)
  - 🟢 feat (GREEN phase)
  - ♻️ refactor (REFACTOR phase)
  - 📚 docs (documentation)
- Merge strategies: --no-ff vs --squash
- Resolución de conflictos
- Hooks y .gitignore
- Comandos útiles

**Usado por:** git-manager, quality-gate

---

## Estadísticas

| Métrica | Valor |
|---------|-------|
| Total líneas | 3,238 |
| Skills creados | 5 |
| Ejemplos incluidos | ~50+ |
| Scripts de validación | 8 |
| Referencias externas | 15+ |

---

## Filosofía de Delegación

```
Command → Agent → Skill
   ↓        ↓        ↓
 Simple  Orquesta  Knowledge
```

**Skills = Conocimiento reutilizable**
- Agents delegan conocimiento a Skills
- Skills contienen detalles, ejemplos, validaciones
- Un Skill puede ser usado por múltiples Agents

---

## Estructura de cada Skill

Todos los Skills foundation siguen esta estructura:

1. **YAML Frontmatter**
   - name, description, version, tags

2. **Introducción**
   - Qué es y para qué sirve

3. **Definiciones y Conceptos**
   - Explicación detallada

4. **Reglas y Convenciones**
   - Qué hacer y qué no hacer

5. **Ejemplos Completos**
   - ✅ BIEN: Código correcto con explicación
   - ❌ MAL: Antipatrones con explicación

6. **Formato en código**
   - Ejemplos de uso en C#, markdown, bash

7. **Scripts de Validación**
   - Bash scripts para verificar cumplimiento

8. **Herramientas**
   - Utilidades para trabajar con el concepto

9. **Troubleshooting**
   - Errores comunes y soluciones

10. **Referencias**
    - Enlaces a documentación externa

11. **Resumen**
    - Bullet points clave

---

## Uso por Agentes

### spec-builder
Usa: `ears.md`, `specs.md`, `tags.md`
- Para crear SPECs con requisitos en EARS
- Para estructurar spec.md, plan.md, acceptance.md
- Para agregar TAGs @SPEC correctamente

### tdd-implementer
Usa: `trust.md`, `tags.md`, `git.md`
- Para implementar siguiendo TRUST 5
- Para agregar TAGs @TEST y @CODE
- Para commits TDD (RED → GREEN → REFACTOR)

### doc-syncer
Usa: `tags.md`, `git.md`
- Para agregar TAGs @DOC
- Para commits de documentación
- Para mantener trazabilidad

### quality-gate
Usa: `trust.md`, `tags.md`, `specs.md`, `ears.md`
- Para validar TRUST 5 (coverage, readability, etc.)
- Para validar cadena TAG completa
- Para validar formato SPEC y EARS

### git-manager
Usa: `git.md`
- Para workflows Personal/Team
- Para branches y commits
- Para merges y PRs

---

## Validación Final

```bash
# Verificar que todos los Skills existen
ls -lh .claude/skills/foundation/

# trust.md   943 líneas
# tags.md    511 líneas
# specs.md   519 líneas
# ears.md    543 líneas
# git.md     722 líneas

# Total: 3,238 líneas de conocimiento
```

---

## Impacto

**Antes:**
- Agents contenían todo el conocimiento inline
- Duplicación de información
- Difícil mantener consistencia

**Después:**
- Agents delegan a Skills
- Conocimiento centralizado y reutilizable
- Fácil actualizar y extender
- Nueva filosofía: Command → Agent → Skill

---

## Próximos Pasos

1. ✅ Crear Foundation Skills (este issue)
2. ⏳ Actualizar agents para referenciar Skills
3. ⏳ Crear Skills específicos por dominio (auth, user, api)
4. ⏳ Crear Skills de testing (xUnit, FluentAssertions, Moq)
5. ⏳ Crear Skills de .NET 9 (minimal APIs, DI, configuration)

---

## Referencias

- Commit: 0fca5cd
- Files:
  - `.claude/skills/foundation/trust.md`
  - `.claude/skills/foundation/tags.md`
  - `.claude/skills/foundation/specs.md`
  - `.claude/skills/foundation/ears.md`
  - `.claude/skills/foundation/git.md`
- GitHub Issue: #17
- Related Issues: #14 (quality-gate), #15 (git-manager)

---

**mj2: Automated .NET 9 development with TRUST 5 principles**
