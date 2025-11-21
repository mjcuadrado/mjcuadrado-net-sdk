---
name: ears
description: EARS (Easy Approach to Requirements Syntax) complete guide
version: 0.1.0
tags: [foundation, requirements, ears]
---

# EARS - Easy Approach to Requirements Syntax

Sintaxis EARS completa para escribir requisitos claros y testables.

## Introducción

EARS (Easy Approach to Requirements Syntax) es un estándar ISO/IEC/IEEE 29148 para escribir requisitos no ambiguos.

**5 tipos de requisitos:**
1. Ubiquitous (ubicuos)
2. Event-driven (basados en eventos)
3. State-driven (basados en estado)
4. Optional (opcionales)
5. Complex (complejos - combinaciones)

---

## 1. Ubiquitous (Ubicuos)

### Definición
Requisitos que siempre aplican, sin condiciones.

### Sintaxis
```
The <system> SHALL <action>
```

### Cuándo usar
- Funcionalidad básica siempre activa
- Reglas de negocio constantes
- Validaciones siempre aplicables

### Ejemplos

**✅ BIEN:**
```
The system SHALL validate email format before registration.

The API SHALL return 401 for unauthenticated requests.

The system SHALL hash passwords with bcrypt before storing.

The application SHALL log all failed login attempts.

The database SHALL enforce unique email addresses.
```

**❌ MAL:**
```
Email should be validated.  // ❌ Débil, no obligatorio

The system might check passwords.  // ❌ No determinista

Passwords need hashing.  // ❌ Pasivo, no claro quién lo hace
```

### Formato en SPEC

```markdown
#### FR-1: Email Validation
**@SPEC:EX-AUTH-001:FR-1**
The system SHALL validate email format before registration.

Validation rules:
- Valid RFC 5322 format
- Maximum 254 characters
- Not already registered
```

---

## 2. Event-Driven (Basados en eventos)

### Definición
Requisitos que se activan cuando ocurre un evento.

### Sintaxis
```
WHEN <trigger event>
THEN the <system> SHALL <action>
```

### Cuándo usar
- Respuesta a acciones del usuario
- Reacciones a eventos del sistema
- Cambios basados en triggers

### Ejemplos

**✅ BIEN:**
```
WHEN user submits login form
THEN the system SHALL validate credentials.

WHEN password validation fails
THEN the system SHALL return 401 Unauthorized.

WHEN JWT token is generated
THEN the system SHALL set expiration to 24 hours.

WHEN user account is created
THEN the system SHALL send welcome email.

WHEN API request includes invalid token
THEN the system SHALL reject request with 401 status.
```

**❌ MAL:**
```
After login, validate.  // ❌ No usa sintaxis WHEN/THEN

The system validates when needed.  // ❌ "when needed" es ambiguo

On form submit, do validation.  // ❌ No usa SHALL
```

### Formato en SPEC

```markdown
#### FR-2: Credential Validation
**@SPEC:EX-AUTH-001:FR-2**
WHEN user submits login form
THEN the system SHALL validate credentials.

Validation includes:
- Email exists in database
- Password matches hash
- Account not locked
```

---

## 3. State-Driven (Basados en estado)

### Definición
Requisitos que aplican mientras el sistema está en cierto estado.

### Sintaxis
```
WHILE <in state>
THEN the <system> SHALL <action>
```

o

```
WHILE <in state>
the <system> SHALL <action>
```

### Cuándo usar
- Comportamiento depende de estado del sistema
- Reglas que aplican durante una condición
- Restricciones temporales

### Ejemplos

**✅ BIEN:**
```
WHILE user is authenticated
THEN the system SHALL allow access to protected resources.

WHILE token is valid
the API SHALL accept requests.

WHILE account is locked
THEN the system SHALL reject login attempts.

WHILE password reset is pending
the system SHALL not allow login with old password.

WHILE subscription is active
THEN the user SHALL have access to premium features.
```

**❌ MAL:**
```
If authenticated, allow access.  // ❌ No usa WHILE/THEN

During valid session, user can access.  // ❌ No usa SHALL

When locked account, no login.  // ❌ Confunde WHEN (evento) con WHILE (estado)
```

### Formato en SPEC

```markdown
#### FR-3: Protected Resource Access
**@SPEC:EX-AUTH-001:FR-3**
WHILE user is authenticated
THEN the system SHALL allow access to protected resources.

Protected resources include:
- User profile endpoints
- Data modification APIs
- Admin panel (if authorized)
```

---

## 4. Optional (Opcionales)

### Definición
Requisitos opcionales que el sistema puede implementar.

### Sintaxis
```
WHERE <precondition>
the <system> MAY <action>
```

### Cuándo usar
- Funcionalidad nice-to-have
- Optimizaciones opcionales
- Features que pueden diferirse

### Ejemplos

**✅ BIEN:**
```
WHERE user is inactive for 30 days
the system MAY send re-engagement email.

WHERE token expires within 1 hour
the system MAY automatically refresh it.

WHERE network is slow
the application MAY cache responses locally.

WHERE user uploads large file
the system MAY compress before storing.

WHERE multiple login attempts fail
the system MAY implement progressive delays.
```

**❌ MAL:**
```
The system might cache.  // ❌ No usa WHERE/MAY

Optionally, compress files.  // ❌ No especifica condición

Could implement delays.  // ❌ No usa sintaxis formal
```

### Formato en SPEC

```markdown
#### FR-4: Token Auto-Refresh (Optional)
**@SPEC:EX-AUTH-001:FR-4**
WHERE token expires within 1 hour
the system MAY automatically refresh it.

Refresh criteria:
- Token still valid
- User still active
- No security flags raised
```

---

## 5. Complex (Complejos)

### Definición
Combinaciones de tipos anteriores para requisitos complejos.

### Sintaxis
```
WHILE <state>
WHEN <event>
THEN the <system> SHALL <action>
```

o

```
WHERE <precondition>
WHEN <event>
THEN the <system> SHALL/MAY <action>
```

### Cuándo usar
- Requisitos con múltiples condiciones
- Lógica de negocio compleja
- Combinación de estado y eventos

### Ejemplos

**✅ BIEN:**
```
WHILE user is authenticated
WHEN session timeout occurs
THEN the system SHALL log out user automatically.

WHERE user has admin role
WHEN user accesses admin panel
THEN the system SHALL allow full access.

WHILE account is locked
WHEN unlock time has passed
THEN the system SHALL automatically unlock account.

WHERE two-factor auth is enabled
WHEN user logs in successfully
THEN the system SHALL send verification code.

WHILE password reset is active
WHEN user clicks reset link after expiration
THEN the system SHALL reject reset attempt.
```

**❌ MAL:**
```
If authenticated and timeout, logout.  // ❌ No usa sintaxis formal

When admin accesses panel, allow if admin.  // ❌ Lógica confusa

Unlock after time passes if locked.  // ❌ Orden poco claro
```

### Formato en SPEC

```markdown
#### FR-5: Auto Logout on Timeout
**@SPEC:EX-AUTH-001:FR-5**
WHILE user is authenticated
WHEN session timeout occurs (30 minutes inactivity)
THEN the system SHALL log out user automatically.

Logout actions:
- Invalidate session token
- Clear session data
- Redirect to login page
```

---

## Uso de SHALL vs MAY

### SHALL
**Obligatorio** - El sistema DEBE implementar esto.

```
The system SHALL validate inputs.
WHEN user logs in THEN system SHALL check credentials.
```

### SHOULD
**Recomendado** - Se espera pero no es estrictamente obligatorio (evitar en EARS).

### MAY
**Opcional** - El sistema PUEDE implementar esto.

```
WHERE user is idle MAY system send reminder.
```

### SHALL NOT
**Prohibido** - El sistema NO DEBE hacer esto.

```
The system SHALL NOT store passwords in plain text.
WHEN user deletes account THEN system SHALL NOT retain personal data.
```

---

## Ejemplos Completos por Dominio

### Autenticación (AUTH)

```markdown
#### FR-1: Login - Ubiquitous
The system SHALL accept email and password for login.

#### FR-2: Validation - Event-driven
WHEN user submits credentials
THEN the system SHALL verify email exists and password matches.

#### FR-3: Access - State-driven
WHILE user is authenticated
THEN the system SHALL allow access to protected resources.

#### FR-4: Refresh - Optional
WHERE token expires within 1 hour
the system MAY automatically refresh token.

#### FR-5: Lockout - Complex
WHILE account has 5 failed attempts
WHEN user attempts to login
THEN the system SHALL reject attempt and lock account for 15 minutes.

#### NFR-1: Security - SHALL NOT
The system SHALL NOT store passwords in plain text.
```

### Gestión de Usuarios (USER)

```markdown
#### FR-1: Profile View - Ubiquitous
The system SHALL display user profile information.

#### FR-2: Profile Update - Event-driven
WHEN user submits profile changes
THEN the system SHALL validate and save changes.

#### FR-3: Edit Mode - State-driven
WHILE user is in edit mode
the system SHALL enable save and cancel buttons.

#### FR-4: Auto-save - Optional
WHERE user has unsaved changes for 5 minutes
the system MAY prompt to save.

#### FR-5: Email Change - Complex
WHILE user is authenticated
WHEN user changes email
THEN the system SHALL send verification to new email before updating.
```

### API (API)

```markdown
#### FR-1: Response Format - Ubiquitous
The API SHALL return JSON formatted responses.

#### FR-2: Error Handling - Event-driven
WHEN API encounters error
THEN the system SHALL return appropriate HTTP status code.

#### FR-3: Rate Limiting - State-driven
WHILE client exceeds rate limit
THEN the API SHALL return 429 Too Many Requests.

#### FR-4: Caching - Optional
WHERE response is cacheable
the API MAY set Cache-Control headers.

#### FR-5: Auth Required - Complex
WHILE endpoint requires authentication
WHEN request lacks valid token
THEN the API SHALL return 401 Unauthorized.
```

---

## Validación de EARS

### Checklist
- [ ] Usa sintaxis formal (WHEN/WHILE/WHERE)
- [ ] Usa SHALL o MAY (no SHOULD, MIGHT, COULD)
- [ ] Especifica el sistema ("the system", "the API")
- [ ] Acción clara y testable
- [ ] Sin ambigüedades (no "usually", "normally", "appropriate")
- [ ] Un requisito = una responsabilidad

### Señales de mal requisito

**Palabras a evitar:**
- ❌ "should", "might", "could" (ambiguo)
- ❌ "appropriate", "reasonable" (subjetivo)
- ❌ "usually", "normally", "typically" (no determinista)
- ❌ "as soon as possible", "quickly" (no medible)
- ❌ "user-friendly", "easy" (subjetivo)

**Estructuras a evitar:**
- ❌ Múltiples SHALL en un requisito
- ❌ Requisitos de implementación (HOW) en lugar de comportamiento (WHAT)
- ❌ Requisitos negativos sin SHALL NOT

---

## Herramientas

### Validar EARS en SPEC

```bash
#!/bin/bash

spec_file="$1"

echo "Validating EARS syntax in $spec_file..."
echo ""

# Buscar palabras prohibidas
echo "Checking for forbidden words..."
forbidden=$(grep -i "should\|might\|could\|appropriate\|reasonable\|usually\|typically" "$spec_file" | grep -v "^#\|^-" || true)

if [ -n "$forbidden" ]; then
    echo "⚠️  Found forbidden words:"
    echo "$forbidden"
    echo ""
fi

# Verificar SHALL/MAY
echo "Checking for SHALL/MAY..."
shall_count=$(grep -c "SHALL\|MAY" "$spec_file")
echo "✅ Found $shall_count SHALL/MAY statements"

# Verificar EARS patterns
echo "Checking EARS patterns..."
when_count=$(grep -c "^WHEN\|WHEN.*THEN" "$spec_file")
while_count=$(grep -c "^WHILE\|WHILE.*THEN\|WHILE.*SHALL" "$spec_file")
where_count=$(grep -c "^WHERE\|WHERE.*MAY" "$spec_file")

echo "   WHEN/THEN: $when_count"
echo "   WHILE/THEN: $while_count"
echo "   WHERE/MAY: $where_count"

echo ""
echo "Validation complete!"
```

---

## Referencias

- [EARS at NASA](https://www.nasa.gov/sec/features/ears-the-easy-approach-to-requirements-syntax.html)
- [IEEE 29148](https://standards.ieee.org/standard/29148-2018.html) - Requirements syntax
- [INCOSE Guide](https://www.incose.org/) - Requirements engineering

---

## Resumen

**EARS = 5 tipos de requisitos**

1. **Ubiquitous:** The system SHALL...
2. **Event-driven:** WHEN... THEN... SHALL...
3. **State-driven:** WHILE... THEN... SHALL...
4. **Optional:** WHERE... MAY...
5. **Complex:** WHILE... WHEN... THEN... SHALL...

**SHALL = obligatorio | MAY = opcional**

**Sin EARS, requisitos ambiguos. Sin requisitos claros, código impredecible.**
