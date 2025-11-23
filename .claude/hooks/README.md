# Claude Hooks & MJ² Advanced Hooks System

Esta carpeta contiene hooks que se ejecutan en respuesta a eventos de Claude Code y del sistema MJ².

## ¿Qué son los hooks?

Los hooks son scripts que se ejecutan automáticamente cuando ocurren ciertos eventos durante el desarrollo con Claude Code y MJ².

> **Tipos de Hooks:**
> 1. **Claude Code Hooks** - Hooks nativos de Claude Code (pre-tool, post-tool, user-prompt-submit)
> 2. **MJ² Hooks** - Hooks avanzados del sistema MJ² (pre-command, post-command, on-spec-created, etc.)

## Tipos de hooks

### Pre-tool hooks
Se ejecutan ANTES de que Claude use una herramienta.

Ejemplo: `pre-write.sh`
```bash
#!/bin/bash
# Valida que el archivo no esté bloqueado antes de escribir
```

### Post-tool hooks
Se ejecutan DESPUÉS de que Claude use una herramienta.

Ejemplo: `post-write.sh`
```bash
#!/bin/bash
# Ejecuta formateo automático después de escribir
dotnet format
```

### User-prompt-submit hooks
Se ejecutan cuando el usuario envía un prompt.

Ejemplo: `user-prompt-submit.sh`
```bash
#!/bin/bash
# Verifica que no haya cambios sin commitear
git status --porcelain
```

## Configuración de hooks

Los hooks se configuran en el archivo de settings de Claude Code o como scripts ejecutables en `.claude/hooks/`.

### Formato de script

```bash
#!/bin/bash
# Nombre: nombre-hook
# Descripción: Qué hace este hook
# Evento: pre-write|post-write|user-prompt-submit

# Tu código aquí
exit 0  # 0 = success, 1 = bloquea la operación
```

## Ejemplos de hooks útiles

### Auto-format on write
```bash
#!/bin/bash
# .claude/hooks/post-write-format.sh
dotnet format "$1"  # $1 es el archivo modificado
```

### Prevent commits without tests
```bash
#!/bin/bash
# .claude/hooks/pre-commit.sh
dotnet test || exit 1
```

### Validate SPECs before save
```bash
#!/bin/bash
# .claude/hooks/pre-write-spec.sh
if [[ "$1" == *.spec.md ]]; then
    mjcuadrado-net-sdk spec validate "$1" || exit 1
fi
```

---

## 🚀 MJ² Advanced Hooks System

Sistema avanzado de hooks específicos para el workflow MJ² (SPEC → TEST → CODE → DOC).

### 📋 ¿Por qué MJ² Hooks?

Los MJ² hooks permiten extender el sistema en puntos clave del workflow de desarrollo:
- **Automatización** de tareas repetitivas
- **Integraciones** con herramientas externas (Slack, Jira, S3, etc.)
- **Validaciones** customizadas
- **Metrics tracking** y observabilidad
- **Notificaciones** automáticas

### 🎯 Eventos Disponibles

| Evento | Cuándo se ejecuta | Variables disponibles |
|--------|-------------------|----------------------|
| `pre-command` | Antes de ejecutar `/mj2:*` | `$MJ2_COMMAND`, `$MJ2_ARGS` |
| `post-command` | Después de ejecutar `/mj2:*` | `$MJ2_COMMAND`, `$MJ2_EXIT_CODE`, `$MJ2_DURATION` |
| `on-spec-created` | Al crear una SPEC | `$MJ2_SPEC_ID`, `$MJ2_SPEC_PATH` |
| `on-spec-updated` | Al actualizar una SPEC | `$MJ2_SPEC_ID`, `$MJ2_SPEC_PATH` |
| `on-sync-done` | Al completar `/mj2:3-sync` | `$MJ2_SYNC_FILES_COUNT` |
| `on-test-run` | Al ejecutar tests | `$MJ2_TEST_RESULT`, `$MJ2_COVERAGE` |
| `on-deploy` | Al hacer deployment | `$MJ2_DEPLOY_ENV`, `$MJ2_DEPLOY_VERSION` |
| `on-release` | Al crear un release | `$MJ2_RELEASE_VERSION`, `$MJ2_RELEASE_TYPE` |

### 📂 Estructura de MJ² Hooks

```
.claude/hooks/
├── config.json              # Configuración de hooks MJ²
├── templates/               # Templates de hooks
│   ├── pre-command.sh
│   ├── post-command.sh
│   ├── on-spec-created.sh
│   ├── on-sync-done.sh
│   ├── on-test-run.sh
│   └── on-deploy.sh
└── examples/                # Ejemplos de hooks
    ├── slack-notification.sh
    ├── metrics-tracker.sh
    ├── spec-backup.sh
    └── coverage-reporter.sh
```

### 🔧 Configuración de MJ² Hooks

**config.json:**
```json
{
  "hooks": {
    "enabled": true,
    "timeout": 30000,
    "hooks": [
      {
        "name": "slack-notification",
        "event": "post-deploy",
        "script": ".claude/hooks/examples/slack-notification.sh",
        "enabled": true,
        "async": true
      },
      {
        "name": "spec-backup",
        "event": "on-spec-created",
        "script": ".claude/hooks/examples/spec-backup.sh",
        "enabled": true,
        "async": false
      }
    ]
  }
}
```

### 💡 Ejemplos de MJ² Hooks

#### **1. Notificación a Slack en Deployment**
```bash
#!/bin/bash
# .claude/hooks/examples/slack-notification.sh

SLACK_WEBHOOK="${SLACK_WEBHOOK_URL}"  # Variable de entorno

if [ "$MJ2_DEPLOY_ENV" == "production" ]; then
  curl -X POST $SLACK_WEBHOOK \
    -H 'Content-Type: application/json' \
    -d "{
      \"text\": \"🚀 Deployment to PRODUCTION\",
      \"attachments\": [{
        \"color\": \"good\",
        \"fields\": [
          {\"title\": \"Version\", \"value\": \"$MJ2_RELEASE_VERSION\"},
          {\"title\": \"Environment\", \"value\": \"$MJ2_DEPLOY_ENV\"}
        ]
      }]
    }"
fi
```

#### **2. Backup de SPECs a S3**
```bash
#!/bin/bash
# .claude/hooks/examples/spec-backup.sh

SPEC_PATH="$MJ2_SPEC_PATH"
S3_BUCKET="s3://my-backups/specs/"

if [ -f "$SPEC_PATH" ]; then
  TIMESTAMP=$(date +%Y%m%d_%H%M%S)
  BACKUP_NAME="${MJ2_SPEC_ID}_${TIMESTAMP}.md"

  aws s3 cp "$SPEC_PATH" "${S3_BUCKET}${BACKUP_NAME}"
  echo "✅ SPEC backed up to S3: ${BACKUP_NAME}"
fi
```

#### **3. Tracking de Métricas**
```bash
#!/bin/bash
# .claude/hooks/examples/metrics-tracker.sh

METRICS_FILE=".mj2/metrics/commands.json"
mkdir -p .mj2/metrics

cat <<EOF >> $METRICS_FILE
{
  "command": "$MJ2_COMMAND",
  "exitCode": $MJ2_EXIT_CODE,
  "duration": $MJ2_DURATION,
  "timestamp": "$(date -u +%Y-%m-%dT%H:%M:%SZ)"
}
EOF
```

#### **4. Coverage Reporter**
```bash
#!/bin/bash
# .claude/hooks/examples/coverage-reporter.sh

if [ "$MJ2_COVERAGE" -lt 85 ]; then
  echo "⚠️  Coverage below threshold: ${MJ2_COVERAGE}% (required: 85%)"

  # Enviar alerta
  curl -X POST "https://api.example.com/alerts" \
    -d "coverage=${MJ2_COVERAGE}&threshold=85"
else
  echo "✅ Coverage OK: ${MJ2_COVERAGE}%"
fi
```

### 🔒 Seguridad en MJ² Hooks

1. **No hardcodear secrets** - Usar variables de entorno
2. **Validar inputs** - Verificar que variables existen
3. **Usar timeout** - Configurar timeout apropiado
4. **Logging seguro** - No loguear datos sensibles

### 🎓 Crear tu Primer MJ² Hook

**Paso 1:** Copiar template
```bash
cp .claude/hooks/templates/post-command.sh .claude/hooks/my-hook.sh
chmod +x .claude/hooks/my-hook.sh
```

**Paso 2:** Editar script
```bash
#!/bin/bash
# my-hook.sh

echo "Command: $MJ2_COMMAND"
echo "Exit Code: $MJ2_EXIT_CODE"
echo "Duration: $MJ2_DURATION ms"
```

**Paso 3:** Registrar en config.json
```json
{
  "hooks": {
    "hooks": [
      {
        "name": "my-hook",
        "event": "post-command",
        "script": ".claude/hooks/my-hook.sh",
        "enabled": true
      }
    ]
  }
}
```

---

## Próximos pasos

En futuras fases, el SDK incluirá:
```bash
# Listar hooks disponibles
mjcuadrado-net-sdk hook list

# Crear nuevo hook
mjcuadrado-net-sdk hook new mi-hook

# Habilitar/deshabilitar hook
mjcuadrado-net-sdk hook enable pre-write-format
mjcuadrado-net-sdk hook disable pre-write-format
```

## Documentación

- **Claude Code Hooks:** https://code.claude.com/docs
- **MJ² Hooks:** `.claude/hooks/templates/` y `.claude/hooks/examples/`
- **Issue #50:** `.github/issues/issue-50.md`
