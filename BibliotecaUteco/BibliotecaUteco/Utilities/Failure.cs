using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Utilities;


public abstract class Failure : IApiResult
{
    public bool IsSuccess { get; set; } = false;
    public List<string> Messages { get; set; } 
    public HttpStatus Status { get; set; }

    public void Build(HttpStatus status, string message = "Bad Request", List<string>? messages = null)
    {
        Messages = messages ?? [message];
        Status = status;
    }
 
}

public class BadRequestApiResult : Failure
{

    public BadRequestApiResult(string message = "La petición no tiene la estructura correcta", List<string>? messages = null)
    {
        Build(HttpStatus.BadRequest, message, messages);
    }
    
}

public class InternalServerErroApiResult : Failure
{

    public InternalServerErroApiResult(string message = "Ocurrió un error en la aplicación. Intenta denuevo.", List<string>? messages = null)
    {
        Build(HttpStatus.InternalServerError, message, messages);
    }
    
}

public class NotFoundApiResult : Failure
{

    public NotFoundApiResult(string message = "El recurso no pudo ser encontrado", List<string>? messages = null)
    {
        Build(HttpStatus.NotFound, message, messages);
    }
    
}


public class UnauthorizedApiResult : Failure
{

    public UnauthorizedApiResult(string message = "No estas autorizado para acceder a este recurso", List<string>? messages = null)
    {
        Build(HttpStatus.Unauthorized, message, messages);
    }
    
}

public class ValidationApiResult : Failure
{

    public ValidationApiResult(string message = "La request no fue válida o alguno de sus campos no es válido", List<string>? messages = null)
    {
        Build(HttpStatus.UnprocessableEntity, message, messages);
    }
    
}
