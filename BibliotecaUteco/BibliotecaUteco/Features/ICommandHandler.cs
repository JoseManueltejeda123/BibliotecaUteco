namespace BibliotecaUteco.Features;

public interface ICommandHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    public Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}