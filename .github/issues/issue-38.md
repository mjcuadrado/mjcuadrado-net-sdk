# Issue #38: Database Expert Agent (PostgreSQL + SQL Server)

**Status:** ✅ Completed
**Priority:** 🟡 High
**Version:** v0.3.0
**Created:** 2025-11-22
**Completed:** 2025-11-22

---

## 📋 Descripción

Se ha completado el agente **Database Expert** con soporte completo para **PostgreSQL** y **SQL Server**, incluyendo skills, agente especializado y comando de migraciones.

---

## 🎯 Objetivos

Implementar expertise completo en bases de datos:

1. ✅ **sqlserver.md Skill** - SQL Server con EF Core 9
2. ✅ **database-expert.md Agent** - Experto en PostgreSQL + SQL Server
3. ✅ **mj2-db-migrate.md Command** - Gestión de migraciones

*Nota: postgresql.md skill ya existía en `.claude/skills/dotnet/`*

---

## 📦 Archivos Creados

### 1. sqlserver.md (442 líneas)

**Ubicación:** `.claude/skills/dotnet/sqlserver.md`

**Contenido:**
- SQL Server 2022+ con EF Core 9
- Connection strings (Windows Auth, SQL Auth, Azure SQL)
- DbContext configuration con PascalCase conventions
- Entity configuration con Fluent API
- Tipos de datos (.NET ↔ SQL Server mapping)
- LINQ to SQL y T-SQL queries
- Stored procedures integration
- Índices (Clustered, Non-Clustered, Covering, Filtered, Columnstore)
- Migraciones con EF Core
- Transactions y isolation levels
- Performance optimization
- Docker con SQL Server
- Best practices

**Conceptos clave:**

```csharp
// Configuración SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 5);
            sqlOptions.CommandTimeout(60);
        }));
```

**Connection Strings:**
```csharp
// SQL Server Authentication
"Server=localhost;Database=MyDatabase;User Id=sa;Password=YourPassword123;TrustServerCertificate=True"

// Windows Authentication
"Server=localhost;Database=MyDatabase;Integrated Security=True;TrustServerCertificate=True"

// Azure SQL Database
"Server=tcp:myserver.database.windows.net,1433;Database=MyDatabase;User Id=myuser@myserver;Password=YourPassword123;Encrypt=True"
```

**Índices Avanzados:**
```csharp
// Covering index con INCLUDE
modelBuilder.Entity<Order>()
    .HasIndex(o => o.CustomerId)
    .IncludeProperties(o => new { o.Total, o.Status });

// Filtered index
modelBuilder.Entity<Order>()
    .HasIndex(o => o.Status)
    .HasFilter("[Status] = 'Pending'");
```

### 2. database-expert.md (665 líneas)

**Ubicación:** `.claude/agents/mj2/database-expert.md`

**Contenido:**
- Persona y filosofía del agente
- TRUST 5 principles para databases
- Workflow de 4 fases (ANALYZE → DESIGN → MIGRATE → OPTIMIZE)
- Decisión PostgreSQL vs SQL Server
- Database design patterns (Aggregate, Soft Delete, Audit Trail)
- Migration strategies (Expand-Contract, Blue-Green, Rolling)
- Rollback strategies
- Performance optimization (PostgreSQL y SQL Server)
- Security best practices
- Integration con otros agentes
- Comandos disponibles

**Workflow Completo:**

```
📊 ANALYZE
  ↓ Entender requisitos de datos
  ↓ Identificar entidades y relaciones
  ↓ Elegir RDBMS (PostgreSQL vs SQL Server)
  ↓ Diseñar schema inicial

🏗️ DESIGN
  ↓ Definir entidades y properties
  ↓ Establecer relaciones (1:1, 1:N, N:M)
  ↓ Aplicar normalización (3NF)
  ↓ Diseñar índices
  ↓ Definir constraints

🔄 MIGRATE
  ↓ Crear migration con dotnet ef
  ↓ Revisar SQL generado
  ↓ Testear en dev/staging
  ↓ Aplicar en producción

⚡ OPTIMIZE
  ↓ Analizar slow queries
  ↓ Identificar missing indexes
  ↓ Optimizar queries existentes
  ↓ Configurar connection pooling
```

**Decisión PostgreSQL vs SQL Server:**

| Criterio | PostgreSQL | SQL Server |
|----------|------------|------------|
| **Licencia** | Open-source (gratis) | Comercial (Express gratis) |
| **Platform** | Linux, macOS, Windows | Principalmente Windows |
| **JSON** | JSONB nativo, excelente | JSON support básico |
| **Indexing** | GiST, GIN, BRIN | Clustered, Columnstore |
| **Ecosystem** | Multiplataforma | Microsoft stack |
| **Enterprise Features** | Replication, Partitioning | Always On, In-Memory OLTP |

**Migration Strategies:**

**Expand-Contract (Zero-Downtime):**
```
1. EXPAND: Agregar nueva columna
2. DUAL-WRITE: Escribir en ambas columnas
3. READ-SWITCH: Leer de nueva columna
4. CONTRACT: Eliminar columna vieja
```

**Soft Delete Pattern:**
```csharp
public class Order
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}

// Global query filter
modelBuilder.Entity<Order>()
    .HasQueryFilter(o => !o.IsDeleted);
```

**Audit Trail Pattern:**
```csharp
public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
}

public class Order : AuditableEntity { }
```

### 3. mj2-db-migrate.md (180 líneas)

**Ubicación:** `.claude/commands/mj2-db-migrate.md`

**Contenido:**
- Sintaxis completa del comando
- Parámetros (add, update, rollback, script, remove)
- Workflow detallado para cada operación
- Ejemplos exhaustivos
- Precauciones (backups, staging first)
- Integration con deployment
- Best practices

**Uso:**
```bash
# Crear migration
/mj2:db-migrate add MigrationName

# Aplicar migrations
/mj2:db-migrate update

# Rollback
/mj2:db-migrate rollback PreviousMigration

# Generar script SQL
/mj2:db-migrate script --idempotent

# Remover última migration
/mj2:db-migrate remove
```

**Workflow de Deploy a Producción:**
```bash
# 1. Generar script (NO ejecutar migrations directamente)
/mj2:db-migrate script --idempotent > migration.sql

# 2. Revisar script
cat migration.sql

# 3. Backup
pg_dump mydb > backup_pre_migration.sql

# 4. Aplicar manualmente
psql -h prod -U user -d mydb < migration.sql

# 5. Verificar
psql -h prod -U user -d mydb -c "SELECT * FROM \"__EFMigrationsHistory\""
```

### 4. issue-38.md

**Ubicación:** `.github/issues/issue-38.md`

**Contenido:** Este archivo - documentación completa del Issue #38

---

## 💡 Ejemplos de Uso

### Ejemplo 1: Diseñar Schema con PostgreSQL

```csharp
// 1. Definir entities
public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public int CustomerId { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }

    public Customer Customer { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}

// 2. Configurar en DbContext (PostgreSQL - snake_case)
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");  // snake_case

        builder.Property(o => o.OrderNumber)
            .HasColumnName("order_number");  // snake_case

        builder.Property(o => o.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");  // PostgreSQL

        builder.HasIndex(o => o.OrderNumber).IsUnique();
    }
}

// 3. Crear migration
/mj2:db-migrate add InitialCreate

// 4. Aplicar
/mj2:db-migrate update
```

### Ejemplo 2: Diseñar Schema con SQL Server

```csharp
// 1. Mismas entities pero configuración diferente
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");  // PascalCase

        builder.Property(o => o.Id)
            .UseIdentityColumn(1, 1);  // IDENTITY(1,1)

        builder.Property(o => o.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");  // SQL Server

        builder.Property(o => o.Total)
            .HasColumnType("decimal(18,2)");
    }
}
```

### Ejemplo 3: Performance Optimization

**PostgreSQL:**
```sql
-- Análisis de slow queries
SELECT query, mean_exec_time, calls
FROM pg_stat_statements
ORDER BY mean_exec_time DESC
LIMIT 10;

-- Missing indexes
SELECT schemaname, tablename, attname
FROM pg_stats
WHERE n_distinct > 100;
```

**SQL Server:**
```sql
-- Slow queries
SELECT TOP 10
    qs.execution_count,
    qs.total_elapsed_time / qs.execution_count AS avg_time,
    SUBSTRING(qt.text, qs.statement_start_offset/2, ...) AS query_text
FROM sys.dm_exec_query_stats qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) qt
ORDER BY avg_time DESC;

-- Missing indexes
SELECT TOP 10
    migs.avg_total_user_cost * migs.avg_user_impact AS improvement,
    mid.statement,
    mid.equality_columns
FROM sys.dm_db_missing_index_details mid
INNER JOIN sys.dm_db_missing_index_group_stats migs
ON mid.index_handle = migs.group_handle
ORDER BY improvement DESC;
```

---

## ✅ Criterios de Éxito

- [x] sqlserver.md skill creada (442 líneas)
- [x] database-expert.md agente creado (665 líneas)
- [x] mj2-db-migrate.md comando creado (180 líneas)
- [x] issue-38.md documentación creada
- [x] Soporte completo PostgreSQL y SQL Server
- [x] EF Core 9 integration documentada
- [x] Migration strategies (Expand-Contract, Blue-Green, Rolling)
- [x] Rollback automation documentado
- [x] Performance optimization (ambos RDBMS)
- [x] Security best practices
- [x] Database design patterns (Aggregate, Soft Delete, Audit)
- [x] Integration con otros agentes
- [x] Ejemplos completos funcionales
- [x] Todo el contenido en español
- [x] README.md actualizado
- [x] ROADMAP.md actualizado
- [x] Todos los archivos committed
- [x] Merged a main
- [x] Issue documentado y cerrado

---

## 🔄 Integración con Otros Agentes

### Workflow Full-Stack con Database

```bash
# 1. Diseñar database schema (database-expert)
# Definir Order.cs, Customer.cs, Product.cs

# 2. Crear migration (database-expert)
/mj2:db-migrate add InitialCreate

# 3. Aplicar migration en dev
/mj2:db-migrate update

# 4. Implementar repository (tdd-implementer)
/mj2:2-run REPO-ORDERS-001

# 5. Tests de integración con Testcontainers
# PostgreSQL o SQL Server en Docker

# 6. Frontend para CRUD (frontend-builder)
/mj2:2f-build COMP-ORDERS-LIST-001

# 7. E2E tests (e2e-tester)
/mj2:4-e2e E2E-ORDERS-001

# 8. Quality check (quality-gate)
/mj2:quality-check

# 9. Deploy con migrations (devops-expert)
/mj2:5-deploy staging --run-migrations
/mj2:db-migrate script --idempotent > migration.sql
# Apply migration.sql manualmente en producción

# 10. Observability (OpenTelemetry)
# Métricas de queries automáticas en Grafana
```

---

## 📈 Resumen de Métricas

| Métrica | Valor |
|---------|-------|
| **Archivos Creados** | 4 (1 skill + 1 agent + 1 command + 1 doc) |
| **Total Líneas** | ~1,287 |
| **RDBMS Soportados** | 2 (PostgreSQL + SQL Server) |
| **Skills** | 2 (postgresql.md existente + sqlserver.md nuevo) |
| **Migration Strategies** | 3 (Expand-Contract, Blue-Green, Rolling) |
| **Database Patterns** | 3 (Aggregate, Soft Delete, Audit Trail) |
| **Índices Documentados** | 6+ tipos |
| **Idioma** | 100% Español ✅ |

---

## 🚀 Próximos Pasos

Con Database Expert completado (Issue #38), se completa la **v0.3.0: Full Stack + DevOps**.

### Issues Completados en v0.3.0:
- ✅ Issue #33: Frontend Testing Stack
- ✅ Issue #34: Docker Foundation
- ✅ Issue #35: DevOps Expert Agent
- ✅ Issue #36: GitHub Actions CI/CD
- ✅ Issue #37: OpenTelemetry Stack
- ✅ Issue #38: Database Expert Agent ← **Este issue**

### v0.3.0 Completa ✅

**Próxima versión: v0.4.0 - Advanced Features**
- Security Expert
- API Designer Agent
- Project Templates
- Performance Engineering

---

## 📚 Recursos Adicionales

### PostgreSQL
- Skill: `.claude/skills/dotnet/postgresql.md`
- Docs: https://www.postgresql.org/docs/
- Npgsql: https://www.npgsql.org/
- EF Core Provider: https://www.npgsql.org/efcore/

### SQL Server
- Skill: `.claude/skills/dotnet/sqlserver.md`
- Docs: https://learn.microsoft.com/en-us/sql/
- EF Core Provider: https://learn.microsoft.com/en-us/ef/core/providers/sql-server/
- T-SQL Reference: https://learn.microsoft.com/en-us/sql/t-sql/

### EF Core
- Skill: `.claude/skills/dotnet/ef-core.md`
- Docs: https://learn.microsoft.com/en-us/ef/core/
- Migrations: https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/

---

**Completado por:** Claude Code
**Commit:** feature/issue-38-database-expert → main
**Archivos:** 4 (sqlserver.md, database-expert.md, mj2-db-migrate.md, issue-38.md)
**Líneas Añadidas:** ~1,287
**Idioma:** 100% Español ✅
**Database Expert:** ✅ **COMPLETO**
**v0.3.0:** ✅ **COMPLETA**
