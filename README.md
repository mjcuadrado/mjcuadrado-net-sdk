# mjcuadrado-net-sdk

SDK para desarrollo automatizado con IA, inspirado en [moai-adk](https://github.com/modu-ai/moai-adk).

[![.NET](https://img.shields.io/badge/.NET-10.0-blue)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com)

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

### Fase 1 (MVP) - En desarrollo

- ✅ Estructura de proyecto completa y automatizada
- ✅ CLI funcional con Spectre.Console
- ⏳ Comando `init` para inicializar proyectos
- ⏳ Comando `doctor` para diagnóstico del sistema
- ✅ Comando `version` para ver versión del SDK
- ⏳ Sistema de templates embebidos
- ⏳ Configuración centralizada en `config.json`

### Fases futuras

- **Fase 2**: Sistema de SPECs y TAGs completo
- **Fase 3**: Integración con EF Core (SQL Server / PostgreSQL)
- **Fase 4**: Comandos avanzados (spec, tags, validate)
- **Fase 5**: Agentes y Skills de Claude Code

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

### Fase 1: MVP (En desarrollo) - Issues #1-#9
- [x] Estructura base del proyecto
- [ ] Comando `init` funcional
- [ ] Comando `doctor` funcional
- [ ] Sistema de templates
- [ ] Documentación completa

### Fase 2: SPECs y TAGs
- [ ] Comando `spec new`
- [ ] Comando `spec validate`
- [ ] Comando `tags validate`
- [ ] Generación automática de reportes

### Fase 3: Base de datos
- [ ] Integración EF Core
- [ ] Soporte SQL Server
- [ ] Soporte PostgreSQL
- [ ] Migraciones automáticas

### Fase 4: Automatización avanzada
- [ ] Hooks automáticos
- [ ] Integración CI/CD
- [ ] Validaciones pre-commit

### Fase 5: IA Completa
- [ ] Agentes especializados
- [ ] Skills completas
- [ ] Generación automática de código desde SPECs

## Arquitectura

Ver documentación detallada de arquitectura en:
- [Visión general](docs/architecture/overview.md)
- [Fase 1 - MVP](docs/architecture/phase-1-mvp.md)

## Documentación

- [Arquitectura](docs/architecture/)
- [Comandos](docs/commands/)
- [Cómo contribuir](docs/contributing.md)

## Issues y desarrollo iterativo

El desarrollo se realiza siguiendo las **9 GitHub Issues** de la Fase 1:

1. [#1 - Estructura base del proyecto](/.github/issues/issue-01.md)
2. [#2 - Sistema de gestión de archivos](/.github/issues/issue-02.md)
3. [#3 - Sistema de configuración](/.github/issues/issue-03.md)
4. [#4 - Servicio de templates](/.github/issues/issue-04.md)
5. [#5 - Comando init](/.github/issues/issue-05.md)
6. [#6 - Comando doctor](/.github/issues/issue-06.md)
7. [#7 - Comando version](/.github/issues/issue-07.md)
8. [#8 - Documentación](/.github/issues/issue-08.md)
9. [#9 - CI/CD](/.github/issues/issue-09.md)

## Inspiración

Este proyecto está inspirado en [moai-adk](https://github.com/modu-ai/moai-adk), adaptando su filosofía y metodología al ecosistema .NET.

## Licencia

[MIT License](LICENSE)

## Autor

**@mjcuadrado**

---

**¿Preguntas o sugerencias?** Abre un [issue](https://github.com/mjcuadrado/mjcuadrado-net-sdk/issues) en GitHub.
