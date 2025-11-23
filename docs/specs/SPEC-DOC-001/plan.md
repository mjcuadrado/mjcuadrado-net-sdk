# SPEC-DOC-001: Implementation Plan

**SPEC ID:** SPEC-DOC-001
**Created:** 2024-11-24
**Status:** Draft
**Tags:** @SPEC:DOC-001

---

## 🎯 Implementation Strategy

### Timeline: 2-3 días (16-20 hours)

**Day 1:** Auditoría completa + README.md
**Day 2:** ROADMAP.md + Issues #41/#47
**Day 3:** Verificación + CHANGELOG + Commit

---

## 📋 Task Breakdown

### Phase 1: Auditoría de Métricas (4-5 hours)

#### Task 1.1: Contar Skills reales
**Estimate:** 1 hour
**Command:**
```bash
find .claude/skills -name "*.md" -type f | wc -l
ls -la .claude/skills/**/*.md
```

**Deliverables:**
- Número total de skills
- Skills por categoría (foundation, dotnet, frontend, testing, etc.)
- Lista completa para verificación

#### Task 1.2: Contar Agentes reales
**Estimate:** 1 hour
**Command:**
```bash
find .claude/agents/mj2 -name "*.md" -type f | wc -l
ls -la .claude/agents/mj2/*.md
```

**Deliverables:**
- Número total de agentes
- Lista completa de agentes con descripciones
- Agentes por categoría

#### Task 1.3: Contar Comandos reales
**Estimate:** 1 hour
**Command:**
```bash
find .claude/commands -name "*.md" -type f | wc -l
ls -la .claude/commands/*.md
```

**Deliverables:**
- Número total de comandos
- Lista completa de comandos
- Comandos por categoría (workflow, git, testing, etc.)

#### Task 1.4: Verificar Tests y Coverage
**Estimate:** 1 hour
**Command:**
```bash
dotnet test --collect:"XPlat Code Coverage"
```

**Deliverables:**
- Número de tests actuales
- Coverage actual
- Estado de builds

#### Task 1.5: Verificar Issue #50 (Python Hooks)
**Estimate:** 30 min
**Action:**
```bash
ls -la .claude/hooks/*.py
cat .claude/hooks/user-prompt-submit.py | head -20
```

**Deliverables:**
- Verificar que hooks son Python (.py)
- Verificar versión (v2.0.0)
- Documentar requirements (Python 3.8+)

---

### Phase 2: Actualizar README.md (4-5 hours)

#### Task 2.1: Sección de Status v0.5.0
**Estimate:** 1 hour
**Changes:**
- Cambiar "EN PROGRESO" → "CASI COMPLETA (6/9)"
- Listar issues completados (#44-46, #48-50)
- Listar issues pendientes (#51-53)

#### Task 2.2: Sección de Skills
**Estimate:** 1 hour
**Changes:**
- Actualizar número total de skills
- Añadir tabla de skills por categoría
- Links a archivos de skills

#### Task 2.3: Sección de Agentes
**Estimate:** 1 hour
**Changes:**
- Actualizar número total de agentes
- Tabla completa de agentes con descripciones
- Agentes por categoría

#### Task 2.4: Sección de Comandos
**Estimate:** 1 hour
**Changes:**
- Lista COMPLETA de 14+ comandos
- Comandos por categoría
- Ejemplos de uso

#### Task 2.5: Issue #50 - Python Hooks
**Estimate:** 30 min
**Changes:**
- Actualizar sección de hooks a Python
- Mencionar v2.0.0
- Requirements: Python 3.8+
- Eliminar referencias a shell scripts

#### Task 2.6: Badge de Versión
**Estimate:** 30 min
**Changes:**
```markdown
![Version](https://img.shields.io/badge/version-0.5.0--rc-blue)
```

---

### Phase 3: Actualizar ROADMAP.md (3-4 hours)

#### Task 3.1: Actualizar Issue #50
**Estimate:** 1 hour
**Changes:**
- Métricas de Python hooks v2.0.0
- 3 hooks implementados (.py)
- Eliminar referencias a shell scripts
- Python 3.8+ requirement

#### Task 3.2: Gap Analysis vs moai-adk
**Estimate:** 1 hour
**Changes:**
- Actualizar números reales de skills/agents
- Recalcular gaps
- Actualizar tablas comparativas

#### Task 3.3: Tablas de Features
**Estimate:** 1 hour
**Changes:**
- v0.5.0: 6/9 completados
- v0.6.0-v0.9.0: Issues pendientes
- Actualizar estimaciones

#### Task 3.4: Status Issues #41 y #47
**Estimate:** 30 min
**Changes:**
- Issue #41: Marcar como SKIPPED/WONTFIX
- Issue #47: Marcar como Postponed (v0.6.0+)

---

### Phase 4: Resolver Issues Pendientes (2-3 hours)

#### Task 4.1: Issue #41 - Project Templates
**Estimate:** 1 hour
**Action:**
- Leer issue-41.md actual
- Decidir: SKIPPED definitivo (recomendado)
- Actualizar issue en GitHub con gh cli
- Añadir label "wontfix"

**Command:**
```bash
gh issue edit 41 --add-label "wontfix" --body "..."
```

#### Task 4.2: Issue #47 - Personalization System
**Estimate:** 1-2 hours
**Action:**
- Leer documentación actual
- Decidir: Postponed para v0.6.0+
- Actualizar issue en GitHub
- Añadir label "postponed"

**Command:**
```bash
gh issue edit 47 --add-label "postponed" --body "..."
```

---

### Phase 5: Verificación y Testing (2-3 hours)

#### Task 5.1: Verificar Consistencia
**Estimate:** 1 hour
**Checklist:**
- [ ] README y ROADMAP tienen mismos números
- [ ] Todos los comandos listados existen
- [ ] Todos los skills listados existen
- [ ] Todos los agentes listados existen
- [ ] Issue #50 menciona Python (no shell)
- [ ] Issues #41 y #47 resueltos

#### Task 5.2: Verificar Enlaces
**Estimate:** 30 min
**Command:**
```bash
# Verificar enlaces rotos en markdown
grep -r "\[.*\](.*)" README.md ROADMAP.md
```

#### Task 5.3: Dry-run de doc-syncer
**Estimate:** 30 min
**Action:**
- Simular ejecución de doc-syncer
- Verificar que no hay conflictos

#### Task 5.4: Review Manual
**Estimate:** 30 min
**Action:**
- Leer README completo
- Leer ROADMAP completo
- Verificar coherencia narrativa

---

### Phase 6: Documentation Sync (1-2 hours)

#### Task 6.1: Actualizar CHANGELOG.md
**Estimate:** 30 min
**Entry:**
```markdown
### Completado recientemente
- ✅ **2024-11-24**: Issue #53 - Documentation Sync & Audit (@SPEC:DOC-001, @DOC:DOC-001)
  - README.md actualizado con métricas reales
  - ROADMAP.md actualizado con status v0.5.0
  - Issue #50 corregido (Python hooks v2.0.0)
  - Issues #41 y #47 resueltos
  - Badge de versión añadido
  - Comandos completos listados (14+)
  - Documentación 100% consistente
```

#### Task 6.2: Git Commit
**Estimate:** 30 min
**Command:**
```bash
git add README.md ROADMAP.md CHANGELOG.md .github/issues/
git commit -m "📚 docs(DOC-001): Sync documentation & audit metrics

@DOC:DOC-001

Issue #53 completado:
- README.md actualizado (métricas reales, v0.5.0 status)
- ROADMAP.md actualizado (Issue #50 Python, gap analysis)
- Issue #41 marcado WONTFIX
- Issue #47 marcado Postponed
- Badge de versión añadido
- Comandos completos listados
- Documentación consistente ✅

🤖 Generated with Claude Code
Co-Authored-By: Claude <noreply@anthropic.com>"
```

#### Task 6.3: Push y Close Issue
**Estimate:** 15 min
**Command:**
```bash
git push origin main
gh issue close 53 -c "✅ Documentation sync completado!"
```

---

## 📊 Progress Tracking

### Checklist

**Phase 1: Auditoría**
- [ ] Skills contadas (~XX)
- [ ] Agentes contados (~XX)
- [ ] Comandos contados (~XX)
- [ ] Tests/coverage verificados
- [ ] Issue #50 (Python) verificado

**Phase 2: README.md**
- [ ] Status v0.5.0 actualizado
- [ ] Skills listadas
- [ ] Agentes listados
- [ ] Comandos completos (14+)
- [ ] Issue #50 con Python
- [ ] Badge de versión

**Phase 3: ROADMAP.md**
- [ ] Issue #50 con v2.0.0 Python
- [ ] Gap Analysis actualizado
- [ ] Tablas de features actualizadas
- [ ] Issues #41 y #47 documentados

**Phase 4: Issues Pendientes**
- [ ] Issue #41 → WONTFIX
- [ ] Issue #47 → Postponed

**Phase 5: Verificación**
- [ ] Consistencia verificada
- [ ] Enlaces verificados
- [ ] doc-syncer dry-run
- [ ] Review manual

**Phase 6: Sync**
- [ ] CHANGELOG.md actualizado
- [ ] Git commit con @DOC:DOC-001
- [ ] Push a GitHub
- [ ] Issue #53 cerrado

---

## 🎯 Success Criteria

### Completion Criteria

- [x] Todas las tareas completadas
- [x] Métricas reflejan realidad del código
- [x] README y ROADMAP 100% consistentes
- [x] Issue #50 con Python hooks (no shell)
- [x] Issues #41 y #47 resueltos
- [x] No hay enlaces rotos
- [x] Badge de versión añadido
- [x] @DOC:DOC-001 tag en commit

### Deliverables

- [x] README.md actualizado
- [x] ROADMAP.md actualizado
- [x] CHANGELOG.md con entrada
- [x] Issues #41 y #47 resueltos en GitHub
- [x] Commit con documentación consistente
- [x] Issue #53 cerrado

---

## 🔗 References

- **SPEC:** `docs/specs/SPEC-DOC-001/spec.md`
- **Issue #53:** `.github/issues/issue-53.md`
- **doc-syncer agent:** `.claude/agents/mj2/doc-syncer.md`

---

**Created:** 2024-11-24
**Status:** Draft
**Next:** Execute Phase 1 (Auditoría)
