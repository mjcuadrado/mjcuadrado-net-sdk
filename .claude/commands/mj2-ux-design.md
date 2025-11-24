---
name: mj2-ux-design
description: Generate UX design artifacts using ui-ux-expert agent
agent: ui-ux-expert
version: 1.0.0
author: mjcuadrado-net-sdk
tags: [mj2, ux, design, research, wireframes, prototyping, usability]
---

# /mj2:ux-design - UX Design Artifacts Generator

Genera UX design artifacts usando **ui-ux-expert** agent que ejecuta workflow RESEARCH → DESIGN → PROTOTYPE → TEST.

---

## 🎯 Purpose

Crear experiencias de usuario excepcionales basadas en user research, Design Thinking methodology y usability testing.

**Supports:**
- User personas y research
- Information architecture
- User journey maps
- Wireframes y layouts
- Prototyping recommendations
- Usability test plans

---

## 📋 Usage

```bash
/mj2:ux-design <feature> [OPTIONS]
```

### Arguments

**`<feature>`** (required)
- Name of feature/flow to design
- Examples: "user-onboarding", "dashboard", "payment-flow"

### Options

**`--research`**
- Run RESEARCH phase only
- Output: User personas, pain points

**`--journey`**
- Create user journey map
- Requires: User persona (existing or created)

**`--wireframe`**
- Create wireframes y layout guidelines
- Output: Wireframe guidelines, component placement

**`--test`**
- Create usability test plan
- Requires: Design artifacts (wireframes or prototype)

**`--full`** (default)
- Run all phases: RESEARCH → DESIGN → PROTOTYPE → TEST
- Complete UX artifact package

---

## 🔄 Actions

### Action 1: Full UX Design (Default)

**Command:**
```bash
/mj2:ux-design user-onboarding
```

**What it does:**
1. **RESEARCH** - Create user personas, identify pain points
2. **DESIGN** - Create IA, journey maps, wireframes
3. **PROTOTYPE** - Recommend fidelity, tools, design tokens
4. **TEST** - Create usability test plan

**Output:**
```
✅ UX Design completado: User Onboarding

📊 Artifacts Generated:

RESEARCH:
- User Personas: 2 personas (Developer Diego, Designer Dana)
- Pain Points: 5 critical issues identified
- Research Summary: Based on 8 interviews + analytics

DESIGN:
- Information Architecture: Sitemap con 3 main sections
- User Journey Map: 4 stages (Discover → Try → Use → Recommend)
- Wireframes: Dashboard wireframe (medium fidelity)

PROTOTYPE:
- Recommended Fidelity: Medium (interactive wireframes)
- Tool: Figma (collaborative, dev handoff)
- Design Tokens: Colors, spacing, typography exported

TEST:
- Usability Test Plan: 5 scenarios, 6 participants
- Success Metrics: 90% completion, <5min, SUS >70
- Test Script: Complete facilitator guide

📂 Files Created:
- .claude/templates/ux/user-persona-diego.md
- .claude/templates/ux/user-journey-onboarding.md
- .claude/templates/ux/wireframe-dashboard.md
- .claude/templates/ux/usability-test-onboarding.md

🤖 Mr. mj2 recomienda:
1. Review personas con stakeholders
2. Validate journey map con 2-3 users
3. Create Figma prototype based on wireframes:
   /mj2:design user-onboarding --wireframes=wireframe-dashboard.md
4. Run usability test (5-8 users)
5. Iterate based on feedback

💡 Tip: "Test early, test often. 5 users find 80% of usability issues."

Next Steps:
→ component-designer: /mj2:design user-onboarding --mode=component-first
→ accessibility-expert: /mj2:accessibility-check wireframe-dashboard.md
→ frontend-builder: /mj2:2-run SPEC-ONB-001 --frontend
```

---

### Action 2: Research Only

**Command:**
```bash
/mj2:ux-design payment-flow --research
```

**What it does:**
1. Plan user research (interviews, surveys)
2. Create user personas template
3. Identify pain points y opportunities

**Output:**
```
✅ Research completada: Payment Flow

📊 User Personas Created:

1. **Buyer Betty** (Primary)
   - Age: 28, E-commerce Manager
   - Goals: Fast checkout, secure payment, order tracking
   - Pain Points: Complex forms, no saved payment, unclear shipping
   - Tools: Shopify, PayPal, Stripe

2. **Merchant Mike** (Secondary)
   - Age: 35, Small Business Owner
   - Goals: Low fees, fast deposits, fraud prevention
   - Pain Points: High transaction fees, slow payouts
   - Tools: Square, QuickBooks

📋 Top Pain Points:
1. **Checkout too many steps** (Impact: High, Frequency: Every order)
   - Opportunity: One-page checkout
2. **No guest checkout** (Impact: High, Frequency: 60% drop-off)
   - Opportunity: Guest checkout option
3. **Unclear shipping costs** (Impact: Medium, Frequency: Cart abandonment)
   - Opportunity: Show shipping upfront

📂 Files Created:
- .claude/templates/ux/user-persona-betty.md
- .claude/templates/ux/user-persona-mike.md
- pain-points-payment-flow.md

🤖 Mr. mj2 recomienda:
1. Validate personas con 3-5 users
2. Prioritize pain points: Focus en #1 y #2 (High impact)
3. Next step: Create journey map
   /mj2:ux-design payment-flow --journey

💡 Tip: "Don't assume. Interview 5-8 users to validate personas."
```

---

### Action 3: Journey Map

**Command:**
```bash
/mj2:ux-design checkout --journey
```

**What it does:**
1. Create user journey map (4 stages)
2. Map actions, emotions, touchpoints per stage
3. Identify pain points y opportunities

**Output:**
```
✅ User Journey Map: Checkout Flow

📊 Journey Overview:

**Persona:** Buyer Betty
**Goal:** Complete purchase in <3 minutes
**Stages:** Discover → Try → Use → Recommend

┌─────────────────────────────────────┐
│  Emotion Journey                    │
│ 10│        ⭐ (Use: Success!)       │
│   │      /   \                      │
│  8│    /       \  😊 (Recommend)    │
│   │  /           \                  │
│  6│😐 (Try)        \                │
│   │               😐 (Discover)     │
│  4│                                 │
│  0└────────────────────────────────>│
│     Discover  Try  Use  Recommend   │
└─────────────────────────────────────┘

**Critical Pain Points:**
1. **Form validation unclear** (Stage: Try)
   - 4/5 users confused by error messages
   - Priority: P0 (Fix immediately)
   - Solution: Inline validation + clear error text

2. **No progress indicator** (Stage: Try)
   - Users don't know how many steps left
   - Priority: P1 (Fix in sprint)
   - Solution: Add step indicator (Step 1 of 3)

**Top Opportunities:**
1. **Auto-fill from profile** (Stage: Try → Use)
   - Save 2 minutes, increase completion 20%
   - ROI: High
2. **One-click reorder** (Stage: Recommend)
   - Increase repeat purchases
   - ROI: Medium

📂 Files Created:
- .claude/templates/ux/user-journey-checkout.md

🤖 Mr. mj2 recomienda:
1. Fix P0 issues before launch (form validation)
2. Implement auto-fill (quick win, high ROI)
3. Next step: Create wireframes
   /mj2:ux-design checkout --wireframe

💡 Tip: "Focus on pain points with High Impact + High Frequency first."
```

---

### Action 4: Wireframes

**Command:**
```bash
/mj2:ux-design dashboard --wireframe
```

**What it does:**
1. Select layout pattern
2. Define component placement
3. Design responsive breakpoints
4. Create wireframe guidelines

**Output:**
```
✅ Wireframe Guidelines: Dashboard

📐 Layout Pattern: Dashboard

### Desktop (≥1200px)
┌─────────────────────────────────────┐
│  HEADER + USER MENU                 │
├─────┬───────────────────────────────┤
│ Nav │  Metric 1  │  Metric 2  │ Met3│
│     ├───────────────────────────────┤
│ S1  │                               │
│ S2  │      MAIN CHART/GRAPH         │
│ S3  │                               │
│ S4  ├───────────────┬───────────────┤
│     │  Table/List   │  Side Widget  │
└─────┴───────────────┴───────────────┘

### Mobile (≤767px)
┌─────────────┐
│ ☰  [User] ⚙│
├─────────────┤
│ Metric 1    │
├─────────────┤
│ Metric 2    │
├─────────────┤
│ Chart       │
│             │
├─────────────┤
│ Recent      │
│ Activity    │
└─────────────┘

📊 Component Placement:

| Component | Location | Priority | Responsive |
|-----------|----------|----------|------------|
| Metrics | Top row | High | Stack on mobile |
| Main Chart | Center | High | Full width |
| Activity List | Bottom left | Medium | Below chart |
| Quick Actions | Bottom right | Low | Bottom |

♿ Accessibility Checkpoints:
✅ Touch targets ≥44x44px (mobile)
✅ Color contrast ≥4.5:1
✅ Keyboard navigation
✅ Heading hierarchy (H1 → H2 → H3)
✅ Skip to main content link

📂 Files Created:
- .claude/templates/ux/wireframe-dashboard.md

🤖 Mr. mj2 recomienda:
1. Review wireframe con team
2. Next step: Handoff to component-designer
   /mj2:design dashboard --wireframes=wireframe-dashboard.md
3. Run accessibility check:
   /mj2:accessibility-check wireframe-dashboard.md
4. Create Figma prototype (medium fidelity)

💡 Tip: "Mobile-first design: Start con mobile layout, expand to desktop."
```

---

### Action 5: Test Plan

**Command:**
```bash
/mj2:ux-design profile-settings --test
```

**What it does:**
1. Create usability test plan
2. Define test scenarios (5+)
3. Specify success metrics
4. Generate test script

**Output:**
```
✅ Usability Test Plan: Profile Settings

📋 Test Overview:

**Feature:** Profile Settings
**Participants:** 6 users
**Method:** Moderated remote (Zoom)
**Duration:** 45 minutes per session
**Date:** 2025-12-01

🎯 Objectives:
1. Validate users can update email (<2 min)
2. Test discoverability of privacy settings
3. Measure satisfaction (SUS >70)

📊 Test Scenarios:

**Scenario 1:** Update Email Address
- Task: "Change your email to newemail@example.com"
- Expected time: 1-2 minutes
- Success: Email updated, confirmation received

**Scenario 2:** Change Password
- Task: "Update your password for security"
- Expected time: 2-3 minutes
- Success: Password changed, re-login works

**Scenario 3:** Upload Profile Photo
- Task: "Add a profile photo"
- Expected time: 2-3 minutes
- Success: Photo uploaded, preview shown

**Scenario 4:** Enable Two-Factor Auth
- Task: "Secure your account with 2FA"
- Expected time: 3-4 minutes
- Success: 2FA enabled, backup codes saved

**Scenario 5:** Delete Account
- Task: "Find where to delete your account"
- Expected time: 1-2 minutes
- Success: Found delete option (don't actually delete)

📈 Success Metrics:

| Metric | Target | Threshold |
|--------|--------|-----------|
| Completion Rate | 90% | 80% |
| Time on Task | <3 min avg | <5 min |
| Error Rate | <10% | <20% |
| SUS Score | >70 | >60 |
| Satisfaction | 4/5 avg | 3/5 |

📂 Files Created:
- .claude/templates/ux/usability-test-profile-settings.md

🤖 Mr. mj2 recomienda:
1. Recruit 6 participants (target users)
2. Schedule tests (1 hour slots, 2/day max)
3. Run pilot test first (validate script)
4. Test early: Don't wait for pixel-perfect design
5. Record sessions (screen + audio)
6. Analyze results:
   - Quantitative: Completion, time, errors, SUS
   - Qualitative: Themes, quotes, pain points
7. Iterate based on feedback

💡 Tip: "5 users find 80% of usability issues. Don't over-recruit."

Next Steps:
→ Run tests with 5-8 users
→ Analyze results (quantitative + qualitative)
→ Prioritize issues (severity × frequency)
→ Iterate design
→ Re-test critical changes
```

---

## 🎨 Examples

### Example 1: E-commerce Checkout

```bash
/mj2:ux-design checkout --full
```

**Use Case:** Design complete checkout flow para e-commerce site.

**Outputs:**
- User personas (Buyer Betty, Guest Gary)
- Journey map (4 stages: Cart → Info → Payment → Confirmation)
- Wireframes (One-page checkout layout)
- Prototype recommendation (High-fi Figma)
- Test plan (5 scenarios, 8 users)

---

### Example 2: SaaS Dashboard

```bash
/mj2:ux-design analytics-dashboard --wireframe
```

**Use Case:** Create dashboard wireframes para analytics SaaS.

**Outputs:**
- Layout pattern (Dashboard)
- Component placement (Metrics, charts, tables)
- Responsive breakpoints (Desktop, tablet, mobile)
- Accessibility checkpoints

---

### Example 3: Mobile App Onboarding

```bash
/mj2:ux-design onboarding --research
/mj2:ux-design onboarding --journey
/mj2:ux-design onboarding --wireframe
/mj2:ux-design onboarding --test
```

**Use Case:** Iterative UX design para mobile onboarding.

**Workflow:**
1. Research → Personas + pain points
2. Journey → Map onboarding stages
3. Wireframes → Mobile-first layouts
4. Test → Validate con users

---

## 🔗 Integration

### With component-designer

After wireframes:
```bash
/mj2:design {{feature}} --wireframes={{wireframe_file}} --mode=component-first
```

### With accessibility-expert

Check WCAG compliance:
```bash
/mj2:accessibility-check {{wireframe_file}} --wcag-level=AA
```

### With frontend-builder

Implement design:
```bash
/mj2:2-run {{SPEC_ID}} --frontend --components={{component_list}}
```

### With spec-builder

Include UX requirements:
```bash
/mj2:1-plan {{feature}} --include-ux-requirements
```

---

## 📚 Related Commands

- `/mj2:design` - Component design (after wireframes)
- `/mj2:accessibility-check` - WCAG validation
- `/mj2:2-run` - Implement con TDD
- `/mj2:1-plan` - Create SPEC con UX requirements

---

## 🐛 Troubleshooting

### Issue: "No personas to base journey on"

**Solution:**
```bash
# First create personas
/mj2:ux-design {{feature}} --research

# Then create journey map
/mj2:ux-design {{feature}} --journey
```

---

### Issue: "Wireframes too generic"

**Solution:**
- Provide more context about feature
- Reference existing design system
- Include user personas in prompt
- Specify device target (mobile, desktop, both)

---

### Issue: "Test plan scenarios not realistic"

**Solution:**
- Base scenarios en user journey map
- Use real user quotes/pain points
- Make tasks goal-oriented (not feature tours)
- Include edge cases

---

**Agent:** ui-ux-expert
**Version:** 1.0.0
**Created:** 2025-11-24
**Tags:** @UX-061
