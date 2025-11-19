# Claude Agents

Esta carpeta contiene definiciones de agentes especializados para Claude Code.

## ¿Qué son los agentes?

Los agentes son subprocess especializados que Claude Code puede lanzar para realizar tareas complejas de forma autónoma.

## Estructura de un agente

```markdown
---
name: nombre-agente
description: Descripción breve del agente
tools: [Read, Write, Bash, Grep]
model: haiku|sonnet|opus
---

# Instrucciones del agente

[Prompt detallado que describe qué debe hacer el agente]

## Contexto
[Información de contexto necesaria]

## Tareas
1. Tarea 1
2. Tarea 2

## Output esperado
[Qué debe retornar el agente]
```

## Ejemplos de agentes

### code-reviewer.md
Agente que revisa código en busca de problemas de calidad, seguridad y mejores prácticas.

### test-generator.md
Agente que genera tests unitarios basándose en el código existente.

### spec-validator.md
Agente que valida que las SPECs estén completas y bien formateadas.

## Uso

```bash
# Claude Code lanzará agentes automáticamente según contexto
# o puedes invocarlos explícitamente con:
/agent code-reviewer
```

## Próximos pasos

En futuras fases, el SDK incluirá comandos para:
```bash
# Listar agentes disponibles
mjcuadrado-net-sdk agent list

# Ejecutar un agente
mjcuadrado-net-sdk agent run code-reviewer

# Crear un nuevo agente
mjcuadrado-net-sdk agent new mi-agente
```
