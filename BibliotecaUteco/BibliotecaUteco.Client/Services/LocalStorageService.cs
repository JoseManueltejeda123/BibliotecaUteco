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

    public async Task<string?> GetTokenAsync()
    {
        return await jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenName);
    }

    public async Task RemoveTokenAsync()
    {
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenName);
    }

}