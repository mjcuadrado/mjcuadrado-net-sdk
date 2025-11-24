# SPEC-FMT-055: Implementation Plan

**SPEC ID:** SPEC-FMT-055
**Created:** 2024-11-24
**Status:** Draft
**Tags:** @SPEC:FMT-055

---

## 🎯 Implementation Strategy

### Timeline: 4-5 días (24-30 hours)

**Day 1:** SPEC + Skills (dotnet-format, prettier, eslint)
**Day 2:** Format Expert Agent
**Day 3:** /mj2-format Command + Integration Testing
**Day 4:** Documentation + Final Testing
**Day 5:** Git + Release (si necesario)

---

## 📋 Task Breakdown

### Phase 1: SPEC & Planning (3-4 hours) - DAY 1

#### Task 1.1: Create SPEC Documents
**Estimate:** 2-3 hours
**Deliverables:**
- spec.md (requirements in EARS format)
- plan.md (this file)
- acceptance.md (acceptance criteria)

#### Task 1.2: Feature Branch
**Estimate:** 30 min
**Command:**
```bash
git checkout -b feature/SPEC-FMT-055
git add docs/specs/SPEC-FMT-055/
git commit -m "📋 spec(FMT-055): Add SPEC for Format Expert Agent @SPEC:FMT-055"
```

---

### Phase 2: Skills Creation (8-10 hours) - DAY 1

#### Task 2.1: Create dotnet-format.md Skill
**Estimate:** 3-4 hours
**File:** `.claude/skills/tools/dotnet-format.md` (~300 líneas)

**Structure:**
```markdown
---
name: dotnet-format
description: .NET code formatting with dotnet format CLI
category: tools
difficulty: intermediate
version: 1.0.0
tags: [dotnet, formatting, cli, csharp]
---

# dotnet format Skill

## Overview
dotnet format CLI tool para formatting C# code.

## Installation
```bash
# Incluido en .NET SDK 6+
dotnet --version
```

## Basic Usage
```bash
# Format entire solution
dotnet format

# Format specific project
dotnet format MyProject.csproj

# Check without formatting
dotnet format --verify-no-changes

# Format with severity level
dotnet format --severity info
```

## .editorconfig Integration
```ini
# .editorconfig example
root = true

[*.cs]
indent_style = space
indent_size = 4
end_of_line = lf
charset = utf-8
trim_trailing_whitespace = true
insert_final_newline = true

# C# specific rules
csharp_new_line_before_open_brace = all
csharp_prefer_braces = true:warning
```

## StyleCop Analyzers Integration
```xml
<!-- Add to .csproj -->
<ItemGroup>
  <PackageReference Include="StyleCop.Analyzers" Version="1.2.0-beta.556" />
</ItemGroup>
```

## Common Options
- `--verify-no-changes` - Check mode
- `--include <path>` - Include specific files
- `--exclude <path>` - Exclude specific files
- `--severity <level>` - Filter by severity (info, warn, error)
- `--verbosity <level>` - Output verbosity

## Best Practices
1. Use .editorconfig for consistency
2. Run in CI with --verify-no-changes
3. Format before committing
4. Use StyleCop for advanced rules

## Examples
[3-5 comprehensive examples]

## Troubleshooting
[Common issues and solutions]
```

**Sections to implement:**
- [ ] Overview & Installation
- [ ] Basic Usage (5+ commands)
- [ ] .editorconfig integration
- [ ] StyleCop Analyzers setup
- [ ] Common options (6+ options)
- [ ] Best practices (5+ items)
- [ ] Examples (3-5 scenarios)
- [ ] Troubleshooting (3+ issues)

#### Task 2.2: Create prettier.md Skill
**Estimate:** 2-3 hours
**File:** `.claude/skills/tools/prettier.md` (~250 líneas)

**Structure:**
```markdown
---
name: prettier
description: Opinionated code formatter for JavaScript/TypeScript
category: tools
difficulty: basic
version: 1.0.0
tags: [prettier, formatting, javascript, typescript]
---

# Prettier Skill

## Overview
Opinionated code formatter para JavaScript, TypeScript, CSS, JSON, y más.

## Installation
```bash
npm install --save-dev prettier
# o
yarn add --dev prettier
```

## Basic Usage
```bash
# Format all files
npx prettier --write .

# Format specific file
npx prettier --write src/App.tsx

# Check without formatting
npx prettier --check .

# List files that need formatting
npx prettier --list-different .
```

## Configuration (.prettierrc)
```json
{
  "semi": true,
  "singleQuote": true,
  "tabWidth": 2,
  "trailingComma": "es5",
  "printWidth": 100,
  "arrowParens": "avoid"
}
```

## Integration con TypeScript
```json
// tsconfig.json adjustments
{
  "compilerOptions": {
    "forceConsistentCasingInFileNames": true
  }
}
```

## Integration con ESLint
```bash
npm install --save-dev eslint-config-prettier
```

```json
// .eslintrc.json
{
  "extends": ["eslint:recommended", "prettier"]
}
```

## Common Options
- `--write` - Format and save
- `--check` - Check without formatting
- `--list-different` - List files needing format
- `--config <path>` - Specify config file

## Best Practices
1. Use .prettierrc for team consistency
2. Add to pre-commit hooks
3. Integrate with ESLint
4. Format on save in IDE

## Examples
[3-5 examples]

## Troubleshooting
[Common issues]
```

**Sections to implement:**
- [ ] Overview & Installation
- [ ] Basic Usage (4+ commands)
- [ ] Configuration examples
- [ ] TypeScript integration
- [ ] ESLint integration
- [ ] Common options (4+ options)
- [ ] Best practices (4+ items)
- [ ] Examples (3-5 scenarios)
- [ ] Troubleshooting

#### Task 2.3: Create eslint.md Skill
**Estimate:** 3-4 hours
**File:** `.claude/skills/tools/eslint.md` (~300 líneas)

**Structure:**
```markdown
---
name: eslint
description: Pluggable JavaScript/TypeScript linter
category: tools
difficulty: intermediate
version: 1.0.0
tags: [eslint, linting, javascript, typescript]
---

# ESLint Skill

## Overview
Pluggable linting tool para JavaScript y TypeScript.

## Installation
```bash
npm install --save-dev eslint @typescript-eslint/parser @typescript-eslint/eslint-plugin
```

## Basic Usage
```bash
# Lint all files
npx eslint .

# Lint specific file
npx eslint src/App.tsx

# Auto-fix issues
npx eslint --fix .

# Output JSON format
npx eslint --format json .
```

## Configuration (.eslintrc.json)
```json
{
  "parser": "@typescript-eslint/parser",
  "extends": [
    "eslint:recommended",
    "plugin:@typescript-eslint/recommended",
    "prettier"
  ],
  "rules": {
    "@typescript-eslint/no-unused-vars": "error",
    "@typescript-eslint/no-explicit-any": "warn",
    "no-console": "warn"
  }
}
```

## TypeScript Configuration
```json
// tsconfig.json
{
  "compilerOptions": {
    "strict": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true
  }
}
```

## Auto-fix Capabilities
- Import ordering
- Spacing and indentation
- Quote style
- Semicolons
- Unused imports

## Integration con Prettier
```bash
npm install --save-dev eslint-config-prettier eslint-plugin-prettier
```

## Common Rules
[10+ common rules con examples]

## Best Practices
1. Use TypeScript-ESLint plugin
2. Integrate with prettier
3. Run in CI/CD
4. Use severity levels appropriately

## Examples
[3-5 comprehensive examples]

## Troubleshooting
[Common issues]
```

**Sections to implement:**
- [ ] Overview & Installation
- [ ] Basic Usage (4+ commands)
- [ ] Configuration examples
- [ ] TypeScript setup
- [ ] Auto-fix capabilities
- [ ] Prettier integration
- [ ] Common rules (10+ rules)
- [ ] Best practices (4+ items)
- [ ] Examples (3-5 scenarios)
- [ ] Troubleshooting

---

### Phase 3: Format Expert Agent (6-8 hours) - DAY 2

#### Task 3.1: Create format-expert.md Agent
**Estimate:** 6-8 hours
**File:** `.claude/agents/mj2/format-expert.md` (~650 líneas)

**Structure:**
```markdown
---
name: format-expert
description: Code formatting and linting orchestrator
model: claude-sonnet-4-5-20250929
version: 1.0.0
author: mjcuadrado-net-sdk
tags: [mj2, formatting, quality, linting]
---

# Format Expert Agent

## 🎭 Agent Persona
Formatting Specialist - Obsesivo con consistencia, pragmático con rules.

## 📋 Responsibilities
1. File type detection (.cs, .ts, .tsx, .js, .jsx)
2. Configuration loading (.editorconfig, .prettierrc, .eslintrc)
3. Tool orchestration (dotnet format, prettier, ESLint)
4. Validation and reporting (violations, auto-fixes)
5. Git integration (staged files, check mode)
6. Performance optimization (parallel processing, caching)

## 🔄 Workflow

### Phase 1: ANALYZE
- Detect file types in scope
- Load configuration files
- Determine tools needed
- Check tool availability

### Phase 2: FORMAT
- Run dotnet format for C# files
- Run prettier for TypeScript/JavaScript files
- Apply configuration rules
- Handle errors gracefully

### Phase 3: LINT
- Run StyleCop (via dotnet format)
- Run ESLint for TypeScript/JavaScript
- Collect violations
- Determine auto-fixable issues

### Phase 4: VALIDATE
- Verify formatting applied correctly
- Report violations and fixes
- Suggest next steps
- Update status

## 📊 Data Sources
1. Git status (staged files)
2. Configuration files (.editorconfig, .prettierrc, .eslintrc)
3. File system (file types)
4. Tool outputs (dotnet format, prettier, ESLint)

## 📤 Output Format
[Mr. mj2 recomienda format]

## 🔗 Integration
- dotnet-format skill
- prettier skill
- eslint skill
- Git workflow
- Pre-commit hooks

## 📚 Examples
[3-5 comprehensive scenarios]

## ⚠️ Constraints
[Limitations and caveats]

## 🔍 Troubleshooting
[Common issues and solutions]
```

**Sections to implement:**
- [ ] Agent metadata (frontmatter)
- [ ] Agent Persona
- [ ] Responsibilities (6 items)
- [ ] Workflow (4 phases: ANALYZE → FORMAT → LINT → VALIDATE)
- [ ] Data Sources (4 sources)
- [ ] File type detection logic
- [ ] Configuration loading logic
- [ ] Tool orchestration logic (dotnet format, prettier, ESLint)
- [ ] Git integration (staged files)
- [ ] Performance optimization
- [ ] Error handling
- [ ] Output format ("Mr. mj2 recomienda")
- [ ] Integration points
- [ ] Examples (3-5 scenarios)
- [ ] Constraints
- [ ] Troubleshooting

---

### Phase 4: Command Implementation (3-4 hours) - DAY 3

#### Task 4.1: Create /mj2-format Command
**Estimate:** 2-3 hours
**File:** `.claude/commands/mj2-format.md` (~150 líneas)

**Structure:**
```markdown
---
name: mj2-format
description: Format and lint code with dotnet format, prettier, and ESLint
agent: format-expert
version: 1.0.0
author: mjcuadrado-net-sdk
tags: [mj2, formatting, linting]
---

# /mj2:format - Code Formatting & Linting

Formatea y valida código usando format-expert agent.

## 🎯 Purpose
Automatizar code formatting y linting para garantizar consistencia.

## 📋 Usage

```bash
/mj2:format [path] [OPTIONS]
```

### Required Arguments
- None (defaults to current directory)

### Optional Arguments
- `[path]` - Path to file or directory (default: .)

### Optional Flags
- `--check` - Verify formatting without modifying files (CI mode)
- `--fix` - Auto-fix violations (default)
- `--staged` - Format only staged files (pre-commit mode)

---

## 🔄 Actions

### Action 1: Format Project (Default)
```bash
/mj2:format
```

### Action 2: Check Mode (CI)
```bash
/mj2:format --check
```

### Action 3: Format Staged Files (Pre-commit)
```bash
/mj2:format --staged
```

### Action 4: Format Specific Path
```bash
/mj2:format src/Services --fix
```

---

## 🎨 Examples
[4-5 comprehensive examples con outputs esperados]

---

## ✅ Best Practices
1. Run /mj2:format before committing
2. Use --check in CI/CD
3. Configure pre-commit hook con --staged
4. Review formatting changes before committing

---

## 🔗 Integration
- format-expert agent
- dotnet-format skill
- prettier skill
- eslint skill
- Git workflow

---

**Agent:** format-expert
**Version:** 1.0.0
**Created:** 2024-11-24
**Tags:** @CODE:FMT-055
```

**Sections to implement:**
- [ ] Command metadata (frontmatter)
- [ ] Purpose statement
- [ ] Usage syntax
- [ ] Options documentation (--check, --fix, --staged)
- [ ] Actions (4 main scenarios)
- [ ] Examples (4-5 con outputs)
- [ ] Best practices
- [ ] Integration points
- [ ] Error handling
- [ ] Help text

#### Task 4.2: Integration Testing
**Estimate:** 1-2 hours
**Test scenarios:**
1. Format C# project (dotnet format)
2. Format TypeScript project (prettier + ESLint)
3. Format full-stack project (both)
4. Check mode (--check)
5. Staged files (--staged)
6. Error handling (missing tools)

---

### Phase 5: Documentation & Testing (4-5 hours) - DAY 4

#### Task 5.1: Update README.md
**Estimate:** 1 hour
**Add:**
- format-expert to agent list
- /mj2:format to command list
- Update agent count: 24 → 25
- Update command count: 24 → 25
- Update v0.6.0 section

#### Task 5.2: Update ROADMAP.md
**Estimate:** 1 hour
**Mark:**
- Issue #55 as COMPLETADO
- v0.6.0 as COMPLETADO (4/4 - 100%)
- Gap Analysis: 25 agentes, 25 comandos, 49 skills
- Visual roadmap updated

#### Task 5.3: Update CHANGELOG.md
**Estimate:** 1 hour
**Entry:**
```markdown
- ✅ **2024-11-24**: Issue #55 - Format Expert Agent
  - format-expert.md agent (~650 líneas)
  - /mj2:format command (~150 líneas)
  - 3 skills: dotnet-format, prettier, eslint
  - Git integration (staged files)
  - Configuration auto-detection
```

#### Task 5.4: End-to-End Testing
**Estimate:** 2 hours
**Test scenarios:**
- Format C# project
- Format React project
- Format full-stack project
- CI check mode
- Pre-commit workflow
- Error scenarios

---

### Phase 6: Git & Release (2-3 hours) - DAY 4-5

#### Task 6.1: Git Commits
**Estimate:** 1 hour
**Commits:**
```bash
# SPEC commit (already done)
git commit -m "📋 spec(FMT-055): Add SPEC @SPEC:FMT-055"

# CODE commit
git add .claude/agents/mj2/format-expert.md
git add .claude/commands/mj2-format.md
git add .claude/skills/tools/dotnet-format.md
git add .claude/skills/tools/prettier.md
git add .claude/skills/tools/eslint.md
git commit -m "🟢 feat(FMT-055): Add format-expert agent & skills @CODE:FMT-055"

# DOC commit
git add README.md ROADMAP.md CHANGELOG.md
git commit -m "📚 docs(FMT-055): Update documentation @DOC:FMT-055"
```

#### Task 6.2: Merge & Push
**Estimate:** 30 min
```bash
git checkout main
git merge feature/SPEC-FMT-055
git push origin main
```

#### Task 6.3: Close Issue
**Estimate:** 30 min
```bash
gh issue close 55 -c "✅ Issue #55 completado!"
```

---

## 📊 Progress Tracking

### Checklist

**Phase 1: SPEC & Planning**
- [ ] spec.md created
- [ ] plan.md created
- [ ] acceptance.md created
- [ ] Feature branch created
- [ ] SPEC commit

**Phase 2: Skills**
- [ ] dotnet-format.md (~300 líneas)
- [ ] prettier.md (~250 líneas)
- [ ] eslint.md (~300 líneas)

**Phase 3: Agent**
- [ ] format-expert.md (~650 líneas)
- [ ] 4-phase workflow implemented
- [ ] 6 responsibilities documented
- [ ] Skills integration

**Phase 4: Command**
- [ ] /mj2-format command (~150 líneas)
- [ ] All options supported
- [ ] Integration testing passed

**Phase 5: Documentation**
- [ ] README.md updated
- [ ] ROADMAP.md updated
- [ ] CHANGELOG.md updated
- [ ] End-to-end testing passed

**Phase 6: Git & Release**
- [ ] CODE commit (@CODE:FMT-055)
- [ ] DOC commit (@DOC:FMT-055)
- [ ] Merge to main
- [ ] Issue #55 closed

---

## 🎯 Success Criteria

### Completion Criteria

- [ ] SPEC completa (spec.md, plan.md, acceptance.md)
- [ ] format-expert.md agent (~650 líneas)
- [ ] /mj2-format command (~150 líneas)
- [ ] 3 skills created (~850 líneas total)
- [ ] Formatting funciona (C#, TypeScript, JavaScript)
- [ ] Git integration funciona (staged, check, fix)
- [ ] Configuration detection funciona
- [ ] TAG chain completa (@SPEC → @CODE → @DOC)
- [ ] Documentation updated (README, ROADMAP, CHANGELOG)
- [ ] v0.6.0 COMPLETADO (4/4 - 100%)

### Deliverables

- [ ] format-expert.md agent
- [ ] /mj2-format command
- [ ] 3 skills (dotnet-format, prettier, eslint)
- [ ] SPEC-FMT-055 documents
- [ ] Documentation updated

---

## 🔗 References

- **SPEC:** `docs/specs/SPEC-FMT-055/spec.md`
- **Issue #55:** `.github/issues/issue-55.md`
- **moai-adk:** format-expert agent
- **dotnet format:** https://github.com/dotnet/format
- **Prettier:** https://prettier.io/
- **ESLint:** https://eslint.org/

---

**Created:** 2024-11-24
**Status:** Draft
**Next:** Execute Phase 1 (SPEC & Planning)
