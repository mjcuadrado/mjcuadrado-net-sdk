---
name: mj2-create-agent
description: Crea un nuevo agente especializado siguiendo patrones mj2
tags: [meta, factory, agents]
---

# /mj2:create-agent - Agent Factory

Comando para crear nuevos agentes especializados de forma guiada siguiendo los patrones y principios de mj2.

---

## 📋 Uso

```bash
# Sintaxis básica
/mj2:create-agent "<descripción>" [options]

# Con opciones
/mj2:create-agent "<descripción>" --domain <dominio> --workflow <pattern>

# Modo interactivo (sin opciones)
/mj2:create-agent "<descripción>"
```

---

## 🎯 Opciones

### --domain <dominio>
Especifica el dominio del agente

**Valores válidos:**
- `backend` - Backend (ASP.NET Core, EF Core, APIs)
- `frontend` - Frontend (React, TypeScript, MUI)
- `testing` - Testing (Unit, Integration, E2E)
- `devops` - DevOps (Docker, CI/CD, deployment)
- `architecture` - Architecture (Patterns, design)
- `security` - Security (Auth, OWASP, encryption)
- `performance` - Performance (Optimization, caching)
- `quality` - Quality (Code review, validation)
- `meta` - Meta (Factories, orchestrators)

### --workflow <pattern>
Especifica el patrón de workflow

**Patrones disponibles:**
- `generator` - PLAN → GENERATE → VALIDATE → REFINE (4 fases)
- `implementer` - RED → GREEN → REFACTOR (3 fases)
- `validator` - ASSESS → IDENTIFY → FIX → VERIFY (4 fases)
- `orchestrator` - ANALYZE → PLAN → EXECUTE → REPORT (4 fases)
- `designer` - ANALYZE → DESIGN → DOCUMENT → VALIDATE (4 fases)

### --skills <skills>
Lista de skills a usar (separadas por coma)

**Ejemplo:**
```bash
--skills "ef-core.md,postgresql.md,docker.md"
```

### --output <path>
Path de salida (default: `.claude/agents/mj2/`)

---

## 💡 Ejemplos

### Ejemplo 1: Crear Database Migration Agent

```bash
/mj2:create-agent "Gestionar migraciones de base de datos" --domain backend --workflow generator
```

**Output:**
```markdown
✨ Creando agente: migration-manager

📋 ANALYZE
- Dominio: backend
- Workflow: PLAN → GENERATE → TEST → APPLY
- Skills relacionadas: ef-core.md, database-expert.md

🏗️ DESIGN
- Nombre: migration-manager
- Descripción: Gestiona migraciones de EF Core con rollback seguro
- Tags: [backend, database, ef-core, migrations]
- Fases: 4 (PLAN → GENERATE → TEST → APPLY)

✨ GENERATE
- Creando frontmatter...
- Generando sección Persona...
- Implementando TRUST 5 Principles...
- Desarrollando workflow (4 fases)...
- Agregando ejemplos de uso (3)...
- Incluyendo criterios de éxito...

✅ VALIDATE
- Frontmatter válido ✓
- 4 fases implementadas ✓
- 3 ejemplos incluidos ✓
- Criterios de éxito medibles ✓

✅ Agente creado exitosamente!
📁 .claude/agents/mj2/migration-manager.md (685 líneas)

📝 Próximos pasos:
1. Revisar el agente generado
2. Ajustar ejemplos si es necesario
3. Crear comando asociado (opcional): /mj2:create-command "migration-manager"
4. Probar el agente
```

### Ejemplo 2: Crear Code Reviewer Agent

```bash
/mj2:create-agent "Code review automatizado" --domain quality --workflow validator
```

**Output:**
```markdown
✨ Creando agente: code-reviewer

📋 ANALYZE
- Dominio: quality
- Workflow: ANALYZE → IDENTIFY → RECOMMEND → REPORT
- Skills relacionadas: csharp.md, typescript.md

🏗️ DESIGN
- Nombre: code-reviewer
- Descripción: Code review automatizado siguiendo best practices
- Tags: [quality, review, best-practices, automation]
- Fases: 4 (ANALYZE → IDENTIFY → RECOMMEND → REPORT)

✨ GENERATE
<generación completa del agente>

✅ Agente creado: .claude/agents/mj2/code-reviewer.md (720 líneas)
```

### Ejemplo 3: Modo Interactivo

```bash
/mj2:create-agent "Análisis de performance de APIs"
```

**Interacción:**
```markdown
✨ Agent Factory - Modo Interactivo

❓ ¿Qué dominio? (backend, frontend, testing, devops, etc.)
→ backend

❓ ¿Qué patrón de workflow? (generator, implementer, validator, orchestrator, designer)
→ orchestrator

❓ ¿Qué skills necesita? (separadas por coma, o ENTER para auto-detectar)
→ performance-optimization.md, aspnet-core.md

📋 ANALYZE
- Dominio: backend
- Workflow: ANALYZE → PLAN → EXECUTE → REPORT
- Skills: performance-optimization.md, aspnet-core.md

¿Proceder con generación? (y/n)
→ y

✨ Generando agente...
✅ Agente creado: .claude/agents/mj2/api-performance-analyzer.md
```

### Ejemplo 4: Con Skills Específicas

```bash
/mj2:create-agent "API testing automation" --domain testing --skills "playwright.md,react-testing-library.md"
```

**Output:**
```markdown
✨ Creando agente: api-test-automator

Skills detectadas:
- playwright.md ✓
- react-testing-library.md ✓

Agente generado con integration a skills especificadas.
```

---

## 🔄 Workflow Detallado

### Fase 1: ANALYZE (Análisis de Requerimientos)

**Input del usuario:**
```bash
/mj2:create-agent "Gestionar deployments con zero-downtime"
```

**Análisis automático:**
```markdown
📋 Analizando requerimientos...

Dominio detectado: devops (keywords: deployments, zero-downtime)
Workflow sugerido: orchestrator (ANALYZE → PLAN → EXECUTE → REPORT)
Skills relacionadas encontradas:
- docker.md
- docker-compose.md
- devops-expert.md (agente similar)

Nombre sugerido: deployment-manager
Tags sugeridas: [devops, deployment, zero-downtime, blue-green]
```

### Fase 2: DESIGN (Diseño de Estructura)

**Diseño automático:**
```markdown
🏗️ Diseñando estructura del agente...

Nombre: deployment-manager
Descripción: Gestiona deployments con zero-downtime usando estrategias Blue-Green

Workflow (4 fases):
1. ANALYZE - Analizar estado actual y requirements
2. PLAN - Planificar estrategia de deployment
3. EXECUTE - Ejecutar deployment con rollback capability
4. REPORT - Generar reporte y métricas

Secciones del agente:
- Frontmatter (agent, description, version, tags)
- Persona (rol, misión, filosofía, especialidad)
- TRUST 5 Principles
- Workflow diagram
- 4 fases detalladas
- 3 ejemplos de uso
- Skills relacionadas
- Criterios de éxito
```

### Fase 3: GENERATE (Generación de Contenido)

**Generación automática:**
```markdown
✨ Generando contenido del agente...

[1/8] Frontmatter... ✓
[2/8] Introducción... ✓
[3/8] Sección Persona... ✓
[4/8] TRUST 5 Principles... ✓
[5/8] Workflow diagram... ✓
[6/8] Fases detalladas (4)... ✓
[7/8] Ejemplos de uso (3)... ✓
[8/8] Criterios de éxito... ✓

Líneas generadas: 750
Code snippets: 12
Ejemplos completos: 3
```

### Fase 4: VALIDATE (Validación)

**Validación automática:**
```markdown
✅ Validando agente generado...

Metadata:
- Frontmatter YAML válido ✓
- agent: deployment-manager (kebab-case) ✓
- description: < 80 caracteres ✓
- tags: 4 tags relevantes ✓

Estructura:
- H1 title ✓
- Sección Persona completa ✓
- TRUST 5 Principles (5 subsecciones) ✓
- Workflow diagram ASCII ✓
- 4 fases detalladas (> 50 palabras cada una) ✓
- 3 ejemplos de uso ✓
- Skills relacionadas ✓
- Criterios de éxito (checkboxes) ✓
- Footer completo ✓

Calidad:
- Total líneas: 750 (> 600 mínimo) ✓
- Code snippets: 12 (> 5 mínimo) ✓
- Ejemplos ejecutables ✓
- Referencias a skills existentes ✓

✅ Validación exitosa!
```

---

## 📚 Ver También

- **Agente:** `.claude/agents/mj2/agent-factory.md`
- **Comando skill:** `/mj2:create-skill` (para crear skills)
- **Documentación:** `.github/issues/issue-45.md`

---

## ✅ Salida Esperada

Al ejecutar este comando exitosamente, se genera:

1. **Archivo del agente:**
   - Path: `.claude/agents/mj2/<nombre-agente>.md`
   - Líneas: 600-800 típicamente
   - Formato: Markdown con frontmatter YAML

2. **Contenido completo:**
   - Frontmatter con metadata
   - Sección Persona
   - TRUST 5 Principles aplicados
   - Workflow diagram ASCII
   - 2-5 fases detalladas
   - 2-3 ejemplos de uso completos
   - Skills relacionadas
   - Criterios de éxito con checkboxes

3. **Validación pasada:**
   - Todas las secciones obligatorias
   - Naming conventions correctas
   - Calidad de contenido verificada

---

## 🚨 Errores Comunes

### Error: Dominio no válido

```bash
/mj2:create-agent "Mi agente" --domain invalid
```

**Error:**
```
❌ Error: Dominio 'invalid' no es válido.
Dominios válidos: backend, frontend, testing, devops, architecture, security, performance, quality, meta
```

### Error: Workflow pattern no válido

```bash
/mj2:create-agent "Mi agente" --workflow invalid
```

**Error:**
```
❌ Error: Workflow 'invalid' no es válido.
Patrones válidos: generator, implementer, validator, orchestrator, designer
```

### Error: Skill no encontrada

```bash
/mj2:create-agent "Mi agente" --skills "nonexistent.md"
```

**Warning:**
```
⚠️ Warning: Skill 'nonexistent.md' no encontrada.
¿Quieres crear esta skill primero? (y/n)
→ y

Ejecutando: /mj2:create-skill "nonexistent"
```

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-23
