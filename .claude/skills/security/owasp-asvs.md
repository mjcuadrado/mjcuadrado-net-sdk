---
skill: owasp-asvs
category: security
description: OWASP ASVS (Application Security Verification Standard) nivel 2 para aplicaciones .NET
version: 1.0.0
tags: [owasp, asvs, security, checklist, verification, aspnet-core]
related_skills: [jwt, rate-limiting, aspnet-core]
---

# OWASP ASVS - Application Security Verification Standard

Guía completa de **OWASP ASVS nivel 2** para aplicaciones .NET, con checklist de seguridad y guidelines de implementación.

---

## 📋 Índice

1. [¿Qué es OWASP ASVS?](#qué-es-owasp-asvs)
2. [Niveles de Verificación](#niveles-de-verificación)
3. [V1: Architecture & Design](#v1-architecture--design)
4. [V2: Authentication](#v2-authentication)
5. [V3: Session Management](#v3-session-management)
6. [V4: Access Control](#v4-access-control)
5. [V5: Validation & Encoding](#v5-validation--encoding)
6. [V7: Error Handling](#v7-error-handling)
7. [V8: Data Protection](#v8-data-protection)
8. [V9: Communication](#v9-communication)
9. [Security Checklist](#security-checklist)
10. [Testing & Validation](#testing--validation)

---

## 🎯 ¿Qué es OWASP ASVS?

**OWASP Application Security Verification Standard (ASVS)** es un estándar de verificación de seguridad de aplicaciones web que proporciona un framework de requisitos de seguridad.

### Propósito

- ✅ **Desarrolladores:** Guía para diseñar y construir aplicaciones seguras
- ✅ **Arquitectos:** Patrones de seguridad y decisiones de diseño
- ✅ **Testers:** Checklist de verificación de seguridad
- ✅ **Organizaciones:** Estándar para requisitos de seguridad

### Versión Actual

- **ASVS 4.0:** Última versión estable (2019)
- **14 Categorías:** V1-V14
- **3 Niveles:** L1 (básico), L2 (estándar), L3 (avanzado)

---

## 🏅 Niveles de Verificación

### Level 1: Opportunistic

- **Uso:** Todas las aplicaciones
- **Objetivo:** Protección contra vulnerabilidades obvias
- **Verificación:** Automática (scanners, linters)

### Level 2: Standard (FOCO DE ESTA SKILL)

- **Uso:** Aplicaciones con datos sensibles
- **Objetivo:** Defensa en profundidad contra ataques comunes
- **Verificación:** Manual + Automática
- **Target:** Mayoría de aplicaciones empresariales

### Level 3: Advanced

- **Uso:** Aplicaciones críticas (banca, salud)
- **Objetivo:** Máxima seguridad
- **Verificación:** Pen testing exhaustivo

---

## 🏗️ V1: Architecture & Design

### V1.1: Secure SDLC

**Requisitos ASVS Nivel 2:**

- [ ] **V1.1.1:** Threat modeling documentado
- [ ] **V1.1.2:** Controles de seguridad centralizados
- [ ] **V1.1.3:** Componentes seguros y actualizados

**Implementación .NET:**

```csharp
// ✅ Centralizar seguridad en middleware
public class SecurityHeadersMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Security headers centralizados
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add("X-Frame-Options", "DENY");
        context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
        context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
        context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");

        await next(context);
    }
}

// Program.cs
app.UseMiddleware<SecurityHeadersMiddleware>();
```

### V1.2: Authentication Architecture

- [ ] **V1.2.1:** Credenciales en canales cifrados (HTTPS)
- [ ] **V1.2.2:** Almacenamiento seguro de credenciales (hash + salt)

```csharp
// ✅ ASP.NET Core Identity - hash automático
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
```

### V1.4: Access Control Architecture

- [ ] **V1.4.1:** Principio de least privilege
- [ ] **V1.4.2:** Authorization basada en servidor (no cliente)

```csharp
// ✅ Policy-based authorization centralizada
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("CanEditOrders", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") ||
            context.User.IsInRole("OrderManager")));
});
```

---

## 🔐 V2: Authentication

### V2.1: Password Security

- [ ] **V2.1.1:** Longitud mínima 8 caracteres (L1), 12+ recomendado (L2)
- [ ] **V2.1.2:** Permitir hasta 64 caracteres (no limitar)
- [ ] **V2.1.3:** Unicode completo (emojis, etc.)
- [ ] **V2.1.7:** No passwords comunes (top 10,000)

```csharp
// Configuración ASP.NET Core Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 12;  // ✅ L2: 12+ caracteres
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredUniqueChars = 4;
});
```

### V2.2: General Authenticator Security

- [ ] **V2.2.1:** Anti-automation (rate limiting, CAPTCHA)
- [ ] **V2.2.2:** MFA disponible
- [ ] **V2.2.3:** Recuperación segura de credenciales

```csharp
// ✅ Account lockout
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});
```

### V2.7: Out of Band Verifiers

- [ ] **V2.7.1:** Tokens OTP resistentes a MITM
- [ ] **V2.7.2:** Single use OTP tokens

```csharp
// ✅ ASP.NET Core Identity - Token providers
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultTokenProviders();  // Genera tokens de un solo uso

// Verificar token
var isValid = await _userManager.VerifyUserTokenAsync(
    user,
    TokenOptions.DefaultProvider,
    "ResetPassword",
    token
);
```

---

## 🔄 V3: Session Management

### V3.1: Fundamental Session Management Security

- [ ] **V3.1.1:** No URLs con tokens de sesión
- [ ] **V3.2.1:** Nuevo session ID después de login
- [ ] **V3.2.2:** Logout invalida sesión

```csharp
// ✅ JWT stateless - no sesiones en servidor

// Logout: Blacklist token (opcional)
public async Task LogoutAsync(string jti)
{
    // Agregar JTI a blacklist hasta su expiración
    await _cache.SetAsync(
        $"blacklist:{jti}",
        Array.Empty<byte>(),
        new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.UtcNow.AddMinutes(15)
        }
    );
}

// Validar en cada request
options.Events = new JwtBearerEvents
{
    OnTokenValidated = async context =>
    {
        var jti = context.Principal?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
        if (jti != null)
        {
            var cache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();
            var blacklisted = await cache.GetAsync($"blacklist:{jti}");
            if (blacklisted != null)
            {
                context.Fail("Token has been revoked");
            }
        }
    }
};
```

### V3.3: Session Termination

- [ ] **V3.3.1:** Logout en cliente y servidor
- [ ] **V3.3.2:** Timeout de inactividad

```csharp
// Cookie timeout
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});
```

---

## 🚪 V4: Access Control

### V4.1: General Access Control Design

- [ ] **V4.1.1:** Enforcement en trusted service layer
- [ ] **V4.1.2:** Attribute/feature-based access control
- [ ] **V4.1.3:** Principle of least privilege
- [ ] **V4.1.5:** Deny by default

```csharp
// ✅ Policy-based authorization
[Authorize(Policy = "CanEditOrders")]
public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequest request)
{
    // Verificar ownership también
    var order = await _context.Orders.FindAsync(id);
    if (order.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
    {
        return Forbid();  // ✅ Deny by default
    }

    // Update order...
    return Ok();
}
```

### V4.2: Operation Level Access Control

- [ ] **V4.2.1:** Datos sensibles protegidos
- [ ] **V4.2.2:** Sin CORS promiscuo

```csharp
// ✅ CORS específico
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://myapp.com")  // ✅ Específico
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// ❌ NUNCA:
policy.AllowAnyOrigin()  // MAL - Promiscuo
```

---

## ✅ V5: Validation & Encoding

### V5.1: Input Validation

- [ ] **V5.1.1:** Whitelist validation
- [ ] **V5.1.2:** Data type validation
- [ ] **V5.1.3:** Longitudes máximas
- [ ] **V5.1.4:** Rangos numéricos

```csharp
// ✅ FluentValidation
public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .WithMessage("Product ID must be positive");

        RuleFor(x => x.Quantity)
            .InclusiveBetween(1, 1000)
            .WithMessage("Quantity must be between 1 and 1000");

        RuleFor(x => x.CustomerEmail)
            .EmailAddress()
            .MaximumLength(255)
            .WithMessage("Valid email required, max 255 characters");
    }
}

// Registrar validators
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderValidator>();

// Usar en controller
public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
{
    var validator = new CreateOrderValidator();
    var result = await validator.ValidateAsync(request);

    if (!result.IsValid)
        return BadRequest(result.Errors);

    // Process order...
}
```

### V5.2: Sanitization and Sandboxing

- [ ] **V5.2.1:** Sanitización contra XSS

```csharp
// ✅ HtmlEncoder automático en Razor
@Model.UserInput  // Automáticamente encoded

// Manual encoding si se necesita
using System.Text.Encodings.Web;

var encoded = HtmlEncoder.Default.Encode(userInput);
```

### V5.3: Output Encoding and Injection Prevention

- [ ] **V5.3.1:** Output encoding context-aware
- [ ] **V5.3.3:** Parameterized queries (SQL injection prevention)

```csharp
// ✅ Entity Framework - Parameterized automáticamente
var orders = await _context.Orders
    .Where(o => o.CustomerId == customerId)  // ✅ Safe
    .ToListAsync();

// ❌ NUNCA string concatenation:
var sql = $"SELECT * FROM Orders WHERE CustomerId = {customerId}";  // MAL - SQL Injection
```

---

## ⚠️ V7: Error Handling

### V7.1: Log Content

- [ ] **V7.1.1:** No logs de credenciales
- [ ] **V7.1.2:** No logs de session tokens
- [ ] **V7.1.3:** No logs de datos sensibles (PII)

```csharp
// ✅ Structured logging con Serilog - redact secrets
Log.Logger = new LoggerConfiguration()
    .Destructure.ByTransforming<LoginRequest>(r => new
    {
        r.Email,
        Password = "***REDACTED***"  // ✅ No loggear password
    })
    .WriteTo.Console()
    .CreateLogger();

// Log
_logger.LogInformation("Login attempt for {Email}", request.Email);  // ✅ OK
_logger.LogInformation("Login attempt {@Request}", request);  // ✅ Password redacted
```

### V7.2: Log Processing

- [ ] **V7.2.1:** Logs protegidos contra inyección
- [ ] **V7.2.2:** Logs accesibles solo para autorizados

### V7.4: Error Handling

- [ ] **V7.4.1:** Mensajes genéricos en producción
- [ ] **V7.4.2:** No stack traces en producción

```csharp
// ✅ Global exception handler
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (app.Environment.IsDevelopment())
        {
            // Dev: Detalle completo
            await context.Response.WriteAsJsonAsync(new
            {
                error = exceptionFeature?.Error.Message,
                stackTrace = exceptionFeature?.Error.StackTrace
            });
        }
        else
        {
            // ✅ Production: Mensaje genérico
            await context.Response.WriteAsJsonAsync(new
            {
                error = "An error occurred processing your request."
            });

            // Log internamente
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(exceptionFeature?.Error, "Unhandled exception");
        }
    });
});
```

---

## 🔒 V8: Data Protection

### V8.1: General Data Protection

- [ ] **V8.1.1:** Datos sensibles identificados y clasificados
- [ ] **V8.1.2:** Datos sensibles cifrados en reposo

```csharp
// ✅ Entity Framework - Encryption
public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;

    [EncryptedColumn]  // Custom attribute para encryption
    public string SocialSecurityNumber { get; set; } = string.Empty;
}

// Encryption en DbContext
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
        .Property(u => u.SocialSecurityNumber)
        .HasConversion(
            v => _encryptionService.Encrypt(v),
            v => _encryptionService.Decrypt(v)
        );
}
```

### V8.2: Client-side Data Protection

- [ ] **V8.2.1:** No secretos en código cliente
- [ ] **V8.2.2:** localStorage con datos no sensibles

### V8.3: Sensitive Private Data

- [ ] **V8.3.4:** Datos sensibles no en logs
- [ ] **V8.3.5:** PII accesible solo cuando necesario
- [ ] **V8.3.6:** PII enmascarada en UI

```csharp
// ✅ Masking de datos sensibles
public string MaskCreditCard(string cardNumber)
{
    if (cardNumber.Length < 4)
        return "****";

    return "****-****-****-" + cardNumber.Substring(cardNumber.Length - 4);
}

// UI: 1234-5678-9012-3456 → ****-****-****-3456
```

---

## 🌐 V9: Communication

### V9.1: Client Communication Security

- [ ] **V9.1.1:** TLS para todas las conexiones cliente
- [ ] **V9.1.2:** TLS 1.2+ (no TLS 1.0/1.1)
- [ ] **V9.1.3:** Latest ciphers configurados

```csharp
// ✅ Forzar HTTPS
builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
    options.HttpsPort = 443;
});

app.UseHttpsRedirection();

// ✅ HSTS
app.UseHsts();  // Strict-Transport-Security header
```

### V9.2: Server Communication Security

- [ ] **V9.2.1:** Conexiones a servicios cifradas (TLS)
- [ ] **V9.2.2:** Certificados válidos y confiables
- [ ] **V9.2.3:** Certificados actualizados

```csharp
// ✅ HttpClient con TLS
builder.Services.AddHttpClient("SecureClient", client =>
{
    client.BaseAddress = new Uri("https://api.example.com");
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator  // ❌ Solo Dev
});

// ✅ Production: Validar certificado correctamente
```

---

## ✅ Security Checklist

### Authentication
- [ ] Passwords hasheadas (bcrypt/PBKDF2/Argon2)
- [ ] MFA disponible
- [ ] Account lockout (5 intentos, 15 min)
- [ ] Password requirements (12+ chars, complejidad)

### Session/Tokens
- [ ] JWT short-lived (15 min)
- [ ] Refresh tokens con rotación
- [ ] Tokens en HttpOnly cookies o Authorization header
- [ ] HTTPS obligatorio

### Authorization
- [ ] Policy-based authorization
- [ ] Deny by default
- [ ] Verificar ownership de recursos
- [ ] Least privilege principle

### Input/Output
- [ ] Validación de todos los inputs
- [ ] Output encoding automático
- [ ] Parameterized queries (EF Core)
- [ ] CORS específico

### Data Protection
- [ ] Datos sensibles cifrados en reposo
- [ ] PII enmascarada en UI
- [ ] No secretos en logs
- [ ] No secretos en código

### Communication
- [ ] HTTPS/TLS everywhere
- [ ] HSTS enabled
- [ ] Security headers configurados
- [ ] Certificate validation

### Error Handling
- [ ] Mensajes genéricos en producción
- [ ] No stack traces expuestos
- [ ] Logging centralizado
- [ ] Alertas para errores críticos

---

## 🧪 Testing & Validation

### Automated Testing

```csharp
// Security tests
[Fact]
public async Task Unauthorized_Request_Returns_401()
{
    var response = await _client.GetAsync("/api/protected");
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
}

[Fact]
public async Task XSS_Input_Is_Encoded()
{
    var maliciousInput = "<script>alert('xss')</script>";
    var result = HtmlEncoder.Default.Encode(maliciousInput);
    Assert.DoesNotContain("<script>", result);
}
```

### Manual Testing

- Penetration testing periódico
- Security code reviews
- OWASP ZAP / Burp Suite scans
- Dependency vulnerability scanning (dotnet list package --vulnerable)

---

## 📚 Recursos

### OWASP Resources
- **ASVS 4.0:** https://owasp.org/www-project-application-security-verification-standard/
- **OWASP Top 10:** https://owasp.org/www-project-top-ten/
- **OWASP Cheat Sheets:** https://cheatsheetseries.owasp.org/

### .NET Security
- **Microsoft Security:** https://learn.microsoft.com/en-us/aspnet/core/security/
- **Identity:** https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-22
**Mantenido por:** mjcuadrado-net-sdk
