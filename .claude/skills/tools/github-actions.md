---
name: github-actions
description: CI/CD automatizado con GitHub Actions - workflows, matrix builds, caching, secrets
version: 0.1.0
tags: [github-actions, ci-cd, automation, devops, workflows]
---

# GitHub Actions Skill

## 📚 Resumen

**GitHub Actions** es la plataforma de CI/CD nativa de GitHub que automatiza build, test y deployment directamente desde los repositorios.

**Beneficios:**
- 🚀 **Integración nativa:** Sin configuración externa
- 🔄 **Automatización completa:** Build, test, deploy
- 📦 **Marketplace:** Miles de actions reutilizables
- 💰 **Gratis:** 2,000 minutos/mes para repos privados
- 🎯 **Flexible:** YAML simple y potente

---

## 🔧 Conceptos Básicos

### Estructura de un Workflow

```yaml
name: CI                    # Nombre del workflow
on: [push, pull_request]    # Triggers (cuándo ejecutar)

jobs:                       # Conjunto de jobs
  build:                    # Nombre del job
    runs-on: ubuntu-latest  # Runner (dónde ejecutar)
    steps:                  # Pasos secuenciales
      - uses: actions/checkout@v4     # Action del marketplace
      - run: npm install              # Comando shell
      - run: npm test                 # Otro comando
```

### Anatomía de un Workflow

```
Workflow (CI/CD Pipeline)
  ├── Job 1 (build)
  │   ├── Step 1 (checkout)
  │   ├── Step 2 (install)
  │   └── Step 3 (build)
  ├── Job 2 (test)
  │   ├── Step 1 (checkout)
  │   └── Step 2 (test)
  └── Job 3 (deploy)
      └── Steps...
```

---

## 📝 Sintaxis de Workflows

### Triggers (on)

```yaml
# Push a branch
on:
  push:
    branches: [main, develop]

# Pull request
on:
  pull_request:
    branches: [main]

# Múltiples eventos
on: [push, pull_request, workflow_dispatch]

# Schedule (cron)
on:
  schedule:
    - cron: '0 2 * * *'  # Diario a las 2 AM

# Manual trigger
on:
  workflow_dispatch:
    inputs:
      environment:
        description: 'Environment to deploy'
        required: true
        default: 'staging'
```

### Jobs

```yaml
jobs:
  job1:
    runs-on: ubuntu-latest
    steps:
      - run: echo "Job 1"

  job2:
    runs-on: ubuntu-latest
    needs: job1  # Espera a que job1 termine
    steps:
      - run: echo "Job 2"

  job3:
    runs-on: ubuntu-latest
    needs: [job1, job2]  # Espera a múltiples jobs
    steps:
      - run: echo "Job 3"
```

### Steps

```yaml
steps:
  # Usar action del marketplace
  - uses: actions/checkout@v4

  # Ejecutar comando
  - run: npm install

  # Comando con nombre
  - name: Run tests
    run: npm test

  # Comando multilínea
  - name: Build app
    run: |
      npm run build
      npm run package

  # Con working directory
  - name: Build backend
    run: dotnet build
    working-directory: ./backend
```

---

## 🎯 Ejemplos Prácticos

### Backend .NET CI

```yaml
name: Backend CI

on:
  push:
    branches: [main, develop]
  pull_request:
    branches: [main]

jobs:
  build-and-test:
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'

      - name: Restore dependencies
        run: dotnet restore

      - name: Build
        run: dotnet build --no-restore --configuration Release

      - name: Test
        run: dotnet test --no-build --verbosity normal --collect:"XPlat Code Coverage"

      - name: Upload coverage
        uses: codecov/codecov-action@v4
        with:
          file: ./coverage.xml
```

### Frontend React CI

```yaml
name: Frontend CI

on:
  push:
    branches: [main]
  pull_request:
    branches: [main]

jobs:
  build-and-test:
    runs-on: ubuntu-latest

    strategy:
      matrix:
        node-version: [18.x, 20.x]

    steps:
      - uses: actions/checkout@v4

      - name: Setup Node.js ${{ matrix.node-version }}
        uses: actions/setup-node@v4
        with:
          node-version: ${{ matrix.node-version }}
          cache: 'npm'

      - name: Install dependencies
        run: npm ci

      - name: Lint
        run: npm run lint

      - name: Test
        run: npm test

      - name: Build
        run: npm run build

      - name: Upload build artifacts
        uses: actions/upload-artifact@v4
        with:
          name: dist
          path: dist/
```

---

## 🔐 Secrets Management

### Definir Secrets

```
GitHub Repo → Settings → Secrets and variables → Actions → New repository secret
```

### Usar Secrets

```yaml
jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - name: Deploy to production
        env:
          DATABASE_URL: ${{ secrets.DATABASE_URL }}
          API_KEY: ${{ secrets.API_KEY }}
        run: ./deploy.sh
```

### Environments

```yaml
jobs:
  deploy:
    runs-on: ubuntu-latest
    environment: production  # Requiere aprobación
    steps:
      - name: Deploy
        run: echo "Deploying to production"
```

---

## ⚡ Caching

### Cache de dependencias

```yaml
# NPM
- name: Cache node modules
  uses: actions/cache@v4
  with:
    path: ~/.npm
    key: ${{ runner.os }}-node-${{ hashFiles('**/package-lock.json') }}

# NuGet
- name: Cache NuGet packages
  uses: actions/cache@v4
  with:
    path: ~/.nuget/packages
    key: ${{ runner.os }}-nuget-${{ hashFiles('**/*.csproj') }}

# Docker layers
- name: Cache Docker layers
  uses: actions/cache@v4
  with:
    path: /tmp/.buildx-cache
    key: ${{ runner.os }}-buildx-${{ github.sha }}
    restore-keys: |
      ${{ runner.os }}-buildx-
```

---

## 🔄 Matrix Builds

```yaml
jobs:
  test:
    runs-on: ${{ matrix.os }}
    strategy:
      matrix:
        os: [ubuntu-latest, windows-latest, macos-latest]
        node-version: [18.x, 20.x]
        include:
          - os: ubuntu-latest
            node-version: 16.x

    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-node@v4
        with:
          node-version: ${{ matrix.node-version }}
      - run: npm ci
      - run: npm test
```

---

## 📦 Docker Build & Push

```yaml
jobs:
  docker:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Set up Docker Buildx
        uses: docker/setup-buildx-action@v3

      - name: Login to Docker Hub
        uses: docker/login-action@v3
        with:
          username: ${{ secrets.DOCKER_USERNAME }}
          password: ${{ secrets.DOCKER_TOKEN }}

      - name: Build and push
        uses: docker/build-push-action@v5
        with:
          context: .
          push: true
          tags: |
            myapp/backend:${{ github.sha }}
            myapp/backend:latest
          cache-from: type=gha
          cache-to: type=gha,mode=max
```

---

## 🎯 Best Practices

### 1. Fail Fast

```yaml
strategy:
  fail-fast: true  # Para en el primer error
  matrix:
    os: [ubuntu, windows]
```

### 2. Paralelización

```yaml
# Jobs paralelos (más rápido)
jobs:
  lint:
    runs-on: ubuntu-latest
    steps: [...]

  test:
    runs-on: ubuntu-latest  # Corre en paralelo con lint
    steps: [...]
```

### 3. Conditional Steps

```yaml
- name: Deploy to production
  if: github.ref == 'refs/heads/main'
  run: ./deploy.sh
```

### 4. Reusable Workflows

```yaml
# .github/workflows/reusable.yml
on:
  workflow_call:
    inputs:
      environment:
        required: true
        type: string

# Llamar desde otro workflow
jobs:
  deploy:
    uses: ./.github/workflows/reusable.yml@main
    with:
      environment: production
```

---

## 📚 Recursos

**Documentación:**
- GitHub Actions: https://docs.github.com/actions
- Workflow Syntax: https://docs.github.com/actions/reference/workflow-syntax
- Marketplace: https://github.com/marketplace?type=actions

---

**Versión:** 0.1.0
**Última Actualización:** 2025-11-22
**Mantenido por:** mjcuadrado-net-sdk
