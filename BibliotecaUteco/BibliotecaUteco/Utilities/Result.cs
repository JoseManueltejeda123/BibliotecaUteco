namespace BibliotecaUteco.Utilities;

public class SuccessApiResult<T>(T data, string message = "Success", HttpStatus status = HttpStatus.OK,  List<string>? messages = null) : IApiResult
{
    public bool IsSuccess { get; set; } = true;
    public List<string> Messages { get; set; } = messages ?? [message];
    public HttpStatus Status { get; set; } = status;
    public T Data { get; set; } = data;
    
    [JsonIgnore]
    public int StatusCode => (int)Status;

}

public abstract class Failure( HttpStatus status = HttpStatus.BadRequest, string message = "Bad Request",  List<string>? messages = null) : IApiResult
{
    public bool IsSuccess { get; set; } = false;
    public List<string> Messages { get; set; } =  messages ?? [message];
    public HttpStatus Status { get; set; } = status;
    
    [JsonIgnore]
    public int StatusCode => (int)Status;

}


public class BadRequestApiResult(string message = "Bad request", List<string>? messages = null)
    : Failure(HttpStatus.BadRequest, message, messages);

public class UnauthorizedApiResult(string message = "Unauthorized", List<string>? messages = null)
    : Failure(HttpStatus.Unauthorized, message, messages);

public class ForbiddenApiResult(string message = "Forbidden", List<string>? messages = null)
    : Failure(HttpStatus.Forbidden, message, messages);

public class NotFoundApiResult(string message = "Resource not found", List<string>? messages = null)
    : Failure(HttpStatus.NotFound, message, messages);

public class ConflictApiResult(string message = "Conflict", List<string>? messages = null)
    : Failure(HttpStatus.Conflict, message, messages);

public class UnprocessableEntityApiResult(string message = "Unprocessable entity", List<string>? messages = null)
    : Failure(HttpStatus.UnprocessableEntity, message, messages);

public class TooManyRequestsApiResult(string message = "Too many requests", List<string>? messages = null)
    : Failure(HttpStatus.TooManyRequests, message, messages);

public class InternalServerErrorApiResult(string message = "Internal server error", List<string>? messages = null)
    : Failure(HttpStatus.InternalServerError, message, messages);

public class ServiceUnavailableApiResult(string message = "Service unavailable", List<string>? messages = null)
    : Failure(HttpStatus.ServiceUnavailable, message, messages);

public class GatewayTimeoutApiResult(string message = "Gateway timeout", List<string>? messages = null)
    : Failure(HttpStatus.GatewayTimeout, message, messages);
