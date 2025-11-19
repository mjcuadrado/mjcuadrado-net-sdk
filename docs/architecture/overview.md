# Arquitectura General - mjcuadrado-net-sdk

## Visión general

mjcuadrado-net-sdk es un CLI construido en .NET 10 que sigue el patrón de Command Pattern usando Spectre.Console.Cli. El SDK está diseñado para ser extensible, modular y fácil de mantener.

## Componentes principales

### 1. CLI Layer (Capa de presentación)
- **Program.cs**: Entry point de la aplicación
- **Commands/**: Comandos CLI implementando `Command<TSettings>`
  - `InitCommand`: Inicializa proyectos
  - `DoctorCommand`: Diagnostica el sistema
  - `VersionCommand`: Muestra versión

### 2. Services Layer (Capa de lógica de negocio)
- **IFileSystemService**: Operaciones de sistema de archivos
- **IConfigurationService**: Lectura/escritura de configuración
- **ITemplateService**: Generación de archivos desde templates
- **IDoctorService**: Diagnóstico del sistema

### 3. Models Layer (Capa de datos)
- **SdkConfiguration**: Modelo de configuración principal
- **ProjectInfo**: Información del proyecto durante init
- **ValidationResult**: Resultado de validaciones

### 4. Templates Layer
- Templates embebidos como recursos del assembly
- Variables reemplazables: `{{PROJECT_NAME}}`, `{{DATE}}`, etc.

## Flujo de ejecución

```
Usuario ejecuta comando
    ↓
Program.cs configura CommandApp
    ↓
Spectre.Console.Cli parsea argumentos
    ↓
Invoca Command.Execute()
    ↓
Command usa Services para lógica de negocio
    ↓
Services usan Models y Templates
    ↓
Resultado mostrado con Spectre.Console
```

## Decisiones arquitectónicas

### 1. Spectre.Console.Cli vs System.CommandLine
**Decisión**: Usar Spectre.Console.Cli

**Razones**:
- Output visual más rico y atractivo
- Mejor manejo de tablas, spinners y progreso
- API más simple y declarativa
- Comunidad activa

### 2. Templates embebidos vs archivos externos
**Decisión**: Templates embebidos como recursos

**Razones**:
- Distribución más simple (un solo ejecutable)
- No depende de archivos externos
- Versionado junto con el código

### 3. Interfaces para todos los servicios
**Decisión**: Definir interfaces (`IXxxService`) desde el inicio

**Razones**:
- Facilita testing con mocks
- Permite dependency injection en el futuro
- Mejor separación de concerns

### 4. Configuración en JSON vs YAML
**Decisión**: JSON con `System.Text.Json`

**Razones**:
- Built-in en .NET 10
- Mejor performance que YAML
- Tooling nativo en IDEs
- Validación de esquema más simple

## Comparación con moai-adk

| Aspecto | moai-adk | mjcuadrado-net-sdk |
|---------|----------|-------------------|
| Lenguaje | TypeScript | C# |
| Runtime | Node.js | .NET 10 |
| CLI Framework | Commander.js | Spectre.Console.Cli |
| Config | JSON | JSON |
| Templates | Handlebars | String replacement |
| DB | Sin ORM built-in | Preparado para EF Core |

## Estructura de carpetas del SDK

```
mjcuadrado-net-sdk/
├── src/MjCuadrado.NetSdk/      # Proyecto principal
│   ├── Commands/               # Comandos CLI
│   ├── Services/               # Lógica de negocio
│   ├── Models/                 # Modelos de datos
│   └── Templates/              # Templates embebidos
├── tests/                      # Tests unitarios
└── docs/                       # Documentación
```

## Extensibilidad

El SDK está diseñado para ser extensible:

1. **Nuevos comandos**: Crear clase heredando de `Command<TSettings>`
2. **Nuevos servicios**: Crear interfaz + implementación
3. **Nuevos templates**: Agregar archivo en `Templates/`
4. **Nuevos modelos**: Agregar clase en `Models/`

## Próximos pasos arquitectónicos

### Fase 2: SPECs
- Agregar `SpecService` para gestión de especificaciones
- Parser YAML para frontmatter de SPECs
- Validador de formato EARS

### Fase 3: Database
- Agregar `DbContext` con EF Core
- Migrations automáticas
- Soporte multi-DB (SQL Server, PostgreSQL)

### Fase 4: Dependency Injection
- Configurar DI container (Microsoft.Extensions.DependencyInjection)
- Registrar todos los servicios
- Logging estructurado con `ILogger<T>`

## Performance

### Objetivos
- Inicio del CLI: < 500ms
- Comando `init`: < 2s para proyecto completo
- Comando `doctor`: < 1s
- Memory footprint: < 50MB

### Optimizaciones planificadas
- Lazy loading de templates
- Caching de configuración
- Parallel file operations donde sea posible
