# SPEC-DOC-001: Acceptance Criteria

**SPEC ID:** SPEC-DOC-001
**Created:** 2024-11-24
**Tags:** @SPEC:DOC-001

---

## ✅ Acceptance Criteria

### 1. Auditoría de Métricas

#### AC-1.1: Skills Contadas
**GIVEN** proyecto con skills en `.claude/skills/`
**WHEN** ejecuto auditoría
**THEN** README.md y ROADMAP.md tienen el número EXACTO de skills

**Verification:**
```bash
actual_skills=$(find .claude/skills -name "*.md" -type f | wc -l)
readme_skills=$(grep -o "Skills:.*[0-9]\+" README.md | grep -o "[0-9]\+")
roadmap_skills=$(grep -o "Skills:.*[0-9]\+" ROADMAP.md | grep -o "[0-9]\+")

# Must match
[ "$actual_skills" = "$readme_skills" ] && [ "$actual_skills" = "$roadmap_skills" ]
```

#### AC-1.2: Agentes Contados
**GIVEN** proyecto con agentes en `.claude/agents/mj2/`
**WHEN** ejecuto auditoría
**THEN** README.md y ROADMAP.md tienen el número EXACTO de agentes

**Verification:**
```bash
actual_agents=$(find .claude/agents/mj2 -name "*.md" -type f | wc -l)
readme_agents=$(grep -o "Agentes:.*[0-9]\+" README.md | grep -o "[0-9]\+")
roadmap_agents=$(grep -o "Agentes:.*[0-9]\+" ROADMAP.md | grep -o "[0-9]\+")

# Must match
[ "$actual_agents" = "$readme_agents" ] && [ "$actual_agents" = "$roadmap_agents" ]
```

#### AC-1.3: Comandos Contados
**GIVEN** proyecto con comandos en `.claude/commands/`
**WHEN** ejecuto auditoría
**THEN** README.md lista TODOS los comandos existentes (≥14)

**Verification:**
```bash
actual_commands=$(find .claude/commands -name "*.md" -type f | wc -l)
readme_commands=$(grep -c "^- \`/mj2:" README.md)

# README must list all commands
[ "$actual_commands" -eq "$readme_commands" ]
```

---

### 2. README.md Actualizado

#### AC-2.1: Status v0.5.0 Correcto
**GIVEN** v0.5.0 con 6/9 issues completados
**WHEN** leo README.md
**THEN** veo status "CASI COMPLETA (6/9)" o similar

**Verification:**
```bash
grep -q "6/9\|6 de 9" README.md
```

#### AC-2.2: Issue #50 con Python
**GIVEN** hooks migrados a Python v2.0.0
**WHEN** leo sección de hooks en README
**THEN** menciona Python (NO shell scripts)

**Verification:**
```bash
grep -q "Python" README.md
grep -q "\.py" README.md
! grep -q "\.sh\|shell script" README.md
```

#### AC-2.3: Badge de Versión Presente
**GIVEN** README.md actualizado
**WHEN** abro archivo
**THEN** veo badge de versión en header

**Verification:**
```bash
grep -q "!\[Version\]" README.md
```

#### AC-2.4: Comandos Completos Listados
**GIVEN** 14+ comandos disponibles
**WHEN** leo sección de comandos
**THEN** veo lista COMPLETA (no faltan comandos)

**Verification:**
```bash
# Verificar que estos comandos clave están listados
grep -q "/mj2:0-project" README.md
grep -q "/mj2:1-plan" README.md
grep -q "/mj2:2-run" README.md
grep -q "/mj2:quality-check" README.md
grep -q "/mj2:3-sync" README.md
grep -q "/mj2:status" README.md
grep -q "/mj2:help" README.md
grep -q "/mj2:git-merge" README.md
```

---

### 3. ROADMAP.md Actualizado

#### AC-3.1: Issue #50 con v2.0.0 Python
**GIVEN** Issue #50 completado con Python
**WHEN** leo sección de Issue #50 en ROADMAP
**THEN** menciona Python, v2.0.0, y NO shell scripts

**Verification:**
```bash
grep -A 10 "Issue #50" ROADMAP.md | grep -q "Python"
grep -A 10 "Issue #50" ROADMAP.md | grep -q "v2.0.0"
! grep -A 10 "Issue #50" ROADMAP.md | grep -q "\.sh\|shell"
```

#### AC-3.2: Gap Analysis Actualizado
**GIVEN** números reales de skills/agents
**WHEN** leo Gap Analysis
**THEN** números coinciden con realidad

**Verification:**
```bash
# Verificar que Gap Analysis menciona números coherentes
grep -q "Skills:" ROADMAP.md
grep -q "Agentes:" ROADMAP.md
```

#### AC-3.3: Issues #41 y #47 Documentados
**GIVEN** decisiones tomadas sobre #41 y #47
**WHEN** leo ROADMAP
**THEN** #41 aparece como SKIPPED/WONTFIX y #47 como Postponed

**Verification:**
```bash
grep -i "Issue #41" ROADMAP.md | grep -qi "skipped\|wontfix"
grep -i "Issue #47" ROADMAP.md | grep -qi "postponed"
```

---

### 4. Issues #41 y #47 Resueltos

#### AC-4.1: Issue #41 Marcado WONTFIX
**GIVEN** Issue #41 abierto
**WHEN** ejecuto resolución
**THEN** Issue #41 tiene label "wontfix" en GitHub

**Verification:**
```bash
gh issue view 41 --json labels --jq '.labels[].name' | grep -q "wontfix"
```

#### AC-4.2: Issue #47 Marcado Postponed
**GIVEN** Issue #47 en estado indefinido
**WHEN** ejecuto resolución
**THEN** Issue #47 tiene label "postponed" o está cerrado/actualizado

**Verification:**
```bash
gh issue view 47 --json labels --jq '.labels[].name' | grep -q "postponed"
```

---

### 5. Consistencia General

#### AC-5.1: No Contradicciones
**GIVEN** README.md y ROADMAP.md actualizados
**WHEN** comparo números
**THEN** no hay contradicciones entre ambos

**Manual Verification:**
- [ ] Skills: mismo número en README y ROADMAP
- [ ] Agentes: mismo número en README y ROADMAP
- [ ] Status v0.5.0: coherente entre ambos
- [ ] Issue #50: Python en ambos, NO shell

#### AC-5.2: Enlaces No Rotos
**GIVEN** documentación con enlaces
**WHEN** verifico enlaces
**THEN** todos los enlaces internos son válidos

**Verification:**
```bash
# Verificar que referencias a archivos existen
# (implementar script de verificación)
```

#### AC-5.3: CHANGELOG Actualizado
**GIVEN** cambios en documentación
**WHEN** leo CHANGELOG.md
**THEN** veo entrada para Issue #53 con @DOC:DOC-001

**Verification:**
```bash
grep -q "Issue #53" CHANGELOG.md
grep -q "@DOC:DOC-001" CHANGELOG.md
```

---

### 6. TAG Chain Completa

#### AC-6.1: Commit con @DOC Tag
**GIVEN** documentación actualizada
**WHEN** reviso git log
**THEN** hay commit con @DOC:DOC-001 tag

**Verification:**
```bash
git log --grep="@DOC:DOC-001" --oneline | wc -l | grep -q "1"
```

#### AC-6.2: Issue #53 Cerrado
**GIVEN** trabajo completado
**WHEN** verifico estado de issue
**THEN** Issue #53 está cerrado en GitHub

**Verification:**
```bash
gh issue view 53 --json state --jq '.state' | grep -q "CLOSED"
```

---

## 📋 Checklist de Verificación Manual

### Pre-Commit Checklist

- [ ] **Métricas verificadas:**
  - [ ] Skills contadas: _____ (número)
  - [ ] Agentes contados: _____ (número)
  - [ ] Comandos contados: _____ (número)

- [ ] **README.md:**
  - [ ] Status v0.5.0 correcto (6/9)
  - [ ] Skills listadas con número correcto
  - [ ] Agentes listados con número correcto
  - [ ] Comandos TODOS listados (≥14)
  - [ ] Issue #50 menciona Python (no shell)
  - [ ] Badge de versión presente

- [ ] **ROADMAP.md:**
  - [ ] Issue #50 con Python v2.0.0
  - [ ] Gap Analysis actualizado
  - [ ] Tablas de features actualizadas
  - [ ] Issue #41 como SKIPPED
  - [ ] Issue #47 como Postponed

- [ ] **Issues GitHub:**
  - [ ] Issue #41 tiene label "wontfix"
  - [ ] Issue #47 tiene label "postponed"

- [ ] **Consistencia:**
  - [ ] README y ROADMAP con mismos números
  - [ ] No hay contradicciones
  - [ ] Enlaces válidos

- [ ] **CHANGELOG.md:**
  - [ ] Entrada para Issue #53
  - [ ] Tag @DOC:DOC-001 presente

### Post-Commit Checklist

- [ ] Commit hecho con @DOC:DOC-001
- [ ] Push a GitHub completado
- [ ] Issue #53 cerrado
- [ ] README.md actualizado en GitHub
- [ ] ROADMAP.md actualizado en GitHub

---

## 🎯 Definition of Done

**Issue #53 está DONE cuando:**

1. ✅ Todas las métricas auditadas y correctas
2. ✅ README.md actualizado con datos reales
3. ✅ ROADMAP.md actualizado con datos reales
4. ✅ Issue #50 menciona Python (no shell)
5. ✅ Issues #41 y #47 resueltos en GitHub
6. ✅ Badge de versión añadido
7. ✅ Comandos completos listados
8. ✅ No hay contradicciones entre docs
9. ✅ CHANGELOG.md actualizado
10. ✅ Commit con @DOC:DOC-001
11. ✅ Push a GitHub
12. ✅ Issue #53 cerrado

---

**Created:** 2024-11-24
**Status:** Draft
**Next:** Begin implementation (Phase 1: Auditoría)
