# Changelog

Todos los cambios notables en este proyecto serán documentados en este archivo.

El formato está basado en [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/),
y este proyecto adhiere a [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### En progreso
- Ninguno

### Completado recientemente
- ✅ **2024-11-24**: Issue #61 - UI/UX Expert Agent (@SPEC:UX-061, @CODE:UX-061, @DOC:UX-061)
  - **ui-ux-expert.md agent** (~850 líneas) - User-centered design completo
    - 4-phase workflow: RESEARCH → DESIGN → PROTOTYPE → TEST
    - 7 responsibilities (user research, information architecture, journey mapping, wireframing, interaction design, prototyping, usability testing)
    - Design Thinking methodology (Empathize → Define → Ideate → Prototype → Test)
    - Jobs-to-be-Done framework integration
    - Nielsen's 10 Usability Heuristics validation
    - Integration con component-designer, accessibility-expert, frontend-builder, spec-builder
    - "Mr. mj2 recomienda" output format
    - Complete examples per phase
  - **`/mj2:ux-design` command** (~350 líneas) - Generate UX design artifacts:
    - Syntax: `/mj2:ux-design <feature> [--research|--journey|--wireframe|--test|--full]`
    - 5 actions:
      - `--full` - Complete UX workflow (default)
      - `--research` - User personas + pain points
      - `--journey` - User journey mapping
      - `--wireframe` - Wireframes + layout guidelines
      - `--test` - Usability test plan
    - 5 complete examples with outputs
    - Integration workflows documented
    - Troubleshooting section
  - **4 UX templates** (~1,340 líneas total):
    - **user-persona.md** (~180 líneas) - Demographics, goals, pain points, behaviors, tools, JTBD
    - **user-journey.md** (~240 líneas) - 4 stages (Discover → Try → Use → Recommend) with emotions, touchpoints
    - **wireframe-guidelines.md** (~400 líneas) - 5 layout patterns (Hero, Grid, List, Master-Detail, Dashboard), responsive breakpoints, accessibility checkpoints
    - **usability-test-plan.md** (~520 líneas) - Test objectives, scenarios, metrics (SUS, completion, time), test script, analysis framework
  - **SPEC-UX-061** completo (spec.md, plan.md, acceptance.md - 1,860+ líneas):
    - Domain: DESIGN
    - Complexity: high
    - Estimated: 40-48 hours
    - 8 Functional Requirements (FR-1 to FR-8)
    - 4 Non-Functional Requirements (NFR-1 to NFR-4)
  - **README.md actualizado** con Issue #61:
    - v0.8.0: 1/2 completado (Issue #61 añadido) 🟡 EN PROGRESO
    - Agentes: 26 (ui-ux-expert añadido)
    - Comandos: 26 (/mj2:ux-design añadido)
    - Skills: 49 (sin cambios, templates no cuentan como skills)
    - Mr. mj2 lista actualizada con UI/UX Expert
  - **ROADMAP.md actualizado**:
    - Issue #61 marcado COMPLETADO
    - Gap Analysis: 26 agentes, 26 comandos, 49 skills
    - Agentes faltantes: 21 (actualizado de 22 - removido ui-ux-expert)
    - v0.8.0 1/2 en progreso
  - **TAG chain completa** - @SPEC:UX-061 → @CODE:UX-061 → @DOC:UX-061 ✅
  - **Total:** ~3,900 líneas (1 agent + 1 command + 4 templates + 3 SPEC docs)
- ✅ **2024-11-24**: Issue #55 - Format Expert Agent (@SPEC:FMT-055, @CODE:FMT-055, @DOC:FMT-055)
  - **format-expert.md agent** (~680 líneas) - Automated code formatting and linting
    - 4-phase workflow: ANALYZE → FORMAT → LINT → VALIDATE
    - 6 responsibilities (file detection, config loading, tool orchestration, validation, reporting, git integration)
    - File type detection (.cs, .ts, .tsx, .js, .jsx)
    - Configuration auto-detection (.editorconfig, .prettierrc, .eslintrc)
    - Tool orchestration (dotnet format, prettier, ESLint)
    - Git integration (--staged, --check, --fix modes)
    - Performance optimization (parallel execution, caching)
    - "Mr. mj2 recomienda" output format
    - 3 comprehensive examples
  - **`/mj2:format` command** (~190 líneas) - Code formatting and linting:
    - Syntax: `/mj2:format [path] [--check|--fix|--staged]`
    - 3 modes:
      - `--check` - Verify formatting without modifying files (CI mode)
      - `--fix` - Auto-fix formatting violations (default)
      - `--staged` - Format only git staged files (pre-commit mode)
    - 5 complete examples with outputs
    - Integration con quality-gate, tdd-implementer
    - CI/CD and pre-commit hook guides
    - Best practices documentation
  - **3 skills created** (~930 líneas total):
    - **dotnet-format.md** (~330 líneas) - .NET code formatting with dotnet format CLI
    - **prettier.md** (~270 líneas) - Opinionated TypeScript/JavaScript formatter
    - **eslint.md** (~330 líneas) - Pluggable JavaScript/TypeScript linter
  - **SPEC-FMT-055** completo (spec.md, plan.md, acceptance.md - 1,813+ líneas):
    - Domain: QUALITY
    - Complexity: medium
    - Estimated: 24-30 hours
    - 8 Functional Requirements (FR-1 to FR-8)
    - 4 Non-Functional Requirements (NFR-1 to NFR-4)
  - **README.md actualizado** con Issue #55:
    - v0.6.0: 2/2 completado (Issue #55 añadido) ✅ COMPLETADA
    - Agentes: 25 (format-expert añadido)
    - Comandos: 25 (/mj2:format añadido)
    - Skills: 49 (dotnet-format, prettier, eslint añadidos)
    - Mr. mj2 lista actualizada con Format Expert
  - **ROADMAP.md actualizado**:
    - Issue #55 marcado COMPLETADO
    - Gap Analysis: 25 agentes, 25 comandos, 49 skills
    - Agentes faltantes: 22 (actualizado de 25 - removidos format-expert, docs-manager, implementation-planner)
    - v0.6.0 4/4 completada ✅
  - **TAG chain completa** - @SPEC:FMT-055 → @CODE:FMT-055 → @DOC:FMT-055 ✅
  - **Total:** ~3,600 líneas (1 agent + 1 command + 3 skills + 3 SPEC docs)
- ✅ **2024-11-24**: Issue #54 - Implementation Planner Agent (@SPEC:IMP-054, @CODE:IMP-054, @DOC:IMP-054)
  - **implementation-planner.md agent** (~750 líneas) - Transform SPECs into executable implementation plans
    - 4-phase workflow: ANALYZE → PLAN → BREAK_DOWN → VALIDATE
    - 7 responsibilities (SPEC analysis, technical planning, task breakdown, dependency analysis, risk assessment, complexity estimation, architectural design)
    - Data sources: SPEC docs, config.json, codebase, git history, available skills
    - 3 complete examples (Simple CRUD API, Complex Payment Integration, Frontend Component)
    - Integration con spec-builder, tdd-implementer, quality-gate, doc-syncer
    - "Mr. mj2 recomienda" output format
  - **`/mj2:plan-impl` command** (~470 líneas) - Generate implementation plans from SPECs:
    - Required: `<SPEC-ID>` - SPEC identifier to analyze
    - Optional flags:
      - `--detail [basic|medium|detailed]` - Detail level (default: medium)
      - `--validate` - Run validation checks before generating plan
      - `--format [markdown|json]` - Output format (default: markdown)
    - 3 detail levels:
      - **basic**: High-level architecture, phase breakdown, top 3 risks, overall estimate
      - **medium** (DEFAULT): Full component diagrams, all phases with tasks, all dependencies, all risks, quality gates
      - **detailed**: Everything from medium PLUS individual task details with code examples, test scenarios, sequence diagrams, DB scripts, API contracts
    - Examples: Simple CRUD, Payment Integration, Frontend Component
  - **SPEC-IMP-054** completo (spec.md, plan.md, acceptance.md - 1,320+ líneas):
    - Domain: PLAN
    - Complexity: high
    - Estimated: 32-40 hours
    - 8 Functional Requirements (FR-1 to FR-8)
    - 4 Non-Functional Requirements (NFR-1 to NFR-4)
  - **README.md actualizado** con Issue #54:
    - v0.6.0: 1/2 completado (Issue #54 añadido)
    - Agentes: 24 (implementation-planner añadido)
    - Comandos: 24 (/mj2:plan-impl añadido)
    - Mr. mj2 lista actualizada con Implementation Planner
  - **ROADMAP.md actualizado**:
    - Issue #54 marcado COMPLETADO
    - Gap Analysis: 24 agentes, 24 comandos (actualizado de 23)
    - Visual roadmap actualizado (v0.6.0 3/4 - 75% completo)
  - **TAG chain completa** - @SPEC:IMP-054 → @CODE:IMP-054 → @DOC:IMP-054 ✅
  - **Total:** ~2,500 líneas (1 agent + 1 command + 3 SPEC docs)
- ✅ **2024-11-24**: Issue #56 - Docs Manager Agent (@SPEC:DOC-002, @CODE:DOC-002, @DOC:DOC-002)
  - **docs-manager.md agent** (~750 líneas) - Complete documentation management
    - 4-phase workflow: AUDIT → UPDATE → GENERATE → PUBLISH
    - 7 responsibilities (README, CHANGELOG, API docs, ADRs, architecture, templates, publishing)
    - Integration con doc-syncer (TAG sync delegation)
    - Integration con api-designer, release-manager, quality-gate
    - "Mr. mj2 recomienda" output format
  - **`/mj2:docs` command** (~380 líneas) - 4 actions:
    - `audit` - Documentation audit con scoring
    - `update` - Update README & CHANGELOG automático
    - `generate` - Generate missing docs (API docs, ADRs, arquitectura)
    - `publish` - GitHub Pages publishing support
  - **5 documentation templates** creados:
    - README.md template (comprehensive)
    - CHANGELOG.md template (Keep a Changelog format)
    - ADR.md template (Architecture Decision Records)
    - CONTRIBUTING.md template (contribution guidelines)
    - CODE_OF_CONDUCT.md template (Contributor Covenant 2.1)
  - **SPEC-DOC-002** completo (spec.md, plan.md, acceptance.md)
  - **README.md actualizado** con Issue #56:
    - v0.5.0: 8/9 completado (Issue #56 añadido)
    - Agentes: 23 (docs-manager añadido)
    - Comandos: 23 (/mj2:docs añadido)
    - Mr. mj2 lista actualizada con Docs Manager
  - **ROADMAP.md actualizado**:
    - Issue #56 marcado COMPLETADO
    - Gap Analysis: 23 agentes, 23 comandos
    - Visual roadmap actualizado (v0.5.0 8/9)
  - **TAG chain completa** - @SPEC:DOC-002 → @CODE:DOC-002 → @DOC:DOC-002 ✅
  - **Total:** ~2,900 líneas (1 agent + 1 command + 5 templates)
- ✅ **2024-11-24**: Issue #53 - Documentation Sync & Audit (@SPEC:DOC-001, @DOC:DOC-001)
  - **Auditoría completa** de métricas reales del proyecto
  - **README.md actualizado** con datos precisos:
    - v0.5.0: 7/9 completado (Issue #64 añadido)
    - Agentes: 22 (actualizado de 21)
    - Comandos: 22 (actualizado de 20)
    - Skills: 46 (actualizado de 45)
    - Issue #50 con Python hooks v2.0.0 ✅
  - **ROADMAP.md actualizado** con métricas reales:
    - Gap Analysis actualizado (22 agentes, 46 skills)
    - Issue #64 marcado COMPLETADO
    - Tablas con números precisos
  - **Issues #41 y #47** resueltos (wontfix y postponed)
  - **Documentación 100% consistente** - No hay contradicciones
  - **TAG chain completa** - @SPEC:DOC-001 → @DOC:DOC-001
- ✅ **2024-11-23**: Issue #64 - Workflow Orchestrator & "Mr. mj2" (@SPEC:ORCH-064, @CODE:ORCH-064, @DOC:ORCH-064)
  - **Concepto "Mr. mj2"** documentado en README.md (orquestador conceptual)
  - **`/mj2:status` command** implementado - Muestra estado del workflow en tiempo real
  - **`/mj2:help` command** implementado - Guía contextual de 20+ comandos disponibles
  - **orchestration-patterns.md skill** (~520 líneas) - Documentación de 3 patrones de orquestación
  - **workflow-status.md agent** (~430 líneas) - Analiza y reporta estado del proyecto
  - **Agent outputs actualizados** - 5 agentes core con formato "Mr. mj2 recomienda":
    - project-manager.md (INITIALIZE/OPTIMIZE modes)
    - spec-builder.md (SPEC creada con TAG chain)
    - tdd-implementer.md (TDD cycle RED/GREEN/REFACTOR)
    - quality-gate.md (PASS/FAIL con validaciones detalladas)
    - doc-syncer.md (workflow completo!)
  - **UX mejorada** - Usuarios tienen claridad sobre próximos pasos en cada fase
  - **Guidance completo** - Outputs guiados con tips útiles y comandos sugeridos
  - **Multiidioma** - Outputs en español e inglés
  - **TAG chain completa** - @SPEC:ORCH-064 → @CODE:ORCH-064 → @DOC:ORCH-064
- ✅ **2024-11-20**: Issue #7 - Comando version
  - VersionCommand ya implementado previamente
  - 6 tests unitarios nuevos (194/195 passing total)
  - Muestra versión SDK y .NET runtime
  - Flag --verbose con tabla detallada (OS, Architecture, Framework)
  - Cross-platform
- ✅ **2024-11-20**: Issue #6 - Comando doctor
  - DoctorService implementado con 5 verificaciones
  - 31 tests unitarios (188/189 passing, 99.5%)
  - Verifica: .NET SDK ≥9.0, Git, estructura proyecto, disco, permisos
  - Interfaz CLI con spinner, tabla, warnings y sugerencias
  - Cross-platform (Windows, Linux, macOS)
  - Smart suggestions para resolver problemas
  - Flag --verbose para detalles adicionales
- ✅ **2024-11-20**: Issue #5 - Comando init
  - InitCommand implementado con CLI completa
  - 25 tests unitarios (100% passing, 158 tests totales)
  - Integración con FileSystemService, ConfigurationService, TemplateService
  - Validaciones: nombres, permisos, espacio en disco (10 MB mínimo)
  - UI profesional con Spectre.Console (spinner, tablas, paneles)
  - Soporte para --force, --author, --framework
  - Dependency Injection con Microsoft.Extensions.DependencyInjection
  - TypeRegistrar para adaptar Spectre.Console.Cli
- ✅ **2024-11-19**: Issue #4 - TemplateService
  - TemplateService implementado con 6 métodos
  - 37 tests unitarios (100% passing)
  - Lectura de 11 templates embebidos
  - Generación de 9 carpetas + 10 archivos de documentación
  - Reemplazo de 6 variables en templates

- ✅ **2024-11-19**: Issue #3 - ConfigurationService
  - ConfigurationService implementado con 6 métodos
  - 38 tests unitarios (100% passing)
  - Validación completa: semver, ISO dates, project names, languages
  - System.Text.Json con camelCase y comentarios
  - Merge de configuraciones y búsqueda en directorios padres

- ✅ **2024-11-19**: Issue #2 - FileSystemService
  - FileSystemService implementado con 11 métodos
  - 44 tests unitarios (100% passing)
  - Normalización cross-platform de rutas
  - Manejo robusto de excepciones
  - Validación de permisos y espacio en disco

- ✅ **2024-11-19**: Issue #1 - Estructura base del proyecto
  - Solución .NET 10 configurada y funcional
  - Proyectos principal y de tests creados
  - Todos los paquetes NuGet instalados
  - Build exitoso (0 errores)
  - Tests ejecutándose correctamente (1/1 passing)
  - Nullable reference types habilitado
  - C# 13 configurado

## [0.1.0] - TBD (Fase 1 - MVP)

### Added (Agregado)
- ✅ Estructura base del proyecto .NET 10
- ✅ Solución y proyectos configurados
- ✅ Comando `version` implementado
- ✅ Interfaces de servicios definidas
- ✅ Modelos de configuración
- ✅ Templates embebidos
- ✅ Documentación completa (README, arquitectura, comandos)
- ✅ 9 GitHub Issues para desarrollo iterativo
- ✅ CI/CD con GitHub Actions
- ✅ Archivos de ejemplo en `.mjcuadrado-net-sdk/`

### Planned (Planeado)
- [ ] Comando `init` funcional
- [ ] Comando `doctor` funcional
- [ ] FileSystemService implementado
- [ ] ConfigurationService implementado
- [ ] TemplateService implementado
- [ ] Tests unitarios completos
- [ ] Coverage ≥ 85%

## Próximas versiones

### [0.2.0] - Fase 2: SPECs y TAGs
- Sistema completo de SPECs con formato EARS
- Comando `spec new`
- Comando `spec validate`
- Comando `tags validate`
- Sistema de TAGs con trazabilidad completa

### [0.3.0] - Fase 3: Base de datos
- Integración Entity Framework Core
- Soporte SQL Server
- Soporte PostgreSQL
- Migraciones automáticas

### [0.4.0] - Fase 4: Automatización avanzada
- Hooks automáticos pre-commit
- Generación automática de tests desde SPECs
- Integración CI/CD avanzada

### [1.0.0] - Fase 5: IA Completa
- Agentes especializados completos
- Skills para tareas comunes
- Generación de código desde SPECs
- Análisis automático con IA

---

## Tipos de cambios

- **Added** (Agregado): Nueva funcionalidad
- **Changed** (Cambiado): Cambios en funcionalidad existente
- **Deprecated** (Deprecado): Funcionalidad que será removida
- **Removed** (Removido): Funcionalidad removida
- **Fixed** (Corregido): Corrección de bugs
- **Security** (Seguridad): Cambios de seguridad
