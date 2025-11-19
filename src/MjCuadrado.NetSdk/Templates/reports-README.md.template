# Reports

Esta carpeta almacena reportes generados automáticamente por el SDK.

## Tipos de reportes

### Coverage reports
Reportes de cobertura de tests generados con:
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=html
```

### Build reports
Información sobre builds:
- Warnings encontrados
- Errores compilación
- Tiempo de compilación

### Validation reports
Resultados de validaciones automáticas:
- Validación de SPECs
- Validación de TAGs
- Validación de estructura de proyecto

### Metrics reports
Métricas del código:
- Complejidad ciclomática
- Líneas de código
- Duplicación de código

## Comandos relacionados (futuros)

```bash
# Generar reporte de coverage
mjcuadrado-net-sdk report coverage

# Generar reporte de SPECs
mjcuadrado-net-sdk report specs

# Generar reporte de métricas
mjcuadrado-net-sdk report metrics
```

## Archivos generados

Los reportes se generan en formato HTML y JSON para facilitar su lectura y procesamiento automático.

```
reports/
├── coverage/
│   └── index.html
├── specs/
│   └── validation-report.json
└── metrics/
    └── code-metrics.json
```

## .gitignore

Típicamente los reportes no se commitan al repositorio. Asegúrate de que `reports/` esté en `.gitignore`.
