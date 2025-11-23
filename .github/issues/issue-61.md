# Issue #61: UI/UX Expert Agent

**Fecha:** 2025-11-23
**Prioridad:** 🟡 Media
**Estado:** 📋 Planificado
**Versión:** v0.8.0
**Branch:** feature/ISSUE-061-ui-ux-expert
**Tiempo Estimado:** 5-6 días

---

## 📋 Descripción

Crear agente **ui-ux-expert** para diseño UX completo, complementando component-designer.

**Gap identificado:** moai-adk tiene este agente. mj2 tiene component-designer (design-first) pero falta UX expertise completo.

---

## 🎯 Objetivos

### 1. UI/UX Expert Agent
- Crear `.claude/agents/mj2/ui-ux-expert.md` (~750 líneas)
  - TRUST 5 principles
  - Workflow: RESEARCH → DESIGN → PROTOTYPE → TEST
  - User research
  - Information architecture
  - Interaction design
  - Usability testing

### 2. Comando Slash
- Crear `.claude/commands/mj2-ux-design.md` (~200 líneas)
  - Sintaxis: `/mj2:ux-design <feature>`
  - Output: UX design document

---

## 📦 Entregables

### 1. ui-ux-expert.md Agent
**Workflow:**
1. **RESEARCH** - User research, personas
2. **DESIGN** - Information architecture, wireframes
3. **PROTOTYPE** - Interactive prototypes
4. **TEST** - Usability testing, A/B testing

**Deliverables:**
- User personas
- User journey maps
- Information architecture
- Wireframes
- Interactive prototypes
- Usability test reports

### 2. Design Artifacts
```markdown
# User Persona: Developer Diego
- **Age:** 32
- **Goals:** Fast development, good DX
- **Pain Points:** Complex configuration
- **Tools:** VS Code, CLI

# User Journey Map
1. Discover feature
2. Read documentation
3. Try example
4. Customize
5. Deploy
```

### 3. Integration
- component-designer: From UX → Component
- accessibility-expert: WCAG validation
- frontend-builder: Implementation

---

## ✅ Criterios de Éxito

- [ ] ui-ux-expert.md agent creado (~750 líneas)
- [ ] /mj2:ux-design command creado (~200 líneas)
- [ ] User persona templates
- [ ] Journey map templates
- [ ] Wireframe guidelines
- [ ] Usability testing checklists
- [ ] Integration con component-designer

---

## 🔗 Referencias

- **Inspirado en:** moai-adk/ui-ux-expert
- **Complementa:** component-designer, accessibility-expert
- **Methods:** Design Thinking, Jobs-to-be-Done

---

## 🚀 Impacto

**Sin ui-ux-expert:**
- ❌ No user research
- ❌ Design without validation
- ❌ Poor UX

**Con ui-ux-expert:**
- ✅ User-centered design
- ✅ Validated design decisions
- ✅ Better UX
- ✅ Higher user satisfaction

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Prioridad:** 🟡 MEDIA
**Milestone:** v0.8.0
