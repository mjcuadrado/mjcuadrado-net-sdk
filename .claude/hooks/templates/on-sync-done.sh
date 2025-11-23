#!/bin/bash
# Template: on-sync-done hook
# Descripción: Se ejecuta cuando se completa /mj2:3-sync
# Variables disponibles:
#   - $MJ2_SYNC_FILES_COUNT: Número de archivos sincronizados
#   - $MJ2_SYNC_DURATION: Duración del sync en ms
#   - $MJ2_SYNC_ERRORS: Número de errores durante sync
#   - $MJ2_TIMESTAMP: Timestamp ISO 8601

set -e  # Exit on error

# Log evento
echo "[ON-SYNC-DONE] Files synced: $MJ2_SYNC_FILES_COUNT"
echo "[ON-SYNC-DONE] Duration: ${MJ2_SYNC_DURATION}ms"
echo "[ON-SYNC-DONE] Errors: $MJ2_SYNC_ERRORS"

# ============================================
# TU CÓDIGO AQUÍ
# ============================================

# Ejemplo 1: Publicar documentación a GitHub Pages
# if [ "$MJ2_SYNC_ERRORS" -eq 0 ]; then
#   cd docs/
#   git add .
#   git commit -m "docs: Update documentation" --no-verify || true
#   git push origin gh-pages
# fi

# Ejemplo 2: Sync a Notion/Confluence
# for file in .mj2/docs/*.md; do
#   # Upload to Notion API
#   notion_page_id=$(notion-cli upload "$file")
#   echo "✅ Synced to Notion: $notion_page_id"
# done

# Ejemplo 3: Generar PDF de documentación
# pandoc .mj2/docs/*.md -o documentation.pdf
# echo "✅ PDF generated: documentation.pdf"

# Ejemplo 4: Notificar que documentación está actualizada
# if [ "$MJ2_SYNC_FILES_COUNT" -gt 0 ]; then
#   echo "📚 Documentation updated: $MJ2_SYNC_FILES_COUNT files"
# fi

# ============================================

exit 0
