# Comando: init

Inicializa un nuevo proyecto con la estructura completa de mjcuadrado-net-sdk.

## Sintaxis

```bash
mjcuadrado-net-sdk init [nombre-proyecto] [opciones]
```

## Argumentos

- `[nombre-proyecto]` (opcional): Nombre del proyecto a crear. Si se omite, inicializa en el directorio actual.

## Opciones

- `--force`: Sobrescribe archivos existentes si el proyecto ya existe
- `--author <nombre>`: Especifica el autor del proyecto (default: `@user`)

## Ejemplos

### Crear nuevo proyecto en carpeta nueva
```bash
mjcuadrado-net-sdk init mi-proyecto
```

### Inicializar en directorio actual
```bash
cd mi-proyecto-existente
mjcuadrado-net-sdk init
```

### Sobrescribir proyecto existente
```bash
mjcuadrado-net-sdk init mi-proyecto --force
```

### Especificar autor
```bash
mjcuadrado-net-sdk init mi-proyecto --author "@mjcuadrado"
```

## Comportamiento

1. Valida el nombre del proyecto (caracteres permitidos)
2. Verifica permisos de escritura
3. Crea la estructura de carpetas completa
4. Genera archivos desde templates
5. Crea `config.json` con valores por defecto
6. Muestra resumen de carpetas creadas

## Estructura generada

```
[nombre-proyecto]/
├── .mjcuadrado-net-sdk/
│   ├── config.json
│   ├── project/
│   │   ├── product.md
│   │   ├── structure.md
│   │   └── tech.md
│   ├── specs/
│   ├── memory/
│   └── reports/
└── .claude/
    ├── agents/
    ├── commands/
    ├── skills/
    └── hooks/
```

## Validaciones

- Nombre de proyecto no contiene caracteres especiales: `/\:*?"<>|`
- Permisos de escritura en el directorio
- Espacio suficiente en disco (mínimo 100MB)
- Si el proyecto existe, requiere `--force`

## Códigos de salida

- `0`: Éxito
- `1`: Error (permisos, nombre inválido, etc.)

## Ver también

- [Comando doctor](doctor.md)
- [Configuración del proyecto](../architecture/overview.md)
