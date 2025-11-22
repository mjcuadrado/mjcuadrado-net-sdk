# Issue #32: Playwright E2E Testing

**Status:** ✅ Completed
**Priority:** 🟡 High
**Version:** v0.2.0
**Created:** 2025-11-22
**Completed:** 2025-11-22

---

## 📋 Description

Created comprehensive E2E testing capabilities using **Playwright** for full-stack React + .NET applications. This issue completes the testing pyramid by adding end-to-end tests that validate complete user workflows across frontend and backend.

---

## 🎯 Objectives

Implemented a complete E2E testing solution with:

1. ✅ **Playwright Skills** - Modern E2E testing patterns
2. ✅ **E2E Tester Agent** - Automated E2E test generation and execution
3. ✅ **Test Orchestration** - CI/CD integration and reporting
4. ✅ **Best Practices** - Page Object Model, visual regression, API mocking
5. ✅ **Full Coverage** - Integration with existing TDD and CDD workflows

---

## 📦 Files Created

### 1. playwright.md (752 lines)

**Location:** `.claude/skills/testing/playwright.md`

**Content:**
- Playwright fundamentals and configuration
- E2E test patterns and best practices
- Page Object Model (POM) implementation
- Visual regression testing with screenshots
- API mocking and network interception
- Cross-browser testing (Chromium, Firefox, WebKit)
- Debugging tools (trace viewer, screenshots, videos)
- CI/CD integration patterns
- Accessibility testing with axe-core
- Performance testing basics

**Key Features:**
- Auto-waiting for elements (no manual waits)
- Network control and mocking
- Screenshot and video capture
- Accessibility validation (WCAG 2.1 AA)
- Mobile emulation
- Parallelization and sharding

### 2. e2e-tester.md (685 lines)

**Location:** `.claude/agents/mj2/e2e-tester.md`

**Content:**
- Agent persona and philosophy
- Language handling (es/en)
- Four-phase workflow: PLAN → GENERATE → EXECUTE → REPORT
- Integration with frontend-builder and tdd-implementer
- Test orchestration and execution
- Coverage reporting and metrics
- CI/CD pipeline integration
- Visual regression validation
- Performance testing (Core Web Vitals)
- TRUST 5 principles for E2E

**Workflow Phases:**
1. **PLAN:** Identify user flows from SPECs
2. **GENERATE:** Create Page Objects and E2E tests
3. **EXECUTE:** Run tests across browsers
4. **REPORT:** Generate coverage and visual diffs

### 3. mj2-4-e2e.md (307 lines)

**Location:** `.claude/commands/mj2-4-e2e.md`

**Content:**
- Slash command definition
- Usage examples and patterns
- Workflow integration
- Success criteria
- Common use cases (Login, Checkout, Dashboard)
- Prerequisites and configuration
- Options: --visual, --browsers, --performance, --headed

**Usage:**
```bash
/mj2:4-e2e E2E-LOGIN-001
/mj2:4-e2e E2E-CHECKOUT-001 --visual
/mj2:4-e2e E2E-USER-FLOW-002 --browsers=chromium,firefox
```

### 4. issue-32.md

**Location:** `.github/issues/issue-32.md`

**Content:** This file - complete documentation of Issue #32

---

## 🔄 E2E Testing Workflow

### Four-Phase Cycle

```
📝 PLAN
  ↓ Analyze SPEC, identify scenarios
🏗️ GENERATE
  ↓ Create Page Objects + tests
▶️ EXECUTE
  ↓ Run tests across browsers
📊 REPORT
  ↓ Generate coverage and metrics
✅ COMPLETE
```

### Integration with Existing Agents

```
spec-builder (SPEC)
    ↓
tdd-implementer (Backend) + frontend-builder (Frontend)
    ↓
e2e-tester (E2E Tests) ← THIS AGENT
    ↓
quality-gate (Validation)
    ↓
doc-syncer (Documentation)
```

### Complete Full-Stack Flow

```bash
# 1. Create SPEC
/mj2:1-plan E2E-LOGIN-001

# 2. Build backend API
/mj2:2-run API-AUTH-001

# 3. Build frontend components
/mj2:2f-build COMP-LOGIN-001

# 4. Create E2E tests (NEW)
/mj2:4-e2e E2E-LOGIN-001

# 5. Validate quality
/mj2:quality-check

# 6. Sync documentation
/mj2:3-sync E2E-LOGIN-001
```

---

## 📊 Testing Pyramid Integration

| Layer | Tool | Coverage | Agent | Status |
|-------|------|----------|-------|--------|
| **E2E** | Playwright | Critical paths | e2e-tester | ✅ **This issue** |
| **Integration** | Testcontainers | API + DB | tdd-implementer | ✅ Issue #27 |
| **Component** | Vitest + RTL | UI components | frontend-builder | ✅ Issue #31 |
| **Unit** | xUnit | Business logic | tdd-implementer | ✅ Issue #21 |

**Testing Pyramid Now Complete! 🎉**

---

## 📈 Metrics & Quality Gates

| Metric | Target | Enforcement |
|--------|--------|-------------|
| E2E Coverage | Critical user flows | 100% of main paths |
| Browser Support | Chromium, Firefox, WebKit | All tests pass |
| Visual Regression | No unintended changes | Automated diffs |
| Performance | LCP < 2.5s | Lighthouse checks |
| Accessibility | WCAG 2.1 AA | axe-core validation |
| Flakiness | < 1% retry rate | Monitor and fix |
| Speed | < 5min full suite | Parallel execution |

---

## 🎯 Example: Login Flow E2E

### SPEC (Input)

```markdown
# SPEC-E2E-LOGIN-001

## User Story
As a user, I want to log in to access my dashboard.

## Acceptance Criteria
- WHEN user enters valid credentials
  THEN user is redirected to dashboard
- WHEN user enters invalid credentials
  THEN error message is displayed
- WHEN user is already logged in
  THEN redirect to dashboard immediately
```

### Page Object (Generated)

```typescript
// tests/pages/LoginPage.ts
import { Page, Locator, expect } from '@playwright/test';

export class LoginPage {
  readonly page: Page;
  readonly emailInput: Locator;
  readonly passwordInput: Locator;
  readonly submitButton: Locator;
  readonly errorAlert: Locator;

  constructor(page: Page) {
    this.page = page;
    this.emailInput = page.getByLabel('Email');
    this.passwordInput = page.getByLabel('Password');
    this.submitButton = page.getByRole('button', { name: /login/i });
    this.errorAlert = page.getByRole('alert');
  }

  async goto() {
    await this.page.goto('/login');
  }

  async login(email: string, password: string) {
    await this.emailInput.fill(email);
    await this.passwordInput.fill(password);
    await this.submitButton.click();
  }

  async expectLoginSuccess() {
    await expect(this.page).toHaveURL(/\/dashboard/);
  }

  async expectLoginError(message: string) {
    await expect(this.errorAlert).toContainText(message);
  }
}
```

### E2E Test (Generated)

```typescript
// tests/e2e/login.spec.ts
import { test, expect } from '@playwright/test';
import { injectAxe, checkA11y } from 'axe-playwright';
import { LoginPage } from '../pages/LoginPage';

test.describe('Login Flow - E2E-LOGIN-001', () => {
  let loginPage: LoginPage;

  test.beforeEach(async ({ page }) => {
    loginPage = new LoginPage(page);
    await loginPage.goto();
  });

  test('should login successfully with valid credentials', async ({ page }) => {
    await loginPage.login('user@example.com', 'password123');
    await loginPage.expectLoginSuccess();
    await expect(page.getByText(/welcome back/i)).toBeVisible();
    await expect(page).toHaveScreenshot('dashboard-logged-in.png');
  });

  test('should show error with invalid credentials', async ({ page }) => {
    await loginPage.login('invalid@example.com', 'wrongpassword');
    await loginPage.expectLoginError('Invalid credentials');
    await expect(page).toHaveURL(/\/login/);
  });

  test('should be accessible (WCAG 2.1 AA)', async ({ page }) => {
    await injectAxe(page);
    await checkA11y(page);
  });

  test('should redirect if already authenticated', async ({ page }) => {
    await loginPage.login('user@example.com', 'password123');
    await loginPage.expectLoginSuccess();
    await loginPage.goto();
    await expect(page).toHaveURL(/\/dashboard/);
  });
});
```

### Output

```
✅ E2E completado: SPEC-E2E-LOGIN-001

📊 Resumen:
   PLAN: ✅ 4 scenarios identificados
   GENERATE: ✅ 2 Page Objects + 4 tests
   EXECUTE: ✅ 12 tests (4 tests × 3 browsers)
   REPORT: ✅ Reporte HTML generado

📈 Métricas:
   Tests: 12/12 passing ✅
   Browsers: Chromium ✓, Firefox ✓, WebKit ✓
   Coverage: 100% acceptance criteria ✅
   Accessibility: WCAG 2.1 AA ✅
   Visual: Baseline created ✅
   Performance: LCP 1.8s ✅

📁 Archivos:
   tests/pages/LoginPage.ts
   tests/pages/DashboardPage.ts
   tests/e2e/login.spec.ts

🎯 Próximo: /mj2:quality-check
```

---

## 🎓 Key Playwright Features

### Auto-Waiting
```typescript
// No manual waits needed!
await page.getByRole('button').click();
// Playwright auto-waits for element to be:
// - Attached to DOM
// - Visible
// - Enabled
// - Stable (not animating)
```

### Network Mocking
```typescript
// Mock API responses
await page.route('**/api/users', async route => {
  await route.fulfill({
    status: 200,
    body: JSON.stringify([{ id: 1, name: 'John' }])
  });
});
```

### Visual Regression
```typescript
// Screenshot comparison
await expect(page).toHaveScreenshot('homepage.png');
// First run: creates baseline
// Later runs: compares against baseline
```

### Accessibility
```typescript
// WCAG 2.1 AA validation
await injectAxe(page);
await checkA11y(page, null, {
  rules: {
    'color-contrast': { enabled: true },
    'label': { enabled: true }
  }
});
```

### Cross-Browser
```typescript
// Configured in playwright.config.ts
projects: [
  { name: 'chromium', use: { ...devices['Desktop Chrome'] } },
  { name: 'firefox', use: { ...devices['Desktop Firefox'] } },
  { name: 'webkit', use: { ...devices['Desktop Safari'] } },
]
```

---

## ✅ Success Criteria

- [x] playwright.md skill created (752 lines)
- [x] e2e-tester.md agent created (685 lines)
- [x] mj2-4-e2e.md command created (307 lines)
- [x] issue-32.md documentation created
- [x] Page Object Model patterns documented
- [x] Visual regression testing covered
- [x] API mocking strategies included
- [x] CI/CD integration examples provided
- [x] Accessibility testing integrated
- [x] Performance testing basics included
- [x] All files committed to feature branch
- [x] Merged to main following GitFlow
- [x] Issue documented and closed

---

## 🎯 Testing Pyramid Now Complete

```
         ▲
        /E\          ← Issue #32 (THIS)
       /2E\            Playwright E2E
      /_____\
     /       \
    /Component\      ← Issue #31
   /___________\       frontend-builder
  /             \
 /  Integration  \   ← Issue #27
/_________________\    Testcontainers
       Unit           ← Issue #21
                        tdd-implementer
```

**Full Testing Stack:**
- ✅ Unit tests (xUnit)
- ✅ Integration tests (Testcontainers)
- ✅ Component tests (Vitest + RTL)
- ✅ E2E tests (Playwright) ← **This issue**

---

## 🔗 Integration Points

### With Backend (tdd-implementer)
```csharp
// Backend API must be ready
[HttpPost("/api/auth/login")]
public async Task<IActionResult> Login(LoginRequest request)
{
    // ... implementation
}
```

### With Frontend (frontend-builder)
```typescript
// Frontend components must be deployed
<LoginForm onSubmit={handleLogin} />
```

### With quality-gate
```bash
/mj2:4-e2e E2E-LOGIN-001  # This agent
/mj2:quality-check        # Validates all
```

---

## 📚 Resources

**Playwright:**
- Official Docs: https://playwright.dev/
- Page Object Model: https://playwright.dev/docs/pom
- Visual Testing: https://playwright.dev/docs/test-snapshots
- CI/CD: https://playwright.dev/docs/ci

**Related:**
- STACK.md > Testing > Playwright
- Skills: testing/playwright.md, foundation/testing.md
- Agents: tdd-implementer, frontend-builder, quality-gate

**Adapted From:**
- moai-adk/playwright-webapp-testing
- moai-adk/mcp-playwright-integrator

**ROADMAP Reference:**
- Section: v0.2.0 - Frontend Foundation
- Location: docs/ROADMAP.md lines 296-312

---

## 📈 Metrics Summary

| Metric | Value |
|--------|-------|
| **Files Created** | 4 (3 main + 1 doc) |
| **Total Lines** | 1,744 |
| **Skills** | 1 (playwright) |
| **Agents** | 1 (e2e-tester) |
| **Commands** | 1 (mj2-4-e2e) |
| **Browsers Supported** | 3 (Chromium, Firefox, WebKit) |
| **Test Types** | E2E, Accessibility, Visual, Performance |

---

## 🚀 Next Steps (Issue #33)

With E2E testing complete, the testing pyramid is finished. Next:

**Issue #33:** Frontend Testing Stack (Vitest + React Testing Library)
- Vitest configuration and patterns
- React Testing Library best practices
- Component testing strategies
- Mocking patterns

**Prerequisites completed:** ✅
- Backend TDD (tdd-implementer) ✅
- Frontend CDD (frontend-builder) ✅
- E2E testing (e2e-tester) ✅ ← **This issue**

**Ready for:**
- Issue #33: Frontend Testing Stack detail
- Issue #34: Docker Foundation
- v0.3.0: Full-stack + DevOps

---

**Completed by:** Claude Code
**Commit:** feature/issue-32-playwright-e2e → main
**Files:** 4 (playwright.md, e2e-tester.md, mj2-4-e2e.md, issue-32.md)
**Lines Added:** ~1,744
**Testing Pyramid:** ✅ **COMPLETE**
