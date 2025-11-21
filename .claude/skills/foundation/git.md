---
name: git
description: Git workflows and conventions for mj2
version: 0.1.0
tags: [foundation, git, workflow]
---

# Git Workflows

Workflows y convenciones de Git para mj2.

## Modos de Trabajo

### Personal Mode
- Auto-merge a main
- Sin Pull Requests
- Branches efímeras
- Rápido y limpio

### Team Mode
- Draft Pull Requests
- Code review required
- Git

Flow completo
- Colaboración

---

## Branch Naming

### Formato
```
feature/SPEC-{ID}
bugfix/SPEC-{ID}
hotfix/SPEC-{ID}
```

### Ejemplos
```bash
feature/SPEC-AUTH-001
feature/SPEC-USER-003
bugfix/SPEC-API-002
hotfix/SPEC-SECURITY-001
```

### Reglas
- **feature/** - Nueva funcionalidad (lo más común)
- **bugfix/** - Corrección de bug
- **hotfix/** - Fix urgente en producción
- **NO** usar nombres personales (no feature/john-login)
- **NO** usar descripciones largas (feature/SPEC-ID es suficiente)

---

## Commit Messages

### Formato
```
<emoji> <type>(<scope>): <subject>

<body>

<footer>
```

### Tipos de commits

**feat** - Nueva funcionalidad
```bash
🟢 feat(auth): implement login endpoint

Implemented JWT-based authentication with:
- Email/password validation
- Token generation
- 24-hour expiration

@CODE:EX-AUTH-001
```

**test** - Nuevos tests
```bash
🔴 test(auth): add failing tests for login

Created tests for:
- Valid credentials
- Invalid credentials
- Token expiration

Status: All failing (RED phase)
@TEST:EX-AUTH-001
```

**refactor** - Refactorización
```bash
♻️ refactor(auth): improve code quality

Improvements:
- Extracted validation methods
- Added dependency injection
- Improved error handling
- Coverage now 87%

@CODE:EX-AUTH-001
```

**docs** - Documentación
```bash
📚 docs(auth): sync documentation

Updated:
- README.md with auth feature
- API documentation
- CHANGELOG.md

@DOC:EX-AUTH-001
```

**fix** - Bug fix
```bash
🐛 fix(auth): correct token expiration

Fixed bug where tokens expired immediately
instead of after 24 hours.

Closes #42
```

**chore** - Tareas de mantenimiento
```bash
🔧 chore(deps): update dependencies

Updated:
- BCrypt.Net to 0.1.0
- FluentAssertions to 6.12.0
```

### Emojis

| Emoji | Tipo | Cuándo usar |
|-------|------|-------------|
| 🔴 | test | RED phase (failing tests) |
| 🟢 | feat | GREEN phase (minimal implementation) |
| ♻️ | refactor | REFACTOR phase (quality improvements) |
| 📚 | docs | Documentation changes |
| 🐛 | fix | Bug fixes |
| 🔧 | chore | Maintenance tasks |
| ⚡ | perf | Performance improvements |
| 🔒 | security | Security fixes |

### Scope
- auth, user, admin, api, core, db
- Corresponde al dominio de la SPEC

### Reglas
- Línea de subject: ≤72 caracteres
- Imperativo ("add" no "added")
- Sin punto final en subject
- Body explica QUÉ y POR QUÉ (no CÓMO)
- Footer para referencias (@CODE:, Closes #)

---

## Personal Mode Workflow

### 1. Crear feature branch

```bash
spec_id="AUTH-001"
git checkout -b "feature/SPEC-${spec_id}"
```

### 2. TDD Cycle (3 commits)

**RED:**
```bash
# Escribir tests que fallan
git add tests/
git commit -m "🔴 test(auth): add failing tests

Created tests for login functionality.
All tests failing as expected (RED phase).

@TEST:EX-${spec_id}"
```

**GREEN:**
```bash
# Implementación mínima
git add src/
git commit -m "🟢 feat(auth): implement login

Minimal implementation to make tests pass.
All tests now passing (GREEN phase).

@CODE:EX-${spec_id}"
```

**REFACTOR:**
```bash
# Mejorar calidad
git add src/ tests/
git commit -m "♻️ refactor(auth): improve code quality

Refactored for TRUST 5:
- Added dependency injection
- Improved error handling
- Coverage: 87%

@CODE:EX-${spec_id}"
```

### 3. Sync docs

```bash
git add README.md docs/ CHANGELOG.md
git commit -m "📚 docs(auth): sync documentation

Updated documentation:
- README.md with feature description
- docs/api.md with endpoints
- CHANGELOG.md with entry

@DOC:EX-${spec_id}"
```

### 4. Merge to main

```bash
git checkout main
git pull origin main

# Merge con --no-ff para preservar historia
git merge --no-ff "feature/SPEC-${spec_id}" -m "feat: complete SPEC-${spec_id}

Merged feature branch with full implementation:
- Tests (🔴 RED)
- Implementation (🟢 GREEN)
- Refactoring (♻️ REFACTOR)
- Documentation (📚 DOCS)

SPEC: docs/specs/SPEC-${spec_id}/spec.md
TAG chain: @SPEC → @TEST → @CODE → @DOC complete"

git push origin main
```

### 5. Cleanup

```bash
# Borrar branch local
git branch -d "feature/SPEC-${spec_id}"

# Borrar branch remoto
git push origin --delete "feature/SPEC-${spec_id}"
```

---

## Team Mode Workflow

### 1. Crear feature branch

```bash
spec_id="AUTH-001"
git checkout -b "feature/SPEC-${spec_id}"
```

### 2. Implementar (3-4 commits)

```bash
# RED
git commit -m "🔴 test(auth): add failing tests..."

# GREEN
git commit -m "🟢 feat(auth): implement login..."

# REFACTOR
git commit -m "♻️ refactor(auth): improve quality..."

# DOCS
git commit -m "📚 docs(auth): sync documentation..."
```

### 3. Push branch

```bash
git push -u origin "feature/SPEC-${spec_id}"
```

### 4. Create Draft PR

```bash
spec_file="docs/specs/SPEC-${spec_id}/spec.md"
title=$(grep "^title:" "$spec_file" | cut -d: -f2- | xargs)

gh pr create \
    --draft \
    --title "[SPEC] ${spec_id}: ${title}" \
    --body "## SPEC
[${spec_id}](docs/specs/SPEC-${spec_id}/spec.md)

## Implementation
- ✅ Tests written (🔴 RED)
- ✅ Code implemented (🟢 GREEN)
- ✅ Refactored (♻️ REFACTOR)
- ✅ Documentation synced (📚 DOCS)

## Quality Gate
- ✅ Coverage: ≥85%
- ✅ Tests passing: 100%
- ✅ TRUST 5: Validated
- ✅ TAG chain: Complete

## TAG Chain
\`@SPEC:EX-${spec_id}\` → \`@TEST:EX-${spec_id}\` → \`@CODE:EX-${spec_id}\` → \`@DOC:EX-${spec_id}\`

## Next Steps
1. Review implementation
2. Mark as 'Ready for review'
3. Request approval
4. Merge to main"
```

### 5. Code Review

```bash
# Marcar como ready for review
gh pr ready <pr-number>

# Asignar reviewers
gh pr edit <pr-number> --add-reviewer @teammate

# Aprobar PR (reviewer)
gh pr review <pr-number> --approve

# Merge PR
gh pr merge <pr-number> --squash
```

---

## Merge Strategies

### --no-ff (No Fast-Forward)

**Personal mode - recomendado**

```bash
git merge --no-ff feature/SPEC-AUTH-001
```

**Ventajas:**
- Preserva historia de feature
- Fácil revertir feature completa
- Claro en git log

**Resultado:**
```
*   Merge feature/SPEC-AUTH-001
|\
| * 📚 docs(auth): sync
| * ♻️ refactor(auth): improve quality
| * 🟢 feat(auth): implement
| * 🔴 test(auth): add tests
|/
* Previous commit
```

### --squash

**Team mode con PR - opcional**

```bash
gh pr merge <pr-number> --squash
```

**Ventajas:**
- Historia limpia en main
- Un commit por feature
- Fácil bisect

**Desventajas:**
- Pierde commits individuales
- Difícil ver progreso TDD

---

## Resolución de Conflictos

### Detectar conflictos

```bash
git merge feature/SPEC-AUTH-001

# Si hay conflictos:
Auto-merging src/Auth/AuthService.cs
CONFLICT (content): Merge conflict in src/Auth/AuthService.cs
Automatic merge failed; fix conflicts and then commit.
```

### Resolver

```bash
# Ver archivos en conflicto
git status

# Abrir archivo y resolver
# Buscar markers: <<<<<<<, =======, >>>>>>>

# Después de resolver
git add src/Auth/AuthService.cs
git commit -m "merge: resolve conflicts in AuthService"
```

### Abort merge

```bash
# Si quieres cancelar el merge
git merge --abort
```

---

## Tags y Releases

### Crear tag

```bash
# Tag con versión
git tag -a v1.0.0 -m "Release v1.0.0

Features:
- Authentication (SPEC-AUTH-001)
- User management (SPEC-USER-001)

Changes:
- See CHANGELOG.md"

git push origin v1.0.0
```

### Listar tags

```bash
git tag -l
# v1.0.0
# v1.1.0
# v2.0.0
```

### Borrar tag

```bash
git tag -d v1.0.0
git push origin --delete v1.0.0
```

---

## Comandos Útiles

### Ver historia

```bash
# Log con gráfico
git log --graph --oneline --all

# Log de un archivo
git log --follow src/Auth/AuthService.cs

# Log con TAGs
git log --grep="@CODE:EX-AUTH"

# Commits por autor
git log --author="mjcuadrado"
```

### Branch management

```bash
# Ver branches
git branch -a

# Borrar branch local
git branch -d feature/SPEC-AUTH-001

# Borrar branch remoto
git push origin --delete feature/SPEC-AUTH-001

# Limpiar branches mergeadas
git branch --merged | grep -v "main" | xargs git branch -d
```

### Stash

```bash
# Guardar cambios temporalmente
git stash

# Listar stashes
git stash list

# Aplicar último stash
git stash pop

# Aplicar stash específico
git stash apply stash@{0}
```

### Reset y Revert

```bash
# Deshacer último commit (mantener cambios)
git reset --soft HEAD~1

# Deshacer último commit (perder cambios)
git reset --hard HEAD~1

# Revertir commit (crear nuevo commit)
git revert <commit-hash>
```

---

## Hooks

### Pre-commit

```bash
#!/bin/bash
# .git/hooks/pre-commit

echo "Running pre-commit checks..."

# Run tests
dotnet test
if [ $? -ne 0 ]; then
    echo "❌ Tests failing. Commit aborted."
    exit 1
fi

# Check coverage
coverage=$(dotnet test --collect:"XPlat Code Coverage" | grep -oP '\d+\.\d+%' | head -1)
if (( $(echo "$coverage < 85" | bc -l) )); then
    echo "❌ Coverage $coverage < 85%. Commit aborted."
    exit 1
fi

echo "✅ Pre-commit checks passed"
```

### Commit-msg

```bash
#!/bin/bash
# .git/hooks/commit-msg

commit_msg=$(cat $1)

# Verificar formato
if ! echo "$commit_msg" | grep -qE "^(feat|fix|docs|test|refactor|chore)(\(.+\))?:.+"; then
    echo "❌ Invalid commit message format"
    echo "Expected: <type>(<scope>): <subject>"
    exit 1
fi

echo "✅ Commit message valid"
```

---

## .gitignore

```gitignore
# .NET
bin/
obj/
*.dll
*.exe
*.pdb

# User-specific files
*.user
*.suo
*.userosscache

# Build results
[Dd]ebug/
[Rr]elease/
x64/
x86/

# Test results
TestResults/
*.trx
coverage*.xml
coverage*.json

# IDE
.vs/
.vscode/
.idea/
*.swp

# OS
.DS_Store
Thumbs.db

# Secrets
appsettings.Development.json
*.secrets.json
.env

# Logs
logs/
*.log

# Temporary
tmp/
temp/
*.tmp
```

---

## Mejores Prácticas

### Commits

- ✅ Commits pequeños y frecuentes
- ✅ Un commit = un cambio lógico
- ✅ Mensajes descriptivos
- ✅ Referencias a TAGs (@CODE:, @TEST:)
- ❌ NO commits gigantes
- ❌ NO "WIP", "fix", "update" sin contexto
- ❌ NO commits con código comentado

### Branches

- ✅ Branches corta duración (<1 semana)
- ✅ Nombres descriptivos (feature/SPEC-ID)
- ✅ Borrar después de merge
- ❌ NO branches de larga duración
- ❌ NO branches con nombres vagos
- ❌ NO acumular branches viejas

### Merge

- ✅ Siempre --no-ff en personal mode
- ✅ Pull antes de merge
- ✅ Resolver conflictos cuidadosamente
- ❌ NO force push a main
- ❌ NO merge sin revisar cambios
- ❌ NO ignorar conflictos

---

## Troubleshooting

### Error: Branch diverged

```bash
# Ver diferencias
git log origin/main..main
git log main..origin/main

# Si tu main está adelante
git push origin main

# Si remote está adelante
git pull --rebase origin main
```

### Error: Merge conflict

```bash
# Abortar y reintentar
git merge --abort
git pull origin main
git merge feature/SPEC-AUTH-001
```

### Error: Accidental commit to main

```bash
# Mover commit a nueva branch
git branch feature/SPEC-AUTH-001
git reset --hard origin/main
git checkout feature/SPEC-AUTH-001
```

---

## Referencias

- [Git Documentation](https://git-scm.com/doc)
- [GitHub Flow](https://docs.github.com/en/get-started/quickstart/github-flow)
- [Conventional Commits](https://www.conventionalcommits.org/)
- [GitFlow](https://nvie.com/posts/a-successful-git-branching-model/)

---

## Resumen

**Git en mj2:**

**Personal Mode:**
- feature/SPEC-{ID} branches
- 3-4 commits (RED, GREEN, REFACTOR, DOCS)
- Merge --no-ff a main
- Cleanup branches

**Team Mode:**
- Same branches y commits
- Push y crear Draft PR
- Code review
- Merge con approval

**Commits = TDD phases + emoji + TAG references**

**Historia limpia = proyecto mantenible.**
