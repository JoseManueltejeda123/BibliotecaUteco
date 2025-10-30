using BibliotecaUteco.Client.Services;

namespace BibliotecaUteco.Client.ServicesInterfaces;

public interface ISonnerService
{
    event Action<Sonner>? OnSonnerAdded;
    Task Show(string title, string description, SonnerTheme theme = SonnerTheme.Info);
    Task Show(string description, SonnerTheme theme = SonnerTheme.Info);
    Task Success(string title, string description, SonnerTheme theme = SonnerTheme.Success);
    Task Success(string description, SonnerTheme theme = SonnerTheme.Success);
    Task Warning(string title, string description, SonnerTheme theme = SonnerTheme.Warning);
    Task Warning(string description, SonnerTheme theme = SonnerTheme.Warning);
    Task Error(string title, string description, SonnerTheme theme = SonnerTheme.Danger);
    Task Error(string description, SonnerTheme theme = SonnerTheme.Danger);
}
