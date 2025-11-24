# mjcuadrado-net-sdk

SDK para desarrollo automatizado con IA, inspirado en [moai-adk](https://github.com/modu-ai/moai-adk).

[![Version](https://img.shields.io/badge/version-0.5.0--rc-orange)](https://github.com/mjcuadrado/mjcuadrado-net-sdk/releases)
[![.NET](https://img.shields.io/badge/.NET-10.0-blue)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MPL--2.0-orange)](LICENSE)
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

## 🤖 Mr. mj2 - Tu Asistente de Desarrollo

**Mr. mj2** es el orquestador conceptual que coordina todos los agentes especializados de mjcuadrado-net-sdk.

Cuando usas mj2, **Mr. mj2** entiende tu intención y delega el trabajo a los expertos apropiados:

- 🏗️ **Project Manager** - Inicializa proyectos con estructura óptima
- 📋 **SPEC Builder** - Convierte ideas en especificaciones EARS
- 🧩 **Implementation Planner** - Transforma SPECs en planes ejecutables con task breakdown
- 🔴🟢♻️ **TDD Implementer** - Ejecuta el ciclo RED-GREEN-REFACTOR
- ✅ **Quality Gate** - Valida que el código cumple TRUST 5 principles
- 📚 **Doc Syncer** - Mantiene documentación sincronizada automáticamente
- 📖 **Docs Manager** - Gestiona documentación completa (README, CHANGELOG, API docs, ADRs)
- 🎨 **Frontend Builder** - Desarrolla componentes React con TDD
- 🧪 **E2E Tester** - Orquesta tests end-to-end con Playwright
- 🚀 **DevOps Expert** - Gestiona despliegues y CI/CD
- 🔒 **Security Expert** - Audita seguridad (OWASP, JWT, rate limiting)
- ⚡ **Performance Engineer** - Optimiza rendimiento (backend y frontend)
- ♿ **Accessibility Expert** - Valida WCAG 2.1 AA compliance
- 🎯 **API Designer** - Diseña APIs RESTful con OpenAPI
- 🎨 **Format Expert** - Automatiza code formatting y linting (C#, TypeScript, JavaScript)
- 🎨 **UI/UX Expert** - Diseña experiencias de usuario con research, wireframes y usability testing
- ... y 10 agentes más especializados

**Mr. mj2 nunca trabaja solo - orquesta expertos para cada tarea.**

### 🔄 Workflow SPEC-First

```
0️⃣  /mj2:0-project    →  Inicializar proyecto
1️⃣  /mj2:1-plan       →  Crear SPEC (Plan)
2️⃣  /mj2:2-run        →  Implementar con TDD (Run)
3️⃣  /mj2:quality-check →  Validar calidad
4️⃣  /mj2:3-sync       →  Sincronizar docs (Sync)
```

**Cada fase guía a la siguiente. Cada agente indica el próximo paso.**

💡 **Comandos útiles:**
- `/mj2:status` - Ver estado del workflow en tiempo real
- `/mj2:help` - Guía de comandos disponibles
- `/mj2:help workflow` - Explicación detallada del workflow

📖 **Más info:** Ver [orchestration-patterns.md](.claude/skills/mj2/orchestration-patterns.md) para patrones de orquestación completos.

---

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
- ✅ **26 agentes mj2** - Core system + especialistas (frontend, DevOps, security, performance, docs, planning, formatting, UX, etc.)
- ✅ **26 comandos slash** - Workflow completo automatizado
- ✅ **49 skills** - Backend, Frontend, Architecture, Testing, DevOps, Security, Tools
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

### v0.5.0 - System Evolution - 🟢 CASI COMPLETA 8/9 (Issues #44-52, #56, #64)

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

**Advanced Hooks System** ✅ Issue #50 (v2.0.0 - Python)
- ✅ **Python hooks** para cross-platform (Windows, macOS, Linux)
- ✅ 6 hook templates Python (pre_command, post_command, on_spec_created, etc.)
- ✅ 4 ejemplos funcionales (slack_notification, spec_backup, metrics_tracker, coverage_reporter)
- ✅ 8 eventos soportados (pre/post-command, on-spec-created/updated, on-sync-done, on-test-run, on-deploy, on-release)
- ✅ Python 3.8+ required (pip install requests boto3)
- ✅ config.json con configuración de hooks
- ✅ Integración con workflow MJ²
- ✅ Extensibilidad completa

**Workflow Orchestrator & "Mr. mj2"** ✅ Issue #64
- ✅ Concepto "Mr. mj2" documentado en README (orquestador conceptual)
- ✅ /mj2:status command (170 líneas) - Estado del workflow en tiempo real
- ✅ /mj2:help command (323 líneas) - Guía contextual de 20+ comandos
- ✅ orchestration-patterns.md skill (520 líneas) - 3 patrones de orquestación
- ✅ workflow-status.md agent (430 líneas) - Analiza estado del proyecto
- ✅ 5 agentes core actualizados con formato "Mr. mj2 recomienda"
- ✅ UX mejorada con guidance completa en cada fase
- ✅ TAG chain completa (@SPEC → @CODE → @DOC)

**Docs Manager Agent** ✅ Issue #56
- ✅ docs-manager agent (750+ líneas) - Complete documentation management
- ✅ /mj2:docs command (380+ líneas) - 4 actions: audit, update, generate, publish
- ✅ 4-phase workflow: AUDIT → UPDATE → GENERATE → PUBLISH
- ✅ README.md management (badges, sections, examples)
- ✅ CHANGELOG.md generation (Keep a Changelog format)
- ✅ API documentation (OpenAPI/Swagger)
- ✅ Architecture docs (C4 diagrams, ADRs)
- ✅ 5 documentation templates (README, CHANGELOG, ADR, CONTRIBUTING, CODE_OF_CONDUCT)
- ✅ GitHub Pages publishing support
- ✅ Integration with doc-syncer, api-designer, release-manager, quality-gate

**Issues Pendientes** (v0.5.0)
- 📋 Personalization System (#47) - Postponed
- 📋 Output Styles (#51)
- 📋 MCP Integrations (#52) - Evaluación

### v0.6.0 - Essential Agents - ✅ COMPLETADA 2/2 (Issues #54-55)

**Implementation Planner** ✅ Issue #54
- ✅ implementation-planner agent (750+ líneas) - Transform SPECs into executable plans
- ✅ /mj2:plan-impl command (470+ líneas) - Generate implementation plans
- ✅ 4-phase workflow: ANALYZE → PLAN → BREAK_DOWN → VALIDATE
- ✅ SPEC analysis (requirements extraction, context analysis)
- ✅ Technical planning (architecture, stack, patterns, API contracts, DB schema)
- ✅ Task breakdown (granular 4-8h tasks, acceptance criteria, dependency graphs)
- ✅ Dependency analysis (external, internal, data, infrastructure)
- ✅ Risk assessment (identification, scoring, mitigation strategies)
- ✅ Complexity estimation (level, time, team, skill)
- ✅ Architectural design (component diagrams, sequence diagrams, Mermaid)
- ✅ 3 detail levels (basic, medium, detailed) + JSON output
- ✅ Integration with spec-builder, tdd-implementer, quality-gate, doc-syncer
- ✅ 3 complete examples (CRUD API, Payment Integration, UI Component)

**Format Expert** ✅ Issue #55
- ✅ format-expert agent (680+ líneas) - Code formatting & linting orchestrator
- ✅ /mj2:format command (190+ líneas) - Automated formatting for C# and TypeScript/JavaScript
- ✅ 4-phase workflow: ANALYZE → FORMAT → LINT → VALIDATE
- ✅ 3 skills created (~930 líneas total):
  - dotnet-format.md (~330 líneas) - .NET formatting con dotnet format CLI
  - prettier.md (~270 líneas) - TypeScript/JavaScript formatting
  - eslint.md (~330 líneas) - JavaScript/TypeScript linting
- ✅ File type detection (.cs, .ts, .tsx, .js, .jsx)
- ✅ Configuration auto-detection (.editorconfig, .prettierrc, .eslintrc)
- ✅ Tool orchestration (dotnet format, prettier, ESLint)
- ✅ Git integration (--staged, --check, --fix modes)
- ✅ Performance optimization (parallel execution, caching)
- ✅ Integration with quality-gate, tdd-implementer, pre-commit hooks
- ✅ "Mr. mj2 recomienda" output format

### v0.8.0 - Specialized Experts - 🟡 EN PROGRESO 1/2 (Issues #60-61)

**UI/UX Expert** ✅ Issue #61
- ✅ ui-ux-expert agent (850+ líneas) - User-centered design completo
- ✅ /mj2:ux-design command (350+ líneas) - Generate UX design artifacts
- ✅ 4-phase workflow: RESEARCH → DESIGN → PROTOTYPE → TEST
- ✅ 4 UX templates (~1,340 líneas total):
  - user-persona.md (~180 líneas) - Demographics, goals, pain points, JTBD
  - user-journey.md (~240 líneas) - 4 stages: Discover → Try → Use → Recommend
  - wireframe-guidelines.md (~400 líneas) - 5 layout patterns, responsive, accessibility
  - usability-test-plan.md (~520 líneas) - Test scenarios, metrics, script, analysis
- ✅ Design Thinking methodology (Empathize → Define → Ideate → Prototype → Test)
- ✅ Jobs-to-be-Done framework integration
- ✅ Nielsen's 10 Usability Heuristics validation
- ✅ Integration with component-designer, accessibility-expert, frontend-builder, spec-builder
- ✅ User research (personas, pain points, interviews)
- ✅ Information architecture (sitemap, navigation, content hierarchy)
- ✅ User journey mapping (actions, emotions, touchpoints)
- ✅ Wireframing (layouts, components, responsive breakpoints)
- ✅ Interaction design (user flows, micro-interactions)
- ✅ Prototyping recommendations (fidelity levels, tools, design tokens)
- ✅ Usability testing (test plans, SUS score, analysis framework)
- ✅ "Mr. mj2 recomienda" output format

**Monitoring Expert** ⏳ Issue #60 (Pending)

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
- [x] 21 agentes mj2 (evolución desde 6 iniciales)
- [x] 20 comandos slash (evolución desde 7 iniciales)
- [x] 45 skills (evolución desde 11 iniciales)
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

### v0.3.0: Full Stack + DevOps ✅ COMPLETADA (Issues #33-38)

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

### v0.4.0: Advanced Features ✅ COMPLETADA (Issues #39-43)

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

**Project Templates** ⏭️ Issue #41 - SKIPPED (postponed)
- Razón: Prioridad baja, enfoque en extensibilidad (agent-factory, skill-factory)
- Los usuarios pueden crear sus propios templates usando /mj2:create-agent

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

### v0.5.0: System Evolution 🟢 CASI COMPLETA 8/9 (Issues #44-52, #56, #64)
- [x] Feedback & Learning System (#44) ✅
- [x] Agent & Skill Factory (#45) ✅ - GAME CHANGER
- [x] Release Management (#46) ✅
- [ ] Personalization System (#47) - Postponed
- [x] Debug & Migration Helpers (#48) ✅
- [x] Component Designer (#49) ✅
- [x] Advanced Hooks System (#50) ✅ - Python v2.0.0
- [ ] Output Styles (#51)
- [ ] MCP Integrations (#52) - Evaluación
- [x] Docs Manager Agent (#56) ✅
- [x] Workflow Orchestrator & "Mr. mj2" (#64) ✅

**Status:** 8 de 9 issues completados (excluye #47 postponed, #52 evaluación). Sistema con documentation management completo.

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

Construido con [Claude Code](https://claude.ai/code) - el CLI oficial de Anthropic para desarrollo asistido por IA.

## Licencia

[Mozilla Public License 2.0](LICENSE)

**¿Qué significa MPL-2.0?**
- ✅ Puedes usar este SDK en tus proyectos privados y comerciales
- ✅ Si modificas el código del SDK, debes compartir tus mejoras
- ✅ Debes mantener el reconocimiento de autoría
- 📖 [Más sobre MPL-2.0](https://www.mozilla.org/en-US/MPL/2.0/)

## Autor

**@mjcuadrado**

---

**¿Preguntas o sugerencias?** Abre un [issue](https://github.com/mjcuadrado/mjcuadrado-net-sdk/issues) en GitHub.
