---
skill: caching-strategies
description: Estrategias de caching para aplicaciones .NET - In-Memory, Distributed, CDN
tags: [caching, performance, redis, cdn, memory]
applies_to: [.NET 9+, ASP.NET Core, Redis, SQL Server]
---

# Caching Strategies

Guía completa de estrategias de caching para maximizar el rendimiento de aplicaciones .NET.

---

## 📋 Tabla de Contenidos

1. [Tipos de Caching](#tipos-de-caching)
2. [In-Memory Caching](#in-memory-caching)
3. [Distributed Caching](#distributed-caching)
4. [CDN & Browser Caching](#cdn-browser-caching)
5. [Cache Patterns](#cache-patterns)
6. [Cache Invalidation](#cache-invalidation)

---

## 🎯 Tipos de Caching

### Comparación de Estrategias

| Tipo | Scope | Latencia | Durabilidad | Uso |
|------|-------|----------|-------------|-----|
| **In-Memory** | Proceso | < 1ms | Volátil | Hot data, session |
| **Distributed** | Aplicación | 1-5ms | Persistente | Multi-instance |
| **CDN** | Global | 10-50ms | Persistente | Static assets |
| **Browser** | Cliente | 0ms | Volátil | Static files |

---

## 💾 In-Memory Caching

### 1. IMemoryCache Básico

```csharp
// Configuración
builder.Services.AddMemoryCache();

// Uso en servicio
public class ProductService
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _context;

    public ProductService(IMemoryCache cache, AppDbContext context)
    {
        _cache = cache;
        _context = context;
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        var cacheKey = $"product:{id}";

        // Intenta obtener del cache
        if (_cache.TryGetValue(cacheKey, out Product? cachedProduct))
        {
            return cachedProduct;
        }

        // Si no está en cache, obtener de DB
        var product = await _context.Products.FindAsync(id);

        if (product != null)
        {
            // Guardar en cache por 5 minutos
            _cache.Set(cacheKey, product, TimeSpan.FromMinutes(5));
        }

        return product;
    }
}
```

### 2. Cache Options Avanzadas

```csharp
public async Task<Product?> GetProductAsync(int id)
{
    var cacheKey = $"product:{id}";

    return await _cache.GetOrCreateAsync(cacheKey, async entry =>
    {
        // Configuración de expiración
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);  // Expira en 10 min
        entry.SlidingExpiration = TimeSpan.FromMinutes(5);  // Renueva si se accede

        // Priority
        entry.Priority = CacheItemPriority.High;  // Alta prioridad

        // Callback cuando se remueve
        entry.RegisterPostEvictionCallback((key, value, reason, state) =>
        {
            Console.WriteLine($"Cache evicted: {key}, Reason: {reason}");
        });

        // Obtener de DB
        return await _context.Products.FindAsync(id);
    });
}
```

**Cache Entry Options:**
- **AbsoluteExpiration:** Fecha absoluta de expiración
- **AbsoluteExpirationRelativeToNow:** Tiempo relativo desde ahora
- **SlidingExpiration:** Renueva si se accede antes de expirar
- **Priority:** Normal, Low, High, NeverRemove

### 3. Cache Aside Pattern

```csharp
public class ProductCacheService
{
    private readonly IMemoryCache _cache;
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductCacheService> _logger;

    public async Task<Product?> GetProductAsync(int id)
    {
        var cacheKey = $"product:{id}";

        // 1. Intentar leer del cache
        if (_cache.TryGetValue(cacheKey, out Product? product))
        {
            _logger.LogInformation("Cache hit: {CacheKey}", cacheKey);
            return product;
        }

        // 2. Cache miss - leer de la fuente
        _logger.LogInformation("Cache miss: {CacheKey}", cacheKey);
        product = await _repository.GetByIdAsync(id);

        if (product != null)
        {
            // 3. Escribir en cache
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };

            _cache.Set(cacheKey, product, cacheOptions);
        }

        return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
        // 1. Actualizar en DB
        await _repository.UpdateAsync(product);

        // 2. Invalidar cache
        var cacheKey = $"product:{product.Id}";
        _cache.Remove(cacheKey);

        _logger.LogInformation("Cache invalidated: {CacheKey}", cacheKey);
    }
}
```

### 4. Cache de Queries Complejas

```csharp
public async Task<List<ProductDto>> GetTopSellingProductsAsync(int count)
{
    var cacheKey = $"products:top:{count}";

    return await _cache.GetOrCreateAsync(cacheKey, async entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);

        return await _context.Products
            .AsNoTracking()
            .OrderByDescending(p => p.SalesCount)
            .Take(count)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            })
            .ToListAsync();
    });
}
```

---

## 🌐 Distributed Caching

### 1. Redis Configuration

```csharp
// Program.cs
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "MyApp:";  // Prefijo para todas las keys
});

// appsettings.json
{
  "ConnectionStrings": {
    "Redis": "localhost:6379,abortConnect=false,ssl=false"
  }
}
```

### 2. IDistributedCache Básico

```csharp
public class ProductCacheService
{
    private readonly IDistributedCache _cache;
    private readonly IProductRepository _repository;

    public async Task<Product?> GetProductAsync(int id)
    {
        var cacheKey = $"product:{id}";

        // Leer del cache distribuido
        var cachedData = await _cache.GetStringAsync(cacheKey);

        if (cachedData != null)
        {
            return JsonSerializer.Deserialize<Product>(cachedData);
        }

        // Cache miss
        var product = await _repository.GetByIdAsync(id);

        if (product != null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };

            // Escribir en cache
            var serializedData = JsonSerializer.Serialize(product);
            await _cache.SetStringAsync(cacheKey, serializedData, options);
        }

        return product;
    }
}
```

### 3. Typed Cache Wrapper

```csharp
public interface ITypedCache
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
    Task RemoveAsync(string key);
}

public class RedisTypedCache : ITypedCache
{
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public RedisTypedCache(IDistributedCache cache)
    {
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var data = await _cache.GetStringAsync(key);

        if (data == null)
            return default;

        return JsonSerializer.Deserialize<T>(data, _jsonOptions);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(10)
        };

        var serialized = JsonSerializer.Serialize(value, _jsonOptions);
        await _cache.SetStringAsync(key, serialized, options);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}
```

### 4. Hybrid Caching (L1 + L2)

```csharp
public class HybridCache
{
    private readonly IMemoryCache _l1Cache;  // Fast, local
    private readonly IDistributedCache _l2Cache;  // Shared, distributed

    public async Task<T?> GetAsync<T>(string key)
    {
        // 1. Intenta L1 (In-Memory)
        if (_l1Cache.TryGetValue(key, out T? value))
        {
            return value;
        }

        // 2. Intenta L2 (Redis)
        var cachedData = await _l2Cache.GetStringAsync(key);

        if (cachedData != null)
        {
            value = JsonSerializer.Deserialize<T>(cachedData);

            // Poblar L1 cache
            _l1Cache.Set(key, value, TimeSpan.FromMinutes(1));

            return value;
        }

        return default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        // Escribir en ambos niveles
        _l1Cache.Set(key, value, TimeSpan.FromMinutes(1));

        var serialized = JsonSerializer.Serialize(value);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        await _l2Cache.SetStringAsync(key, serialized, options);
    }

    public async Task RemoveAsync(string key)
    {
        _l1Cache.Remove(key);
        await _l2Cache.RemoveAsync(key);
    }
}
```

**Beneficios Hybrid Caching:**
- L1 (Memory): < 1ms latency
- L2 (Redis): 1-5ms latency, compartido entre instancias
- Mejor rendimiento + escalabilidad

### 5. Redis Advanced Patterns

```csharp
// Usar StackExchange.Redis directamente para features avanzadas
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse("localhost:6379");
    configuration.AbortOnConnectFail = false;
    return ConnectionMultiplexer.Connect(configuration);
});

public class RedisCacheService
{
    private readonly IConnectionMultiplexer _redis;

    public async Task<T?> GetAsync<T>(string key)
    {
        var db = _redis.GetDatabase();
        var value = await db.StringGetAsync(key);

        return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value!);
    }

    // Cache with tags (para invalidación masiva)
    public async Task SetWithTagsAsync<T>(string key, T value, params string[] tags)
    {
        var db = _redis.GetDatabase();

        // Guardar el valor
        var serialized = JsonSerializer.Serialize(value);
        await db.StringSetAsync(key, serialized, TimeSpan.FromMinutes(10));

        // Asociar tags
        foreach (var tag in tags)
        {
            await db.SetAddAsync($"tag:{tag}", key);
        }
    }

    // Invalidar por tag
    public async Task InvalidateByTagAsync(string tag)
    {
        var db = _redis.GetDatabase();
        var keys = await db.SetMembersAsync($"tag:{tag}");

        foreach (var key in keys)
        {
            await db.KeyDeleteAsync(key.ToString());
        }

        await db.KeyDeleteAsync($"tag:{tag}");
    }
}

// Uso:
await _cache.SetWithTagsAsync("product:123", product, "products", "category:electronics");
await _cache.InvalidateByTagAsync("products");  // Invalida todos los productos
```

---

## 🌍 CDN & Browser Caching

### 1. Response Caching Middleware

```csharp
// Program.cs
builder.Services.AddResponseCaching();

var app = builder.Build();

app.UseResponseCaching();  // Antes de UseEndpoints

// Endpoint con caching
app.MapGet("/api/products", async (AppDbContext db) =>
{
    return await db.Products.ToListAsync();
})
.CacheOutput(options =>
{
    options.Expire(TimeSpan.FromMinutes(5));
    options.SetVaryByQuery("category", "page");
});
```

### 2. Output Caching (.NET 7+)

```csharp
// Program.cs
builder.Services.AddOutputCache(options =>
{
    // Default policy
    options.AddBasePolicy(builder => builder
        .Expire(TimeSpan.FromMinutes(5))
        .Tag("default"));

    // Named policies
    options.AddPolicy("products", builder => builder
        .Expire(TimeSpan.FromMinutes(10))
        .SetVaryByQuery("category", "page")
        .Tag("products"));

    options.AddPolicy("static", builder => builder
        .Expire(TimeSpan.FromDays(30))
        .Tag("static"));
});

var app = builder.Build();

app.UseOutputCache();

// Uso en endpoints
app.MapGet("/api/products", async (AppDbContext db) =>
{
    return await db.Products.ToListAsync();
})
.CacheOutput("products");

app.MapGet("/api/categories", async (AppDbContext db) =>
{
    return await db.Categories.ToListAsync();
})
.CacheOutput(builder => builder
    .Expire(TimeSpan.FromHours(1))
    .Tag("categories"));
```

### 3. Cache Headers

```csharp
public class CacheController : ControllerBase
{
    [HttpGet("products")]
    [ResponseCache(Duration = 300, VaryByQueryKeys = new[] { "category" })]
    public async Task<IActionResult> GetProducts([FromQuery] string? category)
    {
        var products = await _context.Products
            .Where(p => category == null || p.Category == category)
            .ToListAsync();

        // Cache-Control header: public, max-age=300
        return Ok(products);
    }

    [HttpGet("products/{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        // Custom cache headers
        Response.Headers.CacheControl = "public, max-age=600";
        Response.Headers.ETag = $"\"{product.Version}\"";
        Response.Headers.LastModified = product.UpdatedAt.ToString("R");

        return Ok(product);
    }
}
```

### 4. ETags & Conditional Requests

```csharp
public class ProductsController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        // Generar ETag basado en version/hash
        var etag = $"\"{product.Version}\"";

        // Verificar If-None-Match header
        if (Request.Headers.IfNoneMatch == etag)
        {
            return StatusCode(304);  // Not Modified
        }

        Response.Headers.ETag = etag;
        Response.Headers.CacheControl = "private, max-age=300";

        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        // Verificar If-Match header (optimistic concurrency)
        var expectedEtag = Request.Headers.IfMatch.ToString();
        var currentEtag = $"\"{product.Version}\"";

        if (!string.IsNullOrEmpty(expectedEtag) && expectedEtag != currentEtag)
        {
            return StatusCode(412);  // Precondition Failed
        }

        // Update product...
        product.Version = Guid.NewGuid();  // Nueva version

        await _context.SaveChangesAsync();

        return Ok(product);
    }
}
```

### 5. Static File Caching

```csharp
// Program.cs
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Cache for 1 year (immutable files)
        if (ctx.File.Name.Contains(".min.") || ctx.File.Name.Contains("-[hash]."))
        {
            ctx.Context.Response.Headers.CacheControl = "public, max-age=31536000, immutable";
        }
        // Cache for 1 day (regular files)
        else
        {
            ctx.Context.Response.Headers.CacheControl = "public, max-age=86400";
        }
    }
});
```

---

## 🎨 Cache Patterns

### 1. Cache-Aside (Lazy Loading)

```csharp
public async Task<Product?> GetProductAsync(int id)
{
    var cacheKey = $"product:{id}";

    // 1. Leer del cache
    var cached = await _cache.GetAsync<Product>(cacheKey);
    if (cached != null)
        return cached;

    // 2. Leer de DB
    var product = await _repository.GetByIdAsync(id);

    // 3. Escribir en cache
    if (product != null)
        await _cache.SetAsync(cacheKey, product, TimeSpan.FromMinutes(10));

    return product;
}
```

**Ventajas:**
- Simple de implementar
- Solo cachea lo que se usa
- Resistente a fallos del cache

**Desventajas:**
- Cache miss penalty
- Posible cache stampede

### 2. Read-Through

```csharp
public class ReadThroughCache<T>
{
    private readonly IDistributedCache _cache;
    private readonly Func<string, Task<T?>> _loadFunc;

    public async Task<T?> GetAsync(string key)
    {
        // Cache maneja la lectura de la fuente
        var cached = await _cache.GetAsync<T>(key);

        if (cached != null)
            return cached;

        // Load from source
        var value = await _loadFunc(key);

        if (value != null)
            await _cache.SetAsync(key, value, TimeSpan.FromMinutes(10));

        return value;
    }
}
```

### 3. Write-Through

```csharp
public async Task UpdateProductAsync(Product product)
{
    var cacheKey = $"product:{product.Id}";

    // 1. Escribir en cache
    await _cache.SetAsync(cacheKey, product, TimeSpan.FromMinutes(10));

    // 2. Escribir en DB
    await _repository.UpdateAsync(product);
}
```

**Ventajas:**
- Cache siempre actualizado
- No hay cache miss en lecturas

**Desventajas:**
- Writes más lentos
- Cache puede contener datos no usados

### 4. Write-Behind (Write-Back)

```csharp
public class WriteBehindCache
{
    private readonly Channel<Product> _writeQueue;

    public WriteBehindCache()
    {
        _writeQueue = Channel.CreateUnbounded<Product>();
        _ = ProcessWritesAsync();  // Background task
    }

    public async Task UpdateProductAsync(Product product)
    {
        var cacheKey = $"product:{product.Id}";

        // 1. Escribir en cache inmediatamente
        await _cache.SetAsync(cacheKey, product);

        // 2. Encolar para escritura en DB
        await _writeQueue.Writer.WriteAsync(product);
    }

    private async Task ProcessWritesAsync()
    {
        await foreach (var product in _writeQueue.Reader.ReadAllAsync())
        {
            try
            {
                await _repository.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                // Log error, retry, etc.
            }
        }
    }
}
```

---

## 🗑️ Cache Invalidation

### 1. Time-Based Expiration

```csharp
// Absolute expiration
await _cache.SetAsync("key", value, TimeSpan.FromMinutes(10));

// Sliding expiration (renueva si se accede)
var options = new MemoryCacheEntryOptions
{
    SlidingExpiration = TimeSpan.FromMinutes(5)
};
_memoryCache.Set("key", value, options);
```

### 2. Event-Based Invalidation

```csharp
public class ProductService
{
    public async Task UpdateProductAsync(Product product)
    {
        // 1. Update DB
        await _repository.UpdateAsync(product);

        // 2. Invalidate cache
        await _cache.RemoveAsync($"product:{product.Id}");

        // 3. Invalidate related caches
        await _cache.RemoveAsync("products:all");
        await _cache.RemoveAsync($"products:category:{product.CategoryId}");
    }
}
```

### 3. Tag-Based Invalidation

```csharp
public async Task InvalidateProductCachesAsync()
{
    // Invalidar todos los caches con tag "products"
    await _outputCache.EvictByTagAsync("products");
}

// En endpoint:
app.MapGet("/api/products", async (AppDbContext db) =>
{
    return await db.Products.ToListAsync();
})
.CacheOutput(builder => builder
    .Tag("products")  // Tag para invalidación
    .Expire(TimeSpan.FromMinutes(10)));
```

### 4. Cache Stampede Prevention

```csharp
public class StampedePrevention
{
    private readonly SemaphoreSlim _lock = new(1, 1);

    public async Task<T?> GetOrCreateAsync<T>(
        string key,
        Func<Task<T>> factory,
        TimeSpan expiration)
    {
        // Leer del cache
        var cached = await _cache.GetAsync<T>(key);
        if (cached != null)
            return cached;

        // Lock para evitar múltiples requests a DB
        await _lock.WaitAsync();
        try
        {
            // Double-check
            cached = await _cache.GetAsync<T>(key);
            if (cached != null)
                return cached;

            // Load from source
            var value = await factory();

            if (value != null)
                await _cache.SetAsync(key, value, expiration);

            return value;
        }
        finally
        {
            _lock.Release();
        }
    }
}
```

---

## ✅ Best Practices

### Do's
- ✅ Usar cache para datos que se leen frecuentemente
- ✅ Implementar TTL apropiado según el tipo de dato
- ✅ Usar distributed cache en multi-instance deployments
- ✅ Monitorear hit/miss ratios
- ✅ Implementar fallback si cache falla
- ✅ Usar compression para objetos grandes
- ✅ Implementar cache warming para hot data

### Don'ts
- ❌ No cachear datos sensibles sin encryption
- ❌ No usar cache como primary data store
- ❌ No cachear datos que cambian frecuentemente (< 1 min)
- ❌ No ignorar cache invalidation
- ❌ No cachear objetos muy grandes (> 1MB)
- ❌ No usar cache sin monitoring

---

## 📊 Monitoring

```csharp
public class CacheMetrics
{
    private readonly IMeterFactory _meterFactory;
    private readonly Counter<long> _hits;
    private readonly Counter<long> _misses;

    public CacheMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("MyApp.Cache");

        _hits = meter.CreateCounter<long>("cache.hits");
        _misses = meter.CreateCounter<long>("cache.misses");
    }

    public void RecordHit(string cacheType)
    {
        _hits.Add(1, new KeyValuePair<string, object?>("type", cacheType));
    }

    public void RecordMiss(string cacheType)
    {
        _misses.Add(1, new KeyValuePair<string, object?>("type", cacheType));
    }
}
```

---

## 📚 Referencias

- **ASP.NET Core Caching:** https://learn.microsoft.com/en-us/aspnet/core/performance/caching/
- **Redis:** https://redis.io/docs/
- **Output Caching:** https://learn.microsoft.com/en-us/aspnet/core/performance/caching/output
- **CDN Best Practices:** https://web.dev/content-delivery-networks/

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-23
**Mantenido por:** mjcuadrado-net-sdk
