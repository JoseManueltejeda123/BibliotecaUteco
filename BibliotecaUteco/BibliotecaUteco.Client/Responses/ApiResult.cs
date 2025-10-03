using System.Diagnostics.CodeAnalysis;

namespace BibliotecaUteco.Client.Responses;




public interface IApiResult
{
    public int StatusCode => (int)Status;
    public bool IsSuccess { get; set; } 
    public string StatusCodeName => nameof(StatusCode);
    
    public List<string> Messages { get; set; }
    
    public HttpStatus Status { get; set; }

   
}


public class ApiResult<T>(
    T? data,
    HttpStatus status = HttpStatus.OK,
    string message = "Ok",
    List<string>? messages = null)
    : IApiResult
{
    public T? Data { get; set; } = data;

    [MemberNotNullWhen(true, nameof(Data))]
    public bool IsSuccessful() => Data is not null && IsSuccess;
    public bool IsSuccess { get; set; }
    public List<string> Messages { get; set; } = messages ?? [message];
    public HttpStatus Status { get; set; } = status;
}












public enum HttpStatus
{
  

    // 2xx - Success
    OK = 200,
    Created = 201,
    Accepted = 202,
    NoContent = 204,
 
    Found = 302,
    NotModified = 304,

    // 4xx - Client errors
    BadRequest = 400,
    Unauthorized = 401,
    PaymentRequired = 402,
    Forbidden = 403,
    NotFound = 404,
    MethodNotAllowed = 405,
    NotAcceptable = 406,
    RequestTimeout = 408,
    Conflict = 409,
    LengthRequired = 411,
    PreconditionFailed = 412,
    PayloadTooLarge = 413,
    UnsupportedMediaType = 415,
    RangeNotSatisfiable = 416,
    ExpectationFailed = 417,
    UnprocessableEntity = 422,
    TooManyRequests = 429,

    // 5xx - Server errors
    InternalServerError = 500,
    ServiceUnavailable = 503,
    GatewayTimeout = 504,
    InsufficientStorage = 507,
    LoopDetected = 508,
}




