---
skill: serilog
description: Serilog para structured logging en .NET con integración OpenTelemetry
category: tools
tags: [logging, structured-logging, observability, serilog, dotnet]
version: 1.0.0
dotnet_version: "8.0+"
related_skills: [opentelemetry, grafana, loki, docker-compose]
---

# Serilog - Structured Logging para .NET

Serilog es una librería de logging estructurado para .NET que permite capturar eventos como objetos estructurados en lugar de texto plano.

---

## 📋 Tabla de Contenidos

1. [Conceptos Básicos](#conceptos-básicos)
2. [Instalación](#instalación)
3. [Configuración](#configuración)
4. [Sinks](#sinks)
5. [Enrichers](#enrichers)
6. [Structured Logging](#structured-logging)
7. [Integración con OpenTelemetry](#integración-con-opentelemetry)
8. [Best Practices](#best-practices)
9. [Troubleshooting](#troubleshooting)

---

## Conceptos Básicos

### ¿Por qué Structured Logging?

**Logging tradicional:**
```csharp
logger.LogInformation($"User {userId} created order {orderId} with total {total}");
// Output: "User 123 created order ORD-456 with total 99.99"
```

**Structured logging:**
```csharp
logger.LogInformation("User {UserId} created order {OrderId} with total {Total}",
    userId, orderId, total);
// Output (JSON):
{
  "message": "User 123 created order ORD-456 with total 99.99",
  "UserId": 123,
  "OrderId": "ORD-456",
  "Total": 99.99,
  "timestamp": "2025-11-22T10:30:00Z"
}
```

**Ventajas:**
- ✅ Queryable (búsquedas eficientes)
- ✅ Parseable (análisis automático)
- ✅ Correlación con traces
- ✅ Dashboards y alertas

---

## Instalación

### Paquetes NuGet Esenciales

```bash
# Core
dotnet add package Serilog
dotnet add package Serilog.AspNetCore

# Sinks
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Sinks.Seq
dotnet add package Serilog.Sinks.Grafana.Loki

# Enrichers
dotnet add package Serilog.Enrichers.Environment
dotnet add package Serilog.Enrichers.Thread
dotnet add package Serilog.Enrichers.Process

# Integración OpenTelemetry
dotnet add package OpenTelemetry.Logs
```

### Versiones Recomendadas

```xml
<ItemGroup>
  <!-- Serilog Core -->
  <PackageReference Include="Serilog" Version="3.1.1" />
  <PackageReference Include="Serilog.AspNetCore" Version="8.0.0" />

  <!-- Sinks -->
  <PackageReference Include="Serilog.Sinks.Console" Version="5.0.1" />
  <PackageReference Include="Serilog.Sinks.File" Version="5.0.0" />
  <PackageReference Include="Serilog.Sinks.Seq" Version="6.0.0" />
  <PackageReference Include="Serilog.Sinks.Grafana.Loki" Version="8.3.0" />

  <!-- Enrichers -->
  <PackageReference Include="Serilog.Enrichers.Environment" Version="2.3.0" />
  <PackageReference Include="Serilog.Enrichers.Thread" Version="3.1.0" />
  <PackageReference Include="Serilog.Enrichers.Process" Version="2.0.2" />
  <PackageReference Include="Serilog.Enrichers.Span" Version="3.1.0" />
</ItemGroup>
```

---

## Configuración

### Setup Básico en ASP.NET Core

```csharp
// Program.cs
using Serilog;
using Serilog.Events;

// Configurar Serilog antes de builder.Build()
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .WriteTo.Console()
    .WriteTo.File("logs/app-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Usar Serilog como logger provider
    builder.Host.UseSerilog();

    var app = builder.Build();

    // Middleware de logging de requests
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers.UserAgent);
        };
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
```

### Configuración desde appsettings.json

```json
{
  "Serilog": {
    "Using": ["Serilog.Sinks.Console", "Serilog.Sinks.File"],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "theme": "Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console",
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/app-.txt",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 7,
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ],
    "Enrich": ["FromLogContext", "WithMachineName", "WithThreadId"]
  }
}
```

```csharp
// Program.cs
using Serilog;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
```

---

## Sinks

### Console Sink (Desarrollo)

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
        theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
    .CreateLogger();
```

### File Sink (Production)

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        path: "logs/app-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        fileSizeLimitBytes: 100_000_000,  // 100 MB
        rollOnFileSizeLimit: true,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
```

### Seq Sink (Structured Logs UI)

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();
```

**Docker Compose:**
```yaml
services:
  seq:
    image: datalust/seq:latest
    ports:
      - "5341:80"
    environment:
      - ACCEPT_EULA=Y
    volumes:
      - seq-data:/data

volumes:
  seq-data:
```

### Loki Sink (Grafana)

```csharp
using Serilog.Sinks.Grafana.Loki;

Log.Logger = new LoggerConfiguration()
    .WriteTo.GrafanaLoki(
        "http://localhost:3100",
        labels: new[]
        {
            new LokiLabel { Key = "app", Value = "mi-api" },
            new LokiLabel { Key = "environment", Value = "production" }
        },
        propertiesAsLabels: new[] { "level" })
    .CreateLogger();
```

### Sub-loggers (Conditional Logging)

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()  // Todos los logs
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(evt => evt.Level >= LogEventLevel.Error)
        .WriteTo.File("logs/errors-.txt", rollingInterval: RollingInterval.Day))  // Solo errores
    .CreateLogger();
```

---

## Enrichers

### Built-in Enrichers

```csharp
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()      // Propiedades del contexto
    .Enrich.WithMachineName()     // Nombre de la máquina
    .Enrich.WithEnvironmentName() // Environment (Development, Production)
    .Enrich.WithThreadId()        // ID del thread
    .Enrich.WithProcessId()       // ID del proceso
    .Enrich.WithProcessName()     // Nombre del proceso
    .CreateLogger();
```

### Custom Enricher

```csharp
public class RequestIdEnricher : ILogEventEnricher
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestIdEnricher(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            var requestId = httpContext.TraceIdentifier;
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("RequestId", requestId));
        }
    }
}

// Registrar enricher
Log.Logger = new LoggerConfiguration()
    .Enrich.With(new RequestIdEnricher(httpContextAccessor))
    .CreateLogger();
```

### LogContext (Scope Properties)

```csharp
using Serilog.Context;

public class OrderService
{
    private readonly ILogger<OrderService> _logger;

    public async Task ProcessOrderAsync(string orderId, string customerId)
    {
        // Agregar propiedades al contexto
        using (LogContext.PushProperty("OrderId", orderId))
        using (LogContext.PushProperty("CustomerId", customerId))
        {
            _logger.LogInformation("Processing order");
            // Todos los logs en este scope tendrán OrderId y CustomerId

            await ValidateOrderAsync();  // Log incluirá OrderId y CustomerId
            await SaveOrderAsync();      // Log incluirá OrderId y CustomerId
        }
    }
}
```

---

## Structured Logging

### Message Templates

```csharp
// ❌ String interpolation (NO estructurado)
logger.LogInformation($"User {userId} logged in");

// ✅ Message template (estructurado)
logger.LogInformation("User {UserId} logged in", userId);

// ✅ Múltiples propiedades
logger.LogInformation(
    "Order {OrderId} created by {CustomerId} with total {Total}",
    orderId, customerId, total);

// ✅ Con @ para serialización completa de objetos
logger.LogInformation("Created {@Order}", order);
```

### Log Levels

```csharp
// Verbose - Información muy detallada
logger.LogTrace("Entering method GetOrders with filter {Filter}", filter);

// Debug - Información de debugging
logger.LogDebug("Cache miss for key {CacheKey}", key);

// Information - Eventos normales
logger.LogInformation("Order {OrderId} created successfully", orderId);

// Warning - Eventos inesperados pero no críticos
logger.LogWarning("Order {OrderId} processing slow, elapsed {ElapsedMs}ms", orderId, elapsed);

// Error - Errores que pueden recuperarse
logger.LogError(ex, "Failed to process order {OrderId}", orderId);

// Fatal - Errores críticos que terminan la aplicación
logger.LogCritical(ex, "Database connection failed");
```

### Destructuring

```csharp
public class Order
{
    public string Id { get; set; }
    public decimal Total { get; set; }
    public List<OrderItem> Items { get; set; }
}

// @ para destructurar objetos completos
logger.LogInformation("Created {@Order}", order);
// Output:
{
  "Order": {
    "Id": "ORD-123",
    "Total": 99.99,
    "Items": [...]
  }
}

// Sin @ solo muestra ToString()
logger.LogInformation("Created {Order}", order);
// Output: "Created MyNamespace.Order"
```

---

## Integración con OpenTelemetry

### Configuración Completa

```csharp
using OpenTelemetry;
using OpenTelemetry.Logs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.WithSpan()  // Agregar trace_id y span_id automáticamente
    .WriteTo.Console()
    .WriteTo.File("logs/app-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Configurar OpenTelemetry Logging
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeScopes = true;
    logging.IncludeFormattedMessage = true;
    logging.ParseStateValues = true;

    logging.AddOtlpExporter(options =>
    {
        options.Endpoint = new Uri("http://localhost:4317");
    });
});

var app = builder.Build();
app.Run();
```

### Correlación Automática

Cuando OpenTelemetry está configurado, los logs se correlacionan automáticamente:

```json
{
  "timestamp": "2025-11-22T10:30:00Z",
  "level": "Information",
  "message": "Processing order ORD-123",
  "trace_id": "4bf92f3577b34da6a3ce929d0e0e4736",  // ✅ Correlación
  "span_id": "00f067aa0ba902b7",                  // ✅ Correlación
  "OrderId": "ORD-123"
}
```

---

## Best Practices

### 1. Message Templates Consistentes

```csharp
// ✅ HACER: Templates consistentes
logger.LogInformation("Order {OrderId} created by customer {CustomerId}", orderId, customerId);

// ❌ EVITAR: String interpolation
logger.LogInformation($"Order {orderId} created by customer {customerId}");

// ❌ EVITAR: Concatenación
logger.LogInformation("Order " + orderId + " created");
```

### 2. Propiedades Estructuradas

```csharp
// ✅ HACER: Propiedades separadas
logger.LogInformation(
    "Payment processed for order {OrderId} with amount {Amount} and method {PaymentMethod}",
    orderId, amount, paymentMethod);

// ❌ EVITAR: Todo en un string
logger.LogInformation("Payment processed: " + orderId + " - " + amount + " - " + method);
```

### 3. Niveles de Log Apropiados

```csharp
// ✅ HACER
logger.LogInformation("Order created");           // Eventos normales
logger.LogWarning("Slow response time");          // Inesperado pero no crítico
logger.LogError(ex, "Failed to save order");      // Error recuperable

// ❌ EVITAR
logger.LogInformation("Entering method");         // Muy verboso
logger.LogError("User not found");                // No es error, es validación
```

### 4. Excepciones

```csharp
// ✅ HACER: Pasar excepción como primer parámetro
try
{
    await ProcessOrderAsync(orderId);
}
catch (Exception ex)
{
    logger.LogError(ex, "Failed to process order {OrderId}", orderId);
    throw;
}

// ❌ EVITAR: Excepción en message template
logger.LogError("Error: {Exception}", ex.Message);  // Pierde stack trace
```

### 5. Secrets y PII

```csharp
// ❌ EVITAR: Logging de secrets
logger.LogInformation("Password: {Password}", password);
logger.LogInformation("Credit card: {CreditCard}", creditCard);

// ✅ HACER: No loggear información sensible
logger.LogInformation("User {UserId} authenticated", userId);
logger.LogInformation("Payment processed with masked card ****{Last4Digits}", last4);
```

---

## Troubleshooting

### Logs no aparecen

**Problema:** No se ven logs
**Solución:**
```csharp
// Verificar nivel de log
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()  // Bajar a Debug temporalmente
    .WriteTo.Console()
    .CreateLogger();
```

### Archivo de logs muy grande

**Problema:** logs/app.txt crece demasiado
**Solución:**
```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        "logs/app-.txt",
        rollingInterval: RollingInterval.Day,     // Rotar diariamente
        retainedFileCountLimit: 7,                // Mantener solo 7 días
        fileSizeLimitBytes: 100_000_000,          // 100 MB max por archivo
        rollOnFileSizeLimit: true)                // Rotar si excede tamaño
    .CreateLogger();
```

### Formato incorrecto en JSON

**Problema:** JSON malformado
**Solución:**
```csharp
// Usar formatter específico
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(new JsonFormatter(), "logs/app.json")
    .CreateLogger();
```

---

## Recursos Adicionales

- [Serilog Documentation](https://serilog.net/)
- [Serilog Best Practices](https://benfoster.io/blog/serilog-best-practices/)
- [Structured Logging Guide](https://messagetemplates.org/)
- [Serilog Sinks](https://github.com/serilog/serilog/wiki/Provided-Sinks)

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-22
**Mantenido por:** mjcuadrado-net-sdk
