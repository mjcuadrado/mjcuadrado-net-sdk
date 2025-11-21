---
name: /mj2:3-sync
description: Synchronize documentation with code
agent: mj2/doc-syncer
---

# /mj2:3-sync

Synchronizes documentation after implementation.

## Usage

```bash
/mj2:3-sync SPEC-ID

# Examples:
/mj2:3-sync AUTH-001
/mj2:3-sync USER-002
```

## What it does

1. Analyzes implemented code
2. Updates README.md
3. Updates docs/architecture.md
4. Updates docs/api.md (if needed)
5. Updates CHANGELOG.md
6. Adds @DOC: tags
7. Completes TAG chain
8. Makes docs commit

## Output

```
✅ Docs sincronizados: SPEC-AUTH-001
📝 Archivos: README, architecture, api, changelog
🔗 TAG chain: @SPEC → @TEST → @CODE → @DOC
📦 Commit: 📚 docs(AUTH-001)
🎉 Ciclo completo!
```

## Agent

Delegates to: `.claude/agents/mj2/doc-syncer.md`

Loads Skills:
- foundation/tags.md
- foundation/git.md
