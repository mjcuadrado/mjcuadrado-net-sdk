# Claude Commands

Esta carpeta contiene slash commands personalizados para Claude Code.

## ¿Qué son los slash commands?

Los slash commands son atajos que expanden prompts predefinidos cuando los escribes en Claude Code.

## Estructura de un comando

Archivo: `mi-comando.md`

```markdown
---
description: Descripción breve del comando
---

[Prompt que se expandirá cuando ejecutes /mi-comando]

Puedes usar variables:
- {{FILE}}: Archivo actual
- {{SELECTION}}: Texto seleccionado
- {{PROJECT}}: Nombre del proyecto
```

## Ejemplos de comandos útiles

### /review-spec
Revisa una SPEC para validar que sigue el formato EARS correcto.

### /generate-test
Genera tests unitarios para el archivo actual.

### /explain-code
Explica el código seleccionado en detalle.

### /refactor
Sugiere refactorings para mejorar el código seleccionado.

## Creación de comandos

1. Crea un archivo `.md` en `.claude/commands/`
2. Agrega el frontmatter con `description`
3. Escribe el prompt del comando
4. Úsalo con `/nombre-archivo` (sin la extensión .md)

Ejemplo - `.claude/commands/review-pr.md`:
```markdown
---
description: Revisa un Pull Request en busca de problemas
---

Por favor revisa el Pull Request actual y busca:
- Problemas de seguridad
- Code smells
- Tests faltantes
- Documentación faltante

Genera un reporte estructurado con tus hallazgos.
```

Uso:
```
/review-pr
```

## Próximos pasos

En futuras fases, el SDK incluirá:
```bash
# Listar comandos disponibles
mjcuadrado-net-sdk command list

# Crear nuevo comando
mjcuadrado-net-sdk command new mi-comando

# Validar comandos
mjcuadrado-net-sdk command validate
```
