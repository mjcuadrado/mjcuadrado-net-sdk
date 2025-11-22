---
name: vite
description: Vite build tool and dev server for modern React applications
version: 0.1.0
tags: [frontend, vite, build-tool, dev-server, hmr]
---

# Vite - Next Generation Frontend Tooling

Vite es un build tool moderno que proporciona desarrollo rápido con Hot Module Replacement (HMR) instantáneo.

## 🎯 Overview

**Por qué Vite en mj2:**
- **Fast HMR:** Hot Module Replacement instantáneo
- **ESM Native:** Usa ES modules nativos en desarrollo
- **Optimized Build:** Producción optimizada con Rollup
- **Plugin Ecosystem:** Amplio ecosistema de plugins
- **TypeScript Support:** Soporte nativo para TypeScript

---

## 📦 Setup

```bash
npm create vite@latest my-app -- --template react-ts
cd my-app
npm install
npm run dev
```

**vite.config.ts:**
```typescript
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path'

export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
      '@components': path.resolve(__dirname, './src/components'),
      '@hooks': path.resolve(__dirname, './src/hooks'),
      '@utils': path.resolve(__dirname, './src/utils')
    }
  },
  server: {
    port: 3000,
    open: true,
    proxy: {
      '/api': {
        target: 'http://localhost:5000',
        changeOrigin: true
      }
    }
  },
  build: {
    outDir: 'dist',
    sourcemap: true,
    rollupOptions: {
      output: {
        manualChunks: {
          vendor: ['react', 'react-dom'],
          mui: ['@mui/material']
        }
      }
    }
  }
})
```

---

## 🚀 Development

```bash
npm run dev     # Start dev server
npm run build   # Production build
npm run preview # Preview production build
```

---

## 🔌 Plugins

```typescript
import react from '@vitejs/plugin-react'
import svgr from 'vite-plugin-svgr'

export default defineConfig({
  plugins: [
    react(),
    svgr() // Import SVG as React components
  ]
})
```

---

## 📁 Project Structure

```
src/
├── assets/      # Static assets
├── components/  # React components
├── hooks/       # Custom hooks
├── pages/       # Page components
├── utils/       # Utilities
├── App.tsx
└── main.tsx
```

---

## ✅ Best Practices

### DO ✅
1. **Path aliases** - @/ para imports limpios
2. **Environment variables** - VITE_ prefix
3. **Code splitting** - manualChunks en build
4. **Proxy API** - Evitar CORS en desarrollo

### DON'T ❌
1. ❌ NO usar create-react-app (obsoleto)
2. ❌ NO importar archivos grandes en HMR
3. ❌ NO olvidar .env.example

---

**Used by:** frontend-builder
**Related skills:** frontend/react.md, frontend/typescript.md
