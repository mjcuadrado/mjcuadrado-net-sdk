---
spec_id: SPEC-UX-061
title: UI/UX Expert Agent
domain: DESIGN
complexity: high
estimated_hours: 40-48 hours
status: draft
created: 2025-11-24
updated: 2025-11-24
author: mjcuadrado-net-sdk
version: 1.0.0
issue: "#61"
tags: [ux, design, research, prototyping, usability]
---

# SPEC-UX-061: UI/UX Expert Agent

## 📋 Metadata

- **SPEC ID:** SPEC-UX-061
- **Title:** UI/UX Expert Agent
- **Domain:** DESIGN
- **Complexity:** High
- **Estimated Hours:** 40-48 hours (5-6 días)
- **Issue:** #61
- **Version:** v0.8.0
- **Status:** Draft
- **Created:** 2025-11-24
- **Author:** mjcuadrado-net-sdk

---

## 🎯 Purpose

Crear agente **ui-ux-expert** para diseño UX completo, complementando component-designer con user research, information architecture, interaction design y usability testing.

**Gap identificado:** moai-adk tiene ui-ux-expert. mj2 tiene component-designer (design-first) pero falta UX expertise completo con user research y validation.

**Business Value:**
- User-centered design methodology
- Validated design decisions antes de implementation
- Better user experience y satisfaction
- Reduced rework por poor UX decisions

---

## 📦 Scope

### In Scope

1. **UI/UX Expert Agent** - Complete user experience design agent
2. **`/mj2:ux-design` Command** - Generate UX design artifacts
3. **UX Templates** - User personas, journey maps, wireframes templates
4. **Usability Testing** - Testing guidelines y checklists
5. **Integration** - Con component-designer, accessibility-expert, frontend-builder

### Out of Scope

- Visual design (color palettes, typography) - Eso es component-designer
- Frontend implementation - Eso es frontend-builder
- Accessibility compliance - Eso es accessibility-expert
- UI component library creation - Eso es component-designer

---

## 📝 Functional Requirements (EARS Format)

### FR-1: User Research (Ubiquitous)

**The system SHALL provide user research capabilities:**
- User interviews guidelines
- Survey design templates
- Persona creation from research data
- User segmentation
- Behavioral analysis
- Pain points identification
- User goals y motivations mapping

**Acceptance Criteria:**
- Agent genera user personas basados en research data
- Templates para interviews y surveys disponibles
- Personas incluyen: demographics, goals, pain points, behaviors, tools
- Output en formato markdown con Mr. mj2 recommendations

---

### FR-2: Information Architecture (Event-driven)

**WHEN user requests IA design**
**THEN system SHALL create information architecture:**
- Sitemap generation
- Navigation structure
- Content hierarchy
- Labeling strategy
- Search strategy (si aplica)
- Card sorting recommendations

**Acceptance Criteria:**
- Genera sitemap con estructura jerárquica
- Navigation menu structure
- Content grouping lógico
- Labels claros y user-friendly
- Integration con frontend structure

---

### FR-3: User Journey Mapping (State-driven)

**WHILE user is in journey mapping mode**
**THEN system SHALL create user journey maps:**
- Journey stages (Discover → Try → Use → Recommend)
- User actions en cada stage
- User emotions (frustrations, delights)
- Touchpoints con el sistema
- Pain points y opportunities
- Metrics de éxito por stage

**Acceptance Criteria:**
- Journey map completo con 4+ stages
- Actions, emotions, touchpoints por stage
- Pain points identificados
- Opportunities para improvement
- Visual format (markdown table)

---

### FR-4: Wireframing Guidelines (Ubiquitous)

**The system SHALL provide wireframing guidelines:**
- Low-fidelity wireframe templates
- Layout patterns (hero, grid, list, detail)
- Component placement guidelines
- Responsive design considerations
- Mobile-first recommendations
- Accessibility en wireframes

**Acceptance Criteria:**
- Templates para 5+ common layouts
- Component placement best practices
- Responsive breakpoints documentados
- Accessibility checkpoints en wireframes
- Integration con component-designer

---

### FR-5: Interaction Design (Event-driven)

**WHEN user requests interaction design**
**THEN system SHALL create interaction specifications:**
- User flows (happy path + edge cases)
- Interaction patterns (click, hover, scroll, swipe)
- Micro-interactions design
- Animation guidelines
- Feedback mechanisms (loading, success, error)
- State transitions

**Acceptance Criteria:**
- User flows con decision points
- Interaction patterns documentados
- Micro-interactions specified
- Animation timing y easing
- Error handling flows

---

### FR-6: Prototype Recommendations (Ubiquitous)

**The system SHALL provide prototyping recommendations:**
- Prototype fidelity levels (low, medium, high)
- Tool recommendations (Figma, Sketch, Adobe XD, code prototypes)
- Interactive prototype guidelines
- Prototype testing scenarios
- Handoff to component-designer

**Acceptance Criteria:**
- Fidelity level recommendations basadas en context
- Tool selection criteria
- Interactive elements specification
- Testing scenarios con users
- Clear handoff documentation

---

### FR-7: Usability Testing (Event-driven)

**WHEN user requests usability testing plan**
**THEN system SHALL create testing plan:**
- Test objectives
- Participant criteria (target users)
- Test scenarios y tasks
- Success metrics (completion rate, time, satisfaction)
- Testing script
- Data collection methods
- Analysis framework

**Acceptance Criteria:**
- Test plan con objectives claros
- 5+ test scenarios realistas
- Success metrics definidos
- Testing script step-by-step
- Analysis framework (qualitative + quantitative)

---

### FR-8: Integration con Otros Agents (Ubiquitous)

**The system SHALL integrate con:**
- **component-designer** - Handoff wireframes → components
- **accessibility-expert** - WCAG validation en design phase
- **frontend-builder** - Implementation de designs
- **spec-builder** - UX requirements en SPECs

**Acceptance Criteria:**
- Clear handoff format to component-designer
- Accessibility checkpoints integration
- Design tokens exportables
- Requirements traceable a SPECs

---

## 🎨 Non-Functional Requirements

### NFR-1: Design Quality

**REQUIREMENT:**
- Designs deben seguir UX best practices
- User-centered design methodology
- Data-driven design decisions
- Consistent con design systems existentes

**MEASUREMENT:**
- Design heuristics evaluation (Nielsen's 10 heuristics)
- Usability testing scores (SUS score ≥70)
- Accessibility compliance (WCAG AA minimum)
- Design consistency checklist

---

### NFR-2: Deliverable Clarity

**REQUIREMENT:**
- UX artifacts deben ser claros y actionables
- Non-designers pueden entender outputs
- Handoffs a developers sin ambigüedad

**MEASUREMENT:**
- Peer review clarity score ≥8/10
- Developer questions ≤3 per artifact
- Handoff completeness checklist 100%

---

### NFR-3: Template Reusability

**REQUIREMENT:**
- Templates deben ser reusables across projects
- Customizable per project context
- Version controlled

**MEASUREMENT:**
- Template reuse rate ≥70%
- Customization time ≤15 minutes
- Template completeness score ≥90%

---

### NFR-4: Agent Performance

**REQUIREMENT:**
- Agent responses ≤30 seconds para artifacts básicos
- Memory efficient (≤50MB)
- Outputs en español (consistent con mj2)

**MEASUREMENT:**
- Response time p95 ≤30s
- Memory usage ≤50MB
- Spanish language coverage 100%

---

## 🔗 Dependencies

### Internal Dependencies

- **component-designer** - Para handoff wireframes → components
- **accessibility-expert** - Para WCAG validation en designs
- **frontend-builder** - Para implementation de designs
- **spec-builder** - Para incluir UX requirements en SPECs

### External Dependencies

- **Design Tools** - Figma, Sketch, Adobe XD (recommendations, no integration)
- **Usability Tools** - UserTesting, Maze, Optimal Workshop (recommendations)

### Skills Dependencies

- ✅ `frontend/react.md` - Para React component guidelines
- ✅ `frontend/typescript.md` - Para TypeScript interfaces
- ✅ `frontend/material-ui.md` - Para Material UI component patterns
- ✅ `architecture/design-patterns.md` - Para UI patterns

---

## 🎯 Success Criteria

### Phase 1: SPEC Complete ✅

- [ ] spec.md completado con 8 FRs + 4 NFRs
- [ ] plan.md con 6-phase implementation plan
- [ ] acceptance.md con 15+ acceptance tests
- [ ] TAG @SPEC:UX-061 en commit

### Phase 2: Implementation Complete ✅

- [ ] ui-ux-expert.md agent (~750 líneas)
- [ ] /mj2-ux-design command (~200 líneas)
- [ ] 3+ UX templates (persona, journey, wireframe)
- [ ] Integration con component-designer, accessibility-expert
- [ ] TAG @CODE:UX-061 en commit

### Phase 3: Documentation Complete ✅

- [ ] README.md actualizado (agent count, command count)
- [ ] ROADMAP.md actualizado (Issue #61 COMPLETADO)
- [ ] CHANGELOG.md con Issue #61 entry
- [ ] TAG @DOC:UX-061 en commit

### Phase 4: Quality Gate ✅

- [ ] Agent outputs "Mr. mj2 recomienda" format
- [ ] UX artifacts validation checklist
- [ ] Integration tests con otros agents
- [ ] Español 100%

---

## 📊 Metrics

### Implementation Metrics

- **Total Lines:** ~1,100 (agent + command + templates)
- **Agent Size:** ~750 líneas
- **Command Size:** ~200 líneas
- **Templates:** ~150 líneas
- **Time Estimate:** 40-48 hours (5-6 días)

### Quality Metrics

- **UX Heuristics Coverage:** 10/10 Nielsen's heuristics
- **Template Completeness:** ≥90%
- **Integration Coverage:** 3+ agents
- **Spanish Language:** 100%

---

## 🔄 Workflow Overview

```
/mj2:ux-design <feature>
         ↓
    RESEARCH
    - User personas
    - Pain points
         ↓
    DESIGN
    - Information architecture
    - User journeys
    - Wireframes
         ↓
    PROTOTYPE
    - Fidelity selection
    - Tool recommendations
    - Interactive specs
         ↓
    TEST
    - Usability test plan
    - Success metrics
    - Analysis framework
         ↓
    HANDOFF
    → component-designer (components)
    → accessibility-expert (WCAG check)
    → frontend-builder (implementation)
```

---

## 📝 Notas

### Design Thinking Integration

Agent sigue Design Thinking methodology:
1. **Empathize** - User research (personas, interviews)
2. **Define** - Problem statement (pain points, goals)
3. **Ideate** - Solutions (journey maps, wireframes)
4. **Prototype** - Interactive prototypes
5. **Test** - Usability testing

### Jobs-to-be-Done Framework

Outputs incluyen JTBD analysis:
- **Functional Job** - What user wants to accomplish
- **Emotional Job** - How user wants to feel
- **Social Job** - How user wants to be perceived

---

## ✅ Sign-off

- **Author:** mjcuadrado-net-sdk
- **Reviewer:** (pending)
- **Approved:** (pending)
- **Date:** 2025-11-24

---

**SPEC-UX-061 Version 1.0.0**
**Status:** Draft
**Next:** Create plan.md y acceptance.md
