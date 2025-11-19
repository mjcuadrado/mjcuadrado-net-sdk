# Fase 1: MVP - mjcuadrado-net-sdk

## Objetivos de la Fase 1

Crear un CLI funcional que permita:
1. Inicializar proyectos con estructura completa
2. Diagnosticar el sistema y proyecto
3. Gestionar configuración centralizada

## Alcance

### Incluido en Fase 1
- ✅ Estructura base del proyecto .NET 10
- ⏳ Comando `init [nombre-proyecto]`
- ⏳ Comando `doctor` con checks básicos
- ✅ Comando `version`
- ⏳ Sistema de templates embebidos
- ⏳ Servicio de archivos (FileSystemService)
- ⏳ Servicio de configuración (ConfigurationService)
- ⏳ Servicio de templates (TemplateService)
- ⏳ Tests unitarios con cobertura ≥ 85%
- ⏳ CI/CD con GitHub Actions
- ⏳ Documentación completa

### NO incluido en Fase 1 (fases futuras)
- ❌ Sistema de SPECs
- ❌ Sistema de TAGs
- ❌ Integración con EF Core
- ❌ Comandos avanzados (spec, tags)
- ❌ Agentes y Skills completos

## Issues de desarrollo

La Fase 1 se divide en 9 issues:

1. **Issue #1**: Estructura base
2. **Issue #2**: FileSystemService
3. **Issue #3**: ConfigurationService
4. **Issue #4**: TemplateService
5. **Issue #5**: Comando `init`
6. **Issue #6**: Comando `doctor`
7. **Issue #7**: Comando `version` (✅ Completado)
8. **Issue #8**: Documentación
9. **Issue #9**: CI/CD

## Flujo del comando `init`

```
Usuario: mjcuadrado-net-sdk init mi-proyecto
    ↓
InitCommand.Execute()
    ↓
1. Validar nombre del proyecto
2. Verificar permisos
3. FileSystemService.CreateDirectoryStructure()
4. TemplateService.GenerateProjectStructure()
5. ConfigurationService.SaveConfiguration()
    ↓
Output con Spectre.Console:
✓ Creando estructura...
✓ Generando configuración...
✓ Proyecto inicializado
```

## Flujo del comando `doctor`

```
Usuario: mjcuadrado-net-sdk doctor
    ↓
DoctorCommand.Execute()
    ↓
DoctorService.RunFullDiagnostic()
    ↓
1. CheckDotNetVersion()
2. CheckGitInstallation()
3. CheckProjectStructure()
4. CheckDiskSpace()
5. CheckWritePermissions()
    ↓
Output: Tabla con resultados de checks
```

## Tecnologías utilizadas

- **.NET 10**: Framework principal
- **Spectre.Console.Cli**: CLI framework
- **System.Text.Json**: Serialización JSON
- **xUnit**: Framework de testing
- **FluentAssertions**: Assertions para tests
- **Moq**: Mocking para tests
- **GitHub Actions**: CI/CD

## Criterios de completitud de Fase 1

Para considerar la Fase 1 completa:

- [ ] Todos los comandos funcionan correctamente
- [ ] Tests con cobertura ≥ 85%
- [ ] `dotnet build` compila sin warnings
- [ ] `dotnet test` 100% passing
- [ ] Documentación completa
- [ ] CI/CD pipeline verde
- [ ] README.md con ejemplos funcionales
- [ ] Todas las 9 issues cerradas

## Estimaciones

| Tarea | Horas estimadas |
|-------|----------------|
| Issue #1: Estructura base | 2h |
| Issue #2: FileSystemService | 4h |
| Issue #3: ConfigurationService | 4h |
| Issue #4: TemplateService | 3h |
| Issue #5: Comando init | 5h |
| Issue #6: Comando doctor | 4h |
| Issue #7: Comando version | 1h (✅ Completado) |
| Issue #8: Documentación | 3h |
| Issue #9: CI/CD | 2h |
| **Total** | **28h** |

## Próximos pasos después de Fase 1

Una vez completada la Fase 1:
1. Release v0.1.0
2. Comenzar Fase 2: Sistema de SPECs
3. Implementar comandos `spec new`, `spec validate`
4. Agregar sistema de TAGs con trazabilidad
