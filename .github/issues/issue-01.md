# Issue #1: Estructura base del proyecto .NET 9

**Título:** Configurar estructura base del proyecto .NET 9 con solución y proyectos

## 📋 Descripción
Configurar el proyecto .NET 9 con la estructura de carpetas completa, solución, proyectos principales y archivos de configuración base.

## 🎯 Objetivos
- [ ] Crear solución .NET 9 (`mjcuadrado-net-sdk.sln`)
- [ ] Configurar proyecto CLI principal
- [ ] Configurar proyecto de tests
- [ ] Establecer configuración base del proyecto

## 📝 Tareas técnicas
- [ ] Ejecutar `dotnet new sln -n mjcuadrado-net-sdk`
- [ ] Crear proyecto consola: `src/MjCuadrado.NetSdk/MjCuadrado.NetSdk.csproj`
- [ ] Crear proyecto tests: `tests/MjCuadrado.NetSdk.Tests/MjCuadrado.NetSdk.Tests.csproj`
- [ ] Agregar proyectos a la solución
- [ ] Configurar `global.json` para fijar .NET 9.0
- [ ] Crear `.gitignore` completo para .NET
- [ ] Crear `.editorconfig` con estándares C#
- [ ] Instalar NuGet packages iniciales:
  - Spectre.Console.Cli
  - System.CommandLine (opcional)
  - System.Text.Json
  - xUnit
  - xUnit.runner.visualstudio
  - FluentAssertions (para tests)
  - Moq (para mocks en tests)

## ✅ Criterios de aceptación
- [ ] `dotnet build` compila sin errores
- [ ] `dotnet test` ejecuta (aunque no haya tests aún)
- [ ] Estructura de carpetas `src/` y `tests/` creada
- [ ] Nullable reference types habilitado
- [ ] Target framework es `net9.0`
- [ ] Proyecto usa C# 13

## 🧪 Tests requeridos
- N/A (esta es la configuración base)

## 🔗 Dependencias
- Ninguna (es el primer issue)

## 📚 Referencias
- [.NET 9 Documentation](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)
- [Spectre.Console.Cli](https://spectreconsole.net/cli/)
- Inspiración: https://github.com/modu-ai/moai-adk

## 🏷️ Labels sugeridas
`phase-1`, `setup`, `infrastructure`, `good-first-issue`
