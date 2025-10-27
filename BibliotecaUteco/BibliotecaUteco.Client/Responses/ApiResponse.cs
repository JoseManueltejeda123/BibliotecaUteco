using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace BibliotecaUteco.Client.Responses;

public interface IApiResult
{
    public bool IsSuccess { get; set; }

    public List<string> Messages { get; set; }

    public HttpStatus Status { get; set; }
}

public class ApiResponse<T> : IApiResult
{
    public T? Data { get; set; } = default;
    public bool IsSuccess { get; set; } = false;
    public List<string> Messages { get; set; } = new();
    public HttpStatus Status { get; set; } = HttpStatus.BadRequest;

    [JsonIgnore]
    public int StatusCode => (int)Status;

    [MemberNotNullWhen(true, nameof(Data))]
    public bool IsSuccessful() => Data is not null && IsSuccess;
}

public enum HttpStatus
{
    // 2xx - Success
    OK = 200,

    // 4xx - Client errors
    BadRequest = 400,
    Unauthorized = 401,
    PaymentRequired = 402,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409,
    UnprocessableEntity = 422,
    TooManyRequests = 429,

    // 5xx - Server errors
    InternalServerError = 500,
    ServiceUnavailable = 503,
    GatewayTimeout = 504,
}
