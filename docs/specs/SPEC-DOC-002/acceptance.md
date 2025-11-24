# SPEC-DOC-002: Acceptance Criteria

**SPEC ID:** SPEC-DOC-002
**Created:** 2024-11-24
**Tags:** @SPEC:DOC-002

---

## ✅ Acceptance Criteria

### 1. docs-manager Agent Created

#### AC-1.1: Agent File Exists
**GIVEN** SPEC-DOC-002 implemented
**WHEN** checking agent directory
**THEN** `.claude/agents/mj2/docs-manager.md` exists with ~750 lines

**Verification:**
```bash
[ -f ".claude/agents/mj2/docs-manager.md" ]
wc -l .claude/agents/mj2/docs-manager.md | grep -E "7[0-9]{2}|8[0-9]{2}"
```

#### AC-1.2: Agent Has Required Sections
**GIVEN** docs-manager.md file
**WHEN** reading content
**THEN** contains all required sections:
- Agent Persona
- Responsibilities (7 items minimum)
- Workflow (AUDIT → UPDATE → GENERATE → PUBLISH)
- Data Sources
- Output Format ("Mr. mj2 recomienda")
- Examples
- Constraints
- References

**Verification:**
```bash
grep -q "## 🎭 Agent Persona" .claude/agents/mj2/docs-manager.md
grep -q "## 📋 Responsibilities" .claude/agents/mj2/docs-manager.md
grep -q "## 🔄 Workflow" .claude/agents/mj2/docs-manager.md
grep -q "AUDIT.*UPDATE.*GENERATE.*PUBLISH" .claude/agents/mj2/docs-manager.md
```

---

### 2. /mj2:docs Command Implemented

#### AC-2.1: Command File Exists
**GIVEN** SPEC-DOC-002 implemented
**WHEN** checking commands directory
**THEN** `.claude/commands/mj2-docs.md` exists with ~200 lines

**Verification:**
```bash
[ -f ".claude/commands/mj2-docs.md" ]
wc -l .claude/commands/mj2-docs.md | grep -E "1[5-9][0-9]|2[0-9]{2}"
```

#### AC-2.2: Command Actions Supported
**GIVEN** /mj2:docs command
**WHEN** reviewing documentation
**THEN** supports 4 actions:
- `audit` - Documentation audit
- `update` - Update README/CHANGELOG
- `generate` - Generate missing docs
- `publish` - Publish to GitHub Pages

**Verification:**
```bash
grep -q "audit" .claude/commands/mj2-docs.md
grep -q "update" .claude/commands/mj2-docs.md
grep -q "generate" .claude/commands/mj2-docs.md
grep -q "publish" .claude/commands/mj2-docs.md
```

---

### 3. Documentation Templates Created

#### AC-3.1: Template Directory Exists
**GIVEN** SPEC-DOC-002 implemented
**WHEN** checking templates directory
**THEN** `.claude/templates/docs/` exists with 5 templates

**Verification:**
```bash
[ -d ".claude/templates/docs" ]
ls .claude/templates/docs/*.md | wc -l | grep "5"
```

#### AC-3.2: Required Templates Present
**GIVEN** templates directory
**WHEN** listing files
**THEN** contains:
- README.md template
- CHANGELOG.md template
- ADR.md template
- CONTRIBUTING.md template
- CODE_OF_CONDUCT.md template

**Verification:**
```bash
[ -f ".claude/templates/docs/README.md" ]
[ -f ".claude/templates/docs/CHANGELOG.md" ]
[ -f ".claude/templates/docs/ADR.md" ]
[ -f ".claude/templates/docs/CONTRIBUTING.md" ]
[ -f ".claude/templates/docs/CODE_OF_CONDUCT.md" ]
```

---

### 4. README Management

#### AC-4.1: README Audit Works
**GIVEN** project with README.md
**WHEN** running `/mj2:docs audit`
**THEN** reports:
- Title presence
- Badges presence
- Installation section
- Usage section
- Examples section
- License section

**Manual Test:**
```bash
# Create test project with incomplete README
# Run: /mj2:docs audit
# Verify: Missing sections reported
```

#### AC-4.2: README Update Works
**GIVEN** project with outdated README
**WHEN** running `/mj2:docs update`
**THEN** updates:
- Version badge to current version
- Feature list from SPEC docs
- Installation instructions
- Quick Start examples

**Manual Test:**
```bash
# Update version in config.json
# Run: /mj2:docs update
# Verify: README version badge updated
```

---

### 5. CHANGELOG Management

#### AC-5.1: CHANGELOG Audit Works
**GIVEN** project with CHANGELOG.md
**WHEN** running `/mj2:docs audit`
**THEN** verifies:
- Keep a Changelog format
- Unreleased section present
- Versions in descending order
- Semantic Versioning compliance

**Verification:**
```bash
# CHANGELOG must follow Keep a Changelog format
# Sections: Added, Changed, Deprecated, Removed, Fixed, Security
```

#### AC-5.2: CHANGELOG Generation Works
**GIVEN** new release created
**WHEN** running `/mj2:docs update`
**THEN** generates CHANGELOG entry with:
- Version and date
- Categorized changes (Added/Changed/etc.)
- Links to commits/PRs
- Breaking changes highlighted

**Manual Test:**
```bash
# Create release with /mj2:99-release
# Run: /mj2:docs update
# Verify: CHANGELOG has new entry
```

---

### 6. API Documentation

#### AC-6.1: API Docs Generation
**GIVEN** ASP.NET Core project with API controllers
**WHEN** running `/mj2:docs generate`
**THEN** generates:
- OpenAPI/Swagger spec
- API endpoint documentation
- Request/response schemas
- Authentication documentation

**Manual Test:**
```bash
# Add API controller
# Run: /mj2:docs generate
# Verify: docs/api/ contains generated docs
```

---

### 7. Architecture Documentation

#### AC-7.1: C4 Diagrams Generation
**GIVEN** project architecture
**WHEN** running `/mj2:docs generate`
**THEN** generates:
- C4 Context diagram (Mermaid)
- C4 Container diagram (Mermaid)
- System overview

**Manual Test:**
```bash
# Run: /mj2:docs generate
# Verify: docs/architecture/ contains C4 diagrams
```

#### AC-7.2: ADR Template Available
**GIVEN** architecture decision needed
**WHEN** requesting ADR template
**THEN** provides template with:
- Status section
- Context section
- Decision section
- Consequences section

**Verification:**
```bash
[ -f ".claude/templates/docs/ADR.md" ]
grep -q "## Status" .claude/templates/docs/ADR.md
grep -q "## Context" .claude/templates/docs/ADR.md
grep -q "## Decision" .claude/templates/docs/ADR.md
grep -q "## Consequences" .claude/templates/docs/ADR.md
```

---

### 8. Integration Tests

#### AC-8.1: Integration with doc-syncer
**GIVEN** docs-manager updates documentation
**WHEN** TAG sync needed
**THEN** delegates to doc-syncer for TAG chain

**Test:**
```bash
# Run: /mj2:docs update
# Verify: doc-syncer called for TAG sync
# Verify: @DOC tags added to commits
```

#### AC-8.2: Integration with api-designer
**GIVEN** API structure from api-designer
**WHEN** generating API docs
**THEN** uses api-designer data

**Test:**
```bash
# Run: /mj2:api-design
# Run: /mj2:docs generate
# Verify: API docs match api-designer output
```

#### AC-8.3: Integration with release-manager
**GIVEN** release created
**WHEN** CHANGELOG updated
**THEN** uses release-manager data

**Test:**
```bash
# Run: /mj2:99-release
# Verify: docs-manager called automatically
# Verify: CHANGELOG entry added
```

#### AC-8.4: Integration with quality-gate
**GIVEN** quality check runs
**WHEN** checking documentation
**THEN** quality-gate includes documentation coverage

**Test:**
```bash
# Run: /mj2:quality-check
# Verify: Documentation coverage checked
# Verify: README completeness checked
```

---

### 9. Publishing

#### AC-9.1: GitHub Pages Support
**GIVEN** documentation ready
**WHEN** running `/mj2:docs publish`
**THEN** prepares for GitHub Pages:
- Creates docs/ folder structure
- Generates _config.yml
- Creates navigation
- Generates index.md

**Verification:**
```bash
[ -f "docs/_config.yml" ]
[ -f "docs/index.md" ]
[ -d "docs/api" ]
[ -d "docs/architecture" ]
```

---

### 10. Output Format

#### AC-10.1: Mr. mj2 Format
**GIVEN** any docs-manager action completed
**WHEN** showing output
**THEN** uses "Mr. mj2 recomienda" format:

```
✅ [Action] completed: [details]

🤖 Mr. mj2 recomienda:
   1. [Next step]
   2. [Alternative action]
   3. Ver estado: /mj2:status

📊 Estado actual:
   [Metrics]

💡 Tip: [Helpful tip]
```

**Manual Test:**
```bash
# Run: /mj2:docs audit
# Verify: Output follows Mr. mj2 format
```

---

### 11. Documentation Updated

#### AC-11.1: README Updated
**GIVEN** Issue #56 completed
**WHEN** checking README.md
**THEN** includes:
- /mj2:docs command documentation
- docs-manager agent in agent list

**Verification:**
```bash
grep -q "/mj2:docs" README.md
grep -q "docs-manager" README.md
```

#### AC-11.2: ROADMAP Updated
**GIVEN** Issue #56 completed
**WHEN** checking ROADMAP.md
**THEN** Issue #56 marked COMPLETED

**Verification:**
```bash
grep -A 5 "Issue #56" docs/ROADMAP.md | grep -q "✅ COMPLETADO"
```

#### AC-11.3: CHANGELOG Updated
**GIVEN** Issue #56 completed
**WHEN** checking CHANGELOG.md
**THEN** has entry for Issue #56

**Verification:**
```bash
grep -q "Issue #56" CHANGELOG.md
grep -q "@CODE:DOC-002" CHANGELOG.md
grep -q "@DOC:DOC-002" CHANGELOG.md
```

---

### 12. TAG Chain Complete

#### AC-12.1: SPEC Tag Present
**GIVEN** SPEC created
**WHEN** checking git log
**THEN** has commit with @SPEC:DOC-002

**Verification:**
```bash
git log --grep="@SPEC:DOC-002" --oneline | wc -l | grep -E "[1-9]"
```

#### AC-12.2: CODE Tag Present
**GIVEN** agent and command implemented
**WHEN** checking git log
**THEN** has commit(s) with @CODE:DOC-002

**Verification:**
```bash
git log --grep="@CODE:DOC-002" --oneline | wc -l | grep -E "[1-9]"
```

#### AC-12.3: DOC Tag Present
**GIVEN** documentation updated
**WHEN** checking git log
**THEN** has commit with @DOC:DOC-002

**Verification:**
```bash
git log --grep="@DOC:DOC-002" --oneline | wc -l | grep -E "[1-9]"
```

---

## 📋 Manual Testing Checklist

### Pre-Implementation Checklist

- [ ] SPEC-DOC-002 reviewed and approved
- [ ] Dependencies verified (doc-syncer, api-designer, release-manager)
- [ ] Templates designed
- [ ] Integration points identified

### Implementation Checklist

- [ ] docs-manager.md agent created (~750 lines)
- [ ] /mj2:docs command created (~200 lines)
- [ ] 5 documentation templates created
- [ ] README management implemented
- [ ] CHANGELOG management implemented
- [ ] API docs generation implemented
- [ ] Architecture docs generation implemented
- [ ] Publishing support implemented

### Integration Checklist

- [ ] doc-syncer integration tested
- [ ] api-designer integration tested
- [ ] release-manager integration tested
- [ ] quality-gate integration tested

### Testing Checklist

- [ ] `/mj2:docs audit` works
- [ ] `/mj2:docs update` works
- [ ] `/mj2:docs generate` works
- [ ] `/mj2:docs publish` works
- [ ] README audit/update works
- [ ] CHANGELOG generation works
- [ ] API docs generation works
- [ ] C4 diagrams generation works
- [ ] Templates work
- [ ] Output format correct ("Mr. mj2 recomienda")

### Documentation Checklist

- [ ] README.md updated
- [ ] ROADMAP.md updated
- [ ] CHANGELOG.md updated
- [ ] Examples provided

### Git Checklist

- [ ] SPEC commit with @SPEC:DOC-002
- [ ] CODE commit(s) with @CODE:DOC-002
- [ ] DOC commit with @DOC:DOC-002
- [ ] TAG chain complete
- [ ] Merged to main
- [ ] Pushed to GitHub

### Release Checklist

- [ ] Issue #56 closed
- [ ] All acceptance criteria met
- [ ] End-to-end testing passed

---

## 🎯 Definition of Done

**Issue #56 está DONE cuando:**

1. ✅ docs-manager.md agent creado (~750 líneas)
2. ✅ /mj2:docs command creado (~200 líneas)
3. ✅ 5 documentation templates creados
4. ✅ README management funciona (audit/update)
5. ✅ CHANGELOG management funciona (generation)
6. ✅ API docs generation funciona
7. ✅ Architecture docs generation funciona (C4, ADRs)
8. ✅ Publishing support implementado (GitHub Pages)
9. ✅ Integration con 4 agentes funciona
10. ✅ Output format "Mr. mj2 recomienda" implementado
11. ✅ TAG chain completa (@SPEC → @CODE → @DOC)
12. ✅ Documentation actualizada (README, ROADMAP, CHANGELOG)
13. ✅ End-to-end testing passed
14. ✅ Issue #56 cerrado en GitHub

---

**Created:** 2024-11-24
**Status:** Draft
**Next:** Begin implementation (Phase 2: Core Agent)
