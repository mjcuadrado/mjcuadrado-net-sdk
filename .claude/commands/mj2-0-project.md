---
name: /mj2:0-project
description: Initialize or optimize project structure
agent: mj2/project-manager
---

# /mj2:0-project

Initializes new .NET 9 project or optimizes existing one.

## Usage

```bash
# New project
/mj2:0-project

# Existing project (optimization)
/mj2:0-project
```

## What it does

1. Detects if project initialized
2. If new: Interview user, create structure
3. If existing: Analyze, suggest improvements
4. Creates `.mjcuadrado-net-sdk/` structure
5. Recommends Skills to load

## Output

```
✅ Proyecto inicializado
📁 Estructura creada
⚙️ Configuración lista
📚 Skills cargados
🎯 Próximo paso: /mj2:1-plan "feature"
```

## Agent

Delegates to: `.claude/agents/mj2/project-manager.md`

Loads Skills:
- foundation/trust.md
- foundation/tags.md
- foundation/specs.md
- dotnet/csharp.md
