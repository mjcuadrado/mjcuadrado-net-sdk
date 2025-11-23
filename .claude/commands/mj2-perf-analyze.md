---
name: mj2-perf-analyze
description: Analiza y optimiza el rendimiento de aplicaciones .NET y React
tags: [performance, optimization, profiling, benchmarking]
---

# /mj2:perf-analyze - Performance Analysis

Comando para invocar al agente **performance-engineer** y optimizar el rendimiento de tu aplicación.

---

## 📋 Uso

```bash
# Analizar rendimiento de un target específico
/mj2:perf-analyze <target>

# Ejemplos
/mj2:perf-analyze api              # Analizar backend API
/mj2:perf-analyze frontend         # Analizar React frontend
/mj2:perf-analyze database         # Analizar queries de base de datos
/mj2:perf-analyze full-stack       # Análisis completo
```

---

## 🎯 ¿Qué hace este comando?

El comando **perf-analyze** ejecuta un análisis completo de rendimiento que incluye:

1. **MEASURE** - Establecer Baseline
   - Capturar métricas actuales
   - Definir performance budgets
   - Configurar monitoring

2. **ANALYZE** - Identificar Bottlenecks
   - Profiling con dotnet-trace/dotnet-counters
   - Análisis de bundle size (frontend)
   - Identificar N+1 queries, allocations

3. **OPTIMIZE** - Aplicar Mejoras
   - Backend: EF Core, caching, compression
   - Frontend: Code splitting, memoization
   - Database: Indexes, query optimization

4. **VALIDATE** - Verificar Mejoras
   - Re-medir con benchmarks
   - Comparar contra baseline
   - Performance tests E2E

---

## 🔄 Workflow

```
📊 MEASURE
  ↓ dotnet-counters monitor (CPU, Memory, Requests)
  ↓ Establecer performance budgets
  ↓ Capturar baseline metrics
  ↓ Configurar OpenTelemetry traces

🔍 ANALYZE
  ↓ dotnet-trace collect (CPU profiling)
  ↓ Identificar hot paths
  ↓ Detectar N+1 queries (EF Core)
  ↓ Analizar allocations y GC pressure
  ↓ Bundle analysis (frontend)

⚡ OPTIMIZE
  ↓ AsNoTracking en queries read-only
  ↓ Projections vs entidades completas
  ↓ Eager loading (Include)
  ↓ Bulk operations
  ↓ Implementar caching (In-Memory/Distributed)
  ↓ Response compression (Brotli/Gzip)
  ↓ Code splitting (React.lazy)
  ↓ Memoization (React.memo, useMemo)

✅ VALIDATE
  ↓ BenchmarkDotNet (antes/después)
  ↓ Load testing con k6
  ↓ Lighthouse CI (Core Web Vitals)
  ↓ Verificar performance budgets
```

---

## 💡 Ejemplos de Uso

### Ejemplo 1: Optimizar API Backend

**Comando:**
```bash
/mj2:perf-analyze api
```

**Análisis del performance-engineer:**

**MEASURE:**
```
📊 Baseline Performance:
- API Response Time (p50): 245ms ❌ (target: < 100ms)
- API Response Time (p95): 1,234ms ❌ (target: < 200ms)
- Memory Usage: 678MB ❌ (target: < 500MB)
- CPU Usage: 62% ⚠️ (target: < 50%)
```

**ANALYZE:**
```
🔍 Bottlenecks Identificados:

1. N+1 Query Problem (85% del tiempo)
   - GetOrders() hace 1 query + N queries para customers
   - Impact: 1,050ms en p95

2. No Caching (15% del tiempo)
   - GetTopProducts() llama a DB en cada request
   - Impact: 120ms adicionales

3. Tracking Habilitado (5% del tiempo)
   - Queries read-only con tracking
   - Impact: 64ms + allocations

4. Sin Response Compression
   - Payload: 2.3MB sin comprimir
   - Impact: 180ms en network transfer
```

**OPTIMIZE:**
```csharp
// 1. Fix N+1 Query
// ANTES:
public async Task<List<OrderDto>> GetOrders()
{
    var orders = await _context.Orders.ToListAsync();
    foreach (var order in orders)
    {
        order.Customer = await _context.Customers.FindAsync(order.CustomerId);  // N+1!
    }
    return orders;
}

// DESPUÉS:
public async Task<List<OrderDto>> GetOrders()
{
    return await _context.Orders
        .AsNoTracking()  // Sin tracking
        .Include(o => o.Customer)  // Eager loading
        .Include(o => o.Items)
        .Select(o => new OrderDto  // Projection
        {
            Id = o.Id,
            CustomerName = o.Customer.Name,
            Total = o.Total
        })
        .ToListAsync();
}

// 2. Implementar Caching
public async Task<List<ProductDto>> GetTopProducts()
{
    var cacheKey = "products:top:10";

    return await _cache.GetOrCreateAsync(cacheKey, async entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);

        return await _context.Products
            .AsNoTracking()
            .OrderByDescending(p => p.SalesCount)
            .Take(10)
            .Select(p => new ProductDto { Id = p.Id, Name = p.Name })
            .ToListAsync();
    });
}

// 3. Response Compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

app.UseResponseCompression();
```

**VALIDATE:**
```
✅ Resultados Post-Optimización:

📊 Performance Metrics:
- API Response Time (p50): 42ms ✅ (82% mejora)
- API Response Time (p95): 98ms ✅ (92% mejora)
- Memory Usage: 312MB ✅ (54% mejora)
- CPU Usage: 28% ✅ (55% mejora)

🎯 Performance Budgets: CUMPLIDOS

📈 Benchmark Results:
| Métrica | Antes | Después | Mejora |
|---------|-------|---------|--------|
| GetOrders p50 | 1,050ms | 38ms | 96.4% |
| GetTopProducts p50 | 120ms | 2ms | 98.3% |
| Memory/Request | 2.4MB | 0.8MB | 66.7% |
| Payload Size | 2.3MB | 0.4MB | 82.6% |
```

### Ejemplo 2: Optimizar Frontend React

**Comando:**
```bash
/mj2:perf-analyze frontend
```

**Análisis:**

**MEASURE:**
```
📊 Core Web Vitals (Baseline):
- First Contentful Paint: 3.2s ❌ (target: < 1.5s)
- Largest Contentful Paint: 4.8s ❌ (target: < 2.5s)
- Time to Interactive: 5.6s ❌ (target: < 3s)
- Cumulative Layout Shift: 0.18 ❌ (target: < 0.1)
- Initial Bundle Size: 1.2MB ❌ (target: < 200KB)
```

**ANALYZE:**
```
🔍 Bottlenecks:

1. Bundle Size Grande (40% del problema)
   - main.js: 1.2MB
   - vendor.js: 800KB
   - Sin code splitting

2. No Lazy Loading (30%)
   - Todas las rutas cargadas en bundle inicial
   - Impact: FCP 1.8s adicional

3. Re-renders Innecesarios (20%)
   - ProductList re-renderiza en cada scroll
   - Impact: FPS drop a 30fps

4. Imágenes Sin Optimizar (10%)
   - PNG sin lazy loading
   - Impact: LCP 1.2s adicional
```

**OPTIMIZE:**
```tsx
// 1. Code Splitting
// ANTES:
import Dashboard from './pages/Dashboard';
import Settings from './pages/Settings';

// DESPUÉS:
import { lazy, Suspense } from 'react';

const Dashboard = lazy(() => import('./pages/Dashboard'));
const Settings = lazy(() => import('./pages/Settings'));

function App() {
  return (
    <Suspense fallback={<LoadingSpinner />}>
      <Routes>
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/settings" element={<Settings />} />
      </Routes>
    </Suspense>
  );
}

// 2. Memoization
// ANTES:
function ProductList({ products }) {
  return products.map(p => <ProductCard product={p} />);
}

// DESPUÉS:
const ProductCard = memo(function ProductCard({ product }) {
  return <div>{product.name}</div>;
});

function ProductList({ products }) {
  return products.map(p => <ProductCard key={p.id} product={p} />);
}

// 3. Virtual Scrolling
import { FixedSizeList } from 'react-window';

function ProductList({ products }) {
  return (
    <FixedSizeList
      height={600}
      itemCount={products.length}
      itemSize={100}
    >
      {({ index, style }) => (
        <div style={style}>
          <ProductCard product={products[index]} />
        </div>
      )}
    </FixedSizeList>
  );
}

// 4. Image Optimization
<img
  src="/images/hero-800w.webp"
  srcSet="/images/hero-400w.webp 400w,
          /images/hero-800w.webp 800w"
  loading="lazy"
  decoding="async"
  alt="Hero"
/>
```

**VALIDATE:**
```
✅ Core Web Vitals (Post-Optimización):
- First Contentful Paint: 1.2s ✅ (62% mejora)
- Largest Contentful Paint: 2.1s ✅ (56% mejora)
- Time to Interactive: 2.8s ✅ (50% mejora)
- Cumulative Layout Shift: 0.05 ✅ (72% mejora)
- Initial Bundle Size: 180KB ✅ (85% mejora)

📊 Lighthouse Score:
- Performance: 94 ✅ (antes: 42)
- Accessibility: 98
- Best Practices: 100
- SEO: 100
```

### Ejemplo 3: Optimizar Database Queries

**Comando:**
```bash
/mj2:perf-analyze database
```

**Análisis:**

**MEASURE:**
```sql
-- Queries lentas identificadas
SELECT query, mean_exec_time, calls
FROM pg_stat_statements
ORDER BY mean_exec_time DESC
LIMIT 10;

-- Top 3:
1. GetOrdersWithCustomers: 1,234ms (N+1 query)
2. GetProductsByCategory: 456ms (sin index)
3. SearchProducts: 289ms (LIKE sin full-text search)
```

**OPTIMIZE:**
```csharp
// 1. Fix N+1 (ya mostrado arriba)

// 2. Agregar Indexes
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Product>()
        .HasIndex(p => p.CategoryId);  // Para joins frecuentes

    modelBuilder.Entity<Product>()
        .HasIndex(p => p.Name);  // Para búsquedas

    modelBuilder.Entity<Order>()
        .HasIndex(o => new { o.CustomerId, o.CreatedAt });  // Composite index
}

// 3. Full-Text Search
modelBuilder.Entity<Product>()
    .HasGeneratedTsVectorColumn(
        p => p.SearchVector,
        "english",
        p => new { p.Name, p.Description })
    .HasIndex(p => p.SearchVector)
    .HasMethod("GIN");

public async Task<List<Product>> SearchProducts(string query)
{
    return await _context.Products
        .Where(p => p.SearchVector.Matches(EF.Functions.ToTsQuery("english", query)))
        .ToListAsync();
}
```

**VALIDATE:**
```
✅ Query Performance:
| Query | Antes | Después | Mejora |
|-------|-------|---------|--------|
| GetOrdersWithCustomers | 1,234ms | 38ms | 96.9% |
| GetProductsByCategory | 456ms | 12ms | 97.4% |
| SearchProducts | 289ms | 18ms | 93.8% |
```

---

## 📐 Performance Budgets

### Backend Budgets

| Métrica | Target | Max | Acción si excede |
|---------|--------|-----|------------------|
| API Response (p50) | < 100ms | < 200ms | Investigar |
| API Response (p95) | < 200ms | < 500ms | Alertar |
| DB Query Time | < 50ms | < 100ms | Optimizar |
| Memory Usage | < 500MB | < 1GB | Investigar leaks |
| CPU Usage | < 50% | < 80% | Escalar |

### Frontend Budgets

| Métrica | Target | Max | Acción si excede |
|---------|--------|-----|------------------|
| Initial Bundle | < 200KB | < 300KB | Code splitting |
| Total Bundle | < 1MB | < 2MB | Tree shaking |
| FCP | < 1.5s | < 2.5s | Optimizar critical path |
| LCP | < 2s | < 3s | Image optimization |
| TTI | < 3s | < 5s | Code splitting |
| CLS | < 0.1 | < 0.25 | Fix layout shifts |

---

## 🛠️ Herramientas Utilizadas

### Backend
```bash
# Profiling
dotnet-trace collect --process-id <PID>
dotnet-counters monitor --process-id <PID>
dotnet-dump collect --process-id <PID>

# Benchmarking
dotnet run -c Release --project Benchmarks.csproj
```

### Frontend
```bash
# Bundle analysis
npm run build
# Genera stats.html con rollup-plugin-visualizer

# Lighthouse
npm install -g @lhci/cli
lhci autorun

# Web Vitals
npm install web-vitals
```

### Load Testing
```bash
# k6
k6 run --vus 100 --duration 30s load-test.js
```

---

## 🔗 Integración con Workflow

```bash
# 1. Implementar feature (tdd-implementer)
/mj2:2-run FEATURE-001

# 2. Performance analysis (performance-engineer) ← ESTE COMANDO
/mj2:perf-analyze api

# 3. Security review (security-expert)
# Verificar que optimizaciones no comprometan security

# 4. E2E testing (e2e-tester)
/mj2:4-e2e FEATURE-E2E-001

# 5. Deploy (devops-expert)
/mj2:5-deploy staging
```

---

## ✅ Checklist de Salida

Después de ejecutar `/mj2:perf-analyze`, verifica:

### Backend
- [ ] AsNoTracking en queries read-only
- [ ] Projections implementadas
- [ ] N+1 queries eliminadas
- [ ] Caching strategy implementada
- [ ] Response compression habilitada
- [ ] Performance budgets cumplidos

### Frontend
- [ ] Code splitting implementado
- [ ] React.memo en componentes costosos
- [ ] useMemo para cálculos pesados
- [ ] Virtual scrolling para listas largas
- [ ] Images optimizadas (lazy loading, WebP)
- [ ] Core Web Vitals cumplidos

### Database
- [ ] Indexes en columnas frecuentes
- [ ] Queries optimizadas (EXPLAIN ANALYZE)
- [ ] Full-text search si aplica
- [ ] Connection pooling configurado

### Monitoring
- [ ] OpenTelemetry configurado
- [ ] Dashboards en Grafana
- [ ] Alertas configuradas
- [ ] Performance tests en CI/CD

---

## 📚 Ver También

- Agente: `.claude/agents/mj2/performance-engineer.md`
- Skills relacionadas:
  - `.claude/skills/backend/performance-optimization.md`
  - `.claude/skills/backend/caching-strategies.md`
  - `.claude/skills/observability/opentelemetry.md`
- Comandos relacionados:
  - `/mj2:2-run` - TDD implementation
  - `/mj2:4-e2e` - E2E testing
  - `/mj2:5-deploy` - Deployment

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-23
**Mantenido por:** mjcuadrado-net-sdk
