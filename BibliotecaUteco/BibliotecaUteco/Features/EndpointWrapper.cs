using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Features;

public class EndpointWrapper<TEndpoint>(ILogger<TEndpoint> logger) : IEndpointWrapper<TEndpoint> where TEndpoint : IEndpoint
{
    
    public async Task<IResult> ExecuteAsync<TResponse>(Func<Task<IApiResult>> func)
    {
        try
        {
            logger.LogInformation($"Llamando al endpoint: {typeof(TEndpoint).Name} ");

            return BuildResult(await func());
        }
        catch (ValidationException ex)
        {

            logger.LogInformation($"La llamada al endpoint: {typeof(TEndpoint).Name} falló por que la request tiene campos inválidos");
            return BuildResult(ApiResult<object>.BuildFailure(HttpStatus.UnprocessableEntity,"Peticion invalida", ex.Errors));

        }
        catch (Exception ex)
        {
            
            logger.LogInformation($"La llamada al endpoint: {typeof(TEndpoint).Name} falló: ${ex.InnerException?.Message ?? ex.Message}");
            return BuildResult(ApiResult<object>.BuildFailure(HttpStatus.InternalServerError,"Error en el servidor"));



        }
       
    }

    private IResult BuildResult(IApiResult apiResult)
    {

        return apiResult.Status switch
        {
            HttpStatus.BadRequest =>
                Results.BadRequest(apiResult),
            HttpStatus.NotFound =>
                Results.NotFound(apiResult),
            HttpStatus.OK =>
                Results.Ok(apiResult),
            HttpStatus.Unauthorized =>
                Results.Unauthorized(),
            HttpStatus.InternalServerError =>
                Results.InternalServerError(apiResult),
            HttpStatus.Forbidden =>
                Results.Json(apiResult, contentType: ApplicationContentTypes.ApplicationJson,
                    statusCode: StatusCodes.Status403Forbidden),
            HttpStatus.UnprocessableEntity =>
                Results.UnprocessableEntity(apiResult),
            _ =>
                Results.BadRequest(apiResult)

        };

    }
    
    
}