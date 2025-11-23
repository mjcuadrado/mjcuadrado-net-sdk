# Issue #60: Monitoring Expert Agent

**Fecha:** 2025-11-23
**Prioridad:** 🟡 Media
**Estado:** 📋 Planificado
**Versión:** v0.8.0
**Branch:** feature/ISSUE-060-monitoring-expert
**Tiempo Estimado:** 5 días

---

## 📋 Descripción

Crear agente **monitoring-expert** para orchestrar observability (tenemos skills pero no agente).

**Gap identificado:** moai-adk tiene este agente. mj2 tiene skills (opentelemetry, grafana, serilog) pero falta orchestration.

---

## 🎯 Objetivos

### 1. Monitoring Expert Agent
- Crear `.claude/agents/mj2/monitoring-expert.md` (~700 líneas)
  - TRUST 5 principles
  - Workflow: INSTRUMENT → COLLECT → ANALYZE → ALERT
  - Orchestrar OpenTelemetry, Grafana, Serilog
  - SLO/SLI definition
  - Alerting strategy

### 2. Comando Slash
- Crear `.claude/commands/mj2-monitor.md` (~180 líneas)
  - Sintaxis: `/mj2:monitor <action>`
  - Actions: setup, dashboard, alert

---

## 📦 Entregables

### 1. monitoring-expert.md Agent
**Workflow:**
1. **INSTRUMENT** - Añadir telemetry al código
2. **COLLECT** - Configurar collectors
3. **ANALYZE** - Crear dashboards
4. **ALERT** - Definir alertas

**Orchestrates:**
- OpenTelemetry (traces, metrics, logs)
- Grafana (dashboards)
- Prometheus (metrics)
- Jaeger (traces)
- Loki (logs)
- Application Insights (Azure)

### 2. SLO/SLI Templates
```yaml
# SLO Definition
- name: API Availability
  sli: successful_requests / total_requests
  target: 99.9%
  window: 30d
```

---

## ✅ Criterios de Éxito

- [ ] monitoring-expert.md agent creado (~700 líneas)
- [ ] /mj2:monitor command creado (~180 líneas)
- [ ] Integration con skills existentes
- [ ] SLO/SLI templates
- [ ] Dashboard templates (Grafana)
- [ ] Alert rules templates
- [ ] Documentación completa

---

## 🔗 Referencias

- **Inspirado en:** moai-adk/monitoring-expert
- **Skills:** opentelemetry, grafana, serilog
- **Tools:** Prometheus, Jaeger, Loki

---

## 🚀 Impacto

**Sin monitoring-expert:**
- ❌ Manual monitoring setup
- ❌ No orchestration
- ❌ Inconsistent observability

**Con monitoring-expert:**
- ✅ Automated monitoring setup
- ✅ Complete observability stack
- ✅ SLO/SLI driven
- ✅ Production-ready monitoring

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Prioridad:** 🟡 MEDIA
**Milestone:** v0.8.0
