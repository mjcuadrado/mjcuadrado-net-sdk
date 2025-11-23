---
name: mj2-create-skill
description: Crea una nueva skill de conocimiento siguiendo patrones mj2
tags: [meta, factory, skills, knowledge]
---

# /mj2:create-skill - Skill Factory

Comando para crear nuevas skills de conocimiento especializado de forma guiada siguiendo los patrones de mj2.

---

## 📋 Uso

```bash
# Sintaxis básica
/mj2:create-skill "<tecnología>" [options]

# Con opciones
/mj2:create-skill "<tecnología>" --category <categoría> --difficulty <nivel>

# Modo interactivo (sin opciones)
/mj2:create-skill "<tecnología>"
```

---

## 🎯 Opciones

### --category <categoría>
Especifica la categoría de la skill

**Valores válidos:**
- `backend` - Backend (.NET, APIs, database)
- `frontend` - Frontend (React, TypeScript, UI)
- `architecture` - Architecture (Patterns, design)
- `testing` - Testing (Unit, integration, E2E)
- `devops` - DevOps (Docker, CI/CD, cloud)
- `security` - Security (Auth, OWASP, encryption)
- `performance` - Performance (Optimization, caching)

### --difficulty <nivel>
Especifica el nivel de dificultad

**Valores válidos:**
- `básico` - Conceptos fundamentales (300-500 líneas)
- `intermedio` - Conceptos avanzados (500-800 líneas)
- `avanzado` - Conceptos expertos (800-1,200 líneas)

### --output <path>
Path de salida (default: `.claude/skills/<category>/`)

---

## 💡 Ejemplos

### Ejemplo 1: Crear Skill de Mapster

```bash
/mj2:create-skill "Mapster object mapping" --category dotnet --difficulty intermedio
```

**Output:**
```markdown
✨ Creando skill: mapster

📚 RESEARCH
- Tecnología: Mapster
- Categoría: dotnet
- Nivel: intermedio
- Fuentes encontradas:
  - https://github.com/MapsterMapper/Mapster ✓
  - NuGet package docs ✓
  - Community blog posts ✓

🏗️ STRUCTURE
- Nombre: mapster
- Path: .claude/skills/dotnet/mapster.md
- Secciones planificadas: 12
- Ejemplos: 10+
- Líneas estimadas: 650

✨ GENERATE
- [1/12] Frontmatter... ✓
- [2/12] Introducción... ✓
- [3/12] Conceptos Fundamentales... ✓
- [4/12] Instalación... ✓
- [5/12] Uso Básico (5 ejemplos)... ✓
- [6/12] Características Principales... ✓
- [7/12] Patrones Comunes... ✓
- [8/12] Performance vs AutoMapper... ✓
- [9/12] Best Practices (5)... ✓
- [10/12] Anti-Patterns (3)... ✓
- [11/12] Testing... ✓
- [12/12] Referencias... ✓

✅ VALIDATE
- Frontmatter válido ✓
- 12 secciones completas ✓
- 10 code snippets ejecutables ✓
- 5 best practices ✓
- 3 anti-patterns ✓
- Referencias a docs oficiales ✓

✅ Skill creada exitosamente!
📁 .claude/skills/dotnet/mapster.md (672 líneas)

📝 Próximos pasos:
1. Revisar la skill generada
2. Validar code snippets
3. Agregar ejemplos adicionales si es necesario
4. Referenciar desde agentes relevantes
```

### Ejemplo 2: Crear Skill de Vitest

```bash
/mj2:create-skill "Vitest testing framework" --category testing --difficulty intermedio
```

**Output:**
```markdown
✨ Creando skill: vitest

📚 RESEARCH
- Tecnología: Vitest
- Categoría: testing
- Nivel: intermedio
- Documentación: https://vitest.dev/ ✓

🏗️ STRUCTURE
- Secciones: 12
- Ejemplos de testing: 15+
- Mocking patterns: 5
- React integration: ✓

✨ GENERATE
<generación completa>

✅ Skill creada: .claude/skills/testing/vitest.md (622 líneas)
```

### Ejemplo 3: Modo Interactivo

```bash
/mj2:create-skill "RabbitMQ"
```

**Interacción:**
```markdown
✨ Skill Factory - Modo Interactivo

❓ ¿Qué categoría? (backend, frontend, testing, devops, etc.)
→ backend

❓ ¿Qué nivel de dificultad? (básico, intermedio, avanzado)
→ avanzado

❓ ¿Versión de la tecnología? (o ENTER para latest)
→ 3.12

📚 RESEARCH
- Investigando RabbitMQ 3.12...
- Documentación oficial encontrada ✓
- Skills relacionadas: docker.md, aspnet-core.md

¿Proceder con generación? (y/n)
→ y

✨ Generando skill...
✅ Skill creada: .claude/skills/backend/rabbitmq.md (980 líneas)
```

### Ejemplo 4: Skill Básica

```bash
/mj2:create-skill "Git basics" --category foundation --difficulty básico
```

**Output:**
```markdown
✨ Creando skill: git-basics

📚 RESEARCH
- Nivel: básico (300-500 líneas)
- Enfoque: Conceptos fundamentales
- Ejemplos: Simples y didácticos

✨ GENERATE
- Secciones: 8 (enfocadas en básicos)
- Ejemplos: 8 (comandos esenciales)
- Best practices: 3 fundamentales

✅ Skill creada: .claude/skills/foundation/git-basics.md (385 líneas)
```

---

## 🔄 Workflow Detallado

### Fase 1: RESEARCH (Investigación)

**Input del usuario:**
```bash
/mj2:create-skill "SignalR real-time communication"
```

**Investigación automática:**
```markdown
📚 Investigando SignalR...

Tecnología detectada: SignalR (Microsoft)
Categoría sugerida: backend
Nivel sugerido: intermedio
Versión actual: ASP.NET Core SignalR

Fuentes encontradas:
- https://learn.microsoft.com/en-us/aspnet/core/signalr/ ✓
- GitHub: https://github.com/dotnet/aspnetcore (SignalR) ✓
- NuGet: Microsoft.AspNetCore.SignalR ✓

Skills relacionadas:
- aspnet-core.md (existe)
- websockets.md (no existe, crear después?)

Nombre sugerido: signalr
Path sugerido: .claude/skills/backend/signalr.md
Tags sugeridas: [dotnet, signalr, real-time, websockets, backend]
```

### Fase 2: STRUCTURE (Estructuración)

**Diseño automático:**
```markdown
🏗️ Estructurando contenido...

Nivel: intermedio (500-800 líneas)

Secciones planificadas:
1. Introducción (¿Cuándo usar SignalR?)
2. Conceptos Fundamentales (Hubs, Connections, Groups)
3. Instalación y Setup
4. Uso Básico (Simple Hub, Client connection)
5. Características Principales (Broadcasting, Groups, etc.)
6. Patrones Comunes (Chat, Notifications, Real-time dashboard)
7. Casos Avanzados (Scaling con Redis, Azure SignalR Service)
8. Performance & Optimization
9. Seguridad (Authentication, Authorization)
10. Anti-Patterns (Common mistakes)
11. Testing (Unit testing Hubs)
12. Referencias

Ejemplos planificados: 12
- Simple Hub: ✓
- Client connection (TypeScript): ✓
- Broadcasting to all: ✓
- Groups management: ✓
- Strongly-typed hubs: ✓
- Authentication: ✓
- etc.

Líneas estimadas: 720
```

### Fase 3: GENERATE (Generación)

**Generación automática:**
```markdown
✨ Generando contenido de la skill...

[1/12] Frontmatter... ✓
---
skill: signalr
description: SignalR - Real-time web communication para ASP.NET Core
category: backend
tags: [dotnet, signalr, real-time, websockets, backend]
difficulty: intermedio
version: ASP.NET Core 8.0
---

[2/12] Introducción... ✓
# SignalR

SignalR es una biblioteca de ASP.NET Core que simplifica agregar funcionalidad web real-time a aplicaciones.

**¿Cuándo usar SignalR?**
- Chat applications
- Real-time dashboards
- Live notifications
- Collaborative apps
- Gaming

**¿Cuándo NO usar SignalR?**
- RESTful APIs simples
- One-way communication (usar Server-Sent Events)
- High-frequency trading (latency crítica)

[3/12] Conceptos Fundamentales... ✓
## 📋 Conceptos Fundamentales

### 1. Hub
El Hub es el concepto central de SignalR...

[4-12] Resto de secciones... ✓

Total generado: 735 líneas
Code snippets: 14
Best practices: 5
Anti-patterns: 3
```

### Fase 4: VALIDATE (Validación)

**Validación automática:**
```markdown
✅ Validando skill generada...

Metadata:
- Frontmatter YAML válido ✓
- skill: signalr (kebab-case) ✓
- category: backend ✓
- difficulty: intermedio ✓
- tags: 5 tags relevantes ✓

Estructura:
- H1 title ✓
- Introducción con "¿Cuándo usar?" ✓
- Conceptos Fundamentales ✓
- Instalación/Setup ✓
- Uso Básico (3+ ejemplos) ✓
- Características Principales (5+) ✓
- Patrones Comunes (3+) ✓
- Best Practices (5) ✓
- Anti-Patterns (3) ✓
- Testing section ✓
- Referencias con links ✓
- Footer completo ✓

Calidad:
- Total líneas: 735 (500-800 target) ✓
- Code snippets: 14 (> 5 mínimo) ✓
- Syntax highlighting correcto ✓
- Código ejecutable ✓
- Links a docs oficiales válidos ✓

✅ Validación exitosa!
```

---

## 📊 Niveles de Detalle

### Básico (300-500 líneas)

**Características:**
- Conceptos fundamentales solamente
- Syntax básica
- 5-8 ejemplos simples
- 3 best practices esenciales
- 2 anti-patterns comunes

**Ejemplo:**
```bash
/mj2:create-skill "JSON basics" --difficulty básico
```

**Secciones generadas:**
1. Introducción
2. Conceptos Fundamentales (3)
3. Syntax Básica
4. Uso Básico (5 ejemplos)
5. Best Practices (3)
6. Anti-Patterns (2)
7. Referencias

**Líneas típicas:** 350-450

### Intermedio (500-800 líneas)

**Características:**
- Conceptos avanzados incluidos
- Patrones comunes documentados
- 10-15 ejemplos completos
- 5 best practices detalladas
- 3 anti-patterns explicados
- Integration con otras tecnologías

**Ejemplo:**
```bash
/mj2:create-skill "Entity Framework Core" --difficulty intermedio
```

**Secciones generadas:**
1. Introducción
2. Conceptos Fundamentales (5)
3. Instalación y Setup
4. Uso Básico (5 ejemplos)
5. Características Principales (8)
6. Patrones Comunes (5)
7. Casos Avanzados (3)
8. Performance & Optimization
9. Best Practices (5)
10. Anti-Patterns (3)
11. Testing
12. Referencias

**Líneas típicas:** 600-750

### Avanzado (800-1,200 líneas)

**Características:**
- Conceptos expertos y edge cases
- Advanced patterns
- 15-20 ejemplos complejos
- Performance optimization profunda
- Security considerations
- Real-world case studies
- Integration patterns

**Ejemplo:**
```bash
/mj2:create-skill "Kubernetes orchestration" --difficulty avanzado
```

**Secciones generadas:**
1. Introducción
2. Conceptos Fundamentales (8)
3. Architecture Deep-Dive
4. Instalación y Setup (múltiples escenarios)
5. Uso Básico (8 ejemplos)
6. Características Avanzadas (12)
7. Patrones de Producción (8)
8. Scaling y High Availability
9. Performance Optimization (avanzada)
10. Security Hardening
11. Monitoring y Troubleshooting
12. Case Studies (3)
13. Best Practices (8)
14. Anti-Patterns (5)
15. Testing Strategies
16. Referencias Extensas

**Líneas típicas:** 950-1,150

---

## 📚 Ver También

- **Agente:** `.claude/agents/mj2/skill-factory.md`
- **Comando agente:** `/mj2:create-agent` (para crear agentes)
- **Documentación:** `.github/issues/issue-45.md`

---

## ✅ Salida Esperada

Al ejecutar este comando exitosamente, se genera:

1. **Archivo de la skill:**
   - Path: `.claude/skills/<categoría>/<nombre-skill>.md`
   - Líneas: 300-1,200 según dificultad
   - Formato: Markdown con frontmatter YAML

2. **Contenido completo:**
   - Frontmatter con metadata
   - Introducción con cuándo usar/no usar
   - Conceptos fundamentales
   - Instalación/Setup
   - 5-20 code snippets ejecutables
   - 3-8 best practices
   - 2-5 anti-patterns
   - Referencias a docs oficiales

3. **Validación pasada:**
   - Todas las secciones obligatorias
   - Naming conventions correctas
   - Code snippets con syntax correcta
   - Links válidos

---

## 🚨 Errores Comunes

### Error: Categoría no válida

```bash
/mj2:create-skill "Mi skill" --category invalid
```

**Error:**
```
❌ Error: Categoría 'invalid' no es válida.
Categorías válidas: backend, frontend, architecture, testing, devops, security, performance
```

### Error: Dificultad no válida

```bash
/mj2:create-skill "Mi skill" --difficulty expert
```

**Error:**
```
❌ Error: Dificultad 'expert' no es válida.
Niveles válidos: básico, intermedio, avanzado
```

### Warning: Tecnología desconocida

```bash
/mj2:create-skill "Tecnología Desconocida"
```

**Warning:**
```
⚠️ Warning: No se encontró documentación oficial para 'Tecnología Desconocida'.
¿Deseas continuar de todas formas? (y/n)
→ y

✨ Generando skill con información limitada...
⚠️ Recuerda revisar y completar con información precisa.
```

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-23
