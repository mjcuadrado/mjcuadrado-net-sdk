---
spec_id: SPEC-DOC-001
title: Documentation Sync & Audit
domain: DOC
status: draft
created: 2024-11-24
author: @mjcuadrado
tags: [documentation, audit, sync, consistency]
complexity: medium
estimated_hours: 16-20
issue: "#53"
---

# SPEC-DOC-001: Documentation Sync & Audit

## Overview
<!-- @SPEC:DOC-001 -->

Revisión completa y sincronización de toda la documentación del proyecto (README, ROADMAP, issues) para corregir incoherencias, actualizar métricas y reflejar el estado real del proyecto tras completar Issues #44-50.

**Problema:** Documentación contradictoria entre README, ROADMAP y realidad del código.

**Solución:** Auditoría completa + actualización sistemática de toda la documentación.

## Stakeholders

- **Desarrolladores:** Documentación precisa para entender el proyecto
- **Usuarios:** README claro con métricas reales
- **Contribuidores:** ROADMAP actualizado para saber qué hacer
- **Mantenedores:** Coherencia para facilitar mantenimiento

## Requirements

### Functional Requirements

#### FR-1: Auditoría de Métricas (Ubiquitous)
**@SPEC:DOC-001:FR-1**

The system SHALL count and document actual metrics:
- Skills reales en `.claude/skills/**/*.md`
- Agentes reales en `.claude/agents/mj2/*.md`
- Comandos reales en `.claude/commands/*.md`
- Tests y coverage actuales

#### FR-2: Actualización README.md (Event-driven)
**@SPEC:DOC-001:FR-2**

WHEN metrics are audited
THEN system SHALL update README.md with:
- Status correcto v0.5.0 (X/9 completados)
- Issue #50 con Python hooks (no shell scripts)
- Badge de versión actual
- Lista completa de comandos (14+)
- Métricas reales (agentes, skills, comandos)
- Tablas actualizadas

#### FR-3: Actualización ROADMAP.md (Event-driven)
**@SPEC:DOC-001:FR-3**

WHEN README.md is updated
THEN system SHALL update ROADMAP.md with:
- Issue #50 con métricas v2.0.0 (Python)
- Gap Analysis con números reales
- Tablas de features actualizadas
- Status correcto Issue #41 (SKIPPED) y #47 (postponed)

#### FR-4: Resolución de Issues Pendientes (State-driven)
**@SPEC:DOC-001:FR-4**

WHILE issues #41 and #47 are in inconsistent state
THEN system SHALL resolve:
- Issue #41: Marcar como WONTFIX/SKIPPED definitivo
- Issue #47: Crear con status postponed o implementar

#### FR-5: Sincronización CHANGELOG (Event-driven)
**@SPEC:DOC-001:FR-5**

WHEN all documentation is updated
THEN system SHALL update CHANGELOG.md with:
- Entry for Issue #53
- List of documentation changes
- @DOC:DOC-001 tag

#### FR-6: Badge de Versión (Ubiquitous)
**@SPEC:DOC-001:FR-6**

The system SHALL add version badge to README.md showing current SDK version.

### Non-Functional Requirements

#### NFR-1: Consistencia (Constraint)
**@SPEC:DOC-001:NFR-1**

Documentation SHALL NOT contain contradictory information.
All metrics SHALL match actual code.

#### NFR-2: Completitud
**@SPEC:DOC-001:NFR-2**

Documentation SHALL list ALL:
- Commands available (no omissions)
- Skills available (no omissions)
- Agents available (no omissions)

#### NFR-3: Trazabilidad
**@SPEC:DOC-001:NFR-3**

All changes SHALL be documented with @DOC:DOC-001 tag.

## Dependencies

- **Filesystem:** Access to `.claude/` directory for counting
- **Git:** For commits and tagging
- **GitHub CLI:** For issue management (#41, #47)

## Risks

1. **Risk:** Métricas cambian durante auditoría
   - **Mitigation:** Hacer auditoría rápida en una sesión

2. **Risk:** Olvidar actualizar algún archivo
   - **Mitigation:** Checklist completa en acceptance.md

3. **Risk:** Issues #41/#47 requieren decisiones
   - **Mitigation:** Decisiones claras antes de comenzar

## Acceptance Criteria

Ver archivo `acceptance.md` para criterios detallados.

## References

- Issue #53: `.github/issues/issue-53.md`
- foundation/tags.md - TAG system
- doc-syncer.md agent - Documentation sync patterns

---

**SPEC Status:** Draft
**Next Step:** Create implementation plan (plan.md)
