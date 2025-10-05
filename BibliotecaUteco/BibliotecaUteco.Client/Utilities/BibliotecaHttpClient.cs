using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using BibliotecaUteco.Client.Identity.Provider;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Client.ServicesInterfaces;
using Blazor.Sonner.Common;
using Blazor.Sonner.Services;
using Microsoft.AspNetCore.Components;

namespace BibliotecaUteco.Client.Utilities;

public class BibliotecaHttpClient (
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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    private async Task<ApiResult<TResult>> ProcessResult<TResult>(HttpResponseMessage response)
    {
        try
        {
            var apiResult = await response.Content.ReadFromJsonAsync<ApiResult<TResult>>();
          
        
            if (apiResult == null)
            {
                toast.Show("Oops", new ToastModel()
                {
                    Description = "Hubo un problema. Intenta mas luego",
                    Title = "Oops!",
                    Type = ToastType.Error
                    
                });
                return ApiResult<TResult>.BuildFailure(   
                    HttpStatus.BadRequest,
                    "Hubo un problema al deserializar la respuesta"
                   
                );
            }

            if (apiResult.Status != HttpStatus.OK)
            {
                foreach(var message in apiResult.Messages)
                {
                    toast.Show("Error", new ToastModel()
                    {
                        Description = message,
                        Title = "Error",
                        Type = ToastType.Error
                    
                    });
                }
              
            }

            if (apiResult.Status == HttpStatus.Unauthorized)
            {
               
                await authState.UpdateAuthenticationStateAsync(null); 
            }

            return apiResult;
        }
        catch(Exception ex)
        {
            toast.Show("Oops", new ToastModel()
            {
                Description = ex.InnerException?.Message ?? ex.Message,
                Title = "Oops!",
                Type = ToastType.Error,
                
                    
            });
            return  ApiResult<TResult>.BuildFailure(HttpStatus.BadRequest, "Tuvimos un problema. Intentalo mas tarde");
        }
      
    }

    public async Task<ApiResult<TResult>> FetchGetAsync<TResult>(string route)
    {
        await AttachTokenAsync();

        var response = await client.GetAsync(Prefix + route);
        return await ProcessResult<TResult>(response);

    }

    public async Task<ApiResult<TResult>> FetchPostAsync<TResult>(string route, object data)
    {
        await AttachTokenAsync();

        var response = await client.PostAsJsonAsync(Prefix + route, data);
        return await ProcessResult<TResult>(response);

    }

    public async Task<ApiResult<TResult>> FetchPutAsync<TResult>(string route, object data)
    {
        await AttachTokenAsync();

        var response = await client.PutAsJsonAsync(Prefix + route, data);
        return await ProcessResult<TResult>(response);

    }

    public async Task<ApiResult<TResult>> FetchDeleteAsync<TResult>(string route)
    {
        await AttachTokenAsync();
        var response = await client.DeleteAsync(Prefix + route);
       
        return await ProcessResult<TResult>(response);
    }
}