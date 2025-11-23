---
name: mj2-api-design
description: Diseña APIs RESTful siguiendo best practices y genera documentación OpenAPI
tags: [api, rest, openapi, swagger, design]
---

# /mj2:api-design - API Design

Comando para invocar al agente **api-designer** y diseñar APIs RESTful con OpenAPI documentation.

---

## 📋 Uso

```bash
# Diseñar API completa para un recurso
/mj2:api-design <SPEC-ID>

# Ejemplos
/mj2:api-design API-ORDERS-001
/mj2:api-design API-PRODUCTS-001
/mj2:api-design API-AUTH-001
```

---

## 🎯 ¿Qué hace este comando?

El comando **api-designer** analiza la SPEC y diseña una API RESTful completa que incluye:

1. **Resource Modeling**
   - Identificar recursos (sustantivos)
   - Definir relaciones entre recursos
   - Mapear operaciones a HTTP methods

2. **Endpoint Design**
   - URLs siguiendo convenciones REST
   - HTTP methods apropiados
   - Status codes correctos
   - Request/response models (DTOs)

3. **OpenAPI Documentation**
   - Swagger configuration
   - XML documentation comments
   - Request/response examples
   - Authentication documentation

4. **API Patterns**
   - Pagination (offset o cursor)
   - Filtering, sorting, searching
   - Versioning strategy
   - Error handling (RFC 7807)
   - HATEOAS (opcional)

---

## 🔄 Workflow

```
📊 ANALYZE
  ↓ Lee la SPEC (SPEC-ID)
  ↓ Identifica recursos del dominio
  ↓ Define operaciones CRUD
  ↓ Establece relaciones

🏗️ DESIGN
  ↓ Diseña endpoints RESTful
  ↓ Define DTOs (Request/Response)
  ↓ Mapea HTTP methods y status codes
  ↓ Diseña error responses (Problem Details)
  ↓ Planifica versioning

📝 DOCUMENT
  ↓ Genera configuración OpenAPI/Swagger
  ↓ Crea XML documentation comments
  ↓ Define request/response examples
  ↓ Documenta authentication

✅ VALIDATE
  ↓ Verifica REST constraints
  ↓ Valida naming conventions
  ↓ Revisa status codes
  ↓ Confirma security patterns
```

---

## 💡 Ejemplos de Uso

### Ejemplo 1: Diseñar Orders API

**SPEC:**
```markdown
---
id: API-ORDERS-001
title: Orders API
---

# @SPEC:EX-API-ORDERS-001

## Functional
El sistema DEBE proporcionar una API RESTful para gestionar pedidos.

## Operations
- Listar pedidos con paginación
- Obtener detalles de un pedido
- Crear nuevo pedido
- Actualizar pedido existente
- Cancelar pedido
```

**Comando:**
```bash
/mj2:api-design API-ORDERS-001
```

**Output del api-designer:**

```csharp
// 1. Controllers
[ApiController]
[Route("api/v{version:apiVersion}/orders")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class OrdersController : ControllerBase
{
    /// <summary>
    /// Gets a paginated list of orders
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<OrderDto>), 200)]
    public async Task<IActionResult> GetOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        // Implementation...
    }

    /// <summary>
    /// Gets a specific order by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrderDto), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> GetOrder(int id)
    {
        // Implementation...
    }

    /// <summary>
    /// Creates a new order
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), 201)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        // Implementation...
    }

    /// <summary>
    /// Updates an existing order
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(OrderDto), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderRequest request)
    {
        // Implementation...
    }

    /// <summary>
    /// Cancels an order
    /// </summary>
    [HttpPost("{id}/cancel")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    public async Task<IActionResult> CancelOrder(int id)
    {
        // Implementation...
    }
}

// 2. DTOs
public record OrderDto(
    int Id,
    string OrderNumber,
    int CustomerId,
    decimal Total,
    string Status,
    DateTime CreatedAt);

public record CreateOrderRequest(
    int CustomerId,
    List<OrderItemRequest> Items);

public record OrderItemRequest(
    int ProductId,
    int Quantity);

// 3. Pagination
public class PagedResult<T>
{
    public List<T> Items { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}

// 4. Swagger Configuration
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Orders API",
        Version = "v1"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});
```

### Ejemplo 2: Diseñar Authentication API

```bash
/mj2:api-design API-AUTH-001
```

**Output:**
```csharp
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponse), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 401)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request) { }

    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TokenResponse), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 401)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request) { }

    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Logout() { }
}
```

---

## 📐 Patrones Implementados

### 1. Resource Naming
- ✅ Plural: `/api/orders` (not `/api/order`)
- ✅ Lowercase con kebab-case: `/api/order-items`
- ✅ Nested resources: `/api/orders/{id}/items`
- ❌ Sin verbos: `/api/orders` (not `/api/getOrders`)

### 2. HTTP Methods & Status Codes
| Method | Endpoint | Success | Error |
|--------|----------|---------|-------|
| GET | `/api/orders` | 200 | 401 |
| GET | `/api/orders/{id}` | 200 | 404 |
| POST | `/api/orders` | 201 + Location | 400 |
| PUT | `/api/orders/{id}` | 200 | 404, 400 |
| PATCH | `/api/orders/{id}` | 200 | 404, 400 |
| DELETE | `/api/orders/{id}` | 204 | 404 |

### 3. Pagination
```csharp
// Offset-based
GET /api/orders?page=2&pageSize=10

// Cursor-based
GET /api/orders?cursor=eyJpZCI6MTAwfQ==&limit=10
```

### 4. Filtering & Sorting
```csharp
GET /api/orders?status=pending&customerId=123&sortBy=createdAt&sortOrder=desc
```

### 5. Versioning
```csharp
// URL versioning (recomendado)
GET /api/v1/orders
GET /api/v2/orders

// Header versioning
GET /api/orders
X-API-Version: 1

// Query versioning
GET /api/orders?api-version=1
```

### 6. Error Handling (RFC 7807)
```json
{
  "type": "https://api.example.com/errors/not-found",
  "title": "Order not found",
  "status": 404,
  "detail": "Order with ID 123 was not found",
  "instance": "/api/orders/123",
  "traceId": "00-abc123-def456-00"
}
```

---

## 🔗 Integración con Workflow

```bash
# 1. Diseñar API (api-designer)
/mj2:api-design API-ORDERS-001

# 2. Implementar endpoints (tdd-implementer)
/mj2:2-run API-ORDERS-001

# 3. Generar client TypeScript (frontend-builder)
# npx openapi-typescript http://localhost:5000/swagger/v1/swagger.json -o api-client.ts

# 4. Tests de API (e2e-tester)
/mj2:4-e2e API-ORDERS-E2E-001

# 5. Security review (security-expert)
# Verificar auth, validation, rate limiting

# 6. Deploy (devops-expert)
/mj2:5-deploy production
```

---

## ✅ Checklist de Salida

Después de ejecutar `/mj2:api-design`, verifica:

- [ ] Controllers con XML documentation
- [ ] DTOs (Request/Response) definidos
- [ ] HTTP methods apropiados (GET, POST, PUT, DELETE)
- [ ] Status codes correctos (200, 201, 400, 404, 500)
- [ ] OpenAPI/Swagger configurado
- [ ] Pagination implementada
- [ ] Filtering y sorting diseñados
- [ ] Error handling (RFC 7807)
- [ ] Authentication/Authorization configurados
- [ ] Versioning strategy definida

---

## 📚 Ver También

- Agente: `.claude/agents/mj2/api-designer.md`
- Skills relacionadas:
  - `.claude/skills/dotnet/aspnet-core.md`
  - `.claude/skills/security/jwt.md`
  - `.claude/skills/security/rate-limiting.md`
- Comandos relacionados:
  - `/mj2:2-run` - TDD implementation
  - `/mj2:4-e2e` - E2E testing
  - `/mj2:5-deploy` - Deployment

---

**Versión:** 1.0.0
**Última Actualización:** 2025-11-22
**Mantenido por:** mjcuadrado-net-sdk
