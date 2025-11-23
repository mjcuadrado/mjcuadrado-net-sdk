#!/bin/bash
# Template: pre-command hook
# Descripción: Se ejecuta ANTES de ejecutar un comando /mj2:*
# Variables disponibles:
#   - $MJ2_COMMAND: Comando a ejecutar (ej: "1-plan", "2-run")
#   - $MJ2_ARGS: Argumentos del comando
#   - $MJ2_USER: Usuario ejecutando el comando
#   - $MJ2_TIMESTAMP: Timestamp ISO 8601

set -e  # Exit on error

# Log inicio
echo "[PRE-COMMAND] Command: $MJ2_COMMAND"
echo "[PRE-COMMAND] Args: $MJ2_ARGS"
echo "[PRE-COMMAND] User: $MJ2_USER"
echo "[PRE-COMMAND] Timestamp: $MJ2_TIMESTAMP"

# ============================================
# TU CÓDIGO AQUÍ
# ============================================

# Ejemplo 1: Validar que no haya cambios sin commitear
# git_status=$(git status --porcelain)
# if [ -n "$git_status" ]; then
#   echo "⚠️  Warning: Uncommitted changes detected"
# fi

# Ejemplo 2: Validar requirements previos
# if [ "$MJ2_COMMAND" == "2-run" ]; then
#   if [ ! -f ".mj2/specs/current-spec.md" ]; then
#     echo "❌ Error: No SPEC found. Run /mj2:1-plan first"
#     exit 1  # Exit code 1 bloquea la ejecución
#   fi
# fi

# Ejemplo 3: Pre-checks de ambiente
# if [ -z "$DATABASE_URL" ]; then
#   echo "⚠️  Warning: DATABASE_URL not set"
# fi

# ============================================
# IMPORTANTE: Exit codes
# ============================================
# exit 0  - SUCCESS: Permite que el comando continúe
# exit 1  - ERROR: Bloquea la ejecución del comando
# ============================================

exit 0
