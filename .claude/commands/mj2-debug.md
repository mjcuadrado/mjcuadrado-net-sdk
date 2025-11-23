---
name: mj2-debug
description: Analiza y resuelve errores con debugging sistemático
tags: [debugging, troubleshooting, errors]
---

# /mj2:debug - Debug Helper

Comando para analizar errores de forma sistemática con el Debug Helper agent.

## 📋 Uso

```bash
# Con stack trace
/mj2:debug "<stack trace or error message>"

# Con descripción
/mj2:debug "API slow when listing orders"

# Con contexto
/mj2:debug "NullReferenceException in CreateOrder line 42"
```

## 💡 Ejemplos

### Ejemplo 1: NullReferenceException

```bash
/mj2:debug "NullReferenceException at OrdersController.cs:42"
```

**Output:**
```
🔍 INVESTIGATE
✓ Stack trace analyzed
✓ Root cause hypothesis: productId null not validated

💡 ANALYZE
✓ Pattern: Null reference (40% of API errors)
✓ Impact: API crash with 500 error

🔧 DIAGNOSE
✓ Breakpoints suggested
✓ Logging enhanced

✅ RESOLVE
✓ Fix: Add [Required] to ProductId
✓ Fix: Add null checks
✓ Tests: 3 regression tests created
```

### Ejemplo 2: Performance Issue

```bash
/mj2:debug "API slow - 1,234ms to list orders"
```

**Output:**
```
🔍 INVESTIGATE
✓ N+1 query pattern detected
✓ 1 query + 50 queries for customers

✅ RESOLVE
✓ Fix: Add .Include(o => o.Customer)
✓ Performance: 1,234ms → 38ms (96.9% faster)
```

---

**Ver:** `.claude/agents/mj2/debug-helper.md`
**Versión:** 1.0.0
**Última Actualización:** 2025-11-23
