# Issue #3: Sistema de configuración (ConfigurationService)

**Título:** Implementar ConfigurationService para lectura/escritura de config.json

## 📋 Descripción
Implementar el sistema de configuración que lea y escriba el archivo `config.json` con validación de esquema y manejo de versiones.

## 🎯 Objetivos
- [ ] Crear modelos de configuración type-safe
- [ ] Implementar servicio de configuración con validación
- [ ] Soporte para esquema definido en el prompt
- [ ] Cobertura de tests ≥ 85%

## 📝 Tareas técnicas
- [ ] Crear modelos en `src/Models/`:
  - `SdkConfiguration.cs` (clase principal)
  - `ProjectConfig.cs`
  - `SdkConfig.cs`
  - `LanguageConfig.cs`
  - `GitHubConfig.cs`
  - `OptimizationConfig.cs`
- [ ] Crear interfaz `IConfigurationService`
- [ ] Implementar `ConfigurationService` con métodos:
  - `LoadConfiguration(string path)` → retorna `SdkConfiguration`
  - `SaveConfiguration(string path, SdkConfiguration config)`
  - `ValidateConfiguration(SdkConfiguration config)` → retorna lista de errores
  - `CreateDefaultConfiguration(string projectName)` → retorna config por defecto
  - `MergeConfigurations(SdkConfiguration base, SdkConfiguration overrides)`
- [ ] Usar `System.Text.Json` con opciones:
  - `PropertyNamingPolicy = JsonNamingPolicy.CamelCase`
  - `WriteIndented = true`
  - `DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull`
- [ ] Implementar validaciones:
  - Versiones en formato semver válido
  - Nombres de proyecto válidos (sin caracteres especiales)
  - Fechas en formato ISO 8601
  - Idiomas soportados: es, en, pt, fr
- [ ] Crear template `config.json.template` en `src/Templates/`

## ✅ Criterios de aceptación
- [ ] Lee y escribe JSON correctamente
- [ ] Valida esquema completo
- [ ] Maneja archivos corruptos sin crashear
- [ ] Retorna errores descriptivos de validación
- [ ] Soporta configuración parcial (valores por defecto)
- [ ] Preserva campos desconocidos (forward compatibility)

## 🧪 Tests requeridos
- [ ] `ConfigurationServiceTests.cs`
- [ ] `LoadConfiguration_ValidFile_ReturnsConfiguration`
- [ ] `LoadConfiguration_InvalidJson_ThrowsException`
- [ ] `SaveConfiguration_WritesCorrectFormat`
- [ ] `ValidateConfiguration_InvalidVersion_ReturnsErrors`
- [ ] `CreateDefaultConfiguration_ReturnsValidConfig`
- [ ] `MergeConfigurations_OverridesCorrectly`
- [ ] Tests con archivos JSON de ejemplo válidos e inválidos

## 🔗 Dependencias
- Depende de: #1 (estructura base)

## 📚 Referencias
- [System.Text.Json Documentation](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json)
- Esquema config.json definido en el prompt principal

## 🏷️ Labels sugeridas
`phase-1`, `core`, `service`, `configuration`
