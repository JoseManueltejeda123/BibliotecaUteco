using BibliotecaUteco.Client.Requests.Readers.Actions;
using BibliotecaUteco.Client.Responses;

namespace BibliotecaUteco.Client.Services.ApiServicesInterfaces;

public interface IReadersApiServices
{
    Task<ApiResult<ReaderResponse>> CreateAsync(CreateReaderRequest request);
}