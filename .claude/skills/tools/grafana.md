---
skill: grafana
description: Grafana para visualización y alerting de observabilidad
category: tools
tags: [observability, monitoring, dashboards, alerting, visualization, grafana]
version: 1.0.0
related_skills: [opentelemetry, serilog, prometheus, loki, docker-compose]
---

# Grafana - Visualización y Alerting

Grafana es una plataforma open-source para visualización y análisis de métricas, logs y traces. Integración perfecta con Prometheus, Loki y Jaeger.

---

## 📋 Tabla de Contenidos

1. [Conceptos Básicos](#conceptos-básicos)
2. [Instalación](#instalación)
3. [Data Sources](#data-sources)
4. [Dashboards](#dashboards)
5. [Query Builder](#query-builder)
6. [Alerting](#alerting)
7. [Variables](#variables)
8. [Panels](#panels)
9. [Best Practices](#best-practices)
10. [Troubleshooting](#troubleshooting)

---

## Conceptos Básicos

### ¿Qué es Grafana?

Grafana proporciona:
- **Dashboards:** Visualización de métricas y logs
- **Alerting:** Alertas basadas en queries
- **Data Sources:** Integración con múltiples backends
- **Templating:** Dashboards dinámicos con variables
- **Annotations:** Marcar eventos importantes

### Arquitectura

```
┌─────────────┐     ┌─────────────┐
│ Prometheus  │────▶│             │
├─────────────┤     │   Grafana   │◀────┐
│    Loki     │────▶│             │     │
├─────────────┤     └─────────────┘     │
│   Jaeger    │────▶                    │
└─────────────┘          Users ─────────┘
```

---

## Instalación

### Docker Compose (Recomendado)

```yaml
services:
  grafana:
    image: grafana/grafana:latest
    ports:
      - "3000:3000"
    environment:
      - GF_AUTH_ANONYMOUS_ENABLED=true
      - GF_AUTH_ANONYMOUS_ORG_ROLE=Admin
      - GF_SECURITY_ADMIN_PASSWORD=admin
      - GF_INSTALL_PLUGINS=grafana-clock-panel
    volumes:
      - grafana-data:/var/lib/grafana
      - ./grafana/provisioning:/etc/grafana/provisioning
    networks:
      - monitoring

volumes:
  grafana-data:

networks:
  monitoring:
```

### Stack Completo de Observabilidad

```yaml
services:
  # Grafana
  grafana:
    image: grafana/grafana:latest
    ports:
      - "3000:3000"
    volumes:
      - ./grafana/provisioning:/etc/grafana/provisioning
      - grafana-data:/var/lib/grafana
    depends_on:
      - prometheus
      - loki
      - jaeger

  # Prometheus (Métricas)
  prometheus:
    image: prom/prometheus:latest
    ports:
      - "9090:9090"
    volumes:
      - ./prometheus.yml:/etc/prometheus/prometheus.yml
      - prometheus-data:/prometheus
    command:
      - '--config.file=/etc/prometheus/prometheus.yml'
      - '--storage.tsdb.path=/prometheus'

  # Loki (Logs)
  loki:
    image: grafana/loki:latest
    ports:
      - "3100:3100"
    volumes:
      - ./loki-config.yaml:/etc/loki/local-config.yaml
      - loki-data:/loki
    command: -config.file=/etc/loki/local-config.yaml

  # Jaeger (Traces)
  jaeger:
    image: jaegertracing/all-in-one:latest
    ports:
      - "16686:16686"  # UI
      - "14250:14250"  # gRPC
    environment:
      - COLLECTOR_ZIPKIN_HOST_PORT=:9411

volumes:
  grafana-data:
  prometheus-data:
  loki-data:
```

### Acceso Inicial

```bash
# URL: http://localhost:3000
# Usuario: admin
# Password: admin (cambiar en primer login)
```

---

## Data Sources

### Provisioning de Data Sources

**grafana/provisioning/datasources/datasources.yml:**

```yaml
apiVersion: 1

datasources:
  # Prometheus - Métricas
  - name: Prometheus
    type: prometheus
    access: proxy
    url: http://prometheus:9090
    isDefault: true
    jsonData:
      timeInterval: 15s
      queryTimeout: 60s
    editable: false

  # Loki - Logs
  - name: Loki
    type: loki
    access: proxy
    url: http://loki:3100
    jsonData:
      maxLines: 1000
      derivedFields:
        - datasourceUid: Jaeger
          matcherRegex: "trace_id=(\\w+)"
          name: TraceID
          url: "$${__value.raw}"
    editable: false

  # Jaeger - Traces
  - name: Jaeger
    type: jaeger
    access: proxy
    url: http://jaeger:16686
    jsonData:
      tracesToLogsV2:
        datasourceUid: Loki
        filterByTraceID: true
        filterBySpanID: false
        tags: [{ key: 'service.name', value: 'service_name' }]
    editable: false
```

### Configurar Data Source Manualmente

1. **Ir a Configuration → Data Sources → Add data source**

2. **Prometheus:**
   - URL: `http://prometheus:9090`
   - Access: Server (default)
   - Scrape interval: `15s`

3. **Loki:**
   - URL: `http://loki:3100`
   - Maximum lines: `1000`

4. **Jaeger:**
   - URL: `http://jaeger:16686`
   - Trace to logs: Link to Loki

---

## Dashboards

### Crear Dashboard Básico

```json
{
  "dashboard": {
    "title": ".NET API Monitoring",
    "panels": [
      {
        "title": "Request Rate",
        "type": "graph",
        "datasource": "Prometheus",
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

### Provisioning de Dashboards

**grafana/provisioning/dashboards/dashboards.yml:**

```yaml
apiVersion: 1

providers:
  - name: 'Default'
    orgId: 1
    folder: ''
    type: file
    disableDeletion: false
    updateIntervalSeconds: 10
    allowUiUpdates: true
    options:
      path: /etc/grafana/provisioning/dashboards
```

**grafana/provisioning/dashboards/api-dashboard.json:**

```json
{
  "dashboard": {
    "title": ".NET API Dashboard",
    "uid": "dotnet-api",
    "version": 1,
    "timezone": "browser",
    "panels": [
      {
        "id": 1,
        "title": "Request Rate (req/s)",
        "type": "graph",
        "datasource": "Prometheus",
        "gridPos": {"h": 8, "w": 12, "x": 0, "y": 0},
        "targets": [
          {
            "expr": "rate(http_server_request_duration_count{job=\"mi-api\"}[5m])",
            "legendFormat": "{{method}} {{route}}",
            "refId": "A"
          }
        ],
        "yaxes": [
          {"format": "reqps", "label": "Requests/sec"},
          {"format": "short"}
        ]
      },
      {
        "id": 2,
        "title": "Response Time (p95)",
        "type": "graph",
        "datasource": "Prometheus",
        "gridPos": {"h": 8, "w": 12, "x": 12, "y": 0},
        "targets": [
          {
            "expr": "histogram_quantile(0.95, rate(http_server_request_duration_bucket{job=\"mi-api\"}[5m]))",
            "legendFormat": "p95 {{route}}",
            "refId": "A"
          }
        ],
        "yaxes": [
          {"format": "ms", "label": "Latency"},
          {"format": "short"}
        ]
      },
      {
        "id": 3,
        "title": "Error Rate (%)",
        "type": "graph",
        "datasource": "Prometheus",
        "gridPos": {"h": 8, "w": 12, "x": 0, "y": 8},
        "targets": [
          {
            "expr": "rate(http_server_request_duration_count{job=\"mi-api\",http_status_code=~\"5..\"}[5m]) / rate(http_server_request_duration_count{job=\"mi-api\"}[5m]) * 100",
            "legendFormat": "Error Rate",
            "refId": "A"
          }
        ],
        "yaxes": [
          {"format": "percent", "label": "Error %"},
          {"format": "short"}
        ]
      }
    ]
  }
}
```

---

## Query Builder

### PromQL Queries (Prometheus)

```promql
# Request rate por método HTTP
rate(http_server_request_duration_count[5m])

# P95 latency
histogram_quantile(0.95, rate(http_server_request_duration_bucket[5m]))

# Error rate (status 5xx)
sum(rate(http_server_request_duration_count{http_status_code=~"5.."}[5m]))
/ sum(rate(http_server_request_duration_count[5m]))

# Memoria usada
process_resident_memory_bytes / 1024 / 1024

# CPU usage
rate(process_cpu_seconds_total[5m]) * 100

# Active connections
sum(http_server_active_requests)
```

### LogQL Queries (Loki)

```logql
# Todos los logs de mi-api
{job="mi-api"}

# Logs de error
{job="mi-api"} |= "error" | json

# Logs con filtro
{job="mi-api"} | json | level="Error"

# Logs con regex
{job="mi-api"} |~ "Exception.*occurred"

# Rate de logs de error
rate({job="mi-api"} |= "error" [5m])

# Top 10 errores
topk(10, sum by (exception_type) (rate({job="mi-api"} | json | level="Error" [5m])))
```

### Jaeger Query (Traces)

```
# Buscar traces por servicio
service = "mi-api"

# Buscar traces con errores
service = "mi-api" AND error = true

# Buscar traces lentos
service = "mi-api" AND duration > 1s

# Buscar por operación
service = "mi-api" AND operation = "POST /api/orders"
```

---

## Alerting

### Crear Alert Rule

**grafana/provisioning/alerting/alerts.yml:**

```yaml
apiVersion: 1

groups:
  - name: API Alerts
    interval: 1m
    rules:
      # Alta tasa de errores
      - alert: HighErrorRate
        expr: |
          (
            sum(rate(http_server_request_duration_count{http_status_code=~"5.."}[5m]))
            /
            sum(rate(http_server_request_duration_count[5m]))
          ) > 0.05
        for: 5m
        labels:
          severity: critical
          team: backend
        annotations:
          summary: "High error rate detected"
          description: "Error rate is {{ $value | humanizePercentage }} (threshold: 5%)"

      # Alta latencia
      - alert: HighLatency
        expr: |
          histogram_quantile(0.95,
            rate(http_server_request_duration_bucket[5m])
          ) > 1000
        for: 5m
        labels:
          severity: warning
          team: backend
        annotations:
          summary: "High latency detected"
          description: "P95 latency is {{ $value }}ms (threshold: 1000ms)"

      # API Down
      - alert: APIDown
        expr: up{job="mi-api"} == 0
        for: 1m
        labels:
          severity: critical
          team: backend
        annotations:
          summary: "API is down"
          description: "API {{ $labels.instance }} is not responding"

      # Alta memoria
      - alert: HighMemoryUsage
        expr: |
          process_resident_memory_bytes{job="mi-api"}
          / 1024 / 1024 / 1024 > 1
        for: 5m
        labels:
          severity: warning
          team: backend
        annotations:
          summary: "High memory usage"
          description: "Memory usage is {{ $value }}GB (threshold: 1GB)"
```

### Contact Points

**grafana/provisioning/alerting/contact-points.yml:**

```yaml
apiVersion: 1

contactPoints:
  - name: Email
    receivers:
      - uid: email-receiver
        type: email
        settings:
          addresses: "team@example.com"

  - name: Slack
    receivers:
      - uid: slack-receiver
        type: slack
        settings:
          url: "https://hooks.slack.com/services/YOUR/WEBHOOK/URL"
          text: |
            {{ range .Alerts }}
            *Alert:* {{ .Labels.alertname }}
            *Status:* {{ .Status }}
            *Description:* {{ .Annotations.description }}
            {{ end }}

  - name: PagerDuty
    receivers:
      - uid: pagerduty-receiver
        type: pagerduty
        settings:
          integrationKey: "YOUR_INTEGRATION_KEY"
```

### Notification Policies

```yaml
policies:
  - receiver: Email
    group_by: ['alertname', 'severity']
    group_wait: 30s
    group_interval: 5m
    repeat_interval: 4h
    routes:
      - receiver: Slack
        matchers:
          - severity = critical
        continue: true
      - receiver: PagerDuty
        matchers:
          - severity = critical
          - team = backend
```

---

## Variables

### Dashboard Variables

```json
{
  "templating": {
    "list": [
      {
        "name": "namespace",
        "type": "query",
        "datasource": "Prometheus",
        "query": "label_values(up, namespace)",
        "refresh": 1,
        "multi": true,
        "includeAll": true
      },
      {
        "name": "service",
        "type": "query",
        "datasource": "Prometheus",
        "query": "label_values(up{namespace=\"$namespace\"}, job)",
        "refresh": 1,
        "multi": false
      },
      {
        "name": "interval",
        "type": "interval",
        "options": [
          {"text": "1m", "value": "1m"},
          {"text": "5m", "value": "5m"},
          {"text": "15m", "value": "15m"},
          {"text": "1h", "value": "1h"}
        ],
        "current": {"text": "5m", "value": "5m"}
      }
    ]
  }
}
```

### Usar Variables en Queries

```promql
# Usar variable $service
rate(http_server_request_duration_count{job="$service"}[$interval])

# Múltiples valores
rate(http_server_request_duration_count{namespace=~"$namespace"}[5m])
```

---

## Panels

### Tipos de Panels

**1. Time Series (Graph)**
```json
{
  "type": "timeseries",
  "options": {
    "legend": {"calcs": ["mean", "max"], "displayMode": "table"},
    "tooltip": {"mode": "multi"}
  }
}
```

**2. Stat (Single Value)**
```json
{
  "type": "stat",
  "options": {
    "reduceOptions": {"calcs": ["last"]},
    "text": {"titleSize": 20, "valueSize": 60}
  }
}
```

**3. Gauge**
```json
{
  "type": "gauge",
  "options": {
    "showThresholdLabels": true,
    "showThresholdMarkers": true
  },
  "fieldConfig": {
    "defaults": {
      "thresholds": {
        "steps": [
          {"value": 0, "color": "green"},
          {"value": 80, "color": "yellow"},
          {"value": 90, "color": "red"}
        ]
      },
      "unit": "percent"
    }
  }
}
```

**4. Table**
```json
{
  "type": "table",
  "options": {
    "showHeader": true,
    "sortBy": [{"displayName": "Time", "desc": true}]
  }
}
```

**5. Logs Panel**
```json
{
  "type": "logs",
  "datasource": "Loki",
  "options": {
    "showTime": true,
    "showLabels": true,
    "wrapLogMessage": true
  }
}
```

---

## Best Practices

### 1. Dashboard Organization

```
Dashboards/
├── Overview/              # High-level metrics
│   └── System Overview
├── API/                   # API-specific
│   ├── Request Metrics
│   ├── Error Tracking
│   └── Performance
├── Infrastructure/        # Infra metrics
│   ├── CPU & Memory
│   └── Network
└── Business/              # Business metrics
    └── Orders Dashboard
```

### 2. Panel Naming

```
# ❌ Malo
"Graph 1"
"Query A"

# ✅ Bueno
"Request Rate (req/s) - Last 1h"
"P95 Latency by Endpoint"
"Error Rate (%) - 5xx Responses"
```

### 3. Query Optimization

```promql
# ❌ Evitar queries costosas
sum(rate(metric[1d]))  # 1 día es mucho

# ✅ Usar intervalos razonables
sum(rate(metric[5m]))  # 5 minutos

# ❌ Evitar muchas series
{job=~".*"}  # Todas las series

# ✅ Filtrar correctamente
{job="mi-api", namespace="production"}
```

### 4. Thresholds

```json
{
  "thresholds": {
    "steps": [
      {"value": 0, "color": "green"},      // OK
      {"value": 80, "color": "yellow"},    // Warning
      {"value": 95, "color": "red"}        // Critical
    ]
  }
}
```

### 5. Alerting Best Practices

```yaml
# ✅ Alertas accionables
- alert: HighErrorRate
  expr: error_rate > 0.05
  for: 5m  # Evitar alertas por picos temporales

# ❌ Evitar alertas ruidosas
- alert: AnyError
  expr: error_count > 0  # Muy sensible
```

---

## Troubleshooting

### Dashboard no carga datos

**Problema:** Panel vacío
**Solución:**
```bash
# Verificar data source
curl http://localhost:9090/api/v1/query?query=up

# Verificar que Grafana puede alcanzar Prometheus
docker exec grafana curl http://prometheus:9090/-/healthy
```

### Query muy lenta

**Problema:** Panel tarda mucho en cargar
**Solución:**
```promql
# ❌ Query costosa
sum(rate(metric{job=~".*"}[1d]))

# ✅ Query optimizada
sum(rate(metric{job="mi-api"}[5m]))
```

### Alerts no se envían

**Problema:** Alertas no llegan
**Solución:**
```bash
# Verificar contact points
# Grafana UI → Alerting → Contact points → Test

# Verificar logs de Grafana
docker logs grafana | grep -i alert
```

---

## Recursos Adicionales

- [Grafana Docs](https://grafana.com/docs/grafana/latest/)
- [Dashboard Best Practices](https://grafana.com/docs/grafana/latest/best-practices/best-practices-for-creating-dashboards/)
- [PromQL Guide](https://prometheus.io/docs/prometheus/latest/querying/basics/)
- [LogQL Guide](https://grafana.com/docs/loki/latest/logql/)

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-22
**Mantenido por:** mjcuadrado-net-sdk
