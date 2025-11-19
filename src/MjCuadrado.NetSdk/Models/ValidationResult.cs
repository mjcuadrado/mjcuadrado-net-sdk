namespace MjCuadrado.NetSdk.Models;

/// <summary>
/// Resultado de una validación
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// Indica si la validación fue exitosa
    /// </summary>
    public bool IsValid => Errors.Count == 0;

    /// <summary>
    /// Lista de errores encontrados durante la validación
    /// </summary>
    public List<ValidationError> Errors { get; set; } = new();

    /// <summary>
    /// Agrega un error de validación
    /// </summary>
    public void AddError(string field, string message)
    {
        Errors.Add(new ValidationError { Field = field, Message = message });
    }

    /// <summary>
    /// Combina los resultados de otra validación
    /// </summary>
    public void Merge(ValidationResult other)
    {
        Errors.AddRange(other.Errors);
    }
}

/// <summary>
/// Error de validación individual
/// </summary>
public class ValidationError
{
    /// <summary>
    /// Campo que falló la validación
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// Mensaje de error
    /// </summary>
    public string Message { get; set; } = string.Empty;

    public override string ToString() => $"{Field}: {Message}";
}
