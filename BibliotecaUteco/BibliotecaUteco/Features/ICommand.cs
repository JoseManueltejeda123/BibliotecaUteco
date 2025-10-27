using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BibliotecaUteco.Features;

public interface ICommand<TResponse> { }

public abstract class CommandWithUserCredentials
{
    [JsonIgnore, BindNever]
    public int CurrentUserId { get; private set; }

    public void SetCurrentUserId(int userId)
    {
        CurrentUserId = userId;
    }
}
