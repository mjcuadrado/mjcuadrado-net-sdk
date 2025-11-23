# Issue #63: Advanced Testing Skills

**Fecha:** 2025-11-23
**Prioridad:** 🟡 Media
**Estado:** 📋 Planificado
**Versión:** v0.9.0
**Branch:** feature/ISSUE-063-advanced-testing-skills
**Tiempo Estimado:** 5-6 días

---

## 📋 Descripción

Crear skills de **testing avanzado**: load testing, contract testing, mutation testing.

**Gap identificado:** mj2 tiene unit, integration, E2E. Falta testing avanzado.

---

## 🎯 Objetivos

### Skills (3 skills)
1. `.claude/skills/testing/load-testing.md` (~450 líneas)
   - k6 (JavaScript load testing)
   - JMeter alternatives
   - Performance benchmarks
   - Stress testing

2. `.claude/skills/testing/contract-testing.md` (~400 líneas)
   - Consumer-driven contracts
   - Pact (.NET)
   - API contract validation
   - CI/CD integration

3. `.claude/skills/testing/mutation-testing.md` (~350 líneas)
   - Stryker.NET
   - Test quality validation
   - Coverage vs mutation score
   - CI/CD gates

---

## 📦 Entregables

### 1. load-testing.md
```javascript
// k6 script
import http from 'k6/http';
import { check } from 'k6';

export const options = {
  vus: 100,
  duration: '30s',
};

export default function() {
  const res = http.get('https://api.example.com');
  check(res, { 'status 200': (r) => r.status === 200 });
}
```

### 2. contract-testing.md
```csharp
// Pact consumer test
[Fact]
public async Task GetUser_Returns_User()
{
    _pactBuilder
        .UponReceiving("a request for user 1")
        .WithRequest(HttpMethod.Get, "/users/1")
        .WillRespond()
        .WithStatus(HttpStatusCode.OK)
        .WithJsonBody(new { id = 1, name = "John" });

    var client = new HttpClient { BaseAddress = _mockProviderService.BaseUri };
    var result = await client.GetAsync("/users/1");

    Assert.Equal(HttpStatusCode.OK, result.StatusCode);
}
```

### 3. mutation-testing.md
```bash
# Stryker.NET
dotnet stryker

# Output
Mutation score: 85%
Killed: 170/200
Survived: 30/200
```

---

## ✅ Criterios de Éxito

- [ ] 3 skills creados (~1,200 líneas)
- [ ] k6 load testing examples
- [ ] Pact contract testing examples
- [ ] Stryker mutation testing setup
- [ ] CI/CD integration
- [ ] Performance baselines
- [ ] Quality gates

---

## 🔗 Referencias

- **k6:** https://k6.io
- **Pact:** https://docs.pact.io
- **Stryker.NET:** https://stryker-mutator.io/docs/stryker-net
- **Integration:** testing/*, quality-gate

---

## 🚀 Impacto

**Sin advanced testing:**
- ❌ No load testing
- ❌ No contract validation
- ❌ Coverage ≠ quality

**Con advanced testing:**
- ✅ Performance validated
- ✅ API contracts guaranteed
- ✅ Test quality measured
- ✅ Production confidence

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Prioridad:** 🟡 MEDIA
**Milestone:** v0.9.0
