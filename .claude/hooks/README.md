# Claude Hooks

Esta carpeta contiene hooks que se ejecutan en respuesta a eventos de Claude Code.

## ¿Qué son los hooks?

Los hooks son scripts que se ejecutan automáticamente cuando ocurren ciertos eventos durante el desarrollo con Claude Code.

## Tipos de hooks

### Pre-tool hooks
Se ejecutan ANTES de que Claude use una herramienta.

Ejemplo: `pre-write.sh`
```bash
#!/bin/bash
# Valida que el archivo no esté bloqueado antes de escribir
```

### Post-tool hooks
Se ejecutan DESPUÉS de que Claude use una herramienta.

Ejemplo: `post-write.sh`
```bash
#!/bin/bash
# Ejecuta formateo automático después de escribir
dotnet format
```

### User-prompt-submit hooks
Se ejecutan cuando el usuario envía un prompt.

Ejemplo: `user-prompt-submit.sh`
```bash
#!/bin/bash
# Verifica que no haya cambios sin commitear
git status --porcelain
```

## Configuración de hooks

Los hooks se configuran en el archivo de settings de Claude Code o como scripts ejecutables en `.claude/hooks/`.

### Formato de script

```bash
#!/bin/bash
# Nombre: nombre-hook
# Descripción: Qué hace este hook
# Evento: pre-write|post-write|user-prompt-submit

# Tu código aquí
exit 0  # 0 = success, 1 = bloquea la operación
```

## Ejemplos de hooks útiles

### Auto-format on write
```bash
#!/bin/bash
# .claude/hooks/post-write-format.sh
dotnet format "$1"  # $1 es el archivo modificado
```

### Prevent commits without tests
```bash
#!/bin/bash
# .claude/hooks/pre-commit.sh
dotnet test || exit 1
```

### Validate SPECs before save
```bash
#!/bin/bash
# .claude/hooks/pre-write-spec.sh
if [[ "$1" == *.spec.md ]]; then
    mjcuadrado-net-sdk spec validate "$1" || exit 1
fi
```

## Próximos pasos

En futuras fases, el SDK incluirá:
```bash
# Listar hooks disponibles
mjcuadrado-net-sdk hook list

# Crear nuevo hook
mjcuadrado-net-sdk hook new mi-hook

# Habilitar/deshabilitar hook
mjcuadrado-net-sdk hook enable pre-write-format
mjcuadrado-net-sdk hook disable pre-write-format
```

## Documentación

Para más información sobre hooks, consulta la documentación oficial de Claude Code:
https://code.claude.com/docs
