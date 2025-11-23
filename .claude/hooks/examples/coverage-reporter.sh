#!/bin/bash
# Hook: coverage-reporter
# Descripción: Monitorea coverage y envía alertas si cae por debajo del threshold
# Evento: on-test-run
# Output: .mj2/metrics/coverage-history.jsonl

set -e

# Configuración
COVERAGE_THRESHOLD=${COVERAGE_THRESHOLD:-85}  # Default: 85%
METRICS_DIR=".mj2/metrics"
mkdir -p "$METRICS_DIR"

COVERAGE_FILE="$METRICS_DIR/coverage-history.jsonl"

# Log resultado de tests
echo "[COVERAGE REPORTER] Tests: $MJ2_TEST_RESULT"
echo "[COVERAGE REPORTER] Total: $MJ2_TEST_TOTAL (Passed: $MJ2_TEST_PASSED, Failed: $MJ2_TEST_FAILED)"
echo "[COVERAGE REPORTER] Coverage: $MJ2_COVERAGE%"
echo "[COVERAGE REPORTER] Threshold: $COVERAGE_THRESHOLD%"

# Registrar histórico de coverage
cat <<EOF >> "$COVERAGE_FILE"
{"coverage":$MJ2_COVERAGE,"lines":$MJ2_COVERAGE_LINES,"branches":$MJ2_COVERAGE_BRANCHES,"tests":{"total":$MJ2_TEST_TOTAL,"passed":$MJ2_TEST_PASSED,"failed":$MJ2_TEST_FAILED},"result":"$MJ2_TEST_RESULT","timestamp":"$MJ2_TIMESTAMP"}
EOF

# Validar coverage threshold
if [ "$MJ2_COVERAGE" -lt "$COVERAGE_THRESHOLD" ]; then
  echo "❌ Coverage below threshold: ${MJ2_COVERAGE}% (required: ${COVERAGE_THRESHOLD}%)"

  # Calcular diferencia
  DIFF=$((COVERAGE_THRESHOLD - MJ2_COVERAGE))
  echo "   Missing: ${DIFF}% to reach threshold"

  # Enviar alerta (ejemplo con curl - adaptar a tu sistema)
  if [ -n "$COVERAGE_ALERT_WEBHOOK" ]; then
    curl -X POST "$COVERAGE_ALERT_WEBHOOK" \
      -H 'Content-Type: application/json' \
      -d "{
        \"type\": \"coverage_low\",
        \"coverage\": $MJ2_COVERAGE,
        \"threshold\": $COVERAGE_THRESHOLD,
        \"diff\": $DIFF,
        \"timestamp\": \"$MJ2_TIMESTAMP\"
      }" \
      --silent --output /dev/null || echo "⚠️  Failed to send alert"
  fi

  # Bloquear PR si coverage cae (opcional - descomentar para habilitar)
  # if command -v gh &> /dev/null; then
  #   gh pr edit --add-label "coverage-below-threshold" 2>/dev/null || true
  # fi

  exit 1  # Exit 1 para indicar que coverage está bajo
else
  echo "✅ Coverage OK: ${MJ2_COVERAGE}%"

  # Si coverage mejoró, remover label (opcional)
  # if command -v gh &> /dev/null; then
  #   gh pr edit --remove-label "coverage-below-threshold" 2>/dev/null || true
  # fi
fi

# Calcular tendencia de coverage (últimos 10 runs)
if [ -f "$COVERAGE_FILE" ]; then
  RECENT_COVERAGE=$(tail -10 "$COVERAGE_FILE" | grep -oP '"coverage":\K\d+' | tr '\n' ',' | sed 's/,$//')

  if [ -n "$RECENT_COVERAGE" ]; then
    echo "📊 Coverage trend (last 10 runs): $RECENT_COVERAGE"

    # Calcular promedio
    AVG_COVERAGE=$(echo "$RECENT_COVERAGE" | awk -F',' '{s=0;for(i=1;i<=NF;i++)s+=$i;print int(s/NF)}')
    echo "   Average: ${AVG_COVERAGE}%"

    # Detectar si está bajando
    FIRST=$(echo "$RECENT_COVERAGE" | cut -d',' -f1)
    LAST=$MJ2_COVERAGE

    if [ "$LAST" -lt "$FIRST" ]; then
      TREND_DIFF=$((FIRST - LAST))
      echo "⚠️  Coverage is decreasing: -${TREND_DIFF}% (from $FIRST% to $LAST%)"
    elif [ "$LAST" -gt "$FIRST" ]; then
      TREND_DIFF=$((LAST - FIRST))
      echo "✅ Coverage is increasing: +${TREND_DIFF}% (from $FIRST% to $LAST%)"
    else
      echo "→  Coverage is stable at ${LAST}%"
    fi
  fi
fi

# Generar badge de coverage (shields.io)
BADGE_DIR=".github/badges"
mkdir -p "$BADGE_DIR"

# Determinar color del badge
if [ "$MJ2_COVERAGE" -ge 90 ]; then
  BADGE_COLOR="brightgreen"
elif [ "$MJ2_COVERAGE" -ge 80 ]; then
  BADGE_COLOR="green"
elif [ "$MJ2_COVERAGE" -ge 70 ]; then
  BADGE_COLOR="yellow"
elif [ "$MJ2_COVERAGE" -ge 60 ]; then
  BADGE_COLOR="orange"
else
  BADGE_COLOR="red"
fi

# Descargar badge
curl -s -o "$BADGE_DIR/coverage.svg" \
  "https://img.shields.io/badge/coverage-${MJ2_COVERAGE}%25-${BADGE_COLOR}" || true

echo "🏷️  Coverage badge updated: $BADGE_DIR/coverage.svg"

# Generar reporte detallado si coverage es muy bajo
if [ "$MJ2_COVERAGE" -lt 70 ]; then
  REPORT_FILE="$METRICS_DIR/low-coverage-report-$(date +%Y%m%d_%H%M%S).txt"

  {
    echo "⚠️  LOW COVERAGE ALERT"
    echo "======================"
    echo ""
    echo "Current Coverage: $MJ2_COVERAGE%"
    echo "Threshold: $COVERAGE_THRESHOLD%"
    echo "Difference: -$((COVERAGE_THRESHOLD - MJ2_COVERAGE))%"
    echo ""
    echo "Coverage Breakdown:"
    echo "  Lines: $MJ2_COVERAGE_LINES%"
    echo "  Branches: $MJ2_COVERAGE_BRANCHES%"
    echo ""
    echo "Test Results:"
    echo "  Total: $MJ2_TEST_TOTAL"
    echo "  Passed: $MJ2_TEST_PASSED"
    echo "  Failed: $MJ2_TEST_FAILED"
    echo ""
    echo "Timestamp: $MJ2_TIMESTAMP"
    echo ""
    echo "Action Required:"
    echo "- Add unit tests for uncovered code paths"
    echo "- Aim for $COVERAGE_THRESHOLD% coverage minimum"
  } > "$REPORT_FILE"

  echo "📄 Low coverage report generated: $REPORT_FILE"
fi

exit 0
