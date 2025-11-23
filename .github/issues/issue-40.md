# Issue #40: API Designer Agent

**Status:** ✅ Completed
**Priority:** 🟢 Nice to Have
**Version:** v0.4.0
**Created:** 2025-11-22
**Completed:** 2025-11-22

---

## 📋 Descripción

Implementado el agente **API Designer** especializado en diseño de APIs RESTful para aplicaciones .NET.

---

## 🎯 Objetivos

1. ✅ **api-designer.md Agent** - Agente especializado en API design
2. ✅ **mj2-api-design.md Command** - Comando para invocar el agente

---

## 📦 Archivos Creados

### 1. api-designer.md (680 líneas)

**Ubicación:** `.claude/agents/mj2/api-designer.md`

**Contenido:**
- Persona y filosofía del agente
- TRUST 5 principles para API design
- Workflow de 4 fases (ANALYZE → DESIGN → DOCUMENT → VALIDATE)
- RESTful API design best practices
- REST constraints (Client-Server, Stateless, Cacheable, Uniform Interface, Layered System)
- Resource naming conventions (plural, kebab-case, sin verbos)
- HTTP methods (GET, POST, PUT, PATCH, DELETE) con semántica correcta
- Status codes apropiados (2xx, 3xx, 4xx, 5xx)
- OpenAPI/Swagger documentation completa con XML comments
- API versioning strategies: URL, Header, Query String
- Pagination patterns: Offset-based y Cursor-based
- Filtering, sorting, searching
- HATEOAS implementation
- RFC 7807 Problem Details error handling
- API security (authentication, authorization, rate limiting)
- Integration con otros agentes

**Workflow:**
```
📊 ANALYZE → Identificar recursos y operaciones
🏗️ DESIGN → Diseñar endpoints RESTful
📝 DOCUMENT → Generar OpenAPI/Swagger
✅ VALIDATE → Contract testing y review
```

### 2. mj2-api-design.md (210 líneas)

**Ubicación:** `.claude/commands/mj2-api-design.md`

**Contenido:**
- Sintaxis del comando: `/mj2:api-design <SPEC-ID>`
- Workflow completo detallado
- Ejemplos de uso (Orders API, Auth API)
- Patrones implementados:
  - Resource naming conventions
  - HTTP methods & status codes matrix
  - Pagination (offset y cursor)
  - Filtering & sorting
  - Versioning strategies
  - RFC 7807 error handling
- Integration con workflow full-stack
- Checklist de salida completo

### 3. issue-40.md

**Ubicación:** `.github/issues/issue-40.md`

**Contenido:** Este archivo - documentación completa del Issue #40

---

## 💡 Ejemplos de Uso

### Diseñar Orders API

```bash
/mj2:api-design API-ORDERS-001
```

**Output:**
```csharp
[ApiController]
[Route("api/v{version:apiVersion}/orders")]
[ApiVersion("1.0")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<OrderDto>), 200)]
    public async Task<IActionResult> GetOrders([FromQuery] int page = 1) { }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrderDto), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> GetOrder(int id) { }

    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), 201)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request) { }
}
```

### REST Principles Aplicados

**Resource Naming:**
- ✅ `/api/orders` (plural)
- ✅ `/api/orders/{id}` (ID en path)
- ✅ `/api/orders/{id}/items` (nested resources)
- ❌ `/api/getOrders` (sin verbos en URL)

**HTTP Methods & Status Codes:**
- GET /api/orders → 200 OK
- POST /api/orders → 201 Created + Location header
- PUT /api/orders/{id} → 200 OK
- DELETE /api/orders/{id} → 204 No Content
- Error → 404 Not Found, 400 Bad Request, 409 Conflict

**RFC 7807 Error Handling:**
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

## ✅ Criterios de Éxito

- [x] api-designer.md agente creado (680 líneas)
- [x] mj2-api-design.md comando creado (210 líneas)
- [x] issue-40.md documentación creada
- [x] RESTful best practices documentadas
- [x] REST constraints explicados (5 constraints)
- [x] OpenAPI/Swagger integration documentada
- [x] API versioning strategies (3 tipos: URL, Header, Query)
- [x] Pagination patterns (offset y cursor)
- [x] Filtering, sorting, searching documentados
- [x] HATEOAS implementation example
- [x] Error handling RFC 7807 implementado
- [x] Security patterns incluidos
- [x] Integration con otros agentes
- [x] Ejemplos completos funcionales
- [x] Todo el contenido en español
- [ ] README.md actualizado
- [ ] ROADMAP.md actualizado
- [ ] Todos los archivos committed
- [ ] Merged a main
- [ ] Issue documentado y cerrado

---

## 📊 Resumen de Métricas

| Métrica | Valor |
|---------|-------|
| **Archivos Creados** | 3 (1 agent + 1 command + 1 doc) |
| **Total Líneas** | ~890 |
| **Agentes Nuevos** | 1 (api-designer) |
| **Comandos Nuevos** | 1 (/mj2:api-design) |
| **REST Constraints** | 5 |
| **Versioning Strategies** | 3 (URL, Header, Query) |
| **Pagination Patterns** | 2 (Offset, Cursor) |
| **HTTP Methods** | 5 (GET, POST, PUT, PATCH, DELETE) |
| **Idioma** | 100% Español ✅ |

---

## 🚀 Próximos Pasos

Con API Designer completado (Issue #40), continuamos **v0.4.0: Advanced Features**.

### Issues Completados en v0.4.0:
- ✅ Issue #39: Security Expert
- ✅ Issue #40: API Designer Agent ← **Este issue**

### Próximo Issue: #41 - Project Templates
Project templates completos con:
- Clean Architecture template
- Vertical Slice template
- Full-stack React + .NET template

---

## 📚 Recursos Adicionales

### RESTful APIs
- REST Constraints: https://restfulapi.net/
- Microsoft REST Guidelines: https://github.com/microsoft/api-guidelines
- Richardson Maturity Model: https://martinfowler.com/articles/richardsonMaturityModel.html

### OpenAPI/Swagger
- OpenAPI Spec: https://spec.openapis.org/oas/latest.html
- Swashbuckle: https://github.com/domaindrivendev/Swashbuckle.AspNetCore

### RFC 7807 Problem Details
- RFC 7807: https://tools.ietf.org/html/rfc7807
- Microsoft Docs: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails

---

**Completado por:** Claude Code
**Branch:** feature/issue-40-api-designer → main
**Archivos:** 3 (api-designer.md, mj2-api-design.md, issue-40.md)
**Líneas Añadidas:** ~890
**Idioma:** 100% Español ✅
**API Designer:** ✅ **COMPLETO**
**v0.4.0 Progress:** 2/5 issues (40%)
