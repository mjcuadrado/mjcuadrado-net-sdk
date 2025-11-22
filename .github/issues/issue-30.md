# Issue #30: State & Data Management Skills

**Status:** ✅ Completed
**Priority:** 🔴 Critical
**Version:** v0.2.0
**Created:** 2025-11-22
**Completed:** 2025-11-22

---

## 📋 Description

Add frontend state management and data fetching skills for React applications. These skills provide type-safe form handling, schema validation, API client generation, and server state management.

---

## 🎯 Objectives

Create 4 essential skills for modern React frontend development:

1. **zod.md** - TypeScript-first schema validation
2. **react-hook-form.md** - Performant form state management
3. **openapi-typescript.md** - Type-safe API clients from OpenAPI specs
4. **react-query.md** - Server state management with TanStack Query

---

## 📦 Skills Created

### 1. zod.md (~460 lines)

**Location:** `.claude/skills/frontend/zod.md`

**Content:**
- TypeScript-first schema validation
- Type inference from schemas
- Runtime validation patterns
- String, number, object, array validations
- Transformations and refinements
- Integration with React Hook Form
- API response validation
- Best practices and common patterns

**Key Features:**
- Zero dependencies
- Automatic type inference
- Composable schemas
- Custom error messages
- Transform and refine capabilities

### 2. react-hook-form.md (~520 lines)

**Location:** `.claude/skills/frontend/react-hook-form.md`

**Content:**
- Performant form state management
- Uncontrolled components approach
- Zod integration with zodResolver
- Form validation patterns
- Field arrays for dynamic forms
- Material UI integration with Controller
- Form state (dirty, touched, errors)
- Validation modes (onSubmit, onBlur, onChange)
- Custom validation patterns
- Error handling strategies

**Key Features:**
- Minimal re-renders
- TypeScript support
- Built-in validation
- ~9KB bundle size
- Easy MUI integration

### 3. openapi-typescript.md (~470 lines)

**Location:** `.claude/skills/frontend/openapi-typescript.md`

**Content:**
- Generate TypeScript types from OpenAPI/Swagger specs
- Type-safe API clients with openapi-fetch
- Integration with ASP.NET Core backend
- React Query integration patterns
- CI/CD type generation workflows
- Path parameters, query params, headers
- Custom fetch wrappers
- Development workflow with watch mode

**Key Features:**
- Single source of truth (OpenAPI spec)
- Compile-time type safety
- Automatic IntelliSense
- Zero manual typing
- Backend-frontend contract validation

### 4. react-query.md (~580 lines)

**Location:** `.claude/skills/frontend/react-query.md`

**Content:**
- Server state management with TanStack Query
- Query patterns (GET requests)
- Mutation patterns (POST, PUT, DELETE)
- Cache management and invalidation
- Optimistic updates
- Pagination and infinite scroll
- Integration with openapi-typescript
- Query configuration (staleTime, retry, etc.)
- Prefetching strategies
- Loading and error state handling

**Key Features:**
- Automatic caching
- Background refetching
- Request deduplication
- DevTools support
- TypeScript-first design

---

## ✅ Success Criteria

- [x] All 4 skills documented with comprehensive examples
- [x] Each skill includes Best Practices section
- [x] TypeScript examples throughout
- [x] Integration examples between skills
- [x] ASP.NET Core backend integration examples
- [x] Build validation passes
- [x] Tests pass (99%+)
- [x] Documentation complete

---

## 🔗 Integration Chain

These skills work together to create a complete type-safe frontend:

```
OpenAPI Spec (Backend)
    ↓
openapi-typescript (Generate types)
    ↓
React Query (Fetch data with types)
    ↓
Zod (Validate data)
    ↓
React Hook Form (Handle forms with validation)
    ↓
Type-safe React Components
```

**Example workflow:**
1. Backend defines OpenAPI spec in ASP.NET Core
2. `openapi-typescript` generates TypeScript types
3. `react-query` uses types for type-safe API calls
4. `zod` validates form inputs and API responses
5. `react-hook-form` handles form state with Zod validation

---

## 📊 Metrics

| Skill | Lines | Sections | Examples | Best Practices |
|-------|-------|----------|----------|----------------|
| zod.md | 460 | 10 | 25+ | ✅ |
| react-hook-form.md | 520 | 11 | 20+ | ✅ |
| openapi-typescript.md | 470 | 9 | 15+ | ✅ |
| react-query.md | 580 | 10 | 30+ | ✅ |
| **Total** | **2,030** | **40** | **90+** | **All** |

---

## 🎓 Learning Path

**Recommended order for learning:**

1. **TypeScript** (frontend/typescript.md) - Foundation
2. **React** (frontend/react.md) - Component model
3. **Zod** (frontend/zod.md) - Validation basics
4. **React Hook Form** (frontend/react-hook-form.md) - Form handling
5. **OpenAPI TypeScript** (frontend/openapi-typescript.md) - API types
6. **React Query** (frontend/react-query.md) - Data fetching

---

## 🔗 Related Skills

**Dependencies:**
- `frontend/react.md` - React 18 basics
- `frontend/typescript.md` - TypeScript 5 fundamentals
- `dotnet/aspnet-core.md` - Backend API (OpenAPI spec)

**Complements:**
- `frontend/mui.md` - Material UI components
- `frontend/vite.md` - Build tool
- `testing/vitest.md` - Unit testing (future)

**Used by:**
- `frontend-builder` agent (future - Issue #31)
- `component-designer` agent (future)

---

## 📝 Implementation Notes

### Patterns Established

1. **Type Safety Chain:** OpenAPI → TypeScript Types → Zod → React Hook Form
2. **Server State:** Always use React Query for server data
3. **Form State:** Always use React Hook Form with Zod
4. **API Clients:** Always generate from OpenAPI spec
5. **Validation:** Zod for all validation (forms, API responses)

### Convention Decisions

- Use `@tanstack/react-query` v5 (latest)
- Use `openapi-fetch` for type-safe clients
- Use `zodResolver` for React Hook Form validation
- Use `QueryClient` with sensible defaults
- Use TypeScript strict mode

---

## 🚀 Next Steps (Issue #31)

With these skills in place, the next issue will create the **frontend-builder** agent that uses all these skills to implement React components following Component-Driven Development (CDD).

**Prerequisites completed:** ✅
- React 18 patterns (Issue #28)
- TypeScript 5 (Issue #28)
- Vite (Issue #29)
- Material UI (Issue #29)
- State management (Issue #30) ← **This issue**

**Ready for:**
- Issue #31: Frontend Builder Agent
- Component-Driven Development workflow
- Full type-safe frontend development

---

## 📚 References

**External Documentation:**
- Zod: https://zod.dev/
- React Hook Form: https://react-hook-form.com/
- OpenAPI TypeScript: https://github.com/drwpow/openapi-typescript
- TanStack Query: https://tanstack.com/query/latest

**Related Issues:**
- Issue #28: React & TypeScript Core
- Issue #29: Vite & MUI
- Issue #31: Frontend Builder Agent (next)

**ROADMAP Reference:**
- Section: v0.2.0 - Frontend Foundation
- Location: docs/ROADMAP.md lines 260-279

---

**Completed by:** Claude Code
**Commit:** feature/issue-30-state-management → main
**Total Skills:** 4 (zod, react-hook-form, openapi-typescript, react-query)
