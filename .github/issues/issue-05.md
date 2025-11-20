# Issue #5: Comando init

**Estado:** ✅ **COMPLETADO** (2024-11-20)

**Título:** Implementar comando `mjcuadrado-net-sdk init [nombre-proyecto]`

## 📋 Descripción
Implementar el comando principal que inicializa nuevos proyectos con la estructura completa. Debe usar Spectre.Console.Cli para la interfaz y los servicios creados previamente.

## 🎯 Objetivos
- [x] Comando funcional con interfaz CLI elegante
- [x] Soporta crear proyectos nuevos y en directorio actual
- [x] Validaciones y mensajes de error claros
- [x] Output visual atractivo con Spectre.Console

## 📝 Tareas técnicas
- [x] Crear `InitCommand.cs` en `src/Commands/`
- [x] Implementar clase heredando de `Command<InitCommand.Settings>`
- [x] Crear clase `Settings` con:
  - `[CommandArgument(0, "[nombre-proyecto]")]`
  - `[CommandOption("--force")]` para sobrescribir
  - `[CommandOption("--author")]` para autor
  - `[CommandOption("--framework")]` para framework
- [x] Implementar método `Execute()`:
  1. Determinar ruta del proyecto (nueva carpeta o directorio actual)
  2. Validar que la ruta no exista (o usar `--force`)
  3. Usar `FileSystemService` para validaciones
  4. Usar `TemplateService` para generar estructura
  5. Config.json generado por TemplateService
  6. Mostrar progreso con `Spectre.Console.Status`
  7. Mostrar resumen de éxito con `Spectre.Console.Table`
- [x] Validaciones:
  - Nombre de proyecto válido (sin caracteres especiales: `/\:*?"<>|`)
  - Permisos de escritura en el directorio
  - Espacio suficiente en disco (mínimo 10 MB)
  - No sobrescribir proyectos existentes (sin --force)
- [x] Output con Spectre.Console:
  - Spinner durante creación
  - Tabla con resumen de carpetas creadas
  - Panel con "próximos pasos"
  - Colores y emojis (✓, ✗, 📋, 🚀)

## ✅ Criterios de aceptación
- [x] `mjcuadrado-net-sdk init mi-proyecto` crea carpeta nueva
- [x] `mjcuadrado-net-sdk init` (sin args) inicializa en directorio actual
- [x] `mjcuadrado-net-sdk init mi-proyecto --force` sobrescribe
- [x] Muestra errores claros si falta permisos
- [x] Output visualmente atractivo y claro
- [x] Genera estructura completa correctamente
- [x] config.json válido creado con valores por defecto

## 🧪 Tests requeridos
- [x] `InitCommandTests.cs` (25 tests totales)
- [x] `Execute_WithProjectName_CreatesNewFolder`
- [x] `Execute_WithoutProjectName_InitializesCurrentDirectory`
- [x] `Execute_ExistingProject_ReturnsError`
- [x] `Execute_WithForce_OverwritesExisting`
- [x] `Execute_InvalidProjectName_ReturnsError` (9 variantes)
- [x] `Execute_NoPermissions_ReturnsError`
- [x] `Execute_InsufficientDiskSpace_ReturnsError`
- [x] `Execute_TemplateServiceFails_ReturnsError`
- [x] `Execute_UnexpectedException_ReturnsError`
- [x] `Execute_ValidProjectName_Succeeds` (6 variantes)
- [x] Tests de integración verificando estructura completa

## 🔗 Dependencias
- Depende de: #2 (FileSystemService)
- Depende de: #3 (ConfigurationService)
- Depende de: #4 (TemplateService)

## 📚 Referencias
- [Spectre.Console.Cli Documentation](https://spectreconsole.net/cli/)
- Output esperado definido en prompt principal

## 🏷️ Labels sugeridas
`phase-1`, `cli`, `command`, `high-priority`

---

## 📊 Resumen de cierre

**Fecha de cierre:** 2024-11-20
**Estado:** ✅ COMPLETADO

### Resultados de tests
```
Test Results:
- Passed: 158 (25 InitCommand + 133 otros servicios)
- Failed: 0
- Skipped: 0
- Total: 158
Duration: 442 ms
Coverage: 100% de tests passing
```

### Implementación completada

**InitCommand.cs** (265 líneas) - Comando CLI completo con:
- Clase Settings con 4 opciones configurables (ProjectName, Force, Author, Framework)
- Método Execute() con 7 pasos de validación y creación
- Validaciones robustas: nombres de proyecto, permisos, espacio en disco
- Integración con 3 servicios (FileSystemService, ConfigurationService, TemplateService)
- UI rica con Spectre.Console: spinner, tablas, paneles, colores
- Manejo completo de excepciones

**InitCommandTests.cs** (430 líneas) - Suite de tests completa con:
- 25 tests unitarios organizados en 6 categorías:
  1. Constructor Tests (3 tests) - validación de argumentos nulos
  2. Execute Tests - Project Creation (4 tests) - flujo de creación
  3. Execute Tests - Validation Errors (6 tests) - validaciones
  4. Valid Project Names (6 tests) - nombres permitidos
  5. Settings Tests (2 tests) - configuración por defecto
  6. Integration Tests (1 test) - workflow completo end-to-end
- Theory tests para múltiples variantes de nombres inválidos/válidos
- IDisposable pattern para cleanup automático
- Uso de NSubstitute para mocking de servicios

**Program.cs** - Configuración de DI:
- Integración con Microsoft.Extensions.DependencyInjection
- TypeRegistrar personalizado para Spectre.Console.Cli
- Registro de 3 servicios como Singleton

**TypeRegistrar.cs** (66 líneas) - Infraestructura nueva:
- Adaptador entre Spectre.Console.Cli y Microsoft.Extensions.DependencyInjection
- TypeResolver para resolución de dependencias

### Características destacadas

1. **Interfaz CLI profesional**: Spinner animado, tabla de estructura, panel de próximos pasos
2. **Validaciones exhaustivas**: Nombres, permisos, espacio en disco (mínimo 10 MB)
3. **Modo --force**: Permite sobrescribir proyectos existentes
4. **Opciones personalizables**: --author, --framework para customización
5. **Integración completa**: Usa los 3 servicios principales del SDK
6. **100% testeable**: 25 tests cubriendo todos los casos edge

### Funcionalidad final

El comando init ahora soporta:

```bash
# Crear nuevo proyecto en carpeta nueva
mjcuadrado-net-sdk init mi-proyecto

# Inicializar en directorio actual
mjcuadrado-net-sdk init

# Sobrescribir proyecto existente
mjcuadrado-net-sdk init mi-proyecto --force

# Con opciones personalizadas
mjcuadrado-net-sdk init mi-proyecto --author "@developer" --framework "net9.0"
```

### Archivos creados/modificados
- ✅ `src/MjCuadrado.NetSdk/Commands/InitCommand.cs` (265 líneas) - actualizado
- ✅ `src/MjCuadrado.NetSdk/Infrastructure/TypeRegistrar.cs` (66 líneas) - nuevo
- ✅ `src/MjCuadrado.NetSdk/Program.cs` - actualizado con DI
- ✅ `tests/MjCuadrado.NetSdk.Tests/Commands/InitCommandTests.cs` (430 líneas) - nuevo
- ✅ Paquete agregado: Microsoft.Extensions.DependencyInjection 10.0.0
- ✅ Paquete agregado: NSubstitute 5.3.0 (tests)

### Próximos pasos
Issue completado exitosamente. Listo para continuar con:
- Issue #6: Comando `doctor` funcional
