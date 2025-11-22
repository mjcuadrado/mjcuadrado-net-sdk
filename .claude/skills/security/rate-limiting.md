---
skill: rate-limiting
category: security
description: Rate limiting y DDoS protection para aplicaciones ASP.NET Core
version: 1.0.0
tags: [rate-limiting, ddos, security, aspnet-core, redis]
related_skills: [jwt, owasp-asvs, aspnet-core]
---

# Rate Limiting

Guía completa de **rate limiting** y protección contra DDoS en aplicaciones ASP.NET Core.

---

## 📋 Índice

1. [Conceptos Fundamentales](#conceptos-fundamentales)
2. [Algoritmos de Rate Limiting](#algoritmos-de-rate-limiting)
3. [ASP.NET Core 7+ Built-in](#aspnet-core-7-built-in)
4. [AspNetCoreRateLimit Library](#aspnetcoreratelimit-library)
5. [Distributed Rate Limiting (Redis)](#distributed-rate-limiting-redis)
6. [DDoS Protection Patterns](#ddos-protection-patterns)
7. [Best Practices](#best-practices)

---

## 🎯 Conceptos Fundamentales

### ¿Qué es Rate Limiting?

**Rate limiting** es la técnica de controlar el número de requests que un cliente puede hacer a un API en un período de tiempo determinado.

### ¿Por qué Rate Limiting?

**Protección:**
- ✅ Prevenir DDoS (Distributed Denial of Service)
- ✅ Proteger contra brute-force attacks
- ✅ Evitar abuso de API

**Recursos:**
- ✅ Asegurar fair usage
- ✅ Controlar costos de infraestructura
- ✅ Mantener calidad de servicio (QoS)

### Estrategias Comunes

| Estrategia | Límite | Ejemplo |
|------------|--------|---------|
| **Per User** | Por usuario autenticado | 100 req/min por user |
| **Per IP** | Por dirección IP | 1000 req/hour por IP |
| **Per API Key** | Por clave de API | 10,000 req/day por key |
| **Global** | Para toda la aplicación | 100,000 req/min total |

---

## ⚙️ Algoritmos de Rate Limiting

### 1. Fixed Window

**Concepto:** Contador que se resetea cada ventana de tiempo fija.

```
Ventana 1 (0-60s): 0, 1, 2, 3, ..., 100 requests → LÍMITE
Ventana 2 (60-120s): 0, 1, 2, 3, ... → Contador reset
```

**Ventajas:**
- ✅ Simple de implementar
- ✅ Bajo uso de memoria

**Desventajas:**
- ❌ Burst al inicio de ventana
- ❌ Posible el doble de requests en límite de ventanas

**Ejemplo:**
- Límite: 100 req/min
- 11:00:50 → 100 requests (OK)
- 11:01:00 → reset
- 11:01:10 → 100 requests (OK)
- **Total en 20 segundos:** 200 requests (el doble del límite esperado)

### 2. Sliding Window

**Concepto:** Ventana que se mueve con el tiempo, contabilizando requests en los últimos N segundos.

```
t=0:   [----60s----] = 0 requests
t=10:  [--10s--|----50s----] = proporción ponderada
t=60:  [----60s----] = conteo exacto de últimos 60s
```

**Ventajas:**
- ✅ Más justo que fixed window
- ✅ Previene bursts en límites de ventanas

**Desventajas:**
- ❌ Más complejo de implementar
- ❌ Mayor uso de memoria

### 3. Token Bucket

**Concepto:** Bucket que se llena con tokens a rata constante. Cada request consume un token.

```
Bucket Capacity: 100 tokens
Refill Rate: 10 tokens/segundo

Request → consume 1 token
Bucket empty → Request denegado (429)
Bucket se rellena automáticamente
```

**Ventajas:**
- ✅ Permite bursts controlados
- ✅ Flexible para diferentes patrones de uso

**Desventajas:**
- ❌ Más complejo de implementar
- ❌ Configuración requiere tuning

### 4. Leaky Bucket

**Concepto:** Requests entran en bucket y salen a rata fija.

```
Requests → [Bucket] → Procesados a rata fija

Bucket lleno → Requests rechazados
```

**Ventajas:**
- ✅ Output predecible y constante

**Desventajas:**
- ❌ No permite bursts
- ❌ Puede causar latencia

---

## 🚀 ASP.NET Core 7+ Built-in

### Instalación

ASP.NET Core 7.0+ incluye rate limiting built-in (no requiere paquetes externos).

### Configuración Básica

**Program.cs:**
```csharp
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Agregar rate limiting
builder.Services.AddRateLimiter(options =>
{
    // 1. Fixed Window Limiter
    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });

    // 2. Sliding Window Limiter
    options.AddSlidingWindowLimiter("sliding", opt =>
    {
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.SegmentsPerWindow = 6;  // 6 segmentos de 10 segundos
    });

    // 3. Token Bucket Limiter
    options.AddTokenBucketLimiter("token", opt =>
    {
        opt.TokenLimit = 100;
        opt.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
        opt.TokensPerPeriod = 10;
        opt.AutoReplenishment = true;
    });

    // 4. Concurrency Limiter
    options.AddConcurrencyLimiter("concurrency", opt =>
    {
        opt.PermitLimit = 10;  // Máximo 10 requests concurrentes
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 5;
    });

    // Global limiter
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.User.Identity?.Name ?? context.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));

    // Mensaje de rechazo
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

var app = builder.Build();

// Activar rate limiting
app.UseRateLimiter();

app.MapControllers();
app.Run();
```

### Uso en Endpoints

**Attribute en controller:**
```csharp
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    [EnableRateLimiting("fixed")]  // ✅ Aplicar rate limiter específico
    public IActionResult GetOrders()
    {
        return Ok(new [] { "Order1", "Order2" });
    }

    [HttpPost]
    [EnableRateLimiting("token")]  // ✅ Diferente limiter para POST
    public IActionResult CreateOrder()
    {
        return Ok();
    }

    [HttpGet("unlimited")]
    [DisableRateLimiting]  // ✅ Deshabilitar para endpoint específico
    public IActionResult GetHealthCheck()
    {
        return Ok("Healthy");
    }
}
```

**Policy por usuario:**
```csharp
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("perUser", context =>
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "anonymous";

        return RateLimitPartition.GetFixedWindowLimiter(userId, partition =>
            new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            });
    });
});

// Uso
[EnableRateLimiting("perUser")]
```

---

## 📦 AspNetCoreRateLimit Library

### Instalación

```bash
dotnet add package AspNetCoreRateLimit
```

### Configuración

**appsettings.json:**
```json
{
  "IpRateLimiting": {
    "EnableEndpointRateLimiting": true,
    "StackBlockedRequests": false,
    "RealIpHeader": "X-Real-IP",
    "HttpStatusCode": 429,
    "GeneralRules": [
      {
        "Endpoint": "*",
        "Period": "1m",
        "Limit": 100
      },
      {
        "Endpoint": "*",
        "Period": "1h",
        "Limit": 1000
      }
    ]
  }
}
```

**Program.cs:**
```csharp
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Configuración
builder.Services.AddOptions();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(
    builder.Configuration.GetSection("IpRateLimiting"));

builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

var app = builder.Build();

// Middleware
app.UseIpRateLimiting();

app.MapControllers();
app.Run();
```

---

## 🌐 Distributed Rate Limiting (Redis)

### ¿Por qué Redis?

En arquitecturas con múltiples instancias (load balancing), necesitamos rate limiting **compartido** entre servidores.

### Implementación con Redis

```bash
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis
```

**Program.cs:**
```csharp
// Redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "RateLimiting_";
});

// Custom distributed rate limiter
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("distributed", context =>
    {
        var cache = context.RequestServices.GetRequiredService<IDistributedCache>();
        var userId = context.User.Identity?.Name ?? "anonymous";

        return RateLimitPartition.GetFixedWindowLimiter(userId, partition =>
            new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            });
    });
});
```

---

## 🛡️ DDoS Protection Patterns

### 1. Multi-Layer Rate Limiting

```csharp
// Layer 1: Global (toda la aplicación)
options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: "global",
        factory: _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10000,
            Window = TimeSpan.FromMinutes(1)
        }));

// Layer 2: Por IP
options.AddPolicy("perIP", context =>
    RateLimitPartition.GetFixedWindowLimiter(
        context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
        _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 100,
            Window = TimeSpan.FromMinutes(1)
        }));

// Layer 3: Por usuario autenticado
options.AddPolicy("perUser", context =>
    RateLimitPartition.GetTokenBucketLimiter(
        context.User.Identity?.Name ?? "anonymous",
        _ => new TokenBucketLimiterOptions
        {
            TokenLimit = 1000,
            TokensPerPeriod = 100,
            ReplenishmentPeriod = TimeSpan.FromMinutes(1)
        }));
```

### 2. Tiered Limits (Premium vs Free)

```csharp
options.AddPolicy("tiered", context =>
{
    var subscription = context.User.FindFirst("subscription")?.Value ?? "free";

    var limits = subscription switch
    {
        "premium" => new { Limit = 1000, Window = TimeSpan.FromMinutes(1) },
        "pro" => new { Limit = 500, Window = TimeSpan.FromMinutes(1) },
        _ => new { Limit = 100, Window = TimeSpan.FromMinutes(1) }
    };

    return RateLimitPartition.GetFixedWindowLimiter(
        context.User.Identity?.Name ?? "anonymous",
        _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = limits.Limit,
            Window = limits.Window
        });
});
```

### 3. Adaptive Rate Limiting

```csharp
// Ajustar límites según carga del sistema
options.AddPolicy("adaptive", context =>
{
    var metrics = context.RequestServices.GetRequiredService<ISystemMetrics>();
    var cpuUsage = metrics.GetCpuUsage();

    var limit = cpuUsage switch
    {
        > 80 => 50,   // CPU alta → límite bajo
        > 60 => 100,
        _ => 200      // CPU normal → límite alto
    };

    return RateLimitPartition.GetFixedWindowLimiter(
        context.User.Identity?.Name ?? "anonymous",
        _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = limit,
            Window = TimeSpan.FromMinutes(1)
        });
});
```

---

## ✅ Best Practices

### 1. Headers Informativos

```csharp
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 429)
    {
        context.Response.Headers["Retry-After"] = "60";  // Reintentar en 60 segundos
        context.Response.Headers["X-RateLimit-Limit"] = "100";
        context.Response.Headers["X-RateLimit-Remaining"] = "0";
        context.Response.Headers["X-RateLimit-Reset"] = DateTimeOffset.UtcNow.AddMinutes(1).ToUnixTimeSeconds().ToString();
    }
});
```

### 2. Mensajes Claros

```csharp
options.OnRejected = async (context, cancellationToken) =>
{
    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
    await context.HttpContext.Response.WriteAsJsonAsync(new
    {
        error = "Rate limit exceeded",
        message = "You have exceeded the maximum number of requests. Please try again later.",
        retryAfter = 60
    }, cancellationToken);
};
```

### 3. Logging y Monitoring

```csharp
options.OnRejected = async (context, cancellationToken) =>
{
    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
    var user = context.HttpContext.User.Identity?.Name ?? "anonymous";
    var ip = context.HttpContext.Connection.RemoteIpAddress;

    logger.LogWarning(
        "Rate limit exceeded for user {User} from IP {IP}",
        user, ip);

    await context.HttpContext.Response.WriteAsJsonAsync(new
    {
        error = "Rate limit exceeded"
    }, cancellationToken);
};
```

### 4. Whitelist para Servicios Confiables

```csharp
options.AddPolicy("whitelist", context =>
{
    var trustedIPs = new[] { "192.168.1.100", "10.0.0.1" };
    var clientIP = context.Connection.RemoteIpAddress?.ToString();

    if (trustedIPs.Contains(clientIP))
    {
        // Sin límites para IPs confiables
        return RateLimitPartition.GetNoLimiter("trusted");
    }

    return RateLimitPartition.GetFixedWindowLimiter(
        clientIP ?? "unknown",
        _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 100,
            Window = TimeSpan.FromMinutes(1)
        });
});
```

---

## 📚 Recursos

### Documentación
- **ASP.NET Core Rate Limiting:** https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit
- **AspNetCoreRateLimit:** https://github.com/stefanprodan/AspNetCoreRateLimit

### Algoritmos
- **Token Bucket:** https://en.wikipedia.org/wiki/Token_bucket
- **Leaky Bucket:** https://en.wikipedia.org/wiki/Leaky_bucket

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-22
**Mantenido por:** mjcuadrado-net-sdk
