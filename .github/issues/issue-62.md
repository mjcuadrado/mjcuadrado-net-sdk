# Issue #62: MAUI & Blazor Skills

**Fecha:** 2025-11-23
**Prioridad:** 🟡 Media
**Estado:** 📋 Planificado
**Versión:** v0.9.0
**Branch:** feature/ISSUE-062-maui-blazor-skills
**Tiempo Estimado:** 7 días

---

## 📋 Descripción

Crear skills de **MAUI** y **Blazor** para .NET mobile y SPA.

**Gap identificado:** moai-adk tiene mobile skills. mj2 tiene React pero falta .NET mobile (MAUI) y Blazor.

---

## 🎯 Objetivos

### Skills (4 skills)
1. `.claude/skills/frontend/maui.md` (~450 líneas)
   - .NET MAUI fundamentals
   - Cross-platform (iOS, Android, Windows, macOS)
   - MVVM pattern
   - Platform-specific code

2. `.claude/skills/frontend/blazor-server.md` (~400 líneas)
   - Blazor Server architecture
   - SignalR connection
   - State management
   - Performance

3. `.claude/skills/frontend/blazor-wasm.md` (~400 líneas)
   - Blazor WebAssembly
   - PWA support
   - AOT compilation
   - Interop with JavaScript

4. `.claude/skills/frontend/blazor-hybrid.md` (~350 líneas)
   - Blazor Hybrid (MAUI + Blazor)
   - WebView integration
   - Native capabilities
   - Deployment

---

## 📦 Entregables

### 1. maui.md
```csharp
// MAUI App
public class App : Application
{
    public App()
    {
        MainPage = new AppShell();
    }
}

// Platform-specific
#if ANDROID
    // Android code
#elif IOS
    // iOS code
#endif
```

### 2. blazor-wasm.md
```csharp
// Component
@page "/counter"
<h1>Counter</h1>
<p>Count: @count</p>
<button @onclick="Increment">+</button>

@code {
    private int count = 0;
    void Increment() => count++;
}
```

### 3. blazor-hybrid.md
```xml
<!-- MAUI + Blazor -->
<BlazorWebView HostPage="wwwroot/index.html">
    <RootComponents>
        <RootComponent Selector="#app" ComponentType="{x:Type local:Main}" />
    </RootComponents>
</BlazorWebView>
```

---

## ✅ Criterios de Éxito

- [ ] 4 skills creados (~1,600 líneas)
- [ ] MAUI examples (cross-platform)
- [ ] Blazor Server examples
- [ ] Blazor WASM examples (PWA)
- [ ] Blazor Hybrid examples
- [ ] Deployment guides
- [ ] Performance tips

---

## 🔗 Referencias

- **MAUI:** https://learn.microsoft.com/dotnet/maui
- **Blazor:** https://learn.microsoft.com/aspnet/core/blazor
- **Integration:** frontend-builder, component-designer

---

## 🚀 Impacto

**Sin MAUI & Blazor:**
- ❌ No .NET mobile
- ❌ No .NET SPA
- ❌ React only

**Con MAUI & Blazor:**
- ✅ .NET mobile apps
- ✅ .NET SPAs (Server & WASM)
- ✅ Code sharing C#
- ✅ Full-stack .NET

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Prioridad:** 🟡 MEDIA
**Milestone:** v0.9.0
