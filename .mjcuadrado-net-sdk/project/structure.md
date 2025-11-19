# Project Structure - mjcuadrado-net-sdk

## Arquitectura general

El SDK sigue una arquitectura en capas:

```
┌─────────────────────────────┐
│   CLI Layer (Commands)      │  ← Interacción con usuario
├─────────────────────────────┤
│   Services Layer            │  ← Lógica de negocio
├─────────────────────────────┤
│   Models Layer              │  ← Modelos de datos
├─────────────────────────────┤
│   Templates Layer           │  ← Recursos embebidos
└─────────────────────────────┘
```

## Componentes principales

### CLI Layer
- **InitCommand**: Inicializa proyectos
- **DoctorCommand**: Diagnostica el sistema
- **VersionCommand**: Muestra versión

### Services Layer
- **FileSystemService**: Operaciones de archivos
- **ConfigurationService**: Gestión de config.json
- **TemplateService**: Generación desde templates
- **DoctorService**: Checks del sistema

### Models Layer
- **SdkConfiguration**: Modelo principal de config
- **ProjectInfo**: Info del proyecto durante init
- **ValidationResult**: Resultados de validaciones

### Templates Layer
- Templates embebidos como recursos
- Sistema de reemplazo de variables

## Flujos de datos

### Flujo: mjcuadrado-net-sdk init
```
Usuario
  ↓
InitCommand
  ↓
TemplateService → FileSystemService → Disco
  ↓
ConfigurationService → config.json
  ↓
Output visual (Spectre.Console)
```

### Flujo: mjcuadrado-net-sdk doctor
```
Usuario
  ↓
DoctorCommand
  ↓
DoctorService → Ejecuta checks
  ↓
Output tabla de resultados
```

## Decisiones arquitectónicas

### Decisión 1: Spectre.Console.Cli
**Contexto**: Necesitábamos un framework CLI con buena UX
**Decisión**: Usar Spectre.Console.Cli
**Consecuencias**: Output visual atractivo, API simple

### Decisión 2: Templates embebidos
**Contexto**: Distribución simple del SDK
**Decisión**: Embeber templates como recursos
**Consecuencias**: Un solo ejecutable, fácil de distribuir

### Decisión 3: Interfaces para servicios
**Contexto**: Facilitar testing y DI futuro
**Decisión**: Definir interfaces IXxxService
**Consecuencias**: Mejor testability, preparado para DI

## Dependencias externas

- **Spectre.Console.Cli**: Framework CLI
- **System.Text.Json**: Serialización JSON
- **xUnit**: Testing framework
- **FluentAssertions**: Assertions para tests
- **Moq**: Mocking framework
