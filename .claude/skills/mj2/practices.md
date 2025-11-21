---
name: practices
description: Best practices and patterns for mj2 development
version: 0.1.0
tags: [mj2, practices, patterns]
---

# MJ² Best Practices

Prácticas recomendadas para desarrollo con mj2.

## Context Management

### Problema: Context Window Limits
Claude tiene 200K tokens, pero el contexto crece rápidamente con:
- Archivos leídos
- Código generado
- Conversación larga
- Multiple iteraciones

### Solución: /clear estratégico

```bash
# Después de cada fase mayor
/mj2:1-plan "feature"
# SPEC creada
/clear  # ← Limpiar contexto

/mj2:2-run AUTH-001
# Implementación completa
/clear  # ← Limpiar contexto

/mj2:3-sync AUTH-001
# Docs sincronizados
/clear  # ← Limpiar contexto
```

### Cuándo hacer /clear

#### ✅ HACER /clear
- Después de completar SPEC
- Después de completar implementación (REFACTOR done)
- Después de sincronizar docs
- Antes de cambiar a otra SPEC
- Cuando Claude responde lento
- Cuando las respuestas son confusas

#### ❌ NO hacer /clear
- Durante fase RED-GREEN-REFACTOR
- Durante preguntas clarificadoras del agente
- En medio de un commit
- Durante resolución de conflictos

### Señales de que necesitas /clear
```
- Respuestas muy largas sin razón
- Claude menciona contexto de otra feature
- Errores "I apologize for the confusion"
- Tiempo de respuesta >30 segundos
- Contexto actual >150K tokens
```

---

## Error Recovery

### Error durante RED phase

#### Tests no compilan
```bash
# Revisar sintaxis
1. Verificar imports
2. Verificar tipos
3. Verificar nombres de clases/métodos
4. Si persiste: /clear y reintentar
```

#### Tests compilan pero error lógico
```bash
1. Revisar assertions
2. Verificar que el test valida el requirement correcto
3. Si unclear: volver a leer SPEC
```

### Error durante GREEN phase

#### Tests no pasan
```bash
1. Revisar implementación
2. Verificar lógica
3. Agregar logs/debug
4. Si stuck: volver a RED, agregar test más simple
```

#### Tests pasan pero cobertura baja
```bash
1. Identificar paths no cubiertos
2. Agregar tests específicos
3. No pasar a REFACTOR hasta coverage ≥85%
```

### Error durante REFACTOR

#### Tests fallan después de refactor
```bash
# CRÍTICO: Tests NUNCA deben fallar en REFACTOR
1. git reset --hard HEAD~1  (revertir último commit)
2. Refactor más pequeño
3. Correr tests después de CADA cambio
4. Commit solo si tests pasan
```

#### Coverage baja después de refactor
```bash
# Posible: código muerto eliminado
1. Verificar que código eliminado no era necesario
2. Si era necesario: restaurar + agregar tests
3. Si no era necesario: OK, continuar
```

---

## Agent Delegation

### Cuándo usar cada agente

#### project-manager
**Usar para:**
- Proyecto nuevo
- Cambiar configuración
- Optimizar estructura existente
- Migrar a nueva versión .NET

**NO usar para:**
- Implementar features (usar tdd-implementer)
- Crear SPECs (usar spec-builder)

#### spec-builder
**Usar para:**
- Feature nueva
- Requisitos ambiguos
- Cambio grande (>50 líneas)
- Integración compleja

**NO usar para:**
- Bugs simples (fix directo)
- Typos (fix directo)
- Refactoring sin cambio de comportamiento

#### tdd-implementer
**Usar para:**
- Implementar SPEC
- Refactorizar con tests
- TDD estricto
- Cualquier cambio de comportamiento

**NO usar para:**
- Crear SPEC (usar spec-builder)
- Docs (usar doc-syncer)

#### doc-syncer
**Usar para:**
- Después de implementación
- Docs desactualizados
- Completar TAG chain
- Actualizar README

**NO usar para:**
- Implementar código (usar tdd-implementer)
- Crear SPEC (usar spec-builder)

#### quality-gate
**Usar para:**
- Manual check antes de merge
- CI/CD pipeline validation
- Pre-merge validation
- Auditoría de calidad

**NO usar para:**
- Implementar (usar tdd-implementer)
- Fix de issues (ver y fix por separado)

#### git-manager
**Usar para:**
- Merge manual
- Branch cleanup
- PR creation
- Conflict resolution

**NO usar para:**
- Commits individuales (agents lo hacen)
- Push (agents lo hacen)

---

## SPEC Writing Best Practices

### SPEC Granularity

#### ❌ TOO BIG
```
SPEC-APP-001: Complete Application
- Login, profile, admin, reports, analytics, notifications...

Problemas:
- Imposible implementar en un ciclo
- Difícil testear
- Merge conflicts
- Difícil revertir si falla
```

#### ❌ TOO SMALL
```
SPEC-USER-001: Change button color
SPEC-USER-002: Change button text
SPEC-USER-003: Change button size

Problemas:
- Overhead de SPEC
- 3 branches, 3 PRs, 3 reviews
- Fragmentación
```

#### ✅ GOOD SIZE
```
SPEC-AUTH-001: User Authentication
- Login, logout, token
- Implementable en 8-12 horas
- Tests claros
- Merge limpio

SPEC-USER-001: User Profile
- View, edit profile
- Implementable en 4-6 horas
- Feature completa
```

### SPEC Clarity

#### ❌ VAGUE
```markdown
"The system should handle users"
"Make it secure"
"Should be fast"
```

**Problemas:**
- No testable
- Ambiguo
- Imposible validar

#### ✅ CLEAR (EARS format)
```markdown
WHEN user submits valid credentials
THEN system SHALL return JWT token
WITH 24-hour expiration
AND user_id claim
AND email claim

WHILE token is valid
THEN system SHALL allow access to protected endpoints

WHEN token expires
THEN system SHALL return 401 Unauthorized
```

**Ventajas:**
- Testable
- Sin ambigüedad
- Fácil validar

---

## Testing Strategies

### Test Organization

#### Por clase
```
tests/
├── Auth/
│   ├── AuthServiceTests.cs
│   ├── JwtTokenServiceTests.cs
│   └── AuthControllerTests.cs
├── User/
│   ├── UserServiceTests.cs
│   └── UserRepositoryTests.cs
└── Integration/
    └── AuthIntegrationTests.cs
```

#### Nombrado consistente
```csharp
// Format: MethodName_Scenario_ExpectedResult
[Fact]
public void Login_ValidCredentials_ReturnsToken() { }

[Fact]
public void Login_InvalidPassword_ThrowsUnauthorizedException() { }

[Fact]
public void Login_NonExistentUser_ThrowsNotFoundException() { }
```

### Coverage Strategy

#### Target por tipo
```
Core logic:   95%  (business rules)
Services:     90%  (orchestration)
Controllers:  80%  (HTTP endpoints)
Repositories: 85%  (data access)
DTOs:         70%  (properties)
Overall:      ≥85%
```

#### Priorizar
```
1. Business logic (MUST: 95%)
2. Security (MUST: 100%)
3. Data validation (MUST: 90%)
4. API endpoints (SHOULD: 80%)
5. DTOs/Models (NICE: 70%)
```

---

## Git Strategies

### Personal Mode

#### Workflow
```bash
# Auto-merge después de sync
feature/SPEC-AUTH-001 → main (merge --no-ff)
# Branch deleted automáticamente
# Push to remote

# Ventajas:
- Rápido
- Sin overhead
- Historia clara
```

#### Cuándo usar
- Solo developer
- Features pequeñas
- Prototipo rápido

### Team Mode

#### Workflow
```bash
# Draft PR después de sync
feature/SPEC-AUTH-001 → PR #42 (draft)
# Asignar reviewers
# CI/CD checks
# Esperar approval
# Merge (squash o merge --no-ff)

# Ventajas:
- Code review
- Quality gate
- Conocimiento compartido
```

#### Cuándo usar
- Múltiples developers
- Features críticas
- Compliance/auditoría

### Commit Messages

#### Formato
```bash
<emoji> <type>(SPEC-ID): <description>

Ejemplos:
🔴 test(AUTH-001): add failing tests for login
🟢 feat(AUTH-001): implement auth service
♻️ refactor(AUTH-001): improve code quality
📚 docs(AUTH-001): sync documentation
🐛 fix(AUTH-001): correct token expiration
```

#### Emojis
- 🔴 = test (RED phase)
- 🟢 = feat (GREEN phase)
- ♻️ = refactor (REFACTOR phase)
- 📚 = docs (documentation)
- 🐛 = fix (bug fix)
- 🔧 = chore (maintenance)

---

## Performance Tips

### Parallel Development

#### Trabajar en múltiples SPECs
```bash
# Branch 1
git checkout -b feature/SPEC-AUTH-001
/mj2:1-plan "auth"
/mj2:2-run AUTH-001

# Mientras esperas review (team mode)
git checkout main
git checkout -b feature/SPEC-USER-001
/mj2:1-plan "profile"
/mj2:2-run USER-001
```

#### Ventajas
- No bloqueado esperando review
- Mayor throughput
- Contexto switch claro (/clear entre features)

### Batch Operations

#### Múltiples features pequeñas
```bash
# Crear todas las SPECs primero
/mj2:1-plan "view profile"    # SPEC-USER-001
/clear
/mj2:1-plan "edit profile"    # SPEC-USER-002
/clear
/mj2:1-plan "delete account"  # SPEC-USER-003
/clear

# Implementar secuencialmente
for spec in USER-001 USER-002 USER-003; do
    /mj2:2-run $spec
    /clear
    /mj2:3-sync $spec
    /clear
done
```

---

## Common Pitfalls

### ❌ Saltarse SPEC
```
Problema: Ir directo a código
"Es solo un cambio pequeño"

Consecuencia:
- Requisitos ambiguos
- Refactors grandes
- Bugs inesperados
- Sin documentación

Solución: SIEMPRE crear SPEC primero
Excepción: Typos, formatting (no behavioral changes)
```

### ❌ Saltarse tests
```
Problema: Implementar sin RED phase
"Los tests son fáciles, los hago después"

Consecuencia:
- Bajo coverage
- Bugs en producción
- Refactor imposible
- Confianza baja

Solución: TDD estricto, sin excepciones
```

### ❌ No limpiar contexto
```
Problema: Conversación muy larga
200+ mensajes sin /clear

Consecuencia:
- Claude confundido
- Respuestas lentas
- Menciona features antiguas
- Errores de contexto

Solución: /clear después de cada fase mayor
```

### ❌ SPECs muy grandes
```
Problema: SPEC de 50+ requirements
"Vamos a hacer todo el módulo de una vez"

Consecuencia:
- Ciclo muy largo (>1 día)
- Difícil de implementar
- Merge conflicts
- Difícil de revertir

Solución: Dividir en múltiples SPECs pequeños (4-8h cada uno)
```

---

## Real-World Examples

### Example 1: E-commerce Checkout
```bash
# ❌ MAL: Una SPEC gigante
SPEC-CHECKOUT-001: Complete Checkout System
- Cart, payment, shipping, taxes, coupons, inventory...
(2-3 semanas de trabajo)

# ✅ BIEN: Dividir en SPECs
SPEC-CART-001: Add to Cart (4h)
SPEC-CART-002: Update Quantities (2h)
SPEC-CART-003: Apply Coupon (4h)
SPEC-ORDER-001: Create Order (6h)
SPEC-PAYMENT-001: Process Payment (8h)
SPEC-SHIPPING-001: Calculate Shipping (4h)

# Implementar secuencialmente
# Cada SPEC: 2-8 horas
# Total: 1-2 semanas (mismo tiempo, mejor calidad)
```

### Example 2: Blog System
```bash
# SPECs pequeñas, features completas
SPEC-POST-001: Create Post (4h)
SPEC-POST-002: Edit Post (2h)
SPEC-POST-003: Delete Post (2h)
SPEC-POST-004: Publish/Draft Post (3h)
SPEC-COMMENT-001: Add Comment (3h)
SPEC-COMMENT-002: Moderate Comments (4h)
SPEC-TAG-001: Tag Management (5h)

# Total: 23 horas = 3 días
# 7 features independientes
# 7 PRs (team mode)
# Fácil revertir si algo falla
```

### Example 3: User Management
```bash
# Sprint 1: Core functionality
SPEC-USER-001: User Registration (6h)
SPEC-USER-002: Email Verification (4h)
SPEC-USER-003: Password Reset (5h)

# Sprint 2: Profile
SPEC-USER-004: View Profile (2h)
SPEC-USER-005: Edit Profile (4h)
SPEC-USER-006: Upload Avatar (5h)

# Sprint 3: Admin
SPEC-USER-007: List Users (Admin) (4h)
SPEC-USER-008: Suspend User (Admin) (3h)
```

---

## Resumen

**Context Management:**
- /clear después de cada fase mayor
- Nunca /clear durante TDD cycle

**Error Recovery:**
- RED: Fix syntax, reintentar
- GREEN: Debug, volver a RED si stuck
- REFACTOR: Revert si tests fallan

**Agent Delegation:**
- Cada agent tiene su propósito
- No mezclar responsabilidades

**SPEC Granularity:**
- 4-8 horas por SPEC
- Features completas
- No demasiado grande ni pequeño

**Testing:**
- Organizar por clase
- Target: ≥85% overall
- Priorizar business logic

**Git:**
- Personal: auto-merge
- Team: Draft PR
- Commit messages con emoji

**Performance:**
- Parallel development
- Batch small features
- /clear frecuente

**Pitfalls:**
- NUNCA saltarse SPEC
- NUNCA saltarse tests
- SIEMPRE limpiar contexto
- SIEMPRE dividir SPECs grandes

---

## Referencias

- [Workflow Core](workflow-core.md)
- [TRUST 5](../foundation/trust.md)
- [TAG System](../foundation/tags.md)
- [SPEC Format](../foundation/specs.md)
- [Git Workflows](../foundation/git.md)
