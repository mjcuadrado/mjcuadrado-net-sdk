# Issue #37: OpenTelemetry Stack (Observability)

**Status:** ✅ Completed
**Priority:** 🟡 High
**Version:** v0.3.0
**Created:** 2025-11-22
**Completed:** 2025-11-22

---

## 📋 Descripción

Se ha completado el stack completo de **Observability** con OpenTelemetry, Grafana y Serilog, proporcionando capacidades completas de monitoring, alerting y análisis de logs/metrics/traces para aplicaciones .NET.

---

## 🎯 Objetivos

Implementar stack completo de observabilidad:

1. ✅ **opentelemetry.md Skill** - Instrumentation completa (traces, metrics, logs)
2. ✅ **grafana.md Skill** - Dashboards, alerting y visualización
3. ✅ **serilog.md Skill** - Structured logging con integración OTel

---

## 📦 Archivos Creados

### 1. opentelemetry.md (434 líneas)

**Ubicación:** `.claude/skills/tools/opentelemetry.md`

**Contenido:**
- Conceptos básicos (traces, metrics, logs)
- Instalación y configuración para .NET
- Auto-instrumentación (ASP.NET Core, HTTP, SQL)
- Instrumentación manual (ActivitySource, Meters)
- Traces con spans, tags y eventos
- Metrics (Counter, Histogram, Gauge, UpDownCounter)
- Logs con correlación automática
- Exporters (Console, Jaeger, Prometheus, OTLP)
- OpenTelemetry Collector configuration
- Sampling strategies
- Best practices y troubleshooting

**Conceptos clave:**

```csharp
// Setup completo
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService("mi-api", "1.0.0"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddJaegerExporter())
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddPrometheusExporter());
```

**Traces - Instrumentación manual:**

```csharp
using var activity = ActivitySource.StartActivity("CreateOrder");
activity?.SetTag("order.id", request.OrderId);
activity?.SetTag("order.total", request.Total);
activity?.RecordException(ex);
```

**Metrics - 4 tipos:**

```csharp
Counter<long> OrdersCreated = Meter.CreateCounter<long>("orders.created");
Histogram<double> Duration = Meter.CreateHistogram<double>("orders.duration");
ObservableGauge<int> Active = Meter.CreateObservableGauge("orders.active", () => GetCount());
UpDownCounter<int> InProgress = Meter.CreateUpDownCounter<int>("orders.in_progress");
```

**OpenTelemetry Collector:**

```yaml
receivers:
  otlp:
    protocols:
      grpc:
        endpoint: 0.0.0.0:4317

processors:
  batch:
    timeout: 10s

exporters:
  jaeger:
    endpoint: jaeger:14250
  prometheus:
    endpoint: "0.0.0.0:8889"
  loki:
    endpoint: http://loki:3100

service:
  pipelines:
    traces:
      receivers: [otlp]
      processors: [batch]
      exporters: [jaeger]
```

### 2. grafana.md (365 líneas)

**Ubicación:** `.claude/skills/tools/grafana.md`

**Contenido:**
- Conceptos básicos de Grafana
- Instalación con Docker Compose
- Data sources (Prometheus, Loki, Jaeger)
- Provisioning de data sources y dashboards
- Dashboards creation y panels
- Query builder (PromQL, LogQL, Jaeger)
- Alerting rules y contact points
- Notification policies
- Variables y templating
- Tipos de panels (Time Series, Stat, Gauge, Table, Logs)
- Best practices y troubleshooting

**Provisioning de Data Sources:**

```yaml
apiVersion: 1

datasources:
  - name: Prometheus
    type: prometheus
    url: http://prometheus:9090
    isDefault: true
    editable: false

  - name: Loki
    type: loki
    url: http://loki:3100
    jsonData:
      derivedFields:
        - datasourceUid: Jaeger
          matcherRegex: "trace_id=(\\w+)"

  - name: Jaeger
    type: jaeger
    url: http://jaeger:16686
    jsonData:
      tracesToLogsV2:
        datasourceUid: Loki
```

**Dashboard JSON:**

```json
{
  "dashboard": {
    "title": ".NET API Dashboard",
    "panels": [
      {
        "title": "Request Rate (req/s)",
        "type": "graph",
        "targets": [
          {
            "expr": "rate(http_server_request_duration_count[5m])",
            "legendFormat": "{{method}} {{route}}"
          }
        ]
      }
    ]
  }
}
```

**Alerting Rules:**

```yaml
groups:
  - name: API Alerts
    rules:
      - alert: HighErrorRate
        expr: |
          (sum(rate(http_server_request_duration_count{http_status_code=~"5.."}[5m]))
           / sum(rate(http_server_request_duration_count[5m]))) > 0.05
        for: 5m
        labels:
          severity: critical
        annotations:
          summary: "High error rate detected"

      - alert: HighLatency
        expr: histogram_quantile(0.95, rate(http_server_request_duration_bucket[5m])) > 1000
        for: 5m
        labels:
          severity: warning
```

**PromQL Queries:**

```promql
# Request rate
rate(http_server_request_duration_count[5m])

# P95 latency
histogram_quantile(0.95, rate(http_server_request_duration_bucket[5m]))

# Error rate (%)
sum(rate(http_server_request_duration_count{http_status_code=~"5.."}[5m]))
/ sum(rate(http_server_request_duration_count[5m])) * 100
```

**LogQL Queries:**

```logql
# Todos los logs
{job="mi-api"}

# Logs de error
{job="mi-api"} | json | level="Error"

# Rate de errores
rate({job="mi-api"} |= "error" [5m])
```

### 3. serilog.md (318 líneas)

**Ubicación:** `.claude/skills/tools/serilog.md`

**Contenido:**
- Conceptos de structured logging
- Instalación de Serilog para .NET
- Configuración básica y desde appsettings.json
- Sinks (Console, File, Seq, Loki)
- Enrichers (built-in y custom)
- LogContext para scope properties
- Message templates y destructuring
- Log levels apropiados
- Integración con OpenTelemetry
- Correlación automática con traces
- Best practices (secrets, PII, cardinality)
- Troubleshooting

**Setup Básico:**

```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .WriteTo.Console()
    .WriteTo.File("logs/app-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
```

**Structured Logging:**

```csharp
// ❌ String interpolation (NO estructurado)
logger.LogInformation($"User {userId} logged in");

// ✅ Message template (estructurado)
logger.LogInformation("User {UserId} logged in", userId);

// ✅ Con @ para destructurar objetos
logger.LogInformation("Created {@Order}", order);
```

**Multiple Sinks:**

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/app-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Seq("http://localhost:5341")
    .WriteTo.GrafanaLoki("http://localhost:3100")
    .CreateLogger();
```

**LogContext (Scope):**

```csharp
using (LogContext.PushProperty("OrderId", orderId))
using (LogContext.PushProperty("CustomerId", customerId))
{
    logger.LogInformation("Processing order");
    // Todos los logs incluyen OrderId y CustomerId
    await ValidateOrderAsync();
    await SaveOrderAsync();
}
```

**Integración OpenTelemetry:**

```csharp
// Correlación automática
Log.Logger = new LoggerConfiguration()
    .Enrich.WithSpan()  // Agregar trace_id y span_id
    .WriteTo.Console()
    .CreateLogger();

// Output incluye correlación
{
  "message": "Processing order",
  "trace_id": "4bf92f3577b34da6a3ce929d0e0e4736",
  "span_id": "00f067aa0ba902b7"
}
```

### 4. issue-37.md

**Ubicación:** `.github/issues/issue-37.md`

**Contenido:** Este archivo - documentación completa del Issue #37

---

## 🔄 Stack Completo de Observabilidad

### Arquitectura

```
┌─────────────┐     ┌────────────────┐     ┌─────────────┐
│             │     │                │     │   Jaeger    │
│  .NET API   │────▶│ OpenTelemetry  │────▶│  (Traces)   │
│             │     │   Collector    │     ├─────────────┤
│ + Serilog   │     │                │     │ Prometheus  │
│ + OTel SDK  │     │   (OTLP)       │     │  (Metrics)  │
│             │     │                │     ├─────────────┤
└─────────────┘     └────────────────┘     │    Loki     │
                                           │   (Logs)    │
                                           └─────────────┘
                                                  │
                                                  ▼
                                           ┌─────────────┐
                                           │   Grafana   │
                                           │ (Dashboard) │
                                           └─────────────┘
```

### Docker Compose Completo

```yaml
services:
  # OpenTelemetry Collector
  otel-collector:
    image: otel/opentelemetry-collector-contrib:latest
    command: ["--config=/etc/otel-collector-config.yaml"]
    volumes:
      - ./otel-collector-config.yaml:/etc/otel-collector-config.yaml
    ports:
      - "4317:4317"  # OTLP gRPC
      - "4318:4318"  # OTLP HTTP

  # Jaeger (Traces)
  jaeger:
    image: jaegertracing/all-in-one:latest
    ports:
      - "16686:16686"

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
    volumes:
      - ./grafana/provisioning:/etc/grafana/provisioning
    ports:
      - "3000:3000"

  # Seq (Opcional - Logs UI)
  seq:
    image: datalust/seq:latest
    ports:
      - "5341:80"
    environment:
      - ACCEPT_EULA=Y
```

---

## 💡 Ejemplos de Uso

### Ejemplo 1: Instrumentar .NET API

```csharp
// Program.cs
using OpenTelemetry;
using Serilog;

// 1. Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.WithSpan()
    .WriteTo.Console()
    .WriteTo.GrafanaLoki("http://localhost:3100")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// 2. Configurar OpenTelemetry
builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("mi-api"))
    .WithTracing(t => t
        .AddAspNetCoreInstrumentation()
        .AddOtlpExporter(o => o.Endpoint = new Uri("http://localhost:4317")))
    .WithMetrics(m => m
        .AddAspNetCoreInstrumentation()
        .AddOtlpExporter());

var app = builder.Build();
app.UseSerilogRequestLogging();
app.Run();
```

### Ejemplo 2: Custom Metrics

```csharp
public class OrderMetrics
{
    private static readonly Meter Meter = new("MyApp.Orders");
    private static readonly Counter<long> OrdersCreated =
        Meter.CreateCounter<long>("orders.created");

    public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
    {
        var order = await _repository.CreateAsync(request);
        OrdersCreated.Add(1, new("status", "success"));
        return order;
    }
}
```

### Ejemplo 3: Dashboard en Grafana

```promql
# Panel 1: Request Rate
rate(http_server_request_duration_count{job="mi-api"}[5m])

# Panel 2: P95 Latency
histogram_quantile(0.95, rate(http_server_request_duration_bucket{job="mi-api"}[5m]))

# Panel 3: Error Rate
sum(rate(http_server_request_duration_count{job="mi-api",http_status_code=~"5.."}[5m]))
/ sum(rate(http_server_request_duration_count{job="mi-api"}[5m])) * 100
```

### Ejemplo 4: Correlación de Logs y Traces

```csharp
// En un request
public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
{
    using var activity = ActivitySource.StartActivity("CreateOrder");
    activity?.SetTag("order.id", request.OrderId);

    // Log con correlación automática
    _logger.LogInformation("Creating order {OrderId}", request.OrderId);
    // Output:
    // {
    //   "message": "Creating order ORD-123",
    //   "OrderId": "ORD-123",
    //   "trace_id": "4bf92f35...",  ← Correlación
    //   "span_id": "00f067aa..."    ← Correlación
    // }

    return Ok();
}
```

En Grafana, puedes hacer clic en el log y saltar directamente al trace en Jaeger.

---

## 🎯 Best Practices Implementadas

### 1. Semantic Conventions

```csharp
// ✅ Usar semantic conventions de OpenTelemetry
activity?.SetTag("http.method", "POST");
activity?.SetTag("http.url", "https://api.example.com");
activity?.SetTag("http.status_code", 200);
activity?.SetTag("db.system", "postgresql");
```

### 2. Sampling Inteligente

```csharp
// No grabar todos los traces en producción
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .SetSampler(new TraceIdRatioBasedSampler(0.1)));  // 10%
```

### 3. Structured Logging

```csharp
// ✅ Message templates
logger.LogInformation("Order {OrderId} created by {CustomerId}", orderId, customerId);

// ❌ String interpolation
logger.LogInformation($"Order {orderId} created");  // NO
```

### 4. Cardinality Control

```csharp
// ❌ EVITAR alta cardinalidad
activity?.SetTag("user.id", userId);  // Millones de valores

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
catch (Exception ex)
{
    activity?.SetStatus(ActivityStatusCode.Error);
    activity?.RecordException(ex);
    logger.LogError(ex, "Failed to process order {OrderId}", orderId);
    throw;
}
```

---

## ✅ Criterios de Éxito

- [x] opentelemetry.md skill creada (434 líneas)
- [x] grafana.md skill creada (365 líneas)
- [x] serilog.md skill creada (318 líneas)
- [x] issue-37.md documentación creada
- [x] Traces con auto-instrumentación documentado
- [x] Metrics con 4 tipos documentados
- [x] Logs con correlación automática
- [x] OpenTelemetry Collector configurado
- [x] Grafana dashboards y alerting
- [x] Serilog structured logging
- [x] Docker Compose stack completo
- [x] Best practices documentadas
- [x] Ejemplos completos funcionales
- [x] Todo el contenido en español
- [x] README.md actualizado
- [x] ROADMAP.md actualizado
- [x] Todos los archivos committed
- [x] Merged a main
- [x] Issue documentado y cerrado

---

## 🔄 Integración con Otros Agentes/Tools

### Workflow DevOps Completo

```bash
# 1. Desarrollo
/mj2:2-run API-USERS-001        # Backend (tdd-implementer)
/mj2:2f-build COMP-LOGIN-001    # Frontend (frontend-builder)

# 2. Testing
/mj2:4-e2e E2E-LOGIN-001        # E2E (e2e-tester)
/mj2:quality-check              # Quality validation

# 3. Docker build
docker-compose build            # Con multi-stage builds (Issue #34)

# 4. Deploy
/mj2:5-deploy production        # DevOps expert (Issue #35)

# 5. CI/CD
# GitHub Actions ejecuta:        # (Issue #36)
# - Backend CI
# - Frontend CI
# - E2E CI
# - CD (Blue-Green)

# 6. Observability ← ESTE ISSUE
# Automático con OpenTelemetry:
# - Traces → Jaeger
# - Metrics → Prometheus → Grafana
# - Logs → Loki → Grafana
# - Dashboards en Grafana
# - Alertas automáticas
```

---

## 📈 Resumen de Métricas

| Métrica | Valor |
|---------|-------|
| **Archivos Creados** | 4 (3 skills + 1 doc) |
| **Total Líneas** | ~1,117 |
| **Skills** | 3 (OpenTelemetry, Grafana, Serilog) |
| **Data Sources** | 3 (Prometheus, Loki, Jaeger) |
| **Telemetry Signals** | 3 (Traces, Metrics, Logs) |
| **Exporters** | 4 (Console, Jaeger, Prometheus, OTLP) |
| **Sinks Serilog** | 4 (Console, File, Seq, Loki) |
| **Alert Rules** | 4+ ejemplos |
| **Dashboard Panels** | 5+ tipos |
| **Idioma** | 100% Español ✅ |

---

## 🚀 Próximos Pasos (Issue #38)

Con Observability completado (Issue #37), los próximos pasos son:

**Issue #38:** Database Expert Agent
- database-expert.md agent
- mj2-db-migrate.md command
- Database design y optimization
- Migration strategies
- Integration con PostgreSQL skill

**Prerequisites completados:** ✅
- Docker Foundation ✅ (Issue #34)
- DevOps Expert ✅ (Issue #35)
- GitHub Actions ✅ (Issue #36)
- OpenTelemetry Stack ✅ (Issue #37) ← **Este issue**

**Ready for:**
- Issue #38: Database Expert Agent
- v0.3.0: Full-stack + DevOps completion

---

## 📚 Recursos Adicionales

### OpenTelemetry
- [OpenTelemetry .NET](https://opentelemetry.io/docs/instrumentation/net/)
- [Semantic Conventions](https://opentelemetry.io/docs/specs/semconv/)
- [Collector Configuration](https://opentelemetry.io/docs/collector/configuration/)

### Grafana
- [Grafana Documentation](https://grafana.com/docs/grafana/latest/)
- [PromQL Guide](https://prometheus.io/docs/prometheus/latest/querying/basics/)
- [LogQL Guide](https://grafana.com/docs/loki/latest/logql/)

### Serilog
- [Serilog Documentation](https://serilog.net/)
- [Serilog Best Practices](https://benfoster.io/blog/serilog-best-practices/)
- [Message Templates](https://messagetemplates.org/)

---

**Completado por:** Claude Code
**Commit:** feature/issue-37-observability → main
**Archivos:** 4 (opentelemetry.md, grafana.md, serilog.md, issue-37.md)
**Líneas Añadidas:** ~1,117
**Idioma:** 100% Español ✅
**OpenTelemetry Stack:** ✅ **COMPLETO**
