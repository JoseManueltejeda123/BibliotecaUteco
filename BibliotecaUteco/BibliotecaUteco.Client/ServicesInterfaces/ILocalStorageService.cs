namespace BibliotecaUteco.Client.ServicesInterfaces;

public interface ILocalStorageService
{
    Task SaveTokenAsync(string token);
    Task<string?> GetTokenAsync();
    Task RemoveTokenAsync();
}