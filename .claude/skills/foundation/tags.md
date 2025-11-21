---
name: tags
description: TAG system for full traceability in mj2
version: 0.1.0
tags: [foundation, traceability, tags]
---

# TAG System

Sistema de trazabilidad completo @SPEC → @TEST → @CODE → @DOC

## Introducción

El sistema TAG permite rastrear cada requisito desde la especificación hasta su implementación, tests y documentación.

**Cadena completa obligatoria:**
```
@SPEC:EX-{ID}:{REQ} → @TEST:EX-{ID}:{REQ} → @CODE:EX-{ID}:{REQ} → @DOC:EX-{ID}
```

---

## Tipos de TAGs

### @SPEC: - Specification

**Ubicación:** `docs/specs/SPEC-{ID}/spec.md`

**Propósito:** Marcar requisitos en especificaciones

**Formato:**
```markdown
<!-- @SPEC:EX-{SPEC-ID} -->
## Requirement Title
**@SPEC:EX-{SPEC-ID}:FR-{N}**
Requirement description in EARS format
```

**Ejemplo:**
```markdown
<!-- @SPEC:EX-AUTH-001 -->
# User Authentication

## Functional Requirements

### FR-1: User Login
**@SPEC:EX-AUTH-001:FR-1**
The system SHALL accept email and password for authentication.

### FR-2: Token Generation
**@SPEC:EX-AUTH-001:FR-2**
WHEN credentials validated successfully
THEN system SHALL generate JWT with 24-hour expiration
```

---

### @TEST: - Test

**Ubicación:** `tests/**/*.cs`

**Propósito:** Marcar tests que validan requisitos

**Formato:**
```csharp
// @TEST:EX-{SPEC-ID} | SPEC: SPEC-{SPEC-ID}.md
namespace MyApp.Tests;

public class ServiceTests
{
    // @TEST:EX-{SPEC-ID}:FR-{N}
    [Fact]
    public void MethodName_Scenario_ExpectedResult()
    {
        // Test implementation
    }
}
```

**Ejemplo:**
```csharp
// @TEST:EX-AUTH-001 | SPEC: SPEC-AUTH-001.md
using FluentAssertions;
using Xunit;

namespace MyApp.Tests.Auth;

public class AuthServiceTests
{
    private readonly AuthService _sut;

    public AuthServiceTests()
    {
        _sut = new AuthService();
    }

    // @TEST:EX-AUTH-001:FR-1
    [Fact]
    public void Login_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var email = "user@test.com";
        var password = "SecurePass123!";

        // Act
        var result = _sut.Login(email, password);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().NotBeNullOrEmpty();
    }

    // @TEST:EX-AUTH-001:FR-1
    [Fact]
    public void Login_InvalidCredentials_ThrowsException()
    {
        // Arrange
        var email = "user@test.com";
        var password = "wrong";

        // Act
        Action act = () => _sut.Login(email, password);

        // Assert
        act.Should().Throw<UnauthorizedAccessException>();
    }

    // @TEST:EX-AUTH-001:FR-2
    [Fact]
    public void Login_ValidCredentials_TokenExpires24Hours()
    {
        // Arrange
        var email = "user@test.com";
        var password = "SecurePass123!";

        // Act
        var result = _sut.Login(email, password);

        // Assert
        result.ExpiresIn.Should().Be(86400);  // 24 hours in seconds
    }
}
```

---

### @CODE: - Implementation

**Ubicación:** `src/**/*.cs`

**Propósito:** Marcar implementación de requisitos

**Formato:**
```csharp
// @CODE:EX-{SPEC-ID} | SPEC: SPEC-{SPEC-ID}.md | TEST: TestFile.cs
namespace MyApp.Service;

public class ServiceName
{
    // @CODE:EX-{SPEC-ID}:FR-{N}
    public ReturnType MethodName(Parameters params)
    {
        // Implementation
    }
}
```

**Ejemplo:**
```csharp
// @CODE:EX-AUTH-001 | SPEC: SPEC-AUTH-001.md | TEST: AuthServiceTests.cs
namespace MyApp.Auth;

/// <summary>
/// Authentication service implementing JWT-based authentication
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        IJwtGenerator jwtGenerator,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _passwordHasher = passwordHasher;
    }

    // @CODE:EX-AUTH-001:FR-1
    /// <summary>
    /// Authenticates user with email and password
    /// </summary>
    public LoginResult Login(string email, string password)
    {
        ValidateInputs(email, password);

        var user = _userRepository.GetByEmail(email);
        if (user == null || !_passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        return GenerateToken(user);
    }

    // @CODE:EX-AUTH-001:FR-2
    private LoginResult GenerateToken(User user)
    {
        var token = _jwtGenerator.Generate(user);
        return new LoginResult
        {
            Token = token,
            ExpiresIn = 86400  // 24 hours
        };
    }

    private void ValidateInputs(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email required", nameof(email));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password required", nameof(password));
    }
}
```

---

### @DOC: - Documentation

**Ubicación:** `README.md`, `docs/**/*.md`, `CHANGELOG.md`

**Propósito:** Marcar documentación de features

**Formato:**
```markdown
<!-- @DOC:EX-{SPEC-ID} | SPEC: SPEC-{SPEC-ID}.md -->
- ✅ Feature description
  - Details
  - See: [SPEC-{SPEC-ID}](path/to/spec.md)
```

**Ejemplo:**
```markdown
# Features

## Authentication (AUTH)
<!-- @DOC:EX-AUTH-001 | SPEC: SPEC-AUTH-001.md -->
- ✅ User authentication with JWT
  - Email/password login
  - Token generation with 24-hour expiration
  - Token validation middleware
  - See: [SPEC-AUTH-001](docs/specs/SPEC-AUTH-001/spec.md)

## User Management (USER)
<!-- @DOC:EX-USER-001 | SPEC: SPEC-USER-001.md -->
- ✅ User profile management
  - View profile
  - Edit profile information
  - Change password
  - See: [SPEC-USER-001](docs/specs/SPEC-USER-001/spec.md)
```

---

## Cadena Completa

### Flujo de TAGs

```
1. SPEC-BUILDER crea SPEC
   └─> @SPEC:EX-AUTH-001

2. TDD-IMPLEMENTER crea tests
   └─> @TEST:EX-AUTH-001 (referencia a @SPEC)

3. TDD-IMPLEMENTER implementa código
   └─> @CODE:EX-AUTH-001 (referencia a @SPEC y @TEST)

4. DOC-SYNCER actualiza docs
   └─> @DOC:EX-AUTH-001 (referencia a @SPEC)

✅ Cadena completa: @SPEC → @TEST → @CODE → @DOC
```

### Validación de cadena

```bash
#!/bin/bash

spec_id="$1"  # Example: AUTH-001

echo "Validating TAG chain for ${spec_id}..."
echo ""

# 1. @SPEC:
echo "1. Checking @SPEC:EX-${spec_id}..."
spec_file="docs/specs/SPEC-${spec_id}/spec.md"
if [ ! -f "$spec_file" ]; then
    echo "   ❌ SPEC file not found"
    exit 1
fi

spec_count=$(grep -c "@SPEC:EX-${spec_id}" "$spec_file")
if [ $spec_count -eq 0 ]; then
    echo "   ❌ No @SPEC tags found"
    exit 1
else
    echo "   ✅ Found $spec_count @SPEC tags"
fi

# 2. @TEST:
echo "2. Checking @TEST:EX-${spec_id}..."
test_files=$(grep -rl "@TEST:EX-${spec_id}" tests/)
test_count=$(echo "$test_files" | wc -w)
if [ $test_count -eq 0 ]; then
    echo "   ❌ No @TEST tags found"
    exit 1
else
    echo "   ✅ Found @TEST tags in $test_count files"
    echo "$test_files" | sed 's/^/      - /'
fi

# 3. @CODE:
echo "3. Checking @CODE:EX-${spec_id}..."
code_files=$(grep -rl "@CODE:EX-${spec_id}" src/)
code_count=$(echo "$code_files" | wc -w)
if [ $code_count -eq 0 ]; then
    echo "   ❌ No @CODE tags found"
    exit 1
else
    echo "   ✅ Found @CODE tags in $code_count files"
    echo "$code_files" | sed 's/^/      - /'
fi

# 4. @DOC:
echo "4. Checking @DOC:EX-${spec_id}..."
doc_count=$(grep -c "@DOC:EX-${spec_id}" README.md docs/**/*.md 2>/dev/null || echo 0)
if [ $doc_count -eq 0 ]; then
    echo "   ⚠️  No @DOC tags found (may not be synced yet)"
else
    echo "   ✅ Found $doc_count @DOC tags"
fi

echo ""
echo "========================================="
echo "TAG Chain: ✅ COMPLETE"
echo "========================================="
echo "@SPEC → @TEST → @CODE → @DOC"
```

---

## Convenciones

### Nomenclatura de IDs

**Formato:** `SPEC-{DOMAIN}-{NNN}`

**Dominios comunes:**
- `AUTH` - Authentication, Authorization
- `USER` - User management
- `ADMIN` - Admin features
- `DATA` - Data management
- `API` - API endpoints
- `UI` - User interface
- `CORE` - Core functionality

**Ejemplos:**
- `SPEC-AUTH-001` - User login
- `SPEC-AUTH-002` - Password reset
- `SPEC-USER-001` - Profile management
- `SPEC-API-001` - REST API endpoints

### Nomenclatura de requisitos

**Formato:** `FR-{N}` (Functional Requirement) o `NFR-{N}` (Non-Functional Requirement)

**Ejemplos:**
- `@SPEC:EX-AUTH-001:FR-1` - Login functionality
- `@SPEC:EX-AUTH-001:FR-2` - Token generation
- `@SPEC:EX-AUTH-001:NFR-1` - Security requirements

---

## Herramientas

### Buscar TAGs

```bash
# Buscar todos los @SPEC de un dominio
grep -r "@SPEC:EX-AUTH" docs/specs/

# Buscar todos los @TEST de una SPEC
grep -r "@TEST:EX-AUTH-001" tests/

# Buscar todos los @CODE de una SPEC
grep -r "@CODE:EX-AUTH-001" src/

# Buscar cadenas rotas (SPEC sin TEST)
for spec in $(grep -roh "@SPEC:EX-[A-Z]+-[0-9]+" docs/specs/ | sort -u); do
    test_exists=$(grep -r "$spec" tests/ | wc -l)
    if [ $test_exists -eq 0 ]; then
        echo "⚠️  $spec has no tests"
    fi
done
```

### Generar reporte

```bash
#!/bin/bash

echo "TAG Coverage Report"
echo "==================="
echo ""

# Total SPECs
total_specs=$(find docs/specs -name "spec.md" | wc -l)
echo "Total SPECs: $total_specs"

# SPECs con tests
specs_with_tests=0
for spec_file in $(find docs/specs -name "spec.md"); do
    spec_id=$(basename $(dirname $spec_file))
    test_count=$(grep -r "@TEST:EX-${spec_id#SPEC-}" tests/ | wc -l)
    if [ $test_count -gt 0 ]; then
        specs_with_tests=$((specs_with_tests + 1))
    fi
done
echo "SPECs with tests: $specs_with_tests"

# SPECs con código
specs_with_code=0
for spec_file in $(find docs/specs -name "spec.md"); do
    spec_id=$(basename $(dirname $spec_file))
    code_count=$(grep -r "@CODE:EX-${spec_id#SPEC-}" src/ | wc -l)
    if [ $code_count -gt 0 ]; then
        specs_with_code=$((specs_with_code + 1))
    fi
done
echo "SPECs with code: $specs_with_code"

# SPECs con docs
specs_with_docs=0
for spec_file in $(find docs/specs -name "spec.md"); do
    spec_id=$(basename $(dirname $spec_file))
    doc_count=$(grep -r "@DOC:EX-${spec_id#SPEC-}" README.md docs/ | wc -l)
    if [ $doc_count -gt 0 ]; then
        specs_with_docs=$((specs_with_docs + 1))
    fi
done
echo "SPECs with docs: $specs_with_docs"

echo ""
echo "Completion:"
echo "  Tests: $(echo "scale=1; $specs_with_tests * 100 / $total_specs" | bc)%"
echo "  Code:  $(echo "scale=1; $specs_with_code * 100 / $total_specs" | bc)%"
echo "  Docs:  $(echo "scale=1; $specs_with_docs * 100 / $total_specs" | bc)%"
```

---

## Beneficios

### Trazabilidad completa
- Cada línea de código rastreable a requisito original
- Cada test valida un requisito específico
- Documentación sincronizada con código

### Auditoría
- Validar que todos los requisitos están implementados
- Verificar que todo el código tiene tests
- Asegurar que la documentación está actualizada

### Mantenimiento
- Encontrar código relacionado a un requisito
- Identificar tests que validar al modificar código
- Actualizar documentación cuando cambia funcionalidad

### Compliance
- Demostrar cumplimiento de requisitos
- Generar reportes de trazabilidad
- Facilitar auditorías de calidad

---

## Referencias

- [ISO/IEC/IEEE 29148](https://www.iso.org/standard/45171.html) - Systems and software engineering - Requirements
- [Traceability Matrix](https://en.wikipedia.org/wiki/Traceability_matrix) - Concept
- [Requirements Traceability](https://www.tutorialspoint.com/software_testing_dictionary/requirements_traceability_matrix.htm) - Guide

---

## Resumen

**TAG system = Trazabilidad total**

1. **@SPEC:** → Requisitos (docs/specs/)
2. **@TEST:** → Tests que validan (tests/)
3. **@CODE:** → Implementación (src/)
4. **@DOC:** → Documentación (docs/)

**Cadena obligatoria: @SPEC → @TEST → @CODE → @DOC**

**Sin TAG chain completa, no hay feature completa.**
