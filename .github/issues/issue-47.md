# Issue #47: Personalization System

**Fecha:** 2025-11-23
**Prioridad:** 🟡 Media
**Estado:** ⏭️ POSTPONED
**Versión:** v0.6.0+ (futuro)
**Branch:** N/A
**Razón:** Prioridad baja vs Issues #51-52 más críticos

---

## 📋 Descripción

Sistema de personalización para configurar nombre de usuario e idioma del SDK.

**POSTPONED para versión futura** - Issues #51 (Output Styles) y #52 (MCP Integrations) tienen mayor prioridad para completar v0.5.0.

---

## 🎯 Objetivos (Futuro)

### 1. Personalización Usuario
- Actualizar `.mjcuadrado-net-sdk/config.json` template
- Añadir campo `user.name` para personalización
- Actualizar todos los agentes para usar nombre del usuario en mensajes

### 2. Sistema Multilenguaje Básico
- `language.conversation_language` (es, en)
- `language.agent_prompt_language` (en recomendado)
- Actualizar agentes para soportar ambos idiomas
- Templates de mensajes en español e inglés

---

## 📦 Entregables (Cuando se implemente)

### 1. Config.json Actualizado
```json
{
  "user": {
    "name": "Usuario"
  },
  "language": {
    "conversation_language": "es",
    "agent_prompt_language": "en"
  }
}
```

### 2. Agentes Actualizados
- Usar `{{user.name}}` en mensajes
- Soportar español/inglés según config

### 3. Documentación
- `.github/issues/issue-47.md`
- Actualizar README.md con personalización
- Ejemplos de configuración

---

## ⏭️ Por Qué Postponed

### Razones Principales:

1. **Prioridad baja vs Issues #51-52**
   - Issue #51 (Output Styles): Mejora UX importante
   - Issue #52 (MCP Integrations): Evaluación crítica para integración

2. **UX improvement no crítico**
   - Sistema funciona perfectamente sin personalización
   - Agentes son efectivos en español por defecto

3. **Enfoque en completar v0.5.0**
   - 6 de 9 issues completados
   - Priorizar Issues #51-52 para completar versión

4. **Multilenguaje complejo**
   - Requiere actualizar 21 agentes
   - Requiere mantener templates en 2 idiomas
   - Mejor hacerlo en v0.6.0 con más tiempo

---

## 📊 Métricas Estimadas

- **Tiempo:** 4-5 días (cuando se implemente)
- **Archivos a modificar:** 25+ (21 agentes + config + docs)
- **Líneas de código:** ~500
- **Idiomas:** 2 (español, inglés)

---

## 🔗 Referencias

- **Adaptar de:** moai-adk/configuration, moai-adk/language-detection
- **Documentado en:** Issue #53 (Documentation Sync & Audit)
- **Milestone:** v0.6.0+ (futuro)

---

## 🚀 Cuándo Implementar

**Criterios para activar este issue:**

1. v0.5.0 completada (Issues #51-52 done)
2. v1.0.0 en preparación
3. Demanda de usuarios por multilenguaje
4. Tiempo disponible (4-5 días)

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Status:** ⏭️ POSTPONED
**Asignado a:** @mjcuadrado
**Milestone:** Future (v0.6.0+)
