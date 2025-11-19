# Specifications (SPECs)

Esta carpeta contiene las especificaciones del proyecto siguiendo el formato EARS (Easy Approach to Requirements Syntax).

## Formato de SPECs

Cada SPEC debe seguir este formato:

```markdown
---
id: DOMAIN-XXX
title: Título de la especificación
domain: DOMAIN
priority: high|medium|low
status: draft|review|approved|implemented
created: YYYY-MM-DD
updated: YYYY-MM-DD
tags: [tag1, tag2]
---

# @SPEC:EX-DOMAIN-XXX

## Descripción
[Descripción general de la funcionalidad]

## Casos EARS

### Ubiquitous (El sistema siempre...)
- El sistema DEBE...

### Event-driven (CUANDO [evento], el sistema DEBE...)
- CUANDO el usuario..., el sistema DEBE...

### State-driven (MIENTRAS [condición], el sistema DEBE...)
- MIENTRAS..., el sistema DEBE...

### Optional (DONDE [característica], el sistema DEBE...)
- DONDE..., el sistema DEBE...

### Constraints (Restricciones)
- El sistema NO DEBE...
- El sistema DEBE completar... en menos de X segundos

## Criterios de aceptación
- [ ] Criterio 1
- [ ] Criterio 2

## Referencias
- Link a documentación externa
- Link a issues relacionadas
```

## Dominios sugeridos

- **AUTH**: Autenticación y autorización
- **USER**: Gestión de usuarios
- **DATA**: Manejo de datos
- **UI**: Interfaz de usuario
- **API**: Endpoints de API
- **PERF**: Performance
- **SEC**: Seguridad

## Comandos útiles (cuando estén disponibles)

```bash
# Crear nueva SPEC
mjcuadrado-net-sdk spec new AUTH-001

# Validar SPECs
mjcuadrado-net-sdk spec validate

# Listar SPECs
mjcuadrado-net-sdk spec list
```
