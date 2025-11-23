---
name: mj2-9-feedback
description: Gestiona feedback, errores comunes y aprendizaje continuo
tags: [feedback, learning, improvement]
---

# /mj2:9-feedback - Feedback & Learning

Comando para gestionar feedback, trackear errores y facilitar aprendizaje continuo.

---

## 📋 Uso

```bash
# Collect feedback
/mj2:9-feedback collect <type> "<title>" [options]

# Analyze feedback  
/mj2:9-feedback analyze [options]

# Apply learnings
/mj2:9-feedback apply <action>

# Review feedback
/mj2:9-feedback review [filter]

# Clear feedback
/mj2:9-feedback clear <scope>
```

---

## 💡 Ejemplos

### Reportar Bug
```bash
/mj2:9-feedback collect bug "N+1 query en GetOrders" --severity high --context "OrdersController.cs:42"

# Output:
✅ Bug report creado: fb-2025-11-23-001
📁 .mj2/memory/feedback/open/bug-001-n1-query.json
```

### Solicitar Feature
```bash
/mj2:9-feedback collect feature "Agregar pagination a ProductsList" --priority medium

# Output:
✅ Feature request creado: fb-2025-11-23-002
📁 .mj2/memory/feedback/open/feature-002-pagination.json
```

### Analizar Feedback
```bash
/mj2:9-feedback analyze --period 30d

# Output:
📊 Feedback Insights (Last 30 days)
- Total: 23 (15 bugs, 6 features, 2 questions)
- Top Issues: N+1 Queries (15), Accessibility (8), Error Handling (6)
💡 Recommendations: Add Include() checklist, Run a11y-audit, Use Result pattern
```

### Aplicar Learnings
```bash
/mj2:9-feedback apply rule avoid-n1

# Output:
✅ Execution rule 'avoid-n1' aplicada
🔧 EF Core queries serán analizadas para N+1 patterns
```

### Revisar Feedback
```bash
/mj2:9-feedback review open

# Output:
📋 Open Feedback (3)
1. [BUG-HIGH] N+1 query en GetOrders
2. [FEATURE-MEDIUM] Agregar pagination
3. [QUESTION] ¿Cómo implementar CQRS?
```

---

## 📚 Ver También

- Agente: `.claude/agents/mj2/feedback-manager.md`
- Memory: `.mj2/memory/`

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-23
