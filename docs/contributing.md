# Guía de Contribución - mjcuadrado-net-sdk

Gracias por tu interés en contribuir a mjcuadrado-net-sdk!

## Cómo contribuir

### 1. Fork y clone

```bash
# Fork el repositorio en GitHub
# Luego clona tu fork
git clone https://github.com/TU-USUARIO/mjcuadrado-net-sdk.git
cd mjcuadrado-net-sdk
```

### 2. Setup de desarrollo

```bash
# Restaurar dependencias
dotnet restore

# Verificar que compila
dotnet build

# Ejecutar tests
dotnet test
```

### 3. Crear una rama

```bash
git checkout -b feature/mi-nueva-feature
# o
git checkout -b fix/mi-bug-fix
```

### 4. Hacer cambios

- Sigue los estándares de código (ver abajo)
- Agrega tests para nueva funcionalidad
- Actualiza documentación si es necesario

### 5. Ejecutar validaciones

```bash
# Tests
dotnet test

# Verificar cobertura
dotnet test /p:CollectCoverage=true

# Build en Release
dotnet build -c Release
```

### 6. Commit y Push

```bash
git add .
git commit -m "feat: descripción del cambio"
git push origin feature/mi-nueva-feature
```

### 7. Crear Pull Request

- Ve a GitHub y crea un Pull Request
- Describe los cambios realizados
- Referencia el issue relacionado (si aplica)

## Estándares de código

### Convenciones C#

- Usar PascalCase para clases, métodos y propiedades públicas
- Usar camelCase para variables locales y parámetros
- Usar prefijo `I` para interfaces: `IFileSystemService`
- Usar `_` para campos privados: `_fileSystem`

### Documentación

```csharp
/// <summary>
/// Descripción breve del método
/// </summary>
/// <param name="paramName">Descripción del parámetro</param>
/// <returns>Descripción del retorno</returns>
public string MyMethod(string paramName)
{
    // Implementation
}
```

### Tests

- Un archivo de test por cada clase
- Nomenclatura: `[ClassName]Tests.cs`
- Nomenclatura de métodos: `[MethodName]_[Scenario]_[ExpectedResult]`

Ejemplo:
```csharp
[Fact]
public void LoadConfiguration_ValidFile_ReturnsConfiguration()
{
    // Arrange
    var service = new ConfigurationService();

    // Act
    var result = service.LoadConfiguration("valid.json");

    // Assert
    result.Should().NotBeNull();
}
```

### Coverage objetivo

- Cobertura mínima: **85%**
- Tests para todos los métodos públicos
- Tests para casos edge

## Proceso de revisión

1. Al menos 1 aprobación requerida
2. Todos los tests deben pasar
3. Coverage no debe bajar
4. Build exitoso en CI/CD

## Tipos de contribuciones

### Bug fixes
- Describe el bug claramente
- Agrega test que reproduce el bug
- Implementa el fix
- Verifica que el test pasa

### Nuevas features
- Abre un issue primero para discutir
- Implementa la feature siguiendo las issues existentes
- Agrega tests completos
- Actualiza documentación

### Documentación
- Corrige typos
- Mejora explicaciones
- Agrega ejemplos
- Traduce documentación

## Etiquetas de commit

Usar [Conventional Commits](https://www.conventionalcommits.org/):

- `feat:` Nueva funcionalidad
- `fix:` Corrección de bug
- `docs:` Cambios en documentación
- `test:` Agregar o modificar tests
- `refactor:` Refactorización sin cambiar funcionalidad
- `chore:` Tareas de mantenimiento

Ejemplos:
```
feat: add spec validate command
fix: doctor command not checking git config
docs: update README with new examples
test: add tests for ConfigurationService
```

## Reportar bugs

Usa el template de issue "Bug Report" e incluye:

- Versión del SDK
- Versión de .NET
- Sistema operativo
- Pasos para reproducir
- Comportamiento esperado vs actual
- Logs o screenshots si aplica

## Solicitar features

Usa el template "Feature Request" e incluye:

- Descripción clara de la feature
- Casos de uso
- Ejemplos de cómo se usaría
- Alternativas consideradas

## Preguntas

Si tienes preguntas, abre una discusión en GitHub o contacta a @mjcuadrado.

---

¡Gracias por contribuir! 🚀
