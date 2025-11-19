# Issue #6: Comando doctor

**Título:** Implementar comando `mjcuadrado-net-sdk doctor` para diagnóstico del sistema

## 📋 Descripción
Implementar comando de diagnóstico que verifica todas las dependencias del sistema y la salud del proyecto actual.

## 🎯 Objetivos
- [ ] Verificar dependencias del sistema
- [ ] Validar estructura del proyecto actual
- [ ] Output claro con checks visuales (✓/✗)

## 📝 Tareas técnicas
- [ ] Crear `DoctorCommand.cs` en `src/Commands/`
- [ ] Implementar verificaciones:
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
- [ ] Crear `DoctorService.cs` para lógica de verificación:
  - `CheckDotNetVersion()` → (bool success, string version)
  - `CheckGitInstallation()` → (bool success, string version, bool configured)
  - `CheckProjectStructure()` → (bool success, List<string> missingItems)
  - `CheckDiskSpace()` → (bool success, long availableBytes)
  - `CheckWritePermissions()` → bool
- [ ] Output con Spectre.Console:
  - Tabla con resultados de cada check
  - ✓ en verde para éxito
  - ✗ en rojo para fallos
  - Warnings en amarillo
  - Resumen final: "Todo listo!" o "X problemas encontrados"
  - Sugerencias para resolver problemas

## ✅ Criterios de aceptación
- [ ] Detecta correctamente todas las dependencias
- [ ] Muestra versiones instaladas
- [ ] Identifica problemas de configuración
- [ ] Provee sugerencias de solución
- [ ] Funciona en Windows, Linux y macOS
- [ ] No crashea si faltan dependencias

## 🧪 Tests requeridos
- [ ] `DoctorCommandTests.cs`
- [ ] `DoctorServiceTests.cs`
- [ ] `CheckDotNetVersion_ReturnsCorrectVersion`
- [ ] `CheckGitInstallation_DetectsGit`
- [ ] `CheckProjectStructure_DetectsMissingFolders`
- [ ] `CheckDiskSpace_ReturnsAvailableSpace`
- [ ] Mocks de procesos externos (dotnet, git)

## 🔗 Dependencias
- Depende de: #1 (estructura base)
- Depende de: #3 (ConfigurationService para validar config.json)

## 📚 Referencias
- [Process.Start Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process)
- Output esperado en prompt principal

## 🏷️ Labels sugeridas
`phase-1`, `cli`, `command`, `diagnostics`
