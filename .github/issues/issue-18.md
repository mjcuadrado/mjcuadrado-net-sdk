# Issue #18: .NET Skills

**Status:** ✅ Closed
**Created:** 2024-11-21
**Closed:** 2024-11-21
**Agent:** Multiple (used by tdd-implementer, quality-gate)
**Commit:** 07a5f85

---

## Objetivo

Crear 4 Skills de .NET en `.claude/skills/dotnet/` con conocimiento técnico específico de .NET 9 y C# 13.

---

## Skills Creados

### 1. csharp.md (665 líneas)

**Contenido:**

#### Naming Conventions
- **Classes, Interfaces, Methods:** PascalCase
- **Variables, Parameters, Fields:** camelCase con `_` para private
- **Constants:** PascalCase (no SCREAMING_SNAKE_CASE)

#### Modern C# Features
- **Primary Constructors (C# 12):** Simplifica DI
- **Required Members (C# 11):** Para propiedades obligatorias
- **Records (C# 9):** Para DTOs inmutables
- **Nullable Reference Types:** Seguridad contra nulls
- **Pattern Matching:** Switch expressions, property patterns

#### Async/Await Patterns
- Task<T> para async methods
- ValueTask para hot paths
- ConfigureAwait(false) en bibliotecas
- NO async void (excepto event handlers)

#### LINQ Patterns
- Method syntax preferido
- Projection para DTOs
- Deferred execution
- ToList() solo cuando necesario

#### Dependency Injection
- Constructor injection
- Scoped para services HTTP
- Singleton para stateless
- Transient para stateful

#### Performance Tips
- StringBuilder para concatenación en loops
- Collection expressions (C# 12)
- Span<T> para stack allocation
- Capacity hint cuando conocido

**Usado por:** tdd-implementer, quality-gate

---

### 2. xunit.md (657 líneas)

**Contenido:**

#### Test Structure
- **AAA:** Arrange-Act-Assert pattern
- Naming: `MethodName_Scenario_ExpectedResult`

#### FluentAssertions
- **Strings:** Be, StartWith, EndWith, Contain, Match
- **Numbers:** Be, BeGreaterThan, BeLessThan, BeInRange
- **Collections:** HaveCount, Contain, BeEquivalentTo, BeInAscendingOrder
- **Exceptions:** Throw<T>, WithMessage, NotThrow
- **Async:** ThrowAsync<T>

#### Theory (Parametrized Tests)
- **InlineData:** Para casos simples
- **MemberData:** Para datos complejos
- **ClassData:** Para reutilización

#### Mocking (NSubstitute)
- Setup: `Returns()`, `ThrowsAsync()`
- Verify: `Received()`, `DidNotReceive()`
- Argument matching: `Arg.Any<T>()`, `Arg.Is<T>()`

#### Test Organization
- Constructor setup para SUT
- Nested classes para contextos
- IClassFixture para shared context

#### Integration Tests
- WebApplicationFactory
- In-memory database
- Custom factory para override services

#### Code Coverage
- Target: ≥85% overall
- Business logic: ≥95%
- Controllers: ≥70%

**Usado por:** tdd-implementer

---

### 3. ef-core.md (669 líneas)

**Contenido:**

#### DbContext Configuration
- Primary constructor
- DbSet<T> properties
- ApplyConfigurationsFromAssembly

#### Entity Configuration
- IEntityTypeConfiguration<T>
- Property constraints (IsRequired, MaxLength)
- Indexes (IsUnique)
- Default values

#### Relationships
- **One-to-Many:** HasOne + WithMany
- **Many-to-Many:** HasMany + WithMany + UsingEntity
- **One-to-One:** HasOne + WithOne

#### Migrations
- Create: `dotnet ef migrations add`
- Apply: `dotnet ef database update`
- Remove: `dotnet ef migrations remove`
- Data seeding in migrations

#### Repository Pattern
- Generic IRepository<T> interface
- Generic Repository<T> implementation
- Specific repositories (e.g., IUserRepository)

#### Unit of Work Pattern
- IUnitOfWork interface
- Coordina múltiples repositories
- Transaction management

#### Querying Patterns
- **Eager loading:** Include + ThenInclude
- **Projection:** Select para DTOs
- **Filtering:** Where + OrderBy
- **Pagination:** Skip + Take
- **AsNoTracking:** Para read-only queries

#### Performance Optimization
- Batch operations (ExecuteUpdate, ExecuteDelete - EF Core 7+)
- Compiled queries
- Split queries (AsSplitQuery)

**Usado por:** tdd-implementer

---

### 4. aspnet-core.md (712 líneas)

**Contenido:**

#### Minimal API Setup
- Program.cs structure
- Service registration
- Middleware pipeline

#### Endpoints
- **Minimal APIs:** MapGet, MapPost, MapPut, MapDelete
- **Controllers:** ApiController + Route attributes
- **Groups:** MapGroup para organización

#### Request/Response Models
- Records para DTOs
- CreateXRequest, UpdateXRequest patterns
- PagedResult<T> para paginación

#### Authentication & Authorization
- **JWT:** Token generation + validation
- **Configuration:** TokenValidationParameters
- **Policies:** Role-based, claim-based
- **Endpoints:** Login, refresh, logout

#### Middleware
- **Exception handling:** Catch all + return appropriate status
- **Request logging:** Log method, path, status, duration
- **Custom middleware:** InvokeAsync pattern

#### Validation
- **FluentValidation:** AbstractValidator<T>
- **Endpoint filters:** ValidationFilter<T>
- **Registration:** AddValidatorsFromAssembly

#### CORS Configuration
- AllowSpecificOrigin vs AllowAll
- WithOrigins, AllowAnyHeader, AllowAnyMethod

#### Health Checks
- AddHealthChecks
- DbContextCheck, SqlServer, UrlGroup
- /health, /health/ready, /health/live

#### API Versioning
- MapGroup("/api/v1"), MapGroup("/api/v2")
- Version-specific endpoints

#### Swagger/OpenAPI
- AddSwaggerGen
- JWT authentication in Swagger
- XML comments

#### HTTP Status Codes
- 200 OK, 201 Created, 204 NoContent
- 400 BadRequest, 401 Unauthorized, 404 NotFound
- 500 InternalServerError

**Usado por:** tdd-implementer

---

## Estadísticas

| Skill | Líneas | Secciones | Ejemplos | Scripts |
|-------|--------|-----------|----------|---------|
| csharp.md | 665 | 10 | 40+ | 0 |
| xunit.md | 657 | 12 | 35+ | 0 |
| ef-core.md | 669 | 11 | 30+ | 0 |
| aspnet-core.md | 712 | 13 | 35+ | 0 |
| **Total** | **2,703** | **46** | **140+** | **0** |

---

## Estructura de cada Skill

### YAML Frontmatter
```yaml
---
name: skill-name
description: Brief description
version: 0.1.0
tags: [dotnet, category, subcategory]
---
```

### Secciones principales
1. Introducción
2. Conceptos principales
3. Ejemplos ✅ BIEN vs ❌ MAL
4. Patterns y best practices
5. Performance tips (cuando aplica)
6. Testing (cuando aplica)
7. Best Practices (DO vs DON'T)
8. Referencias

---

## Uso por Agentes

### tdd-implementer
Usa los 4 Skills para:
- **csharp.md:** Escribir código C# moderno y limpio
- **xunit.md:** Crear tests con xUnit + FluentAssertions
- **ef-core.md:** Implementar repositories y DbContext
- **aspnet-core.md:** Crear APIs con endpoints, middleware, auth

### quality-gate
Usa **csharp.md** para:
- Validar naming conventions
- Verificar uso de modern C# features
- Validar async/await patterns

---

## Filosofía

```
Command → Agent → Skill
   ↓        ↓        ↓
 Simple  Orquesta  Knowledge
```

**Skills = Conocimiento técnico específico**
- Agents delegan conocimiento técnico a Skills
- Skills contienen patterns, ejemplos, best practices
- Un Skill puede ser usado por múltiples Agents

**Diferencia con Foundation Skills:**
- **Foundation:** Metodología (TRUST, TAGS, SPECS, EARS, Git)
- **.NET:** Tecnología (.NET 9, C# 13, xUnit, EF Core, ASP.NET Core)

---

## Validación Final

```bash
# Verificar que todos los Skills existen
ls -lh .claude/skills/dotnet/

# csharp.md       665 líneas
# xunit.md        657 líneas
# ef-core.md      669 líneas
# aspnet-core.md  712 líneas

# Total: 2,703 líneas de conocimiento .NET
```

---

## Impacto

**Antes:**
- Agents tenían conocimiento .NET inline o incompleto
- Inconsistencia en código generado
- Difícil actualizar a nuevas versiones de .NET

**Después:**
- Conocimiento .NET centralizado y completo
- Código generado consistente y moderno
- Fácil actualizar cuando salga .NET 10

---

## Ejemplos de uso

### tdd-implementer implementando AuthService
```
1. Lee csharp.md → Primary constructors, DI
2. Lee aspnet-core.md → JWT configuration
3. Lee xunit.md → Test structure, mocking
4. Implementa AuthService con patterns correctos
5. Crea tests con FluentAssertions
```

### tdd-implementer implementando UserRepository
```
1. Lee ef-core.md → Repository pattern
2. Lee csharp.md → Async patterns
3. Lee xunit.md → Integration tests
4. Implementa UserRepository con EF Core
5. Crea tests con in-memory database
```

---

## Próximos Pasos

1. ✅ Crear .NET Skills (este issue)
2. ⏳ Crear MJ² Skills (Issue #19)
3. ⏳ Actualizar agents para referenciar Skills
4. ⏳ Crear Skills de arquitectura (Clean Architecture, DDD)
5. ⏳ Crear Skills de testing avanzado (E2E, performance)

---

## Referencias

- Commit: 07a5f85
- Files:
  - `.claude/skills/dotnet/csharp.md`
  - `.claude/skills/dotnet/xunit.md`
  - `.claude/skills/dotnet/ef-core.md`
  - `.claude/skills/dotnet/aspnet-core.md`
- GitHub Issue: #18
- Related Issues: #17 (Foundation Skills)

---

**mj2: Automated .NET 9 development with modern patterns**
