# Comando: version

Muestra la versión del SDK instalado y del runtime .NET.

## Sintaxis

```bash
mjcuadrado-net-sdk version [opciones]
```

## Opciones

- `--verbose`: Muestra información detallada (OS, arquitectura, framework)

## Ejemplos

### Versión básica
```bash
mjcuadrado-net-sdk version
```

Output:
```
mjcuadrado-net-sdk v0.1.0
.NET 10.0.0
```

### Versión detallada
```bash
mjcuadrado-net-sdk version --verbose
```

Output:
```
mjcuadrado-net-sdk v0.1.0
.NET 10.0.0

┌──────────────┬─────────────────────┐
│ Property     │ Value               │
├──────────────┼─────────────────────┤
│ SDK Version  │ 0.1.0               │
│ .NET Runtime │ 10.0.0              │
│ OS           │ macOS 14.6          │
│ Architecture │ Arm64               │
│ Framework    │ .NET 10.0.0         │
└──────────────┴─────────────────────┘
```

## Uso típico

Verificar que tienes la última versión del SDK instalada antes de reportar bugs.
