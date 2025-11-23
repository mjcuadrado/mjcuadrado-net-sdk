# MJ² Roadmap

**Última actualización:** 2025-11-22 (v0.3.0 COMPLETED - Issues #33-38 ✅)
**Basado en:** Gap Analysis vs moai-adk + STACK.md

---

## 📊 Estado Actual

### ✅ v0.1.0 - Core System (COMPLETED - Issues #1-22)

**Sistema base funcional:**
- 6 agentes mj2 (doc-syncer, git-manager, project-manager, quality-gate, spec-builder, tdd-implementer)
- 7 comandos (/mj2:0-project, 1-plan, 2-run, 3-sync, git-merge, quality-check)
- 11 skills (5 foundation + 4 .NET + 2 mj2)
- Git Hooks (pre-commit, commit-msg, pre-push)
- Workflow TDD estricto (RED → GREEN → REFACTOR)
- Build + Tests passing (100%)
- CI/CD configurado
- **PRODUCTION READY**

### ✅ v0.2.0 - Frontend Foundation (COMPLETED - Issues #24-32)

**Frontend stack completo:**
- 7 agentes mj2 (añadido: frontend-builder, e2e-tester)
- 9 comandos (añadidos: /mj2:2f-build, /mj2:4-e2e)
- 20 skills totales (añadidos: 9 frontend + 5 architecture + 2 testing)
- Testing Pyramid COMPLETA (Unit → Integration → Component → E2E)
- React 18 + TypeScript 5 + Material UI v6
- State management (Zod + React Hook Form + TanStack Query)
- E2E testing (Playwright + axe-core)
- **FRONTEND READY**

---

## 🔍 Gap Analysis: moai-adk vs mj2

### Comparación de Estructura

| Aspecto | moai-adk | mj2 (actual) | Gap |
|---------|----------|--------------|-----|
| **Agentes** | 31 agentes | 6 agentes | ❌ 25 agentes faltantes |
| **Comandos** | 6 comandos | 7 comandos | ✅ Equivalente |
| **Skills** | 128 skills | 11 skills | ❌ 117 skills faltantes |
| **Hooks** | Sí (.claude/hooks) | Sí (.claude/scripts) | ✅ Implementado |
| **Settings** | config.json | config.json (template) | ✅ Implementado |
| **Multilenguaje** | 12 idiomas | Solo español | ⚠️ Faltante |
| **MCP Integration** | 4 integraciones | 0 | ❌ Faltante |
| **BaaS Support** | 10 providers | 0 | ❌ Faltante |

### Agentes en moai-adk que NO tenemos

**Agentes Especializados (25 agentes faltantes):**

1. **accessibility-expert** - Accesibilidad web (WCAG, ARIA)
2. **agent-factory** - Meta-agente para crear nuevos agentes
3. **api-designer** - Diseño de APIs RESTful/GraphQL
4. **backend-expert** - Backend specialist (equivalente a nuestro tdd-implementer pero más amplio)
5. **cc-manager** - Claude Code configuration manager
6. **component-designer** - Diseño de componentes UI/UX
7. **database-expert** - Database design y optimization
8. **debug-helper** - Debugging assistant
9. **devops-expert** - DevOps y CI/CD specialist
10. **docs-manager** - Documentation management (más amplio que doc-syncer)
11. **format-expert** - Code formatting y linting
12. **frontend-expert** - Frontend specialist (React, TypeScript, etc.)
13. **implementation-planner** - Planning detallado de implementación
14. **mcp-context7-integrator** - Context7 MCP integration
15. **mcp-figma-integrator** - Figma MCP integration
16. **mcp-notion-integrator** - Notion MCP integration
17. **mcp-playwright-integrator** - Playwright MCP integration
18. **migration-expert** - Code migration y refactoring
19. **monitoring-expert** - Observability y monitoring
20. **performance-engineer** - Performance optimization
21. **security-expert** - Security auditing y best practices
22. **skill-factory** - Meta-skill para crear nuevos skills
23. **sync-manager** - Synchronization management (más amplio que nuestro git-manager)
24. **trust-checker** - TRUST 5 validation (lo tenemos parcialmente en quality-gate)
25. **ui-ux-expert** - UI/UX design specialist

### Skills en moai-adk que NO tenemos (Categorías principales)

**BaaS Providers (10 skills):**
- Auth0, Clerk, Cloudflare, Convex, Firebase, Neon, Railway, Supabase, Vercel

**Claude Code Internals (12 skills):**
- Agents, Commands, Hooks, MCP builder, MCP plugins, Memory, Settings, Skill factory, etc.

**Core Skills (15 skills):**
- Agent factory, Agent guide, Ask user questions, Clone pattern, Code reviewer, Config schema, Context budget, Dev guide, Expertise detection, Feedback templates, Issue labels, Language detection, Personas, Practices, Proactive suggestions, Rules, Session state, SPEC authoring, TodoWrite pattern, Workflow

**Domains (20 skills):**
- Backend, CLI tool, Cloud, Data science, Database, DevOps, Figma, Frontend, ML, ML-Ops, Mobile app, Monitoring, Notion, Security, Testing, Web API

**Essentials (4 skills):**
- Debug, Performance, Refactor, Review

**Languages (19 skills):**
- C, C++, C#, Dart, Go, HTML/CSS, Java, JavaScript, Kotlin, PHP, Python, R, Ruby, Rust, Scala, Shell, SQL, Swift, TailwindCSS, TypeScript

**Libraries/Frameworks (1 skill):**
- shadcn/ui

**Security (10 skills):**
- API security, Auth, Compliance, Encryption, Identity, OWASP, Secrets, SSRF, Threat modeling, Zero trust

**Testing (3 skills):**
- Playwright, React Testing Library, Webapp testing

**Project Management (6 skills):**
- Batch questions, Config manager, Documentation, Language initializer, Template optimizer

**Total moai-adk skills:** ~128 skills
**Total mj2 skills:** 11 skills
**Gap:** ~117 skills faltantes

---

## 🎯 Gap Analysis: STACK.md Requirements

### Stack Tecnológico que mj2 debe SOPORTAR

**Backend (.NET 9):**
- ✅ C# 13 (skill: dotnet/csharp.md)
- ✅ ASP.NET Core (skill: dotnet/aspnet-core.md)
- ✅ Entity Framework Core (skill: dotnet/ef-core.md)
- ❌ PostgreSQL 16+ (snake_case) - **FALTANTE CRÍTICO**
- ❌ MediatR (CQRS) - **FALTANTE**
- ❌ FluentValidation - **FALTANTE**
- ❌ Mapster - **FALTANTE**
- ❌ Testcontainers - **FALTANTE**

**Frontend (React 18):**
- ❌ React 18 - **FALTANTE CRÍTICO**
- ❌ TypeScript 5 - **FALTANTE CRÍTICO**
- ❌ Vite - **FALTANTE**
- ❌ Material UI v6 - **FALTANTE CRÍTICO**
- ❌ React Query (TanStack Query) - **FALTANTE**
- ❌ React Hook Form - **FALTANTE**
- ❌ openapi-typescript - **FALTANTE**
- ❌ Zod - **FALTANTE**

**Architecture:**
- ❌ Clean Architecture - **FALTANTE CRÍTICO**
- ❌ Vertical Slice - **FALTANTE**
- ❌ CQRS - **FALTANTE**
- ❌ DDD - **FALTANTE**
- ❌ Result Pattern - **FALTANTE**

**Testing:**
- ✅ xUnit + FluentAssertions (skill: dotnet/xunit.md)
- ❌ Playwright (E2E) - **FALTANTE CRÍTICO**
- ❌ Vitest - **FALTANTE**
- ❌ React Testing Library - **FALTANTE**
- ❌ Testcontainers - **FALTANTE**

**DevOps:**
- ❌ Docker - **FALTANTE CRÍTICO**
- ❌ Docker Compose - **FALTANTE CRÍTICO**
- ❌ GitHub Actions - **FALTANTE**
- ❌ OpenTelemetry - **FALTANTE**
- ❌ Grafana/Loki - **FALTANTE**

**Security:**
- ❌ JWT + Refresh Tokens - **FALTANTE**
- ❌ OWASP ASVS nivel 2 - **FALTANTE**
- ❌ Rate limiting - **FALTANTE**

**Agentes necesarios:**
- ❌ frontend-builder - **FALTANTE CRÍTICO** (equivalente a tdd-implementer para React)
- ❌ e2e-tester - **FALTANTE** (orquestar Playwright tests)
- ❌ devops-expert - **FALTANTE**
- ❌ database-expert - **FALTANTE**
- ❌ security-expert - **FALTANTE**

---

## 📋 Issues Priorizadas (v0.2.0 - v1.0.0)

### 🔴 CRÍTICO - v0.2.0 (Issues #24-32) - Stack Core

**Backend Advanced - Issues #24-27** (2 semanas)

**Issue #24: PostgreSQL & Mapster**
- `.claude/skills/dotnet/postgresql.md` (~400 líneas)
  - PostgreSQL 16+ patterns
  - snake_case conventions
  - EF Core configuration
  - Migrations
- `.claude/skills/dotnet/mapster.md` (~300 líneas)
  - Object mapping patterns
  - Configuration
  - Performance
- **Adaptar de:** moai-adk/domain-database
- **Referencia:** STACK.md > Backend > Database
- **Tiempo:** 3-4 días

**Issue #25: MediatR & FluentValidation**
- `.claude/skills/dotnet/mediatr.md` (~350 líneas)
  - CQRS patterns
  - Pipeline behaviors
  - Request/Response
- `.claude/skills/dotnet/fluentvalidation.md` (~300 líneas)
  - Validation rules
  - Async validation
  - Integration con MediatR
- **Adaptar de:** moai-adk/domain-backend
- **Referencia:** STACK.md > Backend > Patterns
- **Tiempo:** 3-4 días

**Issue #26: Architecture Patterns**
- `.claude/skills/architecture/clean-architecture.md` (~450 líneas)
  - Layer separation
  - Dependency inversion
  - .NET 9 implementation
- `.claude/skills/architecture/vertical-slice.md` (~350 líneas)
  - Feature-based organization
  - Minimal abstractions
- `.claude/skills/architecture/cqrs.md` (~350 líneas)
  - Commands vs Queries
  - MediatR integration
- `.claude/skills/architecture/ddd.md` (~400 líneas)
  - Entities, Value Objects
  - Aggregate Roots
  - Domain Events
- `.claude/skills/architecture/result-pattern.md` (~250 líneas)
  - Functional error handling
  - Railway-oriented programming
- **Adaptar de:** moai-adk/domain-backend (patterns)
- **Referencia:** STACK.md > Architecture
- **Tiempo:** 5-6 días

**Issue #27: Testcontainers**
- `.claude/skills/testing/testcontainers.md` (~350 líneas)
  - PostgreSQL containers
  - Integration test patterns
  - Lifecycle management
- **Adaptar de:** moai-adk/domain-testing
- **Referencia:** STACK.md > Testing
- **Tiempo:** 2-3 días

**Frontend Foundation - Issues #28-30** (2 semanas) ✅ COMPLETADO

**Issue #28: React & TypeScript Core** ✅ COMPLETADO
- `.claude/skills/frontend/react.md` (~500 líneas)
  - React 18 patterns
  - Hooks best practices
  - Component design
  - Performance optimization
- `.claude/skills/frontend/typescript.md` (~450 líneas)
  - TypeScript 5 strict mode
  - Type-safe patterns
  - Generics and utilities
- **Adaptar de:** moai-adk/domain-frontend, moai-adk/lang-typescript
- **Referencia:** STACK.md > Frontend > Core
- **Tiempo:** 4-5 días

**Issue #29: Vite & MUI** ✅ COMPLETADO
- `.claude/skills/frontend/vite.md` (~300 líneas)
  - Vite configuration
  - Build optimization
  - Dev server
- `.claude/skills/frontend/mui.md` (~450 líneas)
  - Material UI v6 patterns
  - Theming
  - Component customization
  - Design system patterns
- **Adaptar de:** moai-adk/domain-frontend
- **Referencia:** STACK.md > Frontend > UI Framework
- **Tiempo:** 4-5 días

**Issue #30: State & Data Management** ✅ COMPLETADO
- `.claude/skills/frontend/react-query.md` (~400 líneas)
  - TanStack Query patterns
  - Caching strategies
  - Optimistic updates
- `.claude/skills/frontend/react-hook-form.md` (~350 líneas)
  - Form handling
  - Validation
  - Integration con Zod
- `.claude/skills/frontend/zod.md` (~300 líneas)
  - Schema validation
  - Type inference
  - Runtime validation
- `.claude/skills/frontend/openapi-typescript.md` (~300 líneas)
  - Type-safe API client
  - Code generation
  - Integration patterns
- **Adaptar de:** moai-adk/domain-frontend
- **Referencia:** STACK.md > Frontend > State Management
- **Tiempo:** 5-6 días

**Frontend Agent - Issue #31** (1 semana) ✅ COMPLETADO

**Issue #31: Frontend Builder Agent** ✅ COMPLETADO
- `.claude/agents/mj2/frontend-builder.md` (~800 líneas)
  - Component-Driven Development (CDD)
  - TDD para componentes React
  - Test → Component → Style cycle
  - Integration con frontend skills
  - Vitest + React Testing Library
- `.claude/commands/mj2-2f-build.md` (~150 líneas)
  - Comando para activar frontend-builder
- **Adaptar de:** moai-adk/tdd-implementer + moai-adk/frontend-expert
- **Referencia:** STACK.md > Frontend
- **Tiempo:** 6-7 días

**E2E Testing - Issue #32** (1 semana) ✅ COMPLETADO

**Issue #32: Playwright E2E** ✅ COMPLETADO
- `.claude/skills/testing/playwright.md` (~450 líneas)
  - E2E test patterns
  - Page Object Model
  - Visual regression
  - API mocking
- `.claude/agents/mj2/e2e-tester.md` (~600 líneas)
  - Orquestar tests E2E
  - Integration con CI/CD
  - Coverage reporting
- `.claude/commands/mj2-4-e2e.md` (~150 líneas)
  - Comando para ejecutar E2E tests
- **Adaptar de:** moai-adk/playwright-webapp-testing, moai-adk/mcp-playwright-integrator
- **Referencia:** STACK.md > Testing > Playwright
- **Tiempo:** 6-7 días

**Tiempo Total v0.2.0:** 6-7 semanas ✅ COMPLETADO

---

### 🟡 IMPORTANTE - v0.3.0 (Issues #33-38) - Full Stack + DevOps

**Frontend Testing - Issue #33** (4 días) ✅ COMPLETADO

**Issue #33: Frontend Testing Stack** ✅ COMPLETADO
- `.claude/skills/testing/vitest.md` (622 líneas) ✅
  - Framework de testing moderno con Vite
  - Configuración y setup
  - Matchers y aserciones
  - Mocking (funciones, módulos, implementaciones)
  - Coverage configuration
  - Watch mode y UI mode
  - Best practices
- `.claude/skills/testing/react-testing-library.md` (570 líneas) ✅
  - Testing user-centric de componentes
  - Prioridad de queries (getByRole, getByLabel, etc.)
  - userEvent para interacciones
  - Testing asíncrono
  - Custom render con proveedores
  - Patrones de testing (formularios, hooks)
  - Anti-patterns documentados
- `.github/issues/issue-33.md` (documentación completa) ✅
- **Idioma:** 100% español ✅
- **Adaptar de:** moai-adk/domain-testing
- **Referencia:** STACK.md > Testing
- **Tiempo:** 4 días

**Docker & Local Dev - Issues #34-35** (1.5 semanas)

**Issue #34: Docker Foundation** ✅ COMPLETADO
- `.claude/skills/tools/docker.md` (811 líneas) ✅
  - Instalación y configuración
  - Conceptos básicos (imagen vs contenedor)
  - Dockerfile: instrucciones y best practices
  - Multi-stage builds (.NET y Node.js)
  - Optimización de imágenes (86% reducción)
  - Security best practices (no-root, health checks)
  - Networking (bridge, host, overlay)
  - Volumes y persistencia
  - Comandos comunes y debugging
- `.claude/skills/tools/docker-compose.md` (913 líneas) ✅
  - Instalación Docker Compose
  - Estructura docker-compose.yml
  - Configuración de servicios
  - Variables de entorno (.env files)
  - Redes y volúmenes
  - Dependencias y health checks
  - Ejemplo full stack (.NET + React + PostgreSQL)
  - Perfiles para servicios opcionales
  - Security hardening
- **Templates:** ✅
  - `.claude/templates/docker/Dockerfile.dotnet` (90 líneas)
  - `.claude/templates/docker/Dockerfile.react` (126 líneas)
  - `.claude/templates/docker/docker-compose.fullstack.yml` (350 líneas)
- `.github/issues/issue-34.md` (documentación completa) ✅
- **Idioma:** 100% español ✅
- **Adaptar de:** moai-adk/domain-devops
- **Referencia:** STACK.md > DevOps > Docker
- **Tiempo:** 5-6 días

**Issue #35: DevOps Agent** ✅ COMPLETADO
- `.claude/agents/mj2/devops-expert.md` (696 líneas) ✅
  - Persona y filosofía del agente
  - TRUST 5 principles para DevOps
  - Workflow de 4 fases (PLAN → BUILD → DEPLOY → VERIFY)
  - Deployment strategies (Blue-Green, Rolling, Canary)
  - Docker y containerización
  - CI/CD orchestration
  - Security best practices
  - Monitoring y observability
  - Rollback automation
  - Integration con otros agentes
- `.claude/commands/mj2-5-deploy.md` (444 líneas) ✅
  - Comando slash para deployment automatizado
  - Parámetros y opciones completas
  - Workflow de 4 fases documentado
  - Ejemplos exhaustivos (dry-run, canary, etc.)
  - Validaciones pre-deployment
  - Rollback automático
  - Tips y best practices
- `.github/issues/issue-35.md` (documentación completa) ✅
- **Idioma:** 100% español ✅
- **Adaptar de:** moai-adk/devops-expert
- **Referencia:** STACK.md > DevOps
- **Tiempo:** 5 días

**CI/CD - Issue #36** (5 días) ✅ COMPLETADO

**Issue #36: GitHub Actions CI/CD** ✅ COMPLETADO
- `.claude/skills/tools/github-actions.md` (418 líneas) ✅
  - Conceptos básicos (workflows, jobs, steps, runners)
  - Triggers completos (push, pull_request, schedule, workflow_dispatch)
  - Secrets y variables de entorno
  - Caching strategies para optimización
  - Matrix builds para multi-target
  - Docker build & push integration
  - Ejemplos prácticos (.NET CI, React CI)
  - Best practices y troubleshooting
- **Templates:** ✅
  - `.claude/templates/github/workflows/backend-ci.yml` (380+ líneas)
  - `.claude/templates/github/workflows/frontend-ci.yml` (370+ líneas)
  - `.claude/templates/github/workflows/e2e-ci.yml` (450+ líneas)
  - `.claude/templates/github/workflows/cd.yml` (490+ líneas)
- `.github/issues/issue-36.md` (documentación completa) ✅
- **Métricas:** 33 jobs totales, 3 deployment strategies
- **Optimización:** ~7-11 minutos ahorrados con caching
- **Idioma:** 100% español ✅
- **Adaptar de:** moai-adk/domain-devops
- **Referencia:** STACK.md > CI/CD
- **Tiempo:** 5 días

**Observability - Issue #37** (5 días) ✅ COMPLETADO

**Issue #37: OpenTelemetry Stack** ✅ COMPLETADO
- `.claude/skills/tools/opentelemetry.md` (434 líneas) ✅
  - Conceptos básicos (traces, metrics, logs)
  - Auto-instrumentación (ASP.NET Core, HTTP, SQL)
  - Instrumentación manual (ActivitySource, Meters)
  - Exporters (Console, Jaeger, Prometheus, OTLP)
  - OpenTelemetry Collector configuration
  - Sampling strategies y best practices
- `.claude/skills/tools/grafana.md` (365 líneas) ✅
  - Data sources (Prometheus, Loki, Jaeger)
  - Dashboards creation y provisioning
  - Query builder (PromQL, LogQL)
  - Alerting rules y contact points
  - Variables y templating
  - Best practices
- `.claude/skills/tools/serilog.md` (318 líneas) ✅
  - Structured logging concepts
  - Sinks (Console, File, Seq, Loki)
  - Enrichers (built-in y custom)
  - Integración con OpenTelemetry
  - Correlación automática con traces
  - Best practices (secrets, PII, cardinality)
- `.github/issues/issue-37.md` (documentación completa) ✅
- **Stack completo:** Jaeger + Prometheus + Loki + Grafana
- **Métricas:** 3 telemetry signals, 4 exporters, 4 sinks
- **Idioma:** 100% español ✅
- **Adaptar de:** moai-adk/monitoring-expert
- **Referencia:** STACK.md > Observability
- **Tiempo:** 5 días

**Database Expert - Issue #38** (5 días) ✅ COMPLETADO

**Issue #38: Database Expert Agent** ✅ COMPLETADO
- `.claude/skills/dotnet/sqlserver.md` (442 líneas) ✅
  - SQL Server 2022+ con EF Core 9
  - Connection strings (Windows Auth, SQL Auth, Azure SQL)
  - T-SQL queries y stored procedures
  - Índices avanzados (Clustered, Covering, Filtered, Columnstore)
  - Transactions, isolation levels, performance
  - Docker con SQL Server
  - Best practices
- `.claude/agents/mj2/database-expert.md` (665 líneas) ✅
  - Experto en PostgreSQL + SQL Server
  - TRUST 5 principles para databases
  - Workflow: ANALYZE → DESIGN → MIGRATE → OPTIMIZE
  - Migration strategies (Expand-Contract, Blue-Green, Rolling)
  - Database patterns (Aggregate, Soft Delete, Audit Trail)
  - Performance optimization (ambos RDBMS)
  - Security best practices
- `.claude/commands/mj2-db-migrate.md` (180 líneas) ✅
  - Comando para migraciones EF Core
  - Parámetros: add, update, rollback, script, remove
  - Workflow seguro para producción
  - Integration con deployment
- `.github/issues/issue-38.md` (documentación completa) ✅
- **PostgreSQL skill:** Ya existía en `.claude/skills/dotnet/postgresql.md`
- **2 RDBMS completos:** PostgreSQL + SQL Server
- **Migration strategies:** 3 (Expand-Contract, Blue-Green, Rolling)
- **Database patterns:** 3 (Aggregate, Soft Delete, Audit Trail)
- **Idioma:** 100% español ✅
- **Adaptar de:** moai-adk/database-expert
- **Referencia:** STACK.md > Backend > Database
- **Tiempo:** 5 días

**Tiempo Total v0.3.0:** 5-6 semanas ✅ COMPLETADO

---

### 🟢 NICE TO HAVE - v0.4.0 (Issues #39-43) - Advanced Features

**Security - Issue #39** (1 semana) ✅ COMPLETADO

**Issue #39: Security Expert** ✅ COMPLETADO
- `.claude/skills/security/jwt.md` (370 líneas) ✅
  - JWT (JSON Web Tokens) fundamentals
  - Access tokens (15 min) + Refresh tokens (7 días)
  - Claims-based authentication con custom claims
  - Cookie vs Header strategies (HttpOnly, Secure, SameSite)
  - Token generation, validation, y revocation
  - Integration con ASP.NET Core Identity
  - Policy-based authorization con claims
- `.claude/skills/security/owasp-asvs.md` (430 líneas) ✅
  - OWASP ASVS (Application Security Verification Standard) nivel 2
  - Security checklist completo
  - 9 categorías principales (V1-V9)
  - Implementation guidelines para .NET
  - Security testing con xUnit
  - Configuración segura (passwords, lockout, encryption)
- `.claude/skills/security/rate-limiting.md` (280 líneas) ✅
  - 4 algoritmos: Fixed Window, Sliding Window, Token Bucket, Leaky Bucket
  - ASP.NET Core 7+ built-in rate limiting
  - AspNetCoreRateLimit library
  - Redis-based distributed rate limiting
  - Multi-layer rate limiting (Global, Per-IP, Per-User)
  - DDoS protection patterns
  - Tiered limits (Premium vs Free)
  - Adaptive rate limiting
- `.claude/agents/mj2/security-expert.md` (730 líneas) ✅
  - TRUST 5 principles para seguridad
  - Workflow 4 fases: ASSESS → DESIGN → IMPLEMENT → VERIFY
  - Threat modeling con STRIDE framework
  - OWASP Top 10:2021 mitigación completa (A01-A10)
  - Security auditing automation
  - Vulnerability scanning workflow
  - Integration con otros agentes
  - Security checklist completo (10 categorías)
  - Automated security testing examples
- `.github/issues/issue-39.md` (documentación completa) ✅
- **Métricas:** ~1,810 líneas totales
- **OWASP Coverage:** ASVS nivel 2 (9 categorías) + Top 10 (10 amenazas)
- **Rate Limiting:** 4 algoritmos implementados
- **Idioma:** 100% español ✅
- **Adaptar de:** moai-adk/security-* (10 skills)
- **Referencia:** STACK.md > Security
- **Tiempo:** Completado

**API Design - Issue #40** (5 días) ✅ COMPLETADO

**Issue #40: API Designer Agent** ✅ COMPLETADO
- `.claude/agents/mj2/api-designer.md` (680 líneas) ✅
  - RESTful API design best practices
  - REST constraints (Client-Server, Stateless, Cacheable, Uniform Interface, Layered System)
  - Resource naming conventions (plural, kebab-case, sin verbos)
  - HTTP methods y status codes apropiados
  - OpenAPI/Swagger documentation con XML comments
  - API versioning strategies (URL, Header, Query)
  - Pagination patterns (Offset-based, Cursor-based)
  - Filtering, sorting, searching
  - HATEOAS implementation
  - RFC 7807 Problem Details error handling
  - Workflow de 4 fases: ANALYZE → DESIGN → DOCUMENT → VALIDATE
- `.claude/commands/mj2-api-design.md` (210 líneas) ✅
  - Comando para diseño de APIs: `/mj2:api-design <SPEC-ID>`
  - Workflow completo detallado
  - Ejemplos de uso (Orders API, Auth API)
  - Integration con workflow full-stack
  - Checklist de salida
- `.github/issues/issue-40.md` (documentación completa) ✅
- **Métricas:** ~890 líneas totales
- **REST Constraints:** 5 implementados
- **Versioning Strategies:** 3 (URL, Header, Query)
- **Pagination Patterns:** 2 (Offset, Cursor)
- **Idioma:** 100% español ✅
- **Adaptar de:** moai-adk/api-designer
- **Referencia:** STACK.md > Backend > API Design
- **Tiempo:** Completado

**Project Templates - Issue #41** (1 semana)

**Issue #41: Full Stack Templates**
- **Templates:**
  - `templates/projects/clean-architecture/` (estructura completa)
  - `templates/projects/vertical-slice/` (estructura completa)
  - `templates/projects/fullstack-react-dotnet/` (estructura completa)
- **Actualizar project-manager agent:**
  - Integration con templates
  - Template selection
- **Adaptar de:** moai-adk/project-template-optimizer
- **Referencia:** STACK.md > Architecture
- **Tiempo:** 7 días

**Performance - Issue #42** (5 días)

**Issue #42: Performance Engineer Agent**
- `.claude/skills/performance/backend.md` (~350 líneas)
  - Caching strategies
  - Query optimization
  - Async patterns
- `.claude/skills/performance/frontend.md` (~350 líneas)
  - Code splitting
  - Lazy loading
  - Virtualization
- `.claude/agents/mj2/performance-engineer.md` (~600 líneas)
  - Performance profiling
  - Optimization strategies
  - Benchmarking
- **Adaptar de:** moai-adk/performance-engineer
- **Referencia:** STACK.md > Performance
- **Tiempo:** 5 días

**Accessibility - Issue #43** (4 días)

**Issue #43: Accessibility Expert**
- `.claude/skills/frontend/accessibility.md` (~400 líneas)
  - WCAG 2.1 Level AA
  - ARIA patterns
  - Keyboard navigation
  - Screen reader support
- `.claude/agents/mj2/accessibility-expert.md` (~550 líneas)
  - Accessibility auditing
  - WCAG compliance
  - Testing strategies
- **Adaptar de:** moai-adk/accessibility-expert
- **Referencia:** Best practices web
- **Tiempo:** 4 días

**Tiempo Total v0.4.0:** 4-5 semanas

---

## 🗺️ Roadmap Visual

```
v0.1.0 (DONE)
  ├── Core System
  └── TDD Workflow
      │
      ↓
v0.2.0 (6-7 semanas) ← CRÍTICO
  ├── Backend Advanced (#24-27)
  │   ├── PostgreSQL, Mapster
  │   ├── MediatR, FluentValidation
  │   ├── Architecture Patterns
  │   └── Testcontainers
  ├── Frontend Foundation (#28-30)
  │   ├── React, TypeScript
  │   ├── Vite, MUI
  │   └── State Management
  ├── Frontend Agent (#31)
  └── E2E Testing (#32)
      │
      ↓
v0.3.0 (5-6 semanas) ← IMPORTANTE
  ├── Frontend Testing (#33)
  ├── Docker & DevOps (#34-35)
  ├── CI/CD (#36)
  ├── Observability (#37)
  └── Database Expert (#38)
      │
      ↓
v0.4.0 (4-5 semanas) ← NICE TO HAVE
  ├── Security (#39)
  ├── API Designer (#40)
  ├── Templates (#41)
  ├── Performance (#42)
  └── Accessibility (#43)
      │
      ↓
v0.5.0 (3-4 semanas) ← ADVANCED 🆕
  ├── Feedback System (#44)
  ├── Agent/Skill Factory (#45)
  ├── Release Management (#46)
  ├── Personalization (#47)
  ├── Debug & Migration (#48)
  ├── Component Designer (#49)
  ├── Advanced Hooks (#50)
  ├── Output Styles (#51)
  └── MCP Integrations (#52)
      │
      ↓
v1.0.0 - FULL STACK READY + EXTENSIBLE
```

---

## 📊 Resumen Ejecutivo

### Tiempo Total Estimado

| Versión | Issues | Semanas | Prioridad |
|---------|--------|---------|-----------|
| v0.2.0 | #24-32 (9 issues) | 6-7 | 🔴 CRÍTICO |
| v0.3.0 | #33-38 (6 issues) | 5-6 | 🟡 IMPORTANTE |
| v0.4.0 | #39-43 (5 issues) | 4-5 | 🟢 NICE TO HAVE |
| v0.5.0 | #44-52 (9 issues) | 3-4 | 🔵 ADVANCED |
| **Total** | **29 issues** | **18-22 semanas** | **(~5 meses)** |

### Skills Totales

| Categoría | v0.1.0 (actual) | v0.2.0 | v0.3.0 | v0.4.0 | v0.5.0 | v1.0.0 (total) |
|-----------|-----------------|--------|--------|--------|--------|----------------|
| Foundation | 5 | 5 | 5 | 5 | 5 | 5 |
| .NET | 4 | 9 | 10 | 11 | 11 | 11 |
| Frontend | 0 | 8 | 11 | 12 | 12 | 12 |
| Architecture | 0 | 5 | 5 | 5 | 5 | 5 |
| Testing | 1 | 3 | 5 | 5 | 5 | 5 |
| DevOps | 0 | 0 | 5 | 5 | 5 | 5 |
| Security | 0 | 0 | 0 | 3 | 3 | 3 |
| Performance | 0 | 0 | 0 | 2 | 2 | 2 |
| MJ² | 2 | 2 | 2 | 2 | 2 | 2 |
| System | 0 | 0 | 0 | 0 | 3 | 3 |
| **Total** | **11** | **32** | **43** | **50** | **53** | **53** |

### Agentes Totales

| Tipo | v0.1.0 (actual) | v0.2.0 | v0.3.0 | v0.4.0 | v0.5.0 | v1.0.0 (total) |
|------|-----------------|--------|--------|--------|--------|----------------|
| Core | 6 | 8 | 10 | 15 | 21 | 21 |
| **Total** | **6** | **8** | **10** | **15** | **21** | **21** |

---

### 🔵 ADVANCED - v0.5.0 (Issues #44-52) - System Evolution

**Inspirado en moai-adk - Extensibilidad y Mejora Continua**

**Issue #44: Feedback & Learning System** (4-5 días)
- `.claude/agents/mj2/feedback-manager.md` (~550 líneas)
  - Sistema estructurado de feedback
  - Tracking de errores comunes
  - Aprendizaje continuo
- `.claude/commands/mj2-9-feedback.md` (~150 líneas)
- `.mj2/memory/` directory
  - Persistencia de aprendizajes
  - Session state
  - Execution rules
- **Adaptar de:** moai-adk/learning, moai-adk/memory
- **Prioridad:** 🔴 Alta (mejora experiencia usuario)
- **Tiempo:** 4-5 días

**Issue #45: Agent Factory & Skill Factory** (6-7 días)
- `.claude/agents/mj2/agent-factory.md` (~700 líneas)
  - Meta-agente que crea nuevos agentes
  - Template generation
  - Validation y testing
- `.claude/agents/mj2/skill-factory.md` (~650 líneas)
  - Meta-skill que crea nuevos skills
  - Knowledge extraction
  - Documentation generation
- `.claude/commands/mj2-create-agent.md` (~150 líneas)
- `.claude/commands/mj2-create-skill.md` (~150 líneas)
- **Adaptar de:** moai-adk/agent-factory, moai-adk/skill-factory
- **Prioridad:** 🔴 Alta (hace mj2 extensible por usuarios)
- **Impacto:** GAME CHANGER - usuarios pueden extender mj2
- **Tiempo:** 6-7 días

**Issue #46: Release Management System** (5-6 días)
- `.claude/agents/mj2/release-manager.md` (~600 líneas)
  - Workflow completo de release
  - Validación pre-release
  - Versionado semántico
  - CHANGELOG automático
- `.claude/commands/mj2-99-release.md` (~150 líneas)
- Templates de release notes
- Integration con Git tags
- **Adaptar de:** moai-adk/release system
- **Prioridad:** 🔴 Alta (crítico para v1.0.0)
- **Tiempo:** 5-6 días

**Issue #47: Personalization System** (4-5 días)
- Actualizar `.mjcuadrado-net-sdk/config.json` template:
  - `user.name` field (personalización)
  - `language.conversation_language` (es, en)
  - `language.agent_prompt_language` (en recomendado)
- Actualizar todos los agentes para usar nombre del usuario
- Sistema multilenguaje básico (español/inglés)
- **Adaptar de:** moai-adk/configuration, moai-adk/language-detection
- **Prioridad:** 🟡 Media (mejor UX)
- **Tiempo:** 4-5 días

**Issue #48: Debug & Migration Helpers** (5-6 días)
- `.claude/agents/mj2/debug-helper.md` (~550 líneas)
  - Debugging assistant especializado
  - Análisis de errores
  - Sugerencias de fix
- `.claude/agents/mj2/migration-expert.md` (~600 líneas)
  - Migrar proyectos legacy a mj2
  - Análisis de código existente
  - Plan de migración
- `.claude/commands/mj2-debug.md` (~150 líneas)
- `.claude/commands/mj2-migrate.md` (~150 líneas)
- **Adaptar de:** moai-adk/debug-helper, moai-adk/migration-expert
- **Prioridad:** 🟡 Media (expande casos de uso)
- **Tiempo:** 5-6 días

**Issue #49: Component Designer (Design-First)** (5-6 días)
- `.claude/agents/mj2/component-designer.md` (~700 líneas)
  - Design-first approach para componentes
  - Análisis UX/UI requirements
  - Diseño de API de componente
  - Validación de accesibilidad
  - Generación de SPEC de componente
- `.claude/commands/mj2-design-component.md` (~150 líneas)
- Integration con frontend-builder (#31)
- **Adaptar de:** moai-adk/component-designer
- **Prioridad:** 🟡 Media (mejora workflow frontend)
- **Workflow:** Design → SPEC → Implement (frontend-builder)
- **Tiempo:** 5-6 días

**Issue #50: Advanced Hooks System** (4-5 días)
- `.claude/hooks/` directory (diferente de Git hooks)
  - pre-command hook
  - post-command hook
  - on-spec-created hook
  - on-sync-done hook
- Hook templates y documentation
- Integration en agentes principales
- **Use cases:**
  - Notificaciones (Slack, email)
  - Metrics tracking
  - Auto-backup de SPECs
  - Integración con herramientas externas
- **Adaptar de:** moai-adk/hooks system
- **Prioridad:** 🟡 Media (extensibilidad)
- **Tiempo:** 4-5 días

**Issue #51: Output Styles Customization** (3-4 días)
- `.claude/output-styles/` directory
  - minimal.md
  - detailed.md
  - emoji-rich.md
- Configuración en `config.json`
- Templates de output
- **Adaptar de:** moai-adk/output-styles
- **Prioridad:** 🟢 Baja (cosmético, mejora UX)
- **Tiempo:** 3-4 días

**Issue #52: MCP Integrations (Evaluación)** (Variable)
- Evaluar integraciones MCP útiles:
  - **Figma:** Diseños → Componentes React
  - **Notion:** SPECs en Notion
  - **Linear/Jira:** Issues → SPECs
- Crear agentes integrador según evaluación
- **Adaptar de:** moai-adk/mcp-* integrators
- **Prioridad:** 🟢 Baja (evaluar ROI caso por caso)
- **Tiempo:** Variable según integración

**Tiempo Total v0.5.0:** 3-4 semanas

---

## 🎯 Next Steps

### Inmediato (próximos 7 días)

1. ✅ **Issue #23:** Gap Analysis (DONE)
2. ⏳ **Issue #24:** PostgreSQL & Mapster
3. ⏳ **Issue #25:** MediatR & FluentValidation

### Corto Plazo (próximas 4 semanas)

- Issues #26-28: Architecture + React Core
- Milestone: Backend Advanced + Frontend Foundation

### Mediano Plazo (2-3 meses)

- Issues #29-35: Frontend completo + DevOps
- Milestone: Full Stack + CI/CD

### Largo Plazo (4 meses)

- Issues #36-43: Observability + Security + Templates
- Milestone: v1.0.0 Production Ready

---

## 📝 Notas de Implementación

### Adaptación de moai-adk

**Reutilizable:**
- ✅ Estructura de agentes (workflow, orchestration)
- ✅ Skills organization (categorías, naming)
- ✅ Commands pattern (delegation)
- ✅ Config.json structure

**Adaptar (Python → .NET/React):**
- ⚠️ Language-specific skills (Python → C#/TypeScript)
- ⚠️ Framework-specific patterns (FastAPI → ASP.NET Core)
- ⚠️ Testing frameworks (pytest → xUnit/Vitest)
- ⚠️ BaaS providers (algunos no aplicables)

**NO copiar:**
- ❌ Código Python específico
- ❌ BaaS providers que no usamos
- ❌ MCP integrations (evaluar caso por caso)

### Filosofía Consistente

**mj2 mantiene:**
- SPEC-First Development
- TDD estricto (RED → GREEN → REFACTOR)
- Workflow 4-step (0-PROJECT → 1-PLAN → 2-RUN → 3-SYNC)
- TRUST 5 principles
- TAG system
- Git Hooks
- Quality gates (85% coverage mínimo, updated a 90% según STACK.md)

---

## 🔗 Referencias

- **moai-adk:** https://github.com/modu-ai/moai-adk
- **STACK.md:** Stack tecnológico completo que mj2 debe soportar
- **Issues #1-22:** Sistema base (completed)
- **Issue #23:** Este análisis

---

**Mantenido por:** @mjcuadrado
**Próxima revisión:** Después de completar v0.2.0
