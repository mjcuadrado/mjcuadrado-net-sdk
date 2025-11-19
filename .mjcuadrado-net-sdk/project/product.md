# Product Definition - mjcuadrado-net-sdk

## Descripción del producto

mjcuadrado-net-sdk es un SDK de línea de comandos para .NET que automatiza la estructura y organización de proyectos de software siguiendo la metodología **SPEC → TEST → CODE → DOC**.

## Características principales

- Inicialización rápida de proyectos con estructura completa
- Sistema de especificaciones con formato EARS
- Trazabilidad completa con sistema de TAGs
- Integración con Claude Code (agentes, comandos, skills, hooks)
- Preparado para bases de datos con EF Core
- CLI intuitivo con Spectre.Console

## Usuarios objetivo

- **Desarrolladores .NET**: Que quieren estructurar mejor sus proyectos
- **Equipos de desarrollo**: Que necesitan estandarizar su workflow
- **Usuarios de IA**: Que trabajan con Claude Code y quieren aprovechar agentes
- **Estudiantes**: Que aprenden desarrollo con IA

## Propuesta de valor

- **Ahorro de tiempo**: Inicializa proyectos en segundos con estructura completa
- **Calidad**: Fomenta buenas prácticas (SPEC → TEST → CODE → DOC)
- **Trazabilidad**: Sistema de TAGs conecta SPECs, tests, código y docs
- **Automatización**: Reduce tareas repetitivas del setup de proyectos
- **Extensibilidad**: Preparado para crecer con agentes y skills de IA

## Visión

Convertirse en el estándar de facto para estructurar proyectos .NET que usan desarrollo asistido por IA, facilitando la colaboración entre humanos y agentes de IA en todo el ciclo de desarrollo.

## Roadmap

### Fase 1 (v0.1.0) - MVP
- CLI básico funcional
- Comandos: init, doctor, version
- Templates completos
- Documentación base

### Fase 2 (v0.2.0) - SPECs y TAGs
- Sistema completo de SPECs con formato EARS
- Validación de SPECs
- Sistema de TAGs con trazabilidad
- Reportes de validación

### Fase 3 (v0.3.0) - Base de datos
- Integración EF Core
- Soporte SQL Server y PostgreSQL
- Migraciones automáticas
- Scaffolding de modelos

### Fase 4 (v0.4.0) - Automatización
- Hooks pre-commit para validación
- Generación automática de tests desde SPECs
- Integración CI/CD avanzada

### Fase 5 (v1.0.0) - IA Completa
- Agentes especializados completos
- Skills para tareas comunes
- Generación de código desde SPECs con IA
- Análisis automático de código con IA
