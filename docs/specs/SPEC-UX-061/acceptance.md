---
spec_id: SPEC-UX-061
title: UI/UX Expert Agent - Acceptance Criteria
domain: DESIGN
complexity: high
status: draft
created: 2025-11-24
version: 1.0.0
---

# SPEC-UX-061: Acceptance Criteria

## 📋 Overview

Este documento define los criterios de aceptación para verificar que el UI/UX Expert Agent cumple con todos los requisitos especificados en SPEC-UX-061.

**Verification Strategy:**
- Manual testing de agent outputs
- Template validation
- Integration testing con otros agents
- Quality checklist

---

## ✅ Acceptance Test Categories

### 1. Agent Structure & Metadata

**AC-1.1: Agent File Exists**
```bash
# Verification
test -f .claude/agents/mj2/ui-ux-expert.md && echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** File exists

---

**AC-1.2: Agent Size**
```bash
# Verification
LINES=$(wc -l < .claude/agents/mj2/ui-ux-expert.md)
if [ $LINES -ge 700 ] && [ $LINES -le 850 ]; then
    echo "✅ PASS: $LINES líneas"
else
    echo "❌ FAIL: $LINES líneas (expected 700-850)"
fi
```

**Expected:** ~750 líneas (±100)

---

**AC-1.3: Agent Metadata Complete**
```bash
# Verification
grep -q "^name: ui-ux-expert$" .claude/agents/mj2/ui-ux-expert.md && \
grep -q "^description:" .claude/agents/mj2/ui-ux-expert.md && \
grep -q "^version: 1.0.0$" .claude/agents/mj2/ui-ux-expert.md && \
grep -q "^author: mjcuadrado-net-sdk$" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** All metadata fields present

---

### 2. Workflow Phases

**AC-2.1: RESEARCH Phase Exists**
```bash
# Verification
grep -q "## .*RESEARCH" .claude/agents/mj2/ui-ux-expert.md && \
grep -q "User research" .claude/agents/mj2/ui-ux-expert.md && \
grep -q "Persona" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** RESEARCH phase documented con user research, personas, pain points

---

**AC-2.2: DESIGN Phase Exists**
```bash
# Verification
grep -q "## .*DESIGN" .claude/agents/mj2/ui-ux-expert.md && \
grep -q "Information architecture" .claude/agents/mj2/ui-ux-expert.md && \
grep -q "Journey map" .claude/agents/mj2/ui-ux-expert.md && \
grep -q "Wireframe" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** DESIGN phase con IA, journey maps, wireframes

---

**AC-2.3: PROTOTYPE Phase Exists**
```bash
# Verification
grep -q "## .*PROTOTYPE" .claude/agents/mj2/ui-ux-expert.md && \
grep -q -i "fidelity" .claude/agents/mj2/ui-ux-expert.md && \
grep -q -i "interactive" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** PROTOTYPE phase con fidelity levels, interactivity

---

**AC-2.4: TEST Phase Exists**
```bash
# Verification
grep -q "## .*TEST" .claude/agents/mj2/ui-ux-expert.md && \
grep -q "Usability testing" .claude/agents/mj2/ui-ux-expert.md && \
grep -q -i "metrics" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** TEST phase con usability testing, metrics

---

### 3. Command Implementation

**AC-3.1: Command File Exists**
```bash
# Verification
test -f .claude/commands/mj2-ux-design.md && echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** File exists

---

**AC-3.2: Command Size**
```bash
# Verification
LINES=$(wc -l < .claude/commands/mj2-ux-design.md)
if [ $LINES -ge 180 ] && [ $LINES -le 250 ]; then
    echo "✅ PASS: $LINES líneas"
else
    echo "❌ FAIL: $LINES líneas (expected 180-250)"
fi
```

**Expected:** ~200 líneas (±30)

---

**AC-3.3: Command Syntax Documented**
```bash
# Verification
grep -q "/mj2:ux-design" .claude/commands/mj2-ux-design.md && \
grep -q "## .*Syntax\|## .*Usage" .claude/commands/mj2-ux-design.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Command syntax clearly documented

---

**AC-3.4: Command Options Documented**
```bash
# Verification
grep -q "\-\-research\|\-\-journey\|\-\-wireframe\|\-\-test" .claude/commands/mj2-ux-design.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** At least 3 command options documented

---

**AC-3.5: Examples Provided**
```bash
# Verification
EXAMPLES=$(grep -c "## .*Example" .claude/commands/mj2-ux-design.md)
if [ $EXAMPLES -ge 3 ]; then
    echo "✅ PASS: $EXAMPLES examples"
else
    echo "❌ FAIL: $EXAMPLES examples (expected ≥3)"
fi
```

**Expected:** ≥3 examples

---

### 4. Templates

**AC-4.1: User Persona Template Exists**
```bash
# Verification
test -f .claude/templates/ux/user-persona.md && echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Template file exists

---

**AC-4.2: User Persona Template Complete**
```bash
# Verification
grep -q "{{name}}" .claude/templates/ux/user-persona.md && \
grep -q "Goals" .claude/templates/ux/user-persona.md && \
grep -q "Pain Points" .claude/templates/ux/user-persona.md && \
grep -q "Behaviors" .claude/templates/ux/user-persona.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Template con variables y sections estándar

---

**AC-4.3: User Journey Template Exists**
```bash
# Verification
test -f .claude/templates/ux/user-journey.md && echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Template file exists

---

**AC-4.4: User Journey Template Complete**
```bash
# Verification
grep -q "Discover\|Try\|Use\|Recommend" .claude/templates/ux/user-journey.md && \
grep -q "Actions\|Emotions\|Touchpoints" .claude/templates/ux/user-journey.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Journey stages y components documented

---

**AC-4.5: Wireframe Guidelines Template Exists**
```bash
# Verification
test -f .claude/templates/ux/wireframe-guidelines.md && echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Template file exists

---

**AC-4.6: Wireframe Guidelines Complete**
```bash
# Verification
grep -q "Layout\|Component placement\|Responsive" .claude/templates/ux/wireframe-guidelines.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Guidelines con layouts, components, responsiveness

---

**AC-4.7: Usability Test Plan Template Exists**
```bash
# Verification
test -f .claude/templates/ux/usability-test-plan.md && echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Template file exists

---

**AC-4.8: Usability Test Plan Complete**
```bash
# Verification
grep -q "Objectives\|Participants\|Scenarios\|Metrics" .claude/templates/ux/usability-test-plan.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Test plan con objectives, scenarios, metrics

---

### 5. Integration Points

**AC-5.1: component-designer Integration**
```bash
# Verification
grep -q "component-designer" .claude/agents/mj2/ui-ux-expert.md && \
grep -q -i "handoff" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Integration con component-designer documented

---

**AC-5.2: accessibility-expert Integration**
```bash
# Verification
grep -q "accessibility-expert" .claude/agents/mj2/ui-ux-expert.md && \
grep -q "WCAG\|accessibility" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Accessibility checkpoints integration

---

**AC-5.3: frontend-builder Integration**
```bash
# Verification
grep -q "frontend-builder" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Implementation handoff documented

---

### 6. UX Methodologies

**AC-6.1: Design Thinking Referenced**
```bash
# Verification
grep -q -i "design thinking\|empathize\|define\|ideate" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Design Thinking methodology referenced

---

**AC-6.2: Jobs-to-be-Done Referenced**
```bash
# Verification
grep -q -i "jobs.*to.*be.*done\|JTBD\|functional job\|emotional job" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** JTBD framework referenced

---

**AC-6.3: Nielsen's Heuristics Referenced**
```bash
# Verification
grep -q -i "nielsen\|heuristics\|usability principles" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Usability heuristics referenced

---

### 7. Output Format

**AC-7.1: "Mr. mj2 recomienda" Format**
```bash
# Verification
grep -q "Mr\. mj2 recomienda\|🤖 Mr\. mj2" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Outputs con "Mr. mj2 recomienda" format

---

**AC-7.2: Spanish Language**
```bash
# Verification
# Check that Spanish content exists (some technical terms in English are OK)
grep -q "Usuario\|Diseño\|Investigación\|Prototipo" .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Content primarily en español

---

**AC-7.3: Markdown Formatting**
```bash
# Verification
grep -q "^# \|^## \|^### " .claude/agents/mj2/ui-ux-expert.md && \
grep -q "^\- \|^1\. " .claude/agents/mj2/ui-ux-expert.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Proper markdown formatting (headers, lists)

---

### 8. Documentation Updates

**AC-8.1: README.md Updated**
```bash
# Verification
grep -q "ui-ux-expert\|UI/UX Expert" README.md && \
grep -q "/mj2:ux-design" README.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** README updated con agent y command

---

**AC-8.2: Agent Count Updated**
```bash
# Verification
grep -q "26 agentes\|26 agents" README.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Agent count incremented

---

**AC-8.3: Command Count Updated**
```bash
# Verification
grep -q "26 comandos\|26 commands" README.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Command count incremented

---

**AC-8.4: ROADMAP.md Updated**
```bash
# Verification
grep -q "Issue #61.*COMPLETADO\|Issue #61.*✅" docs/ROADMAP.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Issue #61 marked as COMPLETADO

---

**AC-8.5: CHANGELOG.md Updated**
```bash
# Verification
grep -q "Issue #61\|UI/UX Expert Agent" CHANGELOG.md && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** CHANGELOG con Issue #61 entry

---

### 9. Git History & Tags

**AC-9.1: Feature Branch Exists**
```bash
# Verification
git branch -a | grep -q "feature/SPEC-UX-061" && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** Feature branch created

---

**AC-9.2: SPEC Commit with TAG**
```bash
# Verification
git log --all --grep="@SPEC:UX-061" --oneline | grep -q "@SPEC:UX-061" && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** SPEC commit con @SPEC:UX-061 tag

---

**AC-9.3: CODE Commit with TAG**
```bash
# Verification
git log --all --grep="@CODE:UX-061" --oneline | grep -q "@CODE:UX-061" && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** CODE commit con @CODE:UX-061 tag

---

**AC-9.4: DOC Commit with TAG**
```bash
# Verification
git log --all --grep="@DOC:UX-061" --oneline | grep -q "@DOC:UX-061" && \
echo "✅ PASS" || echo "❌ FAIL"
```

**Expected:** DOC commit con @DOC:UX-061 tag

---

**AC-9.5: TAG Chain Complete**
```bash
# Verification
git log --all --oneline | grep -q "@SPEC:UX-061" && \
git log --all --oneline | grep -q "@CODE:UX-061" && \
git log --all --oneline | grep -q "@DOC:UX-061" && \
echo "✅ PASS: TAG chain complete" || echo "❌ FAIL"
```

**Expected:** Full TAG chain @SPEC → @CODE → @DOC

---

### 10. Functional Requirements Coverage

**AC-10.1: FR-1 User Research (AC met)**

Manual verification:
- [ ] Agent provides user research guidelines
- [ ] Interview templates available
- [ ] Survey design templates available
- [ ] Persona generation from research data
- [ ] Pain points identification logic

**Expected:** User research capabilities complete

---

**AC-10.2: FR-2 Information Architecture (AC met)**

Manual verification:
- [ ] Sitemap generation logic
- [ ] Navigation structure recommendations
- [ ] Content hierarchy guidance
- [ ] Labeling strategy

**Expected:** IA design capabilities complete

---

**AC-10.3: FR-3 User Journey Mapping (AC met)**

Manual verification:
- [ ] Journey stages defined (4+ stages)
- [ ] Actions per stage
- [ ] Emotions mapping
- [ ] Touchpoints identification
- [ ] Pain points y opportunities

**Expected:** Journey mapping complete

---

**AC-10.4: FR-4 Wireframing Guidelines (AC met)**

Manual verification:
- [ ] Layout patterns (5+ patterns)
- [ ] Component placement rules
- [ ] Responsive design considerations
- [ ] Accessibility checkpoints en wireframes

**Expected:** Wireframing guidelines complete

---

**AC-10.5: FR-5 Interaction Design (AC met)**

Manual verification:
- [ ] User flows (happy path + edge cases)
- [ ] Interaction patterns documented
- [ ] Micro-interactions specifications
- [ ] Animation guidelines
- [ ] Feedback mechanisms

**Expected:** Interaction design specs complete

---

**AC-10.6: FR-6 Prototype Recommendations (AC met)**

Manual verification:
- [ ] Fidelity levels defined (low, medium, high)
- [ ] Tool recommendations (3+ tools)
- [ ] Interactive prototype guidelines
- [ ] Testing scenarios

**Expected:** Prototyping recommendations complete

---

**AC-10.7: FR-7 Usability Testing (AC met)**

Manual verification:
- [ ] Test objectives framework
- [ ] Participant criteria guidelines
- [ ] Test scenarios (5+ scenarios template)
- [ ] Success metrics (SUS, completion, time)
- [ ] Testing script template
- [ ] Analysis framework

**Expected:** Usability testing plan complete

---

**AC-10.8: FR-8 Integration (AC met)**

Manual verification:
- [ ] component-designer handoff format
- [ ] accessibility-expert checkpoints
- [ ] frontend-builder specs format
- [ ] spec-builder requirements integration

**Expected:** Integration con 3+ agents documented

---

### 11. Non-Functional Requirements

**AC-11.1: NFR-1 Design Quality (AC met)**

Manual verification:
- [ ] UX best practices referenced
- [ ] Design Thinking methodology
- [ ] Nielsen's heuristics referenced
- [ ] Design system consistency

**Expected:** Design quality standards documented

---

**AC-11.2: NFR-2 Deliverable Clarity (AC met)**

Manual verification:
- [ ] Outputs clear and actionable
- [ ] Non-designers can understand
- [ ] Handoffs sin ambigüedad
- [ ] Examples provided

**Expected:** Clarity standards met

---

**AC-11.3: NFR-3 Template Reusability (AC met)**

Manual verification:
- [ ] Templates reusable across projects
- [ ] Variables para customization
- [ ] Clear instructions
- [ ] Examples en templates

**Expected:** Templates reusable y customizable

---

**AC-11.4: NFR-4 Agent Performance (AC met)**

Manual verification:
- [ ] Agent responses documented
- [ ] Outputs en español
- [ ] Memory efficient design
- [ ] Performance considerations

**Expected:** Performance standards met

---

## 📊 Verification Summary

### Automated Checks

Run all automated checks:

```bash
#!/bin/bash
# verification-script.sh

echo "🔍 Running SPEC-UX-061 Verification..."
echo ""

PASS=0
FAIL=0

# AC-1.1: Agent File Exists
if test -f .claude/agents/mj2/ui-ux-expert.md; then
    echo "✅ AC-1.1: Agent file exists"
    ((PASS++))
else
    echo "❌ AC-1.1: Agent file missing"
    ((FAIL++))
fi

# AC-1.2: Agent Size
LINES=$(wc -l < .claude/agents/mj2/ui-ux-expert.md 2>/dev/null || echo 0)
if [ $LINES -ge 700 ] && [ $LINES -le 850 ]; then
    echo "✅ AC-1.2: Agent size OK ($LINES líneas)"
    ((PASS++))
else
    echo "❌ AC-1.2: Agent size out of range ($LINES líneas, expected 700-850)"
    ((FAIL++))
fi

# AC-3.1: Command File Exists
if test -f .claude/commands/mj2-ux-design.md; then
    echo "✅ AC-3.1: Command file exists"
    ((PASS++))
else
    echo "❌ AC-3.1: Command file missing"
    ((FAIL++))
fi

# AC-4.1-4.7: Templates Exist
TEMPLATES=("user-persona.md" "user-journey.md" "wireframe-guidelines.md" "usability-test-plan.md")
for template in "${TEMPLATES[@]}"; do
    if test -f ".claude/templates/ux/$template"; then
        echo "✅ Template exists: $template"
        ((PASS++))
    else
        echo "❌ Template missing: $template"
        ((FAIL++))
    fi
done

# AC-8.1-8.3: Documentation Updated
if grep -q "ui-ux-expert\|UI/UX Expert" README.md 2>/dev/null; then
    echo "✅ AC-8.1: README.md mentions ui-ux-expert"
    ((PASS++))
else
    echo "❌ AC-8.1: README.md not updated"
    ((FAIL++))
fi

if grep -q "26 agentes\|26 agents" README.md 2>/dev/null; then
    echo "✅ AC-8.2: README.md agent count updated"
    ((PASS++))
else
    echo "⚠️  AC-8.2: README.md agent count may need update"
fi

if grep -q "26 comandos\|26 commands" README.md 2>/dev/null; then
    echo "✅ AC-8.3: README.md command count updated"
    ((PASS++))
else
    echo "⚠️  AC-8.3: README.md command count may need update"
fi

# AC-9.2-9.4: Git TAGs
if git log --all --grep="@SPEC:UX-061" --oneline 2>/dev/null | grep -q "@SPEC:UX-061"; then
    echo "✅ AC-9.2: SPEC commit with TAG found"
    ((PASS++))
else
    echo "❌ AC-9.2: SPEC commit TAG missing"
    ((FAIL++))
fi

if git log --all --grep="@CODE:UX-061" --oneline 2>/dev/null | grep -q "@CODE:UX-061"; then
    echo "✅ AC-9.3: CODE commit with TAG found"
    ((PASS++))
else
    echo "❌ AC-9.3: CODE commit TAG missing"
    ((FAIL++))
fi

if git log --all --grep="@DOC:UX-061" --oneline 2>/dev/null | grep -q "@DOC:UX-061"; then
    echo "✅ AC-9.4: DOC commit with TAG found"
    ((PASS++))
else
    echo "❌ AC-9.4: DOC commit TAG missing"
    ((FAIL++))
fi

echo ""
echo "📊 Results: $PASS passed, $FAIL failed"

if [ $FAIL -eq 0 ]; then
    echo "✅ ALL AUTOMATED CHECKS PASSED"
    exit 0
else
    echo "❌ SOME CHECKS FAILED"
    exit 1
fi
```

---

### Manual Verification Checklist

**Quality Review:**
- [ ] Agent outputs clear y professional
- [ ] Templates useful y reusable
- [ ] Integration points well-defined
- [ ] Spanish language quality high
- [ ] "Mr. mj2 recomienda" format consistent
- [ ] Examples realistic y helpful
- [ ] Documentation complete y accurate

**Functional Testing:**
- [ ] Test /mj2:ux-design con example feature
- [ ] Verify RESEARCH phase output
- [ ] Verify DESIGN phase output (IA, journey, wireframes)
- [ ] Verify PROTOTYPE phase output
- [ ] Verify TEST phase output
- [ ] Test handoff to component-designer
- [ ] Test accessibility-expert integration

---

## ✅ Definition of Done

**SPEC-UX-061 is DONE when:**

### Implementation
- [x] ui-ux-expert.md agent created (~750 líneas)
- [x] /mj2-ux-design command created (~200 líneas)
- [x] 4 UX templates created (~150 líneas)
- [x] Integration con 3+ agents documented

### Quality
- [ ] All automated checks PASS
- [ ] Manual verification complete
- [ ] Peer review approved
- [ ] No critical issues

### Documentation
- [ ] README.md updated
- [ ] ROADMAP.md updated
- [ ] CHANGELOG.md updated
- [ ] SPEC docs complete (spec, plan, acceptance)

### Git
- [ ] TAG chain complete: @SPEC:UX-061 → @CODE:UX-061 → @DOC:UX-061
- [ ] Merged to main
- [ ] Pushed to GitHub
- [ ] Issue #61 closed

---

**Acceptance Criteria Version:** 1.0.0
**Status:** Draft
**Total Checks:** 45+ (automated + manual)
**Estimated Verification Time:** 2-3 hours
