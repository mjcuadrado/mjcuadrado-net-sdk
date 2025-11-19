# Changelog

Todos los cambios notables en este proyecto serán documentados en este archivo.

El formato está basado en [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/),
y este proyecto adhiere a [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### En progreso
- Issue #3: ConfigurationService
- Issue #4: TemplateService
- Issue #5: Comando `init` funcional
- Issue #6: Comando `doctor` funcional

### Completado recientemente
- ✅ **2024-11-19**: Issue #2 - FileSystemService
  - FileSystemService implementado con 11 métodos
  - 44 tests unitarios (100% passing)
  - Normalización cross-platform de rutas
  - Manejo robusto de excepciones
  - Validación de permisos y espacio en disco

- ✅ **2024-11-19**: Issue #1 - Estructura base del proyecto
  - Solución .NET 10 configurada y funcional
  - Proyectos principal y de tests creados
  - Todos los paquetes NuGet instalados
  - Build exitoso (0 errores)
  - Tests ejecutándose correctamente (1/1 passing)
  - Nullable reference types habilitado
  - C# 13 configurado

## [0.1.0] - TBD (Fase 1 - MVP)

### Added (Agregado)
- ✅ Estructura base del proyecto .NET 10
- ✅ Solución y proyectos configurados
- ✅ Comando `version` implementado
- ✅ Interfaces de servicios definidas
- ✅ Modelos de configuración
- ✅ Templates embebidos
- ✅ Documentación completa (README, arquitectura, comandos)
- ✅ 9 GitHub Issues para desarrollo iterativo
- ✅ CI/CD con GitHub Actions
- ✅ Archivos de ejemplo en `.mjcuadrado-net-sdk/`

### Planned (Planeado)
- [ ] Comando `init` funcional
- [ ] Comando `doctor` funcional
- [ ] FileSystemService implementado
- [ ] ConfigurationService implementado
- [ ] TemplateService implementado
- [ ] Tests unitarios completos
- [ ] Coverage ≥ 85%

## Próximas versiones

### [0.2.0] - Fase 2: SPECs y TAGs
- Sistema completo de SPECs con formato EARS
- Comando `spec new`
- Comando `spec validate`
- Comando `tags validate`
- Sistema de TAGs con trazabilidad completa

### [0.3.0] - Fase 3: Base de datos
- Integración Entity Framework Core
- Soporte SQL Server
- Soporte PostgreSQL
- Migraciones automáticas

### [0.4.0] - Fase 4: Automatización avanzada
- Hooks automáticos pre-commit
- Generación automática de tests desde SPECs
- Integración CI/CD avanzada

### [1.0.0] - Fase 5: IA Completa
- Agentes especializados completos
- Skills para tareas comunes
- Generación de código desde SPECs
- Análisis automático con IA

---

## Tipos de cambios

- **Added** (Agregado): Nueva funcionalidad
- **Changed** (Cambiado): Cambios en funcionalidad existente
- **Deprecated** (Deprecado): Funcionalidad que será removida
- **Removed** (Removido): Funcionalidad removida
- **Fixed** (Corregido): Corrección de bugs
- **Security** (Seguridad): Cambios de seguridad
