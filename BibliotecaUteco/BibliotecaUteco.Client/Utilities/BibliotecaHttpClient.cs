using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BibliotecaUteco.Client.Identity.Provider;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.ServicesInterfaces;
using Blazor.Sonner.Common;
using Blazor.Sonner.Services;

namespace BibliotecaUteco.Client.Utilities;

public class BibliotecaHttpClient(
    HttpClient client,
    ILocalStorageService localStorageService,
    CustomAuthenticationStateProvider authState,
    ToastService toast
)
{
    public string Prefix { get; set; } = "api/v1";
    
    private async Task AttachTokenAsync()
    {
        var token = await localStorageService.GetTokenAsync();
        client.DefaultRequestHeaders.Authorization = null; // limpiar antes de asignar
        if (!string.IsNullOrWhiteSpace(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                token
            );
        }
    }

    public void ShowErrorToast(string message)
    {
        toast.Error(
            "Oops",
            new ToastModel()
            {
                Description = message,
                Title = "Oops!",
                Type = ToastType.Error,
                Position = ToastPosition.BottomCenter,
            }
        );
    }

    private async Task<ApiResponse<TResult>> ProcessResult<TResult>(
        HttpResponseMessage response,
        CancellationToken cancellationToken = default
    )
    {
         ApiResponse<TResult> failure = new ApiResponse<TResult>()
                    {
                        Data = default,
                        IsSuccess = false,
                        Messages = ["Tuvimos un problema al hacer esta peticion"],
                        Status = HttpStatus.BadRequest
                            
                    }; 
        try
        {
           
            
            var jsonContent = await response.Content.ReadAsStringAsync(cancellationToken);
            

            var apiResult = JsonSerializer.Deserialize<ApiResponse<TResult>>(
                jsonContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            
            if (apiResult is null)
            {
               
                ShowErrorToast("El servidor retornó una respuesta vacía");
                return failure;

            }

            if (!response.IsSuccessStatusCode)
            {
                foreach (var message in apiResult.Messages)
                {
                    ShowErrorToast(message);
                }
            }

            if (apiResult.Status == HttpStatus.Unauthorized)
            {
                ShowErrorToast("Su sessión ha expirado");
                await authState.UpdateAuthenticationStateAsync(null);
            }

            return apiResult;
        }
        catch (Exception ex)
        {
            ShowErrorToast(ex.InnerException?.Message ?? ex.Message);
            return failure;
        }
    }

    public async Task<ApiResponse<TResult>> FetchGetAsync<TResult>(
        string route,
        CancellationToken cancellationToken = default
    )=> await ProcessResult<TResult>(await CallAsync<TResult>(HttpMethod.GET, route, token: cancellationToken), cancellationToken);

    public async Task<ApiResponse<TResult>> FetchPostAsync<TResult>(
        string route,
        object data,
        CancellationToken cancellationToken = default
    )=>  await ProcessResult<TResult>(await CallAsync<TResult>(HttpMethod.POST, route, data, cancellationToken), cancellationToken);
    

    public async Task<ApiResponse<TResult>> FetchPutAsync<TResult>(
        string route,
        object data,
        CancellationToken cancellationToken = default
    )=>  await ProcessResult<TResult>(await CallAsync<TResult>(HttpMethod.PUT, route, data, cancellationToken), cancellationToken);

    public async Task<ApiResponse<TResult>> FetchDeleteAsync<TResult>(
        string route,
        CancellationToken cancellationToken = default
    ) => await ProcessResult<TResult>(await CallAsync<TResult>(HttpMethod.DELETE, route, token: cancellationToken), cancellationToken);
    
    
    public async Task<HttpResponseMessage> CallAsync<TResponse>(HttpMethod method, string route, object? body = null, CancellationToken token = default)
    {
        try
        {
            await AttachTokenAsync();
            return method switch
            {
                HttpMethod.GET => await client.GetAsync(Prefix + route, token),
                HttpMethod.DELETE => await client.DeleteAsync(Prefix + route, token),
                HttpMethod.POST => body is MultipartFormDataContent multipart
                    ? await client.PostAsync(Prefix + route, multipart, token)
                    : await client.PostAsJsonAsync(Prefix + route, body, token),
                HttpMethod.PUT => body is MultipartFormDataContent multipart
                    ? await client.PutAsync(Prefix + route, multipart, token)
                    : await client.PutAsJsonAsync(Prefix + route, body, token),
                _ => throw new NotImplementedException("No existe el metodo pedido")

            };
        }
        catch (Exception ex)
        {
            var apiResponse = new ApiResponse<TResponse>
            {
                IsSuccess = false,
                Messages = [ex.InnerException?.Message ?? ex.Message],
                Status = HttpStatus.BadRequest,
                Data = default
            };

            var json = JsonSerializer.Serialize(apiResponse);
    
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }

    }
}



public enum HttpMethod
{
    GET,
    PUT,
    POST,
    DELETE
}