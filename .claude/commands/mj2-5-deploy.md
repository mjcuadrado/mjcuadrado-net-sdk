---
name: mj2-5-deploy
description: Despliega la aplicación usando estrategias DevOps automatizadas (project)
tags: [deployment, devops, production, ci-cd]
---

# /mj2:5-deploy - Deployment Automatizado

Comando para invocar al agente **devops-expert** y ejecutar deployments automatizados con estrategias DevOps profesionales.

---

## 📋 Uso

```bash
# Deployment básico a producción
/mj2:5-deploy production

# Deployment con versión específica
/mj2:5-deploy production --version 1.2.4

# Deployment con estrategia específica
/mj2:5-deploy staging --strategy blue-green

# Deployment con rollback automático
/mj2:5-deploy production --auto-rollback

# Dry-run (simular sin ejecutar)
/mj2:5-deploy production --dry-run
```

---

## 🎯 Parámetros

### Posicionales

**`<environment>`** (requerido)
- Entorno de deployment
- Valores: `development`, `staging`, `production`
- Ejemplo: `/mj2:5-deploy production`

### Opcionales

**`--version <version>`**
- Versión específica a desplegar
- Formato: semver (ej. `1.2.4`)
- Default: última versión en main
- Ejemplo: `/mj2:5-deploy prod --version 1.2.4`

**`--strategy <strategy>`**
- Estrategia de deployment
- Valores: `blue-green`, `rolling`, `canary`, `recreate`
- Default: `blue-green` (producción), `recreate` (dev/staging)
- Ejemplo: `/mj2:5-deploy prod --strategy canary`

**`--auto-rollback`**
- Rollback automático si fallan health checks
- Default: `true` (producción), `false` (dev/staging)
- Ejemplo: `/mj2:5-deploy prod --auto-rollback`

**`--dry-run`**
- Simular deployment sin ejecutar
- Muestra plan sin aplicar cambios
- Ejemplo: `/mj2:5-deploy prod --dry-run`

**`--skip-tests`**
- Saltar tests antes de deployment (NO RECOMENDADO)
- Default: `false`
- Ejemplo: `/mj2:5-deploy staging --skip-tests`

**`--force`**
- Forzar deployment aunque haya warnings (NO RECOMENDADO)
- Default: `false`
- Ejemplo: `/mj2:5-deploy prod --force`

---

## 🔄 Workflow del Comando

Cuando ejecutas `/mj2:5-deploy`, el agente devops-expert:

### 1. PLAN (Validación y Preparación)
```
📋 Analizando cambios desde último deployment...
   ✅ Backend: 15 commits, 3 PRs merged
   ✅ Frontend: 8 commits, 2 PRs merged
   ✅ Database: 2 migrations pendientes

📋 Validando prerequisites...
   ✅ Secrets configurados
   ✅ Resources disponibles
   ✅ Database migrations ready

📋 Estrategia seleccionada: blue-green
   → Zero-downtime deployment
   → Rollback instantáneo disponible
```

### 2. BUILD (Compilación y Testing)
```
🏗️ Building Docker images...
   ✅ backend:1.2.4 (200 MB) - 3m 45s
   ✅ frontend:1.2.4 (40 MB) - 2m 30s

🧪 Running tests...
   ✅ Unit tests: 195/195 passing
   ✅ Integration tests: 45/45 passing
   ✅ E2E tests: 23/23 passing

🔒 Security scan...
   ✅ 0 critical vulnerabilities
   ⚠️ 2 medium (non-blocking)
```

### 3. DEPLOY (Despliegue)
```
🚀 Deploying to production (blue-green)...
   ✅ Green environment deployed
   ✅ Smoke tests passing (5/5)
   ✅ Load balancer switched
   ✅ Blue environment decommissioned

⏱️ Deployment time: 8m 15s
```

### 4. VERIFY (Verificación)
```
✅ Health checks...
   ✅ Backend: healthy (3/3 replicas)
   ✅ Frontend: healthy (2/2 replicas)
   ✅ Database: healthy

📊 Performance metrics...
   ✅ P50 latency: 45ms
   ✅ P95 latency: 120ms
   ✅ Error rate: 0.01%

✅ Deployment completado exitosamente!
```

---

## 💡 Ejemplos de Uso

### Ejemplo 1: Deployment Estándar a Producción

```bash
/mj2:5-deploy production
```

**Output:**
```
🚀 Deployment a production iniciado

📋 PLAN
   Versión: 1.2.4 (auto-detected)
   Estrategia: blue-green
   Cambios: 23 commits desde v1.2.3

🏗️ BUILD
   ✅ Images built: backend:1.2.4, frontend:1.2.4
   ✅ Tests: 263/263 passing
   ✅ Security: 0 critical issues

🚀 DEPLOY
   ✅ Green deployed
   ✅ Load balancer switched
   ✅ Zero downtime

✅ VERIFY
   ✅ Health checks passing
   ✅ Performance metrics OK

✅ Deployment completado en 10m 30s

📝 Rollback disponible:
   docker-compose -f docker-compose.v1.2.3.yml up -d
```

### Ejemplo 2: Deployment con Versión Específica

```bash
/mj2:5-deploy staging --version 1.3.0-beta
```

**Output:**
```
🚀 Deployment de version 1.3.0-beta a staging

📋 PLAN
   ⚠️ Beta version detected
   Estrategia: recreate (staging env)

🏗️ BUILD
   ✅ Images built: backend:1.3.0-beta, frontend:1.3.0-beta
   ✅ Tests passing

🚀 DEPLOY
   ✅ Staging environment updated

✅ Deployment completado en 5m 15s
```

### Ejemplo 3: Dry-Run (Simulación)

```bash
/mj2:5-deploy production --dry-run
```

**Output:**
```
🔍 DRY-RUN MODE: No changes will be applied

📋 PLAN
   Version: 1.2.4
   Strategy: blue-green
   Changes:
     - Backend: 15 commits
     - Frontend: 8 commits
     - Database: 2 migrations

🏗️ BUILD (simulated)
   Would build:
     - registry.io/myapp-backend:1.2.4
     - registry.io/myapp-frontend:1.2.4
   Would run tests: 263 tests
   Would scan security

🚀 DEPLOY (simulated)
   Would:
     1. Deploy green environment
     2. Run smoke tests
     3. Switch load balancer
     4. Decommission blue

✅ VERIFY (simulated)
   Would check:
     - Health endpoints
     - Performance metrics
     - Error rates

📊 Estimated deployment time: ~10 minutes
✅ Dry-run completado - Todo OK para deployment real
```

### Ejemplo 4: Canary Release

```bash
/mj2:5-deploy production --strategy canary --version 1.2.4
```

**Output:**
```
🚀 Canary deployment a production

📋 PLAN
   Estrategia: canary (10% tráfico)
   Monitoring period: 30 minutos

🚀 DEPLOY
   ✅ Canary deployed (10% traffic)
   📊 Monitoring metrics...

   After 15 minutes:
   ✅ Error rate: 0.01% (same as stable)
   ✅ Latency: 50ms p95 (same as stable)

   ✅ Promoting canary to 100%

✅ Deployment completado en 45m 00s
```

---

## ⚠️ Validaciones Pre-Deployment

El comando valida automáticamente:

### ✅ Tests
```
❌ No se puede desplegar: tests fallando
   Unit tests: 193/195 (2 failing)

   Arreglar tests antes de deployment:
   dotnet test
```

### ✅ Security
```
❌ No se puede desplegar: vulnerabilidades críticas
   Found 3 CRITICAL vulnerabilities in backend:1.2.4

   Ejecutar:
   trivy image backend:1.2.4
```

### ✅ Prerequisites
```
❌ No se puede desplegar: secrets no configurados
   Missing secrets:
   - DB_PASSWORD
   - JWT_SECRET

   Configurar:
   docker secret create db_password ./db_password.txt
```

### ✅ Resources
```
⚠️ Advertencia: recursos limitados
   Available CPU: 50% (recommended: 80%)

   Continuar de todas formas? (y/N)
```

---

## 🔄 Rollback

Si el deployment falla, el comando proporciona rollback automático (si `--auto-rollback` está activo):

```
❌ Health checks fallando después de deployment

🔄 Ejecutando rollback automático...
   ✅ Load balancer revertido a blue
   ✅ Green environment detenido
   ✅ Health checks OK en blue

✅ Rollback completado en 1m 30s
⚠️ Investigar causa del fallo antes de reintentar
```

**Rollback manual:**
```bash
# Ver versión anterior
docker ps | grep myapp

# Ejecutar rollback
docker-compose -f docker-compose.v1.2.3.yml up -d
```

---

## 📊 Métricas y Reporting

Después de cada deployment, se genera un reporte:

```json
{
  "deployment_id": "dep-20251122-001",
  "environment": "production",
  "version": "1.2.4",
  "strategy": "blue-green",
  "duration": "10m 30s",
  "status": "success",

  "metrics": {
    "tests_run": 263,
    "security_issues": 0,
    "downtime": "0s",
    "rollback": false
  },

  "performance": {
    "p50_latency": "45ms",
    "p95_latency": "120ms",
    "error_rate": "0.01%"
  }
}
```

---

## 🎓 Tips y Best Practices

### ✅ HACER

```bash
# Siempre validar con dry-run primero
/mj2:5-deploy production --dry-run

# Desplegar en staging primero
/mj2:5-deploy staging --version 1.2.4
/mj2:5-deploy production --version 1.2.4

# Usar auto-rollback en producción
/mj2:5-deploy production --auto-rollback
```

### ❌ NO HACER

```bash
# NUNCA skipear tests en producción
/mj2:5-deploy production --skip-tests  # ❌

# NUNCA forzar deployment con errores
/mj2:5-deploy production --force  # ❌

# NUNCA desplegar directo a prod sin staging
# ❌ Probar en staging primero
```

---

## 🔗 Integración con Otros Comandos

```bash
# Workflow completo

# 1. Desarrollar feature
/mj2:2-run API-USERS-001

# 2. Validar calidad
/mj2:quality-check

# 3. Deploy a staging
/mj2:5-deploy staging

# 4. E2E tests en staging
/mj2:4-e2e STAGING-001

# 5. Deploy a production
/mj2:5-deploy production

# 6. Sincronizar documentación
/mj2:3-sync
```

---

## 📚 Ver También

- `/mj2:0-project` - Inicializar proyecto
- `/mj2:quality-check` - Validar calidad antes de deployment
- Agente: `devops-expert` - Detalles del agente de deployment
- Skills: `tools/docker.md`, `tools/docker-compose.md`

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-22
**Mantenido por:** mjcuadrado-net-sdk
