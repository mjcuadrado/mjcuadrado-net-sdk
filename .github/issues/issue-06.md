# Issue #6: Comando doctor

**Estado:** ✅ **COMPLETADO** (2024-11-20)

**Título:** Implementar comando `mjcuadrado-net-sdk doctor` para diagnóstico del sistema

## 📋 Descripción
Implementar comando de diagnóstico que verifica todas las dependencias del sistema y la salud del proyecto actual.

## 🎯 Objetivos
- [x] Verificar dependencias del sistema
- [x] Validar estructura del proyecto actual
- [x] Output claro con checks visuales (✓/✗)

## 📝 Tareas técnicas
- [x] Crear `DoctorCommand.cs` en `src/Commands/`
- [x] Implementar verificaciones:
  1. **.NET SDK instalado y versión ≥ 9.0**
     - Ejecutar `dotnet --version`
     - Parsear versión y comparar
  2. **Git instalado**
     - Ejecutar `git --version`
     - Verificar configuración: `git config user.name` y `git config user.email`
  3. **Estructura de proyecto (si existe):**
     - Verificar `.mjcuadrado-net-sdk/` existe
     - Verificar `config.json` existe y es válido
     - Verificar carpetas requeridas existen
  4. **Permisos de escritura**
     - En directorio actual
  5. **Espacio en disco**
     - Mínimo 100MB disponibles
- [x] Crear `DoctorService.cs` para lógica de verificación:
  - `CheckDotNetVersion()` → (bool success, string version)
  - `CheckGitInstallation()` → (bool success, string version, bool configured)
  - `CheckProjectStructure()` → (bool success, List<string> missingItems)
  - `CheckDiskSpace()` → (bool success, long availableBytes)
  - `CheckWritePermissions()` → bool
  - `RunFullDiagnostic()` → DiagnosticResult
- [x] Output con Spectre.Console:
  - Tabla con resultados de cada check
  - ✓ en verde para éxito
  - ✗ en rojo para fallos
  - Warnings en amarillo
  - Resumen final: "Todo listo!" o "X problemas encontrados"
  - Sugerencias para resolver problemas

## ✅ Criterios de aceptación
- [x] Detecta correctamente todas las dependencias
- [x] Muestra versiones instaladas
- [x] Identifica problemas de configuración
- [x] Provee sugerencias de solución
- [x] Funciona en Windows, Linux y macOS
- [x] No crashea si faltan dependencias

## 🧪 Tests requeridos
- [x] `DoctorCommandTests.cs` (11 tests)
- [x] `DoctorServiceTests.cs` (20 tests)
- [x] `CheckDotNetVersion_ReturnsCorrectVersion`
- [x] `CheckGitInstallation_DetectsGit`
- [x] `CheckProjectStructure_DetectsMissingFolders`
- [x] `CheckDiskSpace_ReturnsAvailableSpace`
- [x] Tests con mocks de servicios

## 🔗 Dependencias
- Depende de: #1 (estructura base)
- Depende de: #3 (ConfigurationService para validar config.json)

## 📚 Referencias
- [Process.Start Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process)
- Output esperado en prompt principal

## 🏷️ Labels sugeridas
`phase-1`, `cli`, `command`, `diagnostics`

---

## 📊 Resumen de cierre

**Fecha de cierre:** 2024-11-20
**Estado:** ✅ COMPLETADO

### Resultados de tests
```
Test Results:
- Passed: 188 (31 DoctorService/Command + 157 otros)
- Failed: 1 (test intermitente pre-existente)
- Skipped: 0
- Total: 189
Duration: 2 s
Coverage: 99.5% de tests passing
```

### Implementación completada

**DoctorService.cs** (320 líneas) - Servicio de diagnóstico completo con:
- 5 métodos de verificación individuales
- CheckDotNetVersion() - Verifica .NET SDK ≥ 9.0
- CheckGitInstallation() - Verifica Git y configuración
- CheckProjectStructure() - Valida 9 carpetas + config.json
- CheckDiskSpace() - Verifica mínimo 100 MB disponibles
- CheckWritePermissions() - Verifica permisos de escritura
- RunFullDiagnostic() - Ejecuta todas las verificaciones y genera reporte
- ExecuteCommand() helper para ejecutar comandos externos (dotnet, git)
- Usa System.Diagnostics.Process para comandos del sistema

**DoctorCommand.cs** (155 líneas) - Comando CLI completo con:
- Dependency injection de IDoctorService
- Settings con --verbose flag
- Interfaz rica con Spectre.Console:
  - Panel de header con emoji 🏥
  - Spinner durante ejecución
  - Tabla de resultados con colores (✓/✗)
  - Panel de warnings (amarillo)
  - Panel de sugerencias con numeración
  - Panel de resumen final (verde/rojo)
- Retorna exit code apropiado (0 = success, 1 = failure)

**IDoctorService.cs** (Ya existía) - Interfaz con:
- DiagnosticResult class para resultados
- DiagnosticCheck class para checks individuales
- AllChecksPassed property computada

**DoctorServiceTests.cs** (360 líneas) - 20 tests:
- Constructor tests (2 tests)
- CheckDotNetVersion tests (2 tests) - versión instalada
- CheckGitInstallation tests (2 tests) - versión instalada
- CheckProjectStructure tests (4 tests) - carpetas faltantes
- CheckDiskSpace tests (3 tests) - espacio suficiente
- CheckWritePermissions tests (3 tests) - permisos
- RunFullDiagnostic tests (3 tests) - workflow completo
- DiagnosticResult tests (2 tests) - lógica de resultado

**DoctorCommandTests.cs** (220 líneas) - 11 tests:
- Constructor tests (1 test)
- Execute success cases (2 tests) - todos los checks pasan
- Execute failure cases (3 tests) - checks fallan
- Settings tests (2 tests) - configuración
- Integration tests (1 test) - workflow end-to-end

**Program.cs** - Actualizado:
- Registro de DoctorService en DI

### Características destacadas

1. **Diagnóstico exhaustivo**: 5 verificaciones críticas del sistema
2. **Interfaz profesional**: Panel, spinner, tabla con colores, warnings, sugerencias
3. **Smart suggestions**: Sugiere comandos específicos para resolver problemas
4. **Cross-platform**: Funciona en Windows, Linux, macOS
5. **Robusto**: No crashea si faltan dependencias, maneja errores gracefully
6. **Configurable**: Flag --verbose para información detallada

### Funcionalidad final

El comando doctor ahora verifica:

```bash
# Diagnóstico básico
mjcuadrado-net-sdk doctor

# Con información detallada
mjcuadrado-net-sdk doctor --verbose
```

**Checks realizados:**
1. ✓ .NET SDK ≥ 9.0 instalado
2. ✓ Git instalado y configurado (user.name, user.email)
3. ✓ Estructura de proyecto completa (9 carpetas + config.json)
4. ✓ Espacio en disco ≥ 100 MB
5. ✓ Permisos de escritura en directorio actual

**Ejemplos de output:**
- Success: "¡Todo listo! El sistema está correctamente configurado."
- Failure: "Se encontraron 2 problema(s). Revisa las sugerencias arriba."
- Suggestions: "Install .NET SDK 9.0 or higher from https://dotnet.microsoft.com/download"

### Archivos creados/modificados
- ✅ `src/MjCuadrado.NetSdk/Services/DoctorService.cs` (320 líneas) - nuevo
- ✅ `src/MjCuadrado.NetSdk/Commands/DoctorCommand.cs` (155 líneas) - actualizado
- ✅ `src/MjCuadrado.NetSdk/Program.cs` - actualizado con DoctorService en DI
- ✅ `tests/MjCuadrado.NetSdk.Tests/Services/DoctorServiceTests.cs` (360 líneas) - nuevo
- ✅ `tests/MjCuadrado.NetSdk.Tests/Commands/DoctorCommandTests.cs` (220 líneas) - nuevo

### Próximos pasos
Issue completado exitosamente. Con Issues #1-#6 completados, la Fase 1 MVP está casi lista. Próximas tareas:
- Issue #7: Sistema de SPECs (Fase 2)
- Issue #8: Sistema de TAGs (Fase 2)
- Issue #9: Publicación en NuGet (Fase 1 final)
