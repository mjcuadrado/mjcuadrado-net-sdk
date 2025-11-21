# Issue #19: MJ² Skills

**Status:** ✅ Closed
**Created:** 2024-11-21
**Closed:** 2024-11-21
**Agent:** Multiple (core methodology)
**Commit:** 4bfda70

---

## Objetivo

Crear 2 Skills de MJ² en `.claude/skills/mj2/` con conocimiento específico del sistema mj2 - el workflow de 4 pasos y las best practices.

---

## Skills Creados

### 1. workflow-core.md (539 líneas)

**Contenido:**

#### Overview
```
0. PROJECT → 1. PLAN → 2. RUN → 3. SYNC
   ↓            ↓         ↓        ↓
Initialize   SPEC     TDD     Docs
```

#### Step 0: PROJECT (Initialize)
- **Propósito:** Inicializar o optimizar proyecto .NET 9
- **Comando:** `/mj2:0-project`
- **Agente:** project-manager
- **Lo que hace:**
  - Detecta si proyecto existe
  - Nuevo: Entrevista usuario, crea estructura
  - Existente: Analiza, sugiere mejoras
  - Crea `.mjcuadrado-net-sdk/`
- **Output:** Proyecto inicializado, config.json creado
- **Skills usados:** foundation/trust, tags, specs, dotnet/csharp

#### Step 1: PLAN (Specification)
- **Propósito:** Crear especificación clara y completa
- **Comando:** `/mj2:1-plan "feature description"`
- **Agente:** spec-builder
- **Lo que hace:**
  - Analiza descripción
  - Detecta dominio (AUTH, USER, API, etc.)
  - Hace preguntas clarificadoras
  - Genera SPEC-{DOMAIN}-{NNN}
  - Crea spec.md (EARS), plan.md, acceptance.md
  - Crea rama feature/SPEC-{ID}
- **Preguntas típicas:**
  - AUTH: método auth, password requirements, JWT, MFA
  - USER: campos perfil, cambiar email, upload foto
  - API: método HTTP, formato, autenticación, rate limiting
- **Output:** SPEC creada con 3 archivos, rama creada
- **Skills usados:** foundation/specs, ears, tags, git

#### Step 2: RUN (Implementation)
- **Propósito:** Implementar con TDD estricto
- **Comando:** `/mj2:2-run SPEC-ID`
- **Agente:** tdd-implementer
- **Ciclo TDD:**
  1. **🔴 RED:** Tests que fallan
     - Lee SPEC
     - Diseña tests
     - Verifica que fallan
     - Commit: "🔴 test(SPEC-ID): add failing tests"
  2. **🟢 GREEN:** Implementación mínima
     - Hace pasar los tests
     - Commit: "🟢 feat(SPEC-ID): implement feature"
  3. **♻️ REFACTOR:** Mejorar calidad
     - Aplica TRUST 5
     - Mantiene tests verdes
     - Valida coverage ≥85%
     - Commit: "♻️ refactor(SPEC-ID): improve quality"
- **Output:** Feature implementada, tests passing, coverage ≥85%
- **Skills usados:** dotnet/xunit, csharp, ef-core, aspnet-core, foundation/trust, tags
- **Validación automática:** /mj2:quality-check ejecutado después de REFACTOR

#### Step 3: SYNC (Documentation)
- **Propósito:** Sincronizar documentación con código
- **Comando:** `/mj2:3-sync SPEC-ID`
- **Agente:** doc-syncer
- **Lo que hace:**
  - Analiza código implementado
  - Actualiza README.md, docs/architecture.md, docs/api.md
  - Actualiza CHANGELOG.md
  - Añade @DOC: tags
  - Completa TAG chain
  - Commit: "📚 docs(SPEC-ID): sync documentation"
  - Trigger git-manager (personal: merge, team: PR)
- **Output:**
  - Docs actualizados
  - TAG chain completa
  - Personal mode: Merged to main, branch deleted
  - Team mode: Draft PR created
- **Skills usados:** foundation/tags, git

#### Flujo Completo - Ejemplo
```bash
# Authentication con JWT
/mj2:0-project              # Proyecto listo
/mj2:1-plan "auth JWT"      # SPEC-AUTH-001 creada
/mj2:2-run AUTH-001         # TDD: 🔴 → 🟢 → ♻️
/mj2:3-sync AUTH-001        # Docs + merge

# ✅ Feature completa en ~20 minutos
```

#### Filosofía del Workflow

**Principios:**
1. **SPEC-First:** Nunca código sin SPEC
2. **TDD Estricto:** RED → GREEN → REFACTOR sin excepciones
3. **Calidad No Negociable:** TRUST 5, coverage ≥85%
4. **Trazabilidad Total:** @SPEC → @TEST → @CODE → @DOC
5. **Documentación Viva:** Docs sincronizan con código

**Por qué 4 pasos:**
- 0-PROJECT: Base sólida
- 1-PLAN: Claridad antes de código
- 2-RUN: Implementación disciplinada
- 3-SYNC: Documentación coherente

**No se pueden saltar pasos.**

#### Métricas de Éxito
- SPEC clarity: 4.5/5
- Test coverage: ≥85%
- TRUST 5 compliance: 100%
- TAG chain complete: 100%
- Docs synced: 100%
- Time to implement: <1 day

**Usado por:** Todos los agents mj2 (project-manager, spec-builder, tdd-implementer, doc-syncer, quality-gate, git-manager)

---

### 2. practices.md (610 líneas)

**Contenido:**

#### Context Management
- **Problema:** Context window limits (200K tokens)
- **Solución:** `/clear` estratégico después de cada fase mayor
- **✅ Hacer /clear:**
  - Después de completar SPEC
  - Después de completar implementación
  - Después de sincronizar docs
  - Antes de cambiar a otra SPEC
- **❌ NO hacer /clear:**
  - Durante fase RED-GREEN-REFACTOR
  - Durante preguntas clarificadoras
  - En medio de un commit

#### Error Recovery
- **RED phase:**
  - Tests no compilan → Fix syntax, verificar imports
  - Tests compilan pero error lógico → Revisar assertions
- **GREEN phase:**
  - Tests no pasan → Debug, volver a RED si stuck
  - Coverage baja → Agregar tests específicos
- **REFACTOR:**
  - Tests fallan → `git reset --hard HEAD~1`, refactor más pequeño
  - Coverage baja → Verificar código muerto eliminado

#### Agent Delegation

**Cuándo usar cada agente:**
- **project-manager:** Proyecto nuevo, cambiar config, optimizar estructura
- **spec-builder:** Feature nueva, requisitos ambiguos, cambio grande
- **tdd-implementer:** Implementar SPEC, refactorizar con tests
- **doc-syncer:** Después de implementación, completar TAG chain
- **quality-gate:** Manual check, CI/CD pipeline, pre-merge
- **git-manager:** Merge manual, branch cleanup, PR creation

#### SPEC Writing Best Practices

**Granularity:**
- ❌ Too big: SPEC con 50+ requirements, >1 día
- ❌ Too small: SPEC para cambiar color de botón, <2 horas
- ✅ Good: Feature completa, 4-8 horas, testable

**Clarity:**
- ❌ Vague: "The system should handle users"
- ✅ Clear (EARS): "WHEN user submits valid credentials THEN system SHALL return JWT token WITH 24-hour expiration"

#### Testing Strategies

**Organization:**
```
tests/
├── Auth/
│   ├── AuthServiceTests.cs
│   └── AuthControllerTests.cs
├── User/
│   └── UserServiceTests.cs
└── Integration/
    └── AuthIntegrationTests.cs
```

**Coverage targets:**
- Core logic: 95%
- Services: 90%
- Controllers: 80%
- Overall: ≥85%

**Naming:** `MethodName_Scenario_ExpectedResult`

#### Git Strategies

**Personal Mode:**
- Auto-merge después de sync
- feature/SPEC-{ID} → main (merge --no-ff)
- Branch deleted automáticamente
- Rápido, sin overhead

**Team Mode:**
- Draft PR después de sync
- Asignar reviewers, CI/CD checks
- Esperar approval
- Merge (squash o merge --no-ff)
- Code review, quality gate

**Commit Messages:**
```bash
🔴 test(AUTH-001): add failing tests
🟢 feat(AUTH-001): implement auth service
♻️ refactor(AUTH-001): improve code quality
📚 docs(AUTH-001): sync documentation
```

#### Performance Tips

**Parallel Development:**
```bash
# Branch 1: AUTH
git checkout -b feature/SPEC-AUTH-001
/mj2:2-run AUTH-001

# Mientras esperas review
git checkout -b feature/SPEC-USER-001
/mj2:2-run USER-001
```

**Batch Operations:**
```bash
# Crear múltiples SPECs
/mj2:1-plan "view profile"    # USER-001
/mj2:1-plan "edit profile"    # USER-002
/mj2:1-plan "delete account"  # USER-003

# Implementar secuencialmente
for spec in USER-001 USER-002 USER-003; do
    /mj2:2-run $spec
    /mj2:3-sync $spec
done
```

#### Common Pitfalls

1. **❌ Saltarse SPEC**
   - Problema: Ir directo a código
   - Consecuencia: Requisitos ambiguos, refactors grandes
   - Solución: SIEMPRE crear SPEC primero

2. **❌ Saltarse tests**
   - Problema: Implementar sin RED phase
   - Consecuencia: Bajo coverage, bugs
   - Solución: TDD estricto, sin excepciones

3. **❌ No limpiar contexto**
   - Problema: Conversación muy larga (200+ mensajes)
   - Consecuencia: Claude confundido, respuestas lentas
   - Solución: /clear después de cada fase mayor

4. **❌ SPECs muy grandes**
   - Problema: SPEC de 50+ requirements
   - Consecuencia: Ciclo muy largo, difícil implementar
   - Solución: Dividir en múltiples SPECs pequeños

#### Real-World Examples

**E-commerce Checkout:**
```bash
# Dividir en SPECs pequeñas
SPEC-CART-001: Add to Cart (4h)
SPEC-CART-002: Update Quantities (2h)
SPEC-CART-003: Apply Coupon (4h)
SPEC-ORDER-001: Create Order (6h)
SPEC-PAYMENT-001: Process Payment (8h)

# Total: 24h = 3 días
```

**Blog System:**
```bash
SPEC-POST-001: Create Post (4h)
SPEC-POST-002: Edit Post (2h)
SPEC-POST-003: Delete Post (2h)
SPEC-POST-004: Publish Post (3h)
SPEC-COMMENT-001: Add Comment (3h)

# Total: 14h = 2 días
```

**Usado por:** Todos los agents mj2 y usuarios para entender best practices

---

## Estadísticas

| Skill | Líneas | Secciones principales |
|-------|--------|-----------------------|
| workflow-core.md | 539 | Step 0, 1, 2, 3 + filosofía + métricas + troubleshooting |
| practices.md | 610 | Context, errors, agents, SPEC, testing, git, perf, pitfalls, examples |
| **Total** | **1,149** | **18** |

---

## Comparación con otros Skills

### Foundation Skills (Issue #17) - 3,238 líneas
- **Qué:** Principios universales
- **Contenido:** TRUST 5, TAG system, SPEC format, EARS syntax, Git workflows
- **Para quién:** Todos los proyectos
- **Usado por:** Todos los agents

### .NET Skills (Issue #18) - 2,703 líneas
- **Qué:** Tecnología específica
- **Contenido:** C# 13, xUnit, EF Core, ASP.NET Core
- **Para quién:** Proyectos .NET
- **Usado por:** tdd-implementer, quality-gate

### MJ² Skills (Issue #19) - 1,149 líneas ← Este issue
- **Qué:** Metodología del sistema mj2
- **Contenido:** 4-step workflow, best practices
- **Para quién:** Usuarios del sistema mj2
- **Usado por:** Todos los agents mj2 + usuarios

---

## Filosofía

```
Command → Agent → Skill
   ↓        ↓        ↓
 Simple  Orquesta  Knowledge
```

**MJ² Skills = Cómo usar el sistema**
- Documentan el proceso completo
- Explican cuándo usar cada comando
- Proveen best practices y pitfalls
- Incluyen ejemplos reales

**Complementan Foundation y .NET Skills:**
- Foundation: QUÉ principios seguir
- .NET: CÓMO implementar técnicamente
- MJ²: CUÁNDO y POR QUÉ usar el sistema

---

## Validación Final

```bash
# Verificar que los Skills existen
ls -lh .claude/skills/mj2/

# workflow-core.md   539 líneas
# practices.md       610 líneas

# Total: 1,149 líneas de metodología mj2
```

---

## Impacto

**Antes:**
- Workflow implícito
- Best practices no documentadas
- Cada usuario interpretaba diferente
- Errores comunes repetidos

**Después:**
- Workflow explícito y claro
- Best practices documentadas
- Consistencia entre usuarios
- Errores prevenidos con ejemplos

---

## Próximos Pasos

1. ✅ Crear Foundation Skills (Issue #17)
2. ✅ Crear .NET Skills (Issue #18)
3. ✅ Crear MJ² Skills (Issue #19) ← Este issue
4. ⏳ Issue #20: Hooks configuration
5. ⏳ Actualizar agents para referenciar Skills
6. ⏳ Testing del workflow completo
7. ⏳ Documentación usuario final

---

## Referencias

- Commit: 4bfda70
- Files:
  - `.claude/skills/mj2/workflow-core.md`
  - `.claude/skills/mj2/practices.md`
- GitHub Issue: #19
- Related Issues:
  - #17 (Foundation Skills)
  - #18 (.NET Skills)

---

**mj2: Disciplined .NET 9 development workflow**
