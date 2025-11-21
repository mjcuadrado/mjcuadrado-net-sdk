---
name: /mj2:quality-check
description: Validate quality standards
agent: mj2/quality-gate
---

# /mj2:quality-check

Validates TRUST 5, coverage, and quality standards.

## Usage

```bash
/mj2:quality-check SPEC-ID

# Examples:
/mj2:quality-check AUTH-001
```

## What it does

1. Runs all tests
2. Checks coverage ≥85%
3. Validates TRUST 5 principles
4. Validates TAG chains
5. Checks C# conventions
6. Generates quality report
7. PASS/FAIL decision

## Output

```
✅ CALIDAD APROBADA: SPEC-AUTH-001
📊 Tests: 4/4 (100%)
📊 Coverage: 87% (≥85%)
✅ TRUST 5: 28/30 puntos
✅ TAG chains: Complete
📄 Reporte: .mjcuadrado-net-sdk/reports/
```

## Agent

Delegates to: `.claude/agents/mj2/quality-gate.md`

Loads Skills:
- foundation/trust.md
- foundation/tags.md
- dotnet/csharp.md
