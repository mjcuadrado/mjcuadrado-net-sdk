# Issue #7: Comando version

**Título:** Implementar comando `mjcuadrado-net-sdk --version`

## 📋 Descripción
Implementar comando simple que muestra la versión del SDK instalado y la versión de .NET del sistema.

## 🎯 Objetivos
- [ ] Mostrar versión del SDK
- [ ] Mostrar versión de .NET del sistema
- [ ] Output limpio y simple

## 📝 Tareas técnicas
- [ ] Crear `VersionCommand.cs` en `src/Commands/`
- [ ] Leer versión del SDK desde:
  - `Assembly.GetExecutingAssembly().GetName().Version`
  - O desde archivo `version.txt` embebido
- [ ] Detectar versión de .NET:
  - `Environment.Version`
  - O ejecutar `dotnet --version`
- [ ] Output formato:
  ```
  mjcuadrado-net-sdk v0.1.0
  .NET 9.0.0
  ```
- [ ] Opcionalmente con `--verbose`:
  ```
  mjcuadrado-net-sdk v0.1.0
  .NET SDK: 9.0.0
  Runtime: 9.0.0
  OS: Windows 11 (10.0.22631)
  Architecture: x64
  ```

## ✅ Criterios de aceptación
- [ ] Muestra versión correcta del SDK
- [ ] Muestra versión de .NET instalada
- [ ] Output simple y claro
- [ ] Opción `--verbose` funciona (opcional)

## 🧪 Tests requeridos
- [ ] `VersionCommandTests.cs`
- [ ] `Execute_ReturnsVersionInfo`
- [ ] `Execute_Verbose_ReturnsDetailedInfo` (si se implementa)

## 🔗 Dependencias
- Depende de: #1 (estructura base)

## 📚 Referencias
- [Assembly.GetName Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly.getname)

## 🏷️ Labels sugeridas
`phase-1`, `cli`, `command`, `good-first-issue`
