#!/bin/bash
# Hook: metrics-tracker
# Descripción: Registra métricas de ejecución de comandos MJ²
# Evento: post-command
# Output: .mj2/metrics/commands.jsonl (JSON Lines format)

set -e

# Crear directorio de métricas si no existe
METRICS_DIR=".mj2/metrics"
mkdir -p "$METRICS_DIR"

# Archivo de métricas (JSON Lines format)
METRICS_FILE="$METRICS_DIR/commands.jsonl"

# Determinar si el comando fue exitoso
if [ "$MJ2_EXIT_CODE" -eq 0 ]; then
  STATUS="success"
else
  STATUS="failed"
fi

# Registrar métrica en JSON Lines format
cat <<EOF >> "$METRICS_FILE"
{"command":"$MJ2_COMMAND","args":"$MJ2_ARGS","exitCode":$MJ2_EXIT_CODE,"status":"$STATUS","duration":$MJ2_DURATION,"user":"$MJ2_USER","timestamp":"$MJ2_TIMESTAMP"}
EOF

echo "✅ Metrics recorded to $METRICS_FILE"

# Generar reporte si se ejecutan comandos específicos
if [ "$MJ2_COMMAND" == "2-run" ] || [ "$MJ2_COMMAND" == "2f-build" ]; then
  # Contar total de ejecuciones de este comando
  TOTAL_COUNT=$(grep -c "\"command\":\"$MJ2_COMMAND\"" "$METRICS_FILE" || echo "0")

  # Contar éxitos
  SUCCESS_COUNT=$(grep "\"command\":\"$MJ2_COMMAND\"" "$METRICS_FILE" | grep -c "\"status\":\"success\"" || echo "0")

  # Calcular success rate
  if [ "$TOTAL_COUNT" -gt 0 ]; then
    SUCCESS_RATE=$((SUCCESS_COUNT * 100 / TOTAL_COUNT))
    echo "📊 Command: $MJ2_COMMAND"
    echo "   Total executions: $TOTAL_COUNT"
    echo "   Success rate: ${SUCCESS_RATE}%"
  fi

  # Calcular duración promedio (últimos 10)
  AVG_DURATION=$(grep "\"command\":\"$MJ2_COMMAND\"" "$METRICS_FILE" | tail -10 | grep -oP '"duration":\K\d+' | awk '{s+=$1}END{if(NR>0)print int(s/NR)}')
  if [ -n "$AVG_DURATION" ]; then
    echo "   Avg duration (last 10): ${AVG_DURATION}ms"
  fi
fi

# Alertar si duración es anormalmente alta
if [ "$MJ2_DURATION" -gt 120000 ]; then  # > 2 minutos
  echo "⚠️  Warning: Command took longer than expected (${MJ2_DURATION}ms)"
fi

# Generar reporte diario si es el primer comando del día
CURRENT_DATE=$(date +%Y-%m-%d)
LAST_REPORT_FILE="$METRICS_DIR/.last_report"

if [ ! -f "$LAST_REPORT_FILE" ] || [ "$(cat $LAST_REPORT_FILE)" != "$CURRENT_DATE" ]; then
  # Generar reporte del día anterior
  YESTERDAY=$(date -d "yesterday" +%Y-%m-%d 2>/dev/null || date -v-1d +%Y-%m-%d)

  DAILY_REPORT="$METRICS_DIR/daily-report-${YESTERDAY}.txt"

  if grep -q "$YESTERDAY" "$METRICS_FILE" 2>/dev/null; then
    {
      echo "📊 MJ² Daily Report - $YESTERDAY"
      echo "======================================"
      echo ""
      echo "Commands executed:"
      grep "$YESTERDAY" "$METRICS_FILE" | grep -oP '"command":"\K[^"]+' | sort | uniq -c
      echo ""
      echo "Success rate:"
      TOTAL=$(grep -c "$YESTERDAY" "$METRICS_FILE")
      SUCCESS=$(grep "$YESTERDAY" "$METRICS_FILE" | grep -c '"status":"success"')
      echo "  $SUCCESS/$TOTAL ($(($SUCCESS * 100 / $TOTAL))%)"
      echo ""
      echo "Average durations:"
      for cmd in $(grep "$YESTERDAY" "$METRICS_FILE" | grep -oP '"command":"\K[^"]+' | sort -u); do
        AVG=$(grep "$YESTERDAY" "$METRICS_FILE" | grep "\"command\":\"$cmd\"" | grep -oP '"duration":\K\d+' | awk '{s+=$1}END{if(NR>0)print int(s/NR)}')
        echo "  $cmd: ${AVG}ms"
      done
    } > "$DAILY_REPORT"

    echo "📄 Daily report generated: $DAILY_REPORT"
  fi

  # Actualizar último reporte
  echo "$CURRENT_DATE" > "$LAST_REPORT_FILE"
fi

exit 0
