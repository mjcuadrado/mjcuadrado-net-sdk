---
name: ef-core
description: Entity Framework Core patterns and best practices
version: 0.1.0
tags: [dotnet, ef-core, database, orm]
---

# Entity Framework Core Patterns

Patrones completos para EF Core 9 en .NET 9.

## DbContext Configuration

### Basic DbContext
```csharp
// ✅ BIEN
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
```

### Registration in Program.cs
```csharp
// ✅ BIEN - SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()));

// ✅ BIEN - PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ BIEN - SQLite (development)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// ✅ BIEN - In-Memory (testing)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TestDb"));
```

---

## Entity Configuration

### Entity Type Configuration
```csharp
// ✅ BIEN - Separate configuration
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table name
        builder.ToTable("Users");

        // Primary key
        builder.HasKey(u => u.Id);

        // Properties
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(254);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(256);

        // Indexes
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.CreatedAt);

        // Default values
        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(u => u.IsActive)
            .HasDefaultValue(true);
    }
}
```

```csharp
// ❌ MAL - Configuration in DbContext
public class ApplicationDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ❌ Todo mezclado aquí
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
        modelBuilder.Entity<Order>().HasKey(o => o.Id);
        // ...cientos de líneas
    }
}
```

### Relationships

#### One-to-Many
```csharp
// ✅ BIEN
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        // One User -> Many Orders
        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

#### Many-to-Many
```csharp
// ✅ BIEN - Explicit join entity (preferido)
public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
    }
}

// ✅ También OK - Implicit join table (EF Core 5+)
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles"));
    }
}
```

#### One-to-One
```csharp
// ✅ BIEN
public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.User)
            .WithOne(u => u.Profile)
            .HasForeignKey<UserProfile>(p => p.UserId);
    }
}
```

---

## Migrations

### Create Migration
```bash
# Create new migration
dotnet ef migrations add InitialCreate

# With specific context
dotnet ef migrations add InitialCreate --context ApplicationDbContext

# With specific project
dotnet ef migrations add InitialCreate --project src/Infrastructure
```

### Apply Migrations
```bash
# Update database to latest
dotnet ef database update

# Update to specific migration
dotnet ef database update CreateUsers

# Rollback all migrations
dotnet ef database update 0
```

### Remove Migration
```bash
# Remove last migration (not applied)
dotnet ef migrations remove

# Remove with force (already applied)
dotnet ef migrations remove --force
```

### Migration Best Practices
```csharp
// ✅ BIEN - Descriptive migration names
dotnet ef migrations add AddUserEmailIndex
dotnet ef migrations add AddOrderStatusColumn
dotnet ef migrations add UpdateUserTableConstraints

// ❌ MAL - Vague names
dotnet ef migrations add Update1
dotnet ef migrations add Fix
dotnet ef migrations add Changes
```

### Data Seeding in Migrations
```csharp
// ✅ BIEN
public partial class SeedInitialRoles : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Roles",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Admin" },
                { 2, "User" },
                { 3, "Guest" }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Roles",
            keyColumn: "Id",
            keyValues: new object[] { 1, 2, 3 });
    }
}
```

---

## Repository Pattern

### Generic Repository Interface
```csharp
// ✅ BIEN
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
```

### Generic Repository Implementation
```csharp
// ✅ BIEN
public class Repository<T>(ApplicationDbContext context) : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }
}
```

### Specific Repository
```csharp
// ✅ BIEN - Domain-specific repository
public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetActiveUsersAsync();
    Task<bool> EmailExistsAsync(string email);
}

public class UserRepository(ApplicationDbContext context)
    : Repository<User>(context), IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        return await _context.Users
            .Where(u => u.IsActive)
            .OrderBy(u => u.Name)
            .ToListAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == email);
    }
}
```

---

## Unit of Work Pattern

### Interface
```csharp
// ✅ BIEN
public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IOrderRepository Orders { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
```

### Implementation
```csharp
// ✅ BIEN
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    public IUserRepository Users { get; }
    public IOrderRepository Orders { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Orders = new OrderRepository(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
```

### Usage
```csharp
// ✅ BIEN
public class OrderService(IUnitOfWork unitOfWork)
{
    public async Task CreateOrderAsync(Order order)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            // Add order
            await unitOfWork.Orders.AddAsync(order);
            await unitOfWork.SaveChangesAsync();

            // Update user stats
            var user = await unitOfWork.Users.GetByIdAsync(order.UserId);
            user.TotalOrders++;
            unitOfWork.Users.Update(user);
            await unitOfWork.SaveChangesAsync();

            await unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
```

---

## Querying Patterns

### Eager Loading
```csharp
// ✅ BIEN - Include related entities
var users = await _context.Users
    .Include(u => u.Profile)
    .Include(u => u.Orders)
        .ThenInclude(o => o.Items)
    .ToListAsync();
```

### Projection
```csharp
// ✅ BIEN - Select only needed fields
var userDtos = await _context.Users
    .Select(u => new UserDto
    {
        Id = u.Id,
        Name = u.Name,
        Email = u.Email,
        OrderCount = u.Orders.Count
    })
    .ToListAsync();
```

### Filtering and Sorting
```csharp
// ✅ BIEN
var activeUsers = await _context.Users
    .Where(u => u.IsActive)
    .Where(u => u.CreatedAt > DateTime.UtcNow.AddYears(-1))
    .OrderByDescending(u => u.CreatedAt)
    .ThenBy(u => u.Name)
    .ToListAsync();
```

### Pagination
```csharp
// ✅ BIEN
public async Task<PagedResult<User>> GetUsersPagedAsync(int page, int pageSize)
{
    var query = _context.Users.AsQueryable();

    var totalCount = await query.CountAsync();

    var users = await query
        .OrderBy(u => u.Name)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    return new PagedResult<User>
    {
        Items = users,
        TotalCount = totalCount,
        Page = page,
        PageSize = pageSize
    };
}
```

### AsNoTracking
```csharp
// ✅ BIEN - For read-only queries
var users = await _context.Users
    .AsNoTracking()
    .Where(u => u.IsActive)
    .ToListAsync();

// ✅ BIEN - Global query filter
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
        .HasQueryFilter(u => !u.IsDeleted);
}
```

---

## Performance Optimization

### Batch Operations
```csharp
// ✅ BIEN - Batch insert
await _context.Users.AddRangeAsync(users);
await _context.SaveChangesAsync();

// ✅ BIEN - Batch update (EF Core 7+)
await _context.Users
    .Where(u => u.IsActive == false)
    .ExecuteUpdateAsync(u => u.SetProperty(x => x.Status, "Inactive"));

// ✅ BIEN - Batch delete (EF Core 7+)
await _context.Users
    .Where(u => u.CreatedAt < DateTime.UtcNow.AddYears(-5))
    .ExecuteDeleteAsync();
```

### Compiled Queries
```csharp
// ✅ BIEN - Precompiled query for frequent operations
private static readonly Func<ApplicationDbContext, string, Task<User?>> _getUserByEmail =
    EF.CompileAsyncQuery((ApplicationDbContext context, string email) =>
        context.Users.FirstOrDefault(u => u.Email == email));

public async Task<User?> GetByEmailAsync(string email)
{
    return await _getUserByEmail(_context, email);
}
```

### Split Queries
```csharp
// ✅ BIEN - Split query for multiple collections
var users = await _context.Users
    .Include(u => u.Orders)
    .Include(u => u.Posts)
    .AsSplitQuery()  // Prevents cartesian explosion
    .ToListAsync();
```

---

## Testing with EF Core

### In-Memory Database
```csharp
// ✅ BIEN
public class UserRepositoryTests
{
    private ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingUser_ReturnsUser()
    {
        // Arrange
        await using var context = CreateContext();
        var user = new User { Id = 1, Name = "John", Email = "john@test.com" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var repository = new UserRepository(context);

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("John");
    }
}
```

---

## Best Practices

### DO ✅
- Use IEntityTypeConfiguration for entity setup
- Apply migrations in order
- Use AsNoTracking for read-only queries
- Implement Repository pattern for testability
- Use Unit of Work for transactions
- Project to DTOs when possible
- Enable retry on failure for production
- Use batch operations for bulk changes

### DON'T ❌
- Configure entities in OnModelCreating directly
- Use .Result or .Wait() on async methods
- Load unnecessary related entities
- Query without proper indexes
- Expose DbContext directly to controllers
- Use eager loading for all queries
- Ignore N+1 query problems
- Skip migrations in production

---

## Referencias

- [EF Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [EF Core 9 What's New](https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/whatsnew)
- [Repository Pattern](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
- [Unit of Work Pattern](https://www.martinfowler.com/eaaCatalog/unitOfWork.html)
