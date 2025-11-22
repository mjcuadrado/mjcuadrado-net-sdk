---
name: sqlserver
description: SQL Server 2022+ patterns with EF Core 9 and PascalCase conventions
version: 0.1.0
tags: [dotnet, sqlserver, ef-core, database, mssql]
---

# SQL Server with Entity Framework Core

SQL Server 2022+ integration patterns for .NET 9 applications using PascalCase database conventions.

## 🎯 Overview

SQL Server es un RDBMS enterprise de Microsoft, ideal para aplicaciones .NET. Características clave:
- **Versión:** SQL Server 2022+
- **Provider:** Microsoft.EntityFrameworkCore.SqlServer
- **Convenciones:** PascalCase para tablas y columnas
- **Migrations:** EF Core Code-First
- **Editions:** Express (gratis), Standard, Enterprise

---

## 📦 Packages Requeridos

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.0" />
```

---

## 🔧 Configuración Básica

### Connection String

**appsettings.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MyDatabase;User Id=sa;Password=YourPassword123;TrustServerCertificate=True"
  }
}
```

**Formatos de Connection String:**

```csharp
// Windows Authentication
"Server=localhost;Database=MyDatabase;Integrated Security=True;TrustServerCertificate=True"

// SQL Server Authentication
"Server=localhost;Database=MyDatabase;User Id=sa;Password=YourPassword123;TrustServerCertificate=True"

// Azure SQL Database
"Server=tcp:myserver.database.windows.net,1433;Database=MyDatabase;User Id=myuser@myserver;Password=YourPassword123;Encrypt=True"

// Con connection pooling
"Server=localhost;Database=MyDatabase;User Id=sa;Password=YourPassword123;Min Pool Size=5;Max Pool Size=100;Pooling=True"

// LocalDB (desarrollo)
"Server=(localdb)\\mssqllocaldb;Database=MyDatabase;Integrated Security=True"
```

### Configurar en Program.cs

```csharp
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar SQL Server con EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.MigrationsAssembly("MyApp.Infrastructure");
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
            sqlOptions.CommandTimeout(60);  // 60 segundos
        }));

var app = builder.Build();
app.Run();
```

---

## 🏗️ DbContext Configuration

### DbContext Básico

```csharp
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar configuraciones
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // SQL Server specific configuration
        ConfigureSqlServerSpecifics(modelBuilder);
    }

    private void ConfigureSqlServerSpecifics(ModelBuilder modelBuilder)
    {
        // Configuración global para decimales
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }

        // Configuración global para strings
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(string)))
        {
            property.SetMaxLength(256);  // Default max length
        }
    }
}
```

### Entity Configuration (Fluent API)

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Tabla
        builder.ToTable("Orders");

        // Primary Key
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .UseIdentityColumn(seed: 1, increment: 1);  // IDENTITY(1,1)

        // Properties
        builder.Property(o => o.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.Total)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(o => o.Status)
            .HasMaxLength(50)
            .HasDefaultValue("Pending");

        builder.Property(o => o.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(o => o.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate();

        // Índices
        builder.HasIndex(o => o.OrderNumber).IsUnique();
        builder.HasIndex(o => o.CustomerId);
        builder.HasIndex(o => new { o.CustomerId, o.CreatedAt });

        // Relaciones
        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

---

## 🔢 Tipos de Datos

### Mapeo .NET ↔ SQL Server

| .NET Type | SQL Server Type | Ejemplo |
|-----------|----------------|---------|
| `int` | `int` | 42 |
| `long` | `bigint` | 9223372036854775807 |
| `decimal` | `decimal(18,2)` | 123.45 |
| `float`/`double` | `float` | 3.14159 |
| `string` | `nvarchar(n)` / `nvarchar(max)` | "Hello" |
| `bool` | `bit` | 1/0 |
| `DateTime` | `datetime2` | 2025-11-22 10:30:00 |
| `DateTimeOffset` | `datetimeoffset` | 2025-11-22 10:30:00+00:00 |
| `Guid` | `uniqueidentifier` | NEWID() |
| `byte[]` | `varbinary(max)` | Binary data |

### Configuración de Tipos

```csharp
builder.Property(o => o.Id)
    .HasColumnType("int")
    .UseIdentityColumn();

builder.Property(o => o.Price)
    .HasColumnType("decimal(18,2)");

builder.Property(o => o.Name)
    .HasColumnType("nvarchar(100)");

builder.Property(o => o.Description)
    .HasColumnType("nvarchar(max)");

builder.Property(o => o.CreatedAt)
    .HasColumnType("datetime2");

builder.Property(o => o.RowVersion)
    .IsRowVersion();  // → timestamp (concurrency)
```

---

## 🔍 Queries y Optimización

### LINQ to SQL

```csharp
// Select con filtro
var orders = await _context.Orders
    .Where(o => o.Total > 100)
    .OrderByDescending(o => o.CreatedAt)
    .ToListAsync();

// Include (Eager Loading)
var ordersWithDetails = await _context.Orders
    .Include(o => o.Customer)
    .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.Product)
    .Where(o => o.Status == "Pending")
    .ToListAsync();

// Projection (select solo campos necesarios)
var orderSummary = await _context.Orders
    .Select(o => new OrderSummaryDto
    {
        Id = o.Id,
        OrderNumber = o.OrderNumber,
        Total = o.Total,
        CustomerName = o.Customer.Name
    })
    .ToListAsync();

// AsNoTracking (read-only, mejor performance)
var orders = await _context.Orders
    .AsNoTracking()
    .Where(o => o.Status == "Completed")
    .ToListAsync();

// Agregaciones
var totalRevenue = await _context.Orders
    .Where(o => o.Status == "Completed")
    .SumAsync(o => o.Total);

var averageOrderValue = await _context.Orders
    .AverageAsync(o => o.Total);
```

### SQL Crudo (T-SQL)

```csharp
// FromSqlRaw
var orders = await _context.Orders
    .FromSqlRaw("SELECT * FROM Orders WHERE Total > {0}", 100)
    .ToListAsync();

// FromSqlInterpolated (recomendado - más seguro)
decimal minTotal = 100;
var orders = await _context.Orders
    .FromSqlInterpolated($"SELECT * FROM Orders WHERE Total > {minTotal}")
    .Include(o => o.Customer)  // Puede combinar con LINQ
    .ToListAsync();

// Stored Procedure
var orders = await _context.Orders
    .FromSqlRaw("EXEC GetOrdersByCustomer @CustomerId = {0}", customerId)
    .ToListAsync();

// ExecuteSqlRaw (UPDATE/DELETE/INSERT)
var affectedRows = await _context.Database
    .ExecuteSqlRawAsync(
        "UPDATE Orders SET Status = 'Cancelled' WHERE DATEDIFF(day, CreatedAt, GETDATE()) > 30");
```

### Stored Procedures

```csharp
// Definir stored procedure
/*
CREATE PROCEDURE GetTopCustomers
    @MinOrders INT
AS
BEGIN
    SELECT CustomerId, COUNT(*) AS OrderCount
    FROM Orders
    GROUP BY CustomerId
    HAVING COUNT(*) >= @MinOrders
    ORDER BY OrderCount DESC
END
*/

// Ejecutar desde EF Core
public class TopCustomerResult
{
    public int CustomerId { get; set; }
    public int OrderCount { get; set; }
}

var topCustomers = await _context.Database
    .SqlQueryRaw<TopCustomerResult>("EXEC GetTopCustomers @MinOrders = {0}", 10)
    .ToListAsync();
```

---

## 📊 Índices

### Crear Índices en EF Core

```csharp
modelBuilder.Entity<Order>(entity =>
{
    // Índice simple
    entity.HasIndex(o => o.CustomerId)
        .HasDatabaseName("IX_Orders_CustomerId");

    // Índice compuesto
    entity.HasIndex(o => new { o.CustomerId, o.CreatedAt })
        .HasDatabaseName("IX_Orders_Customer_Date");

    // Índice único
    entity.HasIndex(o => o.OrderNumber)
        .IsUnique()
        .HasDatabaseName("UQ_Orders_OrderNumber");

    // Índice con INCLUDE (covering index)
    entity.HasIndex(o => o.CustomerId)
        .IncludeProperties(o => new { o.Total, o.Status })
        .HasDatabaseName("IX_Orders_CustomerId_Include");

    // Índice filtrado
    entity.HasIndex(o => o.Status)
        .HasFilter("[Status] = 'Pending'")
        .HasDatabaseName("IX_Orders_Status_Pending");
});
```

### Tipos de Índices en SQL Server

```sql
-- Clustered Index (primary key por defecto)
CREATE CLUSTERED INDEX IX_Orders_Id ON Orders(Id);

-- Non-Clustered Index
CREATE NONCLUSTERED INDEX IX_Orders_CustomerId ON Orders(CustomerId);

-- Covering Index (con INCLUDE)
CREATE NONCLUSTERED INDEX IX_Orders_CustomerId_Include
ON Orders(CustomerId)
INCLUDE (Total, Status, CreatedAt);

-- Filtered Index
CREATE NONCLUSTERED INDEX IX_Orders_Pending
ON Orders(Status)
WHERE Status = 'Pending';

-- Columnstore Index (para analytics)
CREATE NONCLUSTERED COLUMNSTORE INDEX IX_Orders_Columnstore
ON Orders(CustomerId, Total, CreatedAt);
```

---

## 🔄 Migraciones

### Crear y Aplicar Migraciones

```bash
# Crear migración
dotnet ef migrations add InitialCreate

# Ver SQL que se ejecutará
dotnet ef migrations script

# Aplicar migración
dotnet ef database update

# Aplicar migración específica
dotnet ef database update MigrationName

# Rollback a migración anterior
dotnet ef database update PreviousMigrationName

# Remover última migración (si no se aplicó)
dotnet ef migrations remove
```

### Migración con Datos Seed

```csharp
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(maxLength: 100, nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
            });

        // Seed data
        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Name", "Price" },
            values: new object[] { "Product 1", 99.99m });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Products");
    }
}
```

---

## 🔒 Transactions

### Transacciones en EF Core

```csharp
using var transaction = await _context.Database.BeginTransactionAsync();

try
{
    // Operación 1
    var order = new Order { CustomerId = 123, Total = 100 };
    _context.Orders.Add(order);
    await _context.SaveChangesAsync();

    // Operación 2
    var customer = await _context.Customers.FindAsync(123);
    customer.LastOrderDate = DateTime.UtcNow;
    await _context.SaveChangesAsync();

    // Operación 3 con SQL crudo
    await _context.Database.ExecuteSqlRawAsync(
        "UPDATE Customers SET TotalOrders = TotalOrders + 1 WHERE Id = {0}", 123);

    // Commit
    await transaction.CommitAsync();
}
catch (Exception ex)
{
    await transaction.RollbackAsync();
    _logger.LogError(ex, "Transaction failed");
    throw;
}
```

### Isolation Levels

```csharp
using var transaction = await _context.Database.BeginTransactionAsync(
    System.Data.IsolationLevel.Serializable);

// Read Uncommitted - Lee dirty reads
// Read Committed - Default en SQL Server
// Repeatable Read - Previene non-repeatable reads
// Serializable - Máxima consistencia
// Snapshot - Versioning basado en tiempo
```

---

## 🛡️ Performance y Best Practices

### 1. Evitar N+1 Queries

```csharp
// ❌ N+1 Problem
var orders = await _context.Orders.ToListAsync();
foreach (var order in orders)
{
    var customer = await _context.Customers.FindAsync(order.CustomerId);  // N queries
}

// ✅ Eager Loading
var orders = await _context.Orders
    .Include(o => o.Customer)
    .ToListAsync();

// ✅ Select Loading (para relaciones específicas)
await _context.Orders
    .Where(o => o.Id == orderId)
    .Select(o => o.Customer)
    .LoadAsync();
```

### 2. AsNoTracking para Read-Only

```csharp
// ❌ Con tracking (más lento)
var orders = await _context.Orders.ToListAsync();

// ✅ Sin tracking (más rápido para read-only)
var orders = await _context.Orders
    .AsNoTracking()
    .ToListAsync();
```

### 3. Projection en lugar de Select *

```csharp
// ❌ Traer toda la entidad
var orders = await _context.Orders
    .Include(o => o.Customer)
    .ToListAsync();

// ✅ Solo campos necesarios
var orderDtos = await _context.Orders
    .Select(o => new OrderDto
    {
        Id = o.Id,
        Total = o.Total,
        CustomerName = o.Customer.Name
    })
    .ToListAsync();
```

### 4. Batch Operations

```csharp
// ❌ Múltiples SaveChanges
foreach (var order in orders)
{
    _context.Orders.Add(order);
    await _context.SaveChangesAsync();  // Una transacción por cada orden
}

// ✅ Un solo SaveChanges
_context.Orders.AddRange(orders);
await _context.SaveChangesAsync();  // Una transacción para todas
```

### 5. Connection Pooling

```csharp
// ✅ Connection string con pooling
"Server=localhost;Database=MyDatabase;User Id=sa;Password=Pass123;Min Pool Size=5;Max Pool Size=100;Pooling=True"

// Verificar connections activas
SELECT
    DB_NAME(dbid) as DatabaseName,
    COUNT(dbid) as ConnectionCount
FROM sys.sysprocesses
WHERE dbid > 0
GROUP BY dbid;
```

---

## 🔧 Docker con SQL Server

```yaml
# docker-compose.yml
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong!Passw0rd
      - MSSQL_PID=Developer  # Express, Developer, Standard, Enterprise
    ports:
      - "1433:1433"
    volumes:
      - sqlserver-data:/var/opt/mssql
    healthcheck:
      test: /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong!Passw0rd" -Q "SELECT 1"
      interval: 10s
      timeout: 5s
      retries: 5

volumes:
  sqlserver-data:
```

---

## 📚 Recursos Adicionales

- [SQL Server Documentation](https://learn.microsoft.com/en-us/sql/sql-server/)
- [EF Core SQL Server Provider](https://learn.microsoft.com/en-us/ef/core/providers/sql-server/)
- [T-SQL Reference](https://learn.microsoft.com/en-us/sql/t-sql/)
- [SQL Server Performance Tuning](https://learn.microsoft.com/en-us/sql/relational-databases/performance/)

---

**Versión:** 0.1.0
**Última Actualización:** 2025-11-22
**Mantenido por:** mjcuadrado-net-sdk
