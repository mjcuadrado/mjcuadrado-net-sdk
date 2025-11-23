# Issue #55: Format Expert Agent

**Fecha:** 2025-11-23
**Prioridad:** 🟡 Media
**Estado:** 📋 Planificado
**Versión:** v0.6.0
**Branch:** feature/ISSUE-055-format-expert
**Tiempo Estimado:** 4-5 días

---

## 📋 Descripción

Crear agente **format-expert** para code formatting y linting automatizado, asegurando consistencia de código en todo el proyecto.

**Gap identificado:** moai-adk tiene este agente, mj2 no. Útil para mantener standards de código automáticamente.

---

## 🎯 Objetivos

### 1. Format Expert Agent
- Crear `.claude/agents/mj2/format-expert.md` (~650 líneas)
  - TRUST 5 principles
  - Workflow: ANALYZE → FORMAT → LINT → VALIDATE
  - Integration con dotnet format, prettier, ESLint
  - Auto-formatting antes de commits
  - Validación de style guidelines

### 2. Comando Slash
- Crear `.claude/commands/mj2-format.md` (~150 líneas)
  - Sintaxis: `/mj2:format [path]`
  - Opciones: --check, --fix, --staged

### 3. Skills de Formateo
- `.claude/skills/tools/dotnet-format.md` (~300 líneas)
- `.claude/skills/tools/prettier.md` (~250 líneas)
- `.claude/skills/tools/eslint.md` (~300 líneas)

---

## 📦 Entregables

### 1. format-expert.md Agent
**Workflow:**
1. **ANALYZE** - Detectar archivos modificados
2. **FORMAT** - Aplicar formateo automático
3. **LINT** - Validar reglas de linting
4. **VALIDATE** - Verificar no hay errores

**Soporta:**
- .NET: dotnet format, StyleCop
- JavaScript/TypeScript: Prettier, ESLint
- CSS: Prettier, Stylelint
- JSON/YAML: Prettier

### 2. Configuración Automática
```json
// .editorconfig
root = true

[*.cs]
dotnet_diagnostic.IDE0055.severity = error
dotnet_sort_system_directives_first = true

[*.{js,ts,tsx}]
indent_size = 2
```

### 3. Git Hook Integration
```bash
# .git/hooks/pre-commit
#!/bin/bash
/mj2:format --staged --check
```

---

## ✅ Criterios de Éxito

- [ ] format-expert.md agent creado (~650 líneas)
- [ ] /mj2:format command creado (~150 líneas)
- [ ] 3 skills de formateo creados (~850 líneas)
- [ ] Integration con git hooks
- [ ] Auto-format en save
- [ ] Validación en CI/CD
- [ ] Documentación completa

---

## 🔗 Referencias

- **Inspirado en:** moai-adk/format-expert
- **Tools:** dotnet format, Prettier, ESLint, StyleCop
- **Integration:** git hooks, CI/CD

---

## 🚀 Impacto

**Sin format-expert:**
- ❌ Código inconsistente
- ❌ Manual formatting
- ❌ Style violations en PRs

**Con format-expert:**
- ✅ Código consistente automáticamente
- ✅ Auto-formatting en save
- ✅ Validación automática pre-commit
- ✅ CI/CD gates para style

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Prioridad:** 🟡 MEDIA
**Milestone:** v0.6.0
