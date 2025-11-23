---
name: mj2-design-component
description: Diseña componentes React con enfoque UX/UI y accesibilidad
tags: [frontend, design, components, ux, accessibility]
---

# /mj2:design-component - Component Designer

Comando para diseñar componentes React siguiendo **Design-First**, **WCAG 2.2 AA**, y **Best Practices**.

---

## 📋 Uso

```bash
# Diseñar componente simple
/mj2:design-component "Button con loading state"

# Diseñar componente complejo
/mj2:design-component "DataTable con sorting, filtering y pagination"

# Especificar patrón de diseño
/mj2:design-component "Select dropdown" --pattern compound-components

# Generar solo SPEC (sin análisis completo)
/mj2:design-component "Modal dialog" --spec-only
```

---

## 🎯 Workflow

El comando ejecuta el workflow **DESIGN → ANALYZE → SPEC → VALIDATE**:

### 1. **DESIGN**
- Analiza requisitos UX/UI
- Define Component API
- Diseña estados del componente
- Selecciona design pattern apropiado

### 2. **ANALYZE**
- Valida accesibilidad (WCAG 2.2 AA)
- Analiza performance
- Determina estrategia de composición
- Identifica integraciones necesarias

### 3. **SPEC**
- Genera SPEC en formato EARS
- Define design tokens
- Especifica tests requeridos
- Documenta decisiones de diseño

### 4. **VALIDATE**
- Design review checklist
- Accessibility validation
- UX validation
- Technical validation

---

## 💡 Ejemplos

### **Ejemplo 1: Button Simple**

```bash
/mj2:design-component "Button con variantes primary, secondary, tertiary"
```

**Output:**
```
🎨 DESIGN
✓ Component API:
  - Props: variant, size, disabled, loading, startIcon, endIcon, onClick
  - Variantes: primary, secondary, tertiary
  - Tamaños: small (32px), medium (40px), large (48px)
  - Estados: default, hover, focus, active, disabled, loading

🔍 ANALYZE
✓ Accessibility (WCAG 2.2 AA):
  - Keyboard: Tab, Enter, Space ✓
  - Screen reader: aria-label, aria-busy ✓
  - Color contrast: 4.5:1 ✓
  - Touch target: 44×44px mínimo ✓
  - Focus visible: outline 2px ✓

✓ Performance:
  - No requiere virtualización
  - No requiere lazy loading
  - React.memo recomendado si se usa en listas

✓ Composición:
  - Componente atómico
  - No requiere Compound Components
  - Forward ref para integración con forms

📋 SPEC
✓ SPEC-COMP-001.md generada en .mj2/specs/components/
✓ Design tokens definidos
✓ Tests especificados (Unit + Accessibility + Visual)

✅ VALIDATE
✓ Design review: 5/5
✓ Accessibility: 5/5
✓ UX: 5/5
✓ Technical: 5/5

📦 Siguiente paso:
  /mj2:1-plan   # Crear SPEC detallada (spec-builder)
  /mj2:2-run    # Implementar componente (frontend-builder)
```

---

### **Ejemplo 2: DataTable Compleja**

```bash
/mj2:design-component "DataTable con sorting, filtering, pagination y virtualización"
```

**Output:**
```
🎨 DESIGN
✓ Component API (Compound Components pattern):
  <DataTable data={...} />
    <DataTable.Header />
    <DataTable.Body virtualizer={true} />
    <DataTable.Pagination />
  </DataTable>

✓ Hooks personalizados:
  - useTableSort(data, sortConfig)
  - useTableFilter(data, filters)
  - useTablePagination(data, pageSize)
  - useTableVirtualizer(rowCount, estimatedRowHeight)

🔍 ANALYZE
✓ Accessibility:
  - role="table", role="row", role="columnheader", role="cell"
  - aria-sort para columnas ordenables
  - Keyboard navigation: Arrow keys, Home, End
  - Screen reader: aria-label, aria-rowcount, aria-colcount
  ⚠️ Complex keyboard navigation (requiere testing extensivo)

✓ Performance:
  - Virtualización con @tanstack/react-virtual
  - Memoización de rows con React.memo
  - useDeferredValue para filtros
  - Code splitting recomendado

✓ Composición:
  - Componente organismo (complejo)
  - Compound Components pattern (recomendado)
  - Subcomponentes: Header, Body, Row, Cell, Pagination, Toolbar

📋 SPEC
✓ SPEC-COMP-015.md generada
✓ Subcomponentes especificados (6)
✓ Hooks especificados (4)
✓ Performance optimizations documentadas
⚠️ Tests requeridos:
  - Unit: 15 tests (componentes + hooks)
  - Accessibility: axe-core + keyboard navigation
  - Integration: Sorting + Filtering + Pagination juntos
  - Performance: Render time con 10,000 rows

⚠️ VALIDATE
✓ Design review: 5/5
✓ Accessibility: 4/5
  ⚠️ Keyboard navigation requiere testing manual extensivo
✓ Performance: 5/5
✓ Technical: 5/5

📦 Acciones recomendadas:
  1. Validar keyboard navigation con usuarios reales
  2. Testing de performance con dataset grande (>5,000 rows)
  3. Considerar feature flags para funcionalidad opcional

📦 Siguiente paso:
  /mj2:1-plan   # Crear SPEC detallada
  /mj2:2-run    # Implementar con TDD
```

---

### **Ejemplo 3: Modal Dialog**

```bash
/mj2:design-component "Modal dialog con focus trap y backdrop"
```

**Output:**
```
🎨 DESIGN
✓ Component API:
  - Props: isOpen, onClose, title, children, size, closeOnBackdrop
  - Compound Components:
    <Modal isOpen={...} onClose={...}>
      <Modal.Header>Título</Modal.Header>
      <Modal.Body>Contenido</Modal.Body>
      <Modal.Footer>Acciones</Modal.Footer>
    </Modal>

✓ Hook personalizado:
  - useDisclosure() → { isOpen, onOpen, onClose, onToggle }

🔍 ANALYZE
✓ Accessibility (WAI-ARIA Dialog pattern):
  - role="dialog", aria-modal="true"
  - aria-labelledby, aria-describedby
  - Focus trap (react-focus-lock)
  - Restore focus on close
  - Close on Escape key
  - Prevent body scroll cuando está abierto

✓ Performance:
  - Portal rendering (ReactDOM.createPortal)
  - Lazy mount (solo renderizar cuando isOpen=true)
  - Animaciones con CSS (no JS)

✓ Composición:
  - Componente molecular
  - Compound Components pattern
  - Backdrop component separado

📋 SPEC
✓ SPEC-COMP-008.md generada
✓ Focus management strategy documentada
✓ Animation strategy: CSS transitions
✓ Tests especificados:
  - Focus trap funcional
  - Restore focus al cerrar
  - Close on Escape
  - Close on backdrop click (si closeOnBackdrop=true)
  - No body scroll cuando está abierto

✅ VALIDATE
✓ Design review: 5/5
✓ Accessibility: 5/5
✓ UX: 5/5
✓ Technical: 5/5

📦 Siguiente paso:
  /mj2:1-plan   # Crear SPEC
  /mj2:2-run    # Implementar
```

---

### **Ejemplo 4: Form Field Compuesto**

```bash
/mj2:design-component "FormField con label, input, error y helper text"
```

**Output:**
```
🎨 DESIGN
✓ Component API (Molecular component):
  <FormField
    label="Email"
    type="email"
    error="Email inválido"
    helperText="Usaremos tu email para notificaciones"
    required={true}
  />

✓ Subcomponentes:
  - Label (con required indicator)
  - Input (con validation state)
  - ErrorMessage (con icon)
  - HelperText

🔍 ANALYZE
✓ Accessibility:
  - <label htmlFor={inputId}>
  - aria-invalid={hasError}
  - aria-describedby (error o helperText)
  - aria-required={required}

✓ Integration:
  - react-hook-form compatible
  - Controlled & Uncontrolled modes
  - Forward ref

📋 SPEC
✓ SPEC-COMP-005.md generada
✓ Integration con react-hook-form documentada
✓ Validation strategy especificada

✅ VALIDATE
✓ Todos los checks: 5/5

📦 Siguiente paso:
  /mj2:1-plan
  /mj2:2-run
```

---

## 🎨 Design Patterns Soportados

El comando reconoce y recomienda estos patrones:

### 1. **Compound Components**
Para componentes con múltiples partes relacionadas:
```typescript
<Select>
  <Select.Trigger />
  <Select.Content />
  <Select.Item />
</Select>
```

**Cuándo:** Componente con >3 partes, necesitas flexibilidad

### 2. **Render Props**
Para compartir lógica de rendering:
```typescript
<DataTable
  data={users}
  renderRow={(user) => <UserRow user={user} />}
/>
```

**Cuándo:** Lógica reutilizable, control sobre rendering

### 3. **Custom Hooks**
Para lógica sin UI:
```typescript
const { isOpen, onOpen, onClose } = useDisclosure();
```

**Cuándo:** Lógica de estado sin UI, reutilizable

### 4. **Controlled vs. Uncontrolled**
```typescript
// Controlled
<Input value={value} onChange={setValue} />

// Uncontrolled
<Input defaultValue="initial" ref={inputRef} />
```

**Cuándo:** Controlled para formularios complejos, Uncontrolled para simples

---

## ♿ Accessibility Checklist

El comando valida automáticamente:

- [ ] **Keyboard navigation:** Tab, Enter, Space, Arrows, Escape
- [ ] **Screen reader:** ARIA labels, roles, states
- [ ] **Focus management:** Visible, trap, restoration
- [ ] **Color contrast:** Mínimo 4.5:1 (texto), 3:1 (UI)
- [ ] **Touch targets:** Mínimo 44×44px
- [ ] **Semantic HTML:** `<button>` no `<div onClick>`
- [ ] **Error handling:** Mensajes accesibles
- [ ] **Loading states:** aria-busy, aria-live

---

## 📐 Design Tokens

El comando genera design tokens para cada componente:

```typescript
const buttonTokens = {
  colors: {
    primary: { bg: '#1976d2', text: '#fff', hover: '#1565c0' },
    secondary: { bg: '#dc004e', text: '#fff', hover: '#c51162' },
  },
  sizes: {
    small: { height: 32, padding: '0 12px', fontSize: 14 },
    medium: { height: 40, padding: '0 16px', fontSize: 16 },
    large: { height: 48, padding: '0 24px', fontSize: 18 },
  },
  borderRadius: 4,
  transitions: { duration: 150, easing: 'ease-in-out' },
};
```

---

## 🔗 Integración con Otros Comandos

```bash
# 1. Diseñar componente
/mj2:design-component "Button con loading state"

# 2. Generar SPEC detallada
/mj2:1-plan

# 3. Implementar con TDD
/mj2:2-run

# 4. Validar calidad
/mj2:quality-check

# 5. Sincronizar documentación
/mj2:3-sync
```

---

## 📊 Output del Comando

El comando genera:

1. **SPEC del componente** (`.mj2/specs/components/SPEC-COMP-XXX.md`)
2. **Design tokens** (documentados en SPEC)
3. **Accessibility checklist** (validación automática)
4. **Test specifications** (Unit + A11y + Visual)
5. **Design decisions log** (rationale de patrones elegidos)
6. **Integration recommendations** (con otros componentes/libs)

---

## 🎓 Best Practices Aplicadas

- ✅ **Accessibility First:** WCAG 2.2 AA en todas las fases
- ✅ **Design Tokens:** No hardcoding de valores
- ✅ **Type Safety:** TypeScript estricto
- ✅ **Testability:** Component API testeable
- ✅ **Composición:** Patrones de composición apropiados
- ✅ **Performance:** Optimizaciones cuando son necesarias
- ✅ **Documentation:** Decisiones de diseño documentadas

---

**Ver agente completo:** `.claude/agents/mj2/component-designer.md`
**Versión:** 1.0.0
**Última Actualización:** 2025-11-23
