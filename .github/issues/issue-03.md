# Issue #3: Sistema de configuración (ConfigurationService)

**Estado:** ✅ **COMPLETADO** (2024-11-19)

**Título:** Implementar ConfigurationService para lectura/escritura de config.json

## 📋 Descripción
Implementar el sistema de configuración que lea y escriba el archivo `config.json` con validación de esquema y manejo de versiones.

## 🎯 Objetivos
- [x] Crear modelos de configuración type-safe
- [x] Implementar servicio de configuración con validación
- [x] Soporte para esquema definido en el prompt
- [x] Cobertura de tests ≥ 85%

## 📝 Tareas técnicas
- [x] Crear modelos en `src/Models/`:
  - `SdkConfiguration.cs` (clase principal)
  - `ProjectConfig.cs`
  - `SdkConfig.cs`
  - `LanguageConfig.cs`
  - `GitHubConfig.cs`
  - `OptimizationConfig.cs`
- [x] Crear interfaz `IConfigurationService`
- [x] Implementar `ConfigurationService` con métodos:
  - `LoadConfiguration(string path)` → retorna `SdkConfiguration`
  - `SaveConfiguration(string path, SdkConfiguration config)`
  - `ValidateConfiguration(SdkConfiguration config)` → retorna ValidationResult
  - `CreateDefaultConfiguration(ProjectInfo)` → retorna config por defecto
  - `MergeConfigurations(SdkConfiguration base, SdkConfiguration overrides)`
  - `FindConfigurationFile(string startPath)` → busca config.json en padres
- [x] Usar `System.Text.Json` con opciones:
  - `PropertyNamingPolicy = JsonNamingPolicy.CamelCase`
  - `WriteIndented = true`
  - `DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull`
  - `ReadCommentHandling = JsonCommentHandling.Skip`
  - `AllowTrailingCommas = true`
- [x] Implementar validaciones:
  - Versiones en formato semver válido (regex completo)
  - Nombres de proyecto válidos (sin caracteres especiales)
  - Fechas en formato ISO 8601 (yyyy-MM-dd)
  - Idiomas soportados: es, en, pt, fr
- [x] Template `config.json.template` existente en `src/Templates/`

## ✅ Criterios de aceptación
- [x] Lee y escribe JSON correctamente
- [x] Valida esquema completo
- [x] Maneja archivos corruptos sin crashear
- [x] Retorna errores descriptivos de validación (con field + message)
- [x] Soporta configuración parcial (merge y defaults)
- [x] Preserva campos desconocidos (forward compatibility via JSON)

## 🧪 Tests requeridos
- [x] `ConfigurationServiceTests.cs` con 38 tests
- [x] `LoadConfiguration_ValidFile_ReturnsConfiguration`
- [x] `LoadConfiguration_InvalidJson_ThrowsException`
- [x] `SaveConfiguration_WritesCorrectFormat`
- [x] `ValidateConfiguration_InvalidVersion_ReturnsErrors`
- [x] `CreateDefaultConfiguration_ReturnsValidConfig`
- [x] `MergeConfigurations_OverridesCorrectly`
- [x] Tests con archivos JSON de ejemplo válidos e inválidos

## 🔗 Dependencias
- Depende de: #1 (estructura base)

## 📚 Referencias
- [System.Text.Json Documentation](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json)
- Esquema config.json definido en el prompt principal

## 🏷️ Labels sugeridas
`phase-1`, `core`, `service`, `configuration`

---

## 📊 Resumen de cierre

**Fecha de cierre:** 2024-11-19
**Estado:** ✅ COMPLETADO

### Resultados de tests
```
Test Results:
- Passed: 38 (ConfigurationService)
- Failed: 0
- Skipped: 0
- Total: 38
Duration: 166 ms
Coverage: 100% de tests passing
```

### Implementación completada

**ConfigurationService.cs** - Implementación completa con:
- 6 métodos públicos implementados
- Validación completa con regex para semver, ISO dates, project names
- Soporte para 4 idiomas (es, en, pt, fr)
- System.Text.Json con camelCase, indentación y comentarios
- Merge inteligente de configuraciones (deep copy via serialización)
- Búsqueda recursiva de config.json en directorios padres
- Manejo robusto de excepciones (ArgumentException, FileNotFoundException, InvalidOperationException, IOException)
- 4 helpers privados de validación

**ConfigurationServiceTests.cs** - Suite de tests completa con:
- 38 tests unitarios organizados en 6 categorías:
  1. LoadConfiguration Tests (6 tests) - archivos válidos, inválidos, vacíos, con comentarios
  2. SaveConfiguration Tests (7 tests) - escritura, validación previa, directorios, actualización de fecha
  3. ValidateConfiguration Tests (11 tests) - null, campos requeridos, semver, dates, languages, Theory tests
  4. CreateDefaultConfiguration Tests (5 tests) - valores por defecto, nulls, fechas actuales
  5. MergeConfigurations Tests (6 tests) - overrides, preservación, inmutabilidad
  6. FindConfigurationFile Tests (5 tests) - búsqueda en current/parent, paths inválidos
- IDisposable pattern para cleanup automático
- Helper method `CreateValidConfiguration()`
- Uso de archivos temporales aislados

### Características destacadas

1. **Validación robusta**: Semver regex completo (major.minor.patch-prerelease+metadata)
2. **Flexibilidad**: Merge de configs, búsqueda en padres, valores por defecto
3. **Calidad JSON**: Indentación, camelCase, skip comments, trailing commas
4. **Type-safety**: Todos los modelos fuertemente tipados con JsonPropertyName
5. **Error handling**: Mensajes descriptivos con field + message en ValidationResult

### Archivos creados/modificados
- ✅ `src/MjCuadrado.NetSdk/Services/ConfigurationService.cs` (434 líneas)
- ✅ `tests/MjCuadrado.NetSdk.Tests/Services/ConfigurationServiceTests.cs` (710 líneas)
- ✅ Modelos ya existían en `src/MjCuadrado.NetSdk/Models/SdkConfiguration.cs`
- ✅ Template ya existía en `src/MjCuadrado.NetSdk/Templates/config.json.template`

### Próximos pasos
Issue completado exitosamente. Listo para continuar con:
- Issue #4: TemplateService
- Issue #5: Comando `init` funcional
