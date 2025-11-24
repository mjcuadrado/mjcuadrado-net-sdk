---
name: Wireframe Guidelines Template
description: Template para crear wireframe guidelines con layouts, components, responsive
category: ux
version: 1.0.0
author: mjcuadrado-net-sdk
tags: [ux, wireframe, layout, responsive, components]
---

# Wireframe Guidelines: {{feature_name}}

**Feature:** {{feature_name}}
**Persona:** {{persona_name}}
**Device Target:** {{device_target}} _(e.g., Desktop, Mobile, Both)_
**Fidelity:** {{fidelity}} _(Low, Medium, High)_
**Date:** {{date}}

---

## 📐 Layout Pattern

**Selected Pattern:** {{layout_pattern}}

### Available Layout Patterns

#### 1. Hero Layout
```
┌─────────────────────────────────────┐
│  NAVIGATION BAR                     │
├─────────────────────────────────────┤
│                                     │
│         HERO IMAGE/VIDEO            │
│                                     │
│     [Primary CTA] [Secondary CTA]   │
│                                     │
├─────────────────────────────────────┤
│  Feature 1  │  Feature 2  │ Feature 3 │
├─────────────────────────────────────┤
│         CONTENT SECTION             │
└─────────────────────────────────────┘
```
**Use when:** Landing pages, marketing pages, feature highlights

---

#### 2. Grid Layout
```
┌─────────────────────────────────────┐
│  HEADER + FILTERS                   │
├───────────┬───────────┬─────────────┤
│           │           │             │
│  Card 1   │  Card 2   │   Card 3    │
│           │           │             │
├───────────┼───────────┼─────────────┤
│           │           │             │
│  Card 4   │  Card 5   │   Card 6    │
│           │           │             │
└───────────┴───────────┴─────────────┘
```
**Use when:** Product listings, galleries, dashboards, search results

---

#### 3. List Layout
```
┌─────────────────────────────────────┐
│  HEADER + SEARCH                    │
├─────────────────────────────────────┤
│  [Image] Item 1 Title               │
│          Brief description...       │
│          [Action] [Secondary]       │
├─────────────────────────────────────┤
│  [Image] Item 2 Title               │
│          Brief description...       │
│          [Action] [Secondary]       │
├─────────────────────────────────────┤
│  [Image] Item 3 Title               │
│          Brief description...       │
│          [Action] [Secondary]       │
└─────────────────────────────────────┘
```
**Use when:** Feeds, articles, notifications, activity logs

---

#### 4. Master-Detail Layout
```
┌──────────┬──────────────────────────┐
│          │  DETAIL HEADER           │
│  Item 1  ├──────────────────────────┤
│  Item 2  │                          │
│ >Item 3  │  DETAIL CONTENT          │
│  Item 4  │                          │
│  Item 5  │                          │
│          │  [Primary] [Secondary]   │
└──────────┴──────────────────────────┘
```
**Use when:** Email clients, settings, file managers, admin panels

---

#### 5. Dashboard Layout
```
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
```
**Use when:** Analytics, dashboards, monitoring, reports

---

## 🧩 Component Placement

### {{feature_name}} Components

| Component | Location | Priority | Size | Responsive Behavior |
|-----------|----------|----------|------|---------------------|
| {{component_1}} | {{component_1_location}} | {{component_1_priority}} | {{component_1_size}} | {{component_1_responsive}} |
| {{component_2}} | {{component_2_location}} | {{component_2_priority}} | {{component_2_size}} | {{component_2_responsive}} |
| {{component_3}} | {{component_3_location}} | {{component_3_priority}} | {{component_3_size}} | {{component_3_responsive}} |
| {{component_4}} | {{component_4_location}} | {{component_4_priority}} | {{component_4_size}} | {{component_4_responsive}} |

### Placement Rules

1. **F-Pattern Reading** (Desktop)
   - Most important content: Top-left
   - Secondary content: Horizontal scan right
   - Tertiary content: Vertical scan down

2. **Z-Pattern Scanning** (Landing pages)
   - Logo/Brand: Top-left
   - CTA/Navigation: Top-right
   - Supporting info: Middle-left
   - Primary CTA: Bottom-right

3. **Mobile-First Hierarchy**
   - Primary action: Thumb zone (bottom third)
   - Navigation: Top or bottom bar
   - Content: Scrollable middle section

---

## 📱 Responsive Breakpoints

### Desktop (≥1200px)
```
┌─────────────────────────────────────┐
│  {{desktop_layout}}                 │
│                                     │
│  [Full 3-column layout]             │
│                                     │
└─────────────────────────────────────┘
```
**Layout:** {{desktop_columns}} columns
**Max Width:** 1440px (centered)
**Sidebar:** {{desktop_sidebar}} _(Visible/Hidden)_

---

### Tablet (768px - 1199px)
```
┌───────────────────────────┐
│  {{tablet_layout}}        │
│                           │
│  [2-column layout]        │
│                           │
└───────────────────────────┘
```
**Layout:** {{tablet_columns}} columns
**Changes:** {{tablet_changes}}

---

### Mobile (≤767px)
```
┌─────────────┐
│ {{mobile}}  │
│             │
│ [1-column]  │
│             │
│             │
│             │
└─────────────┘
```
**Layout:** 1 column, stacked
**Navigation:** {{mobile_nav}} _(e.g., Hamburger menu, Bottom tabs)_
**Changes:** {{mobile_changes}}

---

## 🎨 Visual Hierarchy

### Size Hierarchy
1. **H1 (Page Title):** {{h1_size}} _(e.g., 32px/2rem)_
2. **H2 (Section Headers):** {{h2_size}} _(e.g., 24px/1.5rem)_
3. **H3 (Sub-sections):** {{h3_size}} _(e.g., 20px/1.25rem)_
4. **Body Text:** {{body_size}} _(e.g., 16px/1rem)_
5. **Secondary Text:** {{secondary_size}} _(e.g., 14px/0.875rem)_

### Spacing Scale
- **XS:** {{spacing_xs}} _(e.g., 4px)_
- **SM:** {{spacing_sm}} _(e.g., 8px)_
- **MD:** {{spacing_md}} _(e.g., 16px)_
- **LG:** {{spacing_lg}} _(e.g., 24px)_
- **XL:** {{spacing_xl}} _(e.g., 32px)_
- **XXL:** {{spacing_xxl}} _(e.g., 48px)_

### Color Usage
- **Primary Actions:** {{color_primary}}
- **Secondary Actions:** {{color_secondary}}
- **Success States:** {{color_success}}
- **Error States:** {{color_error}}
- **Neutral/Background:** {{color_neutral}}

---

## ♿ Accessibility Checkpoints

### Visual Accessibility

- [ ] **Contrast Ratio:** Text contrast ≥4.5:1 (WCAG AA)
- [ ] **Touch Targets:** Minimum 44x44px (mobile)
- [ ] **Font Size:** Body text ≥16px (mobile)
- [ ] **Color Independence:** Info not conveyed by color alone
- [ ] **Focus States:** Visible focus indicators on all interactive elements

### Structural Accessibility

- [ ] **Heading Hierarchy:** Logical H1 → H2 → H3 structure
- [ ] **Semantic HTML:** Proper use of nav, main, section, article
- [ ] **Alt Text:** Image placeholders with alt text indicators
- [ ] **Form Labels:** All inputs have associated labels
- [ ] **Skip Links:** "Skip to main content" link at top

### Keyboard Navigation

- [ ] **Tab Order:** Logical tab order follows visual flow
- [ ] **Keyboard Shortcuts:** No conflicts with screen readers
- [ ] **Escape Key:** Closes modals/dropdowns
- [ ] **Enter/Space:** Activates buttons and links
- [ ] **Arrow Keys:** Navigate lists and menus

---

## 🎯 User Flow Integration

### Entry Points
1. **{{entry_point_1}}** → Lands on {{entry_point_1_destination}}
2. **{{entry_point_2}}** → Lands on {{entry_point_2_destination}}
3. **{{entry_point_3}}** → Lands on {{entry_point_3_destination}}

### Primary User Flow
```
{{flow_step_1}}
    ↓
{{flow_step_2}}
    ↓
{{flow_step_3}}
    ↓
{{flow_step_4}} (Success)
```

### Alternative Paths
- **Path A:** {{alt_path_a}}
- **Path B:** {{alt_path_b}}
- **Error Recovery:** {{error_recovery}}

---

## 🔧 Interactive Elements

### Buttons

**Primary Button:**
```
┌───────────────────┐
│   PRIMARY CTA     │ ← High emphasis
└───────────────────┘
```
- Size: {{button_primary_size}}
- Color: {{button_primary_color}}
- Use: Main action per screen (max 1-2)

**Secondary Button:**
```
┌───────────────────┐
│   SECONDARY       │ ← Medium emphasis
└───────────────────┘
```
- Size: {{button_secondary_size}}
- Color: {{button_secondary_color}}
- Use: Alternative actions

**Tertiary/Text Button:**
```
   Tertiary Link      ← Low emphasis
```
- Style: Text link
- Use: Cancel, back, skip actions

### Form Elements

**Input Fields:**
```
┌───────────────────────────────┐
│ Label                         │
│ ┌───────────────────────────┐ │
│ │ Placeholder text...       │ │
│ └───────────────────────────┘ │
│ Helper text or error message  │
└───────────────────────────────┘
```

**Dropdown:**
```
┌─────────────────────┐
│ Select option... ▼  │
└─────────────────────┘
```

**Checkbox/Radio:**
```
☐ Option 1
☐ Option 2
☑ Option 3 (selected)
```

### Feedback Elements

**Loading State:**
```
┌─────────────────────┐
│  🔄 Loading...      │
└─────────────────────┘
```

**Success Message:**
```
┌─────────────────────┐
│  ✅ Success!        │
└─────────────────────┘
```

**Error Message:**
```
┌─────────────────────┐
│  ❌ Error occurred  │
│  [Retry] [Cancel]   │
└─────────────────────┘
```

---

## 📝 Content Guidelines

### Microcopy

| Element | Content | Character Limit |
|---------|---------|-----------------|
| **Page Title** | {{page_title}} | 60 chars |
| **Primary CTA** | {{primary_cta}} | 20 chars |
| **Helper Text** | {{helper_text}} | 100 chars |
| **Error Message** | {{error_message}} | 120 chars |
| **Success Message** | {{success_message}} | 80 chars |

### Tone of Voice
- **Style:** {{tone_style}} _(e.g., Professional, Friendly, Technical)_
- **Person:** {{tone_person}} _(e.g., First-person, Second-person)_
- **Tense:** {{tone_tense}} _(e.g., Present, Imperative)_

---

## 🔍 Handoff Notes

### For Design (component-designer)
- [ ] Design tokens needed: {{design_tokens}}
- [ ] Custom components: {{custom_components}}
- [ ] Icon requirements: {{icon_requirements}}
- [ ] Image dimensions: {{image_dimensions}}

### For Development (frontend-builder)
- [ ] Component library: {{component_library}} _(e.g., Material UI, Custom)_
- [ ] State management: {{state_management}}
- [ ] API endpoints needed: {{api_endpoints}}
- [ ] Responsive framework: {{responsive_framework}}

### For Accessibility (accessibility-expert)
- [ ] WCAG level target: {{wcag_level}} _(AA or AAA)_
- [ ] Screen reader testing: Required
- [ ] Keyboard navigation: Full support
- [ ] ARIA labels needed: {{aria_requirements}}

---

## 🎨 Example: User Profile Wireframe

```markdown
# Wireframe: User Profile Page

**Layout Pattern:** Master-Detail
**Device:** Desktop + Mobile responsive
**Fidelity:** Medium

## Desktop (1200px+)
┌─────────────────────────────────────┐
│  ☰ App Header          [Settings] 👤│
├──────────┬──────────────────────────┤
│          │  Profile Header          │
│ Sidebar  │  [@username] [Edit]      │
│          ├──────────────────────────┤
│ • About  │  📊 Stats: Projects | PRs│
│ • Repos  ├──────────────────────────┤
│ • Stars  │  Pinned Repositories     │
│ • Gists  │  ┌────┐ ┌────┐ ┌────┐   │
│          │  │Rep1│ │Rep2│ │Rep3│   │
│          │  └────┘ └────┘ └────┘   │
│          ├──────────────────────────┤
│          │  Recent Activity         │
│          │  • Committed to repo X   │
│          │  • Opened PR #123       │
└──────────┴──────────────────────────┘

## Mobile (≤767px)
┌─────────────┐
│ ☰  [User] ⚙│ ← Hamburger + Settings
├─────────────┤
│ @username   │
│ [Edit]      │
├─────────────┤
│ 📊 Stats    │
│ 50  10  5   │
├─────────────┤
│ Pinned      │
│ ┌─────────┐ │
│ │  Repo 1 │ │
│ └─────────┘ │
│ ┌─────────┐ │
│ │  Repo 2 │ │
│ └─────────┘ │
├─────────────┤
│ Activity    │
│ • Action 1  │
│ • Action 2  │
└─────────────┘

**Responsive Changes:**
- Sidebar collapses to hamburger menu
- 3-column pinned repos → 1-column stack
- Stats display horizontally
- Touch targets increased to 48px
```

---

## 📚 Related Documents

- **User Journey Map:** {{journey_map_link}}
- **User Persona:** {{persona_link}}
- **Technical Spec:** {{spec_link}}
- **Design System:** {{design_system_link}}

---

## ✅ Review Checklist

Before finalizing wireframes:

- [ ] Layout pattern appropriate for use case
- [ ] All components placed logically
- [ ] Responsive breakpoints defined
- [ ] Accessibility checkpoints met
- [ ] User flow supports goals
- [ ] Content guidelines followed
- [ ] Handoff notes complete
- [ ] Reviewed with {{reviewer_name}}

---

**Template Version:** 1.0.0
**Last Updated:** 2025-11-24
**Created by:** mjcuadrado-net-sdk
**Estimated Time:** 1-2 hours per feature wireframe
