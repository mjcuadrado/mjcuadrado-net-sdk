# Issue #5: Comando init

**Título:** Implementar comando `mjcuadrado-net-sdk init [nombre-proyecto]`

## 📋 Descripción
Implementar el comando principal que inicializa nuevos proyectos con la estructura completa. Debe usar Spectre.Console.Cli para la interfaz y los servicios creados previamente.

## 🎯 Objetivos
- [ ] Comando funcional con interfaz CLI elegante
- [ ] Soporta crear proyectos nuevos y en directorio actual
- [ ] Validaciones y mensajes de error claros
- [ ] Output visual atractivo con Spectre.Console

## 📝 Tareas técnicas
- [ ] Crear `InitCommand.cs` en `src/Commands/`
- [ ] Implementar clase heredando de `Command<InitCommand.Settings>`
- [ ] Crear clase `Settings` con:
  - `[CommandArgument(0, "[nombre-proyecto]")]`
  - `[CommandOption("--force")]` para sobrescribir
- [ ] Implementar método `Execute()`:
  1. Determinar ruta del proyecto (nueva carpeta o directorio actual)
  2. Validar que la ruta no exista (o usar `--force`)
  3. Usar `FileSystemService` para crear estructura
  4. Usar `TemplateService` para generar archivos
  5. Usar `ConfigurationService` para crear config.json
  6. Mostrar progreso con `Spectre.Console.Status`
  7. Mostrar resumen de éxito con `Spectre.Console.Table`
- [ ] Validaciones:
  - Nombre de proyecto válido (sin caracteres especiales: `/\:*?"<>|`)
  - Permisos de escritura en el directorio
  - Espacio suficiente en disco
  - No sobrescribir proyectos existentes (sin --force)
- [ ] Output con Spectre.Console:
  - Spinner durante creación
  - Tabla con resumen de carpetas creadas
  - Panel con "próximos pasos"
  - Colores y emojis

## ✅ Criterios de aceptación
- [ ] `mjcuadrado-net-sdk init mi-proyecto` crea carpeta nueva
- [ ] `mjcuadrado-net-sdk init` (sin args) inicializa en directorio actual
- [ ] `mjcuadrado-net-sdk init mi-proyecto --force` sobrescribe
- [ ] Muestra errores claros si falta permisos
- [ ] Output visualmente atractivo y claro
- [ ] Genera estructura completa correctamente
- [ ] config.json válido creado con valores por defecto

## 🧪 Tests requeridos
- [ ] `InitCommandTests.cs`
- [ ] `Execute_WithProjectName_CreatesNewFolder`
- [ ] `Execute_WithoutProjectName_InitializesCurrentDirectory`
- [ ] `Execute_ExistingProject_ReturnsError`
- [ ] `Execute_WithForce_OverwritesExisting`
- [ ] `Execute_InvalidProjectName_ReturnsError`
- [ ] `Execute_NoPermissions_ReturnsError`
- [ ] Tests de integración verificando estructura completa

## 🔗 Dependencias
- Depende de: #2 (FileSystemService)
- Depende de: #3 (ConfigurationService)
- Depende de: #4 (TemplateService)

## 📚 Referencias
- [Spectre.Console.Cli Documentation](https://spectreconsole.net/cli/)
- Output esperado definido en prompt principal

## 🏷️ Labels sugeridas
`phase-1`, `cli`, `command`, `high-priority`
