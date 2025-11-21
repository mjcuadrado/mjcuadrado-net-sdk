---
name: /mj2:1-plan
description: Create SPEC for new feature
agent: mj2/spec-builder
---

# /mj2:1-plan

Creates SPEC (specification) for a feature in EARS format.

## Usage

```bash
/mj2:1-plan "feature description"

# Examples:
/mj2:1-plan "User authentication with JWT"
/mj2:1-plan "Payment processing with Stripe"
/mj2:1-plan "User profile management"
```

## What it does

1. Analyzes feature description
2. Detects domain (AUTH, USER, API, etc.)
3. Asks clarifying questions
4. Generates SPEC-{DOMAIN}-{NNN}
5. Creates spec.md, plan.md, acceptance.md
6. Creates feature branch
7. Makes initial commit

## Output

```
✅ SPEC creada: SPEC-AUTH-001
📁 Archivos: spec.md, plan.md, acceptance.md
🌿 Rama: feature/SPEC-AUTH-001
🎯 Próximo: /mj2:2-run AUTH-001
```

## Agent

Delegates to: `.claude/agents/mj2/spec-builder.md`

Loads Skills:
- foundation/specs.md
- foundation/ears.md
- foundation/tags.md
