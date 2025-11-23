# Issue #41: Project Templates

**Fecha:** 2025-11-23
**Prioridad:** 🟢 Baja
**Estado:** ⏭️ SKIPPED (postponed indefinitely)
**Versión:** N/A
**Branch:** N/A
**Razón:** Enfoque en extensibilidad (agent-factory, skill-factory)

---

## 📋 Descripción Original

Crear templates de proyectos completos predefinidos:
- Clean Architecture template
- Vertical Slice template
- Full-stack React + .NET template

**SKIPPED** - Los usuarios pueden crear sus propios templates usando `/mj2:create-agent` y `/mj2:create-skill` (Issue #45).

---

## ⏭️ Por Qué SKIPPED

### Razones Principales:

1. **Agent-Factory & Skill-Factory** (#45) hacen esto obsoleto
   - ✅ Usuarios pueden crear **cualquier** tipo de agente o skill
   - ✅ Más flexible que templates estáticos predefinidos
   - ✅ Extensibilidad completa sin límites
   - ✅ Personalización total según necesidades

2. **Prioridad baja vs mantenimiento alto**
   - ❌ Templates estáticos son menos útiles
   - ❌ Requieren actualización constante con cada versión
   - ❌ Difícil mantener múltiples arquitecturas sincronizadas
   - ❌ Usuarios tienen necesidades muy específicas

3. **Alternativa superior ya disponible**
   - ✅ `/mj2:create-agent --domain backend --workflow generator`
   - ✅ `/mj2:create-skill --category architecture --difficulty advanced`
   - ✅ Usuarios crean templates personalizados según su stack
   - ✅ Factories generan código actualizado siempre

4. **Filosofía mj2: Extensibilidad sobre templates**
   - Sistema de factories permite crear cualquier cosa
   - Mejor documentar cómo crear templates personalizados
   - Enfoque en enseñar a pescar, no dar pescado

---

## 🔄 Alternativa Recomendada

En lugar de templates pre-definidos y estáticos, usar las factories:

### Ejemplo 1: Crear Template de Clean Architecture

```bash
/mj2:create-agent clean-arch-generator \
  --domain architecture \
  --workflow generator \
  --skills "clean-architecture, cqrs, ddd, mediatr"
```

### Ejemplo 2: Crear Template de Vertical Slice

```bash
/mj2:create-agent vertical-slice-generator \
  --domain architecture \
  --workflow generator \
  --skills "vertical-slice, result-pattern, mediatr"
```

### Ejemplo 3: Crear Template Full-Stack

```bash
/mj2:create-agent fullstack-generator \
  --domain fullstack \
  --workflow generator \
  --skills "aspnet-core, react, typescript, postgresql"
```

---

## 📊 Impacto Análisis

### Sin este issue:
- ✅ Usuarios pueden crear templates personalizados (Issue #45)
- ✅ Mayor flexibilidad vs templates estáticos predefinidos
- ✅ Sin mantenimiento de templates que se vuelven obsoletos
- ✅ Cada usuario crea lo que necesita exactamente
- ✅ Templates siempre actualizados (factories usan latest skills)

### Si se implementara:
- ❌ Templates estáticos se vuelven obsoletos rápido
- ❌ Alto costo de mantenimiento (3 templates × updates frecuentes)
- ❌ Menos flexible que factories dinámicos
- ❌ No cubre todos los casos de uso posibles
- ❌ Usuarios limitados a 3 opciones predefinidas

---

## 📖 Documentación Alternativa

En lugar de implementar este issue, se recomienda:

1. **Guía: "Cómo crear tu propio template"**
   - Usar agent-factory y skill-factory
   - Ejemplos de templates comunes
   - Best practices para project generators

2. **Actualizar README.md**
   - Sección: "Creando Templates Personalizados"
   - Ejemplos con `/mj2:create-agent`
   - Links a Issue #45 (Agent & Skill Factory)

3. **Video/Tutorial**
   - Demo de creación de template personalizado
   - Live coding session
   - Casos de uso reales

---

## 🔗 Referencias

- **Issue #45:** Agent-Factory & Skill-Factory (✅ COMPLETADO - GAME CHANGER)
- **Alternativa:** `/mj2:create-agent` y `/mj2:create-skill`
- **Documentado en:** Issue #53 (Documentation Sync & Audit)
- **Filosofía:** Extensibilidad > Templates estáticos

---

## 💡 Lecciones Aprendidas

1. **Factories > Templates**
   - Más flexible, menos mantenimiento
   - Usuarios crean lo que necesitan

2. **Teach to fish**
   - Mejor enseñar a crear templates
   - Que dar templates predefinidos limitados

3. **Mantenimiento es costoso**
   - Templates requieren updates constantes
   - Factories se actualizan solos (usan skills actuales)

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Status:** ⏭️ SKIPPED
**Razón:** Superseded by Issue #45
**Asignado a:** N/A
**Milestone:** N/A (won't implement)
