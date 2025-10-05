namespace BibliotecaUteco.Client.ServicesInterfaces;

public interface ILocalStorageService
{
    Task SaveTokenAsync(string token);
    Task SaveTokenAsync(string token, bool remindme);

    Task<string?> GetTokenAsync();
    Task RemoveTokenAsync();
}