---
name: /mj2:git-merge
description: Merge feature branch (respects mode)
agent: mj2/git-manager
---

# /mj2:git-merge

Merges feature branch according to project mode.

## Usage

```bash
/mj2:git-merge SPEC-ID

# Examples:
/mj2:git-merge AUTH-001
```

## What it does

### Personal Mode
1. Merges to main automatically
2. Deletes feature branch
3. Pushes to remote

### Team Mode
1. Creates Draft PR
2. Adds SPEC link
3. Waits for review

## Output

**Personal:**
```
✅ Merged: feature/SPEC-AUTH-001 → main
🧹 Branch deleted
📦 Pushed to remote
```

**Team:**
```
✅ Draft PR created
🔗 URL: github.com/.../pull/42
👥 Awaiting review
```

## Agent

Delegates to: `.claude/agents/mj2/git-manager.md`

Loads Skills:
- foundation/git.md
