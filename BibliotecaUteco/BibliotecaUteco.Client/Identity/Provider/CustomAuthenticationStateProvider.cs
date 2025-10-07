using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BibliotecaUteco.Client.ServicesInterfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BibliotecaUteco.Client.Identity.Provider;

public class CustomAuthenticationStateProvider(ILocalStorageService localStorageService, NavigationManager navManager) : AuthenticationStateProvider
{
  
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorageService.GetTokenAsync();

        if (string.IsNullOrWhiteSpace(token))
            return new AuthenticationState(_anonymous);

        var handler = new JwtSecurityTokenHandler();

        if (!handler.CanReadToken(token))
            return new AuthenticationState(_anonymous);

        var deserializedToken = handler.ReadJwtToken(token);

        if (deserializedToken.ValidTo < DateTime.UtcNow)
        {
            await localStorageService.RemoveTokenAsync();
            return new AuthenticationState(_anonymous);
        }

        var identity = new ClaimsIdentity(deserializedToken.Claims, "Bearer", "nickname", "role");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    /// <summary>
    /// Actualiza el estado de autenticación.
    /// Si token es null, se cierra sesión.
    /// </summary>
    public async Task UpdateAuthenticationStateAsync(string? token, bool remindMe = false)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            await localStorageService.RemoveTokenAsync();

        }
        else
        {
          
            await localStorageService.SaveTokenAsync(token, remindMe);

        }
        
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    }
}