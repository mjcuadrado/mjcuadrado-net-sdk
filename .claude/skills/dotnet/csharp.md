---
name: csharp
description: C# 13 conventions and patterns for .NET 9
version: 0.1.0
tags: [dotnet, csharp, conventions]
---

# C# 13 Conventions

Convenciones completas para C# 13 en .NET 9.

## Naming Conventions

### Classes, Interfaces, Methods
```csharp
// ✅ BIEN - PascalCase
public class AuthService { }
public class UserRepository { }
public interface IUserRepository { }
public interface ILogger<T> { }
public void ProcessData() { }
public async Task<User> GetUserAsync() { }
```

```csharp
// ❌ MAL
public class authService { }  // ❌ camelCase
public class auth_service { }  // ❌ snake_case
public interface UserRepository { }  // ❌ Interfaz sin I
```

### Variables, Parameters, Fields
```csharp
// ✅ BIEN - camelCase con _ para private fields
private readonly ILogger _logger;
private readonly IUserRepository _userRepository;
public void Login(string userName, string password) { }
var isValid = true;
var userCount = 10;
```

```csharp
// ❌ MAL
private readonly ILogger Logger;  // ❌ Sin _
private readonly ILogger m_logger;  // ❌ Hungarian notation
public void Login(string UserName) { }  // ❌ PascalCase en parámetros
```

### Constants and Static Readonly
```csharp
// ✅ BIEN - PascalCase
public const int MaxRetryCount = 3;
public const string DefaultConnectionString = "Server=localhost";
public static readonly TimeSpan TokenExpiration = TimeSpan.FromHours(24);
```

```csharp
// ❌ MAL
public const int MAX_RETRY_COUNT = 3;  // ❌ SCREAMING_SNAKE_CASE (C/C++ style)
public const int maxretrycount = 3;  // ❌ lowercase
```

---

## Modern C# Features

### Primary Constructors (C# 12+)
```csharp
// ✅ BIEN - Primary constructor
public class AuthService(
    IUserRepository repository,
    ILogger<AuthService> logger,
    IOptions<AuthSettings> options)
{
    private readonly AuthSettings _settings = options.Value;

    public async Task<User> GetUserAsync(int id)
    {
        logger.LogInformation("Getting user {Id}", id);
        return await repository.GetByIdAsync(id);
    }
}
```

```csharp
// ❌ MAL - Constructor tradicional innecesario
public class AuthService
{
    private readonly IUserRepository _repository;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUserRepository repository,
        ILogger<AuthService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
}
```

### Required Members (C# 11+)
```csharp
// ✅ BIEN - Required properties
public class LoginRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}

// Usage
var request = new LoginRequest
{
    Email = "user@test.com",
    Password = "secret"
};
```

```csharp
// ❌ MAL - Constructor con muchos parámetros
public class LoginRequest
{
    public string Email { get; }
    public string Password { get; }

    public LoginRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
```

### Records (C# 9+)
```csharp
// ✅ BIEN - Record para DTOs inmutables
public record LoginResult(
    string Token,
    DateTime ExpiresAt,
    string RefreshToken);

// ✅ BIEN - Record con init
public record User
{
    public required int Id { get; init; }
    public required string Email { get; init; }
    public string? Name { get; init; }
}

// ✅ BIEN - Record con with expression
var updatedUser = user with { Name = "New Name" };
```

```csharp
// ❌ MAL - Class mutable para DTO
public class LoginResult
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string RefreshToken { get; set; }
}
```

### Nullable Reference Types
```csharp
#nullable enable

// ✅ BIEN
public class UserService
{
    // string = nunca null
    public string GetName() => "John";

    // string? = puede ser null
    public string? GetMiddleName() => null;

    // Validar nulls
    public void Process(string? input)
    {
        ArgumentNullException.ThrowIfNull(input);  // .NET 6+

        // Aquí input no es null
        Console.WriteLine(input.ToUpper());
    }

    // Null-coalescing
    public string GetDisplayName(string? name) => name ?? "Unknown";

    // Null-conditional
    public int? GetLength(string? text) => text?.Length;
}
```

```csharp
// ❌ MAL
#nullable disable  // ❌ Desactivar nullability

public class UserService
{
    public string GetName() => null;  // ❌ Puede devolver null sin marcarlo
}
```

### Pattern Matching
```csharp
// ✅ BIEN - Switch expressions
public string GetUserType(User user) => user switch
{
    { IsAdmin: true } => "Administrator",
    { Role: "Manager" } => "Manager",
    { CreatedAt: var date } when date > DateTime.Now.AddDays(-30) => "New User",
    _ => "Regular User"
};

// ✅ BIEN - Type patterns
public decimal GetDiscount(object customer) => customer switch
{
    PremiumCustomer p => p.DiscountRate,
    RegularCustomer => 0.05m,
    GuestCustomer => 0m,
    _ => throw new ArgumentException("Unknown customer type")
};

// ✅ BIEN - Property patterns
public bool IsValidOrder(Order order) => order is
{
    Total: > 0,
    Items.Count: > 0,
    Customer: not null
};
```

```csharp
// ❌ MAL - if-else largo
public string GetUserType(User user)
{
    if (user.IsAdmin)
        return "Administrator";
    else if (user.Role == "Manager")
        return "Manager";
    else if (user.CreatedAt > DateTime.Now.AddDays(-30))
        return "New User";
    else
        return "Regular User";
}
```

---

## Async/Await Patterns

### Async Methods
```csharp
// ✅ BIEN - Task<T> para async methods
public async Task<User> GetUserAsync(int id)
{
    var user = await _repository.GetByIdAsync(id);
    return user;
}

// ✅ BIEN - ValueTask para hot paths
public async ValueTask<int> GetCountAsync()
{
    // Si está en caché, no necesita Task allocation
    if (_cache.TryGetValue("count", out int count))
        return count;

    count = await _repository.CountAsync();
    _cache.Set("count", count);
    return count;
}

// ✅ BIEN - Task sin T para void
public async Task SaveChangesAsync()
{
    await _repository.SaveAsync();
    await _logger.FlushAsync();
}
```

```csharp
// ❌ MAL - async void (solo para event handlers)
public async void ProcessData()  // ❌ NO
{
    await DoWorkAsync();
    // Excepciones no se pueden capturar
}

// ❌ MAL - Mixing sync and async
public User GetUser(int id)
{
    return GetUserAsync(id).Result;  // ❌ Puede causar deadlock
}
```

### ConfigureAwait
```csharp
// ✅ BIEN - En bibliotecas (no UI)
public async Task<string> ReadFileAsync(string path)
{
    using var reader = new StreamReader(path);
    return await reader.ReadToEndAsync()
        .ConfigureAwait(false);  // No capturar contexto
}

// ✅ BIEN - En APIs/Controllers - no necesario (ASP.NET Core)
public async Task<IActionResult> GetUser(int id)
{
    var user = await _service.GetUserAsync(id);
    return Ok(user);
}
```

### Parallel Operations
```csharp
// ✅ BIEN - Task.WhenAll para operaciones paralelas
public async Task<Summary> GetDashboardAsync(int userId)
{
    var userTask = _userService.GetUserAsync(userId);
    var ordersTask = _orderService.GetOrdersAsync(userId);
    var statsTask = _statsService.GetStatsAsync(userId);

    await Task.WhenAll(userTask, ordersTask, statsTask);

    return new Summary
    {
        User = userTask.Result,
        Orders = ordersTask.Result,
        Stats = statsTask.Result
    };
}
```

---

## LINQ Patterns

### Query Syntax vs Method Syntax
```csharp
// ✅ BIEN - Method syntax (preferido)
var adults = users
    .Where(u => u.Age >= 18)
    .OrderBy(u => u.Name)
    .Select(u => new { u.Name, u.Age });

// ✅ También OK - Query syntax (para joins complejos)
var result = from user in users
             join order in orders on user.Id equals order.UserId
             where user.IsActive
             select new { user.Name, order.Total };
```

### Projection
```csharp
// ✅ BIEN - Select nuevo objeto
var userDtos = users.Select(u => new UserDto
{
    Id = u.Id,
    Name = u.Name,
    Email = u.Email
});

// ✅ BIEN - SelectMany para listas anidadas
var allOrders = users
    .SelectMany(u => u.Orders)
    .Where(o => o.Total > 100);
```

### Filtering
```csharp
// ✅ BIEN
var activeUsers = users.Where(u => u.IsActive);
var firstUser = users.FirstOrDefault(u => u.Id == 1);
var hasAdmin = users.Any(u => u.IsAdmin);
var allActive = users.All(u => u.IsActive);
```

### Aggregation
```csharp
// ✅ BIEN
var totalOrders = orders.Sum(o => o.Total);
var avgPrice = products.Average(p => p.Price);
var maxPrice = products.Max(p => p.Price);
var count = users.Count(u => u.IsActive);
```

---

## Dependency Injection

### Constructor Injection
```csharp
// ✅ BIEN - Primary constructor
public class AuthService(
    IUserRepository userRepository,
    ILogger<AuthService> logger,
    IOptions<AuthSettings> settings)
{
    private readonly AuthSettings _settings = settings.Value;

    public async Task<LoginResult> LoginAsync(string email, string password)
    {
        logger.LogInformation("Login attempt: {Email}", email);
        var user = await userRepository.GetByEmailAsync(email);

        if (user is null || !VerifyPassword(password, user.PasswordHash))
        {
            logger.LogWarning("Failed login: {Email}", email);
            throw new UnauthorizedException("Invalid credentials");
        }

        return CreateToken(user);
    }
}
```

### Service Registration
```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// ✅ Scoped - Una instancia por request HTTP
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// ✅ Singleton - Una instancia para toda la app
builder.Services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddSingleton<ICache, MemoryCache>();

// ✅ Transient - Nueva instancia cada vez
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();
```

---

## Error Handling

### Custom Exceptions
```csharp
// ✅ BIEN - Primary constructor para exceptions
public class NotFoundException(string message)
    : Exception(message);

public class ValidationException(string message, IDictionary<string, string[]> errors)
    : Exception(message)
{
    public IDictionary<string, string[]> Errors { get; } = errors;
}

public class UnauthorizedException(string message)
    : Exception(message);
```

### Try-Catch Patterns
```csharp
// ✅ BIEN - Result pattern
public async Task<Result<User>> GetUserSafeAsync(int id)
{
    try
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
            return Result<User>.Failure("User not found");

        return Result<User>.Success(user);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error getting user: {Id}", id);
        return Result<User>.Failure("Internal error");
    }
}

// ✅ BIEN - Specific exceptions first
try
{
    await ProcessAsync();
}
catch (ValidationException ex)
{
    _logger.LogWarning(ex, "Validation failed");
    throw;
}
catch (NotFoundException ex)
{
    _logger.LogWarning(ex, "Resource not found");
    throw;
}
catch (Exception ex)
{
    _logger.LogError(ex, "Unexpected error");
    throw;
}
```

---

## Code Organization

### File Structure
```
src/
├── Domain/
│   ├── Entities/
│   │   ├── User.cs
│   │   └── Order.cs
│   ├── Interfaces/
│   │   ├── IUserRepository.cs
│   │   └── IOrderRepository.cs
│   └── Exceptions/
│       ├── NotFoundException.cs
│       └── ValidationException.cs
├── Application/
│   ├── Services/
│   │   ├── AuthService.cs
│   │   └── UserService.cs
│   ├── DTOs/
│   │   ├── LoginRequest.cs
│   │   └── LoginResponse.cs
│   └── Validators/
│       └── LoginRequestValidator.cs
└── Infrastructure/
    ├── Repositories/
    │   ├── UserRepository.cs
    │   └── OrderRepository.cs
    └── Configuration/
        └── AuthConfiguration.cs
```

### Class Size
```csharp
// ✅ BIEN - Clase enfocada (<300 líneas)
public class UserService(IUserRepository repository)
{
    public async Task<User> GetUserAsync(int id) { }
    public async Task<IEnumerable<User>> GetAllUsersAsync() { }
    public async Task CreateUserAsync(User user) { }
    public async Task UpdateUserAsync(User user) { }
    public async Task DeleteUserAsync(int id) { }
}

// ❌ MAL - God class (>500 líneas)
public class MegaService
{
    // 50+ métodos
    // Mezcla User, Order, Payment, Shipping...
}
```

---

## Documentation

### XML Comments
```csharp
/// <summary>
/// Authenticates user and generates JWT token
/// </summary>
/// <param name="email">User email address</param>
/// <param name="password">User password</param>
/// <returns>Login result with token and expiration</returns>
/// <exception cref="UnauthorizedException">Thrown when credentials are invalid</exception>
public async Task<LoginResult> LoginAsync(string email, string password)
{
    // Implementation
}
```

---

## Performance Tips

### String Concatenation
```csharp
// ❌ MAL - En loops
string result = "";
foreach (var item in items)
{
    result += item.Name;  // Crea nueva string cada vez
}

// ✅ BIEN - StringBuilder
var sb = new StringBuilder();
foreach (var item in items)
{
    sb.Append(item.Name);
}
string result = sb.ToString();

// ✅ MEJOR - LINQ
string result = string.Join(", ", items.Select(i => i.Name));
```

### Collection Initialization
```csharp
// ✅ BIEN - Collection expressions (C# 12)
int[] numbers = [1, 2, 3, 4, 5];
List<string> names = ["Alice", "Bob", "Charlie"];

// ✅ BIEN - Capacity conocida
var list = new List<User>(capacity: 100);
var dict = new Dictionary<int, User>(capacity: 100);

// ✅ BIEN - Span<T> para stack allocation
Span<int> numbers = stackalloc int[] { 1, 2, 3, 4, 5 };
```

### LINQ Performance
```csharp
// ✅ BIEN - ToList() solo cuando necesario
var query = users.Where(u => u.IsActive);  // Deferred execution
var count = query.Count();  // No materializa lista

// ❌ MAL - ToList() prematuro
var list = users.ToList();  // Materializa TODA la lista
var activeCount = list.Where(u => u.IsActive).Count();
```

---

## Resumen

**Naming:**
- PascalCase: Classes, Methods, Properties
- camelCase: Variables, Parameters
- `_camelCase`: Private fields

**Modern C#:**
- Primary constructors
- Required members
- Records para DTOs
- Nullable reference types
- Pattern matching

**Async:**
- Task<T> para async methods
- ValueTask para hot paths
- ConfigureAwait(false) en bibliotecas
- NO async void (excepto event handlers)

**LINQ:**
- Method syntax preferido
- Deferred execution cuando posible
- ToList() solo cuando necesario

**DI:**
- Constructor injection
- Scoped para services HTTP
- Singleton para stateless
- Transient para stateful

**Performance:**
- StringBuilder para concatenación en loops
- Collection expressions
- Span<T> para stack allocation
- Capacity hint cuando conocido

---

## Referencias

- [C# Programming Guide](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.NET 9](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)
- [C# 13 What's New](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13)
- [Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
