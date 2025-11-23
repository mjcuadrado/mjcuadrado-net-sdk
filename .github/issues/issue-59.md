# Issue #59: GraphQL & gRPC Skills

**Fecha:** 2025-11-23
**Prioridad:** 🟡 Media
**Estado:** 📋 Planificado
**Versión:** v0.7.0
**Branch:** feature/ISSUE-059-graphql-grpc-skills
**Tiempo Estimado:** 5-6 días

---

## 📋 Descripción

Crear skills de **GraphQL** y **gRPC** para APIs modernas, complementando REST.

**Gap identificado:** mj2 tiene api-designer (REST). Falta GraphQL, gRPC, SignalR.

---

## 🎯 Objetivos

### Skills (4 skills)
1. `.claude/skills/backend/graphql.md` (~400 líneas)
   - GraphQL fundamentals
   - Schema definition
   - Queries, Mutations, Subscriptions
   - Best practices

2. `.claude/skills/backend/hotchocolate.md` (~450 líneas)
   - HotChocolate 13+ (.NET)
   - Schema-first vs Code-first
   - DataLoaders
   - Filtering, Sorting, Paging

3. `.claude/skills/backend/grpc.md` (~400 líneas)
   - gRPC fundamentals
   - Protocol Buffers
   - Streaming (Server, Client, Bidirectional)
   - .NET implementation

4. `.claude/skills/backend/signalr.md` (~350 líneas)
   - Real-time communication
   - Hubs
   - Client libraries
   - Scaling with Redis

---

## 📦 Entregables

### 1. graphql.md + hotchocolate.md
```csharp
// Schema
public class Query
{
    public async Task<User> GetUser(int id, [Service] IUserRepository repo)
        => await repo.GetByIdAsync(id);
}

// Startup
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();
```

### 2. grpc.md
```protobuf
// user.proto
service UserService {
  rpc GetUser (UserRequest) returns (User);
}

message UserRequest {
  int32 id = 1;
}
```

### 3. signalr.md
```csharp
// Hub
public class ChatHub : Hub
{
    public async Task SendMessage(string message)
        => await Clients.All.SendAsync("ReceiveMessage", message);
}
```

---

## ✅ Criterios de Éxito

- [ ] 4 skills creados (~1,600 líneas)
- [ ] GraphQL examples (HotChocolate)
- [ ] gRPC examples (.NET)
- [ ] SignalR examples
- [ ] Performance comparisons
- [ ] Integration con api-designer

---

## 🔗 Referencias

- **GraphQL:** https://graphql.org
- **HotChocolate:** https://chillicream.com/docs/hotchocolate
- **gRPC:** https://grpc.io
- **SignalR:** https://learn.microsoft.com/aspnet/core/signalr

---

## 🚀 Impacto

**Sin GraphQL & gRPC:**
- ❌ Solo REST APIs
- ❌ No real-time
- ❌ Overfetching/Underfetching

**Con GraphQL & gRPC:**
- ✅ Modern API patterns
- ✅ Real-time capabilities
- ✅ Efficient data fetching
- ✅ High performance (gRPC)

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Prioridad:** 🟡 MEDIA
**Milestone:** v0.7.0
