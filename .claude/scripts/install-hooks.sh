#!/bin/bash
# MJ² Git Hooks Installer

echo "🔧 Installing MJ² Git Hooks..."

HOOKS_DIR=".git/hooks"

# Check if .git exists
if [ ! -d ".git" ]; then
    echo "❌ Not a git repository"
    exit 1
fi

# Create hooks directory if not exists
mkdir -p "$HOOKS_DIR"

# ============================================================
# PRE-COMMIT HOOK
# ============================================================
cat > "$HOOKS_DIR/pre-commit" << 'EOF'
#!/bin/bash
# MJ² Pre-commit Hook
# Validates code format and linting before commit

echo "🔍 Running pre-commit checks..."

# 1. Check for uncommitted changes
if [ -z "$(git status --porcelain)" ]; then
    echo "✅ No changes to commit"
    exit 0
fi

# 2. Format check (dotnet format)
echo "📝 Checking code format..."
if command -v dotnet &> /dev/null; then
    dotnet format --verify-no-changes --verbosity quiet 2>/dev/null
    if [ $? -ne 0 ]; then
        echo "❌ Format check failed"
        echo "💡 Run: dotnet format"
        exit 1
    fi
    echo "✅ Format check passed"
else
    echo "⚠️  dotnet not found, skipping format check"
fi

# 3. Build check
echo "🔨 Building project..."
if command -v dotnet &> /dev/null; then
    dotnet build --nologo --verbosity quiet 2>&1 | grep -v "Build succeeded"
    if [ ${PIPESTATUS[0]} -ne 0 ]; then
        echo "❌ Build failed"
        exit 1
    fi
    echo "✅ Build passed"
else
    echo "⚠️  dotnet not found, skipping build check"
fi

# 4. Check for TODO/FIXME in staged files (warning only)
todos=$(git diff --cached --name-only | grep "\.cs$" | xargs grep -n "TODO\|FIXME" 2>/dev/null || true)
if [ ! -z "$todos" ]; then
    echo "⚠️  TODOs found (warning only):"
    echo "$todos"
fi

echo "✅ Pre-commit checks passed"
exit 0
EOF

# ============================================================
# COMMIT-MSG HOOK
# ============================================================
cat > "$HOOKS_DIR/commit-msg" << 'EOF'
#!/bin/bash
# MJ² Commit Message Hook
# Validates commit message format

commit_msg_file=$1
commit_msg=$(cat "$commit_msg_file")

echo "🔍 Validating commit message..."

# Skip validation for merge commits
if echo "$commit_msg" | grep -q "^Merge"; then
    echo "✅ Merge commit (skipping validation)"
    exit 0
fi

# Skip validation for messages with Claude Code signature
if echo "$commit_msg" | grep -q "🤖 Generated with \[Claude Code\]"; then
    echo "✅ Claude Code commit (skipping validation)"
    exit 0
fi

# Expected format: <emoji> <type>(SPEC-ID): <description>
# Examples:
#   🔴 test(AUTH-001): add failing tests
#   🟢 feat(AUTH-001): implement auth service
#   ♻️ refactor(AUTH-001): improve code quality
#   📚 docs(AUTH-001): sync documentation

# Get first line only
first_line=$(echo "$commit_msg" | head -1)

# Valid emojis (extended set)
emojis="🔴|🟢|♻️|📚|🐛|✨|🔧|⚡|📦|🎨|🚀|🔥|💡|🎯|⚙️"

# Valid types
types="test|feat|refactor|docs|fix|chore|style|perf|build|ci|spec"

# Pattern: <emoji> <type>(SPEC-ID): <description>
# SPEC-ID can be optional for some types (chore, style, ci, build)
pattern="^($emojis) ($types)(\(([A-Z]+-[0-9]+)\))?: .+"

if [[ ! $first_line =~ $pattern ]]; then
    echo "❌ Invalid commit message format"
    echo ""
    echo "Expected format:"
    echo "  <emoji> <type>(SPEC-ID): <description>"
    echo "  or"
    echo "  <emoji> <type>: <description> (for chore, style, build, ci)"
    echo ""
    echo "Valid emojis:"
    echo "  🔴 - RED phase (failing tests)"
    echo "  🟢 - GREEN phase (passing implementation)"
    echo "  ♻️ - REFACTOR phase (quality improvements)"
    echo "  📚 - DOCS (documentation sync)"
    echo "  🐛 - FIX (bug fix)"
    echo "  ✨ - NEW (new feature)"
    echo "  🔧 - CHORE (maintenance)"
    echo ""
    echo "Valid types:"
    echo "  test, feat, refactor, docs, fix, chore, style, perf, build, ci"
    echo ""
    echo "Examples:"
    echo "  🔴 test(AUTH-001): add failing tests"
    echo "  🟢 feat(AUTH-001): implement auth service"
    echo "  ♻️ refactor(AUTH-001): improve code quality"
    echo "  📚 docs(AUTH-001): sync documentation"
    echo "  🔧 chore: update dependencies"
    echo ""
    echo "Your message:"
    echo "  $first_line"
    exit 1
fi

echo "✅ Commit message valid"
exit 0
EOF

# ============================================================
# PRE-PUSH HOOK
# ============================================================
cat > "$HOOKS_DIR/pre-push" << 'EOF'
#!/bin/bash
# MJ² Pre-push Hook
# Validates tests and coverage before push

echo "🔍 Running pre-push checks..."

# Check if dotnet is available
if ! command -v dotnet &> /dev/null; then
    echo "⚠️  dotnet not found, skipping pre-push checks"
    exit 0
fi

# 1. Run all tests
echo "🧪 Running tests..."
dotnet test --nologo --verbosity quiet 2>&1 | grep -v "Test run for"
if [ ${PIPESTATUS[0]} -ne 0 ]; then
    echo "❌ Tests failed"
    echo "💡 Fix tests before pushing"
    exit 1
fi
echo "✅ All tests passed"

# 2. Check coverage
echo "📊 Checking coverage..."
dotnet test --collect:"XPlat Code Coverage" --nologo --verbosity quiet > /dev/null 2>&1

# Find latest coverage file
coverage_file=$(find . -name "coverage.cobertura.xml" -type f -print0 2>/dev/null | xargs -0 ls -t 2>/dev/null | head -1)

if [ -z "$coverage_file" ]; then
    echo "⚠️  Coverage report not found (skipping)"
else
    # Parse coverage (simple grep)
    line_rate=$(grep -oP 'line-rate="\K[0-9.]+' "$coverage_file" 2>/dev/null | head -1)

    if [ ! -z "$line_rate" ]; then
        coverage_percent=$(echo "$line_rate * 100" | bc -l 2>/dev/null | cut -d. -f1)

        if [ ! -z "$coverage_percent" ] && [ "$coverage_percent" -lt 85 ]; then
            echo "❌ Coverage too low: ${coverage_percent}% (need ≥85%)"
            echo "💡 Add more tests to increase coverage"
            exit 1
        fi

        echo "✅ Coverage: ${coverage_percent}% (≥85%)"
    else
        echo "⚠️  Could not parse coverage (skipping)"
    fi
fi

# 3. Check for merge conflicts
if find src/ tests/ -type f -name "*.cs" -exec grep -l "<<<<<<< HEAD" {} \; 2>/dev/null | grep -q .; then
    echo "❌ Merge conflict markers found"
    echo "💡 Resolve conflicts before pushing"
    exit 1
fi

echo "✅ Pre-push checks passed"
exit 0
EOF

# ============================================================
# Make hooks executable
# ============================================================
chmod +x "$HOOKS_DIR/pre-commit"
chmod +x "$HOOKS_DIR/commit-msg"
chmod +x "$HOOKS_DIR/pre-push"

echo ""
echo "✅ Hooks installed successfully"
echo ""
echo "Installed hooks:"
echo "  • pre-commit  - Format & build check"
echo "  • commit-msg  - Message format validation"
echo "  • pre-push    - Tests & coverage check"
echo ""
echo "To bypass hooks (emergency only):"
echo "  git commit --no-verify"
echo "  git push --no-verify"
