# Issue #4: Servicio de templates (TemplateService)

**Título:** Implementar TemplateService para gestión de templates de carpetas y archivos

## 📋 Descripción
Crear un servicio que gestione los templates necesarios para inicializar proyectos: estructura de carpetas, archivos README, config.json, etc.

## 🎯 Objetivos
- [ ] Implementar sistema de templates flexible
- [ ] Generar estructura completa definida en el prompt
- [ ] Soportar reemplazo de variables en templates

## 📝 Tareas técnicas
- [ ] Crear interfaz `ITemplateService`
- [ ] Implementar `TemplateService` con métodos:
  - `GenerateProjectStructure(string basePath, string projectName)`
  - `GenerateConfigFile(string path, ProjectConfig config)`
  - `GenerateReadmeFiles(string basePath, string projectName)`
  - `GetTemplateContent(string templateName)` → retorna contenido
  - `ReplaceVariables(string content, Dictionary<string, string> variables)`
- [ ] Crear templates embebidos en `src/Templates/`:
  - `config.json.template`
  - `README.md.template`
  - `product.md.template`
  - `structure.md.template`
  - `tech.md.template`
  - `specs-README.md.template`
  - `memory-README.md.template`
  - `reports-README.md.template`
  - `claude-agents-README.md.template`
  - `claude-commands-README.md.template`
  - `claude-skills-README.md.template`
  - `claude-hooks-README.md.template`
- [ ] Implementar reemplazo de variables:
  - `{{PROJECT_NAME}}`
  - `{{VERSION}}`
  - `{{DATE}}`
  - `{{AUTHOR}}`
  - `{{FRAMEWORK}}`
- [ ] Estructura de carpetas completa según prompt

## ✅ Criterios de aceptación
- [ ] Genera estructura completa correctamente
- [ ] Reemplaza variables en todos los templates
- [ ] READMEs generados son informativos y correctos
- [ ] Maneja errores si templates no existen
- [ ] Funciona con rutas absolutas y relativas

## 🧪 Tests requeridos
- [ ] `TemplateServiceTests.cs`
- [ ] `GenerateProjectStructure_CreatesAllFolders`
- [ ] `GenerateConfigFile_ReplacesVariables`
- [ ] `GenerateReadmeFiles_CreatesAllReadmes`
- [ ] `ReplaceVariables_ReplacesAllOccurrences`
- [ ] `GetTemplateContent_ReturnsContent`
- [ ] Tests verificando que cada carpeta se crea

## 🔗 Dependencias
- Depende de: #2 (FileSystemService debe existir)

## 📚 Referencias
- [Embedded Resources in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/create-resource-files)
- moai-adk estructura de carpetas

## 🏷️ Labels sugeridas
`phase-1`, `core`, `service`, `templates`
