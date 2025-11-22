---
skill: jwt
category: security
description: JWT (JSON Web Tokens) authentication para aplicaciones .NET
version: 1.0.0
tags: [jwt, authentication, security, tokens, claims, aspnet-core]
related_skills: [aspnet-core, owasp-asvs, rate-limiting]
---

# JWT (JSON Web Tokens) Authentication

Guía completa de autenticación con JWT en aplicaciones .NET, incluyendo access tokens, refresh tokens, y claims-based authentication.

---

## 📋 Índice

1. [JWT Fundamentals](#jwt-fundamentals)
2. [Access Tokens](#access-tokens)
3. [Refresh Tokens](#refresh-tokens)
4. [Claims-Based Authentication](#claims-based-authentication)
5. [Cookie vs Header Strategies](#cookie-vs-header-strategies)
6. [.NET Implementation](#net-implementation)
7. [Token Generation](#token-generation)
8. [Token Validation](#token-validation)
9. [Security Best Practices](#security-best-practices)
10. [Integration con Identity](#integration-con-identity)

---

## 🎯 JWT Fundamentals

### ¿Qué es JWT?

**JWT (JSON Web Token)** es un estándar abierto (RFC 7519) para transmitir información de forma segura entre partes como un objeto JSON.

**Estructura de un JWT:**

```
header.payload.signature

eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.
SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

### Componentes

**1. Header (Encabezado):**
```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

**2. Payload (Datos):**
```json
{
  "sub": "1234567890",
  "name": "John Doe",
  "iat": 1516239022,
  "exp": 1516242622
}
```

**3. Signature (Firma):**
```
HMACSHA256(
  base64UrlEncode(header) + "." +
  base64UrlEncode(payload),
  secret)
```

### Claims Estándar

| Claim | Descripción | Ejemplo |
|-------|-------------|---------|
| `sub` | Subject (usuario ID) | `"1234567890"` |
| `iss` | Issuer (emisor) | `"https://myapi.com"` |
| `aud` | Audience (audiencia) | `"https://myapp.com"` |
| `exp` | Expiration time | `1516242622` |
| `iat` | Issued at | `1516239022` |
| `nbf` | Not before | `1516239022` |
| `jti` | JWT ID (identificador único) | `"abc123"` |

---

## 🔑 Access Tokens

### Características

- **Short-lived:** 15 minutos típicamente
- **Stateless:** No se almacenan en servidor
- **Bearer token:** Se incluye en cada request
- **Contains claims:** Información del usuario

### Uso

**1. Login → Obtener access token:**
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "SecurePass123!"
}

Response:
{
  "accessToken": "eyJhbGci...",
  "refreshToken": "def50200...",
  "expiresIn": 900
}
```

**2. Usar access token en requests:**
```http
GET /api/protected
Authorization: Bearer eyJhbGci...
```

### Ventajas

- ✅ **Stateless:** No requiere storage en servidor
- ✅ **Escalable:** Funciona en arquitecturas distribuidas
- ✅ **Cross-domain:** Funciona entre dominios diferentes
- ✅ **Mobile-friendly:** Ideal para apps móviles

### Desventajas

- ❌ **No revocable:** Una vez emitido, válido hasta expiración
- ❌ **Vulnerable a XSS:** Si se almacena en localStorage
- ❌ **Tamaño:** Más grande que un session ID

---

## 🔄 Refresh Tokens

### ¿Por qué Refresh Tokens?

**Problema:** Access tokens de larga duración = mayor riesgo de seguridad

**Solución:** Access token corto (15 min) + Refresh token largo (7 días)

### Workflow

```
1. Login → Access Token (15 min) + Refresh Token (7 días)
   ↓
2. Usar Access Token para requests
   ↓
3. Access Token expira
   ↓
4. Refresh Token → Nuevo Access Token
   ↓
5. Continuar usando nuevo Access Token
```

### Implementation Pattern

**1. Login - Generar ambos tokens:**
```csharp
public async Task<LoginResponse> LoginAsync(LoginRequest request)
{
    // Validar credenciales
    var user = await _userManager.FindByEmailAsync(request.Email);
    if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        throw new UnauthorizedAccessException("Invalid credentials");

    // Generar access token (15 min)
    var accessToken = GenerateAccessToken(user);

    // Generar refresh token (7 días)
    var refreshToken = GenerateRefreshToken();
    user.RefreshToken = refreshToken;
    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
    await _userManager.UpdateAsync(user);

    return new LoginResponse
    {
        AccessToken = accessToken,
        RefreshToken = refreshToken,
        ExpiresIn = 900 // 15 minutos
    };
}
```

**2. Refresh - Intercambiar refresh token:**
```csharp
public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
{
    var user = await _context.Users
        .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

    if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        throw new UnauthorizedAccessException("Invalid or expired refresh token");

    // Generar nuevo access token
    var newAccessToken = GenerateAccessToken(user);

    // Opcionalmente, rotar refresh token también
    var newRefreshToken = GenerateRefreshToken();
    user.RefreshToken = newRefreshToken;
    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
    await _context.SaveChangesAsync();

    return new TokenResponse
    {
        AccessToken = newAccessToken,
        RefreshToken = newRefreshToken
    };
}
```

### Storage de Refresh Tokens

**Base de datos:**
```csharp
public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
```

---

## 👤 Claims-Based Authentication

### ¿Qué son Claims?

**Claims:** Afirmaciones sobre el usuario (key-value pairs)

**Ejemplos:**
```csharp
new Claim(ClaimTypes.NameIdentifier, user.Id),
new Claim(ClaimTypes.Email, user.Email),
new Claim(ClaimTypes.Role, "Admin"),
new Claim("department", "Engineering"),
new Claim("subscription", "Premium")
```

### Claims vs Roles

| Aspecto | Roles | Claims |
|---------|-------|--------|
| **Granularidad** | Baja (Admin, User) | Alta (department=Engineering) |
| **Flexibilidad** | Fija | Dinámica |
| **Authorization** | `[Authorize(Roles = "Admin")]` | `[Authorize(Policy = "PremiumOnly")]` |

### Custom Claims

```csharp
// Generar JWT con custom claims
private string GenerateAccessToken(ApplicationUser user)
{
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("department", user.Department),
        new Claim("subscription", user.SubscriptionLevel)
    };

    // Agregar roles como claims
    var roles = await _userManager.GetRolesAsync(user);
    foreach (var role in roles)
    {
        claims.Add(new Claim(ClaimTypes.Role, role));
    }

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(15),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
```

### Policy-Based Authorization

**Configurar policies:**
```csharp
// Program.cs
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PremiumOnly", policy =>
        policy.RequireClaim("subscription", "Premium"));

    options.AddPolicy("AdminOrManager", policy =>
        policy.RequireRole("Admin", "Manager"));

    options.AddPolicy("EngineeringDepartment", policy =>
        policy.RequireClaim("department", "Engineering"));
});
```

**Usar en controllers:**
```csharp
[Authorize(Policy = "PremiumOnly")]
public IActionResult GetPremiumFeature()
{
    return Ok(new { feature = "Premium Content" });
}
```

---

## 🍪 Cookie vs Header Strategies

### Header Strategy (Bearer Token)

**Almacenamiento:** localStorage o sessionStorage

**Envío:**
```http
Authorization: Bearer eyJhbGci...
```

**Ventajas:**
- ✅ Funciona cross-domain
- ✅ Ideal para APIs públicas
- ✅ Compatible con mobile apps

**Desventajas:**
- ❌ Vulnerable a XSS si se usa localStorage
- ❌ Requiere manejo manual en cada request

### Cookie Strategy (HttpOnly Cookie)

**Configuración:**
```csharp
// Generar token y almacenar en cookie
var token = GenerateAccessToken(user);

Response.Cookies.Append("accessToken", token, new CookieOptions
{
    HttpOnly = true,  // No accesible desde JavaScript
    Secure = true,    // Solo HTTPS
    SameSite = SameSiteMode.Strict,  // CSRF protection
    Expires = DateTimeOffset.UtcNow.AddMinutes(15)
});
```

**Ventajas:**
- ✅ Protección contra XSS (HttpOnly)
- ✅ Envío automático en cada request
- ✅ Más seguro para web apps

**Desventajas:**
- ❌ Vulnerable a CSRF (requiere tokens CSRF)
- ❌ No funciona bien cross-domain
- ❌ Menos compatible con mobile apps

### Recommendation

**Web App (mismo dominio):** Cookie Strategy (HttpOnly + Secure + SameSite)

**API pública / Mobile app:** Header Strategy con:
- Almacenar en memoria (React state) o sessionStorage
- Implementar auto-refresh antes de expiración
- Usar HTTPS siempre

---

## 🔧 .NET Implementation

### Instalación

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt
```

### Configuración Completa

**appsettings.json:**
```json
{
  "Jwt": {
    "Issuer": "https://myapi.com",
    "Audience": "https://myapp.com",
    "Key": "YourSuperSecretKeyWith256BitsOrMore!!!"
  }
}
```

**Program.cs:**
```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ClockSkew = TimeSpan.Zero  // Eliminar 5 min de gracia default
    };

    // Para cookies
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["accessToken"];
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
```

---

## 🔐 Security Best Practices

### 1. Token Expiration

```csharp
// ✅ Access token corto
expires: DateTime.UtcNow.AddMinutes(15)

// ✅ Refresh token más largo
refreshTokenExpiry: DateTime.UtcNow.AddDays(7)

// ❌ NUNCA tokens de larga duración
expires: DateTime.UtcNow.AddYears(1)  // MAL
```

### 2. Secret Key Seguro

```csharp
// ✅ Key de 256 bits o más
"YourSuperSecretKeyWith256BitsOrMore!!!"

// ✅ Almacenar en User Secrets / Environment Variables
var key = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

// ❌ NUNCA hardcodear en código
var key = "mysecret";  // MAL
```

### 3. HTTPS Only

```csharp
// Forzar HTTPS
app.UseHttpsRedirection();

// Cookie secure
new CookieOptions { Secure = true }
```

### 4. Validación Completa

```csharp
options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,       // ✅
    ValidateAudience = true,     // ✅
    ValidateLifetime = true,     // ✅
    ValidateIssuerSigningKey = true,  // ✅
    ClockSkew = TimeSpan.Zero    // ✅ Eliminar gracia de 5 min
};
```

### 5. Refresh Token Rotation

```csharp
// Al hacer refresh, generar NUEVO refresh token
var newRefreshToken = GenerateRefreshToken();
user.RefreshToken = newRefreshToken;  // Rotar

// Invalidar el anterior automáticamente
```

### 6. Token Revocation (Opcional)

**Para casos críticos (logout, cambio de password):**

```csharp
// Implementar blacklist de tokens
public class TokenBlacklist
{
    private static readonly ConcurrentDictionary<string, DateTime> _blacklist = new();

    public static void Add(string jti, DateTime expiry)
    {
        _blacklist[jti] = expiry;
    }

    public static bool IsBlacklisted(string jti)
    {
        return _blacklist.TryGetValue(jti, out var expiry) && expiry > DateTime.UtcNow;
    }
}

// Validar en cada request
options.Events = new JwtBearerEvents
{
    OnTokenValidated = context =>
    {
        var jti = context.Principal?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
        if (jti != null && TokenBlacklist.IsBlacklisted(jti))
        {
            context.Fail("Token has been revoked");
        }
        return Task.CompletedTask;
    }
};
```

---

## 🔗 Integration con Identity

### Setup Completo

```csharp
// Entities/ApplicationUser.cs
public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string Department { get; set; } = string.Empty;
    public string SubscriptionLevel { get; set; } = "Free";
}

// Program.cs
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});
```

---

## 📚 Recursos

### Documentación
- **RFC 7519:** https://tools.ietf.org/html/rfc7519
- **JWT.io:** https://jwt.io/
- **Microsoft Docs:** https://learn.microsoft.com/en-us/aspnet/core/security/authentication/

### NuGet Packages
```bash
Microsoft.AspNetCore.Authentication.JwtBearer
System.IdentityModel.Tokens.Jwt
Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-22
**Mantenido por:** mjcuadrado-net-sdk
