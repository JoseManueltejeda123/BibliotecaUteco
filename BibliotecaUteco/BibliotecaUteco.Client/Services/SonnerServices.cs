using BibliotecaUteco.Client.ServicesInterfaces;

namespace BibliotecaUteco.Client.Services;

public class SonnerService : ISonnerService
{
    public event Action<Sonner>? OnSonnerAdded;

    private TaskCompletionSource? _tcs;

    public Task Show(string title, string description, SonnerTheme theme = SonnerTheme.Info)
    {
        OnSonnerAdded?.Invoke(
            new()
            {
                Title = title,
                Description = description,
                Theme = theme,
            }
        );
        _tcs = new TaskCompletionSource();
        return _tcs.Task;
    }

    public Task Show(string description, SonnerTheme theme = SonnerTheme.Info)
    {
        OnSonnerAdded?.Invoke(new() { Description = description, Theme = theme });
        _tcs = new TaskCompletionSource();
        return _tcs.Task;
    }

    public Task Success(string title, string description, SonnerTheme theme = SonnerTheme.Success)
    {
        OnSonnerAdded?.Invoke(
            new()
            {
                Title = title,
                Description = description,
                Theme = theme,
            }
        );
        _tcs = new TaskCompletionSource();
        return _tcs.Task;
    }

    public Task Success(string description, SonnerTheme theme = SonnerTheme.Success)
    {
        OnSonnerAdded?.Invoke(new() { Description = description, Theme = theme });
        _tcs = new TaskCompletionSource();
        return _tcs.Task;
    }

    public Task Warning(string title, string description, SonnerTheme theme = SonnerTheme.Warning)
    {
        OnSonnerAdded?.Invoke(
            new()
            {
                Title = title,
                Description = description,
                Theme = theme,
            }
        );
        _tcs = new TaskCompletionSource();
        return _tcs.Task;
    }

    public Task Warning(string description, SonnerTheme theme = SonnerTheme.Warning)
    {
        OnSonnerAdded?.Invoke(new() { Description = description, Theme = theme });
        _tcs = new TaskCompletionSource();
        return _tcs.Task;
    }

    public Task Error(string title, string description, SonnerTheme theme = SonnerTheme.Danger)
    {
        OnSonnerAdded?.Invoke(
            new()
            {
                Title = title,
                Description = description,
                Theme = theme,
            }
        );
        _tcs = new TaskCompletionSource();
        return _tcs.Task;
    }

    public Task Error(string description, SonnerTheme theme = SonnerTheme.Danger)
    {
        OnSonnerAdded?.Invoke(new() { Description = description, Theme = theme });
        _tcs = new TaskCompletionSource();
        return _tcs.Task;
    }
}

public enum SonnerTheme
{
    Danger,
    Info,
    Warning,
    Success,
}

public class Sonner
{
    public string? Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public SonnerTheme Theme { get; set; } = SonnerTheme.Info;
}
