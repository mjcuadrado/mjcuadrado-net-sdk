---
name: /mj2:4-e2e
description: Create and execute E2E tests with Playwright following Plan → Generate → Execute → Report cycle
agent: mj2/e2e-tester
---

# /mj2:4-e2e

Creates comprehensive E2E tests using Playwright for full-stack validation: PLAN → GENERATE → EXECUTE → REPORT

## Usage

```bash
/mj2:4-e2e E2E-ID [options]

# Examples:
/mj2:4-e2e E2E-LOGIN-001
/mj2:4-e2e E2E-CHECKOUT-001 --visual
/mj2:4-e2e E2E-USER-FLOW-002 --browsers=chromium,firefox
/mj2:4-e2e E2E-DASHBOARD-003 --performance
```

## What it does

1. Loads SPEC from `docs/specs/SPEC-{E2E-ID}/spec.md`
2. 📝 **PLAN:** Analyzes user flows and identifies test scenarios
3. 🏗️ **GENERATE:** Creates Page Objects and E2E tests
4. ▶️ **EXECUTE:** Runs tests across configured browsers
5. 📊 **REPORT:** Generates coverage and test results
6. Validates WCAG 2.1 AA accessibility
7. Captures visual regression snapshots
8. Measures performance (Core Web Vitals)
9. Makes 1 commit with @E2E: tag

## Workflow Phases

### Phase 1: 📝 PLAN
- Read SPEC file and identify user flows
- List test scenarios (happy path, error cases, edge cases)
- Identify Page Objects needed
- Plan test data requirements

### Phase 2: 🏗️ GENERATE
- Create Page Object classes (`tests/pages/`)
- Create E2E test files (`tests/e2e/`)
- Implement tests with Playwright
- Add accessibility validation (axe-core)
- Configure visual regression tests

### Phase 3: ▶️ EXECUTE
- Run tests across browsers (Chromium, Firefox, WebKit)
- Execute in parallel for speed
- Capture screenshots/videos on failure
- Generate HTML and JSON reports

### Phase 4: 📊 REPORT
- Parse test results
- Calculate coverage metrics
- Identify flaky tests
- Generate summary report

## File Structure

```
tests/
├── pages/                      # Page Object Models
│   ├── LoginPage.ts
│   ├── DashboardPage.ts
│   └── CheckoutPage.ts
├── e2e/                        # E2E test files
│   ├── login.spec.ts
│   └── checkout.spec.ts
├── fixtures.ts                 # Custom fixtures
└── playwright.config.ts        # Playwright configuration

playwright-report/              # HTML report (gitignored)
test-results/                   # Screenshots, videos (gitignored)
```

## Output

```
✅ E2E completado: SPEC-E2E-LOGIN-001

📊 Resumen:
   PLAN: ✅ 4 scenarios identificados
   GENERATE: ✅ 2 Page Objects + 4 tests creados
   EXECUTE: ✅ 12 tests ejecutados (3 browsers)
   REPORT: ✅ Reporte generado

📈 Métricas:
   Tests: 12/12 passing ✅
   Browsers: Chromium ✓, Firefox ✓, WebKit ✓
   Coverage: 100% critical paths ✅
   Accessibility: WCAG 2.1 AA ✅
   Visual Regression: No changes ✅
   Performance: LCP 1.8s ✅

📁 Archivos creados:
   tests/pages/LoginPage.ts
   tests/pages/DashboardPage.ts
   tests/e2e/login.spec.ts

🎯 Próximo paso:
   /mj2:quality-check
```

## Agent

Delegates to: `.claude/agents/mj2/e2e-tester.md`

Loads Skills:
- testing/playwright.md (CRITICAL)
- foundation/testing.md
- frontend/react.md
- frontend/typescript.md
- testing/testcontainers.md (optional, for database setup)

## Success Criteria

✅ All E2E tests passing
✅ Coverage of all critical user paths
✅ WCAG 2.1 AA validated
✅ Visual regression baseline created
✅ Performance metrics captured (LCP < 2.5s)
✅ Page Objects implemented
✅ No flaky tests (< 1% retry rate)
✅ 1 commit with @E2E: tag

## Common Use Cases

### Login Flow
```bash
/mj2:4-e2e E2E-LOGIN-001
# Creates: LoginPage, DashboardPage, login.spec.ts
# Tests: Valid login, invalid credentials, accessibility
```

### E-commerce Checkout
```bash
/mj2:4-e2e E2E-CHECKOUT-001 --visual
# Creates: ProductListPage, CartPage, CheckoutPage, PaymentPage
# Tests: Full checkout flow with visual regression
```

### User Profile Management
```bash
/mj2:4-e2e E2E-PROFILE-002
# Creates: ProfilePage, SettingsPage
# Tests: View profile, edit info, change password
```

### Dashboard Interactions
```bash
/mj2:4-e2e E2E-DASHBOARD-003 --performance
# Creates: DashboardPage, widgets
# Tests: Data loading, filters, performance metrics
```

## Integration

Works seamlessly with:
- `/mj2:1-plan` - Create SPEC first
- `/mj2:2-run` - Backend must be ready
- `/mj2:2f-build` - Frontend must be deployed
- `/mj2:4-e2e` - E2E tests (this command)
- `/mj2:quality-check` - Validate all quality gates
- `/mj2:3-sync` - Sync documentation

## Prerequisites

**SPEC Requirements:**
- SPEC file must exist in `docs/specs/SPEC-{E2E-ID}/spec.md`
- Must contain user flows and acceptance criteria
- UI states and navigation paths documented

**Environment Requirements:**
- Playwright installed: `npm init playwright@latest`
- Backend API running (from tdd-implementer)
- Frontend deployed (from frontend-builder)
- Test database seeded with data

**Configuration:**
- `playwright.config.ts` configured
- Base URL set for environment
- Browsers installed: `npx playwright install`

## Options

### --visual
Enable visual regression testing with screenshot comparison.

```bash
/mj2:4-e2e E2E-LOGIN-001 --visual
# Adds expect(page).toHaveScreenshot() to tests
```

### --browsers
Specify which browsers to test (comma-separated).

```bash
/mj2:4-e2e E2E-LOGIN-001 --browsers=chromium,firefox
# Only runs on Chromium and Firefox (skips WebKit)
```

### --performance
Include Core Web Vitals performance testing.

```bash
/mj2:4-e2e E2E-DASHBOARD-003 --performance
# Measures LCP, FID, CLS
```

### --headed
Run tests in headed mode (visible browser).

```bash
/mj2:4-e2e E2E-LOGIN-001 --headed
# Useful for debugging
```

## Test Quality Standards

### TRUST 5 Principles
- **Test First:** PLAN phase before writing code
- **Readable:** Page Object Model, clear test names
- **Unified:** Consistent patterns across tests
- **Secured:** No hardcoded credentials, isolated data
- **Trackable:** @E2E: tags, coverage reports

### Metrics

| Metric | Target | Validation |
|--------|--------|------------|
| Coverage | 100% critical paths | All acceptance criteria |
| Reliability | < 1% retry rate | No flaky tests |
| Speed | < 5min full suite | Parallel execution |
| Accessibility | WCAG 2.1 AA | axe-core checks |
| Visual | No regressions | Snapshot comparison |
| Performance | LCP < 2.5s | Core Web Vitals |

## Testing Pyramid Position

```
         ▲
        / \
       /E2E\          ← THIS COMMAND
      /_____\           Critical user flows
     /       \
    /Component\        ← frontend-builder
   /___________\         UI component tests
  /             \
 /  Integration  \     ← tdd-implementer
/________________ \      API + Database tests
       Unit             ← tdd-implementer
                          Business logic
```

## Workflow Example

Complete feature development with E2E validation:

```bash
# 1. Plan feature
/mj2:1-plan E2E-LOGIN-001

# 2. Build backend API
/mj2:2-run API-AUTH-001

# 3. Build frontend components
/mj2:2f-build COMP-LOGIN-001

# 4. Create E2E tests (THIS COMMAND)
/mj2:4-e2e E2E-LOGIN-001

# 5. Validate all quality gates
/mj2:quality-check

# 6. Sync documentation
/mj2:3-sync E2E-LOGIN-001
```

**Result:**
- ✅ Backend API tested (Unit + Integration)
- ✅ Frontend components tested (Unit + Component)
- ✅ E2E user flow validated
- ✅ Full stack feature complete

## Notes

- Always runs PLAN phase first (no tests without scenarios)
- Page Object Model is mandatory (no direct page interactions in tests)
- Accessibility validation on every test
- Visual regression baseline created on first run
- Tests run in parallel for speed
- Automatic retry on failure (2 attempts on CI)
- Screenshots/videos captured on failure only
- All skills loaded as references (not copied)

---

**Related Commands:**
- `/mj2:1-plan` - Create SPEC before E2E
- `/mj2:2-run` - Backend TDD
- `/mj2:2f-build` - Frontend CDD
- `/mj2:quality-check` - Quality validation
- `/mj2:3-sync` - Documentation sync
