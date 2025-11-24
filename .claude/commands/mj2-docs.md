---
name: mj2-docs
description: Complete documentation management and automation
agent: docs-manager
version: 1.0.0
author: mjcuadrado-net-sdk
tags: [mj2, documentation, automation]
---

# /mj2:docs - Documentation Management

Gestión completa y automatizada de documentación del proyecto usando el **docs-manager** agent.

## 🎯 Purpose

Automatizar la gestión de documentación del proyecto:
- **Auditoría** de documentación (completeness, format compliance)
- **Actualización** de README.md y CHANGELOG.md
- **Generación** de documentación faltante (API docs, ADRs, arquitectura)
- **Publicación** a GitHub Pages

## 📋 Usage

```bash
/mj2:docs audit           # Auditoría de documentación
/mj2:docs update          # Actualizar README & CHANGELOG
/mj2:docs generate        # Generar documentación faltante
/mj2:docs publish         # Publicar a GitHub Pages
/mj2:docs --help          # Mostrar ayuda
```

---

## 🔄 Actions

### Action 1: `audit` - Documentation Audit

Auditoría completa de documentación del proyecto.

**Command:**
```bash
/mj2:docs audit
```

**What it does:**
1. **README.md Audit:**
   - Verifica título, descripción, badges
   - Verifica secciones: Installation, Usage, Examples
   - Verifica License, Contributing

2. **CHANGELOG.md Audit:**
   - Verifica formato Keep a Changelog
   - Verifica sección Unreleased
   - Verifica orden de versiones (descending)
   - Verifica Semantic Versioning

3. **API Documentation Audit:**
   - Verifica cobertura de API endpoints
   - Verifica OpenAPI/Swagger spec
   - Verifica ejemplos de request/response

4. **Architecture Documentation Audit:**
   - Verifica C4 diagrams
   - Verifica ADRs
   - Verifica documentación de diseño

**Output Format:**
```
🔍 Auditoría de Documentación

📊 Resultados:

README.md: 75/100 ⚠️
  ✅ Título presente
  ✅ Descripción presente
  ⚠️  Badges incompletos (version badge faltante)
  ✅ Installation section presente
  ✅ Usage section presente
  ❌ Examples section faltante
  ✅ License presente

CHANGELOG.md: 85/100 ✅
  ✅ Keep a Changelog format
  ✅ Unreleased section presente
  ✅ Versiones en orden descendente
  ⚠️  v0.5.0 sin fecha

API Docs: 40/100 ❌
  ❌ OpenAPI spec faltante
  ❌ API endpoints no documentados

Architecture: 60/100 ⚠️
  ✅ C4 Context diagram presente
  ❌ C4 Container diagram faltante
  ⚠️  Solo 2 ADRs (recomienda 5+)

📈 Score Global: 65/100 ⚠️

🤖 Mr. mj2 recomienda:
   1. Generar OpenAPI spec: /mj2:docs generate
   2. Actualizar README badges: /mj2:docs update
   3. Crear C4 Container diagram
   4. Agregar examples section a README

💡 Tip: Usa /mj2:docs update para corregir issues automáticos
```

---

### Action 2: `update` - Update Documentation

Actualiza README.md y CHANGELOG.md con datos actuales del proyecto.

**Command:**
```bash
/mj2:docs update
```

**What it does:**
1. **Update README.md:**
   - Version badge (from config.json)
   - Build status badge (from CI)
   - Coverage badge (from coverage report)
   - Feature list (from SPEC docs)
   - Installation instructions (from package info)
   - Quick Start examples (from current API)

2. **Update CHANGELOG.md:**
   - Agrega entry para versión actual
   - Categoriza cambios (Added/Changed/Fixed/etc.)
   - Agrega links a commits/PRs
   - Marca breaking changes

**Output Format:**
```
✅ Documentación actualizada

📝 Cambios realizados:

README.md:
  ✅ Version badge: v0.5.0 → v0.6.0
  ✅ Coverage badge: 75% → 82%
  ✅ Feature list actualizada (3 nuevas features)
  ✅ Quick Start actualizado

CHANGELOG.md:
  ✅ Entry v0.6.0 agregada (2024-11-24)
  ✅ 5 Added, 3 Changed, 2 Fixed
  ✅ Links a commits agregados
  ⚠️  1 breaking change marcado

🤖 Mr. mj2 recomienda:
   1. Review CHANGELOG entry
   2. Commit cambios: git add README.md CHANGELOG.md
   3. Tag release: git tag v0.6.0
   4. Ver estado: /mj2:status

💡 Tip: Siempre review CHANGELOG antes de commit
```

---

### Action 3: `generate` - Generate Missing Documentation

Genera documentación faltante identificada en auditoría.

**Command:**
```bash
/mj2:docs generate
```

**What it does:**
1. **Generate API Documentation:**
   - OpenAPI/Swagger spec from ASP.NET Core controllers
   - API endpoint documentation (Markdown)
   - Request/response schemas
   - Authentication documentation

2. **Generate Architecture Documentation:**
   - C4 Context diagram (Mermaid)
   - C4 Container diagram (Mermaid)
   - C4 Component diagram (Mermaid)
   - System overview

3. **Generate ADR Templates:**
   - ADR template for new decisions
   - Numbered ADR files

4. **Generate Missing Sections:**
   - README sections faltantes
   - CONTRIBUTING.md (if missing)
   - CODE_OF_CONDUCT.md (if missing)

**Output Format:**
```
🔧 Generando documentación faltante...

📄 Documentos generados:

API Documentation:
  ✅ docs/api/openapi.yaml (OpenAPI 3.0 spec)
  ✅ docs/api/endpoints.md (15 endpoints documentados)
  ✅ docs/api/authentication.md
  ✅ docs/api/schemas.md (12 schemas)

Architecture:
  ✅ docs/architecture/c4-context.md (Mermaid diagram)
  ✅ docs/architecture/c4-container.md (Mermaid diagram)
  ✅ docs/architecture/overview.md

Templates:
  ✅ docs/adr/template.md
  ✅ CONTRIBUTING.md
  ✅ CODE_OF_CONDUCT.md

📊 Total: 10 archivos generados

🤖 Mr. mj2 recomienda:
   1. Review generated docs
   2. Customize templates según proyecto
   3. Commit: git add docs/
   4. Publicar: /mj2:docs publish

💡 Tip: C4 diagrams en Mermaid - editables en GitHub
```

---

### Action 4: `publish` - Publish to GitHub Pages

Prepara documentación para publicación a GitHub Pages.

**Command:**
```bash
/mj2:docs publish
```

**What it does:**
1. **Prepare GitHub Pages Structure:**
   - Crea/actualiza `docs/` folder structure
   - Genera `docs/_config.yml` (Jekyll config)
   - Genera `docs/index.md` (landing page)
   - Genera navigation

2. **Generate Static Site:**
   - Convierte Markdown a static site
   - Agrega navigation links
   - Agrega search (si disponible)

3. **Verify Publishing Requirements:**
   - Verifica GitHub Pages habilitado
   - Verifica branch configurado
   - Verifica custom domain (if any)

**Output Format:**
```
🚀 Preparando publicación a GitHub Pages...

📁 Estructura creada:

docs/
  ├── _config.yml ✅
  ├── index.md ✅
  ├── api/
  │   ├── endpoints.md ✅
  │   └── authentication.md ✅
  ├── architecture/
  │   ├── c4-context.md ✅
  │   └── overview.md ✅
  └── adr/
      └── 001-decision.md ✅

🔧 Configuración:

Jekyll:
  ✅ Theme: minima
  ✅ Navigation: 4 secciones
  ✅ Plugins: jekyll-seo-tag, jekyll-sitemap

GitHub Pages:
  ✅ Enabled: true
  ✅ Branch: main
  ✅ Folder: /docs
  ✅ URL: https://mjcuadrado.github.io/mjcuadrado-claude-adk

🤖 Mr. mj2 recomienda:
   1. Commit cambios: git add docs/
   2. Push: git push origin main
   3. Esperar build (~2 min)
   4. Verificar: https://mjcuadrado.github.io/mjcuadrado-claude-adk

💡 Tip: GitHub Pages rebuild automático en cada push
```

---

## 🔗 Integration with Other Agents

### doc-syncer Integration
```
/mj2:docs update → doc-syncer (TAG sync) → Git commit
```

El docs-manager delega TAG chain sync al doc-syncer:
- docs-manager: Genera/actualiza contenido
- doc-syncer: Sincroniza TAG chain (@DOC tags)

### api-designer Integration
```
/mj2:api-design → docs-manager (API docs) → /mj2:docs generate
```

El docs-manager usa estructura de api-designer para generar API docs.

### release-manager Integration
```
/mj2:99-release → docs-manager (CHANGELOG) → /mj2:docs update
```

El docs-manager genera CHANGELOG entry desde datos de release-manager.

### quality-gate Integration
```
/mj2:quality-check → docs-manager (audit) → Quality report
```

El quality-gate incluye documentation coverage check del docs-manager.

---

## 📊 Workflow Examples

### Example 1: Fresh Project Documentation
```bash
# 1. Auditoría inicial
/mj2:docs audit
# Output: Score 30/100 ❌

# 2. Generar docs faltantes
/mj2:docs generate
# Output: 10 archivos generados ✅

# 3. Actualizar README/CHANGELOG
/mj2:docs update
# Output: README & CHANGELOG actualizados ✅

# 4. Nueva auditoría
/mj2:docs audit
# Output: Score 85/100 ✅
```

### Example 2: Post-Release Documentation
```bash
# 1. Release completado
/mj2:99-release

# 2. Actualizar docs con release info
/mj2:docs update
# Output: CHANGELOG v0.6.0 agregado ✅

# 3. Publicar
/mj2:docs publish
# Output: GitHub Pages actualizado ✅
```

### Example 3: API Changes Documentation
```bash
# 1. Diseñar API
/mj2:api-design

# 2. Implementar endpoints
# (código...)

# 3. Generar API docs
/mj2:docs generate
# Output: OpenAPI spec + endpoint docs ✅

# 4. Publicar
/mj2:docs publish
```

---

## 🎯 Best Practices

1. **Run audit frequently:** `/mj2:docs audit` después de cambios significativos
2. **Update before release:** `/mj2:docs update` antes de cada release
3. **Generate early:** `/mj2:docs generate` al principio del proyecto
4. **Publish regularly:** `/mj2:docs publish` después de docs updates
5. **Keep CHANGELOG updated:** Usar Keep a Changelog format
6. **Use templates:** Generar templates con `/mj2:docs generate`

---

## 📋 Command Reference

| Action | Purpose | Output |
|--------|---------|--------|
| `audit` | Auditoría de documentación | Score + recommendations |
| `update` | Actualizar README/CHANGELOG | Updated files |
| `generate` | Generar docs faltantes | Generated files |
| `publish` | Publicar a GitHub Pages | Static site ready |

---

**Agent:** docs-manager
**Version:** 1.0.0
**Created:** 2024-11-24
**Tags:** @CODE:DOC-002
