# Issue #57: Azure Cloud Skills

**Fecha:** 2025-11-23
**Prioridad:** 🔴 Alta
**Estado:** 📋 Planificado
**Versión:** v0.7.0
**Branch:** feature/ISSUE-057-azure-skills
**Tiempo Estimado:** 7 días

---

## 📋 Descripción

Crear skills de **Azure Cloud** críticos para stack .NET.

**Gap identificado:** moai-adk tiene cloud skills. mj2 no tiene Azure, que es crítico para .NET.

---

## 🎯 Objetivos

### Azure Skills (4 skills)
1. `.claude/skills/cloud/azure-fundamentals.md` (~450 líneas)
   - Azure Resource Groups
   - Azure CLI & PowerShell
   - ARM Templates
   - Bicep

2. `.claude/skills/cloud/azure-app-service.md` (~400 líneas)
   - Web Apps deployment
   - App Service Plans
   - Deployment slots
   - Auto-scaling

3. `.claude/skills/cloud/azure-functions.md` (~350 líneas)
   - Serverless .NET
   - HTTP/Timer triggers
   - Durable Functions
   - Application Insights

4. `.claude/skills/cloud/azure-sql.md` (~400 líneas)
   - Azure SQL Database
   - Connection strings
   - Geo-replication
   - Elastic pools

---

## 📦 Entregables

### 1. azure-fundamentals.md
```csharp
// Azure Resource Group
az group create --name rg-myapp --location eastus

// Bicep deployment
az deployment group create \
  --resource-group rg-myapp \
  --template-file main.bicep
```

### 2. azure-app-service.md
```csharp
// Deploy to Azure
dotnet publish -c Release
az webapp deploy \
  --resource-group rg-myapp \
  --name myapp \
  --src-path ./publish.zip
```

### 3. Integration con DevOps
- Azure Pipeline templates
- GitHub Actions Azure deployment
- Terraform Azure modules

---

## ✅ Criterios de Éxito

- [ ] 4 Azure skills creados (~1,600 líneas)
- [ ] Deployment examples
- [ ] CI/CD integration
- [ ] Security best practices
- [ ] Cost optimization tips
- [ ] Documentación en español

---

## 🔗 Referencias

- **Azure Docs:** https://learn.microsoft.com/azure
- **Integration:** devops-expert, database-expert
- **Tools:** Azure CLI, Bicep, Terraform

---

## 🚀 Impacto

**Sin Azure skills:**
- ❌ No hay guidance para .NET cloud
- ❌ Manual Azure configuration
- ❌ Deployment errors

**Con Azure skills:**
- ✅ Azure deployment automatizado
- ✅ Best practices .NET + Azure
- ✅ CI/CD completo
- ✅ Production-ready

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Prioridad:** 🔴 ALTA (.NET stack crítico)
**Milestone:** v0.7.0
