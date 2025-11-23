---
name: mj2-a11y-audit
description: Auditoría de accesibilidad WCAG 2.1 Level AA con automated y manual testing
tags: [accessibility, a11y, wcag, testing, audit]
---

# /mj2:a11y-audit - Accessibility Audit

Comando para invocar al agente **accessibility-expert** y auditar accesibilidad WCAG 2.1 Level AA.

---

## 📋 Uso

```bash
# Auditar target específico
/mj2:a11y-audit <target>

# Ejemplos
/mj2:a11y-audit dashboard       # Auditar Dashboard page
/mj2:a11y-audit product-form    # Auditar Product Form
/mj2:a11y-audit full-app        # Auditoría completa
```

---

## 🎯 ¿Qué hace este comando?

El comando **a11y-audit** ejecuta una auditoría completa de accesibilidad que incluye:

1. **AUDIT** - Testing Automatizado y Manual
   - axe-core automated testing
   - Lighthouse accessibility audit
   - Manual keyboard testing
   - Screen reader testing (NVDA/JAWS/VoiceOver)

2. **IDENTIFY** - Clasificar Issues
   - Severidad (Critical, Serious, Moderate, Minor)
   - Mapeo a WCAG 2.1 Level AA criteria
   - Priorización de fixes

3. **IMPLEMENT** - Remediación
   - Semantic HTML (landmarks, headings)
   - ARIA patterns (roles, states, properties)
   - Keyboard navigation
   - Color contrast fixes
   - Form accessibility

4. **TEST** - Validación
   - Re-run automated tests
   - Manual keyboard verification
   - Screen reader testing
   - WCAG 2.1 AA compliance validation

---

## 🔄 Workflow

```
🔍 AUDIT
  ↓ axe-core automated testing
  ↓ Lighthouse accessibility score
  ↓ Manual keyboard testing (Tab, Enter, Escape, Arrows)
  ↓ Screen reader testing (NVDA, JAWS, VoiceOver)
  ↓ Color contrast verification

🎯 IDENTIFY
  ↓ Classify: Critical | Serious | Moderate | Minor
  ↓ Map to WCAG 2.1 Level AA criteria
  ↓ Prioritize fixes (impact vs effort)
  ↓ Create remediation plan

🔧 IMPLEMENT
  ↓ Fix Critical issues first
  ↓ Semantic HTML (header, nav, main, aside, footer)
  ↓ ARIA patterns (dialog, tabs, accordion)
  ↓ Keyboard navigation (focus management, shortcuts)
  ↓ Color contrast (4.5:1 text, 3:1 UI)
  ↓ Form accessibility (labels, errors, validation)

✅ TEST
  ↓ Re-run axe-core (expect 0 violations)
  ↓ Lighthouse score > 90
  ↓ Keyboard navigation verification
  ↓ Screen reader verification
  ↓ User testing with people with disabilities
```

---

## 💡 Ejemplos de Uso

### Ejemplo 1: Auditar Dashboard

**Comando:**
```bash
/mj2:a11y-audit dashboard
```

**Output del accessibility-expert:**

**AUDIT:**
```
🔍 Automated Testing Results:

axe-core violations:
- 12 violations found
- 8 Critical, 3 Serious, 1 Moderate

Critical Issues:
1. Images missing alt text (5 instances)
   - chart.png, graph.png, logo.png, banner.png, icon.svg
   - WCAG 1.1.1 Non-text Content (Level A)

2. Form inputs missing labels (3 instances)
   - Search input (#search)
   - Filter select (#filter)
   - Date picker (#date-range)
   - WCAG 3.3.2 Labels or Instructions (Level A)

3. Insufficient color contrast (4 instances)
   - Secondary text: #999 on #FFF (2.8:1) ❌ Required: 4.5:1
   - Disabled buttons: #BBB on #EEE (1.6:1) ❌
   - WCAG 1.4.3 Contrast (Minimum) (Level AA)

Serious Issues:
1. Headings out of order (h1 → h3, missing h2)
   - WCAG 1.3.1 Info and Relationships (Level A)

2. Missing landmark regions
   - No <main> landmark
   - No <nav> for navigation
   - WCAG 2.4.1 Bypass Blocks (Level A)

3. No skip link
   - WCAG 2.4.1 Bypass Blocks (Level A)

Moderate Issues:
1. Links without context ("Click here" × 2)
   - WCAG 2.4.4 Link Purpose (In Context) (Level A)

📊 Lighthouse Accessibility Score: 64/100 ❌

⌨️ Manual Keyboard Testing:
- Tab navigation works ✅
- Some elements not reachable (dropdown menu) ❌
- Focus visible on most elements ✅
- Focus trap in modal ❌
- Skip link missing ❌

🔊 Screen Reader Testing (NVDA):
- Images announced as "image" without alt ❌
- Form inputs not labeled properly ❌
- Landmark navigation not available ❌
- Page title announced correctly ✅
```

**IDENTIFY:**
```
🎯 Issues Prioritization:

Priority 1 - Critical (Blockers):
1. Add alt text to all images (WCAG 1.1.1) - 30 min
2. Add labels to form inputs (WCAG 3.3.2) - 20 min
3. Fix color contrast (WCAG 1.4.3) - 15 min
   Total: ~1 hour

Priority 2 - Serious:
4. Fix heading hierarchy (WCAG 1.3.1) - 10 min
5. Add landmarks (header, nav, main, footer) (WCAG 2.4.1) - 20 min
6. Add skip link (WCAG 2.4.1) - 10 min
7. Fix focus trap in modal (WCAG 2.1.2) - 30 min
   Total: ~1 hour

Priority 3 - Moderate:
8. Improve link context (WCAG 2.4.4) - 15 min
9. Fix dropdown keyboard access (WCAG 2.1.1) - 30 min
   Total: ~45 min

Total estimated time: ~3 hours
```

**IMPLEMENT:**
```tsx
// 1. Fix images - Add alt text
// BEFORE:
<img src="/chart.png" />
<img src="/logo.png" />

// AFTER:
<img src="/chart.png" alt="Sales chart showing 15% growth in Q4 2025" />
<img src="/logo.png" alt="Company Logo" />

// 2. Fix forms - Add labels
// BEFORE:
<input type="text" placeholder="Search" />
<select>
  <option>All</option>
  <option>Active</option>
</select>

// AFTER:
<label htmlFor="search">Search</label>
<input id="search" type="text" placeholder="Enter search term" />

<label htmlFor="filter">Filter by status</label>
<select id="filter">
  <option>All</option>
  <option>Active</option>
</select>

// 3. Fix color contrast
// BEFORE:
const colors = {
  textSecondary: '#999999',  // 2.8:1 ❌
  disabled: '#BBBBBB',       // 1.6:1 ❌
};

// AFTER:
const colors = {
  textSecondary: '#666666',  // 5.7:1 ✅
  disabled: '#999999',       // 2.8:1 ✅ (for large text only)
};

// 4. Fix heading hierarchy
// BEFORE:
<h1>Dashboard</h1>
<h3>Recent Orders</h3>  {/* Skips h2! */}

// AFTER:
<h1>Dashboard</h1>
<h2>Recent Orders</h2>
<h3>Order Details</h3>

// 5. Add landmarks
// BEFORE:
<div className="header">...</div>
<div className="content">...</div>

// AFTER:
<header>
  <nav aria-label="Main navigation">...</nav>
</header>
<main id="main-content">
  <h1>Dashboard</h1>
  {/* Content */}
</main>
<footer>...</footer>

// 6. Add skip link
// AFTER:
<a href="#main-content" className="skip-link">
  Skip to main content
</a>

// CSS
.skip-link {
  position: absolute;
  top: -40px;
  left: 0;
  background: #000;
  color: #fff;
  padding: 8px;
  z-index: 100;
}

.skip-link:focus {
  top: 0;
}

// 7. Fix focus trap in modal
// BEFORE: No focus management

// AFTER:
function Modal({ isOpen, onClose }) {
  const modalRef = useRef<HTMLDivElement>(null);
  const previouslyFocused = useRef<HTMLElement | null>(null);

  useEffect(() => {
    if (isOpen) {
      previouslyFocused.current = document.activeElement as HTMLElement;
      modalRef.current?.focus();
    } else {
      previouslyFocused.current?.focus();
    }
  }, [isOpen]);

  const handleKeyDown = (e: KeyboardEvent) => {
    if (e.key === 'Escape') {
      onClose();
    }

    // Trap focus within modal
    if (e.key === 'Tab') {
      const focusable = modalRef.current?.querySelectorAll(
        'button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])'
      );
      // ... focus trap logic
    }
  };

  return (
    <div
      ref={modalRef}
      role="dialog"
      aria-modal="true"
      tabIndex={-1}
      onKeyDown={handleKeyDown}
    >
      {/* Modal content */}
    </div>
  );
}
```

**TEST:**
```
✅ Re-run Automated Tests:

axe-core violations: 0 ✅
Lighthouse Score: 96/100 ✅ (target: > 90)

✅ Manual Keyboard Testing:
- Tab navigation: All elements accessible ✅
- Skip link: Works correctly ✅
- Focus visible: All interactive elements ✅
- Focus trap: Modal works correctly ✅
- No keyboard traps ✅

✅ Screen Reader Testing (NVDA):
- Images: All have appropriate alt text ✅
- Forms: All inputs labeled correctly ✅
- Landmarks: Navigation works correctly ✅
- Headings: Hierarchy is correct ✅

✅ WCAG 2.1 Level AA Compliance:
- Level A: 30/30 criteria passed ✅
- Level AA: 20/20 criteria passed ✅

🎯 Result: WCAG 2.1 Level AA COMPLIANT
```

### Ejemplo 2: Auditar Product Form

**Comando:**
```bash
/mj2:a11y-audit product-form
```

**AUDIT:**
```
🔍 Issues Found:

Critical:
1. Required fields not marked (WCAG 3.3.2)
2. Error messages not associated with inputs (WCAG 3.3.1)
3. Submit button disabled with poor contrast (WCAG 1.4.3)

Serious:
4. Form validation errors not announced (WCAG 4.1.3)
5. No fieldset for related inputs (WCAG 1.3.1)

Lighthouse Score: 72/100 ❌
```

**IMPLEMENT:**
```tsx
function ProductForm() {
  const [errors, setErrors] = useState<Record<string, string>>({});

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();

    const newErrors = validate(formData);

    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);

      // Announce errors to screen readers
      const errorCount = Object.keys(newErrors).length;
      announce(`Form invalid. ${errorCount} errors found.`);

      // Focus first field with error
      const firstErrorField = document.querySelector('[aria-invalid="true"]');
      (firstErrorField as HTMLElement)?.focus();
    }
  };

  return (
    <form onSubmit={handleSubmit} noValidate>
      <fieldset>
        <legend>Product Information</legend>

        {/* Name field */}
        <div>
          <label htmlFor="name">
            Product Name
            <span aria-label="required"> *</span>
          </label>
          <input
            id="name"
            type="text"
            value={formData.name}
            onChange={(e) => setFormData({ ...formData, name: e.target.value })}
            aria-invalid={!!errors.name}
            aria-describedby={errors.name ? 'name-error' : undefined}
            aria-required="true"
          />
          {errors.name && (
            <span id="name-error" role="alert" className="error">
              {errors.name}
            </span>
          )}
        </div>

        {/* Price field */}
        <div>
          <label htmlFor="price">
            Price
            <span aria-label="required"> *</span>
          </label>
          <input
            id="price"
            type="number"
            value={formData.price}
            onChange={(e) => setFormData({ ...formData, price: e.target.value })}
            aria-invalid={!!errors.price}
            aria-describedby={errors.price ? 'price-error' : 'price-help'}
            aria-required="true"
          />
          <span id="price-help" className="help-text">
            Price in USD
          </span>
          {errors.price && (
            <span id="price-error" role="alert" className="error">
              {errors.price}
            </span>
          )}
        </div>

        <button type="submit">Save Product</button>
      </fieldset>

      {/* Live region for announcements */}
      <div role="status" aria-live="polite" aria-atomic="true" className="sr-only">
        {/* Announcements */}
      </div>
    </form>
  );
}
```

**TEST:**
```
✅ axe-core: 0 violations
✅ Lighthouse: 94/100
✅ Form validation errors announced correctly
✅ Required fields marked appropriately
✅ Error messages associated with inputs
```

---

## 📐 WCAG 2.1 Level AA Checklist

### Perceivable
- [ ] 1.1.1 Non-text Content (alt text)
- [ ] 1.3.1 Info and Relationships (semantic HTML)
- [ ] 1.3.2 Meaningful Sequence
- [ ] 1.3.4 Orientation
- [ ] 1.4.1 Use of Color
- [ ] 1.4.3 Contrast (Minimum) 4.5:1 text, 3:1 UI
- [ ] 1.4.4 Resize Text
- [ ] 1.4.10 Reflow
- [ ] 1.4.11 Non-text Contrast 3:1
- [ ] 1.4.12 Text Spacing
- [ ] 1.4.13 Content on Hover or Focus

### Operable
- [ ] 2.1.1 Keyboard
- [ ] 2.1.2 No Keyboard Trap
- [ ] 2.1.4 Character Key Shortcuts
- [ ] 2.4.1 Bypass Blocks (skip links)
- [ ] 2.4.2 Page Titled
- [ ] 2.4.3 Focus Order
- [ ] 2.4.4 Link Purpose (In Context)
- [ ] 2.4.5 Multiple Ways
- [ ] 2.4.6 Headings and Labels
- [ ] 2.4.7 Focus Visible
- [ ] 2.5.1 Pointer Gestures
- [ ] 2.5.2 Pointer Cancellation
- [ ] 2.5.3 Label in Name
- [ ] 2.5.4 Motion Actuation

### Understandable
- [ ] 3.1.1 Language of Page
- [ ] 3.1.2 Language of Parts
- [ ] 3.2.1 On Focus
- [ ] 3.2.2 On Input
- [ ] 3.2.3 Consistent Navigation
- [ ] 3.2.4 Consistent Identification
- [ ] 3.3.1 Error Identification
- [ ] 3.3.2 Labels or Instructions
- [ ] 3.3.3 Error Suggestion
- [ ] 3.3.4 Error Prevention

### Robust
- [ ] 4.1.1 Parsing
- [ ] 4.1.2 Name, Role, Value
- [ ] 4.1.3 Status Messages

---

## 🛠️ Herramientas Utilizadas

### Automated Testing
```bash
# axe-core
npm install --save-dev @axe-core/react jest-axe

# Playwright accessibility
npm install --save-dev @axe-core/playwright

# Lighthouse CI
npm install -g @lhci/cli
lhci autorun
```

### Screen Readers
- **NVDA (Windows):** https://www.nvaccess.org/ (Free)
- **JAWS (Windows):** https://www.freedomscientific.com/ (Paid)
- **VoiceOver (macOS):** Built-in (Cmd + F5)
- **TalkBack (Android):** Built-in

### Browser Extensions
- **axe DevTools:** https://www.deque.com/axe/devtools/
- **WAVE:** https://wave.webaim.org/extension/
- **Lighthouse:** Chrome DevTools (F12 → Lighthouse)

---

## 🔗 Integración con Workflow

```bash
# 1. Implementar feature (tdd-implementer)
/mj2:2-run FEATURE-001

# 2. Accessibility audit (accessibility-expert) ← ESTE COMANDO
/mj2:a11y-audit product-form

# 3. Performance analysis (performance-engineer)
/mj2:perf-analyze frontend

# 4. Security review (security-expert)
# Verificar que a11y no comprometa security

# 5. E2E testing (e2e-tester)
/mj2:4-e2e FEATURE-E2E-001

# 6. Deploy (devops-expert)
/mj2:5-deploy staging
```

---

## ✅ Checklist de Salida

Después de ejecutar `/mj2:a11y-audit`, verifica:

### Automated Testing
- [ ] axe-core: 0 violations
- [ ] Lighthouse accessibility score > 90
- [ ] All pages pass automated testing

### Manual Testing
- [ ] Keyboard navigation completa
- [ ] Focus visible en todos los elementos
- [ ] No hay keyboard traps
- [ ] Skip links funcionan

### Semantic HTML
- [ ] Landmarks correctos (header, nav, main, aside, footer)
- [ ] Headings en orden jerárquico (h1, h2, h3...)
- [ ] Listas usadas apropiadamente

### ARIA
- [ ] Roles apropiados
- [ ] Estados y propiedades correctos
- [ ] Live regions para contenido dinámico
- [ ] aria-label/aria-labelledby donde necesario

### Forms
- [ ] Todos los campos tienen labels
- [ ] Errores identificados y descritos
- [ ] Campos requeridos marcados
- [ ] Validación accesible

### Color & Contrast
- [ ] Contraste de color > 4.5:1 (texto normal)
- [ ] Contraste de color > 3:1 (texto grande, UI components)
- [ ] No se usa solo color para transmitir información

### Screen Readers
- [ ] Todas las imágenes tienen alt text apropiado
- [ ] Live regions funcionan correctamente
- [ ] Navegación por landmarks funciona
- [ ] Formularios anunciados correctamente

---

## 📚 Ver También

- Agente: `.claude/agents/mj2/accessibility-expert.md`
- Skill relacionada:
  - `.claude/skills/frontend/accessibility.md`
- Comandos relacionados:
  - `/mj2:2f-build` - Component building
  - `/mj2:4-e2e` - E2E testing (incluye a11y tests)

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-23
**Mantenido por:** mjcuadrado-net-sdk
