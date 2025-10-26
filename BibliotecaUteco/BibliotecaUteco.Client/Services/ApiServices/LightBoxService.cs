using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaUteco.Client.Services.ApiServicesInterfaces;
namespace BibliotecaUteco.Client.Services.ApiServices
{

    public class LightBoxService : ILightBoxService
    {
        public event Action? OnLightBoxChanged;

        public bool IsOpen { get; private set; } = false;
        public string? PhotoTitle { get; private set; }
        public string? PhotoUrl { get; private set; }

        private TaskCompletionSource? _tcs;

        public Task ShowAsync(
            string title,
            string? photoUrl = null
        )
        {
            PhotoTitle = title;
            PhotoUrl = photoUrl;
            IsOpen = true;
            OnLightBoxChanged?.Invoke();
            _tcs = new TaskCompletionSource();
            return _tcs.Task;
        }



        public void Cancel()
        {
            Close(false);
        }

        private void Close(bool result)
        {
            if (!IsOpen)
                return;

            IsOpen = false;
            OnLightBoxChanged?.Invoke();
            _tcs?.TrySetResult();
        }
    }

}