# Issue #58: Kubernetes & IaC Skills

**Fecha:** 2025-11-23
**Prioridad:** 🟡 Media
**Estado:** 📋 Planificado
**Versión:** v0.7.0
**Branch:** feature/ISSUE-058-k8s-iac-skills
**Tiempo Estimado:** 7 días

---

## 📋 Descripción

Crear skills de **Kubernetes** e **Infrastructure as Code** para DevOps avanzado.

**Gap identificado:** moai-adk tiene estos skills. mj2 tiene Docker pero falta orquestación y IaC.

---

## 🎯 Objetivos

### Skills (4 skills)
1. `.claude/skills/tools/kubernetes.md` (~500 líneas)
   - Pods, Deployments, Services
   - ConfigMaps, Secrets
   - Ingress, LoadBalancer
   - Helm charts

2. `.claude/skills/tools/helm.md` (~350 líneas)
   - Chart structure
   - Values.yaml
   - Release management
   - Chart repositories

3. `.claude/skills/tools/terraform.md` (~450 líneas)
   - HCL syntax
   - Providers (Azure, AWS)
   - State management
   - Modules

4. `.claude/skills/tools/bicep.md` (~350 líneas)
   - Azure IaC
   - Bicep vs ARM
   - Modules
   - Best practices

---

## 📦 Entregables

### 1. kubernetes.md
```yaml
# Deployment
apiVersion: apps/v1
kind: Deployment
metadata:
  name: myapp
spec:
  replicas: 3
  template:
    spec:
      containers:
      - name: myapp
        image: myapp:latest
```

### 2. helm.md
```yaml
# values.yaml
replicaCount: 3
image:
  repository: myapp
  tag: latest
```

### 3. Integration
- Docker → Kubernetes
- Azure App Service → AKS
- GitHub Actions → K8s deployment

---

## ✅ Criterios de Éxito

- [ ] 4 skills creados (~1,650 líneas)
- [ ] K8s manifests examples
- [ ] Helm charts templates
- [ ] Terraform modules
- [ ] Bicep templates
- [ ] CI/CD integration

---

## 🔗 Referencias

- **K8s Docs:** https://kubernetes.io/docs
- **Helm:** https://helm.sh
- **Terraform:** https://terraform.io
- **Bicep:** https://learn.microsoft.com/azure/azure-resource-manager/bicep

---

## 🚀 Impacto

**Sin K8s & IaC:**
- ❌ No orchestration
- ❌ Manual infrastructure
- ❌ No reproducibility

**Con K8s & IaC:**
- ✅ Container orchestration
- ✅ Infrastructure as Code
- ✅ Reproducible deploys
- ✅ GitOps ready

---

**Versión:** 1.0.0
**Creado:** 2025-11-23
**Prioridad:** 🟡 MEDIA
**Milestone:** v0.7.0
