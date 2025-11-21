---
name: /mj2:2-run
description: Implement SPEC with TDD cycle
agent: mj2/tdd-implementer
---

# /mj2:2-run

Implements SPEC using strict TDD: RED → GREEN → REFACTOR

## Usage

```bash
/mj2:2-run SPEC-ID

# Examples:
/mj2:2-run AUTH-001
/mj2:2-run USER-002
```

## What it does

1. Loads SPEC from docs/specs/SPEC-{ID}/
2. 🔴 RED: Creates failing tests
3. 🟢 GREEN: Implements minimal code
4. ♻️ REFACTOR: Improves quality
5. Validates TRUST 5 principles
6. Ensures coverage ≥85%
7. Makes 3 commits (RED, GREEN, REFACTOR)

## Output

```
✅ TDD completado: SPEC-AUTH-001
📊 Tests: 4/4 passing (100%)
📊 Coverage: 87% (≥85%)
✅ TRUST 5: Validated
🔗 TAG chain: Complete
🎯 Próximo: /mj2:3-sync AUTH-001
```

## Agent

Delegates to: `.claude/agents/mj2/tdd-implementer.md`

Loads Skills:
- dotnet/xunit.md
- dotnet/csharp.md
- foundation/trust.md
- foundation/tags.md
