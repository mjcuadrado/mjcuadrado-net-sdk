---
name: mapster
description: Mapster high-performance object mapping patterns for .NET 9
version: 0.1.0
tags: [dotnet, mapster, mapping, dto, performance]
---

# Mapster - High-Performance Object Mapping

Mapster es un object mapper ultra-rápido para .NET, alternativa a AutoMapper con mejor performance y menos configuración.

## 🎯 Overview

**Por qué Mapster en mj2:**
- **Performance:** 2-3x más rápido que AutoMapper
- **Simplicidad:** Menos configuración, más convenciones
- **Type-Safe:** Compile-time checking
- **Flexible:** Configuración fluida cuando se necesita

---

## 📦 Packages Requeridos

```xml
<PackageReference Include="Mapster" Version="7.4.0" />
<PackageReference Include="Mapster.DependencyInjection" Version="1.0.1" />
```

---

## 🚀 Quick Start

### Mapping Básico (Convention-Based)

```csharp
// Entities
public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

// DTOs
public record UserDto(Guid Id, string Email, string FullName);

// Mapping (automático por convenciones)
var user = new User
{
    Id = Guid.NewGuid(),
    Email = "john@test.com",
    FirstName = "John",
    LastName = "Doe"
};

var dto = user.Adapt<UserDto>(); // ✅ Mapea automáticamente Id, Email
```

### Configuración Global

**Program.cs:**
```csharp
using Mapster;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

// Registrar Mapster
builder.Services.AddMapster();

var app = builder.Build();
```

---

## ⚙️ Configuración Avanzada

### TypeAdapterConfig (Startup)

```csharp
using Mapster;

public static class MapsterConfiguration
{
    public static void Configure()
    {
        TypeAdapterConfig.GlobalSettings.Default
            .PreserveReference(true) // Manejar referencias circulares
            .NameMatchingStrategy(NameMatchingStrategy.Flexible); // Matching flexible
    }
}

// En Program.cs
MapsterConfiguration.Configure();
builder.Services.AddMapster();
```

### Custom Mappings

```csharp
public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // User → UserDto
        config.NewConfig<User, UserDto>()
            .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Email, src => src.Email);

        // CreateUserRequest → User
        config.NewConfig<CreateUserRequest, User>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
            .Ignore(dest => dest.UpdatedAt); // Ignorar campos
    }
}
```

### Registro de Configuraciones

**Program.cs:**
```csharp
// Escanear todas las configuraciones del assembly
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(Program).Assembly);

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();
```

---

## 🎯 Patterns Comunes

### 1. Entity → DTO

```csharp
public record ProductDto(
    Guid Id,
    string Name,
    decimal Price,
    string CategoryName
);

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Category Category { get; set; } = null!;
}

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

// Configuración
config.NewConfig<Product, ProductDto>()
    .Map(dest => dest.CategoryName, src => src.Category.Name);

// Uso
var product = await _context.Products
    .Include(p => p.Category)
    .FirstAsync();

var dto = product.Adapt<ProductDto>();
```

### 2. Request → Entity (Create)

```csharp
public record CreateProductRequest(
    string Name,
    decimal Price,
    Guid CategoryId
);

config.NewConfig<CreateProductRequest, Product>()
    .Map(dest => dest.Id, src => Guid.NewGuid())
    .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
    .Ignore(dest => dest.Category); // Se asigna después

// Uso
var product = request.Adapt<Product>();
product.CategoryId = request.CategoryId;
await _context.Products.AddAsync(product);
```

### 3. Request → Entity (Update)

```csharp
public record UpdateProductRequest(string Name, decimal Price);

config.NewConfig<UpdateProductRequest, Product>()
    .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
    .Ignore(dest => dest.Id)
    .Ignore(dest => dest.CreatedAt);

// Uso
var product = await _context.Products.FindAsync(id);
if (product == null) return NotFound();

request.Adapt(product); // ✅ Mapea sobre entidad existente
await _context.SaveChangesAsync();
```

### 4. Collections

```csharp
// List → List
List<User> users = await _context.Users.ToListAsync();
List<UserDto> dtos = users.Adapt<List<UserDto>>();

// IQueryable → List (con ProjectToType)
var dtos = await _context.Users
    .ProjectToType<UserDto>()
    .ToListAsync();
```

---

## 🔥 Integration con CQRS

### Query Handler

```csharp
public record GetUsersQuery : IRequest<List<UserDto>>;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly ApplicationDbContext _context;

    public GetUsersHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserDto>> Handle(
        GetUsersQuery request,
        CancellationToken ct)
    {
        return await _context.Users
            .ProjectToType<UserDto>() // ✅ Mapster projection
            .ToListAsync(ct);
    }
}
```

### Command Handler

```csharp
public record CreateUserCommand(
    string Email,
    string FirstName,
    string LastName
) : IRequest<Result<UserDto>>;

public class CreateUserHandler
    : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateUserHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(
        CreateUserCommand request,
        CancellationToken ct)
    {
        // Request → Entity
        var user = request.Adapt<User>();

        _context.Users.Add(user);
        await _context.SaveChangesAsync(ct);

        // Entity → DTO
        var dto = user.Adapt<UserDto>();

        return Result<UserDto>.Success(dto);
    }
}
```

---

## ⚡ Performance Optimization

### 1. ProjectToType (EF Core Integration)

```csharp
// ✅ BIEN - Projection en database
var dtos = await _context.Users
    .ProjectToType<UserDto>()
    .ToListAsync();

// SQL generado:
// SELECT u.id, u.email, u.first_name + ' ' + u.last_name AS full_name
// FROM users u

// ❌ MAL - Carga completa y luego mapea
var users = await _context.Users.ToListAsync();
var dtos = users.Adapt<List<UserDto>>();
```

### 2. Compiled Adapters

```csharp
// Compilar adapter para reuso
private static readonly Func<User, UserDto> UserToDto =
    TypeAdapter<User, UserDto>.GetAdapter();

// Uso
var dto = UserToDto(user); // Más rápido que .Adapt<UserDto>()
```

### 3. Shallow Copy vs Deep Copy

```csharp
// Shallow copy (default)
config.NewConfig<Source, Destination>()
    .ShallowCopyForSameType(true);

// Deep copy
var deepCopy = source.Adapt<Source>(); // Copia recursiva
```

---

## 🧪 Testing con Mapster

### Unit Tests

```csharp
public class MappingTests
{
    private readonly IMapper _mapper;

    public MappingTests()
    {
        var config = new TypeAdapterConfig();
        config.Scan(typeof(UserMappingConfig).Assembly);
        _mapper = new Mapper(config);
    }

    [Fact]
    public void Should_Map_User_To_UserDto()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@test.com",
            FirstName = "John",
            LastName = "Doe"
        };

        // Act
        var dto = _mapper.Map<UserDto>(user);

        // Assert
        dto.Id.Should().Be(user.Id);
        dto.Email.Should().Be(user.Email);
        dto.FullName.Should().Be("John Doe");
    }

    [Fact]
    public void Should_Map_CreateRequest_To_User()
    {
        // Arrange
        var request = new CreateUserRequest("test@test.com", "John", "Doe");

        // Act
        var user = _mapper.Map<User>(request);

        // Assert
        user.Id.Should().NotBeEmpty();
        user.Email.Should().Be(request.Email);
        user.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}
```

---

## 🎨 Advanced Features

### 1. AfterMapping (Post-Processing)

```csharp
config.NewConfig<User, UserDto>()
    .AfterMapping((src, dest) =>
    {
        // Post-procesamiento
        if (string.IsNullOrEmpty(dest.FullName))
            dest = dest with { FullName = "Unknown" };
    });
```

### 2. Conditional Mapping

```csharp
config.NewConfig<User, UserDto>()
    .Map(dest => dest.Email,
         src => src.Email,
         srcCond => !string.IsNullOrEmpty(srcCond.Email));
```

### 3. Two-Way Mapping

```csharp
// User ↔ UserDto (bidireccional)
config.NewConfig<User, UserDto>()
    .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
    .TwoWays(); // ✅ Configura también UserDto → User
```

### 4. Nested Mappings

```csharp
public record OrderDto(
    Guid Id,
    UserDto Customer,
    List<OrderItemDto> Items
);

// Mapea automáticamente User → UserDto, OrderItem → OrderItemDto
var orderDto = order.Adapt<OrderDto>();
```

---

## ✅ Best Practices

### DO ✅

1. **Usar ProjectToType** para queries EF Core
2. **Configurar mappings complejos** en IRegister
3. **Ignorar navegación properties** cuando no se cargan
4. **Usar records para DTOs** (immutability)
5. **Compilar adapters** para operaciones frecuentes
6. **Validar mappings** con unit tests
7. **Escanear assembly** para auto-registro

### DON'T ❌

1. ❌ **NO** mapear entidades completas en GET endpoints
2. ❌ **NO** olvidar .Ignore() para propiedades no mapeables
3. ❌ **NO** cargar entidades completas si solo necesitas DTO
4. ❌ **NO** usar .Adapt() en loops sin compiled adapter
5. ❌ **NO** mapear passwords o datos sensibles sin filtros

---

## 📚 Comparación: Mapster vs AutoMapper

| Feature | Mapster | AutoMapper |
|---------|---------|------------|
| Performance | ⚡ 2-3x más rápido | Más lento |
| Configuración | Mínima | Verbose |
| Type Safety | ✅ Compile-time | ⚠️ Runtime |
| Projection (EF) | ✅ ProjectToType | ✅ ProjectTo |
| Curva aprendizaje | 📉 Baja | 📈 Media |
| Comunidad | 🟡 Mediana | 🟢 Grande |

**Recomendación mj2:** Mapster por performance y simplicidad.

---

## 🔐 Security Considerations

### 1. Sensitive Data

```csharp
public class User
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; // ⚠️ SENSITIVE
}

// ✅ BIEN - Ignorar campos sensibles
config.NewConfig<User, UserDto>()
    .Ignore(dest => dest.PasswordHash);
```

### 2. Input Validation

```csharp
// ❌ MAL - Mapear directamente sin validación
var user = request.Adapt<User>();

// ✅ BIEN - Validar primero
var validationResult = await _validator.ValidateAsync(request);
if (!validationResult.IsValid)
    return Result.Failure<User>(validationResult.Errors);

var user = request.Adapt<User>();
```

---

## 📋 Common Scenarios

### API Controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetUsers()
    {
        var users = await _context.Users
            .ProjectToType<UserDto>()
            .ToListAsync();

        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserRequest request)
    {
        var user = request.Adapt<User>();

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetUser),
            new { id = user.Id },
            user.Adapt<UserDto>());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(
        Guid id,
        UpdateUserRequest request)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        request.Adapt(user); // Map sobre entidad existente
        await _context.SaveChangesAsync();

        return Ok(user.Adapt<UserDto>());
    }
}
```

---

## 🎓 Referencias

- **Mapster GitHub:** https://github.com/MapsterMapper/Mapster
- **Documentation:** https://github.com/MapsterMapper/Mapster/wiki
- **Performance Benchmarks:** https://github.com/MapsterMapper/Mapster#performance

---

**Used by:** tdd-implementer, database-expert, api-designer
**Related skills:** dotnet/ef-core.md, architecture/cqrs.md, dotnet/fluentvalidation.md
