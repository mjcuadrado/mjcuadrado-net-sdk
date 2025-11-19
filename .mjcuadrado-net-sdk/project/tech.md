# Technical Stack - mjcuadrado-net-sdk

## Stack tecnológico

### Framework
- **.NET 10**: Framework principal del proyecto
- **C# 13**: Lenguaje principal con latest features

### Librerías principales
- **Spectre.Console.Cli v0.49.1**: Framework para CLI
- **System.Text.Json v9.0.0**: Serialización JSON

### Testing
- **xUnit v2.9.3**: Framework de testing
- **FluentAssertions v7.0.0**: Assertions fluent
- **Moq v4.20.72**: Mocking framework
- **Coverlet v6.0.4**: Code coverage

### Herramientas de desarrollo
- **Git**: Control de versiones
- **GitHub Actions**: CI/CD
- **EditorConfig**: Estándares de código
- **dotnet format**: Formatting automático

## Configuración del entorno

### Requisitos previos
1. .NET SDK 9.0 o superior (recomendado 10.0)
2. Git configurado
3. IDE recomendado: Visual Studio 2022, Rider o VS Code

### Instalación
```bash
# Clonar repositorio
git clone https://github.com/mjcuadrado/mjcuadrado-net-sdk.git

# Restaurar dependencias
dotnet restore

# Compilar
dotnet build

# Ejecutar tests
dotnet test
```

### Ejecutar en desarrollo
```bash
# Run
dotnet run --project src/MjCuadrado.NetSdk/MjCuadrado.NetSdk.csproj -- version

# Watch mode
dotnet watch run --project src/MjCuadrado.NetSdk/MjCuadrado.NetSdk.csproj
```

## Convenciones de código

### Nomenclatura
- **PascalCase**: Clases, métodos, propiedades públicas
- **camelCase**: Variables locales, parámetros
- **_camelCase**: Campos privados
- **IPascalCase**: Interfaces (prefijo I)

### Documentación
```csharp
/// <summary>
/// Descripción del método
/// </summary>
/// <param name="param">Descripción del parámetro</param>
/// <returns>Descripción del retorno</returns>
public string Method(string param)
{
    // Implementation
}
```

### Tests
- Coverage objetivo: ≥ 85%
- Nomenclatura: `[MethodName]_[Scenario]_[ExpectedResult]`
- Un archivo de test por clase

## Comandos útiles

```bash
# Compilar en Release
dotnet build -c Release

# Ejecutar tests con coverage
dotnet test /p:CollectCoverage=true

# Formatear código
dotnet format

# Verificar formato
dotnet format --verify-no-changes

# Publicar
dotnet publish -c Release -o ./publish
```

## Estructura de proyecto

```
mjcuadrado-net-sdk/
├── src/MjCuadrado.NetSdk/      # Código principal
│   ├── Commands/               # Comandos CLI
│   ├── Services/               # Servicios
│   ├── Models/                 # Modelos
│   └── Templates/              # Templates
├── tests/                      # Tests unitarios
├── docs/                       # Documentación
└── .github/                    # GitHub configs
```

## Performance

### Métricas objetivo
- Inicio del CLI: < 500ms
- Comando init: < 2s
- Comando doctor: < 1s
- Memory footprint: < 50MB

## Próximos pasos tecnológicos

### Fase 2
- Parser YAML para SPECs

### Fase 3
- Entity Framework Core
- PostgreSQL provider
- SQL Server provider

### Fase 4
- Dependency Injection
- Microsoft.Extensions.Logging
- Microsoft.Extensions.Configuration
