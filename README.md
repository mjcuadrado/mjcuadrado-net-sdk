# mjcuadrado-net-sdk

SDK para desarrollo automatizado con IA, inspirado en [moai-adk](https://github.com/modu-ai/moai-adk).

[![.NET](https://img.shields.io/badge/.NET-10.0-blue)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)
[![CI](https://github.com/mjcuadrado/mjcuadrado-net-sdk/workflows/CI/badge.svg)](https://github.com/mjcuadrado/mjcuadrado-net-sdk/actions)

## Descripción

**mjcuadrado-net-sdk** es un SDK en .NET que automatiza y estructura el desarrollo de software siguiendo la metodología:

**SPEC → TEST → CODE → DOC**

Inspirado en la filosofía de [moai-adk](https://github.com/modu-ai/moai-adk), este SDK proporciona:
- Sistema de especificaciones (SPECs) con formato EARS
- Sistema de trazabilidad con TAGs (`@SPEC:`, `@TEST:`, `@CODE:`, `@DOC:`)
- CLI para gestión de proyectos
- Integración con Claude Code (agentes, comandos, skills, hooks)
- Preparado para EF Core (SQL Server / PostgreSQL) en futuras fases

## Características

### v0.1.0 - Core System - ✅ COMPLETADA (Issues #1-22)

- ✅ Estructura de proyecto completa y automatizada
- ✅ CLI funcional con Spectre.Console
- ✅ Comando `init` para inicializar proyectos
- ✅ Comando `doctor` para diagnóstico del sistema
- ✅ Comando `version` para ver versión del SDK
- ✅ Sistema de templates embebidos
- ✅ Configuración centralizada en `config.json`
- ✅ Tests unitarios (195/195 passing, 100%)
- ✅ 6 agentes mj2 (doc-syncer, git-manager, project-manager, quality-gate, spec-builder, tdd-implementer)
- ✅ 7 comandos (/mj2:0-project, 1-plan, 2-run, 3-sync, git-merge, quality-check)
- ✅ 11 skills foundation (.NET, testing, architecture)
- ✅ Workflow TDD estricto (RED → GREEN → REFACTOR)

### v0.2.0 - Frontend Foundation - ✅ COMPLETADA (Issues #24-32)

**Architecture Patterns** ✅ Issues #24-26
- ✅ Clean Architecture, CQRS, DDD, Vertical Slice, Result Pattern skills

**Testing Infrastructure** ✅ Issue #27
- ✅ Testcontainers skill para integration tests

**Frontend Core** ✅ Issues #28-30
- ✅ React 18 & TypeScript 5 skills (Issue #28)
- ✅ Vite & Material UI v6 skills (Issue #29)
- ✅ State Management: Zod, React Hook Form, TanStack Query, openapi-typescript (Issue #30)

**Frontend Agent** ✅ Issue #31
- ✅ frontend-builder agent (Component-Driven Development)
- ✅ /mj2:2f-build command (TEST → COMPONENT → STYLE → REFACTOR)

**E2E Testing** ✅ Issue #32
- ✅ Playwright skill (E2E testing, visual regression, accessibility)
- ✅ e2e-tester agent (PLAN → GENERATE → EXECUTE → REPORT)
- ✅ /mj2:4-e2e command
- ✅ **Testing Pyramid COMPLETA**: Unit → Integration → Component → E2E

### v0.3.0 - Full Stack + DevOps + Observability + Database - ✅ COMPLETADA (Issues #33-38)

**Frontend Testing** ✅ Issue #33
- ✅ Vitest skill (Framework de testing moderno)
- ✅ React Testing Library skill (Testing user-centric)

**DevOps Foundation** ✅ Issues #34-35
- ✅ Docker skill (Containerización completa, 86% reducción tamaño)
- ✅ Docker Compose skill (Orquestación multi-contenedor)
- ✅ devops-expert agent (PLAN → BUILD → DEPLOY → VERIFY)
- ✅ /mj2:5-deploy command (Blue-Green, Rolling, Canary)

**CI/CD Automation** ✅ Issue #36
- ✅ GitHub Actions skill (33 jobs, 3 deployment strategies)
- ✅ Workflow templates (backend-ci, frontend-ci, e2e-ci, cd)

**Observability Stack** ✅ Issue #37
- ✅ OpenTelemetry skill (Traces, Metrics, Logs)
- ✅ Grafana skill (Dashboards y alerting)
- ✅ Serilog skill (Structured logging)

**Database Expertise** ✅ Issue #38
- ✅ SQL Server skill (SQL Server 2022+ con EF Core 9)
- ✅ PostgreSQL + SQL Server en database-expert agent
- ✅ /mj2:db-migrate command (gestión de migraciones)

### v0.4.0 - Advanced Features - ✅ COMPLETADA (Issues #39-43)

**Security** ✅ Issue #39
- ✅ JWT, OWASP ASVS, Rate Limiting skills (~1,080 líneas)
- ✅ security-expert agent (~730 líneas)
- ✅ OWASP Top 10:2021 + ASVS nivel 2 coverage

**API Design** ✅ Issue #40
- ✅ api-designer agent (680 líneas)
- ✅ /mj2:api-design command (210 líneas)
- ✅ RESTful patterns, OpenAPI, versioning, pagination

**Performance** ✅ Issue #42
- ✅ performance-optimization skill (650+ líneas)
- ✅ caching-strategies skill (800+ líneas)
- ✅ performance-engineer agent (750+ líneas)
- ✅ /mj2:perf-analyze command (600+ líneas)

**Accessibility** ✅ Issue #43
- ✅ accessibility skill (1,000+ líneas - WCAG 2.1 Level AA)
- ✅ accessibility-expert agent (850+ líneas)
- ✅ /mj2:a11y-audit command (650+ líneas)

### v0.5.0 - System Evolution - 🚧 EN PROGRESO (Issues #44-52)

**Feedback & Learning** ✅ Issue #44
- ✅ feedback-manager agent (437 líneas)
- ✅ /mj2:9-feedback command (96 líneas)
- ✅ .mj2/memory/ sistema de persistencia
- ✅ 4 execution rules predefinidas
- ✅ 4 common error patterns

**Agent & Skill Factory** ✅ Issue #45 - 🚀 GAME CHANGER
- ✅ agent-factory meta-agente (683 líneas)
- ✅ skill-factory meta-agente (826 líneas)
- ✅ /mj2:create-agent command (373 líneas)
- ✅ /mj2:create-skill command (527 líneas)
- ✅ 9 dominios, 5 workflow patterns, 3 niveles
- ✅ **mj2 es ahora extensible por usuarios**

**Release Management** ✅ Issue #46
- ✅ release-manager agent (892 líneas)
- ✅ /mj2:99-release command (565 líneas)
- ✅ Semantic versioning automático
- ✅ CHANGELOG generation
- ✅ Pre-release validation (6 checks)
- ✅ GitHub Release integration

**Debug & Migration Helpers** ✅ Issue #48
- ✅ debug-helper agent (768 líneas)
- ✅ migration-expert agent (185 líneas)
- ✅ /mj2:debug command (73 líneas)
- ✅ /mj2:migrate command (57 líneas)
- ✅ Debugging sistemático
- ✅ Migration strategies (3)

**Component Designer** ✅ Issue #49
- ✅ component-designer agent (750+ líneas)
- ✅ /mj2:design-component command (450+ líneas)
- ✅ Design-First approach con WCAG 2.2 AA
- ✅ 4 design patterns (Atomic, Compound, Render Props, Hooks)
- ✅ Design tokens system
- ✅ Integración con frontend-builder

**Advanced Hooks System** ✅ Issue #50
- ✅ 6 hook templates (pre-command, post-command, on-spec-created, etc.)
- ✅ 4 ejemplos funcionales (Slack, S3 backup, metrics, coverage)
- ✅ 8 eventos soportados
- ✅ config.json con configuración de hooks
- ✅ Integración con workflow MJ²
- ✅ Extensibilidad completa

**Próximos Issues**
- 📋 Output Styles (#51)

## Instalación

### Requisitos previos

- **.NET SDK 9.0 o superior** (se recomienda .NET 10)
- **Git** configurado

### Instalación desde source

```bash
# Clonar repositorio
git clone https://github.com/mjcuadrado/mjcuadrado-net-sdk.git
cd mjcuadrado-net-sdk

# Restaurar dependencias
dotnet restore

# Compilar
dotnet build

# Ejecutar
dotnet run --project src/MjCuadrado.NetSdk/MjCuadrado.NetSdk.csproj -- version
```

### Verificar instalación

```bash
dotnet run --project src/MjCuadrado.NetSdk/MjCuadrado.NetSdk.csproj -- doctor
```

## Quick Start

### 1. Inicializar un nuevo proyecto

```bash
# Crear un nuevo proyecto
dotnet run --project src/MjCuadrado.NetSdk/MjCuadrado.NetSdk.csproj -- init mi-proyecto

# O inicializar en el directorio actual
cd mi-proyecto-existente
dotnet run --project /ruta/al/sdk/src/MjCuadrado.NetSdk/MjCuadrado.NetSdk.csproj -- init
```

### 2. Verificar el proyecto

```bash
dotnet run --project /ruta/al/sdk/src/MjCuadrado.NetSdk/MjCuadrado.NetSdk.csproj -- doctor
```

### 3. Estructura generada

```
mi-proyecto/
├── .mjcuadrado-net-sdk/
│   ├── config.json              # Configuración del proyecto
│   ├── project/
│   │   ├── product.md          # Definición del producto
│   │   ├── structure.md        # Arquitectura
│   │   └── tech.md             # Stack técnico
│   ├── specs/                  # Especificaciones EARS
│   ├── memory/                 # Contexto para IA
│   └── reports/                # Reportes generados
└── .claude/
    ├── agents/                 # Agentes de Claude Code
    ├── commands/               # Slash commands
    ├── skills/                 # Skills especializadas
    └── hooks/                  # Hooks automáticos
```

## Comandos disponibles

| Comando | Descripción | Ejemplo |
|---------|-------------|---------|
| `init [nombre]` | Inicializa un nuevo proyecto | `init mi-proyecto` |
| `doctor` | Verifica dependencias del sistema | `doctor --verbose` |
| `version` | Muestra la versión del SDK | `version --verbose` |

Ver documentación completa de comandos en [`docs/commands/`](docs/commands/).

## Metodología SPEC → TEST → CODE → DOC

### 1. SPEC: Especificaciones con formato EARS

```markdown
---
id: AUTH-001
title: Login de usuario
priority: high
---

# @SPEC:EX-AUTH-001

## Event-driven
CUANDO el usuario envíe credenciales válidas,
el sistema DEBE generar un token JWT válido por 24 horas.

## Constraints
- El sistema DEBE hashear contraseñas con bcrypt
- El sistema DEBE bloquear la cuenta tras 5 intentos fallidos
```

### 2. TEST: Tests vinculados a SPECs

```csharp
// @TEST:EX-AUTH-001
[Fact]
public void Login_WithValidCredentials_ReturnsJwtToken()
{
    // Test implementation
}
```

### 3. CODE: Código vinculado a SPECs

```csharp
// @CODE:EX-AUTH-001
public string Login(string email, string password)
{
    // Implementation
}
```

### 4. DOC: Documentación vinculada

```markdown
# @DOC:EX-AUTH-001
Documentación del sistema de autenticación...
```

## Desarrollo

### Setup de desarrollo

```bash
# Clonar el repositorio
git clone https://github.com/mjcuadrado/mjcuadrado-net-sdk.git
cd mjcuadrado-net-sdk

# Restaurar dependencias
dotnet restore

# Compilar
dotnet build

# Ejecutar tests
dotnet test

# Ejecutar con hot reload
dotnet watch run --project src/MjCuadrado.NetSdk/MjCuadrado.NetSdk.csproj
```

### Estándares de código

- Seguir [convenciones C# de Microsoft](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Usar nullable reference types
- Coverage objetivo: ≥ 85%
- Documentar métodos públicos con XML comments

### Contribuir

Ver [CONTRIBUTING.md](docs/contributing.md) para detalles sobre cómo contribuir al proyecto.

## Roadmap

Ver [ROADMAP.md](docs/ROADMAP.md) completo para detalles.

### v0.1.0: Core System ✅ COMPLETADA (Issues #1-22)
- [x] Estructura base del proyecto
- [x] Comandos CLI (init, doctor, version)
- [x] 6 agentes mj2
- [x] 7 comandos slash
- [x] 11 skills foundation
- [x] Workflow TDD (RED → GREEN → REFACTOR)
- [x] 195 tests unitarios (100% passing)
- [x] CI/CD configurado

### v0.2.0: Frontend Foundation ✅ COMPLETADA (Issues #24-32)
- [x] Architecture Patterns skills (Issues #24-26)
- [x] Testcontainers skill (Issue #27)
- [x] React 18 & TypeScript 5 skills (Issue #28)
- [x] Vite & Material UI v6 skills (Issue #29)
- [x] State Management skills (Issue #30)
- [x] frontend-builder agent (Issue #31)
- [x] Playwright E2E testing (Issue #32)
- [x] **Testing Pyramid completa**

### v0.3.0: Full Stack + DevOps 🚧 EN PROGRESO (Issues #33-38)

**Frontend Testing Detail** ✅ Issue #33
- ✅ Vitest skill (Framework de testing moderno con Vite)
- ✅ React Testing Library skill (Testing user-centric)
- ✅ Patrones de testing y best practices
- ✅ 100% contenido en español

**Docker Foundation** ✅ Issue #34
- ✅ Docker skill (811 líneas) - Containerización, multi-stage builds, security
- ✅ Docker Compose skill (913 líneas) - Orquestación multi-contenedor
- ✅ Templates: Dockerfile.dotnet, Dockerfile.react, docker-compose.fullstack.yml
- ✅ Optimización 86% en tamaño de imágenes
- ✅ Security hardening completo

**DevOps Expert Agent** ✅ Issue #35
- ✅ devops-expert agent (696 líneas) - Orquestación de deployment y CI/CD
- ✅ /mj2:5-deploy command (444 líneas) - Deployment automatizado
- ✅ 3 deployment strategies: Blue-Green, Rolling, Canary
- ✅ Security, monitoring, rollback automation
- ✅ Workflow de 4 fases: PLAN → BUILD → DEPLOY → VERIFY

**GitHub Actions CI/CD** ✅ Issue #36
- ✅ github-actions.md skill (418 líneas) - CI/CD completo con GitHub Actions
- ✅ backend-ci.yml (380+ líneas) - CI para .NET backend
- ✅ frontend-ci.yml (370+ líneas) - CI para React frontend
- ✅ e2e-ci.yml (450+ líneas) - E2E tests multi-browser
- ✅ cd.yml (490+ líneas) - Continuous Deployment automatizado
- ✅ 33 jobs totales, 3 deployment strategies
- ✅ Caching, security scanning, rollback automation

**OpenTelemetry Stack (Observability)** ✅ Issue #37
- ✅ opentelemetry.md skill (434 líneas) - Traces, Metrics, Logs completo
- ✅ grafana.md skill (365 líneas) - Dashboards, alerting, visualización
- ✅ serilog.md skill (318 líneas) - Structured logging con OTel integration
- ✅ 3 telemetry signals (Traces, Metrics, Logs)
- ✅ Collector configuration, exporters, correlation automática
- ✅ Stack completo: Jaeger + Prometheus + Loki + Grafana

**Database Expert Agent** ✅ Issue #38
- ✅ sqlserver.md skill (442 líneas) - SQL Server 2022+ con EF Core 9
- ✅ database-expert.md agent (665 líneas) - Experto PostgreSQL + SQL Server
- ✅ mj2-db-migrate.md command (180 líneas) - Gestión de migraciones
- ✅ 2 RDBMS completos (PostgreSQL + SQL Server)
- ✅ Migration strategies: Expand-Contract, Blue-Green, Rolling
- ✅ Database patterns: Aggregate, Soft Delete, Audit Trail

**v0.3.0 Full Stack + DevOps:** ✅ **COMPLETA** (Issues #33-38)

### v0.4.0: Advanced Features 🚧 EN PROGRESO (Issues #39-43)

**Security Expert** ✅ Issue #39
- ✅ jwt.md skill (370 líneas) - JWT + Refresh Tokens, claims-based auth
- ✅ owasp-asvs.md skill (430 líneas) - OWASP ASVS nivel 2 completo
- ✅ rate-limiting.md skill (280 líneas) - Rate limiting y DDoS protection
- ✅ security-expert.md agent (730 líneas) - Security auditing y threat modeling
- ✅ OWASP Top 10:2021 mitigación completa
- ✅ Workflow de 4 fases: ASSESS → DESIGN → IMPLEMENT → VERIFY

**API Designer Agent** ✅ Issue #40
- ✅ api-designer.md agent (680 líneas) - RESTful API design best practices
- ✅ mj2-api-design.md command (210 líneas) - API design automation
- ✅ REST constraints y resource modeling
- ✅ OpenAPI/Swagger documentation completa
- ✅ API versioning strategies (URL, Header, Query)
- ✅ Pagination (offset y cursor), filtering, sorting
- ✅ RFC 7807 Problem Details error handling
- ✅ Workflow de 4 fases: ANALYZE → DESIGN → DOCUMENT → VALIDATE

**Project Templates** 📋 Issue #41
- 📋 Clean Architecture template
- 📋 Vertical Slice template
- 📋 Full-stack React + .NET template

**Performance Engineer** ✅ Issue #42
- ✅ performance-optimization.md skill (650+ líneas) - Backend & Frontend optimization
- ✅ caching-strategies.md skill (800+ líneas) - In-Memory, Distributed, CDN caching
- ✅ performance-engineer.md agent (750+ líneas) - Performance analysis y profiling
- ✅ /mj2:perf-analyze command (600+ líneas) - Performance audit automation
- ✅ EF Core optimization: AsNoTracking, projections, bulk operations
- ✅ React optimization: Code splitting, memoization, virtual scrolling
- ✅ Caching patterns: Cache-aside, read-through, write-through
- ✅ Workflow de 4 fases: MEASURE → ANALYZE → OPTIMIZE → VALIDATE

**Accessibility Expert** ✅ Issue #43
- ✅ accessibility.md skill (1000+ líneas) - WCAG 2.1 Level AA completo
- ✅ accessibility-expert.md agent (850+ líneas) - A11y auditing y testing
- ✅ /mj2:a11y-audit command (650+ líneas) - Accessibility audit automation
- ✅ WCAG 2.1 Principles: Perceivable, Operable, Understandable, Robust
- ✅ Semantic HTML: Landmarks, headings, lists, tables
- ✅ ARIA patterns: Dialog, tabs, accordion, dropdown (25+ patterns)
- ✅ Keyboard navigation: Focus management, shortcuts, skip links
- ✅ Screen reader support: NVDA, JAWS, VoiceOver
- ✅ Color contrast: 4.5:1 (text), 3:1 (UI components)
- ✅ Form accessibility: Labels, errors, validation
- ✅ Testing tools: axe-core, Lighthouse, Playwright a11y
- ✅ Workflow de 4 fases: AUDIT → IDENTIFY → IMPLEMENT → TEST

### v0.5.0: Multi-language & Integrations
- [ ] Multi-language support
- [ ] MCP integrations
- [ ] BaaS providers

## Arquitectura

Ver documentación detallada de arquitectura en:
- [Visión general](docs/architecture/overview.md)
- [Fase 1 - MVP](docs/architecture/phase-1-mvp.md)

## Documentación

- [Arquitectura](docs/architecture/)
- [Comandos](docs/commands/)
- [Cómo contribuir](docs/contributing.md)

## Issues y desarrollo iterativo

El desarrollo sigue un enfoque iterativo documentado en GitHub Issues:

### v0.1.0 - Core System ✅ (Issues #1-22)
Ver documentación completa en `.github/issues/issue-*.md`

### v0.2.0 - Frontend Foundation ✅ (Issues #24-32)
- [#24-26 - Architecture Patterns](/.github/issues/issue-26.md)
- [#27 - Testcontainers](/.github/issues/issue-27.md)
- [#28 - React & TypeScript](/.github/issues/issue-28.md)
- [#29 - Vite & MUI](/.github/issues/issue-29.md)
- [#30 - State Management](/.github/issues/issue-30.md)
- [#31 - Frontend Builder Agent](/.github/issues/issue-31.md)
- [#32 - Playwright E2E Testing](/.github/issues/issue-32.md)

### v0.3.0 - Full Stack + DevOps ✅ COMPLETA (Issues #33-38)
- [#33 - Frontend Testing Stack](/.github/issues/issue-33.md) ✅
- [#34 - Docker Foundation](/.github/issues/issue-34.md) ✅
- [#35 - DevOps Expert Agent](/.github/issues/issue-35.md) ✅
- [#36 - GitHub Actions CI/CD](/.github/issues/issue-36.md) ✅
- [#37 - OpenTelemetry Stack](/.github/issues/issue-37.md) ✅
- [#38 - Database Expert Agent](/.github/issues/issue-38.md) ✅

**Versión 0.3.0 completada:** Full-stack + DevOps + Observability + Database

### v0.4.0 - Advanced Features ✅ COMPLETA (Issues #39-43)
- [#39 - Security Expert](/.github/issues/issue-39.md) ✅
- [#40 - API Designer Agent](/.github/issues/issue-40.md) ✅
- #41 - Project Templates (SKIPPED - postponed)
- [#42 - Performance Engineer](/.github/issues/issue-42.md) ✅
- [#43 - Accessibility Expert](/.github/issues/issue-43.md) ✅

**Versión 0.4.0 completada:** Security + API Design + Performance + Accessibility

## Inspiración

Este proyecto está inspirado en [moai-adk](https://github.com/modu-ai/moai-adk), adaptando su filosofía y metodología al ecosistema .NET.

## Licencia

[MIT License](LICENSE)

## Autor

**@mjcuadrado**

---

**¿Preguntas o sugerencias?** Abre un [issue](https://github.com/mjcuadrado/mjcuadrado-net-sdk/issues) en GitHub.
