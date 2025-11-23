# Issue #56: Docs Manager Agent

**Fecha:** 2025-11-23
**Prioridad:** 🟡 Media
**Estado:** 📋 Planificado
**Versión:** v0.6.0
**Branch:** feature/ISSUE-056-docs-manager
**Tiempo Estimado:** 5-6 días

---

## 📋 Descripción

Crear agente **docs-manager** para gestión completa de documentación, más amplio que doc-syncer actual.

**Gap identificado:** moai-adk tiene este agente. mj2 tiene doc-syncer pero es más limitado (solo sync). docs-manager orquesta toda la documentación del proyecto.

---

## 🎯 Objetivos

### 1. Docs Manager Agent
- Crear `.claude/agents/mj2/docs-manager.md` (~750 líneas)
  - TRUST 5 principles
  - Workflow: AUDIT → UPDATE → GENERATE → PUBLISH
  - Gestión de README, CHANGELOG, API docs, Architecture docs
  - Integration con doc-syncer
  - Documentation standards enforcement

### 2. Comando Slash
- Crear `.claude/commands/mj2-docs.md` (~200 líneas)
  - Sintaxis: `/mj2:docs <action>`
  - Actions: audit, update, generate, publish

---

## 📦 Entregables

### 1. docs-manager.md Agent
**Responsibilities:**
- README.md maintenance
- CHANGELOG.md generation
- API documentation (Swagger/OpenAPI)
- Architecture diagrams (C4 model)
- ADRs (Architecture Decision Records)
- Contributing guidelines
- Code of Conduct

**Workflow:**
1. **AUDIT** - Verificar estado de docs
2. **UPDATE** - Actualizar docs existentes
3. **GENERATE** - Generar docs faltantes
4. **PUBLISH** - Publicar a GitHub Pages

### 2. Documentation Templates
```markdown
# ADR Template
## Status: [Proposed|Accepted|Deprecated]
## Context
## Decision
## Consequences
```

### 3. Integration
- doc-syncer: TAG chain sync
- api-designer: API docs
- release-manager: CHANGELOG

---

## ✅ Criterios de Éxito

- [ ] docs-manager.md agent creado (~750 líneas)
- [ ] /mj2:docs command creado (~200 líneas)
- [ ] Documentation audit completo
- [ ] Templates para README, ADR, CHANGELOG
- [ ] Integration con doc-syncer
- [ ] Auto-update en releases
- [ ] GitHub Pages support

---

## 🔗 Referencias

- **Inspirado en:** moai-adk/docs-manager
- **Complementa:** doc-syncer (TAG sync)
- **Tools:** Markdown, Mermaid, Swagger

---

## 🚀 Impacto

**Sin docs-manager:**
- ❌ Docs desactualizados
- ❌ Inconsistencias entre docs
- ❌ Manual documentation

**Con docs-manager:**
- ✅ Docs siempre actualizados
- ✅ Consistencia garantizada
- ✅ Auto-generation de docs
- ✅ Professional documentation

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Prioridad:** 🟡 MEDIA
**Milestone:** v0.6.0
