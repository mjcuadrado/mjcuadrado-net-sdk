---
skill: accessibility
description: Accesibilidad web WCAG 2.1 Level AA para aplicaciones React y .NET
tags: [accessibility, a11y, wcag, aria, semantic-html, screen-reader]
applies_to: [React 18+, ASP.NET Core, HTML5, ARIA]
---

# Web Accessibility (A11Y)

Guía completa de accesibilidad web siguiendo WCAG 2.1 Level AA para aplicaciones React y .NET.

---

## 📋 Tabla de Contenidos

1. [WCAG 2.1 Principles (POUR)](#wcag-21-principles)
2. [Semantic HTML](#semantic-html)
3. [ARIA Patterns](#aria-patterns)
4. [Keyboard Navigation](#keyboard-navigation)
5. [Screen Reader Support](#screen-reader-support)
6. [Color Contrast](#color-contrast)
7. [Form Accessibility](#form-accessibility)
8. [Testing Tools](#testing-tools)

---

## 🎯 WCAG 2.1 Principles (POUR)

### 1. Perceivable

La información y los componentes de la interfaz deben presentarse de forma que los usuarios puedan percibirlos.

#### 1.1 Text Alternatives

```tsx
// ❌ MAL: Sin texto alternativo
<img src="/logo.png" />
<button><Icon /></button>

// ✅ BIEN: Con texto alternativo
<img src="/logo.png" alt="Company Logo" />
<button aria-label="Close dialog">
  <CloseIcon aria-hidden="true" />
</button>

// ✅ Imágenes decorativas
<img src="/decoration.png" alt="" role="presentation" />

// ✅ Iconos con texto visible
<button>
  <Icon aria-hidden="true" />
  <span>Save</span>
</button>
```

#### 1.2 Time-based Media

```tsx
// ✅ Video con subtítulos y transcripción
<video controls>
  <source src="video.mp4" type="video/mp4" />
  <track kind="subtitles" src="subtitles-es.vtt" srclang="es" label="Español" />
  <track kind="captions" src="captions-en.vtt" srclang="en" label="English" />
</video>

<details>
  <summary>Transcripción del video</summary>
  <p>Texto completo del contenido del video...</p>
</details>
```

#### 1.3 Adaptable

```tsx
// ✅ Estructura semántica adaptable
<article>
  <header>
    <h1>Título del artículo</h1>
    <p>Publicado el <time datetime="2025-11-23">23 de noviembre, 2025</time></p>
  </header>

  <section>
    <h2>Introducción</h2>
    <p>Contenido...</p>
  </section>

  <aside>
    <h3>Información relacionada</h3>
    <ul>
      <li>Recurso 1</li>
      <li>Recurso 2</li>
    </ul>
  </aside>
</article>
```

#### 1.4 Distinguishable

```tsx
// ✅ Contraste de color adecuado (4.5:1 para texto)
const theme = {
  colors: {
    text: '#212121',      // Contraste 16:1 con blanco
    textSecondary: '#666', // Contraste 5.7:1 con blanco
    background: '#FFFFFF',
    primary: '#0066CC',   // Contraste 7:1 con blanco
  }
};

// ✅ No usar solo color para transmitir información
// MAL: Solo color
<span style={{ color: 'red' }}>Error</span>

// BIEN: Color + ícono + texto
<span className="error">
  <ErrorIcon aria-hidden="true" />
  <span>Error: Campo requerido</span>
</span>
```

### 2. Operable

Los componentes de la interfaz y la navegación deben ser operables.

#### 2.1 Keyboard Accessible

```tsx
// ✅ Todos los elementos interactivos accesibles por teclado
function Dialog({ isOpen, onClose, children }) {
  const dialogRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (isOpen) {
      // Guardar elemento con foco
      const previouslyFocused = document.activeElement as HTMLElement;

      // Mover foco al diálogo
      dialogRef.current?.focus();

      return () => {
        // Restaurar foco al cerrar
        previouslyFocused?.focus();
      };
    }
  }, [isOpen]);

  const handleKeyDown = (e: KeyboardEvent) => {
    if (e.key === 'Escape') {
      onClose();
    }
  };

  return (
    <div
      ref={dialogRef}
      role="dialog"
      aria-modal="true"
      tabIndex={-1}
      onKeyDown={handleKeyDown}
    >
      {children}
    </div>
  );
}
```

#### 2.2 Enough Time

```tsx
// ✅ Dar suficiente tiempo o permitir extenderlo
function SessionTimeout() {
  const [timeRemaining, setTimeRemaining] = useState(300); // 5 minutos

  return (
    <div role="alert" aria-live="polite">
      <p>Tu sesión expirará en {timeRemaining} segundos</p>
      <button onClick={extendSession}>Extender sesión</button>
    </div>
  );
}
```

#### 2.3 Seizures and Physical Reactions

```tsx
// ✅ No usar elementos que parpadeen más de 3 veces por segundo
// Evitar animaciones que puedan causar convulsiones

// ✅ Permitir desactivar animaciones
const prefersReducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)');

const AnimatedComponent = styled.div`
  transition: transform 0.3s ease;

  @media (prefers-reduced-motion: reduce) {
    transition: none;
  }
`;
```

#### 2.4 Navigable

```tsx
// ✅ Skip links
function Layout({ children }) {
  return (
    <>
      <a href="#main-content" className="skip-link">
        Saltar al contenido principal
      </a>

      <header>
        <nav aria-label="Navegación principal">
          {/* Navigation */}
        </nav>
      </header>

      <main id="main-content" tabIndex={-1}>
        {children}
      </main>
    </>
  );
}

// CSS para skip link
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
```

### 3. Understandable

La información y el manejo de la interfaz deben ser comprensibles.

#### 3.1 Readable

```tsx
// ✅ Idioma del documento
<html lang="es">

// ✅ Idioma de partes específicas
<p>Este texto está en español</p>
<p lang="en">This text is in English</p>

// ✅ Abreviaturas
<abbr title="Hypertext Markup Language">HTML</abbr>
```

#### 3.2 Predictable

```tsx
// ✅ Navegación consistente
function Navigation() {
  // Mismo orden en todas las páginas
  return (
    <nav aria-label="Navegación principal">
      <ul>
        <li><a href="/">Inicio</a></li>
        <li><a href="/products">Productos</a></li>
        <li><a href="/about">Acerca de</a></li>
        <li><a href="/contact">Contacto</a></li>
      </ul>
    </nav>
  );
}

// ✅ No cambiar contexto automáticamente
// MAL: Submit al cambiar select
<select onChange={(e) => form.submit()}>

// BIEN: Botón explícito para submit
<select onChange={(e) => setSelected(e.target.value)}>
  {/* options */}
</select>
<button onClick={handleSubmit}>Aplicar</button>
```

#### 3.3 Input Assistance

```tsx
// ✅ Identificar errores y sugerir correcciones
function FormField({ name, label, error, value, onChange }) {
  const errorId = `${name}-error`;

  return (
    <div>
      <label htmlFor={name}>
        {label}
        {required && <span aria-label="requerido">*</span>}
      </label>

      <input
        id={name}
        name={name}
        value={value}
        onChange={onChange}
        aria-invalid={!!error}
        aria-describedby={error ? errorId : undefined}
      />

      {error && (
        <span id={errorId} role="alert" className="error">
          {error}
        </span>
      )}
    </div>
  );
}
```

### 4. Robust

El contenido debe ser robusto para ser interpretado por tecnologías asistivas.

```tsx
// ✅ HTML válido
// ✅ ARIA correcto
// ✅ Compatible con tecnologías asistivas

// Validar con:
// - W3C Validator
// - axe DevTools
// - Screen readers (NVDA, JAWS, VoiceOver)
```

---

## 🏗️ Semantic HTML

### Landmarks

```tsx
// ✅ Estructura con landmarks
function App() {
  return (
    <>
      <header>
        <nav aria-label="Navegación principal">
          {/* Navigation */}
        </nav>
      </header>

      <main>
        <h1>Título principal</h1>

        <section aria-labelledby="section1-title">
          <h2 id="section1-title">Sección 1</h2>
          {/* Content */}
        </section>

        <aside aria-label="Contenido relacionado">
          {/* Related content */}
        </aside>
      </main>

      <footer>
        <p>&copy; 2025 Company</p>
      </footer>
    </>
  );
}
```

### Headings Hierarchy

```tsx
// ❌ MAL: Jerarquía incorrecta
<h1>Título</h1>
<h3>Subtítulo</h3>  {/* Salta h2! */}
<h2>Otro subtítulo</h2>  {/* Fuera de orden */}

// ✅ BIEN: Jerarquía correcta
<h1>Título principal</h1>
<h2>Sección 1</h2>
<h3>Subsección 1.1</h3>
<h3>Subsección 1.2</h3>
<h2>Sección 2</h2>
```

### Lists

```tsx
// ✅ Usar listas para contenido relacionado
<ul>
  <li>Item 1</li>
  <li>Item 2</li>
  <li>Item 3</li>
</ul>

// ✅ Lista de definiciones
<dl>
  <dt>Término 1</dt>
  <dd>Definición del término 1</dd>

  <dt>Término 2</dt>
  <dd>Definición del término 2</dd>
</dl>
```

### Tables

```tsx
// ✅ Tabla accesible
<table>
  <caption>Ventas por trimestre</caption>
  <thead>
    <tr>
      <th scope="col">Trimestre</th>
      <th scope="col">Ventas</th>
      <th scope="col">Crecimiento</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th scope="row">Q1 2025</th>
      <td>$1,234</td>
      <td>+15%</td>
    </tr>
  </tbody>
</table>
```

---

## 🎭 ARIA Patterns

### Common ARIA Roles

```tsx
// Button
<div role="button" tabIndex={0} onClick={handleClick} onKeyPress={handleKeyPress}>
  Click me
</div>

// Alert
<div role="alert" aria-live="assertive">
  Error: Formulario inválido
</div>

// Status (live region polite)
<div role="status" aria-live="polite">
  Guardando cambios...
</div>

// Dialog
<div role="dialog" aria-modal="true" aria-labelledby="dialog-title">
  <h2 id="dialog-title">Confirmar acción</h2>
  {/* Dialog content */}
</div>

// Navigation
<nav aria-label="Breadcrumb">
  <ol>
    <li><a href="/">Home</a></li>
    <li><a href="/products">Products</a></li>
    <li aria-current="page">Product Details</li>
  </ol>
</nav>
```

### Tabs Pattern

```tsx
function Tabs({ tabs }) {
  const [selectedTab, setSelectedTab] = useState(0);

  const handleKeyDown = (e: KeyboardEvent, index: number) => {
    if (e.key === 'ArrowRight') {
      setSelectedTab((index + 1) % tabs.length);
    } else if (e.key === 'ArrowLeft') {
      setSelectedTab((index - 1 + tabs.length) % tabs.length);
    }
  };

  return (
    <div>
      <div role="tablist" aria-label="Tabs">
        {tabs.map((tab, index) => (
          <button
            key={index}
            role="tab"
            aria-selected={selectedTab === index}
            aria-controls={`panel-${index}`}
            id={`tab-${index}`}
            tabIndex={selectedTab === index ? 0 : -1}
            onClick={() => setSelectedTab(index)}
            onKeyDown={(e) => handleKeyDown(e, index)}
          >
            {tab.label}
          </button>
        ))}
      </div>

      {tabs.map((tab, index) => (
        <div
          key={index}
          role="tabpanel"
          id={`panel-${index}`}
          aria-labelledby={`tab-${index}`}
          hidden={selectedTab !== index}
        >
          {tab.content}
        </div>
      ))}
    </div>
  );
}
```

### Accordion Pattern

```tsx
function Accordion({ items }) {
  const [expandedIndex, setExpandedIndex] = useState<number | null>(null);

  return (
    <div>
      {items.map((item, index) => (
        <div key={index}>
          <h3>
            <button
              aria-expanded={expandedIndex === index}
              aria-controls={`panel-${index}`}
              id={`accordion-${index}`}
              onClick={() => setExpandedIndex(expandedIndex === index ? null : index)}
            >
              {item.title}
            </button>
          </h3>

          <div
            id={`panel-${index}`}
            role="region"
            aria-labelledby={`accordion-${index}`}
            hidden={expandedIndex !== index}
          >
            {item.content}
          </div>
        </div>
      ))}
    </div>
  );
}
```

### Modal/Dialog Pattern

```tsx
function Modal({ isOpen, onClose, title, children }) {
  const modalRef = useRef<HTMLDivElement>(null);
  const previouslyFocused = useRef<HTMLElement | null>(null);

  useEffect(() => {
    if (isOpen) {
      previouslyFocused.current = document.activeElement as HTMLElement;
      modalRef.current?.focus();

      // Trap focus
      const handleTab = (e: KeyboardEvent) => {
        if (e.key === 'Tab') {
          const focusable = modalRef.current?.querySelectorAll(
            'button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])'
          );
          if (focusable) {
            const first = focusable[0] as HTMLElement;
            const last = focusable[focusable.length - 1] as HTMLElement;

            if (e.shiftKey && document.activeElement === first) {
              last.focus();
              e.preventDefault();
            } else if (!e.shiftKey && document.activeElement === last) {
              first.focus();
              e.preventDefault();
            }
          }
        }
      };

      document.addEventListener('keydown', handleTab);
      return () => document.removeEventListener('keydown', handleTab);
    } else {
      previouslyFocused.current?.focus();
    }
  }, [isOpen]);

  if (!isOpen) return null;

  return (
    <div
      className="modal-overlay"
      onClick={onClose}
      role="presentation"
    >
      <div
        ref={modalRef}
        role="dialog"
        aria-modal="true"
        aria-labelledby="modal-title"
        tabIndex={-1}
        onClick={(e) => e.stopPropagation()}
        onKeyDown={(e) => {
          if (e.key === 'Escape') onClose();
        }}
      >
        <h2 id="modal-title">{title}</h2>
        {children}
        <button onClick={onClose}>Close</button>
      </div>
    </div>
  );
}
```

---

## ⌨️ Keyboard Navigation

### Focus Management

```tsx
// ✅ Visible focus indicator
button:focus-visible {
  outline: 2px solid #0066CC;
  outline-offset: 2px;
}

// ✅ Focus trap en modal (ya mostrado arriba)

// ✅ Restaurar foco al cerrar componentes
function Dropdown() {
  const buttonRef = useRef<HTMLButtonElement>(null);

  const closeDropdown = () => {
    setIsOpen(false);
    buttonRef.current?.focus(); // Restaurar foco
  };

  return (
    <div>
      <button ref={buttonRef} onClick={() => setIsOpen(!isOpen)}>
        Menu
      </button>
      {isOpen && (
        <div role="menu">
          <button role="menuitem" onClick={closeDropdown}>Option 1</button>
          <button role="menuitem" onClick={closeDropdown}>Option 2</button>
        </div>
      )}
    </div>
  );
}
```

### Keyboard Shortcuts

```tsx
// ✅ Implementar shortcuts comunes
function Editor() {
  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      // Ctrl+S para guardar
      if (e.ctrlKey && e.key === 's') {
        e.preventDefault();
        save();
      }

      // Ctrl+Z para deshacer
      if (e.ctrlKey && e.key === 'z') {
        e.preventDefault();
        undo();
      }
    };

    document.addEventListener('keydown', handleKeyDown);
    return () => document.removeEventListener('keydown', handleKeyDown);
  }, []);

  return (
    <div>
      <button onClick={save} title="Guardar (Ctrl+S)">
        Save
      </button>
    </div>
  );
}
```

---

## 🔊 Screen Reader Support

### ARIA Labels

```tsx
// ✅ aria-label para elementos sin texto visible
<button aria-label="Cerrar">
  <CloseIcon aria-hidden="true" />
</button>

// ✅ aria-labelledby para referenciar otro elemento
<section aria-labelledby="section-title">
  <h2 id="section-title">Sección de productos</h2>
  {/* Content */}
</section>

// ✅ aria-describedby para descripciones adicionales
<input
  type="password"
  aria-describedby="password-requirements"
/>
<div id="password-requirements">
  Debe tener al menos 8 caracteres
</div>
```

### Live Regions

```tsx
// ✅ Anunciar cambios dinámicos
function LoadingStatus({ isLoading, error, success }) {
  return (
    <div role="status" aria-live="polite" aria-atomic="true">
      {isLoading && <p>Cargando...</p>}
      {error && <p role="alert">Error: {error}</p>}
      {success && <p>Operación completada exitosamente</p>}
    </div>
  );
}

// aria-live="polite": No interrumpe
// aria-live="assertive": Interrumpe inmediatamente
// aria-atomic="true": Lee todo el contenido, no solo cambios
```

### Visually Hidden Text

```tsx
// ✅ Texto solo para screen readers
const VisuallyHidden = styled.span`
  position: absolute;
  width: 1px;
  height: 1px;
  padding: 0;
  margin: -1px;
  overflow: hidden;
  clip: rect(0, 0, 0, 0);
  white-space: nowrap;
  border: 0;
`;

function Pagination({ currentPage, totalPages }) {
  return (
    <nav aria-label="Paginación">
      <button>
        <VisuallyHidden>Ir a </VisuallyHidden>
        Anterior
      </button>
      <span aria-current="page">
        Página {currentPage} de {totalPages}
      </span>
      <button>
        Siguiente
        <VisuallyHidden> página</VisuallyHidden>
      </button>
    </nav>
  );
}
```

---

## 🎨 Color Contrast

### WCAG 2.1 Level AA Requirements

- **Texto normal:** 4.5:1
- **Texto grande (18pt+ o 14pt bold+):** 3:1
- **Componentes UI y gráficos:** 3:1

```tsx
// ✅ Paleta con contraste adecuado
const colors = {
  // Texto en blanco
  textOnWhite: '#212121',      // 16:1 ✅
  textSecondary: '#666666',    // 5.7:1 ✅
  textDisabled: '#999999',     // 2.8:1 ❌ (solo para texto grande)

  // Colores de marca
  primary: '#0066CC',          // 7:1 con blanco ✅
  secondary: '#6B4C9A',        // 6.7:1 con blanco ✅
  error: '#C62828',            // 5.9:1 con blanco ✅
  success: '#2E7D32',          // 4.5:1 con blanco ✅
  warning: '#F57C00',          // 3.5:1 con blanco ⚠️ (solo texto grande)

  // Backgrounds
  background: '#FFFFFF',
  surface: '#F5F5F5',
};

// Herramientas para verificar contraste:
// - WebAIM Contrast Checker
// - Chrome DevTools
// - axe DevTools
```

### No usar solo color

```tsx
// ❌ MAL: Solo color para estado
<input style={{ borderColor: hasError ? 'red' : 'gray' }} />

// ✅ BIEN: Color + ícono + texto
<div>
  <input
    aria-invalid={hasError}
    aria-describedby={hasError ? 'error-message' : undefined}
    style={{ borderColor: hasError ? '#C62828' : '#999' }}
  />
  {hasError && (
    <span id="error-message" role="alert" className="error">
      <ErrorIcon aria-hidden="true" />
      <span>Este campo es requerido</span>
    </span>
  )}
</div>
```

---

## 📝 Form Accessibility

### Labels y Fields

```tsx
// ✅ Label explícito
<label htmlFor="email">
  Email
  <span aria-label="requerido">*</span>
</label>
<input
  id="email"
  type="email"
  required
  aria-required="true"
/>

// ✅ Fieldset para grupos relacionados
<fieldset>
  <legend>Información de contacto</legend>

  <label htmlFor="name">Nombre</label>
  <input id="name" type="text" />

  <label htmlFor="email">Email</label>
  <input id="email" type="email" />
</fieldset>
```

### Error Messages

```tsx
function FormField({ name, label, value, onChange, error, required }) {
  const errorId = `${name}-error`;
  const descriptionId = `${name}-description`;

  return (
    <div>
      <label htmlFor={name}>
        {label}
        {required && <span aria-label="requerido">*</span>}
      </label>

      <input
        id={name}
        value={value}
        onChange={onChange}
        aria-invalid={!!error}
        aria-describedby={`${error ? errorId : ''} ${descriptionId}`.trim()}
        aria-required={required}
      />

      <span id={descriptionId} className="help-text">
        Formato: nombre@ejemplo.com
      </span>

      {error && (
        <span id={errorId} role="alert" className="error">
          <ErrorIcon aria-hidden="true" />
          {error}
        </span>
      )}
    </div>
  );
}
```

### Form Validation

```tsx
function ContactForm() {
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [submitted, setSubmitted] = useState(false);

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();

    const newErrors = validate(formData);

    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);

      // Anunciar errores
      const errorCount = Object.keys(newErrors).length;
      announce(`Formulario inválido. ${errorCount} errores encontrados.`);

      // Focus en primer campo con error
      const firstErrorField = document.querySelector('[aria-invalid="true"]') as HTMLElement;
      firstErrorField?.focus();
    } else {
      // Submit form
      setSubmitted(true);
      announce('Formulario enviado exitosamente');
    }
  };

  return (
    <form onSubmit={handleSubmit} noValidate>
      {/* Form fields */}

      <button type="submit">Enviar</button>

      {submitted && (
        <div role="status" aria-live="polite">
          Formulario enviado exitosamente
        </div>
      )}
    </form>
  );
}
```

---

## 🧪 Testing Tools

### Automated Testing

#### axe-core

```typescript
// Instalación
npm install --save-dev @axe-core/react

// Setup
import React from 'react';
import ReactDOM from 'react-dom';
import { axe } from '@axe-core/react';

if (process.env.NODE_ENV !== 'production') {
  axe(React, ReactDOM, 1000);
}

// En tests
import { axe, toHaveNoViolations } from 'jest-axe';

expect.extend(toHaveNoViolations);

test('should not have accessibility violations', async () => {
  const { container } = render(<MyComponent />);
  const results = await axe(container);
  expect(results).toHaveNoViolations();
});
```

#### Playwright a11y Testing

```typescript
import { test, expect } from '@playwright/test';
import AxeBuilder from '@axe-core/playwright';

test('should not have accessibility violations', async ({ page }) => {
  await page.goto('/');

  const accessibilityScanResults = await new AxeBuilder({ page }).analyze();

  expect(accessibilityScanResults.violations).toEqual([]);
});
```

#### React Testing Library

```typescript
import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';

test('button is accessible', async () => {
  render(<Button>Click me</Button>);

  // Test keyboard navigation
  const button = screen.getByRole('button', { name: /click me/i });
  await userEvent.tab();
  expect(button).toHaveFocus();

  // Test click
  await userEvent.click(button);
  expect(onClick).toHaveBeenCalled();

  // Test keyboard activation
  await userEvent.keyboard('{Enter}');
  expect(onClick).toHaveBeenCalledTimes(2);
});
```

### Manual Testing

#### Screen Readers

**NVDA (Windows - Free)**
```
Descargar: https://www.nvaccess.org/
Shortcuts:
- NVDA + Espacio: Modo exploración/foco
- H: Siguiente heading
- Ctrl: Detener lectura
```

**JAWS (Windows - Paid)**
```
Trial: https://www.freedomscientific.com/
Shortcuts similares a NVDA
```

**VoiceOver (macOS - Built-in)**
```
Activar: Cmd + F5
Shortcuts:
- VO + A: Comenzar lectura
- VO + Right Arrow: Siguiente item
- VO + Command + H: Siguiente heading
```

#### Keyboard Testing Checklist

- [ ] Tab: Navegar hacia adelante
- [ ] Shift + Tab: Navegar hacia atrás
- [ ] Enter: Activar botones/links
- [ ] Space: Activar checkboxes, radio buttons
- [ ] Arrow keys: Navegación en tabs, menus, sliders
- [ ] Escape: Cerrar modals/dropdowns
- [ ] Home/End: Inicio/fin de lista
- [ ] Visible focus indicator en todos los elementos

#### Lighthouse

```bash
# CLI
npm install -g @lhci/cli
lhci autorun

# Chrome DevTools
1. F12 → Lighthouse tab
2. Select "Accessibility"
3. Generate report
```

---

## ✅ Accessibility Checklist

### General
- [ ] Todas las imágenes tienen texto alternativo apropiado
- [ ] Videos tienen subtítulos y transcripciones
- [ ] Contraste de color cumple 4.5:1 (texto) y 3:1 (UI)
- [ ] No se usa solo color para transmitir información
- [ ] Idioma del documento especificado (lang="es")

### Structure
- [ ] Headings en orden jerárquico (h1, h2, h3...)
- [ ] Landmarks correctos (header, nav, main, aside, footer)
- [ ] Skip links implementados
- [ ] Listas usadas apropiadamente (<ul>, <ol>, <dl>)

### Keyboard
- [ ] Todos los elementos interactivos accesibles por teclado
- [ ] Orden de tab lógico
- [ ] Focus visible en todos los elementos
- [ ] Atajos de teclado documentados
- [ ] No hay trampas de teclado

### Forms
- [ ] Todos los campos tienen labels
- [ ] Errores identificados y descritos
- [ ] Campos requeridos marcados
- [ ] Validación accesible
- [ ] Grupos de campos con fieldset/legend

### ARIA
- [ ] Roles apropiados
- [ ] Estados y propiedades correctos
- [ ] Live regions para contenido dinámico
- [ ] aria-label/aria-labelledby donde necesario
- [ ] No sobrescribir semántica HTML

### Testing
- [ ] Automated tests (axe-core) passing
- [ ] Screen reader testing (NVDA/JAWS/VoiceOver)
- [ ] Keyboard navigation testing
- [ ] Lighthouse accessibility score > 90

---

## 📚 Referencias

- **WCAG 2.1:** https://www.w3.org/WAI/WCAG21/quickref/
- **ARIA Practices:** https://www.w3.org/WAI/ARIA/apg/
- **WebAIM:** https://webaim.org/
- **a11y Project:** https://www.a11yproject.com/
- **MDN Accessibility:** https://developer.mozilla.org/en-US/docs/Web/Accessibility

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-23
**Mantenido por:** mjcuadrado-net-sdk
