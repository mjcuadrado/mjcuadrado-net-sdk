---
name: react-testing-library
description: Testing de componentes React centrado en el usuario - testea comportamiento, no implementación
version: 0.1.0
tags: [testing, react, rtl, component-testing, user-centric]
---

# React Testing Library Skill

## 📚 Resumen

**React Testing Library (RTL)** es una biblioteca de testing ligera para React que fomenta mejores prácticas de testing al enfocarse en probar componentes de la forma en que los usuarios interactúan con ellos.

**Filosofía:**
- Testear comportamiento de usuario, no detalles de implementación
- Consultar por atributos de accesibilidad
- Evitar testear estado interno
- Escribir tests mantenibles y resistentes a refactorización

---

## 🚀 Instalación

```bash
npm install -D @testing-library/react @testing-library/jest-dom @testing-library/user-event
```

**Con Vitest:**
```typescript
// vitest.config.ts
import { defineConfig } from 'vitest/config';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  test: {
    environment: 'jsdom',
    setupFiles: ['./src/test/setup.ts'],
    globals: true,
  },
});
```

**Archivo de setup:**
```typescript
// src/test/setup.ts
import { expect, afterEach } from 'vitest';
import { cleanup } from '@testing-library/react';
import matchers from '@testing-library/jest-dom/matchers';

expect.extend(matchers);

afterEach(() => {
  cleanup();
});
```

---

## ✅ Uso Básico

### Renderizar Componentes

```typescript
import { render, screen } from '@testing-library/react';
import { Button } from './Button';

it('debería renderizar el botón', () => {
  render(<Button>Click aquí</Button>);

  const button = screen.getByRole('button', { name: /click aquí/i });
  expect(button).toBeInTheDocument();
});
```

### Con Props

```typescript
it('debería renderizar con texto personalizado', () => {
  render(<Button variant="primary">Enviar</Button>);

  expect(screen.getByRole('button')).toHaveTextContent('Enviar');
  expect(screen.getByRole('button')).toHaveClass('btn-primary');
});
```

---

## 🔍 Métodos de Consulta

### Prioridad de Consultas (Orden Recomendado)

1. **`getByRole`** - Más accesible (roles ARIA)
2. **`getByLabelText`** - Formularios (labels asociadas)
3. **`getByPlaceholderText`** - Placeholders de inputs
4. **`getByText`** - Contenido de texto visible
5. **`getByDisplayValue`** - Valores actuales de inputs
6. **`getByAltText`** - Imágenes (atributo alt)
7. **`getByTitle`** - Atributo title
8. **`getByTestId`** - Último recurso (data-testid)

### Variantes de Consultas

```typescript
// getBy* - Lanza error si no encuentra (por defecto)
const button = screen.getByRole('button');

// queryBy* - Retorna null si no encuentra (verificar ausencia)
const button = screen.queryByRole('button');
expect(button).not.toBeInTheDocument();

// findBy* - Retorna promesa, espera por el elemento (async)
const button = await screen.findByRole('button');

// getAllBy*, queryAllBy*, findAllBy* - Múltiples elementos
const buttons = screen.getAllByRole('button');
expect(buttons).toHaveLength(3);
```

---

## 🎯 Consultas Comunes

### Por Rol (MEJOR)

```typescript
// Botones
screen.getByRole('button', { name: /enviar/i });

// Enlaces
screen.getByRole('link', { name: /inicio/i });

// Inputs
screen.getByRole('textbox', { name: /email/i });
screen.getByRole('checkbox', { name: /recordarme/i });

// Encabezados
screen.getByRole('heading', { name: /bienvenido/i });
screen.getByRole('heading', { level: 1 });

// Otros roles
screen.getByRole('alert');
screen.getByRole('dialog');
screen.getByRole('navigation');
```

### Por Label (Formularios)

```typescript
// Input con <label>
screen.getByLabelText(/email/i);
screen.getByLabelText('Contraseña');

// Funciona también con aria-label
<button aria-label="Cerrar">×</button>
screen.getByLabelText(/cerrar/i);
```

### Por Texto

```typescript
// Coincidencia exacta
screen.getByText('Bienvenido de nuevo');

// Regex (case-insensitive)
screen.getByText(/bienvenido de nuevo/i);

// Función
screen.getByText((content, element) => {
  return content.startsWith('Bienvenido');
});

// Coincidencia parcial
screen.getByText('Bienvenido', { exact: false });
```

### Por Test ID (Último Recurso)

```typescript
<div data-testid="elemento-personalizado">Contenido</div>

screen.getByTestId('elemento-personalizado');
```

---

## 👤 Interacciones de Usuario

### userEvent (Recomendado)

```typescript
import userEvent from '@testing-library/user-event';

it('debería manejar entrada de usuario', async () => {
  const user = userEvent.setup();

  render(<LoginForm />);

  // Escribir
  await user.type(screen.getByLabelText(/email/i), 'usuario@ejemplo.com');

  // Hacer clic
  await user.click(screen.getByRole('button', { name: /enviar/i }));

  // Limpiar
  await user.clear(screen.getByLabelText(/email/i));

  // Seleccionar opción
  await user.selectOptions(screen.getByLabelText(/país/i), 'España');

  // Subir archivo
  const file = new File(['contenido'], 'test.png', { type: 'image/png' });
  await user.upload(screen.getByLabelText(/subir/i), file);

  // Teclado
  await user.keyboard('{Enter}');
  await user.keyboard('{Shift>}A{/Shift}'); // Shift+A
});
```

### fireEvent (Menos Recomendado)

```typescript
import { fireEvent } from '@testing-library/react';

// Solo usar cuando userEvent no funcione
fireEvent.click(button);
fireEvent.change(input, { target: { value: 'texto' } });
```

---

## ⏰ Testing Asíncrono

### Consultas findBy

```typescript
it('debería mostrar loading y luego datos', async () => {
  render(<UserProfile userId={1} />);

  // Espera a que aparezca el elemento (timeout por defecto: 1000ms)
  const username = await screen.findByText('Juan Pérez');
  expect(username).toBeInTheDocument();
});
```

### waitFor

```typescript
import { waitFor } from '@testing-library/react';

it('debería actualizar después de llamada API', async () => {
  render(<DataFetcher />);

  await waitFor(() => {
    expect(screen.getByText('Cargado')).toBeInTheDocument();
  });

  // Con timeout personalizado
  await waitFor(
    () => {
      expect(screen.getByText('Cargado')).toBeInTheDocument();
    },
    { timeout: 3000 }
  );
});
```

### waitForElementToBeRemoved

```typescript
it('debería eliminar spinner de carga', async () => {
  render(<DataFetcher />);

  const spinner = screen.getByRole('status', { name: /cargando/i });

  await waitForElementToBeRemoved(spinner);

  expect(screen.getByText('Datos cargados')).toBeInTheDocument();
});
```

---

## 🎨 Render Personalizado

### Con Proveedores

```typescript
// test/helpers.tsx
import { render, RenderOptions } from '@testing-library/react';
import { ThemeProvider } from '@mui/material';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

function TodosLosProveedores({ children }: { children: React.ReactNode }) {
  const queryClient = new QueryClient({
    defaultOptions: {
      queries: { retry: false },
    },
  });

  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider theme={theme}>
        {children}
      </ThemeProvider>
    </QueryClientProvider>
  );
}

export function renderConProveedores(
  ui: React.ReactElement,
  options?: Omit<RenderOptions, 'wrapper'>
) {
  return render(ui, { wrapper: TodosLosProveedores, ...options });
}

export * from '@testing-library/react';
```

**Uso:**
```typescript
import { renderConProveedores, screen } from '../test/helpers';

it('debería renderizar con tema', () => {
  renderConProveedores(<ComponenteConTema />);
  expect(screen.getByText('Tematizado')).toBeInTheDocument();
});
```

---

## 🧪 Patrones de Testing

### Testing de Formularios

```typescript
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
```

### Mocking de API

```typescript
import { http, HttpResponse } from 'msw';
import { setupServer } from 'msw/node';

const server = setupServer(
  http.get('/api/usuario', () => {
    return HttpResponse.json({ id: 1, nombre: 'Juan' });
  })
);

beforeAll(() => server.listen());
afterEach(() => server.resetHandlers());
afterAll(() => server.close());

it('debería obtener y mostrar usuario', async () => {
  render(<PerfilUsuario />);

  expect(await screen.findByText('Juan')).toBeInTheDocument();
});

it('debería manejar error de API', async () => {
  server.use(
    http.get('/api/usuario', () => {
      return HttpResponse.error();
    })
  );

  render(<PerfilUsuario />);

  expect(await screen.findByText(/error al cargar usuario/i)).toBeInTheDocument();
});
```

### Testing de Hooks

```typescript
import { renderHook, waitFor } from '@testing-library/react';
import { useContador } from './useContador';

it('debería incrementar contador', () => {
  const { result } = renderHook(() => useContador());

  expect(result.current.count).toBe(0);

  result.current.incrementar();

  expect(result.current.count).toBe(1);
});

it('debería manejar actualizaciones async', async () => {
  const { result } = renderHook(() => useDatosAsync());

  await waitFor(() => {
    expect(result.current.datos).toBeDefined();
  });
});
```

---

## 🎯 Aserciones (jest-dom)

### Visibilidad

```typescript
expect(elemento).toBeVisible();
expect(elemento).toBeInTheDocument();
expect(elemento).not.toBeInTheDocument();
```

### Contenido

```typescript
expect(elemento).toHaveTextContent('Hola');
expect(elemento).toHaveTextContent(/hola/i);
expect(elemento).toContainHTML('<span>Hola</span>');
```

### Atributos

```typescript
expect(elemento).toHaveAttribute('disabled');
expect(elemento).toHaveAttribute('href', '/inicio');
expect(elemento).toHaveClass('btn-primary');
expect(elemento).toHaveStyle({ color: 'red' });
```

### Elementos de Formulario

```typescript
expect(input).toHaveValue('texto');
expect(input).toBeDisabled();
expect(input).toBeEnabled();
expect(checkbox).toBeChecked();
expect(input).toHaveFocus();
expect(input).toBeInvalid();
expect(input).toBeValid();
```

---

## 📏 Mejores Prácticas

### HACER ✅

```typescript
// Consultar por rol (accesible)
screen.getByRole('button', { name: /enviar/i });

// Usar userEvent
const user = userEvent.setup();
await user.click(button);

// Testear comportamiento de usuario
expect(screen.getByText('Éxito')).toBeVisible();

// Usar findBy para async
await screen.findByText('Cargado');
```

### NO HACER ❌

```typescript
// Consultar por clase (implementación)
container.querySelector('.btn-enviar');

// Usar fireEvent
fireEvent.click(button);

// Testear implementación
expect(component.state.loading).toBe(false);

// Acceder a internos
expect(component.props.onClick).toHaveBeenCalled();
```

---

## 🔗 Integración con mjcuadrado-net-sdk

### Con frontend-builder

```typescript
// Generado por /mj2:2f-build
import { describe, it, expect } from 'vitest';
import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { LoginForm } from './LoginForm';

describe('LoginForm', () => {
  it('debería renderizar campos de email y contraseña', () => {
    render(<LoginForm onSubmit={vi.fn()} />);

    expect(screen.getByLabelText(/email/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/contraseña/i)).toBeInTheDocument();
  });

  it('debería manejar envío válido', async () => {
    const user = userEvent.setup();
    const handleSubmit = vi.fn();

    render(<LoginForm onSubmit={handleSubmit} />);

    await user.type(screen.getByLabelText(/email/i), 'usuario@ejemplo.com');
    await user.type(screen.getByLabelText(/contraseña/i), 'password123');
    await user.click(screen.getByRole('button', { name: /iniciar sesión/i }));

    expect(handleSubmit).toHaveBeenCalledWith({
      email: 'usuario@ejemplo.com',
      password: 'password123',
    });
  });
});
```

---

## 📚 Recursos

**Documentación Oficial:**
- React Testing Library: https://testing-library.com/react
- jest-dom: https://github.com/testing-library/jest-dom
- user-event: https://testing-library.com/docs/user-event/intro

**Guías:**
- Prioridad de Consultas: https://testing-library.com/docs/queries/about/#priority
- Errores Comunes: https://kentcdodds.com/blog/common-mistakes-with-react-testing-library

**Skills Relacionadas:**
- testing/vitest.md - Test runner
- testing/playwright.md - Testing E2E

---

**Versión:** 0.1.0
**Última Actualización:** 2025-11-22
**Mantenido por:** mjcuadrado-net-sdk
