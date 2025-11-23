---
name: mj2-migrate
description: Migra proyectos legacy a arquitectura mj2
tags: [migration, refactoring, legacy]
---

# /mj2:migrate - Migration Expert

Comando para migrar proyectos legacy a la arquitectura mj2 de forma incremental y segura.

## 📋 Uso

```bash
# Migrar proyecto legacy
/mj2:migrate "<legacy project path>"

# Con estrategia específica
/mj2:migrate "<path>" --strategy strangler-fig

# Dry run (análisis sin cambios)
/mj2:migrate "<path>" --dry-run
```

## 💡 Ejemplos

### Ejemplo 1: Migrar de EF6 a EF Core

```bash
/mj2:migrate "./MyLegacyApp" --strategy strangler-fig
```

**Output:**
```
📊 ASSESS
✓ Lines of code: 15,000
✓ Test coverage: 45%
✓ Dependencies: 25
✓ Complexity: Medium

📋 PLAN
✓ Strategy: Strangler Fig
✓ Phases: 4
✓ Estimated time: 2 weeks

🔧 MIGRATE (Phase 1)
✓ Migrate Controllers to DI
✓ Tests passing: 45/45

✅ Phase 1 complete
Next: /mj2:migrate continue
```

---

**Ver:** `.claude/agents/mj2/migration-expert.md`
**Versión:** 1.0.0
**Última Actualización:** 2025-11-23
