# Issue #45: Agent Factory & Skill Factory

**Fecha:** 2025-11-23
**Prioridad:** 🔴 Alta (GAME CHANGER)
**Estado:** ✅ Completado
**Branch:** `feature/issue-45-agent-skill-factory`

---

## 📋 Descripción

Meta-agentes que permiten crear nuevos agentes y skills de forma guiada, democratizando la extensibilidad de mj2 y permitiendo a usuarios crear sus propios componentes especializados.

**GAME CHANGER:** Este issue convierte mj2 en una plataforma extensible por usuarios, no solo por desarrolladores core.

---

## 🎯 Objetivos

- [x] Crear agent-factory meta-agente
- [x] Crear skill-factory meta-agente
- [x] Implementar /mj2:create-agent command
- [x] Implementar /mj2:create-skill command
- [x] Documentar patrones y validaciones
- [x] Proveer ejemplos completos
- [x] Definir criterios de calidad

---

## 📦 Entregables

### 1. Agent Factory Meta-Agente
**Archivo:** `.claude/agents/mj2/agent-factory.md` (683 líneas)

**Características:**
- **Workflow 4 fases:** ANALYZE → DESIGN → GENERATE → VALIDATE
- **Análisis de dominio:** 9 dominios soportados
- **Workflow patterns:** 5 patterns predefinidos (generator, implementer, validator, orchestrator, designer)
- **Generación automática:** Estructura completa con TRUST 5 principles
- **Validación exhaustiva:** Metadata, estructura, calidad de contenido

**Dominios Soportados:**
1. Backend (ASP.NET Core, EF Core, APIs)
2. Frontend (React, TypeScript, MUI)
3. Testing (Unit, Integration, E2E)
4. DevOps (Docker, CI/CD, deployment)
5. Architecture (Patterns, design)
6. Security (Auth, OWASP, encryption)
7. Performance (Optimization, caching)
8. Quality (Code review, validation)
9. Meta (Factories, orchestrators)

**Workflow Patterns:**
1. **Generator** (4 fases): PLAN → GENERATE → VALIDATE → REFINE
2. **Implementer** (3 fases): RED → GREEN → REFACTOR
3. **Validator** (4 fases): ASSESS → IDENTIFY → FIX → VERIFY
4. **Orchestrator** (4 fases): ANALYZE → PLAN → EXECUTE → REPORT
5. **Designer** (4 fases): ANALYZE → DESIGN → DOCUMENT → VALIDATE

**Estructura de Agente Generado:**
- Frontmatter (agent, description, version, tags)
- Introducción con "Soy el **X**"
- Sección Persona (rol, misión, filosofía, especialidad)
- TRUST 5 Principles (5 subsecciones)
- Workflow con ASCII diagram
- 2-5 fases detalladas
- 2-3 ejemplos de uso completos
- Skills relacionadas
- Criterios de éxito con checkboxes
- Footer completo

**Validaciones:**
- ✅ Frontmatter YAML válido
- ✅ Headings jerárquicos
- ✅ Secciones obligatorias completas
- ✅ Naming conventions (kebab-case)
- ✅ Calidad de contenido (> 600 líneas)
- ✅ Code snippets ejecutables
- ✅ Ejemplos completos

### 2. Skill Factory Meta-Agente
**Archivo:** `.claude/agents/mj2/skill-factory.md` (826 líneas)

**Características:**
- **Workflow 4 fases:** RESEARCH → STRUCTURE → GENERATE → VALIDATE
- **Categorías:** 7 categorías soportadas
- **Niveles:** Básico (300-500), Intermedio (500-800), Avanzado (800-1,200 líneas)
- **Fuentes:** Documentación oficial, best practices, community resources
- **Validación completa:** Metadata, estructura, code quality, links

**Categorías Soportadas:**
1. Backend (`backend/`) - .NET, APIs, database
2. Frontend (`frontend/`) - React, TypeScript, MUI
3. Architecture (`architecture/`) - Patterns, design
4. Testing (`testing/`) - Unit, integration, E2E
5. DevOps (`devops/`) - Docker, CI/CD, cloud
6. Security (`security/`) - Auth, OWASP, encryption
7. Performance (`performance/`) - Optimization, caching

**Niveles de Detalle:**

**Básico** (300-500 líneas):
- Conceptos fundamentales
- Syntax básica
- 5-8 ejemplos simples
- 3 best practices esenciales
- 2 anti-patterns comunes

**Intermedio** (500-800 líneas):
- Conceptos avanzados
- 10-15 ejemplos completos
- 5 best practices detalladas
- 3 anti-patterns explicados
- Integration con otras tecnologías

**Avanzado** (800-1,200 líneas):
- Conceptos expertos
- 15-20 ejemplos complejos
- Performance optimization profunda
- Security considerations
- Real-world case studies

**Estructura de Skill Generada:**
- Frontmatter (skill, description, category, tags, difficulty, version)
- Introducción con "¿Cuándo usar?" y "¿Cuándo NO usar?"
- Conceptos Fundamentales
- Instalación/Setup
- Uso Básico con ejemplos
- Características Principales
- Patrones Comunes
- Casos Avanzados
- Performance & Optimization
- Seguridad
- Anti-Patterns
- Testing
- Referencias con links
- Footer completo

**Validaciones:**
- ✅ Frontmatter válido con difficulty
- ✅ Introducción con casos de uso
- ✅ Mínimo líneas según nivel
- ✅ 5+ code snippets funcionales
- ✅ 3+ best practices
- ✅ 2+ anti-patterns
- ✅ Referencias a docs oficiales

### 3. Comando /mj2:create-agent
**Archivo:** `.claude/commands/mj2-create-agent.md` (373 líneas)

**Sintaxis:**
```bash
/mj2:create-agent "<descripción>" [options]

Options:
--domain <dominio>     # backend, frontend, testing, devops, etc.
--workflow <pattern>   # generator, implementer, validator, orchestrator, designer
--skills <skills>      # Lista de skills (separadas por coma)
--output <path>        # Path de salida (default: .claude/agents/mj2/)
```

**Ejemplos de Uso:**

1. **Database Migration Agent:**
```bash
/mj2:create-agent "Gestionar migraciones de base de datos" --domain backend --workflow generator
```

2. **Code Reviewer Agent:**
```bash
/mj2:create-agent "Code review automatizado" --domain quality --workflow validator
```

3. **Modo Interactivo:**
```bash
/mj2:create-agent "Análisis de performance de APIs"
# Pregunta dominio, workflow, skills interactivamente
```

**Workflow del Comando:**
1. **ANALYZE:** Captura requerimientos y analiza dominio
2. **DESIGN:** Diseña estructura del agente con workflow
3. **GENERATE:** Genera contenido completo del agente
4. **VALIDATE:** Valida estructura, calidad y formato

**Output:**
- Archivo `.claude/agents/mj2/<nombre-agente>.md`
- 600-800 líneas típicamente
- Todas las validaciones pasadas
- Listo para usar inmediatamente

### 4. Comando /mj2:create-skill
**Archivo:** `.claude/commands/mj2-create-skill.md` (527 líneas)

**Sintaxis:**
```bash
/mj2:create-skill "<tecnología>" [options]

Options:
--category <categoría>   # backend, frontend, testing, devops, etc.
--difficulty <nivel>     # básico, intermedio, avanzado
--output <path>          # Path de salida (default: .claude/skills/<category>/)
```

**Ejemplos de Uso:**

1. **Mapster Skill (Intermedio):**
```bash
/mj2:create-skill "Mapster object mapping" --category dotnet --difficulty intermedio
# Output: .claude/skills/dotnet/mapster.md (650-750 líneas)
```

2. **Vitest Skill (Intermedio):**
```bash
/mj2:create-skill "Vitest testing framework" --category testing --difficulty intermedio
# Output: .claude/skills/testing/vitest.md (600-700 líneas)
```

3. **Git Basics (Básico):**
```bash
/mj2:create-skill "Git basics" --category foundation --difficulty básico
# Output: .claude/skills/foundation/git-basics.md (350-450 líneas)
```

4. **Kubernetes (Avanzado):**
```bash
/mj2:create-skill "Kubernetes orchestration" --difficulty avanzado
# Output: .claude/skills/devops/kubernetes.md (950-1,150 líneas)
```

**Workflow del Comando:**
1. **RESEARCH:** Investiga documentación oficial y best practices
2. **STRUCTURE:** Organiza contenido en secciones estándar
3. **GENERATE:** Genera skill completa con code snippets
4. **VALIDATE:** Valida estructura, código y referencias

**Output:**
- Archivo `.claude/skills/<categoría>/<nombre-skill>.md`
- 300-1,200 líneas según dificultad
- Code snippets ejecutables
- Best practices y anti-patterns
- Referencias a docs oficiales

---

## 🔄 Workflows Implementados

### Agent Factory Workflow

```
📋 ANALYZE
  ↓ Capturar requerimientos del usuario
  ↓ Identificar dominio (9 opciones)
  ↓ Analizar agentes similares existentes
  ↓ Definir workflow (5 patterns)

🏗️ DESIGN
  ↓ Diseñar estructura del agente
  ↓ Definir secciones (persona, workflow, examples)
  ↓ Planificar integration con skills
  ↓ Establecer criterios de éxito

✨ GENERATE
  ↓ Generar frontmatter (metadata)
  ↓ Crear sección Persona
  ↓ Implementar TRUST 5 Principles
  ↓ Desarrollar Workflow detallado (2-5 fases)
  ↓ Agregar ejemplos de uso (2-3)
  ↓ Incluir criterios de éxito

✅ VALIDATE
  ↓ Validar estructura markdown
  ↓ Verificar secciones obligatorias
  ↓ Comprobar formato TRUST 5
  ↓ Revisar ejemplos ejecutables
  ↓ Confirmar con usuario
```

### Skill Factory Workflow

```
📚 RESEARCH
  ↓ Identificar dominio y tecnología (7 categorías)
  ↓ Investigar documentación oficial
  ↓ Analizar best practices
  ↓ Revisar skills similares existentes
  ↓ Determinar nivel (básico, intermedio, avanzado)

🏗️ STRUCTURE
  ↓ Definir secciones de la skill (12-15)
  ↓ Organizar contenido jerárquicamente
  ↓ Planificar ejemplos (5-20 según nivel)
  ↓ Establecer niveles de detalle

✨ GENERATE
  ↓ Crear frontmatter con difficulty
  ↓ Escribir introducción (cuándo usar/no usar)
  ↓ Desarrollar conceptos fundamentales
  ↓ Agregar code snippets (5-20)
  ↓ Incluir best practices (3-8)
  ↓ Documentar anti-patterns (2-5)
  ↓ Crear ejemplos completos
  ↓ Referencias a docs oficiales

✅ VALIDATE
  ↓ Validar estructura markdown
  ↓ Verificar code snippets ejecutables
  ↓ Comprobar referencias válidas
  ↓ Revisar completitud según nivel
  ↓ Confirmar con usuario
```

---

## 📊 Métricas

**Archivos Creados:** 4
- 2 meta-agentes (agent-factory, skill-factory)
- 2 comandos (/mj2:create-agent, /mj2:create-skill)

**Líneas de Código:** 2,409
- agent-factory.md: 683 líneas
- skill-factory.md: 826 líneas
- mj2-create-agent.md: 373 líneas
- mj2-create-skill.md: 527 líneas

**Dominios Soportados:** 9
- Backend, Frontend, Testing, DevOps, Architecture, Security, Performance, Quality, Meta

**Workflow Patterns:** 5
- Generator, Implementer, Validator, Orchestrator, Designer

**Categorías de Skills:** 7
- Backend, Frontend, Architecture, Testing, DevOps, Security, Performance

**Niveles de Skills:** 3
- Básico (300-500 líneas)
- Intermedio (500-800 líneas)
- Avanzado (800-1,200 líneas)

**Validaciones por Agente:** 12+
- Metadata, estructura, calidad, naming, ejemplos, etc.

**Validaciones por Skill:** 15+
- Metadata, estructura, código, links, completitud, etc.

---

## 🔧 Integración con mj2

### Agentes Existentes que se Benefician

**Todos los agentes pueden ser creados ahora con:**
```bash
/mj2:create-agent "<descripción>" --domain <dominio> --workflow <pattern>
```

**Ejemplos:**
- spec-builder → `/mj2:create-agent "Generar SPECs en formato EARS" --domain meta --workflow generator`
- tdd-implementer → `/mj2:create-agent "Implementar features con TDD" --domain testing --workflow implementer`
- quality-gate → `/mj2:create-agent "Validar calidad con TRUST 5" --domain quality --workflow validator`

### Skills Existentes pueden ser Replicadas

**Todas las skills pueden ser creadas ahora con:**
```bash
/mj2:create-skill "<tecnología>" --category <categoría> --difficulty <nivel>
```

**Ejemplos:**
- react.md → `/mj2:create-skill "React 18" --category frontend --difficulty intermedio`
- ef-core.md → `/mj2:create-skill "Entity Framework Core" --category backend --difficulty intermedio`
- docker.md → `/mj2:create-skill "Docker" --category devops --difficulty intermedio`

---

## 💡 Valor Aportado

### Para Usuarios

**Antes:**
- Solo podían usar agentes y skills predefinidos
- Necesitaban conocimiento profundo de markdown y patrones mj2
- Creación manual propensa a errores
- Inconsistencia entre componentes creados por diferentes personas

**Ahora:**
- Pueden crear agentes y skills propios fácilmente
- Workflow guiado con validación automática
- Generación consistente siguiendo patrones mj2
- Extensibilidad democratizada

### Para mj2

**Escalabilidad:**
- Usuarios pueden extender mj2 sin modificar el core
- Community-driven growth de agentes y skills
- Especialización por dominio

**Consistencia:**
- Todos los agentes siguen misma estructura
- Todas las skills tienen formato uniforme
- TRUST 5 principles aplicados automáticamente

**Calidad:**
- Validaciones automáticas garantizan calidad
- Best practices incorporadas por defecto
- Anti-patterns documentados

---

## ✅ Criterios de Éxito

Al completar este issue, el proyecto tiene:

- [x] **Agent Factory completamente funcional**
  - 9 dominios soportados
  - 5 workflow patterns
  - Generación automática completa
  - Validación exhaustiva (12+ checks)

- [x] **Skill Factory completamente funcional**
  - 7 categorías soportadas
  - 3 niveles de dificultad
  - Generación según nivel
  - Validación exhaustiva (15+ checks)

- [x] **Comandos implementados**
  - /mj2:create-agent con options
  - /mj2:create-skill con options
  - Modo interactivo en ambos
  - Error handling completo

- [x] **Documentación completa**
  - Ejemplos de uso detallados
  - Workflows explicados paso a paso
  - Validaciones documentadas
  - Referencias cruzadas

- [x] **Extensibilidad garantizada**
  - Usuarios pueden crear agentes propios
  - Usuarios pueden crear skills propias
  - Consistencia automática
  - Calidad validada

---

## 🚀 Próximos Pasos Sugeridos

### Crear Agentes Útiles

```bash
# Migration Manager
/mj2:create-agent "Gestionar migraciones de EF Core" --domain backend --workflow generator

# Code Reviewer
/mj2:create-agent "Code review automatizado" --domain quality --workflow validator

# API Performance Analyzer
/mj2:create-agent "Analizar performance de APIs" --domain performance --workflow orchestrator
```

### Crear Skills Faltantes

```bash
# Mapster (Backend)
/mj2:create-skill "Mapster object mapping" --category backend --difficulty intermedio

# Zod (Frontend)
/mj2:create-skill "Zod schema validation" --category frontend --difficulty básico

# Redis (Performance)
/mj2:create-skill "Redis caching" --category performance --difficulty intermedio
```

---

## 📚 Documentación Relacionada

- [Agent Factory Agent](.claude/agents/mj2/agent-factory.md)
- [Skill Factory Agent](.claude/agents/mj2/skill-factory.md)
- [Comando /mj2:create-agent](.claude/commands/mj2-create-agent.md)
- [Comando /mj2:create-skill](.claude/commands/mj2-create-skill.md)
- [ROADMAP](docs/ROADMAP.md)

---

## 🔗 Referencias

**GitHub Issue:** https://github.com/mjcuadrado/mjcuadrado-net-sdk/issues/45

**Inspirado por:** moai-adk/agent-factory, moai-adk/skill-factory

---

**Versión:** 1.0.0
**Completado:** 2025-11-23
**Tiempo Estimado:** 6-7 días
**Tiempo Real:** ~4 horas
**Mantenido por:** mjcuadrado-net-sdk
**Impacto:** 🚀 GAME CHANGER - mj2 es ahora extensible por usuarios
