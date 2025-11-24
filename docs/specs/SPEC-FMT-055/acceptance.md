# SPEC-FMT-055: Acceptance Criteria

**SPEC ID:** SPEC-FMT-055
**Created:** 2024-11-24
**Tags:** @SPEC:FMT-055

---

## ✅ Acceptance Criteria

### 1. format-expert Agent Created

#### AC-1.1: Agent File Exists
**GIVEN** SPEC-FMT-055 implemented
**WHEN** checking agent directory
**THEN** `.claude/agents/mj2/format-expert.md` exists with ~650 lines

**Verification:**
```bash
[ -f ".claude/agents/mj2/format-expert.md" ]
wc -l .claude/agents/mj2/format-expert.md | grep -E "[56][0-9]{2}|700"
```

#### AC-1.2: Agent Has Required Sections
**GIVEN** format-expert.md file
**WHEN** reading content
**THEN** contains all required sections:
- Agent Persona
- Responsibilities (6 items minimum)
- Workflow (ANALYZE → FORMAT → LINT → VALIDATE)
- Data Sources
- Output Format ("Mr. mj2 recomienda")
- Examples
- Constraints
- Troubleshooting

**Verification:**
```bash
grep -q "## 🎭 Agent Persona" .claude/agents/mj2/format-expert.md
grep -q "## 📋 Responsibilities" .claude/agents/mj2/format-expert.md
grep -q "## 🔄 Workflow" .claude/agents/mj2/format-expert.md
grep -q "ANALYZE.*FORMAT.*LINT.*VALIDATE" .claude/agents/mj2/format-expert.md
```

---

### 2. /mj2-format Command Implemented

#### AC-2.1: Command File Exists
**GIVEN** SPEC-FMT-055 implemented
**WHEN** checking commands directory
**THEN** `.claude/commands/mj2-format.md` exists with ~150 lines

**Verification:**
```bash
[ -f ".claude/commands/mj2-format.md" ]
wc -l .claude/commands/mj2-format.md | grep -E "1[0-4][0-9]|1[5-9][0-9]"
```

#### AC-2.2: Command Options Supported
**GIVEN** /mj2-format command
**WHEN** reviewing command
**THEN** supports options:
- `[path]` - Optional path parameter
- `--check` - Verify without modifying
- `--fix` - Auto-fix violations (default)
- `--staged` - Format only staged files

**Verification:**
```bash
grep -q "\-\-check" .claude/commands/mj2-format.md
grep -q "\-\-fix" .claude/commands/mj2-format.md
grep -q "\-\-staged" .claude/commands/mj2-format.md
grep -q "\[path\]" .claude/commands/mj2-format.md
```

---

### 3. Skills Created

#### AC-3.1: dotnet-format Skill Exists
**GIVEN** SPEC-FMT-055 implemented
**WHEN** checking skills directory
**THEN** `.claude/skills/tools/dotnet-format.md` exists with ~300 lines

**Verification:**
```bash
[ -f ".claude/skills/tools/dotnet-format.md" ]
wc -l .claude/skills/tools/dotnet-format.md | grep -E "[23][0-9]{2}|3[0-4][0-9]"
```

**THEN** contains required sections:
- Installation instructions
- Basic usage (5+ commands)
- .editorconfig integration
- StyleCop Analyzers setup
- Common options (6+ options)
- Best practices (5+ items)
- Examples (3+ scenarios)
- Troubleshooting

#### AC-3.2: prettier Skill Exists
**GIVEN** SPEC-FMT-055 implemented
**WHEN** checking skills directory
**THEN** `.claude/skills/tools/prettier.md` exists with ~250 lines

**Verification:**
```bash
[ -f ".claude/skills/tools/prettier.md" ]
wc -l .claude/skills/tools/prettier.md | grep -E "[12][0-9]{2}|300"
```

**THEN** contains required sections:
- Installation instructions
- Basic usage (4+ commands)
- Configuration examples (.prettierrc)
- TypeScript integration
- ESLint integration
- Common options (4+ options)
- Best practices (4+ items)
- Examples (3+ scenarios)

#### AC-3.3: eslint Skill Exists
**GIVEN** SPEC-FMT-055 implemented
**WHEN** checking skills directory
**THEN** `.claude/skills/tools/eslint.md` exists with ~300 lines

**Verification:**
```bash
[ -f ".claude/skills/tools/eslint.md" ]
wc -l .claude/skills/tools/eslint.md | grep -E "[23][0-9]{2}|3[0-4][0-9]"
```

**THEN** contains required sections:
- Installation instructions
- Basic usage (4+ commands)
- Configuration examples (.eslintrc.json)
- TypeScript setup
- Auto-fix capabilities
- Prettier integration
- Common rules (10+ rules)
- Best practices (4+ items)
- Examples (3+ scenarios)

---

### 4. File Type Detection Works

#### AC-4.1: C# Files Detected
**GIVEN** project with .cs files
**WHEN** running `/mj2:format`
**THEN** identifies C# files correctly
AND uses dotnet format tool

**Manual Test:**
```bash
# Create test C# file
echo "public class Test{void Method(){}}" > Test.cs
# Run format
/mj2:format
# Verify: dotnet format used
```

#### AC-4.2: TypeScript Files Detected
**GIVEN** project with .ts/.tsx files
**WHEN** running `/mj2:format`
**THEN** identifies TypeScript files correctly
AND uses prettier + ESLint tools

**Manual Test:**
```bash
# Create test TypeScript file
echo "const x:string='test'" > Test.ts
# Run format
/mj2:format
# Verify: prettier + ESLint used
```

#### AC-4.3: JavaScript Files Detected
**GIVEN** project with .js/.jsx files
**WHEN** running `/mj2:format`
**THEN** identifies JavaScript files correctly
AND uses prettier + ESLint tools

---

### 5. Configuration Detection Works

#### AC-5.1: .editorconfig Detection
**GIVEN** project with .editorconfig file
**WHEN** formatting C# files
**THEN** .editorconfig rules applied
AND configuration source logged

**Manual Test:**
```bash
# Create .editorconfig
echo "[*.cs]\nindent_size = 4" > .editorconfig
# Run format
/mj2:format
# Verify: .editorconfig detected and applied
```

#### AC-5.2: .prettierrc Detection
**GIVEN** project with .prettierrc file
**WHEN** formatting TypeScript/JavaScript files
**THEN** .prettierrc rules applied
AND configuration source logged

**Manual Test:**
```bash
# Create .prettierrc
echo '{"singleQuote": true}' > .prettierrc
# Run format
/mj2:format
# Verify: .prettierrc detected and applied
```

#### AC-5.3: .eslintrc Detection
**GIVEN** project with .eslintrc.json file
**WHEN** linting TypeScript/JavaScript files
**THEN** .eslintrc rules applied
AND configuration source logged

**Manual Test:**
```bash
# Create .eslintrc.json
echo '{"rules": {"no-console": "warn"}}' > .eslintrc.json
# Run format
/mj2:format
# Verify: .eslintrc detected and applied
```

---

### 6. Formatting Works

#### AC-6.1: C# Formatting
**GIVEN** C# file with formatting issues
**WHEN** running `/mj2:format --fix`
**THEN** formatting applied correctly
AND code functionality preserved

**Test:**
```csharp
// Before
public class Test{void Method(){var x=1;}}

// After (expected)
public class Test
{
    void Method()
    {
        var x = 1;
    }
}
```

#### AC-6.2: TypeScript Formatting
**GIVEN** TypeScript file with formatting issues
**WHEN** running `/mj2:format --fix`
**THEN** formatting applied correctly
AND code functionality preserved

**Test:**
```typescript
// Before
const x:string="test";function foo(){return x;}

// After (expected)
const x: string = 'test';

function foo() {
  return x;
}
```

#### AC-6.3: JavaScript Formatting
**GIVEN** JavaScript file with formatting issues
**WHEN** running `/mj2:format --fix`
**THEN** formatting applied correctly

---

### 7. Linting Works

#### AC-7.1: ESLint Validation
**GIVEN** TypeScript/JavaScript file with linting issues
**WHEN** running `/mj2:format --check`
**THEN** violations reported with file:line:column
AND auto-fixable issues identified

**Test:**
```typescript
// File with issues
const unused = 'test'; // unused variable
console.log("Hello"); // prefer single quotes

// Expected output:
// src/test.ts:1:7 - @typescript-eslint/no-unused-vars (auto-fixable)
// src/test.ts:2:13 - prettier/prettier (auto-fixable)
```

#### AC-7.2: StyleCop Validation
**GIVEN** C# file with StyleCop violations
**WHEN** running `/mj2:format --check`
**THEN** violations reported
AND auto-fixable issues identified

---

### 8. Git Integration Works

#### AC-8.1: Staged Files Detection
**GIVEN** git repository with staged files
**WHEN** running `/mj2:format --staged`
**THEN** only staged files formatted
AND unstaged files unchanged

**Manual Test:**
```bash
# Stage specific file
git add Test.cs
# Run format on staged
/mj2:format --staged
# Verify: only Test.cs formatted
```

#### AC-8.2: Check Mode (CI)
**GIVEN** project with formatting issues
**WHEN** running `/mj2:format --check`
**THEN** violations reported
AND no files modified

**Manual Test:**
```bash
# Run check mode
/mj2:format --check
# Verify: files not modified
git status  # should show no changes
```

#### AC-8.3: Fix Mode
**GIVEN** project with formatting issues
**WHEN** running `/mj2:format --fix`
**THEN** violations auto-corrected
AND files modified

**Manual Test:**
```bash
# Run fix mode
/mj2:format --fix
# Verify: files modified
git status  # should show changes
```

---

### 9. Performance Acceptable

#### AC-9.1: Small Project Performance
**GIVEN** project with < 50 files
**WHEN** running `/mj2:format`
**THEN** completes in ≤ 5 seconds

**Verification:**
```bash
time /mj2:format
# Should output: real ≤ 5s
```

#### AC-9.2: Medium Project Performance
**GIVEN** project with < 500 files
**WHEN** running `/mj2:format`
**THEN** completes in ≤ 30 seconds

**Verification:**
```bash
time /mj2:format
# Should output: real ≤ 30s
```

---

### 10. Error Handling Robust

#### AC-10.1: Missing Tool Detection
**GIVEN** dotnet CLI not installed
**WHEN** attempting to format C# files
**THEN** error message displayed
AND suggests installation steps

**Manual Test:**
```bash
# Temporarily rename dotnet
# Run format
/mj2:format
# Expected: "dotnet CLI not found. Install .NET SDK 9+"
```

#### AC-10.2: Invalid Path Handling
**GIVEN** non-existent path specified
**WHEN** running `/mj2:format /invalid/path`
**THEN** error message displayed
AND suggests correct usage

#### AC-10.3: Configuration Error Handling
**GIVEN** invalid .prettierrc syntax
**WHEN** running format
**THEN** error message displayed
AND suggests configuration fix

---

### 11. Output Format Consistent

#### AC-11.1: "Mr. mj2 recomienda" Format
**GIVEN** format operation completed
**WHEN** viewing output
**THEN** uses "Mr. mj2 recomienda" format:

```
✅ Formato aplicado exitosamente

📊 Formato Overview:
   - Files processed: X
   - C# files: Y
   - TypeScript files: Z
   - Violations corrected: N

🤖 Mr. mj2 recomienda:
   1. Review changes: git diff
   2. Commit formatted code
   3. Configure pre-commit hook
```

---

### 12. Integration with Other Agents

#### AC-12.1: Integration with quality-gate
**GIVEN** format-expert and quality-gate agents
**WHEN** quality-gate validates code
**THEN** can invoke format-expert for formatting checks
AND formatting violations reported in quality metrics

#### AC-12.2: Integration with pre-commit hooks
**GIVEN** pre-commit hook configured
**WHEN** attempting to commit unformatted code
**THEN** format-expert runs automatically
AND blocks commit if formatting fails

---

### 13. Documentation Updated

#### AC-13.1: README Updated
**GIVEN** Issue #55 completed
**WHEN** checking README.md
**THEN** includes:
- format-expert in agent list
- /mj2:format in command list
- Agent count: 24 → 25
- Command count: 24 → 25

**Verification:**
```bash
grep -q "format-expert" README.md
grep -q "/mj2:format" README.md
grep -q "25 agentes" README.md
grep -q "25 comandos" README.md
```

#### AC-13.2: ROADMAP Updated
**GIVEN** Issue #55 completed
**WHEN** checking ROADMAP.md
**THEN** Issue #55 marked COMPLETADO
AND v0.6.0 marked as COMPLETADO (4/4 - 100%)

**Verification:**
```bash
grep -A 5 "Issue #55" docs/ROADMAP.md | grep -q "✅ COMPLETADO"
grep -q "v0.6.0.*4/4" docs/ROADMAP.md
```

#### AC-13.3: CHANGELOG Updated
**GIVEN** Issue #55 completed
**WHEN** checking CHANGELOG.md
**THEN** has entry for Issue #55

**Verification:**
```bash
grep -q "Issue #55" CHANGELOG.md
grep -q "@CODE:FMT-055" CHANGELOG.md
grep -q "@DOC:FMT-055" CHANGELOG.md
```

---

### 14. TAG Chain Complete

#### AC-14.1: SPEC Tag Present
**GIVEN** SPEC created
**WHEN** checking git log
**THEN** has commit with @SPEC:FMT-055

**Verification:**
```bash
git log --grep="@SPEC:FMT-055" --oneline | wc -l | grep -E "[1-9]"
```

#### AC-14.2: CODE Tag Present
**GIVEN** agent and command implemented
**WHEN** checking git log
**THEN** has commit(s) with @CODE:FMT-055

**Verification:**
```bash
git log --grep="@CODE:FMT-055" --oneline | wc -l | grep -E "[1-9]"
```

#### AC-14.3: DOC Tag Present
**GIVEN** documentation updated
**WHEN** checking git log
**THEN** has commit with @DOC:FMT-055

**Verification:**
```bash
git log --grep="@DOC:FMT-055" --oneline | wc -l | grep -E "[1-9]"
```

---

## 📋 Manual Testing Checklist

### Pre-Implementation Checklist

- [ ] SPEC-FMT-055 reviewed and approved
- [ ] Dependencies verified (dotnet CLI, Node.js, npm)
- [ ] Agent structure designed
- [ ] Skills structure designed
- [ ] Integration points identified

### Implementation Checklist

- [ ] format-expert.md agent created (~650 lines)
- [ ] /mj2-format command created (~150 lines)
- [ ] dotnet-format.md skill created (~300 lines)
- [ ] prettier.md skill created (~250 lines)
- [ ] eslint.md skill created (~300 lines)
- [ ] File type detection implemented
- [ ] Configuration detection implemented
- [ ] Git integration implemented
- [ ] Error handling implemented

### Testing Checklist

- [ ] Format C# project works
- [ ] Format TypeScript project works
- [ ] Format JavaScript project works
- [ ] Format full-stack project works
- [ ] Check mode (--check) works
- [ ] Fix mode (--fix) works
- [ ] Staged files (--staged) works
- [ ] Configuration detection works (.editorconfig, .prettierrc, .eslintrc)
- [ ] Performance acceptable (< 5s small, < 30s medium)
- [ ] Error handling works (missing tools, invalid paths)
- [ ] Output format correct ("Mr. mj2 recomienda")

### Documentation Checklist

- [ ] README.md updated (25 agentes, 25 comandos)
- [ ] ROADMAP.md updated (Issue #55 COMPLETADO, v0.6.0 4/4)
- [ ] CHANGELOG.md updated (Issue #55 entry)
- [ ] Examples provided (4-5 scenarios)

### Git Checklist

- [ ] SPEC commit with @SPEC:FMT-055
- [ ] CODE commit(s) with @CODE:FMT-055
- [ ] DOC commit with @DOC:FMT-055
- [ ] TAG chain complete
- [ ] Merged to main
- [ ] Pushed to GitHub

### Release Checklist

- [ ] Issue #55 closed
- [ ] All acceptance criteria met
- [ ] End-to-end testing passed
- [ ] v0.6.0 COMPLETADO (4/4 - 100%)

---

## 🎯 Definition of Done

**Issue #55 está DONE cuando:**

1. ✅ format-expert.md agent creado (~650 líneas)
2. ✅ /mj2-format command creado (~150 líneas)
3. ✅ 3 skills creados (~850 líneas total):
   - dotnet-format.md (~300)
   - prettier.md (~250)
   - eslint.md (~300)
4. ✅ File type detection funciona (C#, TypeScript, JavaScript)
5. ✅ Configuration detection funciona (.editorconfig, .prettierrc, .eslintrc)
6. ✅ Formatting funciona para todos los file types
7. ✅ Linting validation funciona (StyleCop, ESLint)
8. ✅ Git integration funciona (--staged, --check, --fix)
9. ✅ Performance aceptable (≤5s small, ≤30s medium)
10. ✅ Error handling robusto
11. ✅ Output format "Mr. mj2 recomienda" implementado
12. ✅ TAG chain completa (@SPEC → @CODE → @DOC)
13. ✅ Documentation actualizada (README, ROADMAP, CHANGELOG)
14. ✅ End-to-end testing passed
15. ✅ Issue #55 cerrado en GitHub
16. ✅ v0.6.0 COMPLETADO (4/4 - 100%)

---

**Created:** 2024-11-24
**Status:** Draft
**Next:** Begin implementation (Phase 2: Skills Creation)
