# Issue #31: Frontend Builder Agent

**Status:** ✅ Completed
**Priority:** 🔴 Critical
**Version:** v0.2.0
**Created:** 2025-11-22
**Completed:** 2025-11-22

---

## 📋 Description

Create the **frontend-builder** agent that implements Component-Driven Development (CDD) for React 18 applications. This agent is the frontend counterpart to the `tdd-implementer` agent, following a TEST → COMPONENT → STYLE → REFACTOR cycle.

---

## 🎯 Objectives

Implement a specialized agent for building React components with:

1. **Component-Driven Development (CDD)** - Test-first component development
2. **Four-phase workflow** - TEST → COMPONENT → STYLE → REFACTOR
3. **Type safety** - TypeScript strict mode with complete type coverage
4. **Accessibility** - WCAG 2.1 AA compliance validation
5. **Performance** - Bundle size optimization and React optimization patterns
6. **Integration** - Seamless use of all frontend skills created in Issues #28-30

---

## 📦 Files Created

### 1. frontend-builder.md (~800 lines)

**Location:** `.claude/agents/mj2/frontend-builder.md`

**Content:**
- Agent persona and philosophy
- Language handling (es/en)
- Four-phase CDD workflow
- Detailed examples for each phase
- TRUST 5 integration for frontend
- Accessibility validation
- Performance optimization patterns
- Output format (JSON)
- Troubleshooting guide
- Skills integration strategy

**Key Features:**
- **TEST Phase:** Vitest + React Testing Library failing tests
- **COMPONENT Phase:** Minimal React implementation with TypeScript
- **STYLE Phase:** Material UI theming and visual polish
- **REFACTOR Phase:** Performance optimization, accessibility, documentation

### 2. mj2-2f-build.md (~150 lines)

**Location:** `.claude/commands/mj2-2f-build.md`

**Content:**
- Slash command definition
- Usage examples
- Workflow phase descriptions
- Success criteria
- Common use cases
- Integration with other commands
- Prerequisites checklist

**Usage:**
```bash
/mj2:2f-build COMP-LOGIN-001
```

### 3. issue-31.md

**Location:** `.github/issues/issue-31.md`

**Content:** This file

---

## 🔄 Component-Driven Development (CDD) Workflow

### Phase 1: 🔴 TEST (Failing Tests)

**Creates:**
- `src/components/{Name}/{Name}.test.tsx`

**Actions:**
- Write failing component tests
- Test rendering, user interactions, accessibility
- Commit with @TEST: tag

**Example Test:**
```typescript
it('should render email and password fields', () => {
  render(<LoginForm onSubmit={vi.fn()} />);
  expect(screen.getByLabelText(/email/i)).toBeInTheDocument();
  expect(screen.getByLabelText(/password/i)).toBeInTheDocument();
});
```

### Phase 2: 🟢 COMPONENT (Minimal Implementation)

**Creates:**
- `src/components/{Name}/{Name}.tsx`
- `src/components/{Name}/index.ts`

**Actions:**
- Implement minimal component to pass tests
- Use React Hook Form + Zod for forms
- TypeScript strict mode
- Commit with @CODE: tag

**Example Component:**
```typescript
export function LoginForm({ onSubmit }: LoginFormProps) {
  const { register, handleSubmit, formState: { errors } } = useForm({
    resolver: zodResolver(loginSchema),
  });
  // ... minimal implementation
}
```

### Phase 3: 💅 STYLE (MUI Theming)

**Updates:**
- `src/components/{Name}/{Name}.tsx`

**Actions:**
- Apply Material UI theming
- Add icons, spacing, responsive design
- Visual polish
- Commit with style tag

**Example Enhancements:**
```typescript
<Card sx={{ maxWidth: 400, mx: 'auto', mt: 4 }}>
  <CardContent>
    <Typography variant="h5">Login</Typography>
    {/* Form with icons and spacing */}
  </CardContent>
</Card>
```

### Phase 4: ♻️ REFACTOR (Quality & Performance)

**Updates:**
- `src/components/{Name}/{Name}.tsx`
- Tests as needed

**Actions:**
- Apply memo() for optimization
- Add useCallback for stable references
- JSDoc documentation
- WCAG 2.1 AA accessibility validation
- Bundle size optimization
- Commit with refactor tag

**Example Optimizations:**
```typescript
export const LoginForm = memo(function LoginForm({ onSubmit }: LoginFormProps) {
  const handleFormSubmit = useCallback(async (data) => {
    // ... optimized handler
  }, [onSubmit]);
  // ... accessible and documented
});
```

---

## 📊 Integration with Frontend Skills

The `frontend-builder` agent uses all skills created in Issues #28-30:

| Skill | Usage in frontend-builder |
|-------|---------------------------|
| **frontend/react.md** | React 18 patterns, hooks, component structure |
| **frontend/typescript.md** | TypeScript strict mode, type inference, generics |
| **frontend/mui.md** | Material UI components, theming, customization |
| **frontend/react-hook-form.md** | Form state management, validation integration |
| **frontend/zod.md** | Schema validation for forms and API responses |
| **frontend/react-query.md** | Data fetching for components with server state |
| **foundation/trust.md** | TRUST 5 principles adapted for frontend |
| **foundation/tags.md** | @TEST: and @CODE: tagging system |

---

## ✅ Success Criteria

- [x] frontend-builder.md agent created (~800 lines)
- [x] mj2-2f-build.md command created (~150 lines)
- [x] Four-phase CDD workflow implemented
- [x] Integration with all frontend skills
- [x] Accessibility validation (WCAG 2.1 AA)
- [x] Performance optimization patterns
- [x] TypeScript strict mode enforcement
- [x] TRUST 5 principles for frontend
- [x] Documentation complete

---

## 🎓 Comparison: frontend-builder vs tdd-implementer

| Aspect | tdd-implementer (Backend) | frontend-builder (Frontend) |
|--------|---------------------------|----------------------------|
| **Methodology** | TDD (Test-Driven Dev) | CDD (Component-Driven Dev) |
| **Phases** | 3 (RED → GREEN → REFACTOR) | 4 (TEST → COMPONENT → STYLE → REFACTOR) |
| **Testing** | xUnit + FluentAssertions | Vitest + React Testing Library |
| **Language** | C# (.NET 9) | TypeScript 5 + React 18 |
| **Framework** | ASP.NET Core | React + Material UI |
| **Validation** | FluentValidation | Zod |
| **Coverage** | ≥85% | ≥85% |
| **Quality** | TRUST 5 | TRUST 5 + Accessibility |
| **Extra Phase** | - | STYLE (MUI theming) |
| **Commits** | 3 (🔴 🟢 ♻️) | 4 (🔴 🟢 💅 ♻️) |

---

## 📈 Metrics

| Metric | Target | Enforcement |
|--------|--------|-------------|
| Coverage | ≥85% | Automated check in REFACTOR phase |
| Tests passing | 100% | Must pass before next phase |
| Accessibility | WCAG 2.1 AA | Validated with axe-core |
| Bundle size | Optimized | Monitored, memo() applied |
| TypeScript | Strict mode | Compiler errors = block |
| TRUST 5 | All principles | Checklist in REFACTOR |

---

## 🔗 Agent Flow Integration

```
spec-builder
    ↓
frontend-builder (THIS AGENT)
    ↓
quality-gate
    ↓
doc-syncer
```

**Complete workflow:**
1. `/mj2:1-plan COMP-LOGIN-001` - Create SPEC
2. `/mj2:2f-build COMP-LOGIN-001` - Build component (this agent)
3. `/mj2:3-sync COMP-LOGIN-001` - Sync documentation
4. `/mj2:quality-check` - Final validation

---

## 🚀 Usage Examples

### Example 1: Login Form Component

```bash
/mj2:2f-build COMP-LOGIN-001
```

**Output:**
```
✅ CDD completado: SPEC-COMP-LOGIN-001

📊 Tests: 5/5 passing (100%)
📊 Coverage: 87% (≥85%)
✅ TRUST 5: Validated
✅ Accessibility: WCAG 2.1 AA
📦 Bundle: 12KB gzipped
🔗 TAG chain: Complete

🎯 Próximo: /mj2:3-sync COMP-LOGIN-001
```

**Files created:**
- `src/components/LoginForm/LoginForm.test.tsx` (5 tests)
- `src/components/LoginForm/LoginForm.tsx` (component)
- `src/components/LoginForm/index.ts` (exports)

**Commits:**
- 🔴 test: add failing component tests
- 🟢 feat: implement minimal component
- 💅 style: apply MUI theming
- ♻️ refactor: improve quality and accessibility

### Example 2: Data Table Component

```bash
/mj2:2f-build COMP-DATA-TABLE-002
```

**Includes:**
- React Query integration for data fetching
- Sorting, filtering, pagination
- Responsive design
- Accessibility (keyboard navigation, screen readers)
- Performance optimization (virtualization)

---

## 🎯 Next Steps (Issue #32)

With the frontend-builder agent in place, the next issue will add E2E testing capabilities:

**Prerequisites completed:** ✅
- Backend TDD agent (tdd-implementer) ✅
- Frontend skills (Issues #28-30) ✅
- Frontend CDD agent (frontend-builder) ✅ ← **This issue**

**Ready for:**
- Issue #32: Playwright E2E Testing
- E2E test patterns skill
- E2E tester agent
- Full-stack testing workflow

---

## 📚 References

**Related Agents:**
- `tdd-implementer.md` - Backend TDD counterpart
- `quality-gate.md` - Quality validation
- `doc-syncer.md` - Documentation sync

**Skills Used:**
- All frontend skills (Issues #28-30)
- All foundation skills
- TRUST 5 principles

**External:**
- Component-Driven Development: https://www.componentdriven.org/
- React Testing Library: https://testing-library.com/react
- Vitest: https://vitest.dev/
- WCAG 2.1: https://www.w3.org/WAI/WCAG21/quickref/

**ROADMAP Reference:**
- Section: v0.2.0 - Frontend Foundation
- Location: docs/ROADMAP.md lines 281-294

---

**Completed by:** Claude Code
**Commit:** feature/issue-31-frontend-builder → main
**Files:** 2 (frontend-builder.md, mj2-2f-build.md)
**Lines Added:** ~950
