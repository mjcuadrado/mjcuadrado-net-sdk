# .mj2/memory - Sistema de Memoria y Aprendizaje

Este directorio contiene el sistema de memoria persistente de mj2, incluyendo feedback, execution rules, y session state.

---

## 📁 Estructura

```
.mj2/memory/
├── feedback/              # Feedback del usuario
│   ├── open/             # Feedback abierto (pendiente)
│   ├── resolved/         # Feedback resuelto
│   └── archived/         # Feedback archivado
├── execution-rules.json  # Reglas de ejecución automáticas
├── session-state.json    # Estado de la sesión actual
├── common-errors.json    # Patrones de errores comunes
├── insights.md          # Insights generados del análisis
└── README.md            # Este archivo
```

---

## 🔧 Archivos Principales

### execution-rules.json

Contiene reglas automáticas que se aplican durante la ejecución:

```json
{
  "rules": [
    {
      "id": "avoid-n1",
      "trigger": "EF Core query detected",
      "action": "Suggest .Include() or .AsSplitQuery()",
      "priority": "high",
      "enabled": true
    }
  ]
}
```

**Uso:**
- Las reglas se aplican automáticamente durante la ejecución
- Se pueden habilitar/deshabilitar con `/mj2:9-feedback apply rule <id>`
- Se generan desde feedback recurrente

### session-state.json

Trackea el estado de la sesión actual:

```json
{
  "session_id": "session-2025-11-23",
  "context": {
    "current_spec": "API-ORDERS-001",
    "current_phase": "IMPLEMENT",
    "technologies": ["ASP.NET Core", "React", "PostgreSQL"]
  },
  "learnings_applied": [...]
}
```

**Uso:**
- Se actualiza automáticamente durante la ejecución
- Mantiene contexto entre sesiones
- Trackea learnings aplicados

### common-errors.json

Catálogo de errores comunes detectados:

```json
{
  "patterns": [
    {
      "id": "n1-query",
      "name": "N+1 Query Pattern",
      "frequency": 15,
      "solution": "Usar .Include() para eager loading"
    }
  ]
}
```

**Uso:**
- Se actualiza con `/mj2:9-feedback analyze`
- Identifica patrones recurrentes
- Sugiere soluciones automáticamente

### insights.md

Documento con insights generados del análisis:

- Top issues del período
- Recomendaciones priorizadas
- Tendencias detectadas
- Mejoras implementadas

**Uso:**
- Se genera con `/mj2:9-feedback analyze`
- Se actualiza periódicamente
- Referencia para mejora continua

---

## 📥 Directorio feedback/

Contiene feedback capturado del usuario en 3 estados:

### open/

Feedback pendiente de resolución:
- Bugs reportados
- Features solicitadas
- Questions pendientes

**Formato:**
```json
{
  "type": "bug",
  "severity": "high",
  "title": "N+1 query en GetOrders",
  "status": "open",
  "timestamp": "2025-11-23T10:30:00Z"
}
```

### resolved/

Feedback resuelto:
- Bugs corregidos
- Features implementadas
- Questions respondidas

**Formato:**
```json
{
  "type": "bug",
  "status": "resolved",
  "resolution": {
    "implemented": true,
    "tested": true,
    "documented": true
  },
  "resolved_at": "2025-11-23T15:00:00Z"
}
```

### archived/

Feedback archivado (antiguo):
- Feedback de más de 90 días
- Feedback de versiones antiguas
- Feedback duplicado

---

## 🚀 Uso

### Capturar Feedback

```bash
# Reportar bug
/mj2:9-feedback collect bug "N+1 query en GetOrders" --severity high

# Solicitar feature
/mj2:9-feedback collect feature "Agregar pagination" --priority medium

# Hacer pregunta
/mj2:9-feedback collect question "¿Cómo implementar CQRS?"
```

### Analizar Feedback

```bash
# Analizar últimos 30 días
/mj2:9-feedback analyze --period 30d

# Analizar por tipo
/mj2:9-feedback analyze --type bug

# Analizar por severidad
/mj2:9-feedback analyze --severity high
```

### Aplicar Learnings

```bash
# Aplicar regla específica
/mj2:9-feedback apply rule avoid-n1

# Aplicar todos los insights
/mj2:9-feedback apply all
```

### Revisar Feedback

```bash
# Ver feedback abierto
/mj2:9-feedback review open

# Ver feedback resuelto
/mj2:9-feedback review resolved

# Ver todo
/mj2:9-feedback review all
```

### Limpiar Feedback

```bash
# Limpiar feedback resuelto
/mj2:9-feedback clear resolved

# Limpiar feedback archivado
/mj2:9-feedback clear archived
```

---

## 🔄 Workflow de Feedback

```
1. COLLECT
   ↓ Usuario reporta bug/feature/question
   ↓ Se crea archivo en feedback/open/
   ↓ Se actualiza session-state.json

2. ANALYZE
   ↓ Sistema identifica patrones
   ↓ Se actualiza common-errors.json
   ↓ Se generan insights.md

3. APPLY
   ↓ Se crean execution rules
   ↓ Se actualizan learnings_applied
   ↓ Se aplican automáticamente

4. VALIDATE
   ↓ Se verifica resolución
   ↓ Archivo se mueve a resolved/
   ↓ Se miden mejoras
```

---

## 📚 Ver También

- [Feedback Manager Agent](../../.claude/agents/mj2/feedback-manager.md)
- [Comando /mj2:9-feedback](../../.claude/commands/mj2-9-feedback.md)
- [ROADMAP](../../docs/ROADMAP.md)

---

**Versión:** 1.0.0
**Issue:** #44
**Última Actualización:** 2025-11-23
