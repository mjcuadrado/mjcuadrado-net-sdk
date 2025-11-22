---
name: /mj2:2f-build
description: Build React component with CDD (Component-Driven Development) cycle
agent: mj2/frontend-builder
---

# /mj2:2f-build

Builds React component using Component-Driven Development: TEST → COMPONENT → STYLE → REFACTOR

## Usage

```bash
/mj2:2f-build COMP-ID

# Examples:
/mj2:2f-build COMP-LOGIN-001
/mj2:2f-build COMP-USER-PROFILE-002
/mj2:2f-build COMP-DASHBOARD-003
```

## What it does

1. Loads SPEC from docs/specs/SPEC-{COMP-ID}/
2. 🔴 TEST: Creates failing component tests (Vitest + React Testing Library)
3. 🟢 COMPONENT: Implements minimal React component
4. 💅 STYLE: Applies MUI theming and polish
5. ♻️ REFACTOR: Improves quality, performance, and accessibility
6. Validates TRUST 5 principles
7. Ensures coverage ≥85%
8. Verifies WCAG 2.1 AA accessibility
9. Makes 4 commits (TEST, COMPONENT, STYLE, REFACTOR)

## Workflow Phases

### Phase 1: 🔴 TEST
- Create test file: `src/components/{Name}/{Name}.test.tsx`
- Write failing tests with React Testing Library
- Test rendering, user interactions, accessibility
- Commit with @TEST: tag

### Phase 2: 🟢 COMPONENT
- Create component file: `src/components/{Name}/{Name}.tsx`
- Implement minimal component to pass tests
- Use React Hook Form + Zod for forms
- Commit with @CODE: tag

### Phase 3: 💅 STYLE
- Apply Material UI theming
- Add icons, spacing, colors
- Implement responsive design
- Verify visual polish
- Commit with style tag

### Phase 4: ♻️ REFACTOR
- Apply memo() for optimization
- Add useCallback for stable references
- Add JSDoc documentation
- Verify accessibility (WCAG 2.1 AA)
- Optimize bundle size
- Commit with refactor tag

## Output

```
✅ CDD completado: SPEC-COMP-LOGIN-001

📊 Resumen:
   TEST: ✅ 5 tests (todos fallando → pasando)
   COMPONENT: ✅ Implementación mínima
   STYLE: ✅ MUI theming aplicado
   REFACTOR: ✅ TRUST 5 cumplido

📈 Métricas:
   Coverage: 87% (≥85%) ✅
   Tests: 5/5 passing ✅
   TRUST 5: All principles ✅
   Accessibility: WCAG 2.1 AA ✅
   Bundle size: 12KB gzipped ✅

📁 Archivos creados:
   src/components/LoginForm/LoginForm.test.tsx
   src/components/LoginForm/LoginForm.tsx
   src/components/LoginForm/index.ts

🎯 Próximo paso:
   /mj2:3-sync COMP-LOGIN-001
```

## Agent

Delegates to: `.claude/agents/mj2/frontend-builder.md`

Loads Skills:
- frontend/react.md (CRITICAL)
- frontend/typescript.md (CRITICAL)
- frontend/mui.md
- frontend/react-hook-form.md
- frontend/zod.md
- frontend/react-query.md (if data fetching needed)
- foundation/trust.md
- foundation/tags.md

## Success Criteria

✅ All tests passing
✅ Coverage ≥85%
✅ TRUST 5 validated
✅ WCAG 2.1 AA compliant
✅ TypeScript strict mode
✅ No console errors
✅ Bundle size optimized
✅ 4 commits created

## Common Use Cases

### Login Form
```bash
/mj2:2f-build COMP-LOGIN-001
# Creates: LoginForm component with validation
```

### User Profile Card
```bash
/mj2:2f-build COMP-USER-CARD-002
# Creates: UserCard component with avatar and data
```

### Data Table
```bash
/mj2:2f-build COMP-DATA-TABLE-003
# Creates: DataTable with sorting, filtering, pagination
```

### Dashboard Widget
```bash
/mj2:2f-build COMP-DASHBOARD-WIDGET-004
# Creates: Dashboard widget with React Query
```

## Integration

Works seamlessly with:
- `/mj2:1-plan` - Create SPEC first
- `/mj2:2f-build` - Build component (this command)
- `/mj2:3-sync` - Sync documentation
- `/mj2:quality-check` - Validate quality

## Prerequisites

- SPEC file must exist in `docs/specs/SPEC-{COMP-ID}/spec.md`
- Vite project configured
- Vitest + React Testing Library installed
- Material UI installed
- React Hook Form + Zod installed (for forms)

## Notes

- Always runs TEST phase first (no component without tests)
- Accessibility is validated in REFACTOR phase
- Bundle size is monitored
- Performance optimizations applied automatically
- TypeScript strict mode enforced
- All skills are loaded as references (not copied)

---

**Related Commands:**
- `/mj2:1-plan` - Plan before building
- `/mj2:2-run` - Backend TDD (counterpart)
- `/mj2:3-sync` - Sync after building
- `/mj2:quality-check` - Quality validation
