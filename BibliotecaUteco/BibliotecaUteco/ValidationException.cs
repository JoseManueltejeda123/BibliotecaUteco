namespace BibliotecaUteco;

public class ValidationException(List<string> errors) : System.Exception("La validación de la petición falló")
{
    public List<string> Errors { get; } = errors;

    public override string ToString()
    {
        return $"ValidationException: {string.Join("; ", Errors)}";
    }
}