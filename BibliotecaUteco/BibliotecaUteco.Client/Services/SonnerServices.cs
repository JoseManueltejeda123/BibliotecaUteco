using BibliotecaUteco.Client.ServicesInterfaces;

namespace BibliotecaUteco.Client.Services;

public class SonnerService : ISonnerService
{
    public event Action<Sonner>? OnSonnerAdded;


    private TaskCompletionSource<object?>? _tcs;

    private Task StartSonner(Sonner sonner)
    {
        OnSonnerAdded?.Invoke(sonner);
        _tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        return Task.CompletedTask;

    }

    public Task Show(string title, string description, SonnerTheme theme = SonnerTheme.Info)
    {
        return StartSonner(
            new()
            {
                Title = title,
                Description = description,
                Theme = theme,
            }
        );
    }

    public Task Show(string description, SonnerTheme theme = SonnerTheme.Info)
    {
        return StartSonner(new() { Description = description, Theme = theme });
    }

    public Task Success(string title, string description, SonnerTheme theme = SonnerTheme.Success)
    {
        return StartSonner(
            new()
            {
                Title = title,
                Description = description,
                Theme = theme,
            }
        );
    }

    public Task Success(string description, SonnerTheme theme = SonnerTheme.Success)
    {
        return StartSonner(new() { Description = description, Theme = theme });
    }

    public Task Warning(string title, string description, SonnerTheme theme = SonnerTheme.Warning)
    {
        return StartSonner(
            new()
            {
                Title = title,
                Description = description,
                Theme = theme,
            }
        );
    }

    public Task Warning(string description, SonnerTheme theme = SonnerTheme.Warning)
    {
        return StartSonner(new() { Description = description, Theme = theme });
    }

    public Task Error(string title, string description, SonnerTheme theme = SonnerTheme.Danger)
    {
        return StartSonner(
            new()
            {
                Title = title,
                Description = description,
                Theme = theme,
            }
        );
    }

    public Task Error(string description, SonnerTheme theme = SonnerTheme.Danger)
    {
        return StartSonner(new() { Description = description, Theme = theme });
    }

    // Call this from the UI component when the sonner is dismissed to unblock awaiting callers.
    public void Dismiss()
    {
        if (_tcs is not null)
        {
            _tcs.TrySetResult(null);
            _tcs = null;
        }
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
