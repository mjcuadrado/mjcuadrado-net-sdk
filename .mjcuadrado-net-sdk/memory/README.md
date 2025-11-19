# Memory

Esta carpeta almacena el "contexto de memoria" del proyecto para agentes de IA.

## Propósito

La carpeta `memory/` sirve para:
- Guardar decisiones importantes tomadas durante el desarrollo
- Mantener el historial de problemas resueltos
- Documentar lecciones aprendidas
- Proporcionar contexto a agentes de IA en futuras sesiones

## Estructura recomendada

```
memory/
├── decisions/          # Decisiones arquitectónicas y de diseño
├── learnings/          # Lecciones aprendidas
├── issues/            # Problemas encontrados y sus soluciones
└── context/           # Contexto general del proyecto
```

## Formato sugerido

### Decision log
```markdown
# Decisión: [Título]
**Fecha**: YYYY-MM-DD
**Contexto**: [Por qué era necesario decidir]
**Decisión**: [Qué se decidió]
**Consecuencias**: [Implicaciones de la decisión]
**Alternativas consideradas**: [Otras opciones evaluadas]
```

### Learning log
```markdown
# Aprendizaje: [Título]
**Fecha**: YYYY-MM-DD
**Situación**: [Qué pasó]
**Aprendizaje**: [Qué aprendimos]
**Acción**: [Qué haremos diferente]
```

### Issue log
```markdown
# Issue: [Título]
**Fecha**: YYYY-MM-DD
**Problema**: [Descripción del problema]
**Causa raíz**: [Por qué ocurrió]
**Solución**: [Cómo se resolvió]
**Prevención**: [Cómo evitarlo en el futuro]
```

## Uso con IA

Los archivos en esta carpeta pueden ser referenciados en prompts para proveer contexto:
```
"Revisa memory/decisions/ para entender las decisiones arquitectónicas previas"
```
