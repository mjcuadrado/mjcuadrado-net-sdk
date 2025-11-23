#!/bin/bash
# Hook: slack-notification
# Descripción: Envía notificación a Slack cuando se hace deployment a production
# Evento: post-deploy
# Requires: SLACK_WEBHOOK_URL environment variable

set -e

# Verificar que SLACK_WEBHOOK_URL esté configurado
if [ -z "$SLACK_WEBHOOK_URL" ]; then
  echo "⚠️  Warning: SLACK_WEBHOOK_URL not set. Skipping Slack notification."
  exit 0
fi

# Solo notificar deployments a staging y production
if [ "$MJ2_DEPLOY_ENV" != "staging" ] && [ "$MJ2_DEPLOY_ENV" != "production" ]; then
  echo "ℹ️  Skipping Slack notification for $MJ2_DEPLOY_ENV environment"
  exit 0
fi

# Determinar color y emoji según status
if [ "$MJ2_DEPLOY_STATUS" == "success" ]; then
  COLOR="good"
  EMOJI="🚀"
  MESSAGE="Deployment successful"
else
  COLOR="danger"
  EMOJI="❌"
  MESSAGE="Deployment failed"
fi

# Construir payload JSON
PAYLOAD=$(cat <<EOF
{
  "text": "$EMOJI $MESSAGE to $MJ2_DEPLOY_ENV",
  "attachments": [
    {
      "color": "$COLOR",
      "fields": [
        {
          "title": "Version",
          "value": "$MJ2_DEPLOY_VERSION",
          "short": true
        },
        {
          "title": "Environment",
          "value": "$MJ2_DEPLOY_ENV",
          "short": true
        },
        {
          "title": "Strategy",
          "value": "$MJ2_DEPLOY_STRATEGY",
          "short": true
        },
        {
          "title": "Duration",
          "value": "${MJ2_DEPLOY_DURATION}ms",
          "short": true
        },
        {
          "title": "URL",
          "value": "$MJ2_DEPLOY_URL",
          "short": false
        }
      ],
      "footer": "MJ² Deploy System",
      "ts": $(date +%s)
    }
  ]
}
EOF
)

# Enviar notificación a Slack
response=$(curl -s -o /dev/null -w "%{http_code}" -X POST "$SLACK_WEBHOOK_URL" \
  -H 'Content-Type: application/json' \
  -d "$PAYLOAD")

if [ "$response" == "200" ]; then
  echo "✅ Slack notification sent successfully"
else
  echo "❌ Failed to send Slack notification (HTTP $response)"
  exit 1
fi

exit 0
