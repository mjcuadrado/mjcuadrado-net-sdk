#!/bin/bash
# Template: post-command hook
# Descripción: Se ejecuta DESPUÉS de ejecutar un comando /mj2:*
# Variables disponibles:
#   - $MJ2_COMMAND: Comando ejecutado (ej: "1-plan", "2-run")
#   - $MJ2_ARGS: Argumentos del comando
#   - $MJ2_EXIT_CODE: Código de salida del comando (0 = success)
#   - $MJ2_DURATION: Duración de ejecución en ms
#   - $MJ2_USER: Usuario que ejecutó el comando
#   - $MJ2_TIMESTAMP: Timestamp ISO 8601

set -e  # Exit on error

# Log resultado
echo "[POST-COMMAND] Command: $MJ2_COMMAND"
echo "[POST-COMMAND] Exit Code: $MJ2_EXIT_CODE"
echo "[POST-COMMAND] Duration: ${MJ2_DURATION}ms"
echo "[POST-COMMAND] Timestamp: $MJ2_TIMESTAMP"

# ============================================
# TU CÓDIGO AQUÍ
# ============================================

# Ejemplo 1: Registrar métricas
# METRICS_DIR=".mj2/metrics"
# mkdir -p "$METRICS_DIR"
#
# cat <<EOF >> "$METRICS_DIR/commands.jsonl"
# {"command":"$MJ2_COMMAND","exitCode":$MJ2_EXIT_CODE,"duration":$MJ2_DURATION,"timestamp":"$MJ2_TIMESTAMP"}
# EOF

# Ejemplo 2: Notificar si falla
# if [ "$MJ2_EXIT_CODE" -ne 0 ]; then
#   echo "❌ Command failed: $MJ2_COMMAND"
#
#   # Enviar notificación (Slack, email, etc.)
#   # curl -X POST "${SLACK_WEBHOOK}" -d "{'text':'Command failed: $MJ2_COMMAND'}"
# fi

# Ejemplo 3: Auto-commit después de /mj2:2-run exitoso
# if [ "$MJ2_COMMAND" == "2-run" ] && [ "$MJ2_EXIT_CODE" -eq 0 ]; then
#   git add .
#   git commit -m "feat: Implemented $MJ2_ARGS" --no-verify
# fi

# Ejemplo 4: Generar reporte de performance
# if [ "$MJ2_DURATION" -gt 60000 ]; then  # > 1 minuto
#   echo "⚠️  Command took longer than expected: ${MJ2_DURATION}ms"
# fi

# ============================================
# IMPORTANTE: Exit codes
# ============================================
# En post-command, el exit code NO bloquea nada
# pero se puede usar para logging/alerting
# ============================================

exit 0
