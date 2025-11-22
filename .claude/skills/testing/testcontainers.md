---
name: testcontainers
description: Testcontainers for integration testing with real PostgreSQL databases
version: 0.1.0
tags: [testing, testcontainers, integration-tests, postgresql, docker]
---

# Testcontainers for .NET

Testcontainers permite ejecutar integration tests con bases de datos reales (PostgreSQL, MySQL, etc.) en contenedores Docker, eliminando la necesidad de mocks o bases de datos en memoria.

## 🎯 Overview

**Por qué Testcontainers en mj2:**
- **Real database:** Testa contra PostgreSQL real, no in-memory
- **Isolation:** Cada test suite tiene su propia instancia
- **Disposable:** Contenedores se destruyen automáticamente
- **CI/CD friendly:** Funciona en pipelines sin configuración extra
- **No state pollution:** Cada test run es limpio

**Beneficios vs In-Memory DB:**
```
In-Memory (SQLite):          Testcontainers (PostgreSQL):
- ❌ Diferente sintaxis SQL   ✅ PostgreSQL real
- ❌ Features no soportadas   ✅ Todas las features (extensions, triggers)
- ❌ Comportamiento diferente ✅ Comportamiento idéntico a producción
- ✅ Rápido (~50ms startup)   ⚠️ Más lento (~2-5s startup)
```

---

## 📦 Packages Requeridos

```xml
<PackageReference Include="Testcontainers.PostgreSql" Version="3.6.0" />
<PackageReference Include="xunit" Version="2.6.2" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="9.0.0" />
```

---

## 🚀 Quick Start

### Basic PostgreSQL Container

```csharp
using Testcontainers.PostgreSql;
using Xunit;

public class DatabaseTests : IAsyncLifetime
{
    private PostgreSqlContainer _container = null!;

    public async Task InitializeAsync()
    {
        // Crear y arrancar contenedor PostgreSQL
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .WithDatabase("testdb")
            .WithUsername("test")
            .WithPassword("test")
            .Build();

        await _container.StartAsync();
    }

    [Fact]
    public async Task CanConnectToDatabase()
    {
        // Arrange
        var connectionString = _container.GetConnectionString();

        // Act
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        // Assert
        connection.State.Should().Be(ConnectionState.Open);
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}
```

---

## 🎯 Integration Testing Patterns

### Pattern 1: Database Fixture (Shared Container)

**Use when:** Multiple tests share same database schema

```csharp
public class DatabaseFixture : IAsyncLifetime
{
    public PostgreSqlContainer Container { get; private set; } = null!;
    public ApplicationDbContext DbContext { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        // Start container
        Container = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .WithDatabase("testdb")
            .WithUsername("test")
            .WithPassword("test")
            .Build();

        await Container.StartAsync();

        // Create DbContext
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(Container.GetConnectionString())
            .Options;

        DbContext = new ApplicationDbContext(options);

        // Apply migrations
        await DbContext.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        if (DbContext != null)
            await DbContext.DisposeAsync();

        if (Container != null)
            await Container.DisposeAsync();
    }
}

// Test class using fixture
public class UserRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public UserRepositoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddUser_ValidUser_SavesToDatabase()
    {
        // Arrange
        var user = new User { Email = "test@test.com", FirstName = "John" };

        // Act
        _fixture.DbContext.Users.Add(user);
        await _fixture.DbContext.SaveChangesAsync();

        // Assert
        var saved = await _fixture.DbContext.Users
            .FirstOrDefaultAsync(u => u.Email == "test@test.com");

        saved.Should().NotBeNull();
        saved!.FirstName.Should().Be("John");
    }
}
```

### Pattern 2: Per-Test Container

**Use when:** Tests need isolated databases

```csharp
public class IsolatedDatabaseTests : IAsyncLifetime
{
    private PostgreSqlContainer _container = null!;
    private ApplicationDbContext _context = null!;

    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .Build();

        await _container.StartAsync();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        _context = new ApplicationDbContext(options);
        await _context.Database.MigrateAsync();
    }

    [Fact]
    public async Task Test_WithFreshDatabase()
    {
        // Each test gets a fresh database
        var user = new User { Email = "test@test.com" };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var count = await _context.Users.CountAsync();
        count.Should().Be(1);
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _container.DisposeAsync();
    }
}
```

### Pattern 3: Collection Fixture (Shared Across Test Classes)

**Use when:** Multiple test classes share same container

```csharp
[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    // This class has no code, just the ICollectionFixture<> interface
}

[Collection("Database collection")]
public class UserTests
{
    private readonly DatabaseFixture _fixture;

    public UserTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CreateUser_Success()
    {
        // Use _fixture.DbContext
    }
}

[Collection("Database collection")]
public class ProductTests
{
    private readonly DatabaseFixture _fixture;

    public ProductTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CreateProduct_Success()
    {
        // Same container as UserTests!
    }
}
```

---

## 🎨 Advanced Configurations

### Custom PostgreSQL Configuration

```csharp
_container = new PostgreSqlBuilder()
    .WithImage("postgres:16-alpine")
    .WithDatabase("testdb")
    .WithUsername("test")
    .WithPassword("test")
    .WithPortBinding(5432, true) // Random host port
    .WithCommand("-c", "max_connections=200") // PostgreSQL config
    .WithEnvironment("POSTGRES_INITDB_ARGS", "--encoding=UTF-8")
    .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
    .Build();
```

### Database Seeding

```csharp
public class SeededDatabaseFixture : IAsyncLifetime
{
    public ApplicationDbContext DbContext { get; private set; } = null!;
    private PostgreSqlContainer _container = null!;

    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .Build();

        await _container.StartAsync();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        DbContext = new ApplicationDbContext(options);
        await DbContext.Database.MigrateAsync();

        // Seed data
        await SeedAsync();
    }

    private async Task SeedAsync()
    {
        var users = new[]
        {
            new User { Email = "user1@test.com", FirstName = "User1" },
            new User { Email = "user2@test.com", FirstName = "User2" },
            new User { Email = "user3@test.com", FirstName = "User3" }
        };

        DbContext.Users.AddRange(users);
        await DbContext.SaveChangesAsync();

        // Detach entities para evitar tracking issues
        foreach (var user in users)
        {
            DbContext.Entry(user).State = EntityState.Detached;
        }
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
        await _container.DisposeAsync();
    }
}
```

### SQL Script Initialization

```csharp
_container = new PostgreSqlBuilder()
    .WithImage("postgres:16-alpine")
    .WithResourceMapping(
        new FileInfo("init.sql"),
        "/docker-entrypoint-initdb.d/init.sql") // Auto-executed on startup
    .Build();
```

**init.sql:**
```sql
-- Create extensions
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
CREATE EXTENSION IF NOT EXISTS "pg_trgm";

-- Seed data
INSERT INTO users (id, email, first_name) VALUES
    (uuid_generate_v4(), 'test@test.com', 'Test');
```

---

## 🔧 Testing Repositories

### Repository Integration Test

```csharp
public class UserRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;
    private readonly UserRepository _repository;

    public UserRepositoryIntegrationTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _repository = new UserRepository(_fixture.DbContext);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingUser_ReturnsUser()
    {
        // Arrange
        var user = new User { Email = "test@test.com", FirstName = "John" };
        _fixture.DbContext.Users.Add(user);
        await _fixture.DbContext.SaveChangesAsync();

        // Detach para simular fresh context
        _fixture.DbContext.Entry(user).State = EntityState.Detached;

        // Act
        var result = await _repository.GetByIdAsync(user.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be("test@test.com");
    }

    [Fact]
    public async Task AddAsync_NewUser_PersistsToDatabase()
    {
        // Arrange
        var user = new User { Email = "new@test.com", FirstName = "Jane" };

        // Act
        await _repository.AddAsync(user);

        // Assert - Query directly from DB
        var saved = await _fixture.DbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == user.Id);

        saved.Should().NotBeNull();
    }
}
```

---

## 🧪 Testing Migrations

### Migration Test

```csharp
public class MigrationTests : IAsyncLifetime
{
    private PostgreSqlContainer _container = null!;

    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .Build();

        await _container.StartAsync();
    }

    [Fact]
    public async Task Migrations_ApplySuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        await using var context = new ApplicationDbContext(options);

        // Act
        await context.Database.MigrateAsync();

        // Assert - Verify tables exist
        var tables = await context.Database
            .SqlQueryRaw<string>("SELECT tablename FROM pg_tables WHERE schemaname = 'public'")
            .ToListAsync();

        tables.Should().Contain("users");
        tables.Should().Contain("products");
    }

    [Fact]
    public async Task Migration_CreatesIndexes()
    {
        // Arrange & Act
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        await using var context = new ApplicationDbContext(options);
        await context.Database.MigrateAsync();

        // Assert - Verify indexes
        var indexes = await context.Database
            .SqlQueryRaw<string>(@"
                SELECT indexname
                FROM pg_indexes
                WHERE schemaname = 'public' AND tablename = 'users'")
            .ToListAsync();

        indexes.Should().Contain("idx_users_email");
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}
```

---

## ⚡ Performance Optimization

### Reuse Container Across Tests

```csharp
// Run container once per test run (not per test class)
public class GlobalDatabaseFixture : IAsyncLifetime
{
    private static PostgreSqlContainer? _sharedContainer;
    private static readonly SemaphoreSlim _semaphore = new(1, 1);

    public string ConnectionString { get; private set; } = string.Empty;

    public async Task InitializeAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            if (_sharedContainer == null)
            {
                _sharedContainer = new PostgreSqlBuilder()
                    .WithImage("postgres:16-alpine")
                    .Build();

                await _sharedContainer.StartAsync();
            }

            ConnectionString = _sharedContainer.GetConnectionString();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public Task DisposeAsync()
    {
        // Don't dispose shared container
        return Task.CompletedTask;
    }

    // Dispose shared container at end (in test assembly fixture)
    public static async Task DisposeSharedAsync()
    {
        if (_sharedContainer != null)
            await _sharedContainer.DisposeAsync();
    }
}
```

### Database Reset Between Tests

```csharp
public class FastDatabaseFixture : IAsyncLifetime
{
    public ApplicationDbContext DbContext { get; private set; } = null!;
    private PostgreSqlContainer _container = null!;

    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder().Build();
        await _container.StartAsync();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        DbContext = new ApplicationDbContext(options);
        await DbContext.Database.MigrateAsync();
    }

    // Reset database between tests (faster than recreating container)
    public async Task ResetDatabaseAsync()
    {
        // Truncate all tables
        await DbContext.Database.ExecuteSqlRawAsync(@"
            DO $$
            DECLARE tablename text;
            BEGIN
                FOR tablename IN
                    SELECT table_name FROM information_schema.tables
                    WHERE table_schema = 'public' AND table_type = 'BASE TABLE'
                LOOP
                    EXECUTE 'TRUNCATE TABLE ' || tablename || ' CASCADE';
                END LOOP;
            END $$;
        ");
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
        await _container.DisposeAsync();
    }
}
```

---

## ✅ Best Practices

### DO ✅

1. **Use IAsyncLifetime** para setup/cleanup async
2. **Shared fixtures** para tests que pueden compartir DB
3. **Detach entities** después de seed para evitar tracking issues
4. **PostgreSQL 16-alpine** imagen (más ligera y rápida)
5. **Migrations en setup** para schema real
6. **Truncate entre tests** si reutilizas container
7. **CI/CD ready** - Docker debe estar disponible en CI

### DON'T ❌

1. ❌ **NO** usar in-memory para tests críticos de PostgreSQL
2. ❌ **NO** olvidar DisposeAsync (resource leak)
3. ❌ **NO** hacer assertions en InitializeAsync
4. ❌ **NO** compartir DbContext entre tests paralelos
5. ❌ **NO** olvidar `AsNoTracking()` en assertions
6. ❌ **NO** hardcodear connection strings
7. ❌ **NO** usar latest tag (usar version específica)

---

## 🔍 Debugging

### View Container Logs

```csharp
[Fact]
public async Task DebugContainerLogs()
{
    var container = new PostgreSqlBuilder().Build();
    await container.StartAsync();

    // Get logs
    var (stdout, stderr) = await container.GetLogsAsync();

    _output.WriteLine("STDOUT:");
    _output.WriteLine(stdout);
    _output.WriteLine("STDERR:");
    _output.WriteLine(stderr);

    await container.DisposeAsync();
}
```

### Keep Container Running

```csharp
// Don't dispose for manual inspection
_container = new PostgreSqlBuilder()
    .WithCleanUp(false) // Keep container after test
    .Build();

await _container.StartAsync();

var connectionString = _container.GetConnectionString();
Console.WriteLine($"Connection: {connectionString}");

// Connect with pgAdmin or psql for inspection
```

---

## 📚 Referencias

- **Testcontainers .NET:** https://dotnet.testcontainers.org/
- **PostgreSQL Image:** https://hub.docker.com/_/postgres
- **xUnit Fixtures:** https://xunit.net/docs/shared-context

---

**Used by:** tdd-implementer, database-expert, backend-expert
**Related skills:** dotnet/postgresql.md, dotnet/xunit.md, dotnet/ef-core.md
