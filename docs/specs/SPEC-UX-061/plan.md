---
spec_id: SPEC-UX-061
title: UI/UX Expert Agent - Implementation Plan
domain: DESIGN
complexity: high
estimated_hours: 40-48 hours
status: draft
created: 2025-11-24
version: 1.0.0
---

# SPEC-UX-061: Implementation Plan

## 📋 Overview

**Objetivo:** Crear ui-ux-expert agent con workflow RESEARCH → DESIGN → PROTOTYPE → TEST para user-centered design completo.

**Tiempo Total:** 40-48 hours (5-6 días)
**Complejidad:** High
**Prioridad:** 🟡 Media

---

## 🎯 Implementation Strategy

### Approach

**User-Centered Design Focus:**
- Research-driven (personas, pain points)
- Iterative design process
- Validation-first (usability testing)
- Handoff-optimized (clear artifacts)

**Template-Based:**
- Reusable persona templates
- Journey map templates
- Wireframe guidelines templates
- Usability test plan templates

**Integration-Optimized:**
- Clear handoff to component-designer
- Accessibility checkpoints integration
- Frontend-builder compatible outputs

---

## 📅 Phase Breakdown

### Phase 1: SPEC & Planning (6-8 hours)

**Objetivo:** Complete SPEC documentation y setup

**Tasks:**

1. **SPEC Documentation** (4 hours)
   - ✅ spec.md - 8 FRs + 4 NFRs
   - ⏳ plan.md - 6-phase implementation plan
   - ⏳ acceptance.md - 15+ acceptance tests

2. **Research & Analysis** (2-3 hours)
   - Estudiar moai-adk/ui-ux-expert
   - Analizar Design Thinking methodology
   - Analizar Jobs-to-be-Done framework
   - Review Nielsen's 10 usability heuristics

3. **Git Setup** (0.5 hour)
   - Create feature branch: `feature/SPEC-UX-061`
   - Commit SPEC docs con @SPEC:UX-061 tag

**Deliverables:**
- ✅ SPEC-UX-061 completo (spec, plan, acceptance)
- Git branch creado
- Research notes

**Success Criteria:**
- SPEC complete con sign-off
- Implementation plan clear
- Git history clean

---

### Phase 2: Templates Creation (8-10 hours)

**Objetivo:** Crear UX artifact templates reusables

**Tasks:**

1. **User Persona Template** (2-3 hours)
   - Create `.claude/templates/ux/user-persona.md`
   - Sections: Demographics, Goals, Pain Points, Behaviors, Tools, Quote
   - Example persona: "Developer Diego"
   - Variables: {{name}}, {{age}}, {{role}}, {{goals}}, {{painPoints}}

2. **User Journey Map Template** (2-3 hours)
   - Create `.claude/templates/ux/user-journey.md`
   - Stages: Discover → Try → Use → Recommend
   - Per stage: Actions, Emotions, Touchpoints, Pain Points, Opportunities
   - Markdown table format

3. **Wireframe Guidelines Template** (2 hours)
   - Create `.claude/templates/ux/wireframe-guidelines.md`
   - Layout patterns: Hero, Grid, List, Detail
   - Component placement rules
   - Responsive breakpoints
   - Accessibility checkpoints

4. **Usability Test Plan Template** (2 hours)
   - Create `.claude/templates/ux/usability-test-plan.md`
   - Sections: Objectives, Participants, Scenarios, Metrics, Script, Analysis
   - Success metrics formulas (completion rate, time on task, SUS score)

**Deliverables:**
- 4 UX templates (~150 líneas total)
- Templates con variables para customization
- Examples en cada template

**Success Criteria:**
- Templates reusable across projects
- Clear instructions en cada template
- Variables bien documentadas

---

### Phase 3: UI/UX Expert Agent (16-20 hours)

**Objetivo:** Implementar agent core con workflow completo

**Tasks:**

1. **Agent Structure** (2 hours)
   - Create `.claude/agents/mj2/ui-ux-expert.md`
   - Agent persona y mission
   - Workflow overview: RESEARCH → DESIGN → PROTOTYPE → TEST
   - Integration points

2. **Phase 1: RESEARCH** (4-5 hours)
   - User research guidelines
   - Interview techniques (open-ended questions, probing)
   - Survey design (quantitative + qualitative)
   - Persona generation logic
   - Pain points identification
   - Goals y motivations mapping
   - User segmentation

3. **Phase 2: DESIGN** (4-5 hours)
   - Information architecture
     - Sitemap generation logic
     - Navigation structure
     - Content hierarchy
     - Labeling strategy
   - User journey mapping
     - Journey stages definition
     - Touchpoints identification
     - Emotions mapping
     - Opportunity identification
   - Wireframing
     - Layout selection logic
     - Component placement
     - Responsive considerations

4. **Phase 3: PROTOTYPE** (3-4 hours)
   - Fidelity level selection
     - Low-fi: Paper sketches, basic wireframes
     - Medium-fi: Interactive wireframes
     - High-fi: Pixel-perfect prototypes
   - Tool recommendations
     - Figma (collaborative, versioning)
     - Sketch (Mac-only, plugins)
     - Adobe XD (Adobe ecosystem)
     - Code prototypes (React, HTML)
   - Interactive elements specification
   - Animation guidelines

5. **Phase 4: TEST** (3-4 hours)
   - Usability testing plan generation
     - Test objectives
     - Participant criteria
     - Test scenarios (task-based)
     - Success metrics (SUS, completion rate, time)
   - Testing script
   - Data collection methods
   - Analysis framework
     - Qualitative analysis (themes, quotes)
     - Quantitative analysis (metrics, benchmarks)

6. **Integration Logic** (1-2 hours)
   - Handoff to component-designer
     - Design tokens export
     - Component specifications
   - Accessibility checkpoints
     - Call accessibility-expert for WCAG validation
   - Frontend implementation
     - Call frontend-builder con design specs

**Deliverables:**
- ui-ux-expert.md agent (~750 líneas)
- 4-phase workflow implemented
- Integration con 3+ agents
- "Mr. mj2 recomienda" output format

**Success Criteria:**
- Workflow phases complete
- Clear outputs per phase
- Integration points working
- Español 100%

---

### Phase 4: Command Implementation (6-8 hours)

**Objetivo:** Create /mj2:ux-design command

**Tasks:**

1. **Command Structure** (1 hour)
   - Create `.claude/commands/mj2-ux-design.md`
   - Syntax: `/mj2:ux-design <feature> [OPTIONS]`
   - Options definition

2. **Command Actions** (3-4 hours)
   - **Action 1: Full UX Design** (default)
     - Input: Feature name
     - Run: RESEARCH → DESIGN → PROTOTYPE → TEST
     - Output: Complete UX artifact package

   - **Action 2: Research Only** (`--research`)
     - Run: RESEARCH phase only
     - Output: User personas + pain points

   - **Action 3: Journey Map** (`--journey`)
     - Input: User persona
     - Run: DESIGN phase (journey mapping)
     - Output: User journey map

   - **Action 4: Wireframes** (`--wireframe`)
     - Input: Feature specs
     - Run: DESIGN phase (wireframing)
     - Output: Wireframe guidelines + layout recommendations

   - **Action 5: Test Plan** (`--test`)
     - Input: Design artifacts
     - Run: TEST phase
     - Output: Usability test plan

3. **Examples** (2 hours)
   - Example 1: Full UX design for "User Profile Management"
   - Example 2: Research only for "Payment Flow"
   - Example 3: Journey map for existing persona
   - Example 4: Wireframes for "Dashboard"
   - Example 5: Test plan for prototype

4. **Integration Documentation** (1 hour)
   - Usage con component-designer
   - Usage con accessibility-expert
   - Usage con frontend-builder
   - Workflow diagrams

**Deliverables:**
- /mj2-ux-design command (~200 líneas)
- 5 actions implemented
- 5 complete examples
- Integration documentation

**Success Criteria:**
- Command syntax clear
- Actions bien diferenciadas
- Examples realistic y útiles
- Integration workflows documented

---

### Phase 5: Documentation & Testing (6-8 hours)

**Objetivo:** Complete project documentation y testing

**Tasks:**

1. **README.md Update** (1 hour)
   - Update agent count: 25 → 26
   - Update command count: 25 → 26
   - Add UI/UX Expert to Mr. mj2 agent list
   - Add Issue #61 details to v0.8.0 section

2. **ROADMAP.md Update** (1 hour)
   - Mark Issue #61 as COMPLETADO
   - Update Gap Analysis (26 agentes, 26 comandos)
   - Update missing agents list (remove ui-ux-expert)
   - Update v0.8.0 progress

3. **CHANGELOG.md Update** (1 hour)
   - Add comprehensive Issue #61 entry
   - Details de agent, command, templates
   - TAG chain documentation

4. **Integration Testing** (2-3 hours)
   - Test ui-ux-expert agent outputs
   - Test /mj2:ux-design command actions
   - Test handoff to component-designer
   - Test accessibility-expert integration
   - Test template customization

5. **Quality Review** (1-2 hours)
   - Review agent outputs format
   - Review template completeness
   - Review Spanish language quality
   - Review "Mr. mj2 recomienda" consistency

**Deliverables:**
- README.md actualizado
- ROADMAP.md actualizado
- CHANGELOG.md actualizado
- Integration tests passed
- Quality review complete

**Success Criteria:**
- Documentation 100% up to date
- No inconsistencies
- Integration tests passing
- Quality standards met

---

### Phase 6: Git & Release (2-4 hours)

**Objetivo:** Clean git history y push to GitHub

**Tasks:**

1. **Git Commits** (1-2 hours)
   - Commit SPEC phase: @SPEC:UX-061
   - Commit CODE phase: @CODE:UX-061
     - Agent + Command + Templates
   - Commit DOC phase: @DOC:UX-061
     - README + ROADMAP + CHANGELOG

2. **Code Review** (0.5-1 hour)
   - Self-review all changes
   - Check TAG chain complete
   - Verify no breaking changes

3. **Merge & Push** (0.5-1 hour)
   - Merge feature/SPEC-UX-061 to main
   - Push to GitHub
   - Verify GitHub Actions passing

4. **Issue Closure** (0.5 hour)
   - Close Issue #61 con comment
   - Link commits
   - Tag v0.8.0 (si es el último issue de la versión)

**Deliverables:**
- Clean git history (3 commits)
- Pushed to GitHub
- Issue #61 closed

**Success Criteria:**
- TAG chain complete: @SPEC:UX-061 → @CODE:UX-061 → @DOC:UX-061 ✅
- GitHub Actions passing
- No merge conflicts
- Issue closed

---

## 📊 Timeline Summary

| Phase | Duration | Deliverables |
|-------|----------|-------------|
| 1. SPEC & Planning | 6-8 hours | SPEC docs, branch setup |
| 2. Templates | 8-10 hours | 4 UX templates |
| 3. Agent | 16-20 hours | ui-ux-expert.md (~750 líneas) |
| 4. Command | 6-8 hours | /mj2-ux-design (~200 líneas) |
| 5. Documentation | 6-8 hours | README, ROADMAP, CHANGELOG |
| 6. Git & Release | 2-4 hours | Commits, merge, push |
| **TOTAL** | **44-58 hours** | **6-7 días** |

---

## 🎯 Critical Path

```
SPEC (Phase 1)
    ↓
Templates (Phase 2) ← Blocker para Agent
    ↓
Agent (Phase 3) ← Longest phase (16-20h)
    ↓
Command (Phase 4) ← Depends on Agent
    ↓
Documentation (Phase 5)
    ↓
Git & Release (Phase 6)
```

**Bottleneck:** Phase 3 (Agent implementation) - 16-20 hours

---

## 🚧 Risks & Mitigation

### Risk 1: Scope Creep (Medium)

**Risk:** UX es amplio, puede expandirse infinitamente.

**Mitigation:**
- Stick to SPEC requirements (8 FRs)
- Focus en core UX artifacts (persona, journey, wireframe, test)
- Out of scope: visual design, detailed UI specs

---

### Risk 2: Template Complexity (Low)

**Risk:** Templates demasiado complejos o poco flexibles.

**Mitigation:**
- Start simple, iterate
- Test templates con example scenarios
- Variables claras para customization

---

### Risk 3: Integration Issues (Medium)

**Risk:** Handoffs a otros agents poco claros.

**Mitigation:**
- Define clear handoff formats
- Test integration con component-designer
- Document integration workflows

---

### Risk 4: Time Estimate (Low)

**Risk:** 40-48 hours puede ser optimista.

**Mitigation:**
- Buffer en cada phase (+20%)
- Prioritize core functionality first
- Move nice-to-haves a future iterations

---

## ✅ Success Metrics

### Implementation Metrics

- [ ] **Total Lines:** ~1,100 (agent + command + templates)
- [ ] **Agent Complete:** ~750 líneas
- [ ] **Command Complete:** ~200 líneas
- [ ] **Templates:** 4 templates (~150 líneas)
- [ ] **Time:** ≤58 hours (dentro de estimate)

### Quality Metrics

- [ ] **UX Heuristics:** 10/10 Nielsen's heuristics covered
- [ ] **Template Reusability:** ≥70% reuse rate
- [ ] **Integration Coverage:** 3+ agents integrated
- [ ] **Spanish Language:** 100%
- [ ] **"Mr. mj2" Format:** Consistent

### Delivery Metrics

- [ ] **TAG Chain:** @SPEC → @CODE → @DOC ✅
- [ ] **Documentation:** 100% up to date
- [ ] **Tests:** Integration tests passing
- [ ] **GitHub:** Pushed sin errors

---

## 📝 Implementation Notes

### Design Thinking Integration

Agent sigue Design Thinking methodology en workflow:

1. **Empathize** (RESEARCH phase)
   - User interviews
   - Personas
   - Pain points

2. **Define** (RESEARCH → DESIGN transition)
   - Problem statements
   - User needs
   - Design goals

3. **Ideate** (DESIGN phase)
   - Information architecture
   - Journey maps
   - Wireframes

4. **Prototype** (PROTOTYPE phase)
   - Fidelity selection
   - Interactive specs
   - Tool recommendations

5. **Test** (TEST phase)
   - Usability testing
   - Metrics collection
   - Iteration recommendations

### Jobs-to-be-Done Framework

Outputs incluyen JTBD analysis para cada feature:

```markdown
## Job to Be Done

**Functional Job:**
When [situation], I want to [motivation], so I can [outcome].

**Emotional Job:**
I want to feel [emotion] when using this feature.

**Social Job:**
I want to be perceived as [perception] by using this.
```

### Nielsen's 10 Usability Heuristics

Agent valida designs contra 10 heuristics:

1. Visibility of system status
2. Match between system and real world
3. User control and freedom
4. Consistency and standards
5. Error prevention
6. Recognition rather than recall
7. Flexibility and efficiency of use
8. Aesthetic and minimalist design
9. Help users recognize, diagnose, and recover from errors
10. Help and documentation

---

## 🔗 References

- **moai-adk:** ui-ux-expert agent structure
- **Design Thinking:** Stanford d.school methodology
- **JTBD:** Clayton Christensen framework
- **Nielsen Norman Group:** UX research y best practices
- **Material Design:** Google's design system
- **WCAG 2.1:** Accessibility guidelines integration

---

**Plan Version:** 1.0.0
**Status:** Draft
**Next:** Create acceptance.md
**Estimated Start:** 2025-11-24
**Estimated Complete:** 2025-11-30 (6 días)
