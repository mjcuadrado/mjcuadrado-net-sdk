---
name: /mj2:help
description: Show available commands and workflow guidance
---

# /mj2:help

Muestra comandos disponibles y guía del workflow SPEC-First.

**Tags:** @CODE:ORCH-064

## Usage

```bash
/mj2:help                  # Lista todos los comandos
/mj2:help workflow         # Explicación del workflow SPEC-First
/mj2:help commands         # Lista detallada de comandos
/mj2:help COMMAND          # Ayuda de comando específico
```

## Examples

### Example 1: Help General

```bash
$ /mj2:help

🤖 Mr. mj2 - Ayuda

📚 Workflow SPEC-First:

0️⃣  /mj2:0-project           Inicializar proyecto
1️⃣  /mj2:1-plan              Crear SPEC (Plan)
2️⃣  /mj2:2-run               Implementar con TDD (Run)
3️⃣  /mj2:quality-check       Validar calidad
4️⃣  /mj2:3-sync              Sincronizar docs (Sync)

🔧 Comandos adicionales:

📊 Introspección:
   /mj2:status               Ver estado del workflow
   /mj2:help                 Esta ayuda

🌿 Git & Release:
   /mj2:git-merge            Merge feature branch
   /mj2:99-release           Create release

🎨 Frontend & Testing:
   /mj2:2f-build             Build frontend (React)
   /mj2:4-e2e                E2E tests (Playwright)

🚀 DevOps & Database:
   /mj2:5-deploy             Deploy to environment
   /mj2:db-migrate           Database migrations

🎯 Diseño:
   /mj2:api-design           Design APIs (RESTful)
   /mj2:design-component     Design UI components
   /mj2:ux-design            UX research & design

📝 Feedback & Customization:
   /mj2:9-feedback           Manage feedback
   /mj2:create-agent         Create custom agent
   /mj2:create-skill         Create custom skill

💡 Tip: Usa /mj2:status para ver tu posición en el workflow
💡 Usa /mj2:help COMMAND para ayuda específica
```

### Example 2: Help Workflow

```bash
$ /mj2:help workflow

🤖 Mr. mj2 - Workflow SPEC-First

El workflow mj2 sigue el ciclo: SPEC → TEST → CODE → DOC

📋 Fase 0: Inicialización
   /mj2:0-project <nombre>

   Inicializa proyecto con estructura mj2:
   - Crea .mjcuadrado-net-sdk/ directory
   - Genera config.json
   - Configura Git
   - Crea documentación base

   Output: Proyecto listo para crear SPECs
   Next: /mj2:1-plan

📝 Fase 1: Planning
   /mj2:1-plan "feature description"

   Convierte idea en SPEC (EARS format):
   - Analiza requirements
   - Genera SPEC-{DOMAIN}-{NNN}
   - Crea spec.md, plan.md, acceptance.md
   - Crea feature branch

   Output: SPEC lista para implementar
   Next: /mj2:2-run SPEC-ID

🔴🟢♻️ Fase 2: Implementation (TDD)
   /mj2:2-run SPEC-ID

   Ejecuta ciclo TDD:
   - 🔴 RED: Tests fallando
   - 🟢 GREEN: Implementación mínima
   - ♻️ REFACTOR: Mejora calidad

   Output: Código con tests passing
   Next: /mj2:quality-check SPEC-ID

✅ Fase 3: Quality Gate
   /mj2:quality-check SPEC-ID

   Valida calidad del código:
   - Coverage ≥ 85%
   - Tests passing
   - TRUST 5 principles
   - TAG chain completa

   Output: PASS → continuar | FAIL → fix
   Next (if PASS): /mj2:3-sync SPEC-ID

📚 Fase 4: Documentation Sync
   /mj2:3-sync SPEC-ID

   Sincroniza documentación:
   - Actualiza README.md
   - Actualiza CHANGELOG.md
   - Actualiza architecture.md
   - Completa TAG chain (@DOC)

   Output: Workflow completo! 🎉
   Next: Review y crear PR

💡 Tip: Usa /mj2:status para verificar progreso
💡 Cada fase bloquea hasta que la anterior esté completa
```

### Example 3: Help de Comando Específico

```bash
$ /mj2:help 1-plan

🤖 Mr. mj2 - Ayuda: /mj2:1-plan

📋 Propósito:
   Crear SPEC (especificación) para una feature en formato EARS

📝 Usage:
   /mj2:1-plan "feature description"

📖 Examples:
   /mj2:1-plan "user authentication with JWT"
   /mj2:1-plan "payment processing with Stripe"
   /mj2:1-plan "user profile management"

🔄 What it does:
   1. Analiza feature description
   2. Detecta domain (AUTH, USER, API, etc.)
   3. Hace preguntas clarificadoras
   4. Genera SPEC-{DOMAIN}-{NNN}
   5. Crea spec.md, plan.md, acceptance.md
   6. Crea feature branch
   7. Hace commit inicial

✅ Output:
   SPEC creada: SPEC-AUTH-001
   Archivos: spec.md, plan.md, acceptance.md
   Rama: feature/SPEC-AUTH-001
   Próximo: /mj2:2-run AUTH-001

🎯 Agent:
   Delega a: spec-builder.md

📚 Skills:
   - foundation/specs.md
   - foundation/ears.md
   - foundation/tags.md

💡 Tip: Revisa SPEC antes de implementar
💡 SPECs deben ser claras y completas
```

## What it does

1. **Sin argumentos**
   - Lista workflow commands (5 principales)
   - Lista additional commands (~15)
   - Muestra tips útiles

2. **Con argumento "workflow"**
   - Explica workflow SPEC-First completo
   - Detalla cada fase (0-4)
   - Muestra qué hacer en cada paso

3. **Con argumento "commands"**
   - Lista detallada de todos los comandos
   - Agrupados por categoría
   - Con descripción de cada uno

4. **Con argumento COMMAND**
   - Ayuda específica del comando
   - Usage, examples, output
   - Agent que ejecuta
   - Skills que carga

## Available Commands

### 📚 Workflow Commands (Core)

| Comando | Descripción | Agent |
|---------|-------------|-------|
| `/mj2:0-project` | Inicializar proyecto | project-manager |
| `/mj2:1-plan` | Crear SPEC (Plan) | spec-builder |
| `/mj2:2-run` | Implementar con TDD (Run) | tdd-implementer |
| `/mj2:quality-check` | Validar calidad | quality-gate |
| `/mj2:3-sync` | Sincronizar docs (Sync) | doc-syncer |

### 📊 Introspección

| Comando | Descripción | Agent |
|---------|-------------|-------|
| `/mj2:status` | Ver estado del workflow | workflow-status |
| `/mj2:help` | Esta ayuda | - |

### 🌿 Git & Release

| Comando | Descripción | Agent |
|---------|-------------|-------|
| `/mj2:git-merge` | Merge feature branch | git-manager |
| `/mj2:99-release` | Create release | release-manager |

### 🎨 Frontend & Testing

| Comando | Descripción | Agent |
|---------|-------------|-------|
| `/mj2:2f-build` | Build frontend (React) | frontend-builder |
| `/mj2:4-e2e` | E2E tests (Playwright) | e2e-tester |

### 🚀 DevOps & Database

| Comando | Descripción | Agent |
|---------|-------------|-------|
| `/mj2:5-deploy` | Deploy to environment | devops-expert |
| `/mj2:db-migrate` | Database migrations | database-expert |

### 🎯 Diseño

| Comando | Descripción | Agent |
|---------|-------------|-------|
| `/mj2:api-design` | Design APIs (RESTful) | api-designer |
| `/mj2:design-component` | Design UI components | component-designer |
| `/mj2:ux-design` | UX research & design | ui-ux-expert (v0.8.0) |

### 📝 Feedback & Customization

| Comando | Descripción | Agent |
|---------|-------------|-------|
| `/mj2:9-feedback` | Manage feedback | feedback-manager |
| `/mj2:create-agent` | Create custom agent | agent-factory |
| `/mj2:create-skill` | Create custom skill | skill-factory |

### 🔧 Utilities

| Comando | Descripción | Agent |
|---------|-------------|-------|
| `/mj2:debug` | Debug helper | debug-helper |
| `/mj2:migrate` | Migration expert | migration-expert |
| `/mj2:format` | Code formatting (v0.6.0) | format-expert |
| `/mj2:docs` | Docs management (v0.6.0) | docs-manager |
| `/mj2:monitor` | Monitoring setup (v0.8.0) | monitoring-expert |
| `/mj2:perf-analyze` | Performance analysis | performance-engineer |
| `/mj2:a11y-audit` | Accessibility audit | accessibility-expert |

**Total:** 20+ comandos (21 existentes + 3 proyectados)

## Tips

💡 **Para comenzar:**
```bash
/mj2:help workflow    # Ver proceso completo
/mj2:0-project        # Inicializar
```

💡 **Durante desarrollo:**
```bash
/mj2:status           # Ver progreso
/mj2:help COMMAND     # Ayuda específica
```

💡 **Troubleshooting:**
```bash
/mj2:status SPEC-ID   # Ver estado de SPEC
/mj2:help workflow    # Revisar proceso
```

## Integration

**Referenciado desde:**
- README.md (comandos útiles)
- Outputs de agentes ("Ver ayuda: /mj2:help")
- `/mj2:status` (tips)

**Referencias a:**
- Todos los comandos existentes
- Workflow documentation
- orchestration-patterns.md skill

## Related Commands

- `/mj2:status` - Ver estado del workflow
- `/mj2:0-project` - Inicializar proyecto
- `/mj2:1-plan` - Crear SPEC

---

**Version:** 1.0.0
**Created:** 2025-11-23
**Tags:** @CODE:ORCH-064
