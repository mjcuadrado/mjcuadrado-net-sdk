# Issue #54: Implementation Planner Agent

**Fecha:** 2025-11-23
**Prioridad:** 🔴 Alta
**Estado:** 📋 Planificado
**Versión:** v0.6.0
**Branch:** feature/ISSUE-054-implementation-planner
**Tiempo Estimado:** 5-6 días

---

## 📋 Descripción

Crear agente **implementation-planner** para planning detallado de implementación, complementando spec-builder con planificación técnica exhaustiva.

**Gap identificado:** moai-adk tiene este agente, mj2 no. Es útil para planificar implementaciones complejas antes de ejecutar tdd-implementer.

---

## 🎯 Objetivos

### 1. Implementation Planner Agent
- Crear `.claude/agents/mj2/implementation-planner.md` (~700 líneas)
  - TRUST 5 principles aplicados
  - Workflow de 4 fases: ANALYZE → PLAN → BREAK_DOWN → VALIDATE
  - Análisis de dependencias y orden de implementación
  - Identificación de riesgos técnicos
  - Estimación de complejidad
  - Planning de arquitectura y diseño
  - Integration con otros agentes (spec-builder, tdd-implementer)

### 2. Comando Slash
- Crear `.claude/commands/mj2-plan-impl.md` (~200 líneas)
  - Sintaxis: `/mj2:plan-impl <SPEC-ID>`
  - Opciones: --detail (basic, medium, detailed)
  - Output: Implementation plan detallado

### 3. Documentación
- Crear `.github/issues/issue-54.md`
- Actualizar README.md con nuevo agente
- Actualizar ROADMAP.md

---

## 📦 Entregables

### 1. implementation-planner.md Agent
```markdown
## 🎭 Agent Persona

Soy el **Arquitecto de Implementación**. Meticuloso, analítico, y estratégico.

Mi misión es transformar SPECs en planes de implementación ejecutables:
- Analizar complejidad técnica
- Identificar dependencias
- Planificar orden de implementación
- Estimar esfuerzo y riesgos
- Diseñar arquitectura técnica
```

**Workflow:**
1. **ANALYZE** - Analizar SPEC y contexto técnico
2. **PLAN** - Diseñar estrategia de implementación
3. **BREAK_DOWN** - Dividir en tareas específicas
4. **VALIDATE** - Validar plan con quality-gate

### 2. mj2-plan-impl.md Command
```bash
# Básico
/mj2:plan-impl AUTH-001

# Detallado
/mj2:plan-impl AUTH-001 --detail detailed

# Con validación
/mj2:plan-impl AUTH-001 --validate
```

### 3. Implementation Plan Output
```markdown
# Implementation Plan: SPEC-AUTH-001

## 1. Overview
- **SPEC:** User Authentication with JWT
- **Complexity:** Medium-High
- **Estimated Time:** 12-15 hours
- **Risk Level:** Medium

## 2. Technical Analysis
- **Dependencies:**
  - JWT library (System.IdentityModel.Tokens.Jwt)
  - Password hashing (BCrypt.Net)
  - Database (User table)

- **Architectural Decisions:**
  - Use Repository pattern for User access
  - Implement JWT service interface
  - Use Result pattern for error handling

## 3. Implementation Phases

### Phase 1: Database Setup (2-3h)
1. Create User entity
2. Create UserRepository interface and implementation
3. Add migration
4. Seed test data

### Phase 2: JWT Service (3-4h)
1. Create IJwtService interface
2. Implement JWT generation
3. Implement token validation
4. Add refresh token support

### Phase 3: Authentication Logic (4-5h)
1. Create AuthService
2. Implement Login endpoint
3. Implement password validation
4. Add rate limiting

### Phase 4: Testing (3h)
1. Unit tests for AuthService
2. Integration tests with Testcontainers
3. E2E tests with Playwright

## 4. Risks & Mitigation
- **Risk:** Refresh token storage
  - **Mitigation:** Use distributed cache (Redis)

- **Risk:** Token expiration handling
  - **Mitigation:** Implement automatic refresh

## 5. Quality Gates
- [ ] 90% code coverage
- [ ] All tests passing
- [ ] Security OWASP ASVS compliance
- [ ] Performance < 100ms

## 6. Integration Points
- [ ] tdd-implementer: Execute implementation
- [ ] security-expert: Validate auth security
- [ ] quality-gate: Validate coverage
- [ ] doc-syncer: Update documentation
```

---

## ✅ Criterios de Éxito

- [ ] implementation-planner.md agent creado (~700 líneas)
- [ ] /mj2:plan-impl command creado (~200 líneas)
- [ ] Workflow 4 fases implementado
- [ ] Integration con spec-builder y tdd-implementer
- [ ] Plan output incluye:
  - [ ] Análisis de complejidad
  - [ ] Dependencias identificadas
  - [ ] Fases de implementación
  - [ ] Estimaciones de tiempo
  - [ ] Riesgos y mitigaciones
  - [ ] Quality gates
- [ ] Documentación completa
- [ ] Ejemplos funcionales (3+)

---

## 🔧 Workflow Integration

```
spec-builder → implementation-planner → tdd-implementer → quality-gate
     ↓                    ↓                    ↓                ↓
   SPEC              IMPL PLAN            CODE+TESTS       VALIDATION
```

**Uso típico:**
1. `/mj2:1-plan` → Crear SPEC
2. `/mj2:plan-impl` → Crear implementation plan **← NUEVO**
3. `/mj2:2-run` → Implementar con TDD
4. `/mj2:quality-check` → Validar calidad

---

## 📊 Métricas

- **Archivos creados:** 3 (1 agent + 1 command + 1 doc)
- **Líneas totales:** ~900
- **Workflow phases:** 4 (ANALYZE, PLAN, BREAK_DOWN, VALIDATE)
- **Integration points:** 4 (spec-builder, tdd-implementer, quality-gate, doc-syncer)

---

## 🔗 Referencias

- **Inspirado en:** moai-adk/implementation-planner
- **Complementa:** spec-builder (SPEC), tdd-implementer (CODE)
- **Skills usados:** architecture/*, foundation/trust, mj2/workflow-core

---

## 🚀 Impacto

**Sin implementation-planner:**
- ❌ Salto directo de SPEC a CODE sin planning
- ❌ Riesgos no identificados temprano
- ❌ Estimaciones incorrectas
- ❌ Orden de implementación subóptimo

**Con implementation-planner:**
- ✅ Planning técnico detallado antes de CODE
- ✅ Riesgos identificados y mitigados
- ✅ Estimaciones realistas
- ✅ Orden de implementación optimizado
- ✅ Mejor preparación para tdd-implementer

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Prioridad:** 🔴 ALTA (gap crítico vs moai-adk)
**Milestone:** v0.6.0
