# Issue #33: Frontend Testing Stack Detail

**Status:** ✅ Completed
**Priority:** 🟡 High
**Version:** v0.3.0
**Created:** 2025-11-22
**Completed:** 2025-11-22

---

## 📋 Descripción

Se ha completado el detalle del **Frontend Testing Stack** con skills comprehensivas para **Vitest** y **React Testing Library**. Este issue complementa la Testing Pyramid completa (Issue #32) proporcionando documentación detallada de las herramientas de testing unitario y de componentes React.

---

## 🎯 Objetivos

Implementar documentación detallada del stack de testing frontend:

1. ✅ **Vitest Skill** - Framework de testing moderno con Vite
2. ✅ **React Testing Library Skill** - Testing de componentes user-centric
3. ✅ **Patrones de Testing** - Best practices y anti-patterns
4. ✅ **Integración** - Con frontend-builder y testing pyramid
5. ✅ **Coherencia de Idioma** - Todo en español

---

## 📦 Archivos Creados

### 1. vitest.md (622 líneas)

**Ubicación:** `.claude/skills/testing/vitest.md`

**Contenido:**
- Instalación y configuración de Vitest
- Estructura básica de tests
- Matchers y aserciones comunes
- Mocking (funciones, módulos, implementaciones)
- Setup y teardown hooks
- Configuración de cobertura
- Watch mode y UI mode
- Testing asíncrono
- Snapshot testing
- Mejores prácticas

**Características Clave:**
- ⚡ Framework ultra rápido powered by Vite
- 🎯 Soporte nativo para ESM y TypeScript
- 🔄 Watch mode con HMR instantáneo
- 🎨 UI mode para test runner visual
- ⚙️ API compatible con Jest (migración fácil)

**Secciones Principales:**

```typescript
// Configuración básica
export default defineConfig({
  plugins: [react()],
  test: {
    environment: 'jsdom',
    setupFiles: ['./src/test/setup.ts'],
    coverage: {
      provider: 'v8',
      thresholds: { lines: 80, functions: 80, branches: 80, statements: 80 },
    },
  },
});
```

### 2. react-testing-library.md (570 líneas)

**Ubicación:** `.claude/skills/testing/react-testing-library.md`

**Contenido:**
- Filosofía user-centric testing
- Instalación y setup con Vitest
- Métodos de consulta (queries) y prioridades
- userEvent vs fireEvent
- Testing asíncrono (findBy, waitFor)
- Render personalizado con proveedores
- Patrones de testing (formularios, hooks, API mocking)
- Aserciones jest-dom
- Mejores prácticas y anti-patterns

**Filosofía:**
- 🎯 Testear comportamiento de usuario, no implementación
- ♿ Consultar por atributos de accesibilidad (roles, labels)
- 🚫 Evitar testear estado interno
- ✅ Tests mantenibles y resistentes a refactorización

**Prioridad de Queries:**
```typescript
// 1. getByRole - MÁS RECOMENDADO (accesible)
screen.getByRole('button', { name: /enviar/i });

// 2. getByLabelText - Formularios
screen.getByLabelText(/email/i);

// 3. getByPlaceholderText - Placeholders
screen.getByPlaceholderText(/buscar/i);

// 4. getByText - Contenido visible
screen.getByText(/bienvenido/i);

// 8. getByTestId - ÚLTIMO RECURSO
screen.getByTestId('custom-element');
```

### 3. issue-33.md

**Ubicación:** `.github/issues/issue-33.md`

**Contenido:** Este archivo - documentación completa del Issue #33

---

## 🔄 Flujo de Testing Frontend

### Niveles de Testing Cubiertos

```
E2E (Playwright)          ← Issue #32
    ↓
Component (Vitest + RTL)  ← Issue #33 (ESTE)
    ↓
Unit (Vitest)             ← Issue #33 (ESTE)
```

### Workflow Típico

```bash
# 1. Test unitario de lógica
# vitest.md
describe('calculateTotal', () => {
  it('debería sumar precios correctamente', () => {
    expect(calculateTotal([100, 200])).toBe(300);
  });
});

# 2. Test de componente
# react-testing-library.md
describe('LoginForm', () => {
  it('debería enviar formulario con datos válidos', async () => {
    const user = userEvent.setup();
    render(<LoginForm onSubmit={handleSubmit} />);

    await user.type(screen.getByLabelText(/email/i), 'user@example.com');
    await user.click(screen.getByRole('button', { name: /enviar/i }));

    expect(handleSubmit).toHaveBeenCalled();
  });
});

# 3. E2E test (ya cubierto en Issue #32)
# playwright
test('login flow completo', async ({ page }) => {
  // ... test E2E
});
```

---

## 📊 Comparación: Vitest vs React Testing Library

| Aspecto | Vitest | React Testing Library |
|---------|--------|----------------------|
| **Propósito** | Test runner y assertions | Utilidades de testing React |
| **Nivel** | Unit + Integration | Component |
| **Enfoque** | Lógica de negocio | Comportamiento de usuario |
| **Mocking** | vi.fn(), vi.mock() | Proveedores mock |
| **Queries** | expect() matchers | screen.getByRole(), etc. |
| **Async** | await expect().resolves | findBy*, waitFor |

**Se usan juntos:**
```typescript
import { describe, it, expect } from 'vitest';           // Test runner
import { render, screen } from '@testing-library/react'; // Component testing
import userEvent from '@testing-library/user-event';     // User interactions

describe('Component', () => {
  it('test', async () => {
    const user = userEvent.setup();
    render(<Component />);

    await user.click(screen.getByRole('button'));
    expect(screen.getByText('Success')).toBeInTheDocument();
  });
});
```

---

## 🎯 Ejemplo Completo: Testing de LoginForm

### Estructura del Test

```typescript
// LoginForm.test.tsx
import { describe, it, expect, vi } from 'vitest';
import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { LoginForm } from './LoginForm';

describe('LoginForm', () => {
  it('debería renderizar campos de email y contraseña', () => {
    render(<LoginForm onSubmit={vi.fn()} />);

    expect(screen.getByLabelText(/email/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/contraseña/i)).toBeInTheDocument();
  });

  it('debería enviar formulario con datos válidos', async () => {
    const user = userEvent.setup();
    const handleSubmit = vi.fn();

    render(<LoginForm onSubmit={handleSubmit} />);

    // Llenar formulario
    await user.type(screen.getByLabelText(/email/i), 'usuario@ejemplo.com');
    await user.type(screen.getByLabelText(/contraseña/i), 'password123');

    // Enviar
    await user.click(screen.getByRole('button', { name: /iniciar sesión/i }));

    // Verificar
    expect(handleSubmit).toHaveBeenCalledWith({
      email: 'usuario@ejemplo.com',
      password: 'password123',
    });
  });

  it('debería mostrar errores de validación', async () => {
    const user = userEvent.setup();

    render(<LoginForm onSubmit={vi.fn()} />);

    // Enviar sin llenar
    await user.click(screen.getByRole('button', { name: /iniciar sesión/i }));

    // Verificar errores
    expect(await screen.findByText(/el email es requerido/i)).toBeVisible();
    expect(screen.getByText(/la contraseña es requerida/i)).toBeVisible();
  });

  it('debería ser accesible', () => {
    render(<LoginForm onSubmit={vi.fn()} />);

    // Verificar roles ARIA
    const emailInput = screen.getByRole('textbox', { name: /email/i });
    const submitButton = screen.getByRole('button', { name: /iniciar sesión/i });

    expect(emailInput).toBeInTheDocument();
    expect(submitButton).toBeInTheDocument();
  });
});
```

### Output

```
✅ LoginForm
  ✅ debería renderizar campos de email y contraseña
  ✅ debería enviar formulario con datos válidos
  ✅ debería mostrar errores de validación
  ✅ debería ser accesible

Tests: 4 passed (4 total)
Time: 0.5s
```

---

## 📏 Mejores Prácticas

### ✅ HACER

```typescript
// 1. Consultar por rol (accesible)
screen.getByRole('button', { name: /enviar/i });

// 2. Usar userEvent (simula usuario real)
const user = userEvent.setup();
await user.type(input, 'texto');
await user.click(button);

// 3. Testear comportamiento visible
expect(screen.getByText('Éxito')).toBeVisible();

// 4. Usar findBy para async
await screen.findByText('Cargado');

// 5. Tests independientes
beforeEach(() => {
  // Setup limpio en cada test
});
```

### ❌ NO HACER

```typescript
// 1. NO consultar por clase (implementación)
container.querySelector('.btn-submit');

// 2. NO usar fireEvent (menos realista)
fireEvent.click(button);

// 3. NO testear implementación
expect(component.state.loading).toBe(false);

// 4. NO acceder a internos
expect(component.props.onClick).toHaveBeenCalled();

// 5. NO tests interdependientes
let sharedState; // Compartido entre tests
```

---

## 🔗 Integración con Testing Pyramid

### Arquitectura Completa

```
┌─────────────────────────────────────┐
│        E2E (Playwright)             │  ← Issue #32
│   User flows completos (críticos)   │
├─────────────────────────────────────┤
│    Component (Vitest + RTL)         │  ← Issue #33 (ESTE)
│   UI, interacciones, accesibilidad  │
├─────────────────────────────────────┤
│      Unit (Vitest)                  │  ← Issue #33 (ESTE)
│   Lógica de negocio, utilidades     │
├─────────────────────────────────────┤
│   Integration (Testcontainers)      │  ← Issue #27
│        API + DB                      │
└─────────────────────────────────────┘
```

### Workflow de Testing Completo

```bash
# 1. Unit tests (vitest.md)
npm test utils/

# 2. Component tests (react-testing-library.md)
npm test components/

# 3. Integration tests (testcontainers)
npm test -- --testPathPattern=integration

# 4. E2E tests (playwright)
npm run test:e2e

# 5. Coverage total
npm run test:coverage
```

---

## 📈 Métricas de Cobertura

| Métrica | Target | Herramienta |
|---------|--------|-------------|
| **Unit Coverage** | ≥ 80% | Vitest |
| **Component Coverage** | ≥ 80% | Vitest + RTL |
| **Accessibility** | 100% WCAG 2.1 AA | RTL queries |
| **E2E Critical Paths** | 100% | Playwright |
| **Visual Regression** | 0 diffs | Playwright snapshots |

---

## 🎓 Características Clave de Cada Skill

### Vitest

**Fortalezas:**
- ⚡ Ultra rápido (powered by Vite)
- 🎯 ESM y TypeScript nativos
- 🔄 Watch mode con HMR
- 🎨 UI mode visual
- 📊 Coverage integrado (v8/istanbul)

**Uso Principal:**
- Unit tests de lógica de negocio
- Tests de utilidades y helpers
- Mocking de dependencias

### React Testing Library

**Fortalezas:**
- 🎯 Testing user-centric (no implementación)
- ♿ Queries por accesibilidad (roles, labels)
- 🤖 userEvent para interacciones realistas
- 🔍 Auto-waiting (no manual waits)
- ✅ Tests mantenibles

**Uso Principal:**
- Tests de componentes React
- Validación de interacciones de usuario
- Testing de accesibilidad
- Testing de formularios

---

## ✅ Criterios de Éxito

- [x] vitest.md skill creada (622 líneas)
- [x] react-testing-library.md skill creada (570 líneas)
- [x] issue-33.md documentación creada
- [x] Todo el contenido en español (coherencia)
- [x] Patrones de testing documentados
- [x] Mejores prácticas incluidas
- [x] Anti-patterns identificados
- [x] Integración con testing pyramid explicada
- [x] Ejemplos completos proporcionados
- [x] Todos los archivos committed a feature branch
- [x] Merged a main siguiendo GitFlow
- [x] Issue documentado y cerrado

---

## 🔄 Relación con Otros Issues

### Dependencias Resueltas

- ✅ Issue #21: TDD implementer (backend testing)
- ✅ Issue #27: Testcontainers (integration testing)
- ✅ Issue #31: frontend-builder (CDD workflow)
- ✅ Issue #32: Playwright (E2E testing)

### Habilita

- Issue #34: Docker Foundation (puede usar estos tests)
- Issue #35: Docker Compose (orquestar testing stack)
- Issue #36: PostgreSQL integration (DB testing)

---

## 📚 Recursos

**Vitest:**
- Official Docs: https://vitest.dev/
- API Reference: https://vitest.dev/api/
- Migration from Jest: https://vitest.dev/guide/migration.html

**React Testing Library:**
- Official Docs: https://testing-library.com/react
- jest-dom matchers: https://github.com/testing-library/jest-dom
- user-event: https://testing-library.com/docs/user-event/intro
- Query Priority: https://testing-library.com/docs/queries/about/#priority

**Related:**
- Skills: testing/vitest.md, testing/react-testing-library.md
- Agents: frontend-builder, e2e-tester, tdd-implementer
- Commands: /mj2:2f-build, /mj2:4-e2e

**Adapted From:**
- moai-adk/frontend-testing-patterns
- Testing Library Best Practices
- Kent C. Dodds Testing Guides

**ROADMAP Reference:**
- Section: v0.3.0 - Full Stack + DevOps
- Location: docs/ROADMAP.md lines 313-329

---

## 📈 Resumen de Métricas

| Métrica | Valor |
|---------|-------|
| **Archivos Creados** | 3 (2 skills + 1 doc) |
| **Total Líneas** | 1,192 |
| **Skills** | 2 (vitest, react-testing-library) |
| **Patrones Documentados** | 15+ |
| **Ejemplos de Código** | 30+ |
| **Secciones Principales** | 20+ |
| **Idioma** | 100% Español ✅ |

---

## 🚀 Próximos Pasos (Issue #34)

Con el Frontend Testing Stack completo, los próximos pasos son:

**Issue #34:** Docker Foundation
- Docker skill comprehensivo
- Dockerfile patterns para .NET y Node.js
- Docker Compose básico
- Networking y volumes
- Containerización de apps

**Prerequisites completados:** ✅
- Backend testing (TDD) ✅
- Frontend testing (Vitest + RTL) ✅ ← **Este issue**
- Integration testing (Testcontainers) ✅
- E2E testing (Playwright) ✅
- Component testing (frontend-builder) ✅

**Ready for:**
- Issue #34: Docker Foundation
- Issue #35: Docker Compose Full Stack
- Issue #36: PostgreSQL Integration
- v0.3.0: Full-stack + DevOps

---

## 🎯 Testing Pyramid - COMPLETA

```
         ▲
        /E\          Playwright (Issue #32)
       /2E\          User flows completos
      /_____\
     /       \
    /Component\      Vitest + RTL (Issue #33) ← ESTE
   /___________\     UI + interacciones
  /             \
 /  Integration  \   Testcontainers (Issue #27)
/_________________\  API + DB
       Unit           Vitest (Issue #33) ← ESTE
                      Lógica de negocio
```

**Full Testing Stack - 100% Cubierto:**
- ✅ Unit tests (Vitest)
- ✅ Integration tests (Testcontainers)
- ✅ Component tests (Vitest + React Testing Library)
- ✅ E2E tests (Playwright)

---

**Completado por:** Claude Code
**Commit:** feature/issue-33-frontend-testing → main
**Archivos:** 3 (vitest.md, react-testing-library.md, issue-33.md)
**Líneas Añadidas:** ~1,192
**Idioma:** 100% Español ✅
**Testing Stack:** ✅ **DETAIL COMPLETO**
