---
name: specs
description: SPEC format and structure for mj2
version: 0.1.0
tags: [foundation, specification, structure]
---

# SPEC Format

Formato estándar para especificaciones en mj2.

## Estructura de SPEC

Cada SPEC consta de 3 archivos en `docs/specs/SPEC-{ID}/`:

1. **spec.md** - Especificación principal
2. **plan.md** - Plan de implementación
3. **acceptance.md** - Criterios de aceptación

---

## 1. spec.md

### Estructura completa

```markdown
---
spec_id: SPEC-{DOMAIN}-{NNN}
title: Feature Title
domain: DOMAIN
status: draft | in-progress | completed
created: YYYY-MM-DD
author: @username
tags: [tag1, tag2, tag3]
complexity: low | medium | high
estimated_hours: X-Y
---

# SPEC-{DOMAIN}-{NNN}: Feature Title

## Overview
<!-- @SPEC:EX-{DOMAIN}-{NNN} -->
Brief description of the feature.

## Stakeholders
- **Users:** What they need
- **Developers:** What they must build
- **Business:** What value it provides

## Requirements

### Functional Requirements

#### FR-1: Requirement Title
**@SPEC:EX-{DOMAIN}-{NNN}:FR-1**
Requirement in EARS format.

### Non-Functional Requirements

#### NFR-1: Requirement Title
**@SPEC:EX-{DOMAIN}-{NNN}:NFR-1**
Non-functional requirement.

## Dependencies
- External dependencies
- Internal dependencies

## Risks
1. Risk description → Mitigation strategy

## References
- Links to related SPECs
- External documentation
```

### Ejemplo completo

```markdown
---
spec_id: SPEC-AUTH-001
title: User Authentication with JWT
domain: AUTH
status: draft
created: 2024-11-20
author: @mjcuadrado
tags: [authentication, jwt, security]
complexity: medium
estimated_hours: 8-12
---

# SPEC-AUTH-001: User Authentication with JWT

## Overview
<!-- @SPEC:EX-AUTH-001 -->
Implement JWT-based authentication system for API access.

## Stakeholders
- **Users:** Secure access to their accounts
- **Developers:** Clear authentication API
- **Business:** Compliance with security standards

## Requirements

### Functional Requirements

#### FR-1: User Login
**@SPEC:EX-AUTH-001:FR-1**
The system SHALL accept email and password for authentication.

Input validation:
- Email: Valid format, max 254 characters
- Password: Min 8 characters, at least one digit

#### FR-2: JWT Generation
**@SPEC:EX-AUTH-001:FR-2**
WHEN credentials validated successfully
THEN system SHALL generate JWT with:
- User ID claim
- Email claim
- Roles claim
- Expiration (24 hours from issue)

#### FR-3: Token Validation
**@SPEC:EX-AUTH-001:FR-3**
WHEN request includes JWT in Authorization header
THEN system SHALL validate:
- Signature is valid
- Token not expired
- User still exists

#### FR-4: Token Refresh (Optional)
**@SPEC:EX-AUTH-001:FR-4**
WHERE token expiring within 1 hour
THEN system MAY issue new token with same claims

### Non-Functional Requirements

#### NFR-1: Security
**@SPEC:EX-AUTH-001:NFR-1**
System SHALL NOT store plain passwords.
Passwords SHALL be hashed with bcrypt (≥10 rounds).

#### NFR-2: Performance
**@SPEC:EX-AUTH-001:NFR-2**
Token validation SHALL complete within 50ms (P95).

#### NFR-3: Availability
**@SPEC:EX-AUTH-001:NFR-3**
Authentication service SHALL maintain 99.9% uptime.

## Dependencies
- **External:**
  - BCrypt library for password hashing
  - JWT library (System.IdentityModel.Tokens.Jwt)
- **Internal:**
  - User repository
  - Configuration service

## Risks
1. **Token theft** → Mitigation: HTTPS only, short expiration
2. **Brute force attacks** → Mitigation: Rate limiting (5 attempts/min)
3. **Token expiration issues** → Mitigation: Refresh mechanism

## References
- [JWT RFC 7519](https://tools.ietf.org/html/rfc7519)
- [OWASP Authentication Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Authentication_Cheat_Sheet.html)
```

---

## 2. plan.md

### Estructura

```markdown
# Implementation Plan: SPEC-{ID}
<!-- @SPEC:EX-{ID} -->

## Phase 1: Title (X-Y hours)
1. Task 1
2. Task 2
3. Task 3

**Files:**
- src/Path/File1.cs
- src/Path/File2.cs

**Tests:**
- tests/Path/Test1.cs

## Phase 2: Title (X-Y hours)
...

## Total Estimate
X-Y hours across N phases
```

### Ejemplo completo

```markdown
# Implementation Plan: SPEC-AUTH-001
<!-- @SPEC:EX-AUTH-001 -->

## Phase 1: Core Authentication (4-6h)

1. **Create AuthService**
   - Login method with email/password
   - Password validation
   - User lookup

2. **Create JwtTokenService**
   - Generate JWT with claims
   - Validate JWT signature
   - Check expiration

3. **Create DTOs**
   - LoginRequest (email, password)
   - LoginResponse (token, expiresIn)
   - TokenValidationResult

**Files:**
- src/Auth/AuthService.cs
- src/Auth/JwtTokenService.cs
- src/Auth/DTOs/LoginRequest.cs
- src/Auth/DTOs/LoginResponse.cs
- src/Auth/DTOs/TokenValidationResult.cs

**Tests:**
- tests/Auth/AuthServiceTests.cs
- tests/Auth/JwtTokenServiceTests.cs

**Dependencies:**
- BCrypt.Net-Next NuGet package
- System.IdentityModel.Tokens.Jwt

## Phase 2: API Integration (2-3h)

1. **Create AuthController**
   - POST /api/auth/login endpoint
   - POST /api/auth/refresh endpoint

2. **Create JWT Middleware**
   - Extract token from Authorization header
   - Validate token
   - Set User principal

**Files:**
- src/Api/Controllers/AuthController.cs
- src/Api/Middleware/JwtMiddleware.cs

**Tests:**
- tests/Api/AuthControllerTests.cs
- tests/Api/Middleware/JwtMiddlewareTests.cs

## Phase 3: Security & Testing (2-3h)

1. **Security hardening**
   - Rate limiting configuration
   - HTTPS enforcement
   - CORS setup

2. **Integration tests**
   - Full auth flow (login → use token → refresh)
   - Invalid credentials scenarios
   - Token expiration scenarios

3. **Performance tests**
   - Validate <50ms token validation
   - Load test login endpoint

**Files:**
- src/Configuration/SecurityConfig.cs

**Tests:**
- tests/Integration/AuthIntegrationTests.cs
- tests/Performance/AuthPerformanceTests.cs

## Total Estimate
8-12 hours across 3 phases

## Success Criteria
- ✅ All tests passing (≥85% coverage)
- ✅ TRUST 5 validated
- ✅ API documented
- ✅ Security review passed
```

---

## 3. acceptance.md

### Estructura

```markdown
# Acceptance Criteria: SPEC-{ID}
<!-- @SPEC:EX-{ID} -->

## Scenarios

### Scenario Name
**Given** initial state
**When** action occurs
**Then** expected outcome

## Performance
- Metric: Target

## Security
- Requirement 1
- Requirement 2

## Coverage
- Unit tests: ≥X%
- Integration tests: ≥Y%
```

### Ejemplo completo

```markdown
# Acceptance Criteria: SPEC-AUTH-001
<!-- @SPEC:EX-AUTH-001 -->

## Scenarios

### Successful Login
**Given** valid user credentials (email: user@test.com, password: ValidPass123!)
**When** POST /api/auth/login with credentials
**Then** response contains:
- Status: 200 OK
- Body: `{ "token": "<jwt>", "expiresIn": 86400 }`
- JWT contains claims: userId, email, roles
- JWT expires in 24 hours

### Invalid Credentials - Wrong Password
**Given** valid email but wrong password
**When** POST /api/auth/login
**Then** response contains:
- Status: 401 Unauthorized
- Body: `{ "error": "Invalid credentials" }`
- No token returned

### Invalid Credentials - User Not Found
**Given** non-existent email
**When** POST /api/auth/login
**Then** response contains:
- Status: 401 Unauthorized
- Body: `{ "error": "Invalid credentials" }`
- Error message doesn't reveal if user exists (security)

### Token Validation - Valid Token
**Given** valid non-expired JWT token
**When** GET /api/protected with Authorization: Bearer <token>
**Then** request succeeds with user context available

### Token Validation - Expired Token
**Given** expired JWT token (>24 hours old)
**When** GET /api/protected with Authorization: Bearer <token>
**Then** response contains:
- Status: 401 Unauthorized
- Body: `{ "error": "Token expired" }`

### Token Validation - Invalid Signature
**Given** JWT with tampered signature
**When** GET /api/protected with Authorization: Bearer <token>
**Then** response contains:
- Status: 401 Unauthorized
- Body: `{ "error": "Invalid token" }`

### Token Refresh
**Given** valid token expiring within 1 hour
**When** POST /api/auth/refresh with current token
**Then** response contains:
- Status: 200 OK
- Body: `{ "token": "<new-jwt>", "expiresIn": 86400 }`
- New token has updated expiration
- Old token is invalidated

### Rate Limiting
**Given** 5 failed login attempts from same IP
**When** 6th attempt
**Then** response contains:
- Status: 429 Too Many Requests
- Body: `{ "error": "Too many attempts. Try again in 1 minute" }`

## Performance

- **Token validation:** ≤50ms (P95)
- **Login endpoint:** ≤200ms (P95)
- **Token generation:** ≤100ms (P95)
- **Throughput:** ≥1000 req/sec

## Security

- ✅ Passwords hashed with bcrypt (≥10 rounds)
- ✅ HTTPS only in production
- ✅ Rate limiting: 5 attempts/minute per IP
- ✅ No user enumeration (same error for wrong email/password)
- ✅ Tokens signed with secure key (≥256 bits)
- ✅ CORS configured restrictively
- ✅ No sensitive data in JWT claims

## Coverage

- **Unit tests:** ≥85%
- **Integration tests:** ≥70%
- **Security tests:** OWASP Top 10 coverage
- **Performance tests:** All critical paths
```

---

## Metadatos del SPEC

### Status
- `draft` - Initial creation
- `in-progress` - Being implemented
- `completed` - Implemented and documented
- `deprecated` - No longer used

### Complexity
- `low` - <4 hours, simple feature
- `medium` - 4-16 hours, moderate complexity
- `high` - >16 hours, complex feature

### Tags
Common tags: `authentication`, `api`, `security`, `database`, `ui`, `performance`

---

## Validación de SPEC

### Checklist
- [ ] YAML frontmatter completo
- [ ] @SPEC: tags presentes
- [ ] Requisitos en formato EARS
- [ ] plan.md con fases claras
- [ ] acceptance.md con escenarios testables
- [ ] Estimación de horas
- [ ] Riesgos identificados
- [ ] Dependencias listadas

### Script de validación

```bash
#!/bin/bash

spec_id="$1"
spec_dir="docs/specs/SPEC-${spec_id}"
spec_file="${spec_dir}/spec.md"

echo "Validating SPEC-${spec_id}..."
echo ""

# 1. Verificar archivos existen
if [ ! -f "$spec_file" ]; then
    echo "❌ spec.md not found"
    exit 1
fi
echo "✅ spec.md exists"

if [ ! -f "${spec_dir}/plan.md" ]; then
    echo "❌ plan.md not found"
    exit 1
fi
echo "✅ plan.md exists"

if [ ! -f "${spec_dir}/acceptance.md" ]; then
    echo "❌ acceptance.md not found"
    exit 1
fi
echo "✅ acceptance.md exists"

# 2. Verificar frontmatter
if ! grep -q "^spec_id:" "$spec_file"; then
    echo "❌ spec_id missing in frontmatter"
    exit 1
fi
echo "✅ spec_id present"

# 3. Verificar @SPEC: tags
tag_count=$(grep -c "@SPEC:EX-${spec_id}" "$spec_file")
if [ $tag_count -eq 0 ]; then
    echo "❌ No @SPEC: tags found"
    exit 1
fi
echo "✅ Found $tag_count @SPEC: tags"

# 4. Verificar requisitos
fr_count=$(grep -c "^#### FR-" "$spec_file")
if [ $fr_count -eq 0 ]; then
    echo "⚠️  No Functional Requirements found"
fi
echo "✅ Found $fr_count Functional Requirements"

echo ""
echo "========================================="
echo "SPEC Validation: ✅ PASSED"
echo "========================================="
```

---

## Referencias

- [IEEE 29148](https://standards.ieee.org/standard/29148-2018.html) - Requirements specification
- [EARS](https://alistairmavin.com/ears/) - Easy Approach to Requirements Syntax
- [Requirements Engineering](https://www.pmi.org/learning/library/requirements-engineering-6710) - Best practices

---

## Resumen

**SPEC = 3 archivos obligatorios**

1. **spec.md** → Requisitos en EARS + metadatos
2. **plan.md** → Fases de implementación
3. **acceptance.md** → Criterios testables

**Sin SPEC completa, no hay implementación.**
