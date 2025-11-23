# Issue #50: Advanced Hooks System

**Fecha:** 2025-11-23
**Actualizado:** 2025-11-23 (Migrado a Python)
**Prioridad:** 🟡 Media
**Estado:** ✅ Completado
**Branch:** `main`
**Versión:** 2.0.0

---

## ⚠️ IMPORTANTE: Migración a Python

**Los hooks fueron re-implementados en Python para compatibilidad cross-platform.**

**Por qué Python:**
- ✅ **Cross-platform:** Funciona en Windows, macOS y Linux
- ✅ **Consistente con moai-adk:** Nuestra referencia base
- ✅ **No más problemas en Windows:** Shell scripts (.sh) no funcionan nativamente en Windows
- ✅ **Más poderoso:** Python es mejor para lógica compleja

**Requisitos:**
- Python 3.8+ (verificar: `python3 --version`)
- Paquetes opcionales: `pip install requests boto3`

---

## 📋 Descripción

Sistema avanzado de hooks para extender MJ² en puntos clave del workflow, permitiendo automatización, integraciones, validaciones customizadas, y observabilidad.

> **NOTA:** Estos hooks son diferentes de los Git hooks. Los MJ² hooks se ejecutan en respuesta a eventos del workflow (comandos, tests, deployments, etc.).

---

## 📦 Entregables

### 1. Hook Templates Python (6 templates)
- **pre_command.py** (70 líneas) - Antes de ejecutar `/mj2:*`
- **post_command.py** (95 líneas) - Después de ejecutar `/mj2:*`
- **on_spec_created.py** (92 líneas) - Al crear una SPEC
- **on_sync_done.py** (65 líneas) - Al completar sync
- **on_test_run.py** (110 líneas) - Al ejecutar tests
- **on_deploy.py** (145 líneas) - Al hacer deployment

### 2. Hook Examples Python (4 ejemplos funcionales)
- **slack_notification.py** (78 líneas) - Notificaciones a Slack
- **spec_backup.py** (83 líneas) - Backup de SPECs a S3 con boto3
- **metrics_tracker.py** (110 líneas) - Tracking de métricas con JSON Lines
- **coverage_reporter.py** (170 líneas) - Monitoreo de coverage con badges

### 3. Documentación
- **README.md** actualizado con ejemplos Python y cross-platform notes
- **config.json** (185+ líneas) - Configuración con Python requirements
- **.github/issues/issue-50.md** - Documentación del issue actualizada

---

## 🎯 Eventos Disponibles

| Evento | Cuándo se ejecuta | Variables disponibles |
|--------|-------------------|----------------------|
| `pre-command` | Antes de `/mj2:*` | `$MJ2_COMMAND`, `$MJ2_ARGS` |
| `post-command` | Después de `/mj2:*` | `$MJ2_COMMAND`, `$MJ2_EXIT_CODE`, `$MJ2_DURATION` |
| `on-spec-created` | Al crear SPEC | `$MJ2_SPEC_ID`, `$MJ2_SPEC_PATH` |
| `on-spec-updated` | Al actualizar SPEC | `$MJ2_SPEC_ID`, `$MJ2_SPEC_PATH` |
| `on-sync-done` | Al completar sync | `$MJ2_SYNC_FILES_COUNT` |
| `on-test-run` | Al ejecutar tests | `$MJ2_TEST_RESULT`, `$MJ2_COVERAGE` |
| `on-deploy` | Al hacer deployment | `$MJ2_DEPLOY_ENV`, `$MJ2_DEPLOY_VERSION` |
| `on-release` | Al crear release | `$MJ2_RELEASE_VERSION`, `$MJ2_RELEASE_TYPE` |

---

## 🎯 Use Cases

### 1. **Notificaciones**
- Slack/Teams cuando se completa deployment
- Email cuando fallan tests
- Actualizar estado en Jira/Linear

### 2. **Metrics Tracking**
- Registrar tiempo de ejecución de comandos
- Trackear coverage trends
- Monitorear performance de builds

### 3. **Auto-Backup**
- Backup automático de SPECs a S3/Azure Blob
- Versionado de documentación
- Sync con Notion/Confluence

### 4. **Integración con Herramientas Externas**
- Actualizar dashboard de métricas
- Trigger workflows en GitHub Actions
- Integrar con herramientas de APM (New Relic, DataDog)

### 5. **Validaciones Customizadas**
- Validar nomenclatura de SPECs
- Verificar compliance antes de deployment
- Auditoría de seguridad automática

---

## 📊 Métricas

- **Archivos creados:** 13 (6 templates + 4 examples + 1 config + 1 README + 1 doc)
- **Líneas totales:** ~950+
- **Eventos soportados:** 8
- **Templates:** 6
- **Ejemplos funcionales:** 4
- **Variables de entorno:** 30+

---

## 🔧 Estructura de Archivos

```
.claude/hooks/
├── README.md                    # Documentación completa
├── config.json                  # Configuración de hooks
├── templates/                   # Templates de hooks
│   ├── pre-command.sh
│   ├── post-command.sh
│   ├── on-spec-created.sh
│   ├── on-sync-done.sh
│   ├── on-test-run.sh
│   └── on-deploy.sh
└── examples/                    # Ejemplos funcionales
    ├── slack-notification.sh
    ├── spec-backup.sh
    ├── metrics-tracker.sh
    └── coverage-reporter.sh
```

---

## 💡 Ejemplos de Uso

### **Ejemplo 1: Notificación a Slack en Deployment**

**Hook:** `slack-notification.sh`

```bash
#!/bin/bash
# Se ejecuta: post-deploy

if [ "$MJ2_DEPLOY_ENV" == "production" ]; then
  curl -X POST $SLACK_WEBHOOK_URL \
    -d "{\"text\":\"🚀 Deployment to PRODUCTION\"}"
fi
```

**Salida:**
```
✅ Slack notification sent successfully
```

### **Ejemplo 2: Backup de SPECs a S3**

**Hook:** `spec-backup.sh`

```bash
#!/bin/bash
# Se ejecuta: on-spec-created

aws s3 cp "$MJ2_SPEC_PATH" "s3://backups/${MJ2_SPEC_ID}.md"
```

**Salida:**
```
✅ SPEC backed up to S3: SPEC-AUTH-001_20251123_153045.md
```

### **Ejemplo 3: Tracking de Métricas**

**Hook:** `metrics-tracker.sh`

```bash
#!/bin/bash
# Se ejecuta: post-command

echo "{\"command\":\"$MJ2_COMMAND\",\"duration\":$MJ2_DURATION}" >> metrics.jsonl
```

**Salida:**
```
✅ Metrics recorded to .mj2/metrics/commands.jsonl
📊 Command: 2-run
   Total executions: 42
   Success rate: 95%
   Avg duration (last 10): 3245ms
```

### **Ejemplo 4: Coverage Reporter**

**Hook:** `coverage-reporter.sh`

```bash
#!/bin/bash
# Se ejecuta: on-test-run

if [ "$MJ2_COVERAGE" -lt 85 ]; then
  echo "⚠️  Coverage below threshold: ${MJ2_COVERAGE}%"
fi
```

**Salida:**
```
✅ Coverage OK: 87%
📊 Coverage trend (last 10 runs): 85,86,84,87,88,86,87,89,87,87
   Average: 87%
→  Coverage is stable at 87%
```

---

## 🔧 Configuración

### **config.json**

```json
{
  "hooks": {
    "enabled": true,
    "timeout": 30000,
    "hooks": [
      {
        "name": "metrics-tracker",
        "event": "post-command",
        "script": ".claude/hooks/examples/metrics-tracker.sh",
        "enabled": true,
        "async": true
      },
      {
        "name": "coverage-reporter",
        "event": "on-test-run",
        "script": ".claude/hooks/examples/coverage-reporter.sh",
        "enabled": true,
        "async": false
      }
    ]
  }
}
```

### **Habilitar un Hook**

1. Editar `.claude/hooks/config.json`
2. Cambiar `"enabled": false` a `"enabled": true"`
3. Configurar variables de entorno si son necesarias

---

## ✅ Criterios de Éxito

- [x] Hook templates creados (6)
- [x] Hook examples creados (4)
- [x] Config.json con ejemplos
- [x] Documentación completa
- [x] 8 eventos soportados
- [x] Variables de entorno documentadas
- [x] Security best practices incluidas

---

## 🔒 Seguridad

### **Best Practices Implementadas**

1. **No hardcodear secrets**
   ```bash
   # ✅ BIEN
   SLACK_WEBHOOK="${SLACK_WEBHOOK_URL}"  # Variable de entorno

   # ❌ MAL
   SLACK_WEBHOOK="https://hooks.slack.com/SECRET"
   ```

2. **Validar inputs**
   ```bash
   if [ -z "$MJ2_SPEC_ID" ]; then
     echo "Error: MJ2_SPEC_ID not set"
     exit 1
   fi
   ```

3. **Usar timeout**
   ```json
   { "timeout": 30000 }  // 30 segundos máximo
   ```

4. **Logging seguro** (no loguear datos sensibles)

---

## 🎓 Crear tu Primer Hook

**Paso 1:** Copiar template
```bash
cp .claude/hooks/templates/post-command.sh .claude/hooks/my-hook.sh
chmod +x .claude/hooks/my-hook.sh
```

**Paso 2:** Editar script
```bash
#!/bin/bash
echo "Command: $MJ2_COMMAND"
echo "Duration: $MJ2_DURATION ms"
```

**Paso 3:** Registrar en config.json
```json
{
  "name": "my-hook",
  "event": "post-command",
  "script": ".claude/hooks/my-hook.sh",
  "enabled": true
}
```

---

## 🚀 Impacto

### **Extensibilidad**
- Usuarios pueden crear hooks personalizados
- Integración con herramientas externas
- Automatización de workflows

### **Observabilidad**
- Tracking de métricas automático
- Histórico de coverage
- Reportes de performance

### **Automatización**
- Backups automáticos
- Notificaciones automáticas
- Validaciones automáticas

---

**Versión:** 1.0.0
**Completado:** 2025-11-23
**Eventos:** 8
**Templates:** 6
**Ejemplos:** 4
