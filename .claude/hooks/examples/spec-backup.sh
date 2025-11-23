#!/bin/bash
# Hook: spec-backup
# Descripción: Backup automático de SPECs a S3
# Evento: on-spec-created, on-spec-updated
# Requires: AWS CLI configurado y S3_BACKUP_BUCKET environment variable

set -e

# Verificar que AWS CLI esté instalado
if ! command -v aws &> /dev/null; then
  echo "⚠️  Warning: AWS CLI not installed. Skipping backup."
  exit 0
fi

# Verificar que S3_BACKUP_BUCKET esté configurado
if [ -z "$S3_BACKUP_BUCKET" ]; then
  echo "⚠️  Warning: S3_BACKUP_BUCKET not set. Skipping backup."
  echo "   Set it to: s3://your-bucket-name/specs/"
  exit 0
fi

# Verificar que el archivo SPEC existe
if [ ! -f "$MJ2_SPEC_PATH" ]; then
  echo "❌ Error: SPEC file not found: $MJ2_SPEC_PATH"
  exit 1
fi

# Crear nombre de backup con timestamp
TIMESTAMP=$(date +%Y%m%d_%H%M%S)
BACKUP_NAME="${MJ2_SPEC_ID}_${TIMESTAMP}.md"
S3_PATH="${S3_BACKUP_BUCKET}${BACKUP_NAME}"

echo "📦 Backing up SPEC to S3..."
echo "   Source: $MJ2_SPEC_PATH"
echo "   Destination: $S3_PATH"

# Realizar backup a S3
if aws s3 cp "$MJ2_SPEC_PATH" "$S3_PATH" --quiet; then
  echo "✅ SPEC backed up successfully to S3"

  # También guardar una copia con el nombre "latest"
  LATEST_NAME="${MJ2_SPEC_ID}_latest.md"
  aws s3 cp "$MJ2_SPEC_PATH" "${S3_BACKUP_BUCKET}${LATEST_NAME}" --quiet

  # Guardar metadata
  METADATA_FILE="/tmp/${MJ2_SPEC_ID}_metadata.json"
  cat <<EOF > "$METADATA_FILE"
{
  "specId": "$MJ2_SPEC_ID",
  "priority": "$MJ2_SPEC_PRIORITY",
  "type": "$MJ2_SPEC_TYPE",
  "timestamp": "$MJ2_TIMESTAMP",
  "backupName": "$BACKUP_NAME"
}
EOF

  aws s3 cp "$METADATA_FILE" "${S3_BACKUP_BUCKET}${MJ2_SPEC_ID}_metadata.json" --quiet
  rm "$METADATA_FILE"

  echo "   Backup: $BACKUP_NAME"
  echo "   Latest: $LATEST_NAME"
  echo "   Metadata: ${MJ2_SPEC_ID}_metadata.json"
else
  echo "❌ Failed to backup SPEC to S3"
  exit 1
fi

exit 0
