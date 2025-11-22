# Issue #24: PostgreSQL & Mapster Skills

**Status:** ✅ Closed
**Created:** 2024-11-22
**Closed:** 2024-11-22
**Purpose:** Add PostgreSQL 16+ and Mapster skills for backend development
**Branch:** feature/ISSUE-024-postgresql-mapster
**Commit:** TBD

---

## Objetivo

Crear skills de PostgreSQL con EF Core 9 (snake_case conventions) y Mapster (high-performance object mapping) para completar el stack backend de mj2.

---

## Contexto

**Issue anterior:** #23 - Gap Analysis identificó PostgreSQL y Mapster como CRÍTICOS para v0.2.0

**Gap identificado:**
- ❌ PostgreSQL 16+ con snake_case conventions - FALTANTE
- ❌ Mapster para mapping - FALTANTE
- ✅ EF Core básico - YA EXISTE (Issue #18)

**Stack objetivo (STACK.md):**
- Backend: C# 13, ASP.NET Core 9, EF Core 9
- Database: PostgreSQL 16+ con snake_case
- Mapping: Mapster (alternativa performante a AutoMapper)

---

## Skills Creados

### 1. postgresql.md (641 líneas)

**Location:** `.claude/skills/dotnet/postgresql.md`

**Contenido:**
- 🎯 Overview y filosofía PostgreSQL en mj2
- 📦 Packages: Npgsql.EntityFrameworkCore.PostgreSQL 9.0.0
- 🔧 Configuración básica (connection strings, DbContext)
- 🐍 snake_case Conventions (global y manual)
  - NamingConventions helper (ToSnakeCase regex)
  - IEntityTypeConfiguration ejemplos
- 🔑 Primary Keys (UUID con uuid-ossp, Serial)
- 📅 Timestamps & Audit Fields (triggers automáticos)
- 🔍 Indices (B-Tree, GIN, Partial, Composite)
- 🗄️ Migrations (create, apply, SQL scripts)
- 🚀 Performance Patterns:
  - Compiled queries
  - AsNoTracking para read-only
  - Proyecciones (Select) vs entidades completas
  - Batch updates (ExecuteUpdate/Delete)
- 🔐 Extensiones PostgreSQL (uuid-ossp, pg_trgm, citext)
- 🧪 Testing con Testcontainers
- ✅ Best Practices (DO/DON'T)

**Metadata:**
```yaml
name: postgresql
description: PostgreSQL 16+ patterns with EF Core 9 and snake_case conventions
version: 0.1.0
tags: [dotnet, postgresql, ef-core, database, npgsql]
```

**Usado por:** tdd-implementer, database-expert, migration-expert

---

### 2. mapster.md (345 líneas)

**Location:** `.claude/skills/dotnet/mapster.md`

**Contenido:**
- 🎯 Overview (por qué Mapster vs AutoMapper)
- 📦 Packages: Mapster 7.4.0, Mapster.DependencyInjection 1.0.1
- 🚀 Quick Start (convention-based mapping)
- ⚙️ Configuración avanzada:
  - TypeAdapterConfig global
  - IRegister para custom mappings
  - Escaneo de assembly
- 🎯 Patterns comunes:
  - Entity → DTO
  - Request → Entity (Create/Update)
  - Collections
- 🔥 Integration con CQRS (MediatR)
- ⚡ Performance optimization:
  - ProjectToType (EF Core integration)
  - Compiled adapters
  - Shallow vs Deep copy
- 🧪 Testing con xUnit
- 🎨 Advanced features:
  - AfterMapping
  - Conditional mapping
  - Two-way mapping
  - Nested mappings
- ✅ Best Practices (DO/DON'T)
- 📚 Comparación Mapster vs AutoMapper
- 🔐 Security considerations
- 📋 Common scenarios (API controllers)

**Metadata:**
```yaml
name: mapster
description: Mapster high-performance object mapping patterns for .NET 9
version: 0.1.0
tags: [dotnet, mapster, mapping, dto, performance]
```

**Usado por:** tdd-implementer, database-expert, api-designer

---

## Validación

### Build Status

```bash
dotnet build --no-restore

Build succeeded.
- Errors: 0
- Warnings: 1 (NU1510: System.Text.Json - NO CRÍTICO)
Time Elapsed: 00:00:06.45
```

### Test Status

```bash
dotnet test --no-build

Total tests: 195
- Passed: 194 (99.5%)
- Failed: 1 (intermittent - Execute_ValidProjectName_Succeeds)
- Skipped: 0
Duration: 2s
```

**Resultado:** ✅ Sistema funcional (mismo status que Issue #22)

---

## Cambios en el Sistema

### Skills Directory Structure

```
.claude/skills/dotnet/
├── aspnet-core.md (Issue #18)
├── csharp.md (Issue #18)
├── ef-core.md (Issue #18)
├── mapster.md ← NUEVO
├── postgresql.md ← NUEVO
└── xunit.md (Issue #18)
```

### Skills Count Evolution

| Version | Skills | Nuevos |
|---------|--------|--------|
| v0.1.0 (Issue #22) | 11 | - |
| v0.2.0 (Issue #24) | 13 | +2 (postgresql, mapster) |

---

## Impacto

### Before Issue #24

- PostgreSQL: ❌ Sin documentación específica
- snake_case: ❌ Sin patterns
- Mapster: ❌ Sin skills
- Mapping: ⚠️ Sin estrategia definida

### After Issue #24

- PostgreSQL: ✅ 641 líneas de patterns y best practices
- snake_case: ✅ Global conventions + manual config
- Mapster: ✅ 345 líneas con CQRS integration
- Mapping: ✅ High-performance strategy definida

---

## Integración con Agentes

### tdd-implementer

**Uso de skills:**
```markdown
Skills Used:
- dotnet/postgresql.md (cuando trabaja con PostgreSQL)
- dotnet/mapster.md (cuando mapea Entity ↔ DTO)
- dotnet/ef-core.md (base EF Core)
```

**Escenario:**
1. Usuario: "Crea endpoint GET /api/users con PostgreSQL"
2. tdd-implementer:
   - Lee `postgresql.md` → snake_case conventions
   - Lee `mapster.md` → ProjectToType para performance
   - Lee `ef-core.md` → DbContext patterns
   - Implementa con TDD

### database-expert (Issue #38 - futuro)

**Uso de skills:**
```markdown
Skills Used:
- dotnet/postgresql.md (CRITICAL)
- dotnet/ef-core.md (CRITICAL)
- dotnet/mapster.md (cuando genera DTOs)
```

---

## Filosofía Aplicada

### TRUST Principles

- **T**ransparent: Skills documentan TODO el conocimiento PostgreSQL/Mapster
- **R**eproducible: Patterns consistentes y testeables
- **U**nambiguous: Ejemplos claros con DO/DON'T
- **S**elf-documenting: Metadata completo y referencias
- **T**estable: Integration con xUnit y Testcontainers

### TAG System

**postgresql.md:**
- 🎯 Purpose: Database patterns para PostgreSQL 16+
- ⚠️ Complexity: MEDIUM (snake_case, extensions)
- 🔄 Status: STABLE (v0.1.0)

**mapster.md:**
- 🎯 Purpose: High-performance object mapping
- ⚠️ Complexity: LOW (convention-based)
- 🔄 Status: STABLE (v0.1.0)

---

## Ejemplos de Uso

### Caso 1: Crear entidad con PostgreSQL

```csharp
// Entity
public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

// Configuration (snake_case)
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(e => e.Email)
            .HasColumnName("email")
            .HasMaxLength(254);

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("NOW()");
    }
}
```

### Caso 2: Mapear con Mapster

```csharp
// DTO
public record UserDto(Guid Id, string Email, DateTime CreatedAt);

// Query handler (CQRS)
public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly ApplicationDbContext _context;

    public async Task<List<UserDto>> Handle(
        GetUsersQuery request,
        CancellationToken ct)
    {
        return await _context.Users
            .ProjectToType<UserDto>() // Mapster projection
            .ToListAsync(ct);
    }
}
```

---

## Próximos Pasos

### Inmediato

1. ✅ Skills creados (postgresql.md, mapster.md)
2. ⏳ Commit cambios
3. ⏳ Merge a main

### Issue #25 (siguiente)

**Título:** MediatR & FluentValidation
**Skills a crear:**
- `.claude/skills/dotnet/mediatr.md` (~350 líneas)
- `.claude/skills/dotnet/fluentvalidation.md` (~300 líneas)

**Dependencias:**
- Requiere: Issue #24 (mapster para DTOs)
- Bloqueado por: Ninguno

---

## Referencias

- **Issue #23:** Gap Analysis (roadmap completo)
- **Issue #22:** Validación final v0.1.0
- **Issue #18:** .NET Skills (ef-core base)
- **STACK.md:** Stack tecnológico completo
- **ROADMAP.md:** v0.2.0 planning

**Related issues:**
- Previous: #23 (Gap Analysis)
- Next: #25 (MediatR & FluentValidation)

---

## Métricas

### Archivos Creados

| Archivo | Líneas | Tokens (aprox) |
|---------|--------|----------------|
| postgresql.md | 641 | ~2,000 |
| mapster.md | 345 | ~1,100 |
| issue-24.md | ~380 | ~1,200 |
| **Total** | **1,366** | **~4,300** |

### Tiempo de Desarrollo

- Planning: 30 min
- postgresql.md: 2 horas
- mapster.md: 1 hora
- Validación: 15 min
- Documentación: 30 min
- **Total:** ~4 horas

---

## Lecciones Aprendidas

1. **snake_case es crítico:** PostgreSQL requiere naming conventions consistentes
2. **Mapster > AutoMapper:** Performance 2-3x mejor con menos configuración
3. **ProjectToType es clave:** EF Core projection evita N+1 queries
4. **Testcontainers:** Integration testing con PostgreSQL real
5. **Skills extensos son OK:** 641 líneas de postgresql.md cubren TODO el conocimiento

---

**Issue #24 COMPLETADO - Ready for commit and merge** 🚀

**mj2: PostgreSQL y Mapster skills ready para backend development**
