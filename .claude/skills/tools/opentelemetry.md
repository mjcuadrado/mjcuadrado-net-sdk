---
skill: opentelemetry
description: OpenTelemetry para observabilidad completa (traces, metrics, logs) en .NET
category: tools
tags: [observability, monitoring, tracing, metrics, logging, opentelemetry]
version: 1.0.0
dotnet_version: "8.0+"
related_skills: [serilog, grafana, docker, docker-compose]
---

# OpenTelemetry - Observabilidad Completa para .NET

OpenTelemetry es un framework de observabilidad open-source que proporciona APIs, SDKs y herramientas para instrumentar, generar, recolectar y exportar datos de telemetría (traces, metrics, logs).

---

## 📋 Tabla de Contenidos

1. [Conceptos Básicos](#conceptos-básicos)
2. [Instalación](#instalación)
3. [Configuración](#configuración)
4. [Traces (Trazas)](#traces-trazas)
5. [Metrics (Métricas)](#metrics-métricas)
6. [Logs (Registros)](#logs-registros)
7. [Exporters](#exporters)
8. [Collector](#collector)
9. [Best Practices](#best-practices)
10. [Troubleshooting](#troubleshooting)

---

## Conceptos Básicos

### ¿Qué es OpenTelemetry?

OpenTelemetry (OTel) unifica 3 pilares de observabilidad:

```
┌─────────────┬─────────────┬─────────────┐
│   TRACES    │   METRICS   │    LOGS     │
├─────────────┼─────────────┼─────────────┤
│ Qué pasó    │ Cuánto pasó │ Por qué pasó│
│ Requests    │ Contadores  │ Eventos     │
│ Latencia    │ Histogramas │ Errors      │
│ Spans       │ Gauges      │ Context     │
└─────────────┴─────────────┴─────────────┘
```

### Componentes Principales

**1. API:** Interfaces para instrumentación
**2. SDK:** Implementación de la API
**3. Instrumentación:** Librerías auto-instrumentación
**4. Exporters:** Envío de datos a backends
**5. Collector:** Agregación y procesamiento

---

## Instalación

### Paquetes NuGet Esenciales

```bash
# SDK Core
dotnet add package OpenTelemetry
dotnet add package OpenTelemetry.Extensions.Hosting

# Instrumentation automática
dotnet add package OpenTelemetry.Instrumentation.AspNetCore
dotnet add package OpenTelemetry.Instrumentation.Http
dotnet add package OpenTelemetry.Instrumentation.SqlClient

# Exporters
dotnet add package OpenTelemetry.Exporter.Console
dotnet add package OpenTelemetry.Exporter.Jaeger
dotnet add package OpenTelemetry.Exporter.Prometheus.AspNetCore
dotnet add package OpenTelemetry.Exporter.OpenTelemetryProtocol  # OTLP
```

### Versiones Recomendadas

```xml
<ItemGroup>
  <!-- OpenTelemetry Core -->
  <PackageReference Include="OpenTelemetry" Version="1.7.0" />
  <PackageReference Include="OpenTelemetry.Extensions.Hosting" Version="1.7.0" />

  <!-- Instrumentation -->
  <PackageReference Include="OpenTelemetry.Instrumentation.AspNetCore" Version="1.7.1" />
  <PackageReference Include="OpenTelemetry.Instrumentation.Http" Version="1.7.1" />
  <PackageReference Include="OpenTelemetry.Instrumentation.EntityFrameworkCore" Version="1.0.0-beta.9" />

  <!-- Exporters -->
  <PackageReference Include="OpenTelemetry.Exporter.Console" Version="1.7.0" />
  <PackageReference Include="OpenTelemetry.Exporter.Jaeger" Version="1.5.1" />
  <PackageReference Include="OpenTelemetry.Exporter.Prometheus.AspNetCore" Version="1.7.0-rc.1" />
  <PackageReference Include="OpenTelemetry.Exporter.OpenTelemetryProtocol" Version="1.7.0" />
</ItemGroup>
```

---

## Configuración

### Setup Básico en ASP.NET Core

```csharp
// Program.cs
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Configurar OpenTelemetry
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(
            serviceName: "mi-api",
            serviceVersion: "1.0.0",
            serviceInstanceId: Environment.MachineName))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddSqlClientInstrumentation()
        .AddConsoleExporter()
        .AddJaegerExporter())
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddConsoleExporter()
        .AddPrometheusExporter());

var app = builder.Build();

// Endpoint de métricas para Prometheus
app.MapPrometheusScrapingEndpoint();

app.Run();
```

### Configuración desde appsettings.json

```json
{
  "OpenTelemetry": {
    "ServiceName": "mi-api",
    "ServiceVersion": "1.0.0",
    "Jaeger": {
      "AgentHost": "localhost",
      "AgentPort": 6831,
      "MaxPacketSize": 65000
    },
    "OTLP": {
      "Endpoint": "http://localhost:4317",
      "Protocol": "grpc"
    },
    "Sampling": {
      "Type": "probability",
      "Probability": 1.0
    }
  }
}
```

```csharp
// Leer configuración
var otelConfig = builder.Configuration.GetSection("OpenTelemetry");

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(
            serviceName: otelConfig["ServiceName"],
            serviceVersion: otelConfig["ServiceVersion"]))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddJaegerExporter(options =>
        {
            options.AgentHost = otelConfig["Jaeger:AgentHost"];
            options.AgentPort = int.Parse(otelConfig["Jaeger:AgentPort"]);
        }));
```

---

## Traces (Trazas)

### Auto-Instrumentación

```csharp
// ASP.NET Core auto-instrumentación
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation(options =>
        {
            // Filtrar requests
            options.Filter = httpContext =>
                !httpContext.Request.Path.Value.Contains("/health");

            // Enriquecer spans
            options.Enrich = (activity, eventName, rawObject) =>
            {
                if (eventName == "OnStartActivity")
                {
                    if (rawObject is HttpRequest request)
                    {
                        activity.SetTag("http.client_ip", request.HttpContext.Connection.RemoteIpAddress);
                        activity.SetTag("http.user_agent", request.Headers.UserAgent.ToString());
                    }
                }
            };

            // Grabar excepciones
            options.RecordException = true;
        }));
```

### Instrumentación Manual

```csharp
using System.Diagnostics;
using OpenTelemetry;

public class OrderService
{
    private static readonly ActivitySource ActivitySource = new("MyApp.OrderService");

    public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
    {
        // Crear span manual
        using var activity = ActivitySource.StartActivity("CreateOrder", ActivityKind.Internal);

        // Agregar tags (atributos)
        activity?.SetTag("order.id", request.OrderId);
        activity?.SetTag("order.total", request.Total);
        activity?.SetTag("customer.id", request.CustomerId);

        try
        {
            // Validar order
            using (var validateActivity = ActivitySource.StartActivity("ValidateOrder"))
            {
                validateActivity?.SetTag("validation.step", "inventory");
                await ValidateInventoryAsync(request);
            }

            // Crear order
            var order = await _repository.CreateAsync(request);

            // Agregar evento
            activity?.AddEvent(new ActivityEvent("Order created successfully"));

            return order;
        }
        catch (Exception ex)
        {
            // Grabar excepción
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            activity?.RecordException(ex);
            throw;
        }
    }
}
```

### Registro de ActivitySource

```csharp
// Program.cs
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddSource("MyApp.OrderService")
        .AddSource("MyApp.PaymentService")
        .AddSource("MyApp.*"));  // Wildcard
```

### Context Propagation

```csharp
// Propagar contexto entre servicios
using var client = new HttpClient();

// El contexto se propaga automáticamente en headers (W3C Trace Context)
// traceparent: 00-{trace-id}-{span-id}-{trace-flags}
var response = await client.GetAsync("https://downstream-service/api/orders");
```

---

## Metrics (Métricas)

### Tipos de Métricas

```csharp
using System.Diagnostics.Metrics;

public class OrderMetrics
{
    private static readonly Meter Meter = new("MyApp.Orders", "1.0.0");

    // 1. Counter - Solo incrementa
    private static readonly Counter<long> OrdersCreated = Meter.CreateCounter<long>(
        "orders.created",
        description: "Total de órdenes creadas");

    // 2. Histogram - Distribución de valores
    private static readonly Histogram<double> OrderProcessingDuration = Meter.CreateHistogram<double>(
        "orders.processing.duration",
        unit: "ms",
        description: "Duración de procesamiento de órdenes");

    // 3. ObservableGauge - Valor actual
    private static readonly ObservableGauge<int> ActiveOrders = Meter.CreateObservableGauge(
        "orders.active",
        () => GetActiveOrdersCount(),
        description: "Órdenes activas actualmente");

    // 4. UpDownCounter - Incrementa/Decrementa
    private static readonly UpDownCounter<int> OrdersInProgress = Meter.CreateUpDownCounter<int>(
        "orders.in_progress",
        description: "Órdenes en progreso");

    public async Task<Order> ProcessOrderAsync(CreateOrderRequest request)
    {
        var sw = Stopwatch.StartNew();
        OrdersInProgress.Add(1);

        try
        {
            var order = await CreateOrderAsync(request);

            // Incrementar counter con tags
            OrdersCreated.Add(1, new KeyValuePair<string, object?>("status", "success"));

            return order;
        }
        catch (Exception ex)
        {
            OrdersCreated.Add(1, new KeyValuePair<string, object?>("status", "failed"));
            throw;
        }
        finally
        {
            OrdersInProgress.Add(-1);
            OrderProcessingDuration.Record(sw.ElapsedMilliseconds);
        }
    }

    private static int GetActiveOrdersCount()
    {
        // Lógica para obtener órdenes activas
        return 42;
    }
}
```

### Registro de Meters

```csharp
// Program.cs
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics => metrics
        .AddMeter("MyApp.Orders")
        .AddMeter("MyApp.Payments")
        .AddMeter("MyApp.*")  // Wildcard
        .AddAspNetCoreInstrumentation()  // Métricas HTTP automáticas
        .AddRuntimeInstrumentation()     // Métricas de runtime (.NET)
        .AddProcessInstrumentation());   // Métricas de proceso (CPU, memoria)
```

### Métricas ASP.NET Core Automáticas

```
# Requests
http.server.request.duration  # Histogram
http.server.active_requests   # UpDownCounter

# Métricas disponibles:
- request count
- request duration
- active requests
- response status codes
```

---

## Logs (Registros)

### Integración con ILogger

```csharp
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;

// Program.cs
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeScopes = true;
    logging.IncludeFormattedMessage = true;
    logging.ParseStateValues = true;

    logging.AddConsoleExporter();
    logging.AddOtlpExporter();
});

// Uso en código
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        // Log con structured data
        _logger.LogInformation(
            "Creating order {OrderId} for customer {CustomerId} with total {Total}",
            request.OrderId,
            request.CustomerId,
            request.Total);

        // Los logs se correlacionan automáticamente con traces
        // El trace_id y span_id se agregan automáticamente

        return Ok();
    }
}
```

### Correlación Automática

Cuando OpenTelemetry está configurado, los logs se correlacionan automáticamente con traces:

```json
{
  "timestamp": "2025-11-22T10:30:00Z",
  "level": "Information",
  "message": "Creating order ORD-123",
  "trace_id": "4bf92f3577b34da6a3ce929d0e0e4736",
  "span_id": "00f067aa0ba902b7",
  "service.name": "mi-api",
  "OrderId": "ORD-123",
  "CustomerId": "CUST-456"
}
```

---

## Exporters

### Console Exporter (Desarrollo)

```csharp
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddConsoleExporter(options =>
        {
            options.Targets = ConsoleExporterOutputTargets.Console;
        }));
```

### Jaeger Exporter (Traces)

```csharp
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddJaegerExporter(options =>
        {
            options.AgentHost = "localhost";
            options.AgentPort = 6831;
            options.MaxPayloadSizeInBytes = 65000;
        }));
```

**Docker Compose:**
```yaml
services:
  jaeger:
    image: jaegertracing/all-in-one:latest
    ports:
      - "6831:6831/udp"  # Agent
      - "16686:16686"    # UI
    environment:
      - COLLECTOR_ZIPKIN_HOST_PORT=:9411
```

### Prometheus Exporter (Metrics)

```csharp
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics => metrics
        .AddPrometheusExporter());

var app = builder.Build();

// Endpoint /metrics para Prometheus scraping
app.MapPrometheusScrapingEndpoint();
```

**prometheus.yml:**
```yaml
scrape_configs:
  - job_name: 'mi-api'
    static_configs:
      - targets: ['localhost:5000']
    scrape_interval: 15s
    metrics_path: '/metrics'
```

### OTLP Exporter (Traces, Metrics, Logs)

```csharp
// Exportar a OpenTelemetry Collector
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri("http://localhost:4317");
            options.Protocol = OtlpExportProtocol.Grpc;
        }))
    .WithMetrics(metrics => metrics
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri("http://localhost:4317");
        }));

builder.Logging.AddOpenTelemetry(logging =>
    logging.AddOtlpExporter(options =>
    {
        options.Endpoint = new Uri("http://localhost:4317");
    }));
```

---

## Collector

### ¿Qué es el Collector?

El OpenTelemetry Collector es un proxy que recibe, procesa y exporta datos de telemetría.

```
┌─────────┐     ┌───────────┐     ┌─────────┐
│  App 1  │────▶│           │────▶│ Jaeger  │
├─────────┤     │ Collector │     ├─────────┤
│  App 2  │────▶│           │────▶│Promethe │
├─────────┤     │  (OTLP)   │     ├─────────┤
│  App 3  │────▶│           │────▶│  Loki   │
└─────────┘     └───────────┘     └─────────┘
```

### Configuración del Collector

**otel-collector-config.yaml:**
```yaml
receivers:
  otlp:
    protocols:
      grpc:
        endpoint: 0.0.0.0:4317
      http:
        endpoint: 0.0.0.0:4318

processors:
  batch:
    timeout: 10s
    send_batch_size: 1024

  memory_limiter:
    check_interval: 1s
    limit_mib: 512

exporters:
  jaeger:
    endpoint: jaeger:14250
    tls:
      insecure: true

  prometheus:
    endpoint: "0.0.0.0:8889"

  loki:
    endpoint: http://loki:3100/loki/api/v1/push

service:
  pipelines:
    traces:
      receivers: [otlp]
      processors: [memory_limiter, batch]
      exporters: [jaeger]

    metrics:
      receivers: [otlp]
      processors: [memory_limiter, batch]
      exporters: [prometheus]

    logs:
      receivers: [otlp]
      processors: [memory_limiter, batch]
      exporters: [loki]
```

### Docker Compose con Collector

```yaml
services:
  # OpenTelemetry Collector
  otel-collector:
    image: otel/opentelemetry-collector-contrib:latest
    command: ["--config=/etc/otel-collector-config.yaml"]
    volumes:
      - ./otel-collector-config.yaml:/etc/otel-collector-config.yaml
    ports:
      - "4317:4317"   # OTLP gRPC
      - "4318:4318"   # OTLP HTTP
      - "8889:8889"   # Prometheus exporter
    depends_on:
      - jaeger
      - prometheus
      - loki

  # Jaeger (Traces)
  jaeger:
    image: jaegertracing/all-in-one:latest
    ports:
      - "16686:16686"  # UI
      - "14250:14250"  # gRPC

  # Prometheus (Metrics)
  prometheus:
    image: prom/prometheus:latest
    volumes:
      - ./prometheus.yml:/etc/prometheus/prometheus.yml
    ports:
      - "9090:9090"

  # Loki (Logs)
  loki:
    image: grafana/loki:latest
    ports:
      - "3100:3100"

  # Grafana (Visualización)
  grafana:
    image: grafana/grafana:latest
    ports:
      - "3000:3000"
    environment:
      - GF_AUTH_ANONYMOUS_ENABLED=true
      - GF_AUTH_ANONYMOUS_ORG_ROLE=Admin
```

---

## Best Practices

### 1. Sampling

```csharp
// No grabar todos los traces en producción
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .SetSampler(new TraceIdRatioBasedSampler(0.1))  // 10% de traces
        .AddAspNetCoreInstrumentation());

// Sampling condicional
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .SetSampler(new ParentBasedSampler(
            new AlwaysOnSampler()))  // Si parent está sampled, grabar
        .AddAspNetCoreInstrumentation());
```

### 2. Resource Attributes

```csharp
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService("mi-api", "1.0.0")
        .AddAttributes(new Dictionary<string, object>
        {
            ["deployment.environment"] = builder.Environment.EnvironmentName,
            ["service.namespace"] = "production",
            ["service.instance.id"] = Environment.MachineName,
            ["host.name"] = Environment.MachineName
        }));
```

### 3. Semantic Conventions

```csharp
// Usar semantic conventions de OpenTelemetry
using OpenTelemetry.Trace;

activity?.SetTag("http.method", "POST");
activity?.SetTag("http.url", "https://api.example.com/orders");
activity?.SetTag("http.status_code", 200);
activity?.SetTag("db.system", "postgresql");
activity?.SetTag("db.statement", "SELECT * FROM orders");
```

### 4. Cardinality Control

```csharp
// ❌ EVITAR alta cardinalidad
activity?.SetTag("user.id", userId);  // Millones de valores únicos

// ✅ USAR baja cardinalidad
activity?.SetTag("user.type", "premium");  // Pocos valores
```

### 5. Error Handling

```csharp
try
{
    await ProcessOrderAsync(order);
    activity?.SetStatus(ActivityStatusCode.Ok);
}
catch (ValidationException ex)
{
    activity?.SetStatus(ActivityStatusCode.Error, "Validation failed");
    activity?.SetTag("error.type", "validation");
    activity?.RecordException(ex);
    throw;
}
```

---

## Troubleshooting

### Verificar que OpenTelemetry está funcionando

```csharp
// Agregar Console exporter temporalmente
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddConsoleExporter());  // Ver traces en consola
```

### Problema: No aparecen traces

**Causa:** ActivitySource no registrado

```csharp
// Verificar que el ActivitySource está registrado
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddSource("MyApp.*"));  // Asegurar que coincide con el nombre
```

### Problema: Métricas no se exportan

**Causa:** No hay endpoint de Prometheus

```csharp
// Agregar endpoint
app.MapPrometheusScrapingEndpoint();

// Verificar en http://localhost:5000/metrics
```

### Problema: Demasiados datos

**Solución:** Implementar sampling

```csharp
// Solo 10% de traces
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .SetSampler(new TraceIdRatioBasedSampler(0.1)));
```

---

## Recursos Adicionales

- [OpenTelemetry .NET Docs](https://opentelemetry.io/docs/instrumentation/net/)
- [Semantic Conventions](https://opentelemetry.io/docs/specs/semconv/)
- [Collector Configuration](https://opentelemetry.io/docs/collector/configuration/)
- [Best Practices](https://opentelemetry.io/docs/concepts/instrumentation/manual-instrumentation/)

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-22
**Mantenido por:** mjcuadrado-net-sdk
