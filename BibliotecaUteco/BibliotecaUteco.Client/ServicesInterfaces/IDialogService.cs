using LumexUI.Common;
using Microsoft.AspNetCore.Components;

namespace BibliotecaUteco.Client.Services.ApiServicesInterfaces;

public interface IDialogService
{
    event Action? OnDialogChanged;
    bool IsOpen { get; }
    string? Title { get; }
    string? Body { get; }
    string AcceptText { get; }
    string CancelText { get; }

    ThemeColor CancelColor { get; } 
    ThemeColor AcceptColor { get; set; } 
    RenderFragment? ChildContent { get; }

    Task<bool> ShowAsync(string title, string body, string? acceptText = null, string? cancelText = null,
        ThemeColor cancelColor = ThemeColor.Default, ThemeColor acceptColor = ThemeColor.Danger,
        RenderFragment? childContent = null);
    void Accept();
    void Cancel();
}