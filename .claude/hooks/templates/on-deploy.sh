#!/bin/bash
# Template: on-deploy hook
# Descripción: Se ejecuta cuando se hace un deployment
# Variables disponibles:
#   - $MJ2_DEPLOY_ENV: Environment (development, staging, production)
#   - $MJ2_DEPLOY_VERSION: Versión deployada
#   - $MJ2_DEPLOY_STRATEGY: Estrategia (blue-green, rolling, canary)
#   - $MJ2_DEPLOY_URL: URL del deployment
#   - $MJ2_DEPLOY_DURATION: Duración del deployment en ms
#   - $MJ2_DEPLOY_STATUS: Status (success, failed)
#   - $MJ2_TIMESTAMP: Timestamp ISO 8601

set -e  # Exit on error

# Log evento
echo "[ON-DEPLOY] Environment: $MJ2_DEPLOY_ENV"
echo "[ON-DEPLOY] Version: $MJ2_DEPLOY_VERSION"
echo "[ON-DEPLOY] Strategy: $MJ2_DEPLOY_STRATEGY"
echo "[ON-DEPLOY] URL: $MJ2_DEPLOY_URL"
echo "[ON-DEPLOY] Status: $MJ2_DEPLOY_STATUS"

# ============================================
# TU CÓDIGO AQUÍ
# ============================================

# Ejemplo 1: Notificar a Slack
# SLACK_WEBHOOK="${SLACK_WEBHOOK_URL}"
#
# if [ "$MJ2_DEPLOY_STATUS" == "success" ]; then
#   COLOR="good"
#   EMOJI="🚀"
#   MESSAGE="Deployment successful"
# else
#   COLOR="danger"
#   EMOJI="❌"
#   MESSAGE="Deployment failed"
# fi
#
# curl -X POST "$SLACK_WEBHOOK" \
#   -H 'Content-Type: application/json' \
#   -d "{
#     \"text\": \"$EMOJI $MESSAGE to $MJ2_DEPLOY_ENV\",
#     \"attachments\": [{
#       \"color\": \"$COLOR\",
#       \"fields\": [
#         {\"title\": \"Version\", \"value\": \"$MJ2_DEPLOY_VERSION\", \"short\": true},
#         {\"title\": \"Environment\", \"value\": \"$MJ2_DEPLOY_ENV\", \"short\": true},
#         {\"title\": \"Strategy\", \"value\": \"$MJ2_DEPLOY_STRATEGY\", \"short\": true},
#         {\"title\": \"URL\", \"value\": \"$MJ2_DEPLOY_URL\", \"short\": false}
#       ]
#     }]
#   }"

# Ejemplo 2: Registrar deployment en APM (New Relic, DataDog, etc.)
# if [ "$MJ2_DEPLOY_STATUS" == "success" ]; then
#   curl -X POST "https://api.newrelic.com/v2/applications/${APP_ID}/deployments.json" \
#     -H "X-Api-Key: ${NEW_RELIC_API_KEY}" \
#     -H "Content-Type: application/json" \
#     -d "{
#       \"deployment\": {
#         \"revision\": \"$MJ2_DEPLOY_VERSION\",
#         \"description\": \"Deployed to $MJ2_DEPLOY_ENV\",
#         \"user\": \"$USER\"
#       }
#     }"
# fi

# Ejemplo 3: Ejecutar smoke tests post-deployment
# if [ "$MJ2_DEPLOY_ENV" == "production" ] && [ "$MJ2_DEPLOY_STATUS" == "success" ]; then
#   echo "Running smoke tests against $MJ2_DEPLOY_URL"
#
#   # Health check
#   response=$(curl -s -o /dev/null -w "%{http_code}" "${MJ2_DEPLOY_URL}/health")
#   if [ "$response" != "200" ]; then
#     echo "❌ Smoke test failed: Health check returned $response"
#     # Trigger rollback
#     # /mj2:5-deploy --rollback
#   else
#     echo "✅ Smoke tests passed"
#   fi
# fi

# Ejemplo 4: Crear tag en GitHub
# if [ "$MJ2_DEPLOY_ENV" == "production" ] && [ "$MJ2_DEPLOY_STATUS" == "success" ]; then
#   git tag -a "deploy-${MJ2_DEPLOY_VERSION}" -m "Deployed v${MJ2_DEPLOY_VERSION} to production"
#   git push origin "deploy-${MJ2_DEPLOY_VERSION}"
# fi

# Ejemplo 5: Actualizar status en Jira
# jira_issue=$(git log -1 --pretty=%B | grep -oP '(?<=JIRA-)\d+' || echo "")
# if [ -n "$jira_issue" ]; then
#   curl -X POST "https://your-domain.atlassian.net/rest/api/3/issue/JIRA-${jira_issue}/transitions" \
#     -H "Authorization: Basic ${JIRA_TOKEN}" \
#     -H "Content-Type: application/json" \
#     -d '{"transition":{"id":"31"}}'  # ID 31 = "Deployed"
# fi

# ============================================

exit 0
