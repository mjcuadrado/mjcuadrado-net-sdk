---
name: playwright
description: Modern E2E testing with Playwright - cross-browser automation, visual regression, and API mocking
version: 0.1.0
tags: [testing, e2e, playwright, automation, cross-browser]
---

# Playwright Skill

## 📚 Overview

**Playwright** is a modern end-to-end testing framework for web applications. It enables reliable, cross-browser automation with powerful features like auto-waiting, network interception, and visual regression testing.

**Use Cases:**
- End-to-end user flow testing
- Cross-browser compatibility validation
- Visual regression testing
- API mocking and network interception
- Accessibility testing
- Performance monitoring

**Why Playwright:**
- **Auto-wait:** Automatically waits for elements to be ready
- **Multi-browser:** Chromium, Firefox, WebKit (Safari) support
- **Fast & Reliable:** Parallel execution, isolation, retry mechanisms
- **Rich API:** Screenshots, videos, traces, network control
- **TypeScript First:** Built-in TypeScript support

---

## 🚀 Installation & Setup

### Install Playwright

```bash
npm init playwright@latest
# or
pnpm create playwright
```

**What it creates:**
- `playwright.config.ts` - Configuration
- `tests/` - Test directory
- `tests-examples/` - Example tests
- `.github/workflows/playwright.yml` - CI workflow

### Project Structure

```
project/
├── playwright.config.ts          # Playwright configuration
├── tests/
│   ├── e2e/
│   │   ├── login.spec.ts        # E2E tests
│   │   └── checkout.spec.ts
│   └── pages/
│       ├── LoginPage.ts          # Page Object Models
│       └── CheckoutPage.ts
├── playwright-report/            # HTML report (gitignored)
├── test-results/                 # Screenshots, videos (gitignored)
└── .github/workflows/
    └── playwright.yml            # CI/CD integration
```

---

## 🎯 Configuration

### playwright.config.ts

```typescript
import { defineConfig, devices } from '@playwright/test';

export default defineConfig({
  // Test directory
  testDir: './tests/e2e',

  // Timeout per test
  timeout: 30 * 1000,

  // Expect timeout
  expect: {
    timeout: 5000
  },

  // Run tests in files in parallel
  fullyParallel: true,

  // Fail the build on CI if you accidentally left test.only
  forbidOnly: !!process.env.CI,

  // Retry on CI only
  retries: process.env.CI ? 2 : 0,

  // Opt out of parallel tests on CI
  workers: process.env.CI ? 1 : undefined,

  // Reporter to use
  reporter: [
    ['html'],
    ['json', { outputFile: 'test-results/results.json' }],
    ['junit', { outputFile: 'test-results/junit.xml' }]
  ],

  // Shared settings for all projects
  use: {
    // Base URL for page.goto('/')
    baseURL: 'http://localhost:5173',

    // Collect trace when retrying failed test
    trace: 'on-first-retry',

    // Screenshot on failure
    screenshot: 'only-on-failure',

    // Video on failure
    video: 'retain-on-failure',
  },

  // Configure projects for major browsers
  projects: [
    {
      name: 'chromium',
      use: { ...devices['Desktop Chrome'] },
    },
    {
      name: 'firefox',
      use: { ...devices['Desktop Firefox'] },
    },
    {
      name: 'webkit',
      use: { ...devices['Desktop Safari'] },
    },
    // Mobile viewports
    {
      name: 'Mobile Chrome',
      use: { ...devices['Pixel 5'] },
    },
    {
      name: 'Mobile Safari',
      use: { ...devices['iPhone 12'] },
    },
  ],

  // Run local dev server before starting tests
  webServer: {
    command: 'npm run dev',
    url: 'http://localhost:5173',
    reuseExistingServer: !process.env.CI,
  },
});
```

---

## ✅ Writing Tests

### Basic Test Structure

```typescript
import { test, expect } from '@playwright/test';

test.describe('Login Flow', () => {
  test('should login successfully with valid credentials', async ({ page }) => {
    // Navigate
    await page.goto('/login');

    // Interact
    await page.getByLabel('Email').fill('user@example.com');
    await page.getByLabel('Password').fill('password123');
    await page.getByRole('button', { name: 'Login' }).click();

    // Assert
    await expect(page).toHaveURL('/dashboard');
    await expect(page.getByText('Welcome back')).toBeVisible();
  });

  test('should show error with invalid credentials', async ({ page }) => {
    await page.goto('/login');

    await page.getByLabel('Email').fill('invalid@example.com');
    await page.getByLabel('Password').fill('wrongpassword');
    await page.getByRole('button', { name: 'Login' }).click();

    await expect(page.getByText('Invalid credentials')).toBeVisible();
    await expect(page).toHaveURL('/login');
  });
});
```

### Locator Strategies

```typescript
// By role (best for accessibility)
await page.getByRole('button', { name: 'Submit' });
await page.getByRole('link', { name: 'Home' });
await page.getByRole('textbox', { name: 'Email' });

// By label (forms)
await page.getByLabel('Email');
await page.getByLabel('Password');

// By placeholder
await page.getByPlaceholder('Enter your email');

// By text
await page.getByText('Welcome back');
await page.getByText(/welcome/i); // Regex

// By test ID (fallback)
await page.getByTestId('submit-button');

// By CSS selector (last resort)
await page.locator('.submit-btn');
await page.locator('#login-form');
```

**Priority:**
1. `getByRole()` - Accessibility-first
2. `getByLabel()` - Forms
3. `getByPlaceholder()` - Inputs
4. `getByText()` - Visible text
5. `getByTestId()` - Explicit test IDs
6. CSS/XPath selectors - Last resort

---

## 🎭 Page Object Model

### Page Class

```typescript
// tests/pages/LoginPage.ts
import { Page, Locator, expect } from '@playwright/test';

export class LoginPage {
  readonly page: Page;
  readonly emailInput: Locator;
  readonly passwordInput: Locator;
  readonly submitButton: Locator;
  readonly errorMessage: Locator;

  constructor(page: Page) {
    this.page = page;
    this.emailInput = page.getByLabel('Email');
    this.passwordInput = page.getByLabel('Password');
    this.submitButton = page.getByRole('button', { name: 'Login' });
    this.errorMessage = page.getByRole('alert');
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
    await expect(this.page).toHaveURL('/dashboard');
  }

  async expectLoginError(message: string) {
    await expect(this.errorMessage).toHaveText(message);
  }
}
```

### Using Page Objects

```typescript
import { test } from '@playwright/test';
import { LoginPage } from '../pages/LoginPage';

test('successful login', async ({ page }) => {
  const loginPage = new LoginPage(page);

  await loginPage.goto();
  await loginPage.login('user@example.com', 'password123');
  await loginPage.expectLoginSuccess();
});
```

---

## 🔄 Fixtures & Setup

### Custom Fixtures

```typescript
// tests/fixtures.ts
import { test as base } from '@playwright/test';
import { LoginPage } from './pages/LoginPage';

type MyFixtures = {
  loginPage: LoginPage;
  authenticatedPage: Page;
};

export const test = base.extend<MyFixtures>({
  loginPage: async ({ page }, use) => {
    await use(new LoginPage(page));
  },

  authenticatedPage: async ({ page }, use) => {
    const loginPage = new LoginPage(page);
    await loginPage.goto();
    await loginPage.login('user@example.com', 'password123');
    await use(page);
  },
});

export { expect } from '@playwright/test';
```

### Using Fixtures

```typescript
import { test, expect } from './fixtures';

test('dashboard shows user info', async ({ authenticatedPage }) => {
  // Already logged in via fixture
  await expect(authenticatedPage.getByText('Welcome back')).toBeVisible();
});
```

---

## 🌐 Network Interception

### Mocking API Responses

```typescript
test('shows users from mocked API', async ({ page }) => {
  // Mock API response
  await page.route('**/api/users', async route => {
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify([
        { id: 1, name: 'John Doe' },
        { id: 2, name: 'Jane Smith' }
      ])
    });
  });

  await page.goto('/users');

  await expect(page.getByText('John Doe')).toBeVisible();
  await expect(page.getByText('Jane Smith')).toBeVisible();
});
```

### Intercepting Requests

```typescript
test('submits form data correctly', async ({ page }) => {
  let requestData: any;

  // Intercept POST request
  await page.route('**/api/users', async (route, request) => {
    if (request.method() === 'POST') {
      requestData = request.postDataJSON();
      await route.fulfill({
        status: 201,
        body: JSON.stringify({ id: 1, ...requestData })
      });
    }
  });

  await page.goto('/users/new');
  await page.getByLabel('Name').fill('John Doe');
  await page.getByLabel('Email').fill('john@example.com');
  await page.getByRole('button', { name: 'Create' }).click();

  // Verify request data
  expect(requestData).toEqual({
    name: 'John Doe',
    email: 'john@example.com'
  });
});
```

### Wait for Network

```typescript
test('waits for API call', async ({ page }) => {
  await page.goto('/dashboard');

  // Wait for specific API call
  const response = await page.waitForResponse('**/api/users');
  const users = await response.json();

  expect(users).toHaveLength(10);
});
```

---

## 📸 Visual Regression Testing

### Screenshot Comparison

```typescript
test('homepage looks correct', async ({ page }) => {
  await page.goto('/');

  // Take screenshot and compare
  await expect(page).toHaveScreenshot('homepage.png');
});

test('button has correct styling', async ({ page }) => {
  await page.goto('/');

  const button = page.getByRole('button', { name: 'Submit' });
  await expect(button).toHaveScreenshot('submit-button.png');
});
```

**First run:** Creates baseline screenshots in `tests/e2e/*.spec.ts-snapshots/`
**Subsequent runs:** Compares against baseline, fails if different

### Update Snapshots

```bash
# Update all snapshots
npx playwright test --update-snapshots

# Update specific test
npx playwright test login.spec.ts --update-snapshots
```

### Visual Comparison Options

```typescript
await expect(page).toHaveScreenshot({
  // Tolerance for pixel differences
  maxDiffPixelRatio: 0.01,

  // Full page screenshot
  fullPage: true,

  // Hide dynamic elements
  mask: [page.getByText('2025-11-22')],

  // Custom animations
  animations: 'disabled',
});
```

---

## ♿ Accessibility Testing

### axe-core Integration

```bash
npm install -D @axe-core/playwright
```

```typescript
import { test, expect } from '@playwright/test';
import { injectAxe, checkA11y } from 'axe-playwright';

test('homepage is accessible', async ({ page }) => {
  await page.goto('/');

  await injectAxe(page);
  await checkA11y(page, null, {
    detailedReport: true,
    detailedReportOptions: {
      html: true,
    },
  });
});

test('login form is accessible', async ({ page }) => {
  await page.goto('/login');

  await injectAxe(page);

  // Check specific element
  await checkA11y(page, '#login-form', {
    rules: {
      'color-contrast': { enabled: true },
      'label': { enabled: true },
    },
  });
});
```

---

## 🎬 Debugging

### Debug Mode

```bash
# Run with headed browser and inspector
npx playwright test --debug

# Debug specific test
npx playwright test login.spec.ts --debug
```

### Trace Viewer

```bash
# Run tests with trace
npx playwright test --trace on

# Open trace viewer
npx playwright show-trace test-results/trace.zip
```

### Pause Execution

```typescript
test('debug this test', async ({ page }) => {
  await page.goto('/login');

  // Pause execution
  await page.pause();

  await page.getByLabel('Email').fill('user@example.com');
});
```

### Screenshots & Videos

```typescript
test('take screenshot', async ({ page }) => {
  await page.goto('/');

  // Manual screenshot
  await page.screenshot({ path: 'screenshot.png' });

  // Element screenshot
  const button = page.getByRole('button', { name: 'Submit' });
  await button.screenshot({ path: 'button.png' });
});
```

---

## 🚀 Running Tests

### Command Line

```bash
# Run all tests
npx playwright test

# Run specific file
npx playwright test login.spec.ts

# Run tests in headed mode
npx playwright test --headed

# Run tests in specific browser
npx playwright test --project=chromium

# Run with UI mode
npx playwright test --ui

# Generate HTML report
npx playwright show-report
```

### CI/CD Integration

```yaml
# .github/workflows/playwright.yml
name: Playwright Tests

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main, develop ]

jobs:
  test:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4

    - name: Setup Node.js
      uses: actions/setup-node@v4
      with:
        node-version: '20'

    - name: Install dependencies
      run: npm ci

    - name: Install Playwright Browsers
      run: npx playwright install --with-deps

    - name: Run Playwright tests
      run: npx playwright test

    - name: Upload test results
      if: always()
      uses: actions/upload-artifact@v4
      with:
        name: playwright-report
        path: playwright-report/
        retention-days: 30
```

---

## 📊 Best Practices

### 1. Use Stable Locators

```typescript
// ✅ Good - Accessible, stable
await page.getByRole('button', { name: 'Submit' });
await page.getByLabel('Email');

// ❌ Bad - Fragile, implementation-specific
await page.locator('.MuiButton-root.css-1234');
await page.locator('div > form > button:nth-child(3)');
```

### 2. Isolate Tests

```typescript
// ✅ Good - Each test is independent
test('test 1', async ({ page }) => {
  await page.goto('/');
  // ... test logic
});

test('test 2', async ({ page }) => {
  await page.goto('/');
  // ... test logic
});

// ❌ Bad - Tests depend on each other
let sharedState: any;

test('test 1', async ({ page }) => {
  sharedState = await page.evaluate(() => window.data);
});

test('test 2', async ({ page }) => {
  // Depends on test 1
  expect(sharedState).toBeDefined();
});
```

### 3. Use Page Objects

```typescript
// ✅ Good - Reusable, maintainable
const loginPage = new LoginPage(page);
await loginPage.login('user@example.com', 'password');

// ❌ Bad - Duplicated, hard to maintain
await page.getByLabel('Email').fill('user@example.com');
await page.getByLabel('Password').fill('password');
await page.getByRole('button', { name: 'Login' }).click();
```

### 4. Meaningful Assertions

```typescript
// ✅ Good - Specific, clear
await expect(page.getByRole('alert')).toHaveText('Login successful');
await expect(page).toHaveURL('/dashboard');

// ❌ Bad - Vague, unhelpful
await expect(page.locator('div')).toBeVisible();
```

### 5. Auto-waiting

```typescript
// ✅ Good - Playwright auto-waits
await page.getByRole('button').click();

// ❌ Bad - Unnecessary manual wait
await page.waitForTimeout(3000);
await page.getByRole('button').click();
```

---

## 🔗 Integration with mjcuadrado-net-sdk

### With Backend (ASP.NET Core)

```typescript
// Use real backend in E2E tests
test.use({
  baseURL: 'http://localhost:5000',
});

test('API integration', async ({ page }) => {
  // Real API call to .NET backend
  await page.goto('/users');

  // Wait for real data from database
  await expect(page.getByRole('row')).toHaveCount(10);
});
```

### With Frontend (React + Vite)

```typescript
// Tests use real React components
test('component interaction', async ({ page }) => {
  await page.goto('/login');

  // Interact with real MUI components
  const emailField = page.getByLabel('Email');
  await expect(emailField).toBeVisible();

  // Test React Hook Form validation
  await page.getByRole('button', { name: 'Login' }).click();
  await expect(page.getByText('Email is required')).toBeVisible();
});
```

---

## 📚 Resources

**Official Documentation:**
- Playwright: https://playwright.dev/
- API Reference: https://playwright.dev/docs/api/class-playwright
- Best Practices: https://playwright.dev/docs/best-practices

**Testing Patterns:**
- Page Object Model: https://playwright.dev/docs/pom
- Fixtures: https://playwright.dev/docs/test-fixtures
- Parallelization: https://playwright.dev/docs/test-parallel

**Advanced Topics:**
- Visual Regression: https://playwright.dev/docs/test-snapshots
- Network Mocking: https://playwright.dev/docs/network
- Accessibility: https://playwright.dev/docs/accessibility-testing

---

**Version:** 0.1.0
**Last Updated:** 2025-11-22
**Maintained by:** mjcuadrado-net-sdk
