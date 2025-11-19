# Comando: doctor

Verifica las dependencias del sistema y la salud del proyecto actual.

## Sintaxis

```bash
mjcuadrado-net-sdk doctor [opciones]
```

## Opciones

- `--verbose`: Muestra información detallada de cada check

## Ejemplo

```bash
mjcuadrado-net-sdk doctor
mjcuadrado-net-sdk doctor --verbose
```

## Checks realizados

1. **.NET SDK**: Verifica versión ≥ 9.0
2. **Git**: Verifica instalación y configuración
3. **Estructura del proyecto**: Valida carpetas necesarias
4. **Permisos**: Verifica permisos de escritura
5. **Espacio en disco**: Verifica mínimo 100MB disponibles

## Output esperado

```
Diagnóstico del sistema

✓ .NET SDK: 10.0.0
✓ Git: 2.43.0 (configurado)
✓ Estructura de proyecto: OK
✓ Permisos de escritura: OK
✓ Espacio en disco: 5.2 GB disponibles

Todo listo para empezar! 🚀
```

## Troubleshooting

### .NET SDK no encontrado
```bash
# Instalar .NET SDK desde:
https://dotnet.microsoft.com/download
```

### Git no configurado
```bash
git config --global user.name "Tu Nombre"
git config --global user.email "tu@email.com"
```

### Falta estructura de proyecto
```bash
# Inicializar proyecto
mjcuadrado-net-sdk init
```

## Códigos de salida

- `0`: Todos los checks pasaron
- `1`: Uno o más checks fallaron
