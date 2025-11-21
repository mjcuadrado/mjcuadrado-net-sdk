---
name: trust
description: TRUST 5 principles for quality code in .NET
version: 0.1.0
tags: [foundation, quality, principles]
---

# TRUST 5 Principles

Los 5 pilares de calidad en mj2.

## Principio 1: Test First

### Definición
Tests escritos ANTES del código, siempre.

### Reglas
- Coverage mínimo: 85%
- Tests en xUnit + FluentAssertions
- Arrange-Act-Assert pattern
- Un test = un comportamiento
- Tests deben fallar primero (RED)
- Código mínimo para pasar (GREEN)
- Refactorizar manteniendo tests verdes (REFACTOR)

### Validación
```bash
dotnet test --collect:"XPlat Code Coverage"
# Coverage ≥85%

# Parsear resultado
coverage=$(grep -oP 'line-rate="\K[0-9.]+' coverage.cobertura.xml | head -1)
coverage_pct=$(echo "$coverage * 100" | bc -l)

if (( $(echo "$coverage_pct < 85" | bc -l) )); then
    echo "❌ Coverage: ${coverage_pct}% (need ≥85%)"
    exit 1
else
    echo "✅ Coverage: ${coverage_pct}%"
fi
```

### Ejemplos

**✅ BIEN:**
```csharp
// 1. RED: Test que falla
[Fact]
public void Login_ValidCredentials_ReturnsToken()
{
    // Arrange
    var service = new AuthService();
    var email = "user@test.com";
    var password = "SecurePass123!";

    // Act
    var result = service.Login(email, password);

    // Assert
    result.Should().NotBeNull();
    result.Token.Should().NotBeNullOrEmpty();
    result.ExpiresIn.Should().BeGreaterThan(0);
}

// 2. GREEN: Código mínimo
public class AuthService
{
    public LoginResult Login(string email, string password)
    {
        return new LoginResult
        {
            Token = "temp-token",
            ExpiresIn = 3600
        };
    }
}

// 3. REFACTOR: Mejorar sin romper tests
public class AuthService
{
    private readonly IUserRepository _repository;
    private readonly IJwtGenerator _jwtGenerator;

    public AuthService(
        IUserRepository repository,
        IJwtGenerator jwtGenerator)
    {
        _repository = repository;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<LoginResult> LoginAsync(string email, string password)
    {
        var user = await _repository.GetByEmailAsync(email);
        if (user == null || !VerifyPassword(password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        var token = _jwtGenerator.Generate(user);
        return new LoginResult
        {
            Token = token,
            ExpiresIn = 3600
        };
    }
}
```

**❌ MAL:**
```csharp
// Código sin tests
public class AuthService
{
    public LoginResult Login(string email, string password)
    {
        // Implementación sin tests previos
        // ¿Cómo sé que funciona?
        // ¿Qué pasa con casos edge?
        return new LoginResult { Token = "xyz" };
    }
}

// Test después del código (no TDD)
[Fact]
public void Login_Works()  // Nombre poco descriptivo
{
    var service = new AuthService();
    var result = service.Login("a", "b");  // Sin validación
    Assert.NotNull(result);  // Assertion débil
}
```

### Métricas
- **Coverage objetivo:** 85-95%
- **Coverage crítico:** >95% para lógica de negocio
- **Coverage mínimo:** ≥85% (hard requirement)

### Excepciones permitidas
- DTOs simples (solo propiedades)
- Program.cs / Startup.cs
- Migrations de base de datos

---

## Principio 2: Readable

### Definición
Código claro que se explica solo.

### Reglas
- **Métodos:** ≤50 líneas
- **Clases:** ≤300 líneas
- **Nombres descriptivos:** No abreviaturas (excepto comunes: id, dto, api)
- **XML docs:** En todas las APIs públicas
- **Comentarios:** Solo para "por qué", nunca "qué"
- **Complejidad ciclomática:** ≤10 por método
- **Parámetros:** ≤4 por método

### Ejemplos

**✅ BIEN:**
```csharp
/// <summary>
/// Validates user credentials and generates JWT token.
/// </summary>
/// <param name="email">User email address</param>
/// <param name="password">User password (will be hashed)</param>
/// <returns>Login result with JWT token</returns>
/// <exception cref="ArgumentException">Invalid input</exception>
/// <exception cref="UnauthorizedAccessException">Invalid credentials</exception>
public async Task<LoginResult> AuthenticateUserAsync(
    string email,
    string password)
{
    ValidateInputs(email, password);

    var user = await GetUserByEmailAsync(email);
    VerifyPassword(password, user.PasswordHash);

    return GenerateToken(user);
}

private void ValidateInputs(string email, string password)
{
    if (string.IsNullOrWhiteSpace(email))
        throw new ArgumentException("Email required", nameof(email));

    if (string.IsNullOrWhiteSpace(password))
        throw new ArgumentException("Password required", nameof(password));
}

private async Task<User> GetUserByEmailAsync(string email)
{
    var user = await _repository.GetByEmailAsync(email);
    if (user == null)
        throw new UnauthorizedAccessException("User not found");
    return user;
}

private void VerifyPassword(string password, string hash)
{
    if (!BCrypt.Net.BCrypt.Verify(password, hash))
        throw new UnauthorizedAccessException("Invalid password");
}

private LoginResult GenerateToken(User user)
{
    var token = _jwtGenerator.Generate(user);
    return new LoginResult
    {
        Token = token,
        ExpiresIn = 3600
    };
}
```

**❌ MAL:**
```csharp
// Método muy largo (>50 líneas)
public LoginResult Auth(string e, string p)  // Nombres crípticos
{
    // Valida email
    if (string.IsNullOrEmpty(e)) throw new Exception("bad");

    // Busca usuario
    User u = null;
    using (var db = new DbContext())
    {
        u = db.Users.Where(x => x.Email == e).FirstOrDefault();
    }

    // Valida password
    if (u == null) throw new Exception("not found");
    if (p != u.Pwd) throw new Exception("wrong");  // ❌ Plain text!

    // Genera token
    string t = "";
    Random r = new Random();
    for (int i = 0; i < 32; i++)
    {
        t += (char)r.Next(65, 90);
    }

    // Guarda token
    u.Token = t;
    using (var db = new DbContext())
    {
        db.Users.Update(u);
        db.SaveChanges();
    }

    // Log
    Console.WriteLine("Login OK");

    // Retorna
    return new LoginResult { Token = t };

    // 40 líneas más de lógica mezclada...
}
```

### Métricas
- **Complejidad ciclomática:** ≤10 por método
- **Líneas por método:** ≤50
- **Líneas por clase:** ≤300
- **Parámetros por método:** ≤4
- **Nivel de anidación:** ≤3

### Herramientas
```bash
# Analizar complejidad
dotnet tool install -g CodeMetrics
codemetrics analyze src/ --threshold 10

# Contar líneas
find src -name "*.cs" -exec wc -l {} \; | awk '{sum+=$1} END {print "Average:", sum/NR}'
```

---

## Principio 3: Unified

### Definición
Patrones consistentes en todo el proyecto.

### Reglas
- **Naming conventions:** PascalCase (clases, métodos), camelCase (variables, parámetros)
- **Error handling:** Siempre try-catch en servicios públicos
- **Async/await:** Métodos async terminan en "Async"
- **Dependency injection:** Constructor injection siempre
- **Repository pattern:** Para acceso a datos
- **Logging:** ILogger<T> inyectado
- **Validación:** FluentValidation o DataAnnotations

### Ejemplos

**✅ BIEN - Patrón consistente:**
```csharp
// Todos los servicios siguen el mismo patrón
public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUserRepository repository,
        IPasswordHasher passwordHasher,
        IJwtGenerator jwtGenerator,
        ILogger<AuthService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<LoginResult> LoginAsync(LoginRequest request)
    {
        try
        {
            _logger.LogInformation("Login attempt: {Email}", request.Email);

            var user = await _repository.GetByEmailAsync(request.Email);
            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                _logger.LogWarning("Failed login attempt: {Email}", request.Email);
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var token = _jwtGenerator.Generate(user);
            _logger.LogInformation("Successful login: {Email}", request.Email);

            return new LoginResult { Token = token, ExpiresIn = 3600 };
        }
        catch (Exception ex) when (!(ex is UnauthorizedAccessException))
        {
            _logger.LogError(ex, "Login error: {Email}", request.Email);
            throw;
        }
    }
}

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUserRepository repository,
        ILogger<UserService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<User> GetByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Getting user: {Id}", id);
            var user = await _repository.GetByIdAsync(id);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user: {Id}", id);
            throw;
        }
    }
}
```

**❌ MAL - Inconsistente:**
```csharp
// AuthService con DI y logging
public class AuthService
{
    private readonly IRepository _repo;
    private readonly ILogger _logger;

    public AuthService(IRepository repo, ILogger logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<LoginResult> LoginAsync(LoginRequest request)
    {
        _logger.LogInfo("Login: " + request.Email);
        // ...
    }
}

// UserService SIN DI ni logging (inconsistente)
public class UserService
{
    public User GetUser(int id)
    {
        var repo = new UserRepository();  // ❌ No DI
        return repo.Get(id);  // ❌ No async
        // ❌ No logging
        // ❌ No error handling
    }
}

// ProductService con patrón diferente
public class ProductService
{
    public static Product Get(int id)  // ❌ Método estático
    {
        using var db = new DbContext();  // ❌ DbContext directo
        return db.Products.Find(id);
    }
}
```

### Checklist de consistencia
- [ ] Todos los servicios usan DI
- [ ] Todos los métodos async terminan en "Async"
- [ ] Todos los servicios inyectan ILogger<T>
- [ ] Todos los repositorios implementan IRepository<T>
- [ ] Todos los controladores retornan ActionResult<T>
- [ ] Todas las validaciones usan FluentValidation
- [ ] Todos los errores se loggean

---

## Principio 4: Secured

### Definición
Código seguro por diseño.

### Reglas
- **Passwords:** Hasheados con bcrypt (≥10 rounds)
- **Inputs:** Validados SIEMPRE
- **Secrets:** appsettings.json + User Secrets (no en código)
- **HTTPS:** Forzado en producción
- **SQL Injection:** Usar EF Core (nunca raw SQL)
- **XSS:** Sanitizar outputs
- **CSRF:** Tokens anti-forgery
- **Rate limiting:** En APIs públicas
- **CORS:** Configurado restrictivamente

### Ejemplos

**✅ BIEN:**
```csharp
public class AuthService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _config;

    public AuthService(
        IUserRepository repository,
        IPasswordHasher passwordHasher,
        IConfiguration config)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _config = config;
    }

    public async Task<User> RegisterAsync(RegisterRequest request)
    {
        // 1. Validar inputs
        ValidateRegisterRequest(request);

        // 2. Hash password (bcrypt)
        var passwordHash = _passwordHasher.Hash(request.Password);

        // 3. Guardar usuario
        var user = new User
        {
            Email = request.Email.ToLowerInvariant(),
            PasswordHash = passwordHash,  // ✅ Hasheado
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(user);
        return user;
    }

    public async Task<LoginResult> LoginAsync(LoginRequest request)
    {
        // 1. Validar inputs
        ValidateLoginRequest(request);

        // 2. Buscar usuario
        var user = await _repository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            // ✅ No revelar si el usuario existe o no
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        // 3. Verificar password
        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            await _repository.IncrementFailedAttemptsAsync(user.Id);
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        // 4. Rate limiting check
        if (user.FailedLoginAttempts >= 5)
        {
            throw new TooManyRequestsException("Account locked");
        }

        // 5. Generar token seguro
        var secret = _config["Jwt:Secret"];  // ✅ Desde config
        var token = GenerateSecureToken(user, secret);

        // 6. Reset failed attempts
        await _repository.ResetFailedAttemptsAsync(user.Id);

        return new LoginResult { Token = token };
    }

    private void ValidateRegisterRequest(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email required");

        if (!EmailValidator.IsValid(request.Email))
            throw new ArgumentException("Invalid email format");

        if (request.Password?.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters");

        if (!ContainsDigit(request.Password))
            throw new ArgumentException("Password must contain at least one digit");

        if (!ContainsUpperCase(request.Password))
            throw new ArgumentException("Password must contain at least one uppercase letter");
    }
}

// Program.cs - HTTPS y CORS
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 443;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins", policy =>
    {
        policy.WithOrigins("https://myapp.com")  // ✅ Específico
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("api", opt =>
    {
        opt.Window = TimeSpan.FromMinutes(1);
        opt.PermitLimit = 60;
    });
});
```

**❌ MAL:**
```csharp
public class AuthService
{
    public User Register(string email, string password)
    {
        // ❌ Sin validación
        // ❌ Password en plain text
        var user = new User
        {
            Email = email,
            Password = password  // ❌ No hasheado!
        };

        _db.Users.Add(user);
        _db.SaveChanges();
        return user;
    }

    public LoginResult Login(string email, string password)
    {
        // ❌ SQL Injection vulnerable
        var sql = $"SELECT * FROM Users WHERE Email = '{email}' AND Password = '{password}'";
        var user = _db.Users.FromSqlRaw(sql).FirstOrDefault();

        if (user == null)
            return null;

        // ❌ Secret hardcodeado
        var secret = "my-super-secret-key-123";
        var token = GenerateToken(user, secret);

        return new LoginResult { Token = token };
    }
}

// ❌ CORS permisivo
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod());

// ❌ HTTP permitido en producción
// Sin configuración de HTTPS
```

### Checklist de seguridad
- [ ] Passwords hasheados con bcrypt/Argon2
- [ ] Inputs validados (email, longitud, formato)
- [ ] Secrets en appsettings.json (no en código)
- [ ] HTTPS forzado en producción
- [ ] EF Core para queries (no raw SQL)
- [ ] CORS configurado restrictivamente
- [ ] Rate limiting en APIs
- [ ] Failed login attempts tracking
- [ ] Account lockout después de X intentos
- [ ] Logs de eventos de seguridad

### Herramientas
```bash
# Security audit
dotnet list package --vulnerable

# Dependency check
dotnet restore --verify-security

# Secret scanning
git secrets --scan
```

---

## Principio 5: Trackable

### Definición
Trazabilidad completa con TAG system.

### Reglas
- **@SPEC:** en especificaciones (docs/specs/)
- **@TEST:** en tests (tests/)
- **@CODE:** en implementación (src/)
- **@DOC:** en documentación (docs/, README.md)
- Cadena completa obligatoria: @SPEC → @TEST → @CODE → @DOC

### Formato de TAGs

```
@SPEC:EX-{SPEC-ID}:{REQ-ID}
@TEST:EX-{SPEC-ID}:{REQ-ID}
@CODE:EX-{SPEC-ID}:{REQ-ID}
@DOC:EX-{SPEC-ID}
```

### Ejemplos

**✅ BIEN:**
```csharp
// Specification (docs/specs/SPEC-AUTH-001/spec.md)
---
spec_id: SPEC-AUTH-001
title: User Authentication
---

<!-- @SPEC:EX-AUTH-001 -->
## FR-1: User Login
**@SPEC:EX-AUTH-001:FR-1**
The system SHALL authenticate users with email/password.

## FR-2: Token Generation
**@SPEC:EX-AUTH-001:FR-2**
WHEN credentials validated successfully
THEN system SHALL generate JWT with 24h expiration

// Test (tests/Auth/AuthServiceTests.cs)
// @TEST:EX-AUTH-001 | SPEC: SPEC-AUTH-001.md
using FluentAssertions;
using Xunit;

namespace MyApp.Tests.Auth;

public class AuthServiceTests
{
    // @TEST:EX-AUTH-001:FR-1
    [Fact]
    public void Login_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var service = new AuthService();

        // Act
        var result = service.Login("user@test.com", "pass123");

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().NotBeNullOrEmpty();
    }

    // @TEST:EX-AUTH-001:FR-2
    [Fact]
    public void Login_ValidCredentials_TokenExpires24Hours()
    {
        // Arrange
        var service = new AuthService();

        // Act
        var result = service.Login("user@test.com", "pass123");

        // Assert
        result.ExpiresIn.Should().Be(86400);  // 24 hours
    }
}

// Implementation (src/Auth/AuthService.cs)
// @CODE:EX-AUTH-001 | SPEC: SPEC-AUTH-001.md | TEST: AuthServiceTests.cs
namespace MyApp.Auth;

/// <summary>
/// Authentication service
/// </summary>
public class AuthService : IAuthService
{
    // @CODE:EX-AUTH-001:FR-1
    public LoginResult Login(string email, string password)
    {
        ValidateCredentials(email, password);
        var user = GetUser(email);
        VerifyPassword(password, user.PasswordHash);
        return GenerateToken(user);
    }

    // @CODE:EX-AUTH-001:FR-2
    private LoginResult GenerateToken(User user)
    {
        var token = _jwtGenerator.Generate(user);
        return new LoginResult
        {
            Token = token,
            ExpiresIn = 86400  // 24 hours
        };
    }
}

// Documentation (README.md)
# Features

## Authentication
<!-- @DOC:EX-AUTH-001 | SPEC: SPEC-AUTH-001.md -->
- ✅ User authentication with JWT
  - Email/password login
  - 24-hour token expiration
  - See: [SPEC-AUTH-001](docs/specs/SPEC-AUTH-001/spec.md)
```

**❌ MAL:**
```csharp
// Sin TAGs, sin trazabilidad
public class AuthService
{
    public LoginResult Login(string email, string password)
    {
        // ¿De qué SPEC viene esto?
        // ¿Qué test lo valida?
        // ¿Está documentado?
        // Imposible rastrear el origen del requisito
        var token = GenerateToken(email);
        return new LoginResult { Token = token };
    }
}
```

### Validación de TAGs

```bash
#!/bin/bash

spec_id="$1"  # Example: AUTH-001

echo "Validating TAG chain for SPEC-${spec_id}..."

# 1. Verificar @SPEC:
spec_tags=$(grep -r "@SPEC:EX-${spec_id}" docs/specs/SPEC-${spec_id}/ | wc -l)
if [ $spec_tags -eq 0 ]; then
    echo "❌ @SPEC: tags not found"
    exit 1
else
    echo "✅ @SPEC: $spec_tags tags found"
fi

# 2. Verificar @TEST:
test_tags=$(grep -r "@TEST:EX-${spec_id}" tests/ | wc -l)
if [ $test_tags -eq 0 ]; then
    echo "❌ @TEST: tags not found"
    exit 1
else
    echo "✅ @TEST: $test_tags tags found"
fi

# 3. Verificar @CODE:
code_tags=$(grep -r "@CODE:EX-${spec_id}" src/ | wc -l)
if [ $code_tags -eq 0 ]; then
    echo "❌ @CODE: tags not found"
    exit 1
else
    echo "✅ @CODE: $code_tags tags found"
fi

# 4. Verificar @DOC:
doc_tags=$(grep -r "@DOC:EX-${spec_id}" docs/ README.md | wc -l)
if [ $doc_tags -eq 0 ]; then
    echo "⚠️  @DOC: tags not found (may not be synced yet)"
else
    echo "✅ @DOC: $doc_tags tags found"
fi

echo ""
echo "TAG chain validation complete!"
```

---

## Validación TRUST 5 Completa

### Script de validación
```bash
#!/bin/bash

echo "========================================="
echo "TRUST 5 Validation"
echo "========================================="
echo ""

# 1. Test First
echo "1️⃣  Test First"
dotnet test --collect:"XPlat Code Coverage" > /dev/null 2>&1
coverage=$(grep -oP 'line-rate="\K[0-9.]+' TestResults/*/coverage.cobertura.xml | head -1)
coverage_pct=$(echo "$coverage * 100" | bc -l | cut -d. -f1)

if [ $coverage_pct -ge 85 ]; then
    echo "   ✅ Coverage: ${coverage_pct}% (≥85%)"
    test_first_score=10
else
    echo "   ❌ Coverage: ${coverage_pct}% (need ≥85%)"
    test_first_score=0
fi
echo ""

# 2. Readable
echo "2️⃣  Readable"
long_methods=$(find src -name "*.cs" -exec awk '/public |private /{p=1} p && /\{/{b++} p && /\}/{b--; if(b==0){if(NR-start>50)print FILENAME":"start; p=0}} p{start=NR}' {} \; | wc -l)

if [ $long_methods -eq 0 ]; then
    echo "   ✅ No methods >50 lines"
    readable_score=10
else
    echo "   ⚠️  $long_methods methods >50 lines"
    readable_score=8
fi
echo ""

# 3. Unified
echo "3️⃣  Unified"
echo "   ✅ Manual review required"
unified_score=10
echo ""

# 4. Secured
echo "4️⃣  Secured"
secrets=$(grep -r "password.*=.*\".*\"" src/ --include="*.cs" | grep -v "//" | wc -l)
if [ $secrets -eq 0 ]; then
    echo "   ✅ No hardcoded secrets"
    secured_score=10
else
    echo "   ⚠️  $secrets possible hardcoded secrets"
    secured_score=5
fi
echo ""

# 5. Trackable
echo "5️⃣  Trackable"
code_tags=$(grep -r "@CODE:" src/ | wc -l)
if [ $code_tags -gt 0 ]; then
    echo "   ✅ $code_tags @CODE: tags found"
    trackable_score=10
else
    echo "   ❌ No @CODE: tags found"
    trackable_score=0
fi
echo ""

# Total score
total_score=$((test_first_score + readable_score + unified_score + secured_score + trackable_score))

echo "========================================="
echo "TOTAL SCORE: $total_score / 50"
echo "========================================="

if [ $total_score -ge 45 ]; then
    echo "✅ TRUST 5: EXCELLENT"
    exit 0
elif [ $total_score -ge 35 ]; then
    echo "⚠️  TRUST 5: GOOD (needs improvement)"
    exit 0
else
    echo "❌ TRUST 5: FAILED"
    exit 1
fi
```

---

## Referencias

### Libros
- [Clean Code (Robert Martin)](https://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882)
- [The Pragmatic Programmer (Hunt & Thomas)](https://pragprog.com/titles/tpp20/the-pragmatic-programmer-20th-anniversary-edition/)
- [Test Driven Development (Kent Beck)](https://www.amazon.com/Test-Driven-Development-Kent-Beck/dp/0321146530)

### Estándares
- [OWASP Top 10](https://owasp.org/www-project-top-ten/)
- [.NET Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [C# Naming Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)

### Herramientas
- [SonarQube](https://www.sonarqube.org/) - Code quality analysis
- [ReSharper](https://www.jetbrains.com/resharper/) - Code analysis and refactoring
- [CodeMetrics](https://github.com/terrajobst/code-metrics) - Complexity analysis

---

## Resumen

**TRUST 5 = Calidad garantizada**

1. **T**est First → Coverage ≥85%
2. **R**eadable → Métodos ≤50 líneas, nombres claros
3. **U**nified → Patrones consistentes
4. **S**ecured → Bcrypt, validación, secrets en config
5. **T**rackable → TAG chain completa

**Sin TRUST 5, no hay merge. Sin excepciones.**
