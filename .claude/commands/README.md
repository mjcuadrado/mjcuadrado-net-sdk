# MJ² Commands

Quick reference for mj2 commands in Claude Code.

## Main Workflow

```bash
# 1. Initialize project
/mj2:0-project

# 2. Create SPEC
/mj2:1-plan "feature description"

# 3. Implement with TDD
/mj2:2-run SPEC-ID

# 4. Sync documentation
/mj2:3-sync SPEC-ID
```

## Auxiliary Commands

```bash
# Check quality manually
/mj2:quality-check SPEC-ID

# Merge feature branch
/mj2:git-merge SPEC-ID
```

## Complete Example

```bash
# Initialize
/mj2:0-project

# Plan authentication feature
/mj2:1-plan "User authentication with JWT"
# → Creates SPEC-AUTH-001

# Implement with TDD
/mj2:2-run AUTH-001
# → RED, GREEN, REFACTOR cycle

# Sync docs
/mj2:3-sync AUTH-001
# → Updates README, docs, changelog

# Feature complete! 🎉
```

## Command Reference

| Command | Agent | Description |
|---------|-------|-------------|
| `/mj2:0-project` | project-manager | Initialize/optimize project |
| `/mj2:1-plan` | spec-builder | Create SPEC |
| `/mj2:2-run` | tdd-implementer | Implement with TDD |
| `/mj2:3-sync` | doc-syncer | Sync documentation |
| `/mj2:quality-check` | quality-gate | Validate quality |
| `/mj2:git-merge` | git-manager | Merge feature |

## Skills Loaded

Commands automatically load relevant Skills:

- **foundation/trust.md** - TRUST 5 principles
- **foundation/tags.md** - TAG system
- **foundation/specs.md** - SPEC format
- **foundation/ears.md** - EARS syntax
- **foundation/git.md** - Git workflows
- **dotnet/csharp.md** - C# conventions
- **dotnet/xunit.md** - Test patterns

## Notes

- Commands respect project mode (personal vs team)
- Commands respect language setting (es/en)
- Each command is ≤200 lines
- Commands delegate to agents
- Agents delegate to Skills

## Philosophy

```
Command (short)
  → Agent (orchestration)
    → Skill (knowledge)
```

Keep it simple. Delegate everything.
