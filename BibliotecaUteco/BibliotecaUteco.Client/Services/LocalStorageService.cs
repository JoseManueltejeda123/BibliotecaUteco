using BibliotecaUteco.Client.ServicesInterfaces;
using Microsoft.JSInterop;

namespace BibliotecaUteco.Client.Services;

public class LocalStorageService(IJSRuntime jsRuntime) : ILocalStorageService
{
    private const string TokenName = "BibliotecaUtecoJwt";
    public async Task SaveTokenAsync(string token)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenName, token);
    }
    
    public async Task SaveTokenAsync(string token, bool remindMe)
    {
        if (remindMe)
        {
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenName, token);
            return;

        }
        
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", TokenName, token);

    }

    public async Task<string?> GetTokenAsync()
    {
        
        
        
        var sessionToken = await jsRuntime.InvokeAsync<string?>("sessionStorage.getItem", TokenName);

        if (sessionToken is not null)
        {
            return sessionToken;
        }
        
        
       return await jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenName);
       
    }

    public async Task RemoveTokenAsync()
    {
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenName);
        await jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", TokenName);

    }

}