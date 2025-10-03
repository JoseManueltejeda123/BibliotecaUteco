using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Features;

public interface IEndpointWrapper<TEndpoint> where TEndpoint : IEndpoint
{

    public Task<IResult> ExecuteAsync<TResponse>(Func<Task<IApiResult>> func);
}