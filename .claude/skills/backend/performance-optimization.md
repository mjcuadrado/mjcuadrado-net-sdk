---
skill: performance-optimization
description: Optimización de rendimiento para aplicaciones .NET y React
tags: [performance, optimization, backend, frontend, profiling]
applies_to: [.NET 9+, React 18+, ASP.NET Core, EF Core 9]
---

# Performance Optimization

Guía completa de optimización de rendimiento para aplicaciones full-stack .NET y React.

---

## 📋 Tabla de Contenidos

1. [Backend Performance (.NET)](#backend-performance)
2. [Frontend Performance (React)](#frontend-performance)
3. [Performance Metrics](#performance-metrics)
4. [Profiling Tools](#profiling-tools)
5. [Performance Budgets](#performance-budgets)

---

## 🎯 Backend Performance (.NET)

### 1. EF Core Optimization

#### AsNoTracking para Queries Read-Only

```csharp
// ❌ MAL: Tracking innecesario para read-only
public async Task<List<ProductDto>> GetProducts()
{
    return await _context.Products
        .Select(p => new ProductDto { Id = p.Id, Name = p.Name })
        .ToListAsync();
}

// ✅ BIEN: AsNoTracking para mejor rendimiento
public async Task<List<ProductDto>> GetProducts()
{
    return await _context.Products
        .AsNoTracking()  // 30-40% más rápido
        .Select(p => new ProductDto { Id = p.Id, Name = p.Name })
        .ToListAsync();
}
```

**Beneficios:**
- 30-40% más rápido en queries read-only
- Menor uso de memoria (no tracking change tracker)
- Ideal para DTOs y projections

#### Projections vs Entidades Completas

```csharp
// ❌ MAL: Cargar entidad completa
public async Task<ProductDto> GetProduct(int id)
{
    var product = await _context.Products.FindAsync(id);
    return new ProductDto { Id = product.Id, Name = product.Name };
}

// ✅ BIEN: Projection directa
public async Task<ProductDto> GetProduct(int id)
{
    return await _context.Products
        .AsNoTracking()
        .Where(p => p.Id == id)
        .Select(p => new ProductDto { Id = p.Id, Name = p.Name })
        .FirstOrDefaultAsync();
}
```

**Beneficios:**
- Solo trae las columnas necesarias
- Menor transferencia de datos
- Mejor rendimiento en queries complejas

#### Avoid N+1 Queries

```csharp
// ❌ MAL: N+1 query problem
public async Task<List<OrderDto>> GetOrders()
{
    var orders = await _context.Orders.ToListAsync();

    foreach (var order in orders)
    {
        // 1 query por cada order! (N+1)
        order.Customer = await _context.Customers.FindAsync(order.CustomerId);
    }

    return orders;
}

// ✅ BIEN: Eager loading con Include
public async Task<List<OrderDto>> GetOrders()
{
    return await _context.Orders
        .AsNoTracking()
        .Include(o => o.Customer)
        .Include(o => o.Items)
            .ThenInclude(i => i.Product)
        .Select(o => new OrderDto
        {
            Id = o.Id,
            CustomerName = o.Customer.Name,
            Items = o.Items.Select(i => new OrderItemDto
            {
                ProductName = i.Product.Name,
                Quantity = i.Quantity
            }).ToList()
        })
        .ToListAsync();
}

// ✅ ALTERNATIVA: Split queries para relaciones 1-N
public async Task<List<OrderDto>> GetOrders()
{
    return await _context.Orders
        .AsNoTracking()
        .AsSplitQuery()  // Genera múltiples queries optimizadas
        .Include(o => o.Customer)
        .Include(o => o.Items)
        .ToListAsync();
}
```

#### Bulk Operations

```csharp
// ❌ MAL: Inserción individual (lento)
public async Task AddProducts(List<Product> products)
{
    foreach (var product in products)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();  // 1 round-trip por producto!
    }
}

// ✅ BIEN: Bulk insert
public async Task AddProducts(List<Product> products)
{
    await _context.Products.AddRangeAsync(products);
    await _context.SaveChangesAsync();  // 1 solo round-trip
}

// ✅ MEJOR: Bulk operations con EFCore.BulkExtensions
public async Task AddProducts(List<Product> products)
{
    await _context.BulkInsertAsync(products);  // Mucho más rápido
}

// ✅ Bulk update
public async Task UpdatePrices(Dictionary<int, decimal> priceUpdates)
{
    await _context.Products
        .Where(p => priceUpdates.Keys.Contains(p.Id))
        .ExecuteUpdateAsync(setters => setters
            .SetProperty(p => p.Price, p => priceUpdates[p.Id])
            .SetProperty(p => p.UpdatedAt, DateTime.UtcNow)
        );
}
```

### 2. Async/Await Best Practices

#### Evitar Async Over Sync

```csharp
// ❌ MAL: Async over sync (bloquea threads)
public async Task<string> GetData()
{
    return await Task.Run(() =>
    {
        Thread.Sleep(1000);  // Bloqueante!
        return "data";
    });
}

// ✅ BIEN: Async nativo
public async Task<string> GetData()
{
    await Task.Delay(1000);  // Non-blocking
    return "data";
}
```

#### ConfigureAwait en Libraries

```csharp
// ✅ En bibliotecas: ConfigureAwait(false)
public async Task<User> GetUserAsync(int id)
{
    var response = await _httpClient
        .GetAsync($"/api/users/{id}")
        .ConfigureAwait(false);  // No captura SynchronizationContext

    return await response.Content
        .ReadFromJsonAsync<User>()
        .ConfigureAwait(false);
}
```

#### ValueTask para Hot Paths

```csharp
// ✅ ValueTask para operaciones que pueden ser síncronas
public ValueTask<User?> GetUserFromCacheAsync(int id)
{
    if (_cache.TryGetValue(id, out var user))
    {
        return new ValueTask<User?>(user);  // Completado síncronamente
    }

    return new ValueTask<User?>(GetUserFromDbAsync(id));  // Async
}

private async Task<User?> GetUserFromDbAsync(int id)
{
    return await _context.Users.FindAsync(id);
}
```

### 3. Response Compression

```csharp
// Program.cs
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();

    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/json", "text/plain" });
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

var app = builder.Build();

app.UseResponseCompression();  // ANTES de otros middlewares
```

**Beneficios:**
- Brotli: 15-20% mejor compresión que Gzip
- Reduce bandwidth hasta 70-80%
- Mejora tiempo de carga

### 4. Connection Pooling

```csharp
// ✅ Connection pooling configurado
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorCodesToAdd: null);

        // Connection pooling automático en Npgsql
        // Max pool size: 100 (default)
        // Min pool size: 0 (default)
    });
});

// ✅ HttpClient con pooling
builder.Services.AddHttpClient<IExternalApiClient, ExternalApiClient>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5));  // Reutiliza connections
```

### 5. Minimal APIs vs Controllers

```csharp
// ✅ Minimal APIs: Mejor rendimiento
app.MapGet("/api/products", async (AppDbContext db) =>
{
    return await db.Products
        .AsNoTracking()
        .Select(p => new { p.Id, p.Name })
        .ToListAsync();
});

// vs Controllers (más overhead)
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromServices] AppDbContext db)
    {
        var products = await db.Products
            .AsNoTracking()
            .Select(p => new { p.Id, p.Name })
            .ToListAsync();

        return Ok(products);
    }
}
```

**Benchmarks:**
- Minimal APIs: ~30% menos allocations
- Minimal APIs: ~15-20% mejor throughput
- Controllers: Mejor para APIs complejas con validación

---

## ⚛️ Frontend Performance (React)

### 1. Code Splitting & Lazy Loading

```tsx
// ❌ MAL: Bundle único grande
import Dashboard from './pages/Dashboard';
import Settings from './pages/Settings';
import Profile from './pages/Profile';

function App() {
  return (
    <Routes>
      <Route path="/dashboard" element={<Dashboard />} />
      <Route path="/settings" element={<Settings />} />
      <Route path="/profile" element={<Profile />} />
    </Routes>
  );
}

// ✅ BIEN: Code splitting con React.lazy
import { lazy, Suspense } from 'react';

const Dashboard = lazy(() => import('./pages/Dashboard'));
const Settings = lazy(() => import('./pages/Settings'));
const Profile = lazy(() => import('./pages/Profile'));

function App() {
  return (
    <Suspense fallback={<LoadingSpinner />}>
      <Routes>
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/settings" element={<Settings />} />
        <Route path="/profile" element={<Profile />} />
      </Routes>
    </Suspense>
  );
}
```

**Beneficios:**
- Reduce bundle inicial hasta 70-80%
- Carga componentes bajo demanda
- Mejora First Contentful Paint (FCP)

### 2. React.memo & useMemo

```tsx
// ❌ MAL: Re-render innecesario
function ProductList({ products, onAddToCart }) {
  return (
    <div>
      {products.map(product => (
        <ProductCard
          key={product.id}
          product={product}
          onAddToCart={onAddToCart}
        />
      ))}
    </div>
  );
}

// ✅ BIEN: React.memo evita re-renders
const ProductCard = memo(function ProductCard({ product, onAddToCart }) {
  return (
    <div>
      <h3>{product.name}</h3>
      <p>${product.price}</p>
      <button onClick={() => onAddToCart(product.id)}>
        Add to Cart
      </button>
    </div>
  );
});

// ✅ useMemo para cálculos costosos
function ShoppingCart({ items }) {
  const total = useMemo(() => {
    return items.reduce((sum, item) => sum + item.price * item.quantity, 0);
  }, [items]);

  const tax = useMemo(() => total * 0.15, [total]);

  return (
    <div>
      <p>Subtotal: ${total}</p>
      <p>Tax: ${tax}</p>
      <p>Total: ${total + tax}</p>
    </div>
  );
}
```

### 3. useCallback para Event Handlers

```tsx
// ❌ MAL: Nueva función en cada render
function ProductList({ products }) {
  return (
    <div>
      {products.map(product => (
        <ProductCard
          key={product.id}
          product={product}
          onAddToCart={(id) => console.log('Added', id)}  // Nueva función!
        />
      ))}
    </div>
  );
}

// ✅ BIEN: useCallback estabiliza la función
function ProductList({ products }) {
  const handleAddToCart = useCallback((id) => {
    console.log('Added', id);
  }, []);

  return (
    <div>
      {products.map(product => (
        <ProductCard
          key={product.id}
          product={product}
          onAddToCart={handleAddToCart}  // Misma referencia
        />
      ))}
    </div>
  );
}
```

### 4. Virtual Scrolling

```tsx
// ❌ MAL: Renderizar 10,000 items (lento)
function ProductList({ products }) {
  return (
    <div>
      {products.map(product => (
        <ProductCard key={product.id} product={product} />
      ))}
    </div>
  );
}

// ✅ BIEN: Virtual scrolling con react-window
import { FixedSizeList } from 'react-window';

function ProductList({ products }) {
  const Row = ({ index, style }) => (
    <div style={style}>
      <ProductCard product={products[index]} />
    </div>
  );

  return (
    <FixedSizeList
      height={600}
      itemCount={products.length}
      itemSize={100}
      width="100%"
    >
      {Row}
    </FixedSizeList>
  );
}
```

**Beneficios:**
- Solo renderiza elementos visibles
- Maneja listas de 100k+ items sin problemas
- Mejora FPS y reduce memory usage

### 5. Image Optimization

```tsx
// ❌ MAL: Imagen sin optimizar
<img src="/images/hero.jpg" alt="Hero" />

// ✅ BIEN: Lazy loading + srcSet
<img
  src="/images/hero-800w.jpg"
  srcSet="/images/hero-400w.jpg 400w,
          /images/hero-800w.jpg 800w,
          /images/hero-1200w.jpg 1200w"
  sizes="(max-width: 600px) 400px,
         (max-width: 1200px) 800px,
         1200px"
  alt="Hero"
  loading="lazy"
  decoding="async"
/>

// ✅ MEJOR: WebP con fallback
<picture>
  <source srcSet="/images/hero.webp" type="image/webp" />
  <source srcSet="/images/hero.jpg" type="image/jpeg" />
  <img src="/images/hero.jpg" alt="Hero" loading="lazy" />
</picture>
```

### 6. Bundle Optimization (Vite)

```typescript
// vite.config.ts
export default defineConfig({
  build: {
    rollupOptions: {
      output: {
        manualChunks: {
          // Vendor chunks
          'react-vendor': ['react', 'react-dom', 'react-router-dom'],
          'mui-vendor': ['@mui/material', '@mui/icons-material'],
          'query-vendor': ['@tanstack/react-query'],

          // Feature chunks
          'dashboard': ['./src/pages/Dashboard'],
          'settings': ['./src/pages/Settings'],
        },
      },
    },
    chunkSizeWarningLimit: 1000,
  },
});
```

---

## 📊 Performance Metrics

### Core Web Vitals

```typescript
// Medir Core Web Vitals
import { getCLS, getFID, getLCP, getFCP, getTTFB } from 'web-vitals';

function sendToAnalytics(metric: Metric) {
  // Enviar a analytics
  console.log(metric.name, metric.value);
}

getCLS(sendToAnalytics);  // Cumulative Layout Shift (< 0.1)
getFID(sendToAnalytics);  // First Input Delay (< 100ms)
getLCP(sendToAnalytics);  // Largest Contentful Paint (< 2.5s)
getFCP(sendToAnalytics);  // First Contentful Paint (< 1.8s)
getTTFB(sendToAnalytics); // Time to First Byte (< 600ms)
```

### Backend Metrics (.NET)

```csharp
// Métricas con OpenTelemetry
using var meter = new Meter("MyApp.Performance");

var requestCounter = meter.CreateCounter<long>("http.requests");
var requestDuration = meter.CreateHistogram<double>("http.request.duration");

app.Use(async (context, next) =>
{
    var sw = Stopwatch.StartNew();

    await next();

    sw.Stop();

    requestCounter.Add(1, new KeyValuePair<string, object?>("method", context.Request.Method));
    requestDuration.Record(sw.ElapsedMilliseconds,
        new KeyValuePair<string, object?>("endpoint", context.Request.Path));
});
```

---

## 🔍 Profiling Tools

### Backend (.NET)

**dotnet-trace**
```bash
# Capturar trace
dotnet-trace collect --process-id 1234 --providers Microsoft-DotNETCore-SampleProfiler

# Analizar con PerfView o Visual Studio
```

**dotnet-counters**
```bash
# Monitorear en tiempo real
dotnet-counters monitor --process-id 1234 --counters System.Runtime,Microsoft.AspNetCore.Hosting

# Métricas:
# - CPU usage
# - Memory usage (GC heap)
# - Request rate
# - Exception rate
```

**BenchmarkDotNet**
```csharp
[MemoryDiagnoser]
public class PerformanceBenchmarks
{
    [Benchmark]
    public async Task<List<ProductDto>> GetProducts_WithTracking()
    {
        return await _context.Products.ToListAsync();
    }

    [Benchmark]
    public async Task<List<ProductDto>> GetProducts_NoTracking()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }
}
```

### Frontend (React)

**React DevTools Profiler**
```tsx
import { Profiler } from 'react';

function onRenderCallback(
  id: string,
  phase: 'mount' | 'update',
  actualDuration: number,
) {
  console.log(`${id} (${phase}) took ${actualDuration}ms`);
}

<Profiler id="ProductList" onRender={onRenderCallback}>
  <ProductList products={products} />
</Profiler>
```

**Lighthouse CI**
```bash
# Lighthouse en CI
npm install -g @lhci/cli

lhci autorun --config=lighthouserc.json
```

**Bundle Analyzer**
```bash
# Vite bundle analyzer
npm install -D rollup-plugin-visualizer

# En vite.config.ts
import { visualizer } from 'rollup-plugin-visualizer';

plugins: [
  visualizer({ open: true })
]
```

---

## 💰 Performance Budgets

### Backend Performance Budgets

| Métrica | Target | Max |
|---------|--------|-----|
| API Response Time (p50) | < 100ms | < 200ms |
| API Response Time (p95) | < 200ms | < 500ms |
| Database Query Time | < 50ms | < 100ms |
| Memory Usage | < 500MB | < 1GB |
| CPU Usage | < 50% | < 80% |

### Frontend Performance Budgets

| Métrica | Target | Max |
|---------|--------|-----|
| Initial Bundle Size | < 200KB | < 300KB |
| Total Bundle Size | < 1MB | < 2MB |
| First Contentful Paint (FCP) | < 1.5s | < 2.5s |
| Largest Contentful Paint (LCP) | < 2s | < 3s |
| Time to Interactive (TTI) | < 3s | < 5s |
| Cumulative Layout Shift (CLS) | < 0.1 | < 0.25 |

---

## ✅ Checklist de Optimización

### Backend
- [ ] AsNoTracking en queries read-only
- [ ] Projections en lugar de entidades completas
- [ ] Eager loading (Include) para evitar N+1
- [ ] Bulk operations para inserciones/actualizaciones masivas
- [ ] Response compression (Brotli/Gzip)
- [ ] Connection pooling configurado
- [ ] Async/await sin blocking
- [ ] Minimal APIs para hot paths

### Frontend
- [ ] Code splitting con React.lazy
- [ ] React.memo para componentes costosos
- [ ] useMemo para cálculos pesados
- [ ] useCallback para event handlers
- [ ] Virtual scrolling para listas largas
- [ ] Image optimization (lazy loading, WebP, srcSet)
- [ ] Bundle optimization (code splitting, tree shaking)
- [ ] Core Web Vitals monitorizados

### Monitoring
- [ ] OpenTelemetry configurado
- [ ] Performance metrics en producción
- [ ] Alertas para degradación de rendimiento
- [ ] Profiling regular con dotnet-trace
- [ ] Lighthouse CI en pipeline

---

## 📚 Referencias

- **EF Core Performance:** https://learn.microsoft.com/en-us/ef/core/performance/
- **React Performance:** https://react.dev/learn/render-and-commit
- **Web Vitals:** https://web.dev/vitals/
- **BenchmarkDotNet:** https://benchmarkdotnet.org/
- **React DevTools:** https://react.dev/learn/react-developer-tools

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-23
**Mantenido por:** mjcuadrado-net-sdk
