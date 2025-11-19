# Issue #9: CI/CD con GitHub Actions

**Título:** Configurar pipeline CI/CD con GitHub Actions

## 📋 Descripción
Configurar integración continua para ejecutar build, tests y validaciones en cada push y pull request.

## 🎯 Objetivos
- [ ] Build automático en cada push
- [ ] Tests automáticos
- [ ] Validación de código
- [ ] Badge de estado en README

## 📝 Tareas técnicas
- [ ] Crear `.github/workflows/ci.yml`
- [ ] Configurar workflow:
  ```yaml
  name: CI

  on:
    push:
      branches: [ main, develop ]
    pull_request:
      branches: [ main, develop ]

  jobs:
    build-and-test:
      runs-on: ubuntu-latest
      strategy:
        matrix:
          dotnet: ['9.0.x']

      steps:
      - uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: ${{ matrix.dotnet }}

      - name: Restore dependencies
        run: dotnet restore

      - name: Build
        run: dotnet build --no-restore --configuration Release

      - name: Test
        run: dotnet test --no-build --configuration Release --verbosity normal
  ```
- [ ] Agregar job para validación de formato:
  - `dotnet format --verify-no-changes`
- [ ] Agregar job para análisis estático (opcional):
  - SonarCloud o CodeQL
- [ ] Configurar test coverage report (opcional):
  - Coverlet + Codecov
- [ ] Multi-platform testing (opcional):
  - Matrix con: ubuntu-latest, windows-latest, macos-latest

## ✅ Criterios de aceptación
- [ ] Pipeline ejecuta en cada push
- [ ] Pipeline ejecuta en cada PR
- [ ] Build exitoso required para merge
- [ ] Tests ejecutan correctamente
- [ ] Badge en README muestra estado actual
- [ ] Pipeline falla si tests fallan

## 🧪 Tests requeridos
- [ ] Verificar que pipeline ejecuta correctamente
- [ ] Simular fallos para verificar que detecta errores

## 🔗 Dependencias
- Depende de: #1 (estructura base con tests)

## 📚 Referencias
- [GitHub Actions for .NET](https://docs.github.com/en/actions/automating-builds-and-tests/building-and-testing-net)
- [setup-dotnet action](https://github.com/actions/setup-dotnet)

## 🏷️ Labels sugeridas
`phase-1`, `ci-cd`, `infrastructure`, `devops`
