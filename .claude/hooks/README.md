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
├── templates/               # Templates de hooks (Python)
│   ├── pre_command.py
│   ├── post_command.py
│   ├── on_spec_created.py
│   ├── on_sync_done.py
│   ├── on_test_run.py
│   └── on_deploy.py
└── examples/                # Ejemplos de hooks (Python)
    ├── slack_notification.py
    ├── metrics_tracker.py
    ├── spec_backup.py
    └── coverage_reporter.py
```

### 🐍 ¿Por qué Python?

**Los hooks están escritos en Python (no shell scripts) por:**

1. **✅ Cross-platform:** Funciona en Windows, macOS y Linux
2. **✅ Consistente con moai-adk:** Nuestra referencia base
3. **✅ Más poderoso:** Mejor para lógica compleja
4. **✅ Común en DevOps:** Python es estándar en automatización

**Requisitos:**
- Python 3.8+ (verificar con `python3 --version`)
- Paquetes opcionales: `pip install requests boto3`

### 🔧 Configuración de MJ² Hooks

**config.json:**
```json
{
  "python": {
    "required": true,
    "minVersion": "3.8"
  },
  "hooks": {
    "enabled": true,
    "timeout": 30000,
    "hooks": [
      {
        "name": "slack-notification",
        "event": "post-deploy",
        "script": ".claude/hooks/examples/slack_notification.py",
        "enabled": true,
        "async": true,
        "dependencies": ["requests"]
      },
      {
        "name": "spec-backup",
        "event": "on-spec-created",
        "script": ".claude/hooks/examples/spec_backup.py",
        "enabled": true,
        "async": false,
        "dependencies": ["boto3"]
      }
    ]
  }
}
```

### 💡 Ejemplos de MJ² Hooks

#### **1. Notificación a Slack en Deployment**
```python
#!/usr/bin/env python3
# .claude/hooks/examples/slack_notification.py

import os
import requests

webhook_url = os.getenv('SLACK_WEBHOOK_URL')
env = os.getenv('MJ2_DEPLOY_ENV')
version = os.getenv('MJ2_RELEASE_VERSION')

if env == 'production' and webhook_url:
    payload = {
        'text': '🚀 Deployment to PRODUCTION',
        'attachments': [{
            'color': 'good',
            'fields': [
                {'title': 'Version', 'value': version},
                {'title': 'Environment', 'value': env}
            ]
        }]
    }
    requests.post(webhook_url, json=payload)
```

#### **2. Backup de SPECs a S3**
```python
#!/usr/bin/env python3
# .claude/hooks/examples/spec_backup.py

import os
import boto3
from datetime import datetime
from pathlib import Path

spec_path = os.getenv('MJ2_SPEC_PATH')
spec_id = os.getenv('MJ2_SPEC_ID')
s3_bucket = os.getenv('S3_BACKUP_BUCKET', 's3://my-backups/specs/')

if Path(spec_path).exists():
    timestamp = datetime.now().strftime('%Y%m%d_%H%M%S')
    backup_name = f"{spec_id}_{timestamp}.md"

    s3 = boto3.client('s3')
    bucket = s3_bucket.replace('s3://', '').split('/')[0]

    with open(spec_path, 'rb') as f:
        s3.upload_fileobj(f, bucket, f'backups/{backup_name}')

    print(f"✅ SPEC backed up to S3: {backup_name}")
```

#### **3. Tracking de Métricas**
```python
#!/usr/bin/env python3
# .claude/hooks/examples/metrics_tracker.py

import os
import json
from datetime import datetime
from pathlib import Path

metrics_dir = Path('.mj2/metrics')
metrics_dir.mkdir(parents=True, exist_ok=True)

metric = {
    'command': os.getenv('MJ2_COMMAND'),
    'exitCode': int(os.getenv('MJ2_EXIT_CODE', '0')),
    'duration': int(os.getenv('MJ2_DURATION', '0')),
    'timestamp': datetime.utcnow().isoformat()
}

with open(metrics_dir / 'commands.jsonl', 'a') as f:
    f.write(json.dumps(metric) + '\n')
```

#### **4. Coverage Reporter**
```python
#!/usr/bin/env python3
# .claude/hooks/examples/coverage_reporter.py

import os
import sys

coverage = int(os.getenv('MJ2_COVERAGE', '0'))
threshold = 85

if coverage < threshold:
    print(f"⚠️  Coverage below threshold: {coverage}% (required: {threshold}%)")

    # Send alert
    import requests
    webhook = os.getenv('COVERAGE_ALERT_WEBHOOK')
    if webhook:
        requests.post(webhook, json={'coverage': coverage, 'threshold': threshold})

    sys.exit(1)
else:
    print(f"✅ Coverage OK: {coverage}%")
```

### 🔒 Seguridad en MJ² Hooks

1. **No hardcodear secrets** - Usar variables de entorno
2. **Validar inputs** - Verificar que variables existen
3. **Usar timeout** - Configurar timeout apropiado
4. **Logging seguro** - No loguear datos sensibles

### 🎓 Crear tu Primer MJ² Hook

**Paso 1:** Copiar template
```bash
cp .claude/hooks/templates/post_command.py .claude/hooks/my_hook.py
chmod +x .claude/hooks/my_hook.py
```

**Paso 2:** Editar script
```python
#!/usr/bin/env python3
# my_hook.py

import os

command = os.getenv('MJ2_COMMAND')
exit_code = os.getenv('MJ2_EXIT_CODE')
duration = os.getenv('MJ2_DURATION')

print(f"Command: {command}")
print(f"Exit Code: {exit_code}")
print(f"Duration: {duration}ms")
```

**Paso 3:** Registrar en config.json
```json
{
  "hooks": {
    "hooks": [
      {
        "name": "my-hook",
        "event": "post-command",
        "script": ".claude/hooks/my_hook.py",
        "enabled": true
      }
    ]
  }
}
```

**Paso 4:** Probar el hook
```bash
python3 .claude/hooks/my_hook.py
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
