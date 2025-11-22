---
name: openapi-typescript
description: Generate type-safe TypeScript clients from OpenAPI/Swagger specifications
version: 0.1.0
tags: [frontend, typescript, openapi, api, codegen]
---

# OpenAPI TypeScript

Genera tipos TypeScript type-safe desde especificaciones OpenAPI 3.x para APIs completamente tipadas.

## 🎯 Overview

**Por qué openapi-typescript en mj2:**
- **Type safety:** Tipos TypeScript generados desde el contrato API
- **Single source of truth:** El schema OpenAPI define el contrato
- **Autocomplete:** IntelliSense completo para endpoints y payloads
- **Compile-time errors:** Detecta errores de API antes de runtime
- **Zero manual typing:** Los tipos se generan automáticamente
- **Integration:** Funciona perfectamente con React Query, Axios, Fetch

---

## 📦 Setup Básico

```bash
# Instalar generador
npm install -D openapi-typescript

# Instalar cliente (opcional)
npm install openapi-fetch
```

**package.json:**
```json
{
  "devDependencies": {
    "openapi-typescript": "^7.0.0"
  },
  "dependencies": {
    "openapi-fetch": "^0.10.0"
  },
  "scripts": {
    "generate:api": "openapi-typescript http://localhost:5000/swagger/v1/swagger.json -o ./src/types/api.d.ts",
    "generate:api:watch": "openapi-typescript http://localhost:5000/swagger/v1/swagger.json -o ./src/types/api.d.ts --watch"
  }
}
```

---

## 🔧 Generate Types

### From Remote OpenAPI Spec

```bash
# From URL
npx openapi-typescript https://api.example.com/openapi.json -o ./src/types/api.d.ts

# From local file
npx openapi-typescript ./openapi.yaml -o ./src/types/api.d.ts

# Watch mode (regenerate on changes)
npx openapi-typescript https://api.example.com/openapi.json -o ./src/types/api.d.ts --watch
```

### From .NET ASP.NET Core

```bash
# Backend genera swagger.json en /swagger/v1/swagger.json
# Frontend genera tipos TypeScript

# package.json script
{
  "scripts": {
    "api:generate": "openapi-typescript http://localhost:5000/swagger/v1/swagger.json -o ./src/types/api.d.ts"
  }
}

# Uso
npm run api:generate
```

---

## 📋 Generated Types Structure

### Example OpenAPI Spec

```yaml
# openapi.yaml
openapi: 3.0.0
info:
  title: Users API
  version: 1.0.0
paths:
  /users:
    get:
      summary: Get all users
      responses:
        '200':
          description: Success
          content:
            application/json:
              schema:
                type: array
                items:
                  $ref: '#/components/schemas/User'
    post:
      summary: Create user
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/CreateUserRequest'
      responses:
        '201':
          description: Created
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/User'

  /users/{id}:
    get:
      summary: Get user by ID
      parameters:
        - name: id
          in: path
          required: true
          schema:
            type: integer
      responses:
        '200':
          description: Success
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/User'

components:
  schemas:
    User:
      type: object
      properties:
        id:
          type: integer
        name:
          type: string
        email:
          type: string
          format: email
        createdAt:
          type: string
          format: date-time

    CreateUserRequest:
      type: object
      required:
        - name
        - email
      properties:
        name:
          type: string
        email:
          type: string
```

### Generated TypeScript Types

```typescript
// src/types/api.d.ts (generated)
export interface paths {
  "/users": {
    get: {
      responses: {
        200: {
          content: {
            "application/json": components["schemas"]["User"][];
          };
        };
      };
    };
    post: {
      requestBody: {
        content: {
          "application/json": components["schemas"]["CreateUserRequest"];
        };
      };
      responses: {
        201: {
          content: {
            "application/json": components["schemas"]["User"];
          };
        };
      };
    };
  };
  "/users/{id}": {
    get: {
      parameters: {
        path: {
          id: number;
        };
      };
      responses: {
        200: {
          content: {
            "application/json": components["schemas"]["User"];
          };
        };
      };
    };
  };
}

export interface components {
  schemas: {
    User: {
      id: number;
      name: string;
      email: string;
      createdAt: string;
    };
    CreateUserRequest: {
      name: string;
      email: string;
    };
  };
}
```

---

## 🚀 Using with openapi-fetch

### Basic Usage

```typescript
import createClient from 'openapi-fetch';
import type { paths } from './types/api';

// Create type-safe client
const client = createClient<paths>({
  baseUrl: 'https://api.example.com',
});

// GET request (fully typed)
const { data, error } = await client.GET("/users");
// data is User[] | undefined
// error is typed error response

// GET with path params
const { data, error } = await client.GET("/users/{id}", {
  params: {
    path: { id: 123 },
  },
});
// data is User | undefined

// POST request
const { data, error } = await client.POST("/users", {
  body: {
    name: "John Doe",
    email: "john@example.com",
  },
});
// body is type-checked against CreateUserRequest
```

### With Query Params

```typescript
// OpenAPI spec with query params
paths:
  /users:
    get:
      parameters:
        - name: page
          in: query
          schema:
            type: integer
        - name: limit
          in: query
          schema:
            type: integer

// Usage
const { data } = await client.GET("/users", {
  params: {
    query: {
      page: 1,
      limit: 10,
    },
  },
});
```

### With Headers

```typescript
const { data } = await client.GET("/users", {
  headers: {
    Authorization: `Bearer ${token}`,
  },
});
```

---

## 🎨 Integration with React Query

```typescript
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import createClient from 'openapi-fetch';
import type { paths, components } from './types/api';

type User = components["schemas"]["User"];
type CreateUserRequest = components["schemas"]["CreateUserRequest"];

const client = createClient<paths>({
  baseUrl: 'https://api.example.com',
});

// GET all users
export function useUsers() {
  return useQuery({
    queryKey: ['users'],
    queryFn: async () => {
      const { data, error } = await client.GET("/users");
      if (error) throw error;
      return data!;
    },
  });
}

// GET user by ID
export function useUser(id: number) {
  return useQuery({
    queryKey: ['users', id],
    queryFn: async () => {
      const { data, error } = await client.GET("/users/{id}", {
        params: { path: { id } },
      });
      if (error) throw error;
      return data!;
    },
  });
}

// POST create user
export function useCreateUser() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (newUser: CreateUserRequest) => {
      const { data, error } = await client.POST("/users", {
        body: newUser,
      });
      if (error) throw error;
      return data!;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });
}

// Usage in component
export function UsersList() {
  const { data: users, isLoading } = useUsers();
  const createUser = useCreateUser();

  const handleCreate = () => {
    createUser.mutate({
      name: "Jane Doe",
      email: "jane@example.com",
    });
  };

  if (isLoading) return <div>Loading...</div>;

  return (
    <div>
      <button onClick={handleCreate}>Create User</button>
      <ul>
        {users?.map((user) => (
          <li key={user.id}>{user.name}</li>
        ))}
      </ul>
    </div>
  );
}
```

---

## 🔄 Custom Fetch Wrapper

```typescript
import type { paths } from './types/api';

type ApiResponse<T> = {
  data: T | null;
  error: Error | null;
};

class ApiClient {
  private baseUrl: string;
  private token: string | null = null;

  constructor(baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  setToken(token: string) {
    this.token = token;
  }

  private async request<T>(
    path: string,
    options: RequestInit = {}
  ): Promise<ApiResponse<T>> {
    try {
      const headers: HeadersInit = {
        'Content-Type': 'application/json',
        ...options.headers,
      };

      if (this.token) {
        headers.Authorization = `Bearer ${this.token}`;
      }

      const response = await fetch(`${this.baseUrl}${path}`, {
        ...options,
        headers,
      });

      if (!response.ok) {
        throw new Error(`HTTP ${response.status}: ${response.statusText}`);
      }

      const data = await response.json();
      return { data, error: null };
    } catch (error) {
      return {
        data: null,
        error: error instanceof Error ? error : new Error('Unknown error'),
      };
    }
  }

  async get<T>(path: string): Promise<ApiResponse<T>> {
    return this.request<T>(path, { method: 'GET' });
  }

  async post<T>(path: string, body: unknown): Promise<ApiResponse<T>> {
    return this.request<T>(path, {
      method: 'POST',
      body: JSON.stringify(body),
    });
  }

  async put<T>(path: string, body: unknown): Promise<ApiResponse<T>> {
    return this.request<T>(path, {
      method: 'PUT',
      body: JSON.stringify(body),
    });
  }

  async delete<T>(path: string): Promise<ApiResponse<T>> {
    return this.request<T>(path, { method: 'DELETE' });
  }
}

// Typed API client
export const api = new ApiClient('https://api.example.com');

// Type-safe usage
type User = components["schemas"]["User"];

const { data, error } = await api.get<User[]>('/users');
```

---

## 🛠️ ASP.NET Core Integration

### Backend Setup (ASP.NET Core)

```csharp
// Program.cs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });

    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

### Frontend Generation Script

```json
{
  "scripts": {
    "api:generate": "openapi-typescript http://localhost:5000/swagger/v1/swagger.json -o ./src/types/api.d.ts",
    "api:watch": "openapi-typescript http://localhost:5000/swagger/v1/swagger.json -o ./src/types/api.d.ts --watch",
    "dev": "concurrently \"npm run api:watch\" \"vite\""
  }
}
```

### CI/CD Integration

```yaml
# .github/workflows/frontend-ci.yml
name: Frontend CI

on: [push, pull_request]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3

      - name: Setup Node.js
        uses: actions/setup-node@v3
        with:
          node-version: '20'

      - name: Install dependencies
        run: npm ci

      - name: Generate API types
        run: npm run api:generate

      - name: Build
        run: npm run build

      - name: Test
        run: npm test
```

---

## ✅ Best Practices

### DO ✅

1. **Generate types in CI/CD** - Ensure types are always up-to-date
   ```yaml
   - run: npm run api:generate
   - run: npm run build
   ```

2. **Version your OpenAPI spec** - Track changes to API contract

3. **Use openapi-fetch** - Type-safe client with minimal overhead

4. **Integrate with React Query** - Combine with TanStack Query for caching

5. **Single source of truth** - OpenAPI spec defines the contract

6. **Automate generation** - Use watch mode during development
   ```bash
   npm run api:watch
   ```

7. **Export reusable types**
   ```typescript
   export type User = components["schemas"]["User"];
   ```

### DON'T ❌

1. ❌ **NO** manually write API types - Generate from OpenAPI
2. ❌ **NO** commit generated types to Git - Generate in CI/CD
3. ❌ **NO** ignore type errors - They indicate contract mismatches
4. ❌ **NO** use `any` - Defeats the purpose of type generation
5. ❌ **NO** skip validation - Validate responses at runtime too
6. ❌ **NO** hardcode endpoints - Use generated paths types
7. ❌ **NO** mix manual and generated types - Be consistent

---

## 📚 Referencias

- **openapi-typescript:** https://github.com/drwpow/openapi-typescript
- **openapi-fetch:** https://github.com/drwpow/openapi-typescript/tree/main/packages/openapi-fetch
- **OpenAPI Spec:** https://spec.openapis.org/oas/v3.1.0
- **Swagger/OpenAPI:** https://swagger.io/specification/

---

**Used by:** frontend-builder, api-designer
**Related skills:** frontend/react-query.md, frontend/typescript.md, dotnet/aspnet-core.md
