namespace BibliotecaUteco.Client.ServicesInterfaces
{
    public interface ILightBoxService
    {
        bool IsOpen { get; }
        string? PhotoTitle { get; }
        string? PhotoUrl { get; }

        event Action? OnLightBoxChanged;

        void Cancel();
        Task ShowAsync(string title, string? photoUrl = null);
    }
}
