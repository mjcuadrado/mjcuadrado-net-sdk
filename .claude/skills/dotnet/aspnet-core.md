---
name: aspnet-core
description: ASP.NET Core API patterns and best practices
version: 0.1.0
tags: [dotnet, aspnet-core, api, web]
---

# ASP.NET Core API Patterns

Patrones completos para ASP.NET Core 9 APIs.

## Minimal API Setup

### Program.cs Structure
```csharp
// ✅ BIEN - Modern minimal API
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add custom services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapGet("/", () => "Hello World!");
app.MapUserEndpoints();
app.MapAuthEndpoints();

app.Run();
```

---

## Minimal API Endpoints

### Basic CRUD Endpoints
```csharp
// ✅ BIEN
public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users")
            .WithTags("Users")
            .WithOpenApi();

        // GET /api/users
        group.MapGet("/", async (IUserService service) =>
        {
            var users = await service.GetAllUsersAsync();
            return Results.Ok(users);
        });

        // GET /api/users/{id}
        group.MapGet("/{id:int}", async (int id, IUserService service) =>
        {
            var user = await service.GetUserByIdAsync(id);
            return user is not null
                ? Results.Ok(user)
                : Results.NotFound();
        });

        // POST /api/users
        group.MapPost("/", async (CreateUserRequest request, IUserService service) =>
        {
            var user = await service.CreateUserAsync(request);
            return Results.Created($"/api/users/{user.Id}", user);
        })
        .WithName("CreateUser");

        // PUT /api/users/{id}
        group.MapPut("/{id:int}", async (int id, UpdateUserRequest request, IUserService service) =>
        {
            var updated = await service.UpdateUserAsync(id, request);
            return updated
                ? Results.NoContent()
                : Results.NotFound();
        });

        // DELETE /api/users/{id}
        group.MapDelete("/{id:int}", async (int id, IUserService service) =>
        {
            var deleted = await service.DeleteUserAsync(id);
            return deleted
                ? Results.NoContent()
                : Results.NotFound();
        });
    }
}
```

---

## Controllers (Alternative)

### API Controller Structure
```csharp
// ✅ BIEN
[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Get all users
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var users = await userService.GetAllUsersAsync();
        return Ok(users);
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user is null)
            return NotFound();

        return Ok(user);
    }

    /// <summary>
    /// Create new user
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var user = await userService.CreateUserAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    /// <summary>
    /// Update existing user
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
    {
        var updated = await userService.UpdateUserAsync(id, request);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Delete user
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await userService.DeleteUserAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
```

---

## Request/Response Models

### DTOs
```csharp
// ✅ BIEN - Request DTOs
public record CreateUserRequest
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public record UpdateUserRequest
{
    public string? Name { get; init; }
    public string? Email { get; init; }
}

// ✅ BIEN - Response DTOs
public record UserDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public DateTime CreatedAt { get; init; }
}

public record PagedResult<T>
{
    public required IEnumerable<T> Items { get; init; }
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
```

---

## Authentication & Authorization

### JWT Configuration
```csharp
// ✅ BIEN - appsettings.json
{
  "Jwt": {
    "Key": "your-secret-key-min-32-chars-long",
    "Issuer": "your-app",
    "Audience": "your-app-users",
    "ExpirationHours": 24
  }
}

// ✅ BIEN - Program.cs
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();
```

### JWT Token Generation
```csharp
// ✅ BIEN
public class JwtTokenGenerator(IConfiguration configuration) : ITokenGenerator
{
    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(
                double.Parse(configuration["Jwt:ExpirationHours"]!)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
```

### Auth Endpoints
```csharp
// ✅ BIEN
public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Authentication");

        group.MapPost("/login", async (
            LoginRequest request,
            IAuthService authService) =>
        {
            var result = await authService.LoginAsync(request.Email, request.Password);
            return Results.Ok(result);
        })
        .AllowAnonymous();

        group.MapPost("/refresh", async (
            RefreshTokenRequest request,
            IAuthService authService) =>
        {
            var result = await authService.RefreshTokenAsync(request.RefreshToken);
            return Results.Ok(result);
        })
        .AllowAnonymous();

        group.MapPost("/logout", async (
            HttpContext context,
            IAuthService authService) =>
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await authService.LogoutAsync(token);
            return Results.NoContent();
        })
        .RequireAuthorization();
    }
}
```

### Authorization Policies
```csharp
// ✅ BIEN - Define policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("UserOrAdmin", policy =>
        policy.RequireRole("User", "Admin"));

    options.AddPolicy("MinimumAge", policy =>
        policy.Requirements.Add(new MinimumAgeRequirement(18)));
});

// ✅ BIEN - Use in endpoints
group.MapDelete("/{id:int}", async (int id, IUserService service) =>
{
    await service.DeleteUserAsync(id);
    return Results.NoContent();
})
.RequireAuthorization("AdminOnly");

// ✅ BIEN - Use in controllers
[HttpDelete("{id}")]
[Authorize(Roles = "Admin")]
public async Task<IActionResult> Delete(int id) { }
```

---

## Middleware

### Custom Middleware
```csharp
// ✅ BIEN
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex, "Resource not found");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (ValidationException ex)
        {
            logger.LogWarning(ex, "Validation failed");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { error = ex.Message, errors = ex.Errors });
        }
        catch (UnauthorizedException ex)
        {
            logger.LogWarning(ex, "Unauthorized access");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { error = "Internal server error" });
        }
    }
}

// Extension method
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}

// Usage in Program.cs
app.UseExceptionHandling();
```

### Request Logging Middleware
```csharp
// ✅ BIEN
public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;

        logger.LogInformation(
            "Request: {Method} {Path}",
            context.Request.Method,
            context.Request.Path);

        await next(context);

        var duration = DateTime.UtcNow - startTime;

        logger.LogInformation(
            "Response: {StatusCode} in {Duration}ms",
            context.Response.StatusCode,
            duration.TotalMilliseconds);
    }
}
```

---

## Validation

### FluentValidation
```csharp
// ✅ BIEN - Install FluentValidation.DependencyInjectionExtensions

// Validator
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(254);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(@"[A-Z]").WithMessage("Must contain uppercase")
            .Matches(@"[a-z]").WithMessage("Must contain lowercase")
            .Matches(@"\d").WithMessage("Must contain digit");
    }
}

// Registration
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Validation filter
public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices
            .GetService<IValidator<T>>();

        if (validator is not null)
        {
            var request = context.Arguments.OfType<T>().FirstOrDefault();
            if (request is not null)
            {
                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
            }
        }

        return await next(context);
    }
}

// Usage
group.MapPost("/", async (CreateUserRequest request, IUserService service) =>
{
    var user = await service.CreateUserAsync(request);
    return Results.Created($"/api/users/{user.Id}", user);
})
.AddEndpointFilter<ValidationFilter<CreateUserRequest>>();
```

---

## CORS Configuration

### Setup CORS
```csharp
// ✅ BIEN
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://example.com", "https://www.example.com")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });

    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Use CORS
app.UseCors("AllowSpecificOrigin");
```

---

## Health Checks

### Basic Health Check
```csharp
// ✅ BIEN
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!)
    .AddUrlGroup(new Uri("https://api.example.com/health"), "External API");

// Map health endpoints
app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false
});
```

---

## API Versioning

### Setup Versioning
```csharp
// ✅ BIEN
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Version-specific endpoints
var v1 = app.MapGroup("/api/v1");
v1.MapUserEndpoints();

var v2 = app.MapGroup("/api/v2");
v2.MapUserEndpointsV2();
```

---

## Swagger/OpenAPI

### Enhanced Swagger Setup
```csharp
// ✅ BIEN
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "API for managing users and orders",
        Contact = new OpenApiContact
        {
            Name = "Support",
            Email = "support@example.com"
        }
    });

    // Add JWT authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
```

---

## Best Practices

### DO ✅
- Use minimal APIs for simple endpoints
- Use controllers for complex operations
- Implement proper error handling middleware
- Use DTOs for requests/responses
- Validate input with FluentValidation
- Implement JWT authentication properly
- Configure CORS restrictively
- Add health checks
- Use API versioning
- Document with Swagger/OpenAPI
- Log requests and responses
- Use dependency injection
- Return proper HTTP status codes

### DON'T ❌
- Expose entities directly
- Skip input validation
- Return detailed error messages to clients
- Allow all CORS origins in production
- Hardcode secrets in code
- Use sync methods for I/O operations
- Skip authentication/authorization
- Ignore rate limiting
- Mix business logic in controllers
- Use HTTP GET for operations that modify data

---

## HTTP Status Codes

```csharp
// ✅ BIEN - Use appropriate status codes

// Success
Results.Ok(data);                    // 200
Results.Created($"/api/users/{id}", user);  // 201
Results.NoContent();                 // 204

// Client Errors
Results.BadRequest();                // 400
Results.Unauthorized();              // 401
Results.Forbid();                    // 403
Results.NotFound();                  // 404
Results.Conflict();                  // 409
Results.ValidationProblem(errors);   // 422

// Server Errors
Results.Problem();                   // 500
```

---

## Referencias

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Minimal APIs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
- [JWT Authentication](https://jwt.io/introduction)
- [FluentValidation](https://docs.fluentvalidation.net/)
- [API Best Practices](https://docs.microsoft.com/en-us/azure/architecture/best-practices/api-design)
