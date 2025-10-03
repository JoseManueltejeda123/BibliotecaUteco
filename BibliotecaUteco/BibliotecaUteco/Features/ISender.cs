namespace BibliotecaUteco.Features;

public interface ISender
{
    Task<TResponse> SendAndValidateAsync<TResponse>(
        ICommand<TResponse> command,
        CancellationToken cancellationToken = default
    );
}