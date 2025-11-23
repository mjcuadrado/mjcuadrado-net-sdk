#!/bin/bash
# Template: on-spec-created hook
# Descripción: Se ejecuta cuando se crea una nueva SPEC
# Variables disponibles:
#   - $MJ2_SPEC_ID: ID de la SPEC (ej: "SPEC-AUTH-001")
#   - $MJ2_SPEC_PATH: Path del archivo SPEC
#   - $MJ2_SPEC_PRIORITY: Prioridad (high, medium, low)
#   - $MJ2_SPEC_TYPE: Tipo (feature, bug, refactor, docs)
#   - $MJ2_TIMESTAMP: Timestamp ISO 8601

set -e  # Exit on error

# Log evento
echo "[ON-SPEC-CREATED] SPEC ID: $MJ2_SPEC_ID"
echo "[ON-SPEC-CREATED] Path: $MJ2_SPEC_PATH"
echo "[ON-SPEC-CREATED] Priority: $MJ2_SPEC_PRIORITY"
echo "[ON-SPEC-CREATED] Type: $MJ2_SPEC_TYPE"

# ============================================
# TU CÓDIGO AQUÍ
# ============================================

# Ejemplo 1: Backup automático a S3/Azure
# BACKUP_DIR="s3://my-bucket/specs/backups/"
# TIMESTAMP=$(date +%Y%m%d_%H%M%S)
# BACKUP_NAME="${MJ2_SPEC_ID}_${TIMESTAMP}.md"
#
# aws s3 cp "$MJ2_SPEC_PATH" "${BACKUP_DIR}${BACKUP_NAME}"
# echo "✅ SPEC backed up to: ${BACKUP_DIR}${BACKUP_NAME}"

# Ejemplo 2: Crear issue en Jira/Linear
# if [ "$MJ2_SPEC_PRIORITY" == "high" ]; then
#   curl -X POST "https://api.linear.app/graphql" \
#     -H "Authorization: Bearer $LINEAR_API_KEY" \
#     -d '{"query":"mutation { issueCreate(input: {title: \"'$MJ2_SPEC_ID'\", priority: 1}) { success } }"}'
# fi

# Ejemplo 3: Validar formato SPEC
# if ! grep -q "## EARS Format" "$MJ2_SPEC_PATH"; then
#   echo "⚠️  Warning: SPEC missing EARS format section"
# fi

# Ejemplo 4: Notificar al equipo
# SLACK_WEBHOOK="${SLACK_WEBHOOK_URL}"
# curl -X POST "$SLACK_WEBHOOK" \
#   -H 'Content-Type: application/json' \
#   -d "{
#     \"text\": \"📋 New SPEC created: $MJ2_SPEC_ID\",
#     \"attachments\": [{
#       \"color\": \"good\",
#       \"fields\": [
#         {\"title\": \"Priority\", \"value\": \"$MJ2_SPEC_PRIORITY\", \"short\": true},
#         {\"title\": \"Type\", \"value\": \"$MJ2_SPEC_TYPE\", \"short\": true}
#       ]
#     }]
#   }"

# ============================================

exit 0
