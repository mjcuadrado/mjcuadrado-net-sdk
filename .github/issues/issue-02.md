# Issue #2: Sistema de gestión de archivos (FileSystemService)

**Título:** Implementar FileSystemService para creación y gestión de estructura de carpetas

## 📋 Descripción
Crear un servicio reutilizable que maneje todas las operaciones de sistema de archivos necesarias para el SDK: crear directorios, copiar templates, verificar permisos, etc.

## 🎯 Objetivos
- [ ] Implementar `FileSystemService` con operaciones básicas
- [ ] Manejo robusto de errores y excepciones
- [ ] Cobertura de tests ≥ 85%

## 📝 Tareas técnicas
- [ ] Crear interfaz `IFileSystemService` en `src/Services/`
- [ ] Implementar `FileSystemService` con métodos:
  - `CreateDirectory(string path)`
  - `CreateDirectoryStructure(string basePath, string[] folders)`
  - `FileExists(string path)`
  - `DirectoryExists(string path)`
  - `CopyFile(string source, string destination)`
  - `WriteTextFile(string path, string content)`
  - `ReadTextFile(string path)`
  - `GetCurrentDirectory()`
  - `EnsureDirectoryExists(string path)`
- [ ] Implementar validaciones:
  - Permisos de escritura
  - Espacio en disco suficiente
  - Rutas válidas
- [ ] Manejo de excepciones específicas:
  - `UnauthorizedAccessException`
  - `IOException`
  - `PathTooLongException`
- [ ] Logging con `ILogger<FileSystemService>`

## ✅ Criterios de aceptación
- [ ] Todos los métodos manejan excepciones apropiadamente
- [ ] Retorna errores descriptivos al usuario
- [ ] Funciona en Windows, Linux y macOS
- [ ] No lanza excepciones no controladas
- [ ] Paths normalizados correctamente (Windows `\` vs Unix `/`)

## 🧪 Tests requeridos
- [ ] `FileSystemServiceTests.cs`
- [ ] `CreateDirectory_WhenPathValid_CreatesDirectory`
- [ ] `CreateDirectory_WhenNoPermissions_ThrowsException`
- [ ] `CreateDirectoryStructure_CreatesAllFolders`
- [ ] `WriteTextFile_CreatesFileWithContent`
- [ ] `DirectoryExists_ReturnsCorrectValue`
- [ ] Tests con directorios temporales (usar `Path.GetTempPath()`)

## 🔗 Dependencias
- Depende de: #1 (estructura base debe estar lista)

## 📚 Referencias
- [System.IO Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.io)
- Patrón Service en .NET

## 🏷️ Labels sugeridas
`phase-1`, `core`, `service`, `testing`
