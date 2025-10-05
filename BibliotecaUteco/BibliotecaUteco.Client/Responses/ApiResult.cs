using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace BibliotecaUteco.Client.Responses;




public interface IApiResult
{
    public bool IsSuccess { get; set; } 
    
    public List<string> Messages { get; set; }
    
    public HttpStatus Status { get; set; }

   
}


public class ApiResult<T> : IApiResult
{
  
    public T? Data { get; set; } = default;
    public bool IsSuccess { get; set; } = false;
    public List<string> Messages { get; set; } = new();
    public HttpStatus Status { get; set; } = HttpStatus.BadRequest;

    [JsonIgnore]
    public int StatusCode => (int)Status;

    [MemberNotNullWhen(true, nameof(Data))]
    public bool IsSuccessful() => Data is not null && IsSuccess;


   
    
    public static ApiResult<T> BuildSuccess(T? data, HttpStatus status = HttpStatus.OK, string message = "Success",
        List<string> messages = null) => new()
    {
        Status = status,
        Messages = messages ?? [message],
        IsSuccess = true,
        Data = data
    };
    
    public static ApiResult<T> BuildFailure(HttpStatus status = HttpStatus.BadRequest, string message = "Success",
        List<string> messages = null) => new()
    {
        Status = status,
        Messages = messages ?? [message],
        IsSuccess = false,
        Data = default
    };
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




