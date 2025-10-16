using BibliotecaUteco.Client.Services.ApiServicesInterfaces;
using LumexUI.Common;

namespace BibliotecaUteco.Client.Services.ApiServices;

using Microsoft.AspNetCore.Components;

public class DialogService : IDialogService
{
    public event Action? OnDialogChanged;

    public bool IsOpen { get; private set; }
    public string? Title { get; private set; }
    public string? Body { get; private set; }
    public string AcceptText { get; private set; } = "Aceptar";
    public string CancelText { get; private set; } = "Cancelar";
    public RenderFragment? ChildContent { get; private set; }
    
    public ThemeColor CancelColor { get; set; }  = ThemeColor.Default;
    public ThemeColor AcceptColor { get; set; } = ThemeColor.Danger;

    private TaskCompletionSource<bool>? _tcs;

    // Mostrar el diálogo y esperar resultado
    public Task<bool> ShowAsync(string title, string body, string? acceptText = null, string? cancelText = null, ThemeColor cancelColor = ThemeColor.Default, ThemeColor acceptColor = ThemeColor.Danger, RenderFragment? childContent = null)
    {
        Title = title;
        Body = body;
        AcceptText = acceptText ?? "Aceptar";
        CancelText = cancelText ?? "Cancelar";
        ChildContent = childContent;
        IsOpen = true;
        AcceptColor = acceptColor;
        CancelColor = cancelColor;
        OnDialogChanged?.Invoke();
        _tcs = new TaskCompletionSource<bool>();
        return _tcs.Task;
    }

    public void Accept()
    {
        Close(true);
    }

    public void Cancel()
    {
        Close(false);
    }

    private void Close(bool result)
    {
        if (!IsOpen) return;

        IsOpen = false;
        OnDialogChanged?.Invoke();
        _tcs?.TrySetResult(result);
    }
}
