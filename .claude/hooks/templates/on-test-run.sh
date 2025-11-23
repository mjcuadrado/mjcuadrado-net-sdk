#!/bin/bash
# Template: on-test-run hook
# Descripción: Se ejecuta cuando se ejecutan tests
# Variables disponibles:
#   - $MJ2_TEST_RESULT: Resultado (passed, failed)
#   - $MJ2_TEST_TOTAL: Total de tests ejecutados
#   - $MJ2_TEST_PASSED: Tests que pasaron
#   - $MJ2_TEST_FAILED: Tests que fallaron
#   - $MJ2_COVERAGE: Coverage % (ej: 87)
#   - $MJ2_COVERAGE_LINES: Coverage de líneas %
#   - $MJ2_COVERAGE_BRANCHES: Coverage de branches %
#   - $MJ2_TEST_DURATION: Duración de tests en ms
#   - $MJ2_TIMESTAMP: Timestamp ISO 8601

set -e  # Exit on error

# Log evento
echo "[ON-TEST-RUN] Result: $MJ2_TEST_RESULT"
echo "[ON-TEST-RUN] Total: $MJ2_TEST_TOTAL (Passed: $MJ2_TEST_PASSED, Failed: $MJ2_TEST_FAILED)"
echo "[ON-TEST-RUN] Coverage: $MJ2_COVERAGE%"
echo "[ON-TEST-RUN] Duration: ${MJ2_TEST_DURATION}ms"

# ============================================
# TU CÓDIGO AQUÍ
# ============================================

# Ejemplo 1: Alertar si coverage baja del threshold
# COVERAGE_THRESHOLD=85
# if [ "$MJ2_COVERAGE" -lt "$COVERAGE_THRESHOLD" ]; then
#   echo "⚠️  Coverage below threshold: ${MJ2_COVERAGE}% (required: ${COVERAGE_THRESHOLD}%)"
#
#   # Enviar alerta
#   curl -X POST "https://api.example.com/alerts" \
#     -H 'Content-Type: application/json' \
#     -d "{
#       \"type\": \"coverage_low\",
#       \"coverage\": $MJ2_COVERAGE,
#       \"threshold\": $COVERAGE_THRESHOLD
#     }"
# else
#   echo "✅ Coverage OK: ${MJ2_COVERAGE}%"
# fi

# Ejemplo 2: Registrar histórico de coverage
# METRICS_FILE=".mj2/metrics/coverage-history.jsonl"
# mkdir -p .mj2/metrics
#
# cat <<EOF >> "$METRICS_FILE"
# {"coverage":$MJ2_COVERAGE,"lines":$MJ2_COVERAGE_LINES,"branches":$MJ2_COVERAGE_BRANCHES,"timestamp":"$MJ2_TIMESTAMP"}
# EOF

# Ejemplo 3: Bloquear merge si tests fallan
# if [ "$MJ2_TEST_RESULT" == "failed" ]; then
#   echo "❌ Tests failed - blocking merge"
#   gh pr edit --add-label "tests-failing"
#   exit 1
# fi

# Ejemplo 4: Generar badge de coverage
# coverage_color="red"
# if [ "$MJ2_COVERAGE" -ge 90 ]; then
#   coverage_color="brightgreen"
# elif [ "$MJ2_COVERAGE" -ge 80 ]; then
#   coverage_color="green"
# elif [ "$MJ2_COVERAGE" -ge 70 ]; then
#   coverage_color="yellow"
# fi
#
# curl -o .github/badges/coverage.svg \
#   "https://img.shields.io/badge/coverage-${MJ2_COVERAGE}%25-${coverage_color}"

# ============================================

exit 0
